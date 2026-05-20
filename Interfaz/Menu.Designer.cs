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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menúToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BttnDistanciaSeguridadCiclo = new System.Windows.Forms.ToolStripMenuItem();
            this.BttnAñadirFlightPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportarFlightPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnConflicto = new System.Windows.Forms.Button();
            this.BttnRetroceder = new System.Windows.Forms.Button();
            this.RestartSimBtn = new System.Windows.Forms.Button();
            this.CambiarVelBtn = new System.Windows.Forms.Button();
            this.BtnInfoVuelos = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnIniciar = new System.Windows.Forms.Button();
            this.BttnAñadirUnCiclo = new System.Windows.Forms.Button();
            this.btnParar = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ChocaLabel = new System.Windows.Forms.Label();
            this.seguridad = new System.Windows.Forms.Label();
            this.BotonEncendidoApagado = new System.Windows.Forms.Button();
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
            this.menuStrip1.Size = new System.Drawing.Size(1309, 28);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menúToolStripMenuItem
            // 
            this.menúToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BttnDistanciaSeguridadCiclo,
            this.BttnAñadirFlightPlan,
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
            this.BtnConflicto.Location = new System.Drawing.Point(35, 68);
            this.BtnConflicto.Name = "BtnConflicto";
            this.BtnConflicto.Size = new System.Drawing.Size(210, 131);
            this.BtnConflicto.TabIndex = 5;
            this.BtnConflicto.Text = "¿Habrá Conflicto?";
            this.BtnConflicto.UseVisualStyleBackColor = true;
            this.BtnConflicto.Click += new System.EventHandler(this.BtnConflicto_Click);
            // 
            // BttnRetroceder
            // 
            this.BttnRetroceder.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BttnRetroceder.Location = new System.Drawing.Point(603, 552);
            this.BttnRetroceder.Name = "BttnRetroceder";
            this.BttnRetroceder.Size = new System.Drawing.Size(140, 70);
            this.BttnRetroceder.TabIndex = 21;
            this.BttnRetroceder.Text = "◁";
            this.BttnRetroceder.UseVisualStyleBackColor = true;
            this.BttnRetroceder.Click += new System.EventHandler(this.BttnRetroceder_Click);
            // 
            // RestartSimBtn
            // 
            this.RestartSimBtn.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RestartSimBtn.Location = new System.Drawing.Point(457, 552);
            this.RestartSimBtn.Name = "RestartSimBtn";
            this.RestartSimBtn.Size = new System.Drawing.Size(140, 70);
            this.RestartSimBtn.TabIndex = 19;
            this.RestartSimBtn.Text = "⟳";
            this.RestartSimBtn.UseVisualStyleBackColor = true;
            this.RestartSimBtn.Click += new System.EventHandler(this.RestartSimBtn_Click);
            // 
            // CambiarVelBtn
            // 
            this.CambiarVelBtn.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CambiarVelBtn.Location = new System.Drawing.Point(1103, 550);
            this.CambiarVelBtn.Name = "CambiarVelBtn";
            this.CambiarVelBtn.Size = new System.Drawing.Size(140, 70);
            this.CambiarVelBtn.TabIndex = 18;
            this.CambiarVelBtn.Text = "⏲️";
            this.CambiarVelBtn.UseVisualStyleBackColor = true;
            this.CambiarVelBtn.Click += new System.EventHandler(this.CambiarVelBtn_Click);
            // 
            // BtnInfoVuelos
            // 
            this.BtnInfoVuelos.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnInfoVuelos.Location = new System.Drawing.Point(311, 552);
            this.BtnInfoVuelos.Name = "BtnInfoVuelos";
            this.BtnInfoVuelos.Size = new System.Drawing.Size(140, 70);
            this.BtnInfoVuelos.TabIndex = 17;
            this.BtnInfoVuelos.Text = "ⓘ";
            this.BtnInfoVuelos.UseVisualStyleBackColor = true;
            this.BtnInfoVuelos.Click += new System.EventHandler(this.BtnInfoVuelos_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(269, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 500);
            this.panel1.TabIndex = 16;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // BtnIniciar
            // 
            this.BtnIniciar.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnIniciar.Location = new System.Drawing.Point(749, 550);
            this.BtnIniciar.Name = "BtnIniciar";
            this.BtnIniciar.Size = new System.Drawing.Size(140, 70);
            this.BtnIniciar.TabIndex = 13;
            this.BtnIniciar.Text = "▶︎";
            this.BtnIniciar.UseVisualStyleBackColor = true;
            this.BtnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // BttnAñadirUnCiclo
            // 
            this.BttnAñadirUnCiclo.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BttnAñadirUnCiclo.Location = new System.Drawing.Point(895, 552);
            this.BttnAñadirUnCiclo.Name = "BttnAñadirUnCiclo";
            this.BttnAñadirUnCiclo.Size = new System.Drawing.Size(140, 70);
            this.BttnAñadirUnCiclo.TabIndex = 14;
            this.BttnAñadirUnCiclo.Text = "▷";
            this.BttnAñadirUnCiclo.UseVisualStyleBackColor = true;
            this.BttnAñadirUnCiclo.Click += new System.EventHandler(this.BttnAñadirUnCiclo_Click);
            // 
            // btnParar
            // 
            this.btnParar.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnParar.Location = new System.Drawing.Point(749, 552);
            this.btnParar.Name = "btnParar";
            this.btnParar.Size = new System.Drawing.Size(140, 70);
            this.btnParar.TabIndex = 15;
            this.btnParar.Text = "||";
            this.btnParar.UseVisualStyleBackColor = true;
            this.btnParar.Click += new System.EventHandler(this.btnParar_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ChocaLabel
            // 
            this.ChocaLabel.AutoSize = true;
            this.ChocaLabel.Location = new System.Drawing.Point(1046, 574);
            this.ChocaLabel.Name = "ChocaLabel";
            this.ChocaLabel.Size = new System.Drawing.Size(46, 16);
            this.ChocaLabel.TabIndex = 20;
            this.ChocaLabel.Text = "Choca";
            // 
            // seguridad
            // 
            this.seguridad.AutoSize = true;
            this.seguridad.Location = new System.Drawing.Point(1046, 590);
            this.seguridad.Name = "seguridad";
            this.seguridad.Size = new System.Drawing.Size(51, 16);
            this.seguridad.TabIndex = 22;
            this.seguridad.Text = "Seguro";
            // 
            // BotonEncendidoApagado
            // 
            this.BotonEncendidoApagado.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BotonEncendidoApagado.Location = new System.Drawing.Point(35, 500);
            this.BotonEncendidoApagado.Name = "BotonEncendidoApagado";
            this.BotonEncendidoApagado.Size = new System.Drawing.Size(140, 70);
            this.BotonEncendidoApagado.TabIndex = 23;
            this.BotonEncendidoApagado.Text = "⏻";
            this.BotonEncendidoApagado.UseVisualStyleBackColor = true;
            this.BotonEncendidoApagado.Click += new System.EventHandler(this.BotonEncendidoApagado_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1309, 632);
            this.Controls.Add(this.BtnIniciar);
            this.Controls.Add(this.BotonEncendidoApagado);
            this.Controls.Add(this.CambiarVelBtn);
            this.Controls.Add(this.seguridad);
            this.Controls.Add(this.ChocaLabel);
            this.Controls.Add(this.BttnRetroceder);
            this.Controls.Add(this.RestartSimBtn);
            this.Controls.Add(this.BtnInfoVuelos);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BttnAñadirUnCiclo);
            this.Controls.Add(this.btnParar);
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
        private System.Windows.Forms.Button BtnConflicto;
        private System.Windows.Forms.Button BttnRetroceder;
        private System.Windows.Forms.Button RestartSimBtn;
        private System.Windows.Forms.Button CambiarVelBtn;
        private System.Windows.Forms.Button BtnInfoVuelos;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BttnAñadirUnCiclo;
        private System.Windows.Forms.Button BtnIniciar;
        private System.Windows.Forms.Button btnParar;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem ImportarFlightPlan;
        private System.Windows.Forms.Label ChocaLabel;
        private System.Windows.Forms.Label seguridad;
        private System.Windows.Forms.Button BotonEncendidoApagado;
    }
}

