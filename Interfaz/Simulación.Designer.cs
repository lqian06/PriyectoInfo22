namespace Interfaz
{
    partial class Simulación
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
            this.components = new System.ComponentModel.Container();
            this.BttnIniciar = new System.Windows.Forms.Button();
            this.BttnAñadirUnCiclo = new System.Windows.Forms.Button();
            this.btnParar = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnInfoVuelos = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.CambiarVelBtn = new System.Windows.Forms.Button();
            this.RestartSimBtn = new System.Windows.Forms.Button();
            this.BttnRetroceder = new System.Windows.Forms.Button();
            this.ChocaLabel = new System.Windows.Forms.Label();
            this.seguridad = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BttnArchivoGuardar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BttnIniciar
            // 
            this.BttnIniciar.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BttnIniciar.Location = new System.Drawing.Point(622, 486);
            this.BttnIniciar.Name = "BttnIniciar";
            this.BttnIniciar.Size = new System.Drawing.Size(140, 70);
            this.BttnIniciar.TabIndex = 0;
            this.BttnIniciar.Text = "▶︎";
            this.BttnIniciar.UseVisualStyleBackColor = true;
            this.BttnIniciar.Click += new System.EventHandler(this.BttnIniciar_Click);
            // 
            // BttnAñadirUnCiclo
            // 
            this.BttnAñadirUnCiclo.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BttnAñadirUnCiclo.Location = new System.Drawing.Point(768, 486);
            this.BttnAñadirUnCiclo.Name = "BttnAñadirUnCiclo";
            this.BttnAñadirUnCiclo.Size = new System.Drawing.Size(140, 70);
            this.BttnAñadirUnCiclo.TabIndex = 1;
            this.BttnAñadirUnCiclo.Text = "▷";
            this.BttnAñadirUnCiclo.UseVisualStyleBackColor = true;
            this.BttnAñadirUnCiclo.Click += new System.EventHandler(this.BttnAñadirUnCiclo_Click);
            // 
            // btnParar
            // 
            this.btnParar.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnParar.Location = new System.Drawing.Point(622, 486);
            this.btnParar.Name = "btnParar";
            this.btnParar.Size = new System.Drawing.Size(140, 70);
            this.btnParar.TabIndex = 2;
            this.btnParar.Text = "||";
            this.btnParar.UseVisualStyleBackColor = true;
            this.btnParar.Click += new System.EventHandler(this.btnParar_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1320, 453);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            // 
            // BtnInfoVuelos
            // 
            this.BtnInfoVuelos.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnInfoVuelos.Location = new System.Drawing.Point(12, 486);
            this.BtnInfoVuelos.Name = "BtnInfoVuelos";
            this.BtnInfoVuelos.Size = new System.Drawing.Size(140, 70);
            this.BtnInfoVuelos.TabIndex = 4;
            this.BtnInfoVuelos.Text = "ⓘ";
            this.BtnInfoVuelos.UseVisualStyleBackColor = true;
            this.BtnInfoVuelos.Click += new System.EventHandler(this.BtnInfoVuelos_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1228, 449);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(109, 70);
            this.button5.TabIndex = 6;
            this.button5.Text = "reiniciar simulación";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // CambiarVelBtn
            // 
            this.CambiarVelBtn.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CambiarVelBtn.Location = new System.Drawing.Point(1192, 486);
            this.CambiarVelBtn.Name = "CambiarVelBtn";
            this.CambiarVelBtn.Size = new System.Drawing.Size(140, 70);
            this.CambiarVelBtn.TabIndex = 6;
            this.CambiarVelBtn.Text = "⏲️";
            this.CambiarVelBtn.UseVisualStyleBackColor = true;
            this.CambiarVelBtn.Click += new System.EventHandler(this.CambiarVelBtn_Click);
            // 
            // RestartSimBtn
            // 
            this.RestartSimBtn.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RestartSimBtn.Location = new System.Drawing.Point(330, 486);
            this.RestartSimBtn.Name = "RestartSimBtn";
            this.RestartSimBtn.Size = new System.Drawing.Size(140, 70);
            this.RestartSimBtn.TabIndex = 9;
            this.RestartSimBtn.Text = "⟳";
            this.RestartSimBtn.UseVisualStyleBackColor = true;
            this.RestartSimBtn.Click += new System.EventHandler(this.RestartSimBtn_Click);
            // 
            // BttnRetroceder
            // 
            this.BttnRetroceder.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BttnRetroceder.Location = new System.Drawing.Point(476, 486);
            this.BttnRetroceder.Name = "BttnRetroceder";
            this.BttnRetroceder.Size = new System.Drawing.Size(140, 70);
            this.BttnRetroceder.TabIndex = 11;
            this.BttnRetroceder.Text = "◁";
            this.BttnRetroceder.UseVisualStyleBackColor = true;
            this.BttnRetroceder.Click += new System.EventHandler(this.BttnRetroceder_Click);
            // 
            // ChocaLabel
            // 
            this.ChocaLabel.AutoSize = true;
            this.ChocaLabel.Location = new System.Drawing.Point(984, 496);
            this.ChocaLabel.Name = "ChocaLabel";
            this.ChocaLabel.Size = new System.Drawing.Size(46, 16);
            this.ChocaLabel.TabIndex = 10;
            this.ChocaLabel.Text = "Choca";
            // 
            // seguridad
            // 
            this.seguridad.AutoSize = true;
            this.seguridad.Location = new System.Drawing.Point(984, 524);
            this.seguridad.Name = "seguridad";
            this.seguridad.Size = new System.Drawing.Size(51, 16);
            this.seguridad.TabIndex = 12;
            this.seguridad.Text = "Seguro";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1344, 28);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BttnArchivoGuardar});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // BttnArchivoGuardar
            // 
            this.BttnArchivoGuardar.Name = "BttnArchivoGuardar";
            this.BttnArchivoGuardar.Size = new System.Drawing.Size(224, 26);
            this.BttnArchivoGuardar.Text = "Guardar";
            this.BttnArchivoGuardar.Click += new System.EventHandler(this.BttnArchivoGuardar_Click_1);
            // 
            // Simulación
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 568);
            this.Controls.Add(this.seguridad);
            this.Controls.Add(this.ChocaLabel);
            this.Controls.Add(this.BttnRetroceder);
            this.Controls.Add(this.RestartSimBtn);
            this.Controls.Add(this.CambiarVelBtn);
            this.Controls.Add(this.BtnInfoVuelos);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BttnAñadirUnCiclo);
            this.Controls.Add(this.BttnIniciar);
            this.Controls.Add(this.btnParar);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Simulación";
            this.Text = "Form4";
            this.Load += new System.EventHandler(this.FormLinea_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BttnIniciar;
        private System.Windows.Forms.Button BttnAñadirUnCiclo;
        private System.Windows.Forms.Button btnParar;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnInfoVuelos;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button CambiarVelBtn;
        private System.Windows.Forms.Button RestartSimBtn;
        private System.Windows.Forms.Button BttnRetroceder;
        private System.Windows.Forms.Label ChocaLabel;
        private System.Windows.Forms.Label seguridad;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BttnArchivoGuardar;
    }
}