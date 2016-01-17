using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reverse_ebay
{
    class GUI_NameAendern : Form
    {
        private Button abbrechenButton;
        private Button aendernButton;
        private TextBox nameAltTextbox;
        public TextBox nameNeuTextbox;
        private Label nameNeuLabel;
        private Label nameAltLabel;
        private Label errorLabel;
        private IFachkonzept fachkonzept;
        Benutzer andererBenutzer;


        public GUI_NameAendern()
        {
            InitializeComponent();
        }

        public GUI_NameAendern(IFachkonzept fachkonzept)
        {
            this.fachkonzept = fachkonzept;
            this.andererBenutzer = fachkonzept.gibAktBenutzer();
            InitializeComponent();
            nameAltTextbox.Text = andererBenutzer.name;
        }
        private void InitializeComponent()
        {
            this.abbrechenButton = new System.Windows.Forms.Button();
            this.aendernButton = new System.Windows.Forms.Button();
            this.nameAltTextbox = new System.Windows.Forms.TextBox();
            this.nameNeuTextbox = new System.Windows.Forms.TextBox();
            this.nameNeuLabel = new System.Windows.Forms.Label();
            this.nameAltLabel = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // abbrechenButton
            // 
            this.abbrechenButton.Location = new System.Drawing.Point(178, 93);
            this.abbrechenButton.Name = "abbrechenButton";
            this.abbrechenButton.Size = new System.Drawing.Size(136, 30);
            this.abbrechenButton.TabIndex = 12;
            this.abbrechenButton.Text = "Abbrechen";
            this.abbrechenButton.UseVisualStyleBackColor = true;
            this.abbrechenButton.Click += new System.EventHandler(this.abbrechenOnClick);
            // 
            // aendernButton
            // 
            this.aendernButton.Location = new System.Drawing.Point(10, 93);
            this.aendernButton.Name = "aendernButton";
            this.aendernButton.Size = new System.Drawing.Size(136, 30);
            this.aendernButton.TabIndex = 11;
            this.aendernButton.Text = "Ändern";
            this.aendernButton.UseVisualStyleBackColor = true;
            this.aendernButton.Click += new System.EventHandler(this.AendernOnClick);
            // 
            // nameAltTextbox
            // 
            this.nameAltTextbox.Enabled = false;
            this.nameAltTextbox.Location = new System.Drawing.Point(92, 35);
            this.nameAltTextbox.Name = "nameAltTextbox";
            this.nameAltTextbox.Size = new System.Drawing.Size(222, 22);
            this.nameAltTextbox.TabIndex = 9;
            // 
            // nameNeuTextbox
            // 
            this.nameNeuTextbox.Location = new System.Drawing.Point(92, 60);
            this.nameNeuTextbox.Name = "nameNeuTextbox";
            this.nameNeuTextbox.Size = new System.Drawing.Size(222, 22);
            this.nameNeuTextbox.TabIndex = 10;
            // 
            // nameNeuLabel
            // 
            this.nameNeuLabel.AutoSize = true;
            this.nameNeuLabel.Location = new System.Drawing.Point(7, 65);
            this.nameNeuLabel.Name = "nameNeuLabel";
            this.nameNeuLabel.Size = new System.Drawing.Size(87, 17);
            this.nameNeuLabel.TabIndex = 8;
            this.nameNeuLabel.Text = "Name (neu):";
            // 
            // nameAltLabel
            // 
            this.nameAltLabel.AutoSize = true;
            this.nameAltLabel.Location = new System.Drawing.Point(7, 38);
            this.nameAltLabel.Name = "nameAltLabel";
            this.nameAltLabel.Size = new System.Drawing.Size(78, 17);
            this.nameAltLabel.TabIndex = 7;
            this.nameAltLabel.Text = "Name (alt):";
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(12, 9);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(0, 17);
            this.errorLabel.TabIndex = 13;
            // 
            // GUI_NameAendern
            // 
            this.ClientSize = new System.Drawing.Size(326, 135);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.abbrechenButton);
            this.Controls.Add(this.aendernButton);
            this.Controls.Add(this.nameAltTextbox);
            this.Controls.Add(this.nameNeuTextbox);
            this.Controls.Add(this.nameNeuLabel);
            this.Controls.Add(this.nameAltLabel);
            this.Name = "GUI_NameAendern";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.GUI_NameAendern_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void AendernOnClick(object sender, EventArgs e)
        {
            
            if ((!nameNeuTextbox.Text.Equals("")) && (!andererBenutzer.name.Equals(nameNeuTextbox.Text)))
            {
                andererBenutzer.name = nameNeuTextbox.Text;
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
                errorLabel.Text = "Name ist identisch oder leer. Ändern nicht erfolgreich.";
            }
        }


        private void GUI_NameAendern_Load(object sender, EventArgs e)
        {

        }

        private void abbrechenOnClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
