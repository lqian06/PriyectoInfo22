namespace Interfaz
{
    partial class CambiarVelocidadPregunta
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
            this.label1 = new System.Windows.Forms.Label();
            this.SiCambiarVelBtn = new System.Windows.Forms.Button();
            this.bttnNo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(707, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Quieres que se cambie la velocidad de alguno de los aviones automàticamente?";
            // 
            // SiCambiarVelBtn
            // 
            this.SiCambiarVelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SiCambiarVelBtn.Location = new System.Drawing.Point(212, 229);
            this.SiCambiarVelBtn.Name = "SiCambiarVelBtn";
            this.SiCambiarVelBtn.Size = new System.Drawing.Size(125, 49);
            this.SiCambiarVelBtn.TabIndex = 1;
            this.SiCambiarVelBtn.Text = "Sí";
            this.SiCambiarVelBtn.UseVisualStyleBackColor = true;
            this.SiCambiarVelBtn.Click += new System.EventHandler(this.SiCambiarVelBtn_Click);
            // 
            // bttnNo
            // 
            this.bttnNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnNo.Location = new System.Drawing.Point(454, 229);
            this.bttnNo.Name = "bttnNo";
            this.bttnNo.Size = new System.Drawing.Size(123, 49);
            this.bttnNo.TabIndex = 2;
            this.bttnNo.Text = "No";
            this.bttnNo.UseVisualStyleBackColor = true;
            this.bttnNo.Click += new System.EventHandler(this.bttnNo_Click);
            // 
            // CambiarVelocidadPregunta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.bttnNo);
            this.Controls.Add(this.SiCambiarVelBtn);
            this.Controls.Add(this.label1);
            this.Name = "CambiarVelocidadPregunta";
            this.Text = "CambiarVelocidadPregunta";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SiCambiarVelBtn;
        private System.Windows.Forms.Button bttnNo;
    }
}