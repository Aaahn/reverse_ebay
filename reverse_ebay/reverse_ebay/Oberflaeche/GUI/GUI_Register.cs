using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reverse_ebay
{
    class GUI_Register:Form
    {
        private TextBox passwortTextbox;
        private Label nameLabel;
        private Label errorLabel;
        private Label passwortLabel;
        private TextBox passwort2Textbox;
        private Label passwort2Label;
        private Button abbrechenButton;
        private Button registerButton;
        private TextBox nameTextbox;
        IFachkonzept fachkonzept;

        public GUI_Register()
        {
            InitializeComponent();
        }

        public GUI_Register(IFachkonzept fachkonzept)
        {
            this.fachkonzept = fachkonzept;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.nameTextbox = new System.Windows.Forms.TextBox();
            this.passwortTextbox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.passwortLabel = new System.Windows.Forms.Label();
            this.passwort2Textbox = new System.Windows.Forms.TextBox();
            this.passwort2Label = new System.Windows.Forms.Label();
            this.abbrechenButton = new System.Windows.Forms.Button();
            this.registerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameTextbox
            // 
            this.nameTextbox.Location = new System.Drawing.Point(186, 43);
            this.nameTextbox.Name = "nameTextbox";
            this.nameTextbox.Size = new System.Drawing.Size(222, 22);
            this.nameTextbox.TabIndex = 5;
            // 
            // passwortTextbox
            // 
            this.passwortTextbox.Location = new System.Drawing.Point(186, 68);
            this.passwortTextbox.Name = "passwortTextbox";
            this.passwortTextbox.PasswordChar = '*';
            this.passwortTextbox.Size = new System.Drawing.Size(222, 22);
            this.passwortTextbox.TabIndex = 6;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(9, 43);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(49, 17);
            this.nameLabel.TabIndex = 7;
            this.nameLabel.Text = "Name:";
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(9, 9);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(0, 17);
            this.errorLabel.TabIndex = 8;
            // 
            // passwortLabel
            // 
            this.passwortLabel.AutoSize = true;
            this.passwortLabel.Location = new System.Drawing.Point(9, 71);
            this.passwortLabel.Name = "passwortLabel";
            this.passwortLabel.Size = new System.Drawing.Size(69, 17);
            this.passwortLabel.TabIndex = 9;
            this.passwortLabel.Text = "Passwort:";
            // 
            // passwort2Textbox
            // 
            this.passwort2Textbox.Location = new System.Drawing.Point(186, 93);
            this.passwort2Textbox.Name = "passwort2Textbox";
            this.passwort2Textbox.PasswordChar = '*';
            this.passwort2Textbox.Size = new System.Drawing.Size(222, 22);
            this.passwort2Textbox.TabIndex = 10;
            this.passwort2Textbox.UseSystemPasswordChar = true;
            // 
            // passwort2Label
            // 
            this.passwort2Label.AutoSize = true;
            this.passwort2Label.Location = new System.Drawing.Point(9, 96);
            this.passwort2Label.Name = "passwort2Label";
            this.passwort2Label.Size = new System.Drawing.Size(177, 17);
            this.passwort2Label.TabIndex = 11;
            this.passwort2Label.Text = "Passwort erneut eingeben:";
            // 
            // abbrechenButton
            // 
            this.abbrechenButton.Location = new System.Drawing.Point(272, 122);
            this.abbrechenButton.Name = "abbrechenButton";
            this.abbrechenButton.Size = new System.Drawing.Size(136, 30);
            this.abbrechenButton.TabIndex = 13;
            this.abbrechenButton.Text = "Abbrechen";
            this.abbrechenButton.UseVisualStyleBackColor = true;
            this.abbrechenButton.Click += new System.EventHandler(this.AbbrechenOnClick);
            // 
            // registerButton
            // 
            this.registerButton.Location = new System.Drawing.Point(12, 122);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(136, 30);
            this.registerButton.TabIndex = 12;
            this.registerButton.Text = "Registrieren";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.RegistrierenOnClick);
            // 
            // GUI_Register
            // 
            this.ClientSize = new System.Drawing.Size(420, 164);
            this.ControlBox = false;
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.abbrechenButton);
            this.Controls.Add(this.passwort2Label);
            this.Controls.Add(this.passwort2Textbox);
            this.Controls.Add(this.passwortLabel);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.passwortTextbox);
            this.Controls.Add(this.nameTextbox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GUI_Register";
            this.Text = "Registrieren";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void AbbrechenOnClick(object sender, EventArgs e)
        {
            Close();
        }

        private void RegistrierenOnClick(object sender, EventArgs e)
        {
            if (!passwortTextbox.Text.Equals(passwort2Textbox.Text))
            {
                errorLabel.Text = "Die Passwörter müssen übereinstimmen";
                return;
            }
            if (nameTextbox.Text.Equals(""))
            {
                errorLabel.Text = "Bitte alle Felder ausfüllen!";
                return;
            }
            Benutzer neuerBenutzer = new Benutzer();
            neuerBenutzer.name = nameTextbox.Text;
            neuerBenutzer.passwort = passwortTextbox.Text;
            if (fachkonzept.erzeugeBenutzer(neuerBenutzer))
            {
                Close();
            }
            else
            {
                errorLabel.Text = "Login war nicht erfolgreich. Bitte versuchen Sie es erneut!";
            }
        }
    }
}
