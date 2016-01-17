using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace reverse_ebay
{
    class GUI_Main : Form
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
        private Label adressErrorLabel;
        private Label adresseAendernLabel;
        private Label landLabel;
        private Label ortLabel;
        private Label plzLabel;
        private Label StrNrLabel;
        private Label addrzusatzLabel;
        private Label nnameLabel;
        private Label vnameLabel;
        private TextBox landTextBox;
        private TextBox ortTextBox;
        private TextBox plzTextBox;
        private TextBox strNrTextBox;
        private TextBox addrzusatzTextBox;
        private TextBox nachnameTextBox;
        private TextBox vornameTextBox;
        private Button neueAdresseButton;
        private Button speichernButton;
        private Button felderLeerenButton;
        private CheckBox liefAdresseCheckbox;
        private CheckBox rechAdresseCheckbox;
        private Label liefAdresseLabel;
        private Label rechAdresseLabel;
        List<BenutzerAdresse> meineAdressen;
        private Button adresseLoeschenButton;

        private enum status { ausgeloggt, eingeloggt, meineSeite, Adressen, Artikel };
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
            this.adresseLoeschenButton = new System.Windows.Forms.Button();
            this.liefAdresseCheckbox = new System.Windows.Forms.CheckBox();
            this.rechAdresseCheckbox = new System.Windows.Forms.CheckBox();
            this.liefAdresseLabel = new System.Windows.Forms.Label();
            this.rechAdresseLabel = new System.Windows.Forms.Label();
            this.neueAdresseButton = new System.Windows.Forms.Button();
            this.speichernButton = new System.Windows.Forms.Button();
            this.felderLeerenButton = new System.Windows.Forms.Button();
            this.adressErrorLabel = new System.Windows.Forms.Label();
            this.adresseAendernLabel = new System.Windows.Forms.Label();
            this.landLabel = new System.Windows.Forms.Label();
            this.ortLabel = new System.Windows.Forms.Label();
            this.plzLabel = new System.Windows.Forms.Label();
            this.StrNrLabel = new System.Windows.Forms.Label();
            this.addrzusatzLabel = new System.Windows.Forms.Label();
            this.nnameLabel = new System.Windows.Forms.Label();
            this.vnameLabel = new System.Windows.Forms.Label();
            this.landTextBox = new System.Windows.Forms.TextBox();
            this.ortTextBox = new System.Windows.Forms.TextBox();
            this.plzTextBox = new System.Windows.Forms.TextBox();
            this.strNrTextBox = new System.Windows.Forms.TextBox();
            this.addrzusatzTextBox = new System.Windows.Forms.TextBox();
            this.nachnameTextBox = new System.Windows.Forms.TextBox();
            this.vornameTextBox = new System.Windows.Forms.TextBox();
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
            this.meineAdressenToolStripMenuItem.Click += new System.EventHandler(this.meineAdressenOnClick);
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
            this.meineAdressenGroupBox.Controls.Add(this.adresseLoeschenButton);
            this.meineAdressenGroupBox.Controls.Add(this.liefAdresseCheckbox);
            this.meineAdressenGroupBox.Controls.Add(this.rechAdresseCheckbox);
            this.meineAdressenGroupBox.Controls.Add(this.liefAdresseLabel);
            this.meineAdressenGroupBox.Controls.Add(this.rechAdresseLabel);
            this.meineAdressenGroupBox.Controls.Add(this.neueAdresseButton);
            this.meineAdressenGroupBox.Controls.Add(this.speichernButton);
            this.meineAdressenGroupBox.Controls.Add(this.felderLeerenButton);
            this.meineAdressenGroupBox.Controls.Add(this.adressErrorLabel);
            this.meineAdressenGroupBox.Controls.Add(this.adresseAendernLabel);
            this.meineAdressenGroupBox.Controls.Add(this.landLabel);
            this.meineAdressenGroupBox.Controls.Add(this.ortLabel);
            this.meineAdressenGroupBox.Controls.Add(this.plzLabel);
            this.meineAdressenGroupBox.Controls.Add(this.StrNrLabel);
            this.meineAdressenGroupBox.Controls.Add(this.addrzusatzLabel);
            this.meineAdressenGroupBox.Controls.Add(this.nnameLabel);
            this.meineAdressenGroupBox.Controls.Add(this.vnameLabel);
            this.meineAdressenGroupBox.Controls.Add(this.landTextBox);
            this.meineAdressenGroupBox.Controls.Add(this.ortTextBox);
            this.meineAdressenGroupBox.Controls.Add(this.plzTextBox);
            this.meineAdressenGroupBox.Controls.Add(this.strNrTextBox);
            this.meineAdressenGroupBox.Controls.Add(this.addrzusatzTextBox);
            this.meineAdressenGroupBox.Controls.Add(this.nachnameTextBox);
            this.meineAdressenGroupBox.Controls.Add(this.vornameTextBox);
            this.meineAdressenGroupBox.Controls.Add(this.adressFlowLayoutPanel);
            this.meineAdressenGroupBox.Location = new System.Drawing.Point(6, 33);
            this.meineAdressenGroupBox.Name = "meineAdressenGroupBox";
            this.meineAdressenGroupBox.Size = new System.Drawing.Size(810, 458);
            this.meineAdressenGroupBox.TabIndex = 3;
            this.meineAdressenGroupBox.TabStop = false;
            this.meineAdressenGroupBox.Text = "Meine Adressen";
            // 
            // adresseLoeschenButton
            // 
            this.adresseLoeschenButton.Location = new System.Drawing.Point(414, 360);
            this.adresseLoeschenButton.Name = "adresseLoeschenButton";
            this.adresseLoeschenButton.Size = new System.Drawing.Size(190, 34);
            this.adresseLoeschenButton.TabIndex = 24;
            this.adresseLoeschenButton.Text = "Adresse löschen";
            this.adresseLoeschenButton.UseVisualStyleBackColor = true;
            this.adresseLoeschenButton.Click += new System.EventHandler(this.AdresseLoeschenOnClick);
            // 
            // liefAdresseCheckbox
            // 
            this.liefAdresseCheckbox.AutoSize = true;
            this.liefAdresseCheckbox.Location = new System.Drawing.Point(610, 332);
            this.liefAdresseCheckbox.Name = "liefAdresseCheckbox";
            this.liefAdresseCheckbox.Size = new System.Drawing.Size(18, 17);
            this.liefAdresseCheckbox.TabIndex = 23;
            this.liefAdresseCheckbox.UseVisualStyleBackColor = true;
            // 
            // rechAdresseCheckbox
            // 
            this.rechAdresseCheckbox.AutoSize = true;
            this.rechAdresseCheckbox.Location = new System.Drawing.Point(610, 305);
            this.rechAdresseCheckbox.Name = "rechAdresseCheckbox";
            this.rechAdresseCheckbox.Size = new System.Drawing.Size(18, 17);
            this.rechAdresseCheckbox.TabIndex = 22;
            this.rechAdresseCheckbox.UseVisualStyleBackColor = true;
            // 
            // liefAdresseLabel
            // 
            this.liefAdresseLabel.AutoSize = true;
            this.liefAdresseLabel.Location = new System.Drawing.Point(443, 332);
            this.liefAdresseLabel.Name = "liefAdresseLabel";
            this.liefAdresseLabel.Size = new System.Drawing.Size(99, 17);
            this.liefAdresseLabel.TabIndex = 21;
            this.liefAdresseLabel.Text = "Lieferadresse:";
            // 
            // rechAdresseLabel
            // 
            this.rechAdresseLabel.AutoSize = true;
            this.rechAdresseLabel.Location = new System.Drawing.Point(443, 305);
            this.rechAdresseLabel.Name = "rechAdresseLabel";
            this.rechAdresseLabel.Size = new System.Drawing.Size(135, 17);
            this.rechAdresseLabel.TabIndex = 20;
            this.rechAdresseLabel.Text = "Rechnungsadresse:";
            // 
            // neueAdresseButton
            // 
            this.neueAdresseButton.Location = new System.Drawing.Point(414, 400);
            this.neueAdresseButton.Name = "neueAdresseButton";
            this.neueAdresseButton.Size = new System.Drawing.Size(190, 34);
            this.neueAdresseButton.TabIndex = 19;
            this.neueAdresseButton.Text = "Als neue Adresse anlegen";
            this.neueAdresseButton.UseVisualStyleBackColor = true;
            this.neueAdresseButton.Click += new System.EventHandler(this.NeueAdresseOnClick);
            // 
            // speichernButton
            // 
            this.speichernButton.Location = new System.Drawing.Point(610, 400);
            this.speichernButton.Name = "speichernButton";
            this.speichernButton.Size = new System.Drawing.Size(190, 34);
            this.speichernButton.TabIndex = 18;
            this.speichernButton.Text = "Änderungen speichern";
            this.speichernButton.UseVisualStyleBackColor = true;
            this.speichernButton.Click += new System.EventHandler(this.AdresseAendernOnClick);
            // 
            // felderLeerenButton
            // 
            this.felderLeerenButton.Location = new System.Drawing.Point(610, 360);
            this.felderLeerenButton.Name = "felderLeerenButton";
            this.felderLeerenButton.Size = new System.Drawing.Size(190, 34);
            this.felderLeerenButton.TabIndex = 17;
            this.felderLeerenButton.Text = "Felder leeren";
            this.felderLeerenButton.UseVisualStyleBackColor = true;
            this.felderLeerenButton.Click += new System.EventHandler(this.leereFelderOnClick);
            // 
            // adressErrorLabel
            // 
            this.adressErrorLabel.AutoSize = true;
            this.adressErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adressErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.adressErrorLabel.Location = new System.Drawing.Point(443, 66);
            this.adressErrorLabel.Name = "adressErrorLabel";
            this.adressErrorLabel.Size = new System.Drawing.Size(0, 17);
            this.adressErrorLabel.TabIndex = 16;
            // 
            // adresseAendernLabel
            // 
            this.adresseAendernLabel.AutoSize = true;
            this.adresseAendernLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.adresseAendernLabel.Location = new System.Drawing.Point(442, 32);
            this.adresseAendernLabel.Name = "adresseAendernLabel";
            this.adresseAendernLabel.Size = new System.Drawing.Size(141, 20);
            this.adresseAendernLabel.TabIndex = 15;
            this.adresseAendernLabel.Text = "Adresse ändern";
            // 
            // landLabel
            // 
            this.landLabel.AutoSize = true;
            this.landLabel.Location = new System.Drawing.Point(443, 277);
            this.landLabel.Name = "landLabel";
            this.landLabel.Size = new System.Drawing.Size(44, 17);
            this.landLabel.TabIndex = 14;
            this.landLabel.Text = "Land:";
            // 
            // ortLabel
            // 
            this.ortLabel.AutoSize = true;
            this.ortLabel.Location = new System.Drawing.Point(443, 249);
            this.ortLabel.Name = "ortLabel";
            this.ortLabel.Size = new System.Drawing.Size(32, 17);
            this.ortLabel.TabIndex = 13;
            this.ortLabel.Text = "Ort:";
            // 
            // plzLabel
            // 
            this.plzLabel.AutoSize = true;
            this.plzLabel.Location = new System.Drawing.Point(443, 221);
            this.plzLabel.Name = "plzLabel";
            this.plzLabel.Size = new System.Drawing.Size(84, 17);
            this.plzLabel.TabIndex = 12;
            this.plzLabel.Text = "Postleitzahl:";
            // 
            // StrNrLabel
            // 
            this.StrNrLabel.AutoSize = true;
            this.StrNrLabel.Location = new System.Drawing.Point(443, 193);
            this.StrNrLabel.Name = "StrNrLabel";
            this.StrNrLabel.Size = new System.Drawing.Size(143, 17);
            this.StrNrLabel.TabIndex = 11;
            this.StrNrLabel.Text = "Straße Hausnummer:";
            // 
            // addrzusatzLabel
            // 
            this.addrzusatzLabel.AutoSize = true;
            this.addrzusatzLabel.Location = new System.Drawing.Point(443, 165);
            this.addrzusatzLabel.Name = "addrzusatzLabel";
            this.addrzusatzLabel.Size = new System.Drawing.Size(161, 17);
            this.addrzusatzLabel.TabIndex = 10;
            this.addrzusatzLabel.Text = "Adresszusatz (optional):";
            // 
            // nnameLabel
            // 
            this.nnameLabel.AutoSize = true;
            this.nnameLabel.Location = new System.Drawing.Point(443, 137);
            this.nnameLabel.Name = "nnameLabel";
            this.nnameLabel.Size = new System.Drawing.Size(84, 17);
            this.nnameLabel.TabIndex = 9;
            this.nnameLabel.Text = "Nachname: ";
            // 
            // vnameLabel
            // 
            this.vnameLabel.AutoSize = true;
            this.vnameLabel.Location = new System.Drawing.Point(443, 109);
            this.vnameLabel.Name = "vnameLabel";
            this.vnameLabel.Size = new System.Drawing.Size(69, 17);
            this.vnameLabel.TabIndex = 8;
            this.vnameLabel.Text = "Vorname:";
            // 
            // landTextBox
            // 
            this.landTextBox.Location = new System.Drawing.Point(610, 274);
            this.landTextBox.Name = "landTextBox";
            this.landTextBox.Size = new System.Drawing.Size(190, 22);
            this.landTextBox.TabIndex = 7;
            // 
            // ortTextBox
            // 
            this.ortTextBox.Location = new System.Drawing.Point(610, 246);
            this.ortTextBox.Name = "ortTextBox";
            this.ortTextBox.Size = new System.Drawing.Size(190, 22);
            this.ortTextBox.TabIndex = 6;
            // 
            // plzTextBox
            // 
            this.plzTextBox.Location = new System.Drawing.Point(610, 218);
            this.plzTextBox.Name = "plzTextBox";
            this.plzTextBox.Size = new System.Drawing.Size(190, 22);
            this.plzTextBox.TabIndex = 5;
            // 
            // strNrTextBox
            // 
            this.strNrTextBox.Location = new System.Drawing.Point(610, 190);
            this.strNrTextBox.Name = "strNrTextBox";
            this.strNrTextBox.Size = new System.Drawing.Size(190, 22);
            this.strNrTextBox.TabIndex = 4;
            // 
            // addrzusatzTextBox
            // 
            this.addrzusatzTextBox.Location = new System.Drawing.Point(610, 162);
            this.addrzusatzTextBox.Name = "addrzusatzTextBox";
            this.addrzusatzTextBox.Size = new System.Drawing.Size(190, 22);
            this.addrzusatzTextBox.TabIndex = 3;
            // 
            // nachnameTextBox
            // 
            this.nachnameTextBox.Location = new System.Drawing.Point(610, 134);
            this.nachnameTextBox.Name = "nachnameTextBox";
            this.nachnameTextBox.Size = new System.Drawing.Size(190, 22);
            this.nachnameTextBox.TabIndex = 2;
            // 
            // vornameTextBox
            // 
            this.vornameTextBox.Location = new System.Drawing.Point(610, 106);
            this.vornameTextBox.Name = "vornameTextBox";
            this.vornameTextBox.Size = new System.Drawing.Size(190, 22);
            this.vornameTextBox.TabIndex = 1;
            // 
            // adressFlowLayoutPanel
            // 
            this.adressFlowLayoutPanel.AutoScroll = true;
            this.adressFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.adressFlowLayoutPanel.Location = new System.Drawing.Point(5, 19);
            this.adressFlowLayoutPanel.Name = "adressFlowLayoutPanel";
            this.adressFlowLayoutPanel.Size = new System.Drawing.Size(403, 432);
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
            this.meineAdressenGroupBox.PerformLayout();
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

            meineAdressen = new List<BenutzerAdresse>();

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
                    ladeAdressen();
                }
            }
            else
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
            Button neueTextBox = new Button();
            //neueTextBox.Multiline = true;
            neueTextBox.Size = new Size(150, 100);
            neueTextBox.Click += new System.EventHandler(this.AdresseButtonOnClick);
            meineAdressen.Clear();
            adressFlowLayoutPanel.Controls.Clear();
            meineAdressen = fachkonzept.meineAdressen();
            foreach (BenutzerAdresse eineAdresse in meineAdressen)
            {
                neueTextBox = new Button();
                //neueTextBox.Multiline = true;
                neueTextBox.Size = new Size(150, 100);
                neueTextBox.Click += new System.EventHandler(this.AdresseButtonOnClick);
                neueTextBox.Name = meineAdressen.IndexOf(eineAdresse).ToString();
                neueTextBox.Text = string.Format("{0} {1}", eineAdresse.vname, eineAdresse.nname);
                if (!eineAdresse.addr_zusatz.Equals(""))
                {
                    neueTextBox.Text += string.Format("\r\n{0}", eineAdresse.addr_zusatz);
                }
                neueTextBox.Text += string.Format("\r\n{0}\r\n{1} {2}\r\n{3}",
                    eineAdresse.adresse.str_nr, eineAdresse.adresse.plz, eineAdresse.adresse.ort, eineAdresse.adresse.land);
                if (eineAdresse.rech_addr)
                {
                    neueTextBox.Text += "\r\n- Rechnungsadresse";
                }
                if (eineAdresse.lief_addr)
                {
                    neueTextBox.Text += "\r\n- Lieferadresse";
                }
                adressFlowLayoutPanel.Controls.Add(neueTextBox);
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
            //TODO
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

        private void meineAdressenOnClick(object sender, EventArgs e)
        {
            switch (aktuellerStatus)
            {
                case status.meineSeite:
                    aktuellerStatus = status.Adressen;
                    break;
                case status.Adressen:
                    aktuellerStatus = status.meineSeite;
                    break;
            }
            Reload();
        }
        private void AdresseButtonOnClick(object sender, EventArgs e)
        {
            int adressIndex = Convert.ToInt32(((Button)sender).Name);
            BenutzerAdresse dieseAdresse = meineAdressen[adressIndex];
            vornameTextBox.Text = dieseAdresse.vname;
            nachnameTextBox.Text = dieseAdresse.nname;
            addrzusatzTextBox.Text = dieseAdresse.addr_zusatz;
            liefAdresseCheckbox.Checked = dieseAdresse.lief_addr;
            rechAdresseCheckbox.Checked = dieseAdresse.rech_addr;
            strNrTextBox.Text = dieseAdresse.adresse.str_nr;
            plzTextBox.Text = dieseAdresse.adresse.plz;
            ortTextBox.Text = dieseAdresse.adresse.ort;
            landTextBox.Text = dieseAdresse.adresse.land;
            adressErrorLabel.Text = "";
        }

        private void AdresseBearbeiten (bool neuAnlegen)
        {
            if (!(vornameTextBox.Text.Equals("") || nachnameTextBox.Text.Equals("") || strNrTextBox.Text.Equals("") || plzTextBox.Text.Equals("") || ortTextBox.Text.Equals("") || landTextBox.Text.Equals("")))
            {
                BenutzerAdresse andereBenutzerAdresse = new BenutzerAdresse();
                Adresse andereAdresse = new Adresse();
                andereAdresse.str_nr = strNrTextBox.Text;
                andereAdresse.plz = plzTextBox.Text;
                andereAdresse.ort = ortTextBox.Text;
                andereAdresse.land = landTextBox.Text;
                andereBenutzerAdresse.vname = vornameTextBox.Text;
                andereBenutzerAdresse.nname = nachnameTextBox.Text;
                andereBenutzerAdresse.addr_zusatz = addrzusatzTextBox.Text;
                andereBenutzerAdresse.benutzer_id = fachkonzept.gibAktBenutzer().id;
                andereBenutzerAdresse.rech_addr = rechAdresseCheckbox.Checked;
                andereBenutzerAdresse.lief_addr = liefAdresseCheckbox.Checked;
                andereBenutzerAdresse.adresse = andereAdresse;
                if (neuAnlegen)
                {
                    if (fachkonzept.erzeugeBenutzerAdresse(andereBenutzerAdresse))
                    {
                        adressErrorLabel.ForeColor = System.Drawing.Color.Green;
                        adressErrorLabel.Text = "Erstellen erfolgreich.";
                        ladeAdressen();
                    }
                    else
                    {
                        adressErrorLabel.ForeColor = System.Drawing.Color.Red;
                        adressErrorLabel.Text = "Erstellen nicht erfolgreich. Bitte versuchen Sie es erneut.";
                    }
                }
                else
                {
                    if (fachkonzept.aendereBenutzerAdresse(andereBenutzerAdresse))
                    {
                        adressErrorLabel.ForeColor = System.Drawing.Color.Green;
                        adressErrorLabel.Text = "Ändern erfolgreich.";
                        ladeAdressen();
                    }
                    else
                    {
                        adressErrorLabel.ForeColor = System.Drawing.Color.Red;
                        adressErrorLabel.Text = "Ändern nicht erfolgreich. Bitte versuchen Sie es erneut.";
                    }
                }
            }
            else
            {
                adressErrorLabel.ForeColor = System.Drawing.Color.Red;
                adressErrorLabel.Text = "Es sind nicht alle Felder gefüllt.";
            }          
        }

        private void NeueAdresseOnClick(object sender, EventArgs e)
        {
            AdresseBearbeiten(true);
        }

        private void AdresseAendernOnClick(object sender, EventArgs e)
        {
            AdresseBearbeiten(false);
        }

        private void leereFelder()
        {
            vornameTextBox.Text = "";
            nachnameTextBox.Text = "";
            addrzusatzTextBox.Text = "";
            liefAdresseCheckbox.Checked = false;
            rechAdresseCheckbox.Checked = false;
            strNrTextBox.Text = "";
            plzTextBox.Text = "";
            ortTextBox.Text = "";
            landTextBox.Text = "";
        }

        private void AdresseLoeschenOnClick(object sender, EventArgs e)
        {
            BenutzerAdresse andereBenutzerAdresse = new BenutzerAdresse();
            Adresse andereAdresse = new Adresse();
            andereAdresse.str_nr = strNrTextBox.Text;
            andereAdresse.plz = plzTextBox.Text;
            andereAdresse.ort = ortTextBox.Text;
            andereAdresse.land = landTextBox.Text;
            andereBenutzerAdresse.vname = vornameTextBox.Text;
            andereBenutzerAdresse.nname = nachnameTextBox.Text;
            andereBenutzerAdresse.addr_zusatz = addrzusatzTextBox.Text;
            andereBenutzerAdresse.benutzer_id = fachkonzept.gibAktBenutzer().id;
            andereBenutzerAdresse.rech_addr = rechAdresseCheckbox.Checked;
            andereBenutzerAdresse.lief_addr = liefAdresseCheckbox.Checked;
            andereBenutzerAdresse.adresse = andereAdresse;

            if (fachkonzept.loescheBenutzerAdresse(andereBenutzerAdresse))
            {
                adressErrorLabel.ForeColor = System.Drawing.Color.Green;
                adressErrorLabel.Text = "Löschen erfolgreich.";
                ladeAdressen();
                leereFelder();
            }
            else
            {
                adressErrorLabel.ForeColor = System.Drawing.Color.Red;
                adressErrorLabel.Text = "Löschen nicht erfolgreich. Bitte versuchen Sie es erneut.";
            }
        }

        private void leereFelderOnClick(object sender, EventArgs e)
        {
            leereFelder();
        }
    }
}
