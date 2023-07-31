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
 ***                    Variable à modifier                                     ***
 *********************************************************************************/
#define ETHERNET //modifiez en "WIFI" ou "ETHERNET" suivant la version désirée
#define TEMPO //Active la temporisation sur les relais 13 et 14

#ifdef WIFI
const char* ssid     = "SSID";                          //SSID de votre box
const char* password = "MotDePasse";          //Mote de passe de votre box
#endif

#ifdef ETHERNET
byte mac[] = { 0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0x28 };   //physical mac address
#endif

unsigned long tempo13Timer = 0;
unsigned long tempo14Timer = 0;
int tempoDelay = 2000; //Delais de la temporisation des relais 13 et 14 miliseconde
boolean tempo13IsRunning = false;
boolean tempo14IsRunning = false;

int nbRelais = 14;                                           //Nombre de relais à afficher sur l'interface Web
boolean doubleColonne = true;                               //True: boutons de l'interface Web sur 2 colonnes, false: sur 1 colonne
int pinRelais[14] = {21, 13, 16, 12, 22, 14, 17, 27, 4, 26, 15, 25, 32, 33};  //Pin de l'ESP qui commande les relais
String nomRelais[14] =
{
  "Relais 1",                      //Nom affichés sur les bouton de l'interface Web
  "Relais 2",                      //36 caractères maxi par nom
  "Relais 3",
  "Relais 4",
  "Relais 5",
  "Relais 6",
  "Relais 7",
  "Relais 8",
  "Relais 9",
  "Relais 10",
  "Relais 11",
  "Relais 12",
  "Relais 13",
  "Relais 14"
};



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

boolean responseAscom = false;   //Ascom attend une reponse
String messageAscom;          //Stocke la mise en forme de la reponse au driver Ascom
boolean etatRelais[14] = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}; //Stocke l'etat des relais
String idRelais[14] = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N"}; //Identification des relais par une lettre. Plus simple pour les traitements en boucle
unsigned long timerMaintain;
boolean ecrireNoms = false;  //Ne pas modifier!!!

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
  Serial.begin(115200);           //Demarre la liaison serie pour debug
  EEPROM.begin(510);

  /******************************************
  ****** Connection a la box Wifi   *********
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
  /*****************************************
  ***      Fin de la connection wifi     ***
  *****************************************/
  #endif

  #ifdef ETHERNET
  Serial.print("Setup.");
  Ethernet.begin(mac);  
  Serial.print("server is at ");
  Serial.println(Ethernet.localIP());
  timerMaintain = millis();
  #endif
  
  server.begin();   //Demarrage du serveur Web
  Serial.println("Serveur demarré");
  /**************************************************
  *** Gestion du nom des boutons depuis l'EEprom  ***
  **************************************************/
  char EEpromCopyNameKey[5] = "COPY";                         //Clé de 4 caractères utilisée pour ecrire les noms de bouton en EEprpm lors du premier televersement.
  char EEpromCopyNameLock[5];                                 //Ne pas modifier. Stocke la valeur de serrure. Si serrure et clef sont differents, l'ecriture des noms en EEprom est declenché.
  EEPROM.get(1, EEpromCopyNameLock);                          //Recupere la valeure de serrure depuis l'EEprom
  Serial.print("Key: "); Serial.println(EEpromCopyNameKey);   //Debug
  Serial.print("Lock: "); Serial.println(EEpromCopyNameLock); //Debug
  if(strcmp(EEpromCopyNameKey, EEpromCopyNameLock) != 0)      //Si clef et serrure differents, Memorisation des noms en EEprom
  {
    for(int i = 11; i < 501; i += 35)    //Boucle d'ecriture des noms
    {
      int e = i / 35;
      char tampon[35] = "";
      nomRelais[e].toCharArray(tampon, 35);
      EEPROM.put(i, tampon);
    }
    EEPROM.put(1, EEpromCopyNameKey);
    EEPROM.commit();
    Serial.println("Noms Boutons Sauvegardés");
  }
  else Serial.println("Noms boutons deja memorisés");  //Debug
  
  for(int i = 11; i < 501; i += 35)  //Boucle de lecture des noms pour debug
  {    
    char inputName[35];
    int e = i /35;
    EEPROM.get(i, inputName);
    nomRelais[(e)] = inputName;
    Serial.print("Bouton "); Serial.print(e); Serial.println(": " + nomRelais[e]);
  }
  Serial.println("Setup terminé");
}

/************************************************************************************************
***                            void serverWeb()                                               ***
***     Gere l'affichage de l'interface Web, la gestion des requetes et des relais            ***
************************************************************************************************/

void serveurWeb()
{
  #ifdef ETHERNET
  EthernetClient client = server.available();   //Surveille le client
  #endif
  #ifdef WIFI
  WiFiClient client = server.available();   //Surveille le client
  #endif
  if (client)                               //Si un client se connecte
  {
    //Serial.println("New Client.");          //Debug
    String currentLine = "";                //Stocke les caractere entrant
    while (client.connected())              //Tant que le client est connecté
    {
      if (client.available())
      { //Si des caracteres sont dispo
        char c = client.read();             //Lis un caractere
        //Serial.write(c);                    //Debug, copie le caractere sur le port serie
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
                 *** Transmet un bouton par tour de boucle ***
                 ********************************************/
                client.print("<input type=submit value='" + nomRelais[i] + "' style=font-size:30px;width:40%;height:75px;margin-right:20px;margin-bottom:20px;background-color:" + couleurBouton + " onClick=location.href='/R" + idRelais[i] + etatCommande + "'>");
                if ((doubleColonne == true) && ((i % 2) > 0)) client.print("<br>"); //Si affichage sur deux colonne, affiche un retour a la ligne apres les numero de bouton impair
                else if (doubleColonne == false) client.print("<br>");              //Si affichage sur une colonne, imprime un retour a la ligne apres chaque bouton
              }
            }
            if (responseAscom == true)                          //Reponse a un Get Name ou State
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
              //Serial.print("I = "); Serial.println(i);
              //******************
              #ifdef TEMPO
              if(i == 12)
              {
                tempo13Timer = millis(); 
                tempo13IsRunning = i;
                //Serial.println("Tempo activé");                
              }
               if(i == 13)
              {
                tempo14Timer = millis(); 
                tempo14IsRunning = i;
                //Serial.println("Tempo activé");                
              }
              #endif
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
    //Serial.println("Client Disconnected."); //Debug
  }
}

void EcrireNoms()
{
  for(int i = 11; i < 501; i += 35)
  {
    int e = i / 35;
    char tampon[35] = "";
    nomRelais[e].toCharArray(tampon, 35);
    EEPROM.put(i, tampon);
  }
  EEPROM.commit();
  ecrireNoms = false;
  for(int i = 11; i < 501; i += 35)
  {    
    char inputName[35] = "";
    int e = i / 35;
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
  
  #ifdef TEMPO
  if(tempo13IsRunning > 0)
  {
    if((millis() - tempo13Timer) > tempoDelay)
    {
      etatRelais[12] = 1 - etatRelais[12];
      digitalWrite(pinRelais[12], etatRelais[12]);
      tempo13IsRunning = 0;
      //Serial.println("fin de tempo 13");
    }
  }
  if(tempo14IsRunning > 0)
  {
    if((millis() - tempo14Timer) > tempoDelay)
    {
      etatRelais[13] = 1 - etatRelais[13];
      digitalWrite(pinRelais[13], etatRelais[13]);
      tempo14IsRunning = 0;
      //Serial.println("fin de tempo 14");
    }
  }
  #endif
  
}
