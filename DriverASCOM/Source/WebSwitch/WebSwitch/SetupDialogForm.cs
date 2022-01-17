using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ASCOM.Utilities;
using ASCOM.WebSwitch;

using System.Net;
using System.IO;

namespace ASCOM.WebSwitch
{
    [ComVisible(false)]					// Form not registered for COM!
    public partial class SetupDialogForm : Form
    {
        string[] idSwitch = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P" };
        string[] nomBouton = new string[14];
        TraceLogger tl; // Holder for a reference to the driver's trace logger

        public SetupDialogForm(TraceLogger tlDriver)
        {
            InitializeComponent();

            // Save the provided trace logger for use within the setup dialogue
            tl = tlDriver;

            // Initialise current values of user settings from the ASCOM Profile
            InitUI();
        }

        
        private void cmdOK_Click(object sender, EventArgs e) // OK button event handler
        {
            // Place any validation constraint checks here
            // Update the state variables with results from the dialogue
            Switch.comPort = textBoxIP.Text;
            tl.Enabled = chkTrace.Checked;
            //Switch.timeOut = textBoxTimeOut.Text;
            
        }

        private void cmdCancel_Click(object sender, EventArgs e) // Cancel button event handler
        {
            Close();
        }

        private void BrowseToAscom(object sender, EventArgs e) // Click on ASCOM logo event handler
        {
            try
            {
                System.Diagnostics.Process.Start("https://ascom-standards.org/");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void InitUI()
        {
            textBoxIP.Text = Switch.comPort;
            chkTrace.Checked = tl.Enabled;          
            textBoxTimeOut.Text = Convert.ToString(Switch.timeOut);



            // set the list of com ports to those that are currently available

            //comboBoxComPort.Items.Clear();
            //comboBoxComPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());      // use System.IO because it's static
            // select the current port if possible
            //if (comboBoxComPort.Items.Contains(Switch.comPort))
            //{
            //    comboBoxComPort.SelectedItem = Switch.comPort;
            //}
        }

        private void buttonSetName_Click(object sender, EventArgs e)
        {
            if (textBoxNameA.Text == "") textBoxNameA.Text = "Bouton 1";
            nomBouton[0] = textBoxNameA.Text;
            if (textBoxNameB.Text == "") textBoxNameB.Text = "Bouton 2";
            nomBouton[1] = textBoxNameB.Text;
            if (textBoxNameC.Text == "") textBoxNameC.Text = "Bouton 3";
            nomBouton[2] = textBoxNameC.Text;
            if (textBoxNameD.Text == "") textBoxNameD.Text = "Bouton 4";
            nomBouton[3] = textBoxNameD.Text;
            if (textBoxNameE.Text == "") textBoxNameE.Text = "Bouton 5";
            nomBouton[4] = textBoxNameE.Text;
            if (textBoxNameF.Text == "") textBoxNameF.Text = "Bouton 6";
            nomBouton[5] = textBoxNameF.Text;
            if (textBoxNameG.Text == "") textBoxNameG.Text = "Bouton 7";
            nomBouton[6] = textBoxNameG.Text;
            if (textBoxNameH.Text == "") textBoxNameH.Text = "Bouton 8";
            nomBouton[7] = textBoxNameH.Text;
            if (textBoxNameI.Text == "") textBoxNameI.Text = "Bouton 9";
            nomBouton[8] = textBoxNameI.Text;
            if (textBoxNameJ.Text == "") textBoxNameJ.Text = "Bouton 10";
            nomBouton[9] = textBoxNameJ.Text;
            if (textBoxNameK.Text == "") textBoxNameK.Text = "Bouton 11";
            nomBouton[10] = textBoxNameK.Text;
            if (textBoxNameL.Text == "") textBoxNameL.Text = "Bouton 12";
            nomBouton[11] = textBoxNameL.Text;
            if (textBoxNameM.Text == "") textBoxNameM.Text = "Bouton 13";
            nomBouton[12] = textBoxNameM.Text;
            if (textBoxNameN.Text == "") textBoxNameN.Text = "Bouton 14";
            nomBouton[13] = textBoxNameN.Text;
            try
            {
                for (int i = 0; i <= ((Switch.numSwitch) - 1); i++)
                {
                    //string name = string.Format("textBoxSetName" + idSwitch[i] + ".Text");
                    string messageUrl2 = string.Format("http://" + Switch.comPort + "/R" + idSwitch[i] + "XXX" + nomBouton[i] + "FFF");
                    WebRequest request2 = WebRequest.Create(messageUrl2);
                    request2.Timeout = Switch.timeOut;
                    WebResponse response2 = request2.GetResponse();
                    response2.Close();
                }
                string messageUrl = string.Format("http://" + Switch.comPort + "/RAZZZ");
                WebRequest request = WebRequest.Create(messageUrl);
                request.Timeout = Switch.timeOut;
                WebResponse response = request.GetResponse();
                response.Close();
            }
            catch(WebException)
            {
                string message = "Le switch ne repond pas, verifiez que l'adresse saisie est correcte, que le switch est sous tension et correctement connecté au reseau.";
                string titre = "WebSwitchESP. Delais d'attente depassé.";
                System.Windows.Forms.MessageBox.Show(message, titre);
            }
        }        

        private void boutonTestIp_Click(object sender, EventArgs e)
        {
            try
            {
                Switch.comPort = textBoxIP.Text;
                Switch.comPortDefault = textBoxIP.Text;
                string messageUrl = string.Format("http://" + Switch.comPort + "/RAIII");
                WebRequest request = WebRequest.Create(messageUrl);
                request.Timeout = Switch.timeOut;
                WebResponse response = request.GetResponse();
                string idUrl = System.String.Empty;
                using (Stream datastream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(datastream);
                    string responseUrl = reader.ReadToEnd();
                    idUrl = responseUrl;
                }
                response.Close();
                if (idUrl.IndexOf("ArduinoWebSwitch") >= 0)
                {                    
                    buttonSetName.Enabled = true;
                    buttonRafraichirNoms.Enabled = true;
                   
                    //demande le nombre de relais du switch 
                    String messageurl = string.Format("http://" + Switch.comPort + "/RABBB");
                    WebRequest request2 = WebRequest.Create(messageurl);
                    request2.Timeout = Switch.timeOut;
                    WebResponse response2 = request2.GetResponse();
                    string responsename = System.String.Empty;
                    using (Stream datastream = response2.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(datastream);
                        string responseUrl = reader.ReadToEnd();
                        responsename = responseUrl;
                    }
                    response.Close();
                    //Active le bon nombre de textBox
                    Switch.numSwitch = Convert.ToInt16(responsename);
                    if (Switch.numSwitch >= 1) textBoxNameA.Enabled = true;
                    if (Switch.numSwitch >= 2) textBoxNameB.Enabled = true;
                    if (Switch.numSwitch >= 3) textBoxNameC.Enabled = true;
                    if (Switch.numSwitch >= 4) textBoxNameD.Enabled = true;
                    if (Switch.numSwitch >= 5) textBoxNameE.Enabled = true;
                    if (Switch.numSwitch >= 6) textBoxNameF.Enabled = true;
                    if (Switch.numSwitch >= 7) textBoxNameG.Enabled = true;
                    if (Switch.numSwitch >= 8) textBoxNameH.Enabled = true;
                    if (Switch.numSwitch >= 9) textBoxNameI.Enabled = true;
                    if (Switch.numSwitch >= 10) textBoxNameJ.Enabled = true;
                    if (Switch.numSwitch >= 11) textBoxNameK.Enabled = true;
                    if (Switch.numSwitch >= 12) textBoxNameL.Enabled = true;
                    if (Switch.numSwitch >= 13) textBoxNameM.Enabled = true;
                    if (Switch.numSwitch >= 14) textBoxNameN.Enabled = true;
                }
                buttonRafraichirNoms_Click(sender, e);
            }
            
            catch (WebException)
            {
                string message = "Le switch ne repond pas, verifiez que l'adresse saisie est correcte, que le switch est sous tension et correctement connecté au reseau.";
                string titre = "WebSwitchESP. Delais d'attente depassé.";
                System.Windows.Forms.MessageBox.Show(message, titre);
            }
            
        }

        private void boutonDefinirTimeOut_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxTimeOut.Text, out int result))
            {
                if((result >= 2000) && (result <= 10000))
                {
                    Switch.timeOut = result;
                }
                else
                {
                    string message = "WebSwitchESP. Erreur de saisie";
                    string titre = "La valeur du timeout doit etre comprise entre 2000 et 10 000.";
                    System.Windows.Forms.MessageBox.Show(titre, message);
                }

            }
            else
            {
                string message = "WebSwitchESP. Erreur de saisie";
                string titre = "La valeur du timeout doit etre comprise entre 2000 et 10 000.";
                System.Windows.Forms.MessageBox.Show(titre, message);
            }
        }

        private void buttonRafraichirNoms_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Switch.numSwitch; i++)
            {
                string[] idSwitch = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N" };
                string messageUrl = string.Format("http://" + Switch.comPort + "/R" + idSwitch[i] + "NNN");
                WebRequest request = WebRequest.Create(messageUrl);
                WebResponse response = request.GetResponse();
                string responseName = System.String.Empty;
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    string responseUrl = reader.ReadToEnd();
                    responseName = responseUrl;
                }
                response.Close();
                nomBouton[i] = responseName;
            }
            if (Switch.numSwitch >= 1) textBoxNameA.Text = nomBouton[0];
            if (Switch.numSwitch >= 2) textBoxNameB.Text = nomBouton[1];
            if (Switch.numSwitch >= 3) textBoxNameC.Text = nomBouton[2];
            if (Switch.numSwitch >= 4) textBoxNameD.Text = nomBouton[3];
            if (Switch.numSwitch >= 5) textBoxNameE.Text = nomBouton[4];
            if (Switch.numSwitch >= 6) textBoxNameF.Text = nomBouton[5];
            if (Switch.numSwitch >= 7) textBoxNameG.Text = nomBouton[6];
            if (Switch.numSwitch >= 8) textBoxNameH.Text = nomBouton[7];
            if (Switch.numSwitch >= 9) textBoxNameI.Text = nomBouton[8];
            if (Switch.numSwitch >= 10) textBoxNameJ.Text = nomBouton[9];
            if (Switch.numSwitch >= 11) textBoxNameK.Text = nomBouton[10];
            if (Switch.numSwitch >= 12) textBoxNameL.Text = nomBouton[11];
            if (Switch.numSwitch >= 13) textBoxNameM.Text = nomBouton[12];
            if (Switch.numSwitch >= 14) textBoxNameN.Text = nomBouton[13];
        }
    }
}