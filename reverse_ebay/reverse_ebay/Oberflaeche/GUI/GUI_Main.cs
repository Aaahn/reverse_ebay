﻿using System;
using System.Collections.Generic;
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
        private DataGridViewTextBoxColumn gebotenVonDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn gebotDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn ablaufdatumDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn kurzbeschreibungDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private DataGridView dataGridView1;
        DataTable table;

        public GUI_Main()
        {
            InitializeComponent();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Kurzbeschreibung", typeof(string));
            table.Columns.Add("Ablaufdatum", typeof(DateTime));
            table.Columns.Add("Gebot", typeof(double));
            table.Columns.Add("Geboten von", typeof(string));
            UpdateData();
        }

        public GUI_Main(IFachkonzept fachkonzept)
        {
            this.fachkonzept = fachkonzept;
            InitializeComponent();

            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Kurzbeschreibung", typeof(string));
            table.Columns.Add("Ablaufdatum", typeof(DateTime));
            table.Columns.Add("Gebot", typeof(double));
            table.Columns.Add("Geboten von", typeof(string));
            UpdateData();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.table = new System.Data.DataTable();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.anmeldenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registrierenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.artikelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.table)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.artikelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.registrierenToolStripMenuItem});
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
            // artikelBindingSource
            // 
            this.artikelBindingSource.DataSource = typeof(reverse_ebay.Artikel);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.DataSource = this.table;
            this.dataGridView1.Location = new System.Drawing.Point(3, 30);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(814, 462);
            this.dataGridView1.TabIndex = 1;
            // 
            // GUI_Main
            // 
            this.ClientSize = new System.Drawing.Size(818, 496);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GUI_Main";
            ((System.ComponentModel.ISupportInitialize)(this.table)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.artikelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
                neueZeile["Gebot"] = einzelnerArtikel.hoechstgebot;
                neueZeile["Geboten von"] = fachkonzept.gibBenutzer(einzelnerArtikel.bieter_id).name;
                table.Rows.Add(neueZeile);

            }
                
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
                registrierenToolStripMenuItem.Text = "Registrieren";
            } else
            {
                anmeldenToolStripMenuItem.Text = "Abmelden";
                registrierenToolStripMenuItem.Text = "Meine Seite";
            }
            UpdateData();
        }

        private void RegistrierenOnClick(object sender, EventArgs e)
        {
            if (fachkonzept.gibAktBenutzer() == null)
            {
                GUI_Register registerPage = new GUI_Register(fachkonzept);
                registerPage.ShowDialog();
            }
            Reload();
        }
    }
}
