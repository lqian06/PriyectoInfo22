using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightLib;

namespace Interfaz
{
    public partial class Grid : Form // Formulario para mostrar los datos de los vuelos
    {
        FlightPlanList ListaVuelosInterna;
        public Grid() // Constructor del formulario
        {
            InitializeComponent();
        }
        private void GridDatosVuelos_CellClick(object sender, DataGridViewCellEventArgs e) // Evento al hacer clic en una celda
        {
            // Verificamos que la lista no sea nula y tenga al menos 2 vuelos
            if (ListaVuelosInterna.GetNum() == 2)
            {
                double distancia = ListaVuelosInterna.GetFlightPlan(0).Distancia(ListaVuelosInterna.GetFlightPlan(1));
                MessageBox.Show("La distancia entre los vuelos es: " + Math.Round(distancia, 2) + " metros.");
            }
        }

        public void CargarDatos(FlightPlanList ListaVuelos) // Método para cargar los datos de los vuelos en el grid
        {
            this.ListaVuelosInterna = ListaVuelos;
            GridDatosVuelos.ColumnCount = 7;
            GridDatosVuelos.RowCount = ListaVuelos.GetNum() + 1; // +1 para los encabezados
            GridDatosVuelos.ColumnHeadersVisible = false;
            GridDatosVuelos.RowHeadersVisible = false;

            // Encabezados
            GridDatosVuelos[0, 0].Value = "";
            GridDatosVuelos[1, 0].Value = "ID";
            GridDatosVuelos[2, 0].Value = "Company";
            GridDatosVuelos[3, 0].Value = "InitialPosition";
            GridDatosVuelos[4, 0].Value = "CurrentPosition";
            GridDatosVuelos[5, 0].Value = "FinalPosition";
            GridDatosVuelos[6, 0].Value = "Velocidad";

            // Datos de cada vuelo
            int i = 0;
            while (i < ListaVuelos.GetNum())
            {
                var v = ListaVuelos.GetFlightPlan(i);
                GridDatosVuelos[0, i + 1].Value = "Vuelo " + v.GetID();
                GridDatosVuelos[1, i + 1].Value = v.GetID();
                GridDatosVuelos[2, i + 1].Value = v.GetCompany();
                GridDatosVuelos[3, i + 1].Value = v.GetInitialPosition().GetX() + "," + v.GetInitialPosition().GetY();
                GridDatosVuelos[4, i + 1].Value = v.GetCurrentPosition().GetX() + "," + v.GetCurrentPosition().GetY();
                GridDatosVuelos[5, i + 1].Value = v.GetFinalPosition().GetX() + "," + v.GetFinalPosition().GetY();
                GridDatosVuelos[6, i + 1].Value = v.GetVelocidad();
                i++;
            }
        }

        public void ActualizarValores(FlightPlanList ListaVuelos) // Método para actualizar los valores de los vuelos en el grid
        {
            this.ListaVuelosInterna = ListaVuelos;
            if (ListaVuelos.GetNum() < 1) return;

            int i = 0;
            while (i < ListaVuelos.GetNum())
            {
                var v = ListaVuelos.GetFlightPlan(i);
                GridDatosVuelos[1, i + 1].Value = v.GetID();
                GridDatosVuelos[2, i + 1].Value = v.GetCompany();
                GridDatosVuelos[3, i + 1].Value = v.GetInitialPosition().GetX() + "," + v.GetInitialPosition().GetY();
                GridDatosVuelos[4, i + 1].Value = v.GetCurrentPosition().GetX() + "," + v.GetCurrentPosition().GetY();
                GridDatosVuelos[5, i + 1].Value = v.GetFinalPosition().GetX() + "," + v.GetFinalPosition().GetY();
                GridDatosVuelos[6, i + 1].Value = v.GetVelocidad();
                i++;
            }
        }

     

        private void GridDatosVuelos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (ListaVuelosInterna.GetNum() > 2)
            {
                if (e.ColumnIndex == 6 && e.RowIndex > 0)
                {
                    try
                    {
                        int indicevuelo = e.RowIndex - 1; // -1 por el encabezado
                        double nuevaVelocidad = Convert.ToDouble(GridDatosVuelos[e.ColumnIndex, e.RowIndex].Value);
                        ListaVuelosInterna.GetFlightPlan(indicevuelo).SetVelocidad(nuevaVelocidad);
                        MessageBox.Show("Velocidad del vuelo " + ListaVuelosInterna.GetFlightPlan(indicevuelo).GetID() + " cambiada a " + nuevaVelocidad + " km/h correctamente.");
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Introduce un número válido para la velocidad");
                    }
                }
            }
        }
    }
}
