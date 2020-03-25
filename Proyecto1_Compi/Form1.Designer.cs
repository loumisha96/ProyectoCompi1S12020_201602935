namespace Proyecto1_Compi
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.area = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aBRIRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analizadorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salidaDeTokensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salidaDeErroresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // area
            // 
            this.area.Location = new System.Drawing.Point(12, 37);
            this.area.Name = "area";
            this.area.Size = new System.Drawing.Size(514, 401);
            this.area.TabIndex = 0;
            this.area.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aBRIRToolStripMenuItem,
            this.analizadorToolStripMenuItem,
            this.salidaDeTokensToolStripMenuItem,
            this.salidaDeErroresToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aBRIRToolStripMenuItem
            // 
            this.aBRIRToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem1,
            this.guardarToolStripMenuItem});
            this.aBRIRToolStripMenuItem.Name = "aBRIRToolStripMenuItem";
            this.aBRIRToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.aBRIRToolStripMenuItem.Text = "Opciones";
            this.aBRIRToolStripMenuItem.Click += new System.EventHandler(this.ABRIRToolStripMenuItem_Click);
            // 
            // abrirToolStripMenuItem1
            // 
            this.abrirToolStripMenuItem1.Name = "abrirToolStripMenuItem1";
            this.abrirToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.abrirToolStripMenuItem1.Text = "Abrir";
            this.abrirToolStripMenuItem1.Click += new System.EventHandler(this.AbrirToolStripMenuItem1_Click);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.guardarToolStripMenuItem.Text = "Guardar";
            // 
            // analizadorToolStripMenuItem
            // 
            this.analizadorToolStripMenuItem.Name = "analizadorToolStripMenuItem";
            this.analizadorToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.analizadorToolStripMenuItem.Text = "Analizador";
            this.analizadorToolStripMenuItem.Click += new System.EventHandler(this.AnalizadorToolStripMenuItem_Click);
            // 
            // salidaDeTokensToolStripMenuItem
            // 
            this.salidaDeTokensToolStripMenuItem.Name = "salidaDeTokensToolStripMenuItem";
            this.salidaDeTokensToolStripMenuItem.Size = new System.Drawing.Size(105, 20);
            this.salidaDeTokensToolStripMenuItem.Text = "Salida de Tokens";
            this.salidaDeTokensToolStripMenuItem.Click += new System.EventHandler(this.SalidaDeTokensToolStripMenuItem_Click);
            // 
            // salidaDeErroresToolStripMenuItem
            // 
            this.salidaDeErroresToolStripMenuItem.Name = "salidaDeErroresToolStripMenuItem";
            this.salidaDeErroresToolStripMenuItem.Size = new System.Drawing.Size(105, 20);
            this.salidaDeErroresToolStripMenuItem.Text = "Salida de Errores";
            this.salidaDeErroresToolStripMenuItem.Click += new System.EventHandler(this.SalidaDeErroresToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.area);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox area;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aBRIRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analizadorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salidaDeTokensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salidaDeErroresToolStripMenuItem;
    }
}

