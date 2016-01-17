using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reverse_ebay
{
    class GUI_PasswortAendern:Form
    {
        private Button aendernButton;
        private Button abbrechenButton;
        private Label passwort2Label;
        private TextBox passwortNeu2Textbox;
        private Label passwortNeuLabel;
        private Label errorLabel;
        private Label passwortAltLabel;
        private TextBox passwortNeuTextbox;
        private TextBox passwortAltTextbox;
        private IFachkonzept fachkonzept;
        private Benutzer andererBenutzer;

        public GUI_PasswortAendern()
        {
            InitializeComponent();
        }

        public GUI_PasswortAendern(IFachkonzept fachkonzept)
        {
            this.fachkonzept = fachkonzept;
            this.andererBenutzer = fachkonzept.gibAktBenutzer();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.aendernButton = new System.Windows.Forms.Button();
            this.abbrechenButton = new System.Windows.Forms.Button();
            this.passwort2Label = new System.Windows.Forms.Label();
            this.passwortNeu2Textbox = new System.Windows.Forms.TextBox();
            this.passwortNeuLabel = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.passwortAltLabel = new System.Windows.Forms.Label();
            this.passwortNeuTextbox = new System.Windows.Forms.TextBox();
            this.passwortAltTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // aendernButton
            // 
            this.aendernButton.Location = new System.Drawing.Point(13, 122);
            this.aendernButton.Name = "aendernButton";
            this.aendernButton.Size = new System.Drawing.Size(136, 30);
            this.aendernButton.TabIndex = 21;
            this.aendernButton.Text = "Ändern";
            this.aendernButton.UseVisualStyleBackColor = true;
            this.aendernButton.Click += new System.EventHandler(this.aendernOnClick);
            // 
            // abbrechenButton
            // 
            this.abbrechenButton.Location = new System.Drawing.Point(273, 122);
            this.abbrechenButton.Name = "abbrechenButton";
            this.abbrechenButton.Size = new System.Drawing.Size(136, 30);
            this.abbrechenButton.TabIndex = 22;
            this.abbrechenButton.Text = "Abbrechen";
            this.abbrechenButton.UseVisualStyleBackColor = true;
            this.abbrechenButton.Click += new System.EventHandler(this.abbrechenOnClick);
            // 
            // passwort2Label
            // 
            this.passwort2Label.AutoSize = true;
            this.passwort2Label.Location = new System.Drawing.Point(10, 96);
            this.passwort2Label.Name = "passwort2Label";
            this.passwort2Label.Size = new System.Drawing.Size(177, 17);
            this.passwort2Label.TabIndex = 20;
            this.passwort2Label.Text = "Passwort erneut eingeben:";
            // 
            // passwortNeu2Textbox
            // 
            this.passwortNeu2Textbox.Location = new System.Drawing.Point(187, 93);
            this.passwortNeu2Textbox.Name = "passwortNeu2Textbox";
            this.passwortNeu2Textbox.PasswordChar = '*';
            this.passwortNeu2Textbox.Size = new System.Drawing.Size(222, 22);
            this.passwortNeu2Textbox.TabIndex = 19;
            this.passwortNeu2Textbox.UseSystemPasswordChar = true;
            // 
            // passwortNeuLabel
            // 
            this.passwortNeuLabel.AutoSize = true;
            this.passwortNeuLabel.Location = new System.Drawing.Point(10, 71);
            this.passwortNeuLabel.Name = "passwortNeuLabel";
            this.passwortNeuLabel.Size = new System.Drawing.Size(107, 17);
            this.passwortNeuLabel.TabIndex = 18;
            this.passwortNeuLabel.Text = "Passwort (neu):";
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(10, 9);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(0, 17);
            this.errorLabel.TabIndex = 17;
            // 
            // passwortAltLabel
            // 
            this.passwortAltLabel.AutoSize = true;
            this.passwortAltLabel.Location = new System.Drawing.Point(10, 43);
            this.passwortAltLabel.Name = "passwortAltLabel";
            this.passwortAltLabel.Size = new System.Drawing.Size(98, 17);
            this.passwortAltLabel.TabIndex = 16;
            this.passwortAltLabel.Text = "Passwort (alt):";
            // 
            // passwortNeuTextbox
            // 
            this.passwortNeuTextbox.Location = new System.Drawing.Point(187, 68);
            this.passwortNeuTextbox.Name = "passwortNeuTextbox";
            this.passwortNeuTextbox.PasswordChar = '*';
            this.passwortNeuTextbox.Size = new System.Drawing.Size(222, 22);
            this.passwortNeuTextbox.TabIndex = 15;
            // 
            // passwortAltTextbox
            // 
            this.passwortAltTextbox.Location = new System.Drawing.Point(187, 43);
            this.passwortAltTextbox.Name = "passwortAltTextbox";
            this.passwortAltTextbox.PasswordChar = '*';
            this.passwortAltTextbox.Size = new System.Drawing.Size(222, 22);
            this.passwortAltTextbox.TabIndex = 14;
            this.passwortAltTextbox.UseSystemPasswordChar = true;
            // 
            // GUI_PasswortAendern
            // 
            this.ClientSize = new System.Drawing.Size(416, 160);
            this.Controls.Add(this.aendernButton);
            this.Controls.Add(this.abbrechenButton);
            this.Controls.Add(this.passwort2Label);
            this.Controls.Add(this.passwortNeu2Textbox);
            this.Controls.Add(this.passwortNeuLabel);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.passwortAltLabel);
            this.Controls.Add(this.passwortNeuTextbox);
            this.Controls.Add(this.passwortAltTextbox);
            this.Name = "GUI_PasswortAendern";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void abbrechenOnClick(object sender, EventArgs e)
        {
            Close();
        }

        private void aendernOnClick(object sender, EventArgs e)
        {
            if (passwortAltTextbox.Text.Equals(fachkonzept.gibAktBenutzer().passwort))
            {
                if (passwortNeuTextbox.Text.Equals(passwortNeu2Textbox.Text))
                {
                    if (!passwortNeuTextbox.Text.Equals(passwortAltTextbox.Text))
                    {
                        andererBenutzer.passwort = passwortNeuTextbox.Text;
                        if (fachkonzept.aendereBenutzer(andererBenutzer))
                        {
                            Close();
                        }
                        else
                        {
                            errorLabel.Text = "Ändern nicht erfolgreich.";
                        }
                    }
                    else
                    {
                        errorLabel.Text = "Das neue und das alte Passwort sind identisch.";
                    }
                }
                else
                {
                    errorLabel.Text = "Die beiden Passwörter stimmen nicht überein.";
                }
            }
            else
            {
                errorLabel.Text = "Das alte Passwort ist nicht korrekt.";
            }
        }
    }
}
