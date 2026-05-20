namespace Interfaz
{
    partial class Menu
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menúToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BttnDistanciaSeguridadCiclo = new System.Windows.Forms.ToolStripMenuItem();
            this.BttnAñadirFlightPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.BttnSimulador = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportarFlightPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnConflicto = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menúToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";

            // 
            // menúToolStripMenuItem
            // 
            this.menúToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BttnDistanciaSeguridadCiclo,
            this.BttnAñadirFlightPlan,
            this.BttnSimulador,
            this.ImportarFlightPlan});
            this.menúToolStripMenuItem.Name = "menúToolStripMenuItem";
            this.menúToolStripMenuItem.Size = new System.Drawing.Size(60, 24);
            this.menúToolStripMenuItem.Text = "Menú";

            // 
            // BttnDistanciaSeguridadCiclo
            // 
            this.BttnDistanciaSeguridadCiclo.Name = "BttnDistanciaSeguridadCiclo";
            this.BttnDistanciaSeguridadCiclo.Size = new System.Drawing.Size(409, 26);
            this.BttnDistanciaSeguridadCiclo.Text = "Añadir distancia de seguridad y tiempo de ciclo";
            this.BttnDistanciaSeguridadCiclo.Click += new System.EventHandler(this.BttnDistanciaSeguridadCiclo_Click);
            // 
            // BttnAñadirFlightPlan
            // 
            this.BttnAñadirFlightPlan.Name = "BttnAñadirFlightPlan";
            this.BttnAñadirFlightPlan.Size = new System.Drawing.Size(409, 26);
            this.BttnAñadirFlightPlan.Text = "Añadir FlightPlan";
            this.BttnAñadirFlightPlan.Click += new System.EventHandler(this.BttnAñadirFlightPlan_Click);
            // 
            // BttnSimulador
            // 
            this.BttnSimulador.Name = "BttnSimulador";
            this.BttnSimulador.Size = new System.Drawing.Size(409, 26);
            this.BttnSimulador.Text = "Simulador";
            this.BttnSimulador.Click += new System.EventHandler(this.BttnSimulador_Click);
            // 
            // ImportarFlightPlan
            // 
            this.ImportarFlightPlan.Name = "ImportarFlightPlan";
            this.ImportarFlightPlan.Size = new System.Drawing.Size(409, 26);
            this.ImportarFlightPlan.Text = "Importar FlightPlan";
            this.ImportarFlightPlan.Click += new System.EventHandler(this.ImportarFlightPlan_Click);
            // 
            // BtnConflicto
            // 
            this.BtnConflicto.Font = new System.Drawing.Font("Comic Sans MS", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnConflicto.Location = new System.Drawing.Point(162, 203);
            this.BtnConflicto.Name = "BtnConflicto";
            this.BtnConflicto.Size = new System.Drawing.Size(412, 77);
            this.BtnConflicto.TabIndex = 5;
            this.BtnConflicto.Text = "¿Habrá Conflicto?";
            this.BtnConflicto.UseVisualStyleBackColor = true;
            this.BtnConflicto.Click += new System.EventHandler(this.BtnConflicto_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnConflicto);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Menu";
            this.Text = "Form1";
            this.TransparencyKey = System.Drawing.SystemColors.ActiveCaption;
            this.Load += new System.EventHandler(this.Menu_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menúToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BttnDistanciaSeguridadCiclo;
        private System.Windows.Forms.ToolStripMenuItem BttnAñadirFlightPlan;
        private System.Windows.Forms.ToolStripMenuItem BttnSimulador;
        private System.Windows.Forms.Button BtnConflicto;
        private System.Windows.Forms.ToolStripMenuItem ImportarFlightPlan;
    }
}

