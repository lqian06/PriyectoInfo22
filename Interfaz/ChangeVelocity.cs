using FlightLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaz
{
    public partial class ChangeVelocity : Form 
    {
        FlightPlanList Lista;


        public ChangeVelocity() 
        {
            InitializeComponent();
        }


        // Se le asigna la lista de aviones a la simulación
        public void SetPlanes(FlightPlanList fpl) 
        {
            this.Lista = fpl;
        }


        // Se obtiene la lista de aviones del formulario
        public FlightPlanList GetPlanes() 
        {
            return Lista;
        }


        // Botón para cambiar la velocidad de los aviones
        private void ChangeVelBtn_Click(object sender, EventArgs e) 
        {
            try
            {
                Lista.GetFlightPlan(0).SetVelocidad(Convert.ToDouble(textBox1.Text));
                Lista.GetFlightPlan(1).SetVelocidad(Convert.ToDouble(textBox2.Text));
                Close();
            }
            catch
            {
                MessageBox.Show("Introduce un número válido");
            }
        }


        // Al cargar el formulario se muestran las velocidades actuales de los aviones para dos FlightPlans
        private void ChangeVelocity_Load(object sender, EventArgs e) 
        {
            textBox1.Text = Convert.ToString(Lista.GetFlightPlan(0).GetVelocidad());
            textBox2.Text = Convert.ToString(Lista.GetFlightPlan(1).GetVelocidad());
        }
    }
}
