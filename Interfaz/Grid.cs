using System;
using System.Windows.Forms;
using FlightLib;

namespace Interfaz
{
    public partial class Grid : Form
    {
        // ATRIBUTOS E INSTANCIAS
        private FlightPlanList listaVuelosInterna;

        // CONSTRUCTOR
        public Grid()
        {
            InitializeComponent();
        }

        // CONTROL GRID

        public void CargarDatos(FlightPlanList listaVuelos)
        {
            this.listaVuelosInterna = listaVuelos;

            GridDatosVuelos.ColumnCount = 7;
            GridDatosVuelos.RowCount = listaVuelos.GetNum() + 1; 
            GridDatosVuelos.ColumnHeadersVisible = false;
            GridDatosVuelos.RowHeadersVisible = false;

            ConfigurarEncabezados();

            // Rellenar datos de cada vuelo
            int i = 0;
            while (i < listaVuelos.GetNum())
            {
                FlightPlan v = listaVuelos.GetFlightPlan(i);
                PintarFilaVuelo(v, i + 1);
                i++;
            }
        }

        public void ActualizarValores(FlightPlanList listaVuelos)
        {
            this.listaVuelosInterna = listaVuelos;
            if (listaVuelos.GetNum() < 1) return;

            int i = 0;
            while (i < listaVuelos.GetNum())
            {
                FlightPlan v = listaVuelos.GetFlightPlan(i);
                PintarFilaVuelo(v, i + 1);
                i++;
            }
        }

        // EVENTOS DEL CONTROL GRID (INTERFAZ)

        private void GridDatosVuelos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (listaVuelosInterna != null && listaVuelosInterna.GetNum() == 2)
            {
                double distancia = listaVuelosInterna.GetFlightPlan(0).Distancia(listaVuelosInterna.GetFlightPlan(1));
                MessageBox.Show("La distancia entre los vuelos es: " + Math.Round(distancia, 2) + " metros.");
            }
        }

        private void GridDatosVuelos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (listaVuelosInterna.GetNum() > 2 && e.ColumnIndex == 6 && e.RowIndex > 0)
            {
                try
                {
                    int indiceVuelo = e.RowIndex - 1; 
                    double nuevaVelocidad = Convert.ToDouble(GridDatosVuelos[e.ColumnIndex, e.RowIndex].Value);

                    FlightPlan planModificado = listaVuelosInterna.GetFlightPlan(indiceVuelo);
                    planModificado.SetVelocidad(nuevaVelocidad);

                    MessageBox.Show($"Velocidad del vuelo {planModificado.GetID()} cambiada a {nuevaVelocidad} km/h correctamente.");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Introduce un número válido para la velocidad");
                }
            }
        }
        private void ConfigurarEncabezados()
        {
            GridDatosVuelos[0, 0].Value = "";
            GridDatosVuelos[1, 0].Value = "ID";
            GridDatosVuelos[2, 0].Value = "Company";
            GridDatosVuelos[3, 0].Value = "InitialPosition";
            GridDatosVuelos[4, 0].Value = "CurrentPosition";
            GridDatosVuelos[5, 0].Value = "FinalPosition";
            GridDatosVuelos[6, 0].Value = "Velocidad";
        }

        private void PintarFilaVuelo(FlightPlan v, int filaIndex)
        {
            GridDatosVuelos[0, filaIndex].Value = "Vuelo " + v.GetID();
            GridDatosVuelos[1, filaIndex].Value = v.GetID();
            GridDatosVuelos[2, filaIndex].Value = v.GetCompany();
            GridDatosVuelos[3, filaIndex].Value = $"{v.GetInitialPosition().GetX()},{v.GetInitialPosition().GetY()}";
            GridDatosVuelos[4, filaIndex].Value = $"{v.GetCurrentPosition().GetX()},{v.GetCurrentPosition().GetY()}";
            GridDatosVuelos[5, filaIndex].Value = $"{v.GetFinalPosition().GetX()},{v.GetFinalPosition().GetY()}";
            GridDatosVuelos[6, filaIndex].Value = v.GetVelocidad();
        }
    }
}