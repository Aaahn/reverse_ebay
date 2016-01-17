using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace reverse_ebay
{
    class GUI_Main:Form
    {
        private IFachkonzept fachkonzept;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem anmeldenToolStripMenuItem;
        private ToolStripMenuItem registrierenToolStripMenuItem;
        private BindingSource artikelBindingSource;
        private System.ComponentModel.IContainer components;
        private DataGridView artikelGridView;
        private DataTable table;
        private GroupBox meineSeiteGroupBox;
        private Label nameLabel;
        private Button nameAendernButton;
        private Button passwortAendernButton;
        private DataGridViewTextBoxColumn gebotenVonDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn gebotDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn ablaufdatumDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn kurzbeschreibungDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private ToolStripMenuItem meineAdressenToolStripMenuItem;
        private GroupBox meineAdressenGroupBox;
        private FlowLayoutPanel adressFlowLayoutPanel;

        private enum status {ausgeloggt,eingeloggt,meineSeite,Adressen,Artikel};
        private status aktuellerStatus;

        public GUI_Main()
        {
            InitializeComponent();
            Initialize();
        }

        public GUI_Main(IFachkonzept fachkonzept)
        {
            this.fachkonzept = fachkonzept;
            InitializeComponent();
            Initialize();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.table = new System.Data.DataTable();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.anmeldenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registrierenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.meineAdressenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.artikelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.artikelGridView = new System.Windows.Forms.DataGridView();
            this.meineSeiteGroupBox = new System.Windows.Forms.GroupBox();
            this.nameAendernButton = new System.Windows.Forms.Button();
            this.passwortAendernButton = new System.Windows.Forms.Button();
            this.nameLabel = new System.Windows.Forms.Label();
            this.meineAdressenGroupBox = new System.Windows.Forms.GroupBox();
            this.adressFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.table)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.artikelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.artikelGridView)).BeginInit();
            this.meineSeiteGroupBox.SuspendLayout();
            this.meineAdressenGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // table
            // 
            this.table.TableName = "Artikel";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.anmeldenToolStripMenuItem,
            this.registrierenToolStripMenuItem,
            this.meineAdressenToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(818, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // anmeldenToolStripMenuItem
            // 
            this.anmeldenToolStripMenuItem.Name = "anmeldenToolStripMenuItem";
            this.anmeldenToolStripMenuItem.Size = new System.Drawing.Size(89, 24);
            this.anmeldenToolStripMenuItem.Text = "Anmelden";
            this.anmeldenToolStripMenuItem.Click += new System.EventHandler(this.LoginOnClick);
            // 
            // registrierenToolStripMenuItem
            // 
            this.registrierenToolStripMenuItem.Name = "registrierenToolStripMenuItem";
            this.registrierenToolStripMenuItem.Size = new System.Drawing.Size(100, 24);
            this.registrierenToolStripMenuItem.Text = "Registrieren";
            this.registrierenToolStripMenuItem.Click += new System.EventHandler(this.RegistrierenOnClick);
            // 
            // meineAdressenToolStripMenuItem
            // 
            this.meineAdressenToolStripMenuItem.Name = "meineAdressenToolStripMenuItem";
            this.meineAdressenToolStripMenuItem.Size = new System.Drawing.Size(126, 24);
            this.meineAdressenToolStripMenuItem.Text = "Meine Adressen";
            // 
            // artikelBindingSource
            // 
            this.artikelBindingSource.DataSource = this.table;
            // 
            // artikelGridView
            // 
            this.artikelGridView.AllowUserToAddRows = false;
            this.artikelGridView.AllowUserToDeleteRows = false;
            this.artikelGridView.AutoGenerateColumns = false;
            this.artikelGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.artikelGridView.DataSource = this.artikelBindingSource;
            this.artikelGridView.Location = new System.Drawing.Point(3, 30);
            this.artikelGridView.MultiSelect = false;
            this.artikelGridView.Name = "artikelGridView";
            this.artikelGridView.ReadOnly = true;
            this.artikelGridView.RowTemplate.Height = 24;
            this.artikelGridView.Size = new System.Drawing.Size(814, 462);
            this.artikelGridView.TabIndex = 1;
            this.artikelGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ZelleOnClick);
            // 
            // meineSeiteGroupBox
            // 
            this.meineSeiteGroupBox.Controls.Add(this.nameAendernButton);
            this.meineSeiteGroupBox.Controls.Add(this.passwortAendernButton);
            this.meineSeiteGroupBox.Controls.Add(this.nameLabel);
            this.meineSeiteGroupBox.Location = new System.Drawing.Point(5, 30);
            this.meineSeiteGroupBox.Name = "meineSeiteGroupBox";
            this.meineSeiteGroupBox.Size = new System.Drawing.Size(811, 460);
            this.meineSeiteGroupBox.TabIndex = 2;
            this.meineSeiteGroupBox.TabStop = false;
            this.meineSeiteGroupBox.Text = "Meine Seite";
            // 
            // nameAendernButton
            // 
            this.nameAendernButton.Location = new System.Drawing.Point(543, 32);
            this.nameAendernButton.Name = "nameAendernButton";
            this.nameAendernButton.Size = new System.Drawing.Size(126, 23);
            this.nameAendernButton.TabIndex = 2;
            this.nameAendernButton.Text = "Name ändern";
            this.nameAendernButton.UseVisualStyleBackColor = true;
            this.nameAendernButton.Click += new System.EventHandler(this.nameAendernOnClick);
            // 
            // passwortAendernButton
            // 
            this.passwortAendernButton.Location = new System.Drawing.Point(675, 32);
            this.passwortAendernButton.Name = "passwortAendernButton";
            this.passwortAendernButton.Size = new System.Drawing.Size(126, 23);
            this.passwortAendernButton.TabIndex = 1;
            this.passwortAendernButton.Text = "Passwort ändern";
            this.passwortAendernButton.UseVisualStyleBackColor = true;
            this.passwortAendernButton.Click += new System.EventHandler(this.passwortAendernOnClick);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(7, 32);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(49, 17);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Name:";
            // 
            // meineAdressenGroupBox
            // 
            this.meineAdressenGroupBox.Controls.Add(this.adressFlowLayoutPanel);
            this.meineAdressenGroupBox.Location = new System.Drawing.Point(6, 33);
            this.meineAdressenGroupBox.Name = "meineAdressenGroupBox";
            this.meineAdressenGroupBox.Size = new System.Drawing.Size(810, 458);
            this.meineAdressenGroupBox.TabIndex = 3;
            this.meineAdressenGroupBox.TabStop = false;
            this.meineAdressenGroupBox.Text = "Meine Adressen";
            // 
            // adressFlowLayoutPanel
            // 
            this.adressFlowLayoutPanel.Location = new System.Drawing.Point(5, 19);
            this.adressFlowLayoutPanel.Name = "adressFlowLayoutPanel";
            this.adressFlowLayoutPanel.Size = new System.Drawing.Size(804, 397);
            this.adressFlowLayoutPanel.TabIndex = 0;
            // 
            // GUI_Main
            // 
            this.ClientSize = new System.Drawing.Size(818, 496);
            this.Controls.Add(this.meineAdressenGroupBox);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.artikelGridView);
            this.Controls.Add(this.meineSeiteGroupBox);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GUI_Main";
            this.Text = "reverse ebay App";
            ((System.ComponentModel.ISupportInitialize)(this.table)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.artikelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.artikelGridView)).EndInit();
            this.meineSeiteGroupBox.ResumeLayout(false);
            this.meineSeiteGroupBox.PerformLayout();
            this.meineAdressenGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Initialize()
        {
            this.artikelGridView.AutoGenerateColumns = true;
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kurzbeschreibungDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ablaufdatumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gebotDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gebotenVonDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();

            if (table.Columns.Count == 0)
            {
                table.Columns.Add("Name", typeof(string));
                table.Columns.Add("Kurzbeschreibung", typeof(string));
                table.Columns.Add("Ablaufdatum", typeof(DateTime));
                table.Columns.Add("Gebot", typeof(string));
                table.Columns.Add("Geboten von", typeof(string));
            }
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // kurzbeschreibungDataGridViewTextBoxColumn
            // 
            this.kurzbeschreibungDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.kurzbeschreibungDataGridViewTextBoxColumn.DataPropertyName = "Kurzbeschreibung";
            this.kurzbeschreibungDataGridViewTextBoxColumn.HeaderText = "Kurzbeschreibung";
            this.kurzbeschreibungDataGridViewTextBoxColumn.Name = "kurzbeschreibungDataGridViewTextBoxColumn";
            this.kurzbeschreibungDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ablaufdatumDataGridViewTextBoxColumn
            // 
            this.ablaufdatumDataGridViewTextBoxColumn.DataPropertyName = "Ablaufdatum";
            this.ablaufdatumDataGridViewTextBoxColumn.HeaderText = "Ablaufdatum";
            this.ablaufdatumDataGridViewTextBoxColumn.Name = "ablaufdatumDataGridViewTextBoxColumn";
            this.ablaufdatumDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // gebotDataGridViewTextBoxColumn
            // 
            this.gebotDataGridViewTextBoxColumn.DataPropertyName = "Gebot";
            this.gebotDataGridViewTextBoxColumn.HeaderText = "Gebot";
            this.gebotDataGridViewTextBoxColumn.Name = "gebotDataGridViewTextBoxColumn";
            this.gebotDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // gebotenVonDataGridViewTextBoxColumn
            // 
            this.gebotenVonDataGridViewTextBoxColumn.DataPropertyName = "Geboten von";
            this.gebotenVonDataGridViewTextBoxColumn.HeaderText = "Geboten von";
            this.gebotenVonDataGridViewTextBoxColumn.Name = "gebotenVonDataGridViewTextBoxColumn";
            this.gebotenVonDataGridViewTextBoxColumn.ReadOnly = true;
            this.gebotenVonDataGridViewTextBoxColumn.Width = 120;

            UpdateData();
            Reload();
        }
        private void UpdateData()
        {
            table.Clear();
            List<Artikel> artikelListe = fachkonzept.gibArtikelListe("");
            foreach (Artikel einzelnerArtikel in artikelListe)
            {
                DataRow neueZeile = table.NewRow();
                neueZeile["Name"] = einzelnerArtikel.name;
                neueZeile["Kurzbeschreibung"] = einzelnerArtikel.kurzbeschr;
                neueZeile["Ablaufdatum"] = einzelnerArtikel.ablaufdatum;
                neueZeile["Gebot"] = string.Format("{0} EUR", einzelnerArtikel.hoechstgebot.ToString("0.00"));
                neueZeile["Geboten von"] = fachkonzept.gibBenutzer(einzelnerArtikel.bieter_id).name;
                table.Rows.Add(neueZeile);
            }
            if (fachkonzept.gibAktBenutzer() != null)
            {
                this.nameLabel.Text = String.Format("Name: {0}", fachkonzept.gibAktBenutzer().name);
            }
                
        }

        private void LoginOnClick(object sender, EventArgs e)
        {
            if (fachkonzept.gibAktBenutzer() == null)
            {
                GUI_Login loginPage = new GUI_Login(fachkonzept);
                loginPage.ShowDialog();
                if (fachkonzept.gibAktBenutzer() != null)
                {
                    aktuellerStatus = status.eingeloggt;
                }
            } else
            {
                fachkonzept.ausloggen();
                if (fachkonzept.gibAktBenutzer() == null)
                {
                    aktuellerStatus = status.ausgeloggt;
                }
            }
            Reload();
        }

        private void Reload()
        {
            switch (aktuellerStatus) 
            {
                case status.ausgeloggt:
                    artikelGridView.Visible = true;
                    meineSeiteGroupBox.Visible = false;
                    meineAdressenGroupBox.Visible = false;
                    meineAdressenToolStripMenuItem.Visible = false;
                    anmeldenToolStripMenuItem.Text = "Anmelden";
                    registrierenToolStripMenuItem.Text = "Registrieren";
                    break;
                case status.eingeloggt:
                    artikelGridView.Visible = true;
                    meineSeiteGroupBox.Visible = false;
                    meineAdressenGroupBox.Visible = false;
                    meineAdressenToolStripMenuItem.Visible = false;
                    anmeldenToolStripMenuItem.Text = "Abmelden";
                    registrierenToolStripMenuItem.Text = "Meine Seite";
                    break;
                case status.meineSeite:
                    artikelGridView.Visible = false;
                    meineSeiteGroupBox.Visible = true;
                    meineAdressenGroupBox.Visible = false;
                    meineAdressenToolStripMenuItem.Visible = true;
                    meineAdressenToolStripMenuItem.Text = "Meine Adressen";
                    anmeldenToolStripMenuItem.Text = "Abmelden";
                    registrierenToolStripMenuItem.Text = "Zurück zur Hauptseite";
                    break;
                case status.Adressen:
                    artikelGridView.Visible = false;
                    meineSeiteGroupBox.Visible = false;
                    meineAdressenGroupBox.Visible = true;
                    meineAdressenToolStripMenuItem.Visible = true;
                    meineAdressenToolStripMenuItem.Text = "Meine Seite";
                    anmeldenToolStripMenuItem.Text = "Abmelden";
                    registrierenToolStripMenuItem.Text = "Zurück zur Hauptseite";
                    break;
            }
            UpdateData();
        }

        private void ladeAdressen()
        {
            TextBox neueTextBox = new TextBox();
            List<BenutzerAdresse> meineAdressen = fachkonzept.meineAdressen();
            foreach (BenutzerAdresse eineAdresse in meineAdressen)
            {
                neueTextBox.Text = string.Format("{0} {1}", eineAdresse.vname, eineAdresse.nname);
                if (!eineAdresse.addr_zusatz.Equals("")) { //TODO
                    neueTextBox.Text += string.Format("\r\n{0}",eineAdresse.addr_zusatz);
                }
                neueTextBox.Text = string.Format("\r\n{0}\r\n{1} {2}\r\n{3}");
            }
        }

        private void RegistrierenOnClick(object sender, EventArgs e)
        {
            switch (aktuellerStatus)
            {
                case status.ausgeloggt:
                    GUI_Register registerPage = new GUI_Register(fachkonzept);
                    registerPage.ShowDialog();
                    break;
                case status.eingeloggt:
                    aktuellerStatus = status.meineSeite;
                    break;
                case status.meineSeite:
                    aktuellerStatus = status.eingeloggt;
                    break;
            }
            Reload();
        }

        private void ZelleOnClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void nameAendernOnClick(object sender, EventArgs e)
        {
            GUI_NameAendern nameAendernPage = new GUI_NameAendern(fachkonzept);
            nameAendernPage.ShowDialog();
            Reload();
        }

        private void passwortAendernOnClick(object sender, EventArgs e)
        {
            GUI_PasswortAendern passwortAendernPage = new GUI_PasswortAendern(fachkonzept);
            passwortAendernPage.ShowDialog();
            Reload();
        }
    }
}
