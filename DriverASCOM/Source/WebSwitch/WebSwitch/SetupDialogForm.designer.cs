namespace ASCOM.WebSwitch
{
    partial class SetupDialogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.picASCOM = new System.Windows.Forms.PictureBox();
            this.chkTrace = new System.Windows.Forms.CheckBox();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxNameA = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSetName = new System.Windows.Forms.Button();
            this.textBoxNameB = new System.Windows.Forms.TextBox();
            this.textBoxNameC = new System.Windows.Forms.TextBox();
            this.buttonTestIp = new System.Windows.Forms.Button();
            this.textBoxNameD = new System.Windows.Forms.TextBox();
            this.textBoxNameE = new System.Windows.Forms.TextBox();
            this.textBoxNameF = new System.Windows.Forms.TextBox();
            this.textBoxNameG = new System.Windows.Forms.TextBox();
            this.textBoxNameH = new System.Windows.Forms.TextBox();
            this.textBoxNameI = new System.Windows.Forms.TextBox();
            this.textBoxNameJ = new System.Windows.Forms.TextBox();
            this.textBoxNameK = new System.Windows.Forms.TextBox();
            this.textBoxNameL = new System.Windows.Forms.TextBox();
            this.textBoxNameM = new System.Windows.Forms.TextBox();
            this.textBoxNameN = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.textBoxTimeOut = new System.Windows.Forms.TextBox();
            this.boutonDefinrTimeOut = new System.Windows.Forms.Button();
            this.buttonRafraichirNoms = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(428, 273);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(59, 24);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(493, 273);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(59, 25);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Annuler";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.label1.Location = new System.Drawing.Point(320, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "WebSwitchESP";
            // 
            // picASCOM
            // 
            this.picASCOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picASCOM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picASCOM.Image = global::ASCOM.WebSwitch.Properties.Resources.ASCOM;
            this.picASCOM.Location = new System.Drawing.Point(502, 7);
            this.picASCOM.Name = "picASCOM";
            this.picASCOM.Size = new System.Drawing.Size(48, 56);
            this.picASCOM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picASCOM.TabIndex = 3;
            this.picASCOM.TabStop = false;
            this.picASCOM.Click += new System.EventHandler(this.BrowseToAscom);
            this.picASCOM.DoubleClick += new System.EventHandler(this.BrowseToAscom);
            // 
            // chkTrace
            // 
            this.chkTrace.AutoSize = true;
            this.chkTrace.Location = new System.Drawing.Point(21, 273);
            this.chkTrace.Name = "chkTrace";
            this.chkTrace.Size = new System.Drawing.Size(69, 17);
            this.chkTrace.TabIndex = 6;
            this.chkTrace.Text = "Trace on";
            this.chkTrace.UseVisualStyleBackColor = true;
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(79, 12);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(129, 20);
            this.textBoxIP.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Adresse IP:";
            // 
            // textBoxNameA
            // 
            this.textBoxNameA.Enabled = false;
            this.textBoxNameA.Location = new System.Drawing.Point(28, 38);
            this.textBoxNameA.Name = "textBoxNameA";
            this.textBoxNameA.Size = new System.Drawing.Size(190, 20);
            this.textBoxNameA.TabIndex = 10;
            this.textBoxNameA.Text = "Relais1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Noms des relais:";
            // 
            // buttonSetName
            // 
            this.buttonSetName.Enabled = false;
            this.buttonSetName.Location = new System.Drawing.Point(466, 207);
            this.buttonSetName.Name = "buttonSetName";
            this.buttonSetName.Size = new System.Drawing.Size(84, 42);
            this.buttonSetName.TabIndex = 12;
            this.buttonSetName.Text = "Sauvegarder les noms.";
            this.buttonSetName.UseVisualStyleBackColor = true;
            this.buttonSetName.Click += new System.EventHandler(this.buttonSetName_Click);
            // 
            // textBoxNameB
            // 
            this.textBoxNameB.Enabled = false;
            this.textBoxNameB.Location = new System.Drawing.Point(247, 38);
            this.textBoxNameB.Name = "textBoxNameB";
            this.textBoxNameB.Size = new System.Drawing.Size(190, 20);
            this.textBoxNameB.TabIndex = 14;
            this.textBoxNameB.Text = "Relais2";
            // 
            // textBoxNameC
            // 
            this.textBoxNameC.Enabled = false;
            this.textBoxNameC.Location = new System.Drawing.Point(28, 64);
            this.textBoxNameC.Name = "textBoxNameC";
            this.textBoxNameC.Size = new System.Drawing.Size(190, 20);
            this.textBoxNameC.TabIndex = 15;
            this.textBoxNameC.Text = "Relais3";
            // 
            // buttonTestIp
            // 
            this.buttonTestIp.Location = new System.Drawing.Point(214, 9);
            this.buttonTestIp.Name = "buttonTestIp";
            this.buttonTestIp.Size = new System.Drawing.Size(100, 23);
            this.buttonTestIp.TabIndex = 16;
            this.buttonTestIp.Text = "Tester l\'adresse.";
            this.buttonTestIp.UseVisualStyleBackColor = true;
            this.buttonTestIp.Click += new System.EventHandler(this.boutonTestIp_Click);
            // 
            // textBoxNameD
            // 
            this.textBoxNameD.Enabled = false;
            this.textBoxNameD.Location = new System.Drawing.Point(247, 64);
            this.textBoxNameD.Name = "textBoxNameD";
            this.textBoxNameD.Size = new System.Drawing.Size(190, 20);
            this.textBoxNameD.TabIndex = 17;
            this.textBoxNameD.Text = "Relais4";
            // 
            // textBoxNameE
            // 
            this.textBoxNameE.Enabled = false;
            this.textBoxNameE.Location = new System.Drawing.Point(28, 90);
            this.textBoxNameE.Name = "textBoxNameE";
            this.textBoxNameE.Size = new System.Drawing.Size(190, 20);
            this.textBoxNameE.TabIndex = 18;
            this.textBoxNameE.Text = "Relais5";
            // 
            // textBoxNameF
            // 
            this.textBoxNameF.Enabled = false;
            this.textBoxNameF.Location = new System.Drawing.Point(247, 90);
            this.textBoxNameF.Name = "textBoxNameF";
            this.textBoxNameF.Size = new System.Drawing.Size(190, 20);
            this.textBoxNameF.TabIndex = 19;
            this.textBoxNameF.Text = "Relais6";
            // 
            // textBoxNameG
            // 
            this.textBoxNameG.Enabled = false;
            this.textBoxNameG.Location = new System.Drawing.Point(28, 116);
            this.textBoxNameG.Name = "textBoxNameG";
            this.textBoxNameG.Size = new System.Drawing.Size(190, 20);
            this.textBoxNameG.TabIndex = 20;
            this.textBoxNameG.Text = "Relais7";
            // 
            // textBoxNameH
            // 
            this.textBoxNameH.Enabled = false;
            this.textBoxNameH.Location = new System.Drawing.Point(247, 116);
            this.textBoxNameH.Name = "textBoxNameH";
            this.textBoxNameH.Size = new System.Drawing.Size(190, 20);
            this.textBoxNameH.TabIndex = 21;
            this.textBoxNameH.Text = "Relais8";
            // 
            // textBoxNameI
            // 
            this.textBoxNameI.Enabled = false;
            this.textBoxNameI.Location = new System.Drawing.Point(28, 142);
            this.textBoxNameI.Name = "textBoxNameI";
            this.textBoxNameI.Size = new System.Drawing.Size(190, 20);
            this.textBoxNameI.TabIndex = 22;
            this.textBoxNameI.Text = "Relais9";
            // 
            // textBoxNameJ
            // 
            this.textBoxNameJ.Enabled = false;
            this.textBoxNameJ.Location = new System.Drawing.Point(247, 142);
            this.textBoxNameJ.Name = "textBoxNameJ";
            this.textBoxNameJ.Size = new System.Drawing.Size(190, 20);
            this.textBoxNameJ.TabIndex = 23;
            this.textBoxNameJ.Text = "Relais10";
            // 
            // textBoxNameK
            // 
            this.textBoxNameK.Enabled = false;
            this.textBoxNameK.Location = new System.Drawing.Point(28, 168);
            this.textBoxNameK.Name = "textBoxNameK";
            this.textBoxNameK.Size = new System.Drawing.Size(190, 20);
            this.textBoxNameK.TabIndex = 24;
            this.textBoxNameK.Text = "Relais11";
            // 
            // textBoxNameL
            // 
            this.textBoxNameL.Enabled = false;
            this.textBoxNameL.Location = new System.Drawing.Point(247, 168);
            this.textBoxNameL.Name = "textBoxNameL";
            this.textBoxNameL.Size = new System.Drawing.Size(189, 20);
            this.textBoxNameL.TabIndex = 25;
            this.textBoxNameL.Text = "Relais12";
            // 
            // textBoxNameM
            // 
            this.textBoxNameM.Enabled = false;
            this.textBoxNameM.Location = new System.Drawing.Point(28, 194);
            this.textBoxNameM.Name = "textBoxNameM";
            this.textBoxNameM.Size = new System.Drawing.Size(189, 20);
            this.textBoxNameM.TabIndex = 26;
            this.textBoxNameM.Text = "Relais13";
            // 
            // textBoxNameN
            // 
            this.textBoxNameN.Enabled = false;
            this.textBoxNameN.Location = new System.Drawing.Point(247, 194);
            this.textBoxNameN.Name = "textBoxNameN";
            this.textBoxNameN.Size = new System.Drawing.Size(189, 20);
            this.textBoxNameN.TabIndex = 27;
            this.textBoxNameN.Text = "Relais14";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(224, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "10";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(230, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 13);
            this.label7.TabIndex = 33;
            this.label7.Text = "3";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(230, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 13);
            this.label8.TabIndex = 34;
            this.label8.Text = "4";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 93);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 13);
            this.label9.TabIndex = 35;
            this.label9.Text = "5";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(230, 93);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(13, 13);
            this.label10.TabIndex = 36;
            this.label10.Text = "6";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 119);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(13, 13);
            this.label11.TabIndex = 37;
            this.label11.Text = "7";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(230, 119);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(13, 13);
            this.label12.TabIndex = 38;
            this.label12.Text = "8";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 145);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(13, 13);
            this.label13.TabIndex = 39;
            this.label13.Text = "9";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 171);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(19, 13);
            this.label14.TabIndex = 40;
            this.label14.Text = "11";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(224, 171);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(19, 13);
            this.label15.TabIndex = 41;
            this.label15.Text = "12";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 197);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(19, 13);
            this.label16.TabIndex = 42;
            this.label16.Text = "13";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(223, 197);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(19, 13);
            this.label17.TabIndex = 43;
            this.label17.Text = "14";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.textBoxNameM);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.textBoxNameD);
            this.panel1.Controls.Add(this.textBoxNameB);
            this.panel1.Controls.Add(this.textBoxNameF);
            this.panel1.Controls.Add(this.textBoxNameH);
            this.panel1.Controls.Add(this.textBoxNameJ);
            this.panel1.Controls.Add(this.textBoxNameL);
            this.panel1.Controls.Add(this.textBoxNameN);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textBoxNameK);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.textBoxNameI);
            this.panel1.Controls.Add(this.textBoxNameG);
            this.panel1.Controls.Add(this.textBoxNameE);
            this.panel1.Controls.Add(this.textBoxNameC);
            this.panel1.Controls.Add(this.textBoxNameA);
            this.panel1.Location = new System.Drawing.Point(15, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(445, 228);
            this.panel1.TabIndex = 46;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(490, 77);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(50, 13);
            this.label18.TabIndex = 47;
            this.label18.Text = "Time Out";
            // 
            // textBoxTimeOut
            // 
            this.textBoxTimeOut.Location = new System.Drawing.Point(468, 96);
            this.textBoxTimeOut.Name = "textBoxTimeOut";
            this.textBoxTimeOut.Size = new System.Drawing.Size(84, 20);
            this.textBoxTimeOut.TabIndex = 48;
            // 
            // boutonDefinrTimeOut
            // 
            this.boutonDefinrTimeOut.Location = new System.Drawing.Point(475, 122);
            this.boutonDefinrTimeOut.Name = "boutonDefinrTimeOut";
            this.boutonDefinrTimeOut.Size = new System.Drawing.Size(75, 23);
            this.boutonDefinrTimeOut.TabIndex = 49;
            this.boutonDefinrTimeOut.Text = "Definir";
            this.boutonDefinrTimeOut.UseVisualStyleBackColor = true;
            this.boutonDefinrTimeOut.Click += new System.EventHandler(this.boutonDefinirTimeOut_Click);
            // 
            // buttonRafraichirNoms
            // 
            this.buttonRafraichirNoms.Enabled = false;
            this.buttonRafraichirNoms.Location = new System.Drawing.Point(468, 166);
            this.buttonRafraichirNoms.Name = "buttonRafraichirNoms";
            this.buttonRafraichirNoms.Size = new System.Drawing.Size(82, 35);
            this.buttonRafraichirNoms.TabIndex = 50;
            this.buttonRafraichirNoms.Text = "Rafraichir les noms";
            this.buttonRafraichirNoms.UseVisualStyleBackColor = true;
            this.buttonRafraichirNoms.Click += new System.EventHandler(this.buttonRafraichirNoms_Click);
            // 
            // SetupDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 304);
            this.Controls.Add(this.buttonRafraichirNoms);
            this.Controls.Add(this.boutonDefinrTimeOut);
            this.Controls.Add(this.textBoxTimeOut);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonTestIp);
            this.Controls.Add(this.buttonSetName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.chkTrace);
            this.Controls.Add(this.picASCOM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupDialogForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WebSwitch Setup";
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picASCOM;
        private System.Windows.Forms.CheckBox chkTrace;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxNameA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSetName;
        private System.Windows.Forms.TextBox textBoxNameB;
        private System.Windows.Forms.TextBox textBoxNameC;
        private System.Windows.Forms.Button buttonTestIp;
        private System.Windows.Forms.TextBox textBoxNameD;
        private System.Windows.Forms.TextBox textBoxNameE;
        private System.Windows.Forms.TextBox textBoxNameF;
        private System.Windows.Forms.TextBox textBoxNameG;
        private System.Windows.Forms.TextBox textBoxNameH;
        private System.Windows.Forms.TextBox textBoxNameI;
        private System.Windows.Forms.TextBox textBoxNameJ;
        private System.Windows.Forms.TextBox textBoxNameK;
        private System.Windows.Forms.TextBox textBoxNameL;
        private System.Windows.Forms.TextBox textBoxNameM;
        private System.Windows.Forms.TextBox textBoxNameN;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBoxTimeOut;
        private System.Windows.Forms.Button boutonDefinrTimeOut;
        private System.Windows.Forms.Button buttonRafraichirNoms;
    }
}