using System;
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
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn kurzbeschrDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn langbeschrDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn ablaufdatumDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn hoechstgebotDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn bieteridDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn anbieteridDataGridViewTextBoxColumn;
        private BindingSource artikelBindingSource;
        private System.ComponentModel.IContainer components;
        private ToolStripMenuItem wunschEintragenToolStripMenuItem;

        public GUI_Main()
        {
            InitializeComponent();
        }

        public GUI_Main(IFachkonzept fachkonzept)
        {
            this.fachkonzept = fachkonzept;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.anmeldenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registrierenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wunschEintragenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kurzbeschrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.langbeschrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ablaufdatumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hoechstgebotDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bieteridDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.anbieteridDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.artikelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.artikelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.anmeldenToolStripMenuItem,
            this.registrierenToolStripMenuItem,
            this.wunschEintragenToolStripMenuItem});
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
            // wunschEintragenToolStripMenuItem
            // 
            this.wunschEintragenToolStripMenuItem.Name = "wunschEintragenToolStripMenuItem";
            this.wunschEintragenToolStripMenuItem.Size = new System.Drawing.Size(139, 24);
            this.wunschEintragenToolStripMenuItem.Text = "Wunsch eintragen";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 30);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(814, 462);
            this.dataGridView1.TabIndex = 1;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.kurzbeschrDataGridViewTextBoxColumn,
            this.langbeschrDataGridViewTextBoxColumn,
            this.ablaufdatumDataGridViewTextBoxColumn,
            this.hoechstgebotDataGridViewTextBoxColumn,
            this.bieteridDataGridViewTextBoxColumn,
            this.anbieteridDataGridViewTextBoxColumn});
            this.dataGridView2.DataSource = this.artikelBindingSource;
            this.dataGridView2.Location = new System.Drawing.Point(0, 30);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(816, 461);
            this.dataGridView2.TabIndex = 2;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.Visible = false;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Artikel";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // kurzbeschrDataGridViewTextBoxColumn
            // 
            this.kurzbeschrDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.kurzbeschrDataGridViewTextBoxColumn.DataPropertyName = "kurzbeschr";
            this.kurzbeschrDataGridViewTextBoxColumn.HeaderText = "Kurzbeschreibung";
            this.kurzbeschrDataGridViewTextBoxColumn.Name = "kurzbeschrDataGridViewTextBoxColumn";
            // 
            // langbeschrDataGridViewTextBoxColumn
            // 
            this.langbeschrDataGridViewTextBoxColumn.DataPropertyName = "langbeschr";
            this.langbeschrDataGridViewTextBoxColumn.HeaderText = "langbeschr";
            this.langbeschrDataGridViewTextBoxColumn.Name = "langbeschrDataGridViewTextBoxColumn";
            this.langbeschrDataGridViewTextBoxColumn.Visible = false;
            // 
            // ablaufdatumDataGridViewTextBoxColumn
            // 
            this.ablaufdatumDataGridViewTextBoxColumn.DataPropertyName = "ablaufdatum";
            this.ablaufdatumDataGridViewTextBoxColumn.HeaderText = "Ablaufdatum";
            this.ablaufdatumDataGridViewTextBoxColumn.Name = "ablaufdatumDataGridViewTextBoxColumn";
            // 
            // hoechstgebotDataGridViewTextBoxColumn
            // 
            this.hoechstgebotDataGridViewTextBoxColumn.DataPropertyName = "hoechstgebot";
            this.hoechstgebotDataGridViewTextBoxColumn.HeaderText = "Höchstgebot";
            this.hoechstgebotDataGridViewTextBoxColumn.Name = "hoechstgebotDataGridViewTextBoxColumn";
            // 
            // bieteridDataGridViewTextBoxColumn
            // 
            this.bieteridDataGridViewTextBoxColumn.DataPropertyName = "bieter_id";
            this.bieteridDataGridViewTextBoxColumn.HeaderText = "Geboten von";
            this.bieteridDataGridViewTextBoxColumn.Name = "bieteridDataGridViewTextBoxColumn";
            this.bieteridDataGridViewTextBoxColumn.Width = 120;
            // 
            // anbieteridDataGridViewTextBoxColumn
            // 
            this.anbieteridDataGridViewTextBoxColumn.DataPropertyName = "anbieter_id";
            this.anbieteridDataGridViewTextBoxColumn.HeaderText = "Käufer";
            this.anbieteridDataGridViewTextBoxColumn.Name = "anbieteridDataGridViewTextBoxColumn";
            this.anbieteridDataGridViewTextBoxColumn.Width = 120;
            // 
            // artikelBindingSource
            // 
            this.artikelBindingSource.DataSource = typeof(reverse_ebay.Artikel);
            // 
            // GUI_Main
            // 
            this.ClientSize = new System.Drawing.Size(818, 496);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GUI_Main";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.artikelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void LoginOnClick(object sender, EventArgs e)
        {
            if (fachkonzept.gibAktBenutzer() == null)
            {
                GUI_Login loginPage = new GUI_Login(fachkonzept);
                loginPage.ShowDialog();
            } else
            {
                fachkonzept.ausloggen();
            }
            Reload();
        }

        private void Reload()
        {
            if (fachkonzept.gibAktBenutzer() == null)
            {
                anmeldenToolStripMenuItem.Text = "Anmelden";
            } else
            {
                anmeldenToolStripMenuItem.Text = "Abmelden";
            }
        }

        private void RegistrierenOnClick(object sender, EventArgs e)
        {
            if (fachkonzept.gibAktBenutzer() == null)
            {
                GUI_Login loginPage = new GUI_Login(fachkonzept);
                loginPage.ShowDialog();
            }
            Reload();
        }
    }
}
