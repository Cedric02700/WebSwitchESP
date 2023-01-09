/**************************************************************************************************
***                Protocole de communication avec le SWITCH                                    ***                           
***                                                                                             ***                
***  Le switch herberge un serveur Web. Les commande sont donc passée par URL sous la forme     ***
***  suivante: Http://adresseIPduSwitch/"commande".                                             ***  
*** Les relais sont numerotés par des lettres. 1->A, 2->B ...                                   ***                            
*** J'ai utilisé comme balise des lettre triples.                                               *** 
*** Liste des commandes:                                                                        ***
***   -Activer ou desactiver un relais: "Rne" où: "n" represente le numero du relais            ***
***                                               "e" sont etat A pour activer                  ***
***                                                             E pour desactiver               ***                                  
***                  Exemple: "Http://adresseIPduSwitch/RCA" active le troisieme relais         ***                             
***   -Changer le nom d'un relais: "RnXXXnomDuRelaisFFF" où: "n" represente le numero du relais ***       
***                                                          "XXX" la balise de debut           ***
***                                                          "FFF" la balise de fin             ***
***   -Savegarder la liste des noms de relais en EEprom: "RAZZZ"                                ***                                                            
***   -Pour recuperer le nom d'un relais: "RnNNN" où "n" represente le numero du relais         ***  
***   -Pour connaitre le nombre de relais pilotés par le switch : "RABBB"                       ***   
***                                                                                             ***
**************************************************************************************************/

/**********************************************************************************
***                    Variable à modifier                                      ***
**********************************************************************************/
#define WIFI //modifiez en "WIFI" ou "ETHERNET" suivant la version désirée

#ifdef WIFI
const char* ssid     = "";          //SSID de votre box
const char* password = "";          //Mote de passe de votre box
#endif

#ifdef ETHERNET
byte mac[] = { 0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0x28 };   //Adresse MAC de l'ENC28J60. Doit etre unique sur le reseau
#endif

int nbRelais = 14;                   //Nombre de relais à afficher sur l'interface Web
boolean doubleColonne = true;        //True: boutons de l'interface Web sur 2 colonnes, false: sur 1 colonne
int pinRelais[14] = {21, 13, 16, 12, 22, 14, 17, 27, 4, 26, 15, 25, 32, 33};  //Pins de l'ESP qui commande les relais
String nomRelais[14] =
{
  "Relais1",                      //Nom affichés sur les bouton de l'interface Web
  "Relais2",                      //36 caractères maxi par nom
  "Relais3",
  "Relais4",
  "Relais5",
  "Relais6",
  "Relais7",
  "Relais8",
  "Relais9",
  "Relais10",
  "Relais11",
  "Relais12",
  "Relais13",
  "Relais14"
};

/*******************************************************************************
***                     Bibliotheques                                        ***     
*******************************************************************************/

#include <EEPROM.h>
#ifdef WIFI
#include <WiFi.h>
#endif

#ifdef ETHERNET
#include <UIPEthernet.h>
#endif

/******************************************************************************
 ***                     Variables                                          ***
 *****************************************************************************/

boolean ecrireNoms = false;      //Consigne qui declenche l'ecriture des nom de relais en EEprom
boolean responseAscom = false;   //Ascom attend une reponse
String messageAscom;             //Stocke la mise en forme de la reponse au driver Ascom
boolean etatRelais[14] = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}; //Stocke l'etat des relais
String idRelais[14] = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N"}; //Identification des relais par une lettre. Plus simple pour les traitements en boucle
unsigned long timerMaintain;    //Timer requete DHCP

#ifdef ETHERNET
EthernetServer server(80);     //Declaration du serveur Web
#endif

#ifdef WIFI
WiFiServer server(80);     //Declaration du serveur Web
#endif

/*******************************************************************************
 ***                     void setup                                          ***
 ******************************************************************************/

void setup()
{
  for (int i = 0; i < nbRelais; i++)      //Decalartion des pins relais en sortie
  {
    pinMode(pinRelais[i], OUTPUT);
  }
  Serial.begin(115200);           //Demarre la liaison serie
  EEPROM.begin(512);              //Initialise l'EEprom

  /******************************************
  ****** Connexion a la box Wifi    *********
  ******************************************/
  #ifdef WIFI
  delay(10);
  Serial.println();
  Serial.println();
  Serial.print("Connecting to ");
  Serial.println(ssid);
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED)
  {
      delay(500);
      Serial.print(".");
  }
  Serial.println("");
  Serial.println("WiFi connected.");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());
  #endif

  /*****************************************
  ***      Connexion Ethernet            ***
  *****************************************/
  #ifdef ETHERNET
  Ethernet.begin(mac);  
  Serial.print("server is at ");
  Serial.println(Ethernet.localIP());
  timerMaintain = millis();
  #endif
  
  server.begin();                      //Demarrage du serveur Web
  
  /*************************************************
  *** Charge le nom des boutons depuis l'EEprom  ***
  *************************************************/
  for(int i = 1; i < 505; i += 36)
  {    
    char inputName[36];
    int e = i/36;
    EEPROM.get(i, inputName);
    nomRelais[(e)] = inputName;
    Serial.print("Bouton "); Serial.print(e); Serial.println(": " + nomRelais[e]);
  }
}

/************************************************************************************************
***                            void serverWeb()                                               ***
***     Gere l'affichage de l'interface Web, la gestion des requetes et des relais            ***
************************************************************************************************/

void serveurWeb()
{
  #ifdef ETHERNET
  EthernetClient client = server.available();   //Surveille le client Ethernet
  #endif
  #ifdef WIFI
  WiFiClient client = server.available();   //Surveille le client Wifi
  #endif
  if (client)                               //Si un client se connecte
  {
    Serial.println("New Client.");          //Debug
    String currentLine = "";                //Stocke les caractere entrant
    while (client.connected())              //Tant que le client est connecté
    {
      if (client.available())
      {                                     //Si des caracteres sont dispo
        char c = client.read();             //Lis un caractere
        Serial.write(c);                    //Debug, copie le caractere sur le port serie
        if (c == '\n')                      //Si le caractere est "new line".
        {
          /****************************************************************************
          ***   A la fin d'une requete client HTTP deux caractere "new lines" sont  ***
          *** envoyés. A la reception du premier, on efface currentLine. A la       ***
          *** recepion du deuxieme, le serveur affiche l'interface Web ou transmet  ***
          *** la reponse au driver Ascom suivant la requete.                        ***
          ****************************************************************************/
          if (currentLine.length() == 0)
          {
            if (responseAscom == false)                      //Affiche l'interface Web
            {
              client.println("HTTP/1.1 200 OK");             //Entete HTML
              client.println("Content-type:text/html");
              client.println();
              client.println("<title>Web Switch</title>");
              client.println("<h1>Web Switch<h1>");
              for (int i = 0; i < nbRelais; i++)            //Construit l'interface Web en fonction de l'etat des relais
              {
                String etatCommande;
                String couleurBouton;
                if (etatRelais[i] == 1)                     //Si le realais est alimenté le bouton sera vert
                {
                  couleurBouton = "green";
                  etatCommande = "E";
                }
                else                                        //Si le relais n'est pas alimenté le bouton sera rouge
                {
                  couleurBouton = "red";
                  etatCommande = "A";
                }
  /*********************************************
  *** Transmet un bouton par tour de boucle  ***
  *********************************************/
                client.print("<input type=submit value='" + nomRelais[i] + "' style=font-size:30px;width:40%;height:75px;margin-right:20px;margin-bottom:20px;background-color:" + couleurBouton + " onClick=location.href='/R" + idRelais[i] + etatCommande + "'>");
                if ((doubleColonne == true) && ((i % 2) > 0)) client.print("<br>"); //Si affichage sur deux colonne, affiche un retour a la ligne apres les numero de bouton impair
                else if (doubleColonne == false) client.print("<br>");              //Si affichage sur une colonne, imprime un retour a la ligne apres chaque bouton
              }
            }
            if (responseAscom == true)                          //Reponse a la demande du driver ASCOM
            {
              //Serial.println("Debug: " + messageAscom);
              client.println("HTTP/1.1 200 OK");
              client.println("Content-type:text/html");
              client.println();
              client.println(messageAscom);
              responseAscom = false;
            }
            client.println();    //La reponse HTTP se termine par une ligne vide
            break;   //Sort de la boucle
          }
          else                        //Vide currentLine à la reception du premier "newLine" de la requete du client
          {
            currentLine = "";
          }
        }
        else if (c != '\r')                      //Si le caratere n'est pas un "retour chariot"
        {
          currentLine += c;                      //Ajoute le caractere à currentLine
        }
        for (int i = 0; i < nbRelais; i++)       //Gere les requetes client
        {
          String requete = ("GET /R" + idRelais[i]);
          if (currentLine.endsWith(requete + "A"))             //Si le client veut activer un relais
          {
            if (etatRelais[i] == 0)
            {
              etatRelais[i] = 1 - etatRelais[i];
              digitalWrite(pinRelais[i], etatRelais[i]);
            }
          }
          if (currentLine.endsWith(requete + "E"))           //Si le client veut eteindre un relais
          {
            if (etatRelais[i] == 1)
            {
              etatRelais[i] = 1 - etatRelais[i];
              digitalWrite(pinRelais[i], etatRelais[i]);
            }
          }
          if (currentLine.endsWith("FFF"))                    //Si le client veut changer le nom d'un relais
          {
            int validBouton = currentLine.indexOf(requete);
            if (validBouton >= 0)
            {
              int indexDebut = currentLine.indexOf("XXX");
              int indexFin = currentLine.indexOf("FFF");
              String nomBouton = currentLine.substring((indexDebut + 3), indexFin);
              if ((nomBouton.indexOf("%20")) >= 0) nomBouton.replace("%20", " ");                    //mise en forme
              if ((nomBouton.indexOf("%0D%0A%0D%0A")) >= 0) nomBouton.replace("%0D%0A%0D%0A", "");   //mise en forme
              nomRelais[i] = nomBouton;
              //ecrireNoms = true;
            }
          }
          if (currentLine.endsWith("ZZZ"))                    //Signal pour inscrire le nom des boutons en EEprom
          {
            int validBouton = currentLine.indexOf(requete);
            if (validBouton >= 0)
            {
              ecrireNoms = true;
            }
          }          
          if (currentLine.endsWith("NNN"))                    //Si le client veut connaitre le nom d'un relais
          {
            int validBouton = currentLine.indexOf(requete);
            if (validBouton >= 0)
            {
              responseAscom = true;  //getName
              messageAscom = (nomRelais[i]);
            }
          }
          if (currentLine.endsWith("SSS"))                    //Si le client veut connaitre l'etat d'un relais
          {
            int validBouton = currentLine.indexOf(requete);
            if (validBouton >= 0)
            {
              responseAscom = true;   //getState
              messageAscom = (etatRelais[i]);
              //Serial.println("Debug: " + messageAscom);
            }
          }
          if (currentLine.endsWith("III"))                    //Si le client demande confirmation de l'ID
          {
            int validBouton = currentLine.indexOf(requete);
            if (validBouton >= 0)
            {
              responseAscom = true;   //getState
              messageAscom = ("ArduinoWebSwitch");
              //Serial.println("Debug: " + messageAscom);
            }
          }
          if (currentLine.endsWith("BBB"))                    //Si le client demande le nombre de relais pilotés
          {
            int validBouton = currentLine.indexOf(requete);
            if (validBouton >= 0)
            {
              responseAscom = true;   //getState
              messageAscom = String(nbRelais);
            }
          }
        }
      }
    }
    client.stop();   //Deconnecte le client
    Serial.println("Client Disconnected."); //Debug
  }
}

void EcrireNoms()
{
  for(int i = 1; i < 505; i += 36)
  {
    int e = i/36;
    char tampon[36] = "";
    nomRelais[e].toCharArray(tampon, 36);
    EEPROM.put(i, tampon);
  }
  EEPROM.commit();
  ecrireNoms = false;
  for(int i = 1; i < 505; i += 36)
  {    
    char inputName[36] = "";
    int e = i/36;
    EEPROM.get(i, inputName);
    nomRelais[(e)] = inputName;
    Serial.print("Bouton "); Serial.print(e); Serial.println(": " + nomRelais[e]);
  }
}

/*****************************************************************************
***                         void loop()                                    ***
*****************************************************************************/
void loop()
{
  serveurWeb();
  if(ecrireNoms == true) EcrireNoms();
  #ifdef ETHERNET
  if ((millis() - timerMaintain) > 3000)  //Requete serveur DHCP
  {
    int reponse = Ethernet.maintain();
    timerMaintain = millis();   
  }
  #endif
}
