namespace Interfaz
{
    partial class Importar__exportar_fichero
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
            this.ImportarFlightPlan = new System.Windows.Forms.Label();
            this.ExportarFlightPlan = new System.Windows.Forms.Label();
            this.ImportarFlightPlanTextBox = new System.Windows.Forms.TextBox();
            this.ExportarFlightPlanTextBox = new System.Windows.Forms.TextBox();
            this.bttnImportarFlightPlan = new System.Windows.Forms.Button();
            this.bttnExportarFlightPlan = new System.Windows.Forms.Button();
            this.BttnImportarOrdenador = new System.Windows.Forms.Button();
            this.BttnExportarOrdenador = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ImportarFlightPlan
            // 
            this.ImportarFlightPlan.AutoSize = true;
            this.ImportarFlightPlan.Location = new System.Drawing.Point(93, 75);
            this.ImportarFlightPlan.Name = "ImportarFlightPlan";
            this.ImportarFlightPlan.Size = new System.Drawing.Size(118, 16);
            this.ImportarFlightPlan.TabIndex = 0;
            this.ImportarFlightPlan.Text = "Importar FlightPlan";
            // 
            // ExportarFlightPlan
            // 
            this.ExportarFlightPlan.AutoSize = true;
            this.ExportarFlightPlan.Location = new System.Drawing.Point(348, 75);
            this.ExportarFlightPlan.Name = "ExportarFlightPlan";
            this.ExportarFlightPlan.Size = new System.Drawing.Size(118, 16);
            this.ExportarFlightPlan.TabIndex = 1;
            this.ExportarFlightPlan.Text = "Guardar FlightPlan";
            // 
            // ImportarFlightPlanTextBox
            // 
            this.ImportarFlightPlanTextBox.Location = new System.Drawing.Point(96, 105);
            this.ImportarFlightPlanTextBox.Name = "ImportarFlightPlanTextBox";
            this.ImportarFlightPlanTextBox.Size = new System.Drawing.Size(115, 22);
            this.ImportarFlightPlanTextBox.TabIndex = 2;

            // 
            // ExportarFlightPlanTextBox
            // 
            this.ExportarFlightPlanTextBox.Location = new System.Drawing.Point(350, 105);
            this.ExportarFlightPlanTextBox.Name = "ExportarFlightPlanTextBox";
            this.ExportarFlightPlanTextBox.Size = new System.Drawing.Size(116, 22);
            this.ExportarFlightPlanTextBox.TabIndex = 3;

            // 
            // bttnImportarFlightPlan
            // 
            this.bttnImportarFlightPlan.Location = new System.Drawing.Point(106, 138);
            this.bttnImportarFlightPlan.Name = "bttnImportarFlightPlan";
            this.bttnImportarFlightPlan.Size = new System.Drawing.Size(83, 41);
            this.bttnImportarFlightPlan.TabIndex = 4;
            this.bttnImportarFlightPlan.Text = "Aceptar";
            this.bttnImportarFlightPlan.UseVisualStyleBackColor = true;
            this.bttnImportarFlightPlan.Click += new System.EventHandler(this.bttnImportarFlightPlan_Click);
            // 
            // bttnExportarFlightPlan
            // 
            this.bttnExportarFlightPlan.Location = new System.Drawing.Point(365, 138);
            this.bttnExportarFlightPlan.Name = "bttnExportarFlightPlan";
            this.bttnExportarFlightPlan.Size = new System.Drawing.Size(83, 41);
            this.bttnExportarFlightPlan.TabIndex = 5;
            this.bttnExportarFlightPlan.Text = "Aceptar";
            this.bttnExportarFlightPlan.UseVisualStyleBackColor = true;
            this.bttnExportarFlightPlan.Click += new System.EventHandler(this.bttnExportarFlightPlan_Click_1);
            // 
            // BttnImportarOrdenador
            // 
            this.BttnImportarOrdenador.Location = new System.Drawing.Point(56, 219);
            this.BttnImportarOrdenador.Name = "BttnImportarOrdenador";
            this.BttnImportarOrdenador.Size = new System.Drawing.Size(190, 36);
            this.BttnImportarOrdenador.TabIndex = 7;
            this.BttnImportarOrdenador.Text = "Importar desde ordenador";
            this.BttnImportarOrdenador.UseVisualStyleBackColor = true;
            this.BttnImportarOrdenador.Click += new System.EventHandler(this.BttnImportarOrdenador_Click);
            // 
            // BttnExportarOrdenador
            // 
            this.BttnExportarOrdenador.Location = new System.Drawing.Point(315, 219);
            this.BttnExportarOrdenador.Name = "BttnExportarOrdenador";
            this.BttnExportarOrdenador.Size = new System.Drawing.Size(190, 36);
            this.BttnExportarOrdenador.TabIndex = 8;
            this.BttnExportarOrdenador.Text = "Exportar desde ordenador";
            this.BttnExportarOrdenador.UseVisualStyleBackColor = true;
            this.BttnExportarOrdenador.Click += new System.EventHandler(this.BttnExportarOrdenador_Click);
            // 
            // Importar__exportar_fichero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 315);
            this.Controls.Add(this.BttnExportarOrdenador);
            this.Controls.Add(this.BttnImportarOrdenador);
            this.Controls.Add(this.bttnExportarFlightPlan);
            this.Controls.Add(this.bttnImportarFlightPlan);
            this.Controls.Add(this.ExportarFlightPlanTextBox);
            this.Controls.Add(this.ImportarFlightPlanTextBox);
            this.Controls.Add(this.ExportarFlightPlan);
            this.Controls.Add(this.ImportarFlightPlan);
            this.Name = "Importar__exportar_fichero";
            this.Text = "Importar__exportar_fichero";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ImportarFlightPlan;
        private System.Windows.Forms.Label ExportarFlightPlan;
        private System.Windows.Forms.TextBox ImportarFlightPlanTextBox;
        private System.Windows.Forms.TextBox ExportarFlightPlanTextBox;
        private System.Windows.Forms.Button bttnImportarFlightPlan;
        private System.Windows.Forms.Button bttnExportarFlightPlan;
        private System.Windows.Forms.Button BttnImportarOrdenador;
        private System.Windows.Forms.Button BttnExportarOrdenador;
    }
}