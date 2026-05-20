using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightLib;

namespace Interfaz
{
    public partial class AñadirFlightPlan : Form //Form para añadir planes de vuelo
    {

        FlightPlanList lista = new FlightPlanList();

        FlightPlan plan_a;
        FlightPlan plan_b;


        //Inicia el formulario
        public AñadirFlightPlan() 
        {
            InitializeComponent();
        }


        //Determina si hay una lista de planes de vuelo creada
        public int HayLista()  
        {
            if (lista != null && lista.GetNum() > 0)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }


        //Gets
        public FlightPlanList GetLista()
        {
            return lista;
        }


        //Botón para guardar los planes de vuelo
        private void bttnGuardar_Click(object sender, EventArgs e)
        {
            try
            {  
                string ida = textBox1.Text;
                int velocidada = Convert.ToInt32(textBox2.Text);
                string companya= CompanyATextBox.Text;
                int xia = Convert.ToInt32(textBox3.Text);
                int yia = Convert.ToInt32(textBox4.Text);
                int xfa = Convert.ToInt32(textBox5.Text);
                int yfa = Convert.ToInt32(textBox6.Text);

                string idb = textBox7.Text;
                string companyb = CompanyBTextBox.Text;
                int velocidadb = Convert.ToInt32(textBox8.Text);
                int xib = Convert.ToInt32(textBox9.Text);
                int yib = Convert.ToInt32(textBox10.Text);
                int xfb = Convert.ToInt32(textBox11.Text);
                int yfb = Convert.ToInt32(textBox12.Text);

                this.plan_a = new FlightPlan(ida, companya, xia, yia, xfa, yfa, velocidada);
                this.plan_b = new FlightPlan(idb, companyb, xib, yib, xfb, yfb, velocidadb);
                int añadida = lista.AddFlightPlan(plan_a);
                int añadidb = lista.AddFlightPlan(plan_b);

                if (añadida == 0 && añadidb == 0)
                {
                    MessageBox.Show("Se ha creado los planes de vuelo correctamente.");
                }
                else
                {
                    MessageBox.Show("Ha habido un problema al guardar algun flight plan.");
                }

                Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Error de formato");
            }
        }

        //Botón de autorrellenar
        private void btnAutorrellenar_Click(object sender, EventArgs e)
        {
            textBox1.Text = "PlanA";
            CompanyATextBox.Text = "CompanyA";
            textBox2.Text = "100";
            textBox3.Text = "50";
            textBox4.Text = "50";
            textBox5.Text = "500";
            textBox6.Text = "200";

            textBox7.Text = "PlanB";
            CompanyBTextBox.Text = "CompanyB";
            textBox8.Text = "100";
            textBox9.Text = "500";
            textBox10.Text = "50";
            textBox11.Text = "50";
            textBox12.Text = "200";
        }

    }
}