﻿
namespace MusicEditor.Forms
{
    partial class FormGestion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGestion));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnChangeFolder = new System.Windows.Forms.Button();
            this.txtPathFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gridMusicaCorrecta = new System.Windows.Forms.DataGridView();
            this.tabMusica = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblMusicaCorrecta = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gridMusicaIncorrecta = new System.Windows.Forms.DataGridView();
            this.lblMusicaIncorrecta = new System.Windows.Forms.Label();
            this.toolAcciones = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.LabelModificacies = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMusicaCorrecta)).BeginInit();
            this.tabMusica.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMusicaIncorrecta)).BeginInit();
            this.toolAcciones.SuspendLayout();
            this.StatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnChangeFolder);
            this.groupBox1.Controls.Add(this.txtPathFolder);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(24, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(907, 74);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // btnChangeFolder
            // 
            this.btnChangeFolder.Location = new System.Drawing.Point(742, 18);
            this.btnChangeFolder.Name = "btnChangeFolder";
            this.btnChangeFolder.Size = new System.Drawing.Size(138, 48);
            this.btnChangeFolder.TabIndex = 2;
            this.btnChangeFolder.Text = "cambiar carpeta";
            this.btnChangeFolder.UseVisualStyleBackColor = true;
            this.btnChangeFolder.Click += new System.EventHandler(this.btnChangeFolder_Click);
            // 
            // txtPathFolder
            // 
            this.txtPathFolder.Location = new System.Drawing.Point(93, 31);
            this.txtPathFolder.Name = "txtPathFolder";
            this.txtPathFolder.ReadOnly = true;
            this.txtPathFolder.Size = new System.Drawing.Size(620, 22);
            this.txtPathFolder.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Carpeta";
            // 
            // gridMusicaCorrecta
            // 
            this.gridMusicaCorrecta.AllowUserToAddRows = false;
            this.gridMusicaCorrecta.AllowUserToDeleteRows = false;
            this.gridMusicaCorrecta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMusicaCorrecta.Location = new System.Drawing.Point(31, 53);
            this.gridMusicaCorrecta.Name = "gridMusicaCorrecta";
            this.gridMusicaCorrecta.ReadOnly = true;
            this.gridMusicaCorrecta.RowHeadersWidth = 51;
            this.gridMusicaCorrecta.RowTemplate.Height = 24;
            this.gridMusicaCorrecta.Size = new System.Drawing.Size(834, 294);
            this.gridMusicaCorrecta.TabIndex = 1;
            this.gridMusicaCorrecta.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridMusicaCorrecta_CellContentClick);
            // 
            // tabMusica
            // 
            this.tabMusica.Controls.Add(this.tabPage1);
            this.tabMusica.Controls.Add(this.tabPage2);
            this.tabMusica.Location = new System.Drawing.Point(24, 129);
            this.tabMusica.Name = "tabMusica";
            this.tabMusica.SelectedIndex = 0;
            this.tabMusica.Size = new System.Drawing.Size(907, 397);
            this.tabMusica.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblMusicaCorrecta);
            this.tabPage1.Controls.Add(this.gridMusicaCorrecta);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(899, 368);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Musica Correcta";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblMusicaCorrecta
            // 
            this.lblMusicaCorrecta.AutoSize = true;
            this.lblMusicaCorrecta.Location = new System.Drawing.Point(28, 17);
            this.lblMusicaCorrecta.Name = "lblMusicaCorrecta";
            this.lblMusicaCorrecta.Size = new System.Drawing.Size(191, 17);
            this.lblMusicaCorrecta.TabIndex = 2;
            this.lblMusicaCorrecta.Text = "Musica con formato correcto:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gridMusicaIncorrecta);
            this.tabPage2.Controls.Add(this.lblMusicaIncorrecta);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(899, 368);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Musica Incorrecta";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gridMusicaIncorrecta
            // 
            this.gridMusicaIncorrecta.AllowUserToAddRows = false;
            this.gridMusicaIncorrecta.AllowUserToDeleteRows = false;
            this.gridMusicaIncorrecta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMusicaIncorrecta.Location = new System.Drawing.Point(32, 57);
            this.gridMusicaIncorrecta.Name = "gridMusicaIncorrecta";
            this.gridMusicaIncorrecta.ReadOnly = true;
            this.gridMusicaIncorrecta.RowHeadersWidth = 51;
            this.gridMusicaIncorrecta.RowTemplate.Height = 24;
            this.gridMusicaIncorrecta.Size = new System.Drawing.Size(834, 294);
            this.gridMusicaIncorrecta.TabIndex = 2;
            this.gridMusicaIncorrecta.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridMusicaIncorrecta_CellContentClick_1);
            // 
            // lblMusicaIncorrecta
            // 
            this.lblMusicaIncorrecta.AutoSize = true;
            this.lblMusicaIncorrecta.Location = new System.Drawing.Point(28, 17);
            this.lblMusicaIncorrecta.Name = "lblMusicaIncorrecta";
            this.lblMusicaIncorrecta.Size = new System.Drawing.Size(202, 17);
            this.lblMusicaIncorrecta.TabIndex = 1;
            this.lblMusicaIncorrecta.Text = "Musica con formato incorrecto:";
            // 
            // toolAcciones
            // 
            this.toolAcciones.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave});
            this.toolAcciones.Location = new System.Drawing.Point(0, 0);
            this.toolAcciones.Name = "toolAcciones";
            this.toolAcciones.Size = new System.Drawing.Size(957, 27);
            this.toolAcciones.TabIndex = 3;
            this.toolAcciones.Text = "toolAcciones";
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(29, 24);
            this.btnSave.Text = "toolStripButton1";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // StatusBar
            // 
            this.StatusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LabelModificacies});
            this.StatusBar.Location = new System.Drawing.Point(0, 541);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(957, 22);
            this.StatusBar.TabIndex = 4;
            this.StatusBar.Text = "statusStrip1";
            // 
            // LabelModificacies
            // 
            this.LabelModificacies.Name = "LabelModificacies";
            this.LabelModificacies.Size = new System.Drawing.Size(0, 16);
            // 
            // FormGestion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 563);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.toolAcciones);
            this.Controls.Add(this.tabMusica);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormGestion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormGestion";
            this.Activated += new System.EventHandler(this.FormGestion_Activated);
            this.Load += new System.EventHandler(this.FormGestion_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMusicaCorrecta)).EndInit();
            this.tabMusica.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMusicaIncorrecta)).EndInit();
            this.toolAcciones.ResumeLayout(false);
            this.toolAcciones.PerformLayout();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPathFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gridMusicaCorrecta;
        private System.Windows.Forms.TabControl tabMusica;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnChangeFolder;
        private System.Windows.Forms.Label lblMusicaCorrecta;
        private System.Windows.Forms.Label lblMusicaIncorrecta;
        private System.Windows.Forms.ToolStrip toolAcciones;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.DataGridView gridMusicaIncorrecta;
        private System.Windows.Forms.StatusStrip StatusBar;
        private System.Windows.Forms.ToolStripStatusLabel LabelModificacies;
    }
}