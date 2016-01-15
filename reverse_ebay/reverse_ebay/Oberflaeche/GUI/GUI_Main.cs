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
            this.menuStrip1 = new MenuStrip();
            this.anmeldenToolStripMenuItem = new ToolStripMenuItem();
            this.registrierenToolStripMenuItem = new ToolStripMenuItem();
            this.wunschEintragenToolStripMenuItem = new ToolStripMenuItem();
            this.dataGridView1 = new DataGridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new Size(20, 20);
            this.menuStrip1.Items.AddRange(new ToolStripItem[] {
            this.anmeldenToolStripMenuItem,
            this.registrierenToolStripMenuItem,
            this.wunschEintragenToolStripMenuItem});
            this.menuStrip1.Location = new Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new Size(818, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // anmeldenToolStripMenuItem
            // 
            this.anmeldenToolStripMenuItem.Name = "anmeldenToolStripMenuItem";
            this.anmeldenToolStripMenuItem.Size = new Size(89, 24);
            this.anmeldenToolStripMenuItem.Text = "Anmelden";
            this.anmeldenToolStripMenuItem.Click += new System.EventHandler(this.LoginOnClick);
            // 
            // registrierenToolStripMenuItem
            // 
            this.registrierenToolStripMenuItem.Name = "registrierenToolStripMenuItem";
            this.registrierenToolStripMenuItem.Size = new Size(100, 24);
            this.registrierenToolStripMenuItem.Text = "Registrieren";
            // 
            // wunschEintragenToolStripMenuItem
            // 
            this.wunschEintragenToolStripMenuItem.Name = "wunschEintragenToolStripMenuItem";
            this.wunschEintragenToolStripMenuItem.Size = new Size(139, 24);
            this.wunschEintragenToolStripMenuItem.Text = "Wunsch eintragen";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new Point(3, 30);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new Size(814, 462);
            this.dataGridView1.TabIndex = 1;
            // 
            // GUI_Main
            // 
            this.ClientSize = new Size(818, 496);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GUI_Main";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
    }
}
