using System;
using System.Windows.Forms;

namespace reverse_ebay
{
    class GUI_Login:Form
    {
        IFachkonzept fachkonzept;
        private Label nameLabel;
        private Label passwortLabel;
        private Label errorLabel;
        public TextBox passwortTextbox;
        private TextBox nameTextbox;
        private Button loginButton;
        private Button abbrechenButton;

        public GUI_Login()
        {
            InitializeComponent();
        }

        public GUI_Login(IFachkonzept fachkonzept)
        {
            this.fachkonzept = fachkonzept;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.nameLabel = new Label();
            this.passwortLabel = new Label();
            this.errorLabel = new Label();
            this.passwortTextbox = new TextBox();
            this.nameTextbox = new TextBox();
            this.loginButton = new Button();
            this.abbrechenButton = new Button();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(12, 43);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(49, 17);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Name:";
            // 
            // passwortLabel
            // 
            this.passwortLabel.AutoSize = true;
            this.passwortLabel.Location = new System.Drawing.Point(12, 70);
            this.passwortLabel.Name = "passwortLabel";
            this.passwortLabel.Size = new System.Drawing.Size(69, 17);
            this.passwortLabel.TabIndex = 1;
            this.passwortLabel.Text = "Passwort:";
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(12, 9);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(0, 17);
            this.errorLabel.TabIndex = 2;
            // 
            // passwortTextbox
            // 
            this.passwortTextbox.Location = new System.Drawing.Point(97, 67);
            this.passwortTextbox.Name = "passwortTextbox";
            this.passwortTextbox.PasswordChar = '*';
            this.passwortTextbox.Size = new System.Drawing.Size(222, 22);
            this.passwortTextbox.TabIndex = 4;
            this.passwortTextbox.UseSystemPasswordChar = true;
            this.passwortTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.passwortTextbox_KeyPress);
            // 
            // nameTextbox
            // 
            this.nameTextbox.Location = new System.Drawing.Point(97, 40);
            this.nameTextbox.Name = "nameTextbox";
            this.nameTextbox.Size = new System.Drawing.Size(222, 22);
            this.nameTextbox.TabIndex = 3;
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(15, 98);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(136, 30);
            this.loginButton.TabIndex = 5;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new EventHandler(this.LoginOnClick);
            // 
            // abbrechenButton
            // 
            this.abbrechenButton.Location = new System.Drawing.Point(183, 98);
            this.abbrechenButton.Name = "abbrechenButton";
            this.abbrechenButton.Size = new System.Drawing.Size(136, 30);
            this.abbrechenButton.TabIndex = 6;
            this.abbrechenButton.Text = "Abbrechen";
            this.abbrechenButton.UseVisualStyleBackColor = true;
            this.abbrechenButton.Click += new EventHandler(this.AbbrechenOnClick);
            // 
            // GUI_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 140);
            this.ControlBox = false;
            this.Controls.Add(this.abbrechenButton);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.nameTextbox);
            this.Controls.Add(this.passwortTextbox);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.passwortLabel);
            this.Controls.Add(this.nameLabel);
            this.Name = "GUI_Login";
            this.Text = "Login";
            this.Load += new EventHandler(this.GUI_Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void GUI_Login_Load(object sender, EventArgs e)
        {

        }

        private void LoginOnClick(object sender, EventArgs e)
        {
            if ((nameTextbox.Text == "") | (passwortTextbox.Text == ""))
            {
                errorLabel.Text = "Bitte alle Felder ausfüllen!";
                return;
            }
            if (fachkonzept.einloggen(nameTextbox.Text,passwortTextbox.Text))
            {
                Close();
            } else
            {
                errorLabel.Text = "Login war nicht erfolgreich. Bitte versuchen Sie es erneut!";
            }
        }

        private void AbbrechenOnClick(object sender, EventArgs e)
        {
            Close();
        }

        private void passwortTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                LoginOnClick(sender, e);
            }
        }
    }
}
