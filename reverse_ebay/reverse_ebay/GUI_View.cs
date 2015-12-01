using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reverse_ebay
{
    class GUI_View:Form
    {
        private MenuStrip menuStrip1;
        private ToolStripMenuItem anmeldenToolStripMenuItem;
        private ToolStripMenuItem registrierenToolStripMenuItem;
        private DataGridView dataGridView1;
        private ToolStripMenuItem wunschEintragenToolStripMenuItem;

        public GUI_View()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.anmeldenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registrierenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wunschEintragenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
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
            // 
            // registrierenToolStripMenuItem
            // 
            this.registrierenToolStripMenuItem.Name = "registrierenToolStripMenuItem";
            this.registrierenToolStripMenuItem.Size = new System.Drawing.Size(100, 24);
            this.registrierenToolStripMenuItem.Text = "Registrieren";
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
            // GUI_View
            // 
            this.ClientSize = new System.Drawing.Size(818, 496);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GUI_View";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Point Point(int p1, int p2)
        {
            throw new NotImplementedException();
        }
    }
}
