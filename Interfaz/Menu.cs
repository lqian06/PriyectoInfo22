using FlightLib;
using System;
using System.Windows.Forms;

namespace Interfaz
{
    public partial class Menu : Form // Formulario del menú principal
    {
        // lista gloval
        FlightPlanList planes = new FlightPlanList();

        // Datos del form3
        int distanciaSeguridad = 0;
        int tiempoCiclo = 0;


        // Iniciar el formulario
        public Menu() 
        {
            InitializeComponent();
        }


        // Cargar el menú
        private void Menu_Load(object sender, EventArgs e) { } 



        //Distancias de seguridad y tiempo de ciclo
        private void BttnDistanciaSeguridadCiclo_Click(object sender, EventArgs e)
        {
            DistanciaSeguridadCiclo form3 = new DistanciaSeguridadCiclo();

            form3.ShowDialog();

            this.distanciaSeguridad = form3.GetDistancia();
            this.tiempoCiclo = form3.GetTiempo();

            if (distanciaSeguridad > 0)
            {
                MessageBox.Show("Configuración guardada en el Menú.");
            }
        }


        //Añadir datos de vuelo de los FlightPlans
        private void BttnAñadirFlightPlan_Click(object sender, EventArgs e)
        {
            AñadirFlightPlan Form2 = new AñadirFlightPlan();
            Form2.ShowDialog();
            if (Form2.HayLista() == 0)
            {
                this.planes = Form2.GetLista();
                MessageBox.Show("Planes de vuelo cargados correctamente");
            }
        }

 
        //Botón para empezar simulación
        private void BttnSimulador_Click(object sender, EventArgs e)
        {
            if (planes.GetNum() < 2 )
            {
                MessageBox.Show("Error: No hay datos de vuelos.");
                return;
            }
            if (distanciaSeguridad == 0 || tiempoCiclo == 0)
            {
                MessageBox.Show("Error: Configuración de seguridad o tiempo de ciclo no válida.");
                return;
            }
            Simulación ventana = new Simulación();
            ventana.SetVuelos(planes, distanciaSeguridad, tiempoCiclo);
            ventana.Show();
        }


        // Botón para importar o exportar un fichero con los datos de los vuelos
        private void ImportarFlightPlan_Click(object sender, EventArgs e) 
        {

            Importar__exportar_fichero form4 = new Importar__exportar_fichero();
            form4.SetLista(planes);
            form4.ShowDialog();
            planes = form4.GetLista();
        }


        // Botón para mostrar si habrá un conflicto entre los vuelos
        private void BtnConflicto_Click(object sender, EventArgs e)
        {
            if (planes.GetNum() >= 2)
            {
                if (planes.HabraConflictoLista(distanciaSeguridad))
                {
                    MessageBox.Show("¡Alerta! Habrá un conflicto entre los vuelos.");
                }
                else
                {
                    MessageBox.Show("No habrá ningún conflicto entre los vuelos.");
                }
            }
            else
            {
                MessageBox.Show("Error: No hay datos de vuelos.");
            }
        }

    }
}