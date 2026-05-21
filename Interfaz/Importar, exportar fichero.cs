using FlightLib;
using Interfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaz
{
    public partial class Importar__exportar_fichero : Form //Form para importar y exportar los datos de los planes de vuelo
    {
        public Importar__exportar_fichero() //Iniciar el formulario
        {
            InitializeComponent();
        }


        //GetLista de FormLinea y así importamos los datos de los planes de vuelo
        FlightPlanList listaImportada = null;

        public FlightPlanList GetLista()
        {
            return listaImportada;
        }   

        //Comprueba que se han cargado los datos
        private void bttnImportarFlightPlan_Click(object sender, EventArgs e)
        {
            try
            {
                FlightPlanList nueva = new FlightPlanList();
                nueva = nueva.CargarDesdeArchivo(ImportarFlightPlanTextBox.Text);

                if (nueva!=null)
                {
                    listaImportada = nueva;
                    MessageBox.Show("Archivo cargado correctamente");
                }
                else
                {
                    MessageBox.Show("No se ha podido cargar el archivo");

                }
                Close();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("No hay ningún archivo encontrado");
            }
            catch (FormatException)
            {
                MessageBox.Show("El formato del archivo no es correcto");
            }

        }

        //Cogemos los datos del menú de los FlightPlans
        FlightPlanList lista;
        int tiempoCiclo;
        int distanciaSeguridad;
        public void SetLista(FlightPlanList lista, int tiempoCiclo, int distanciaSeguridad)
        {
            this.lista = lista;
            this.tiempoCiclo = tiempoCiclo;
            this.distanciaSeguridad = distanciaSeguridad;
        }

        private void bttnExportarFlightPlan_Click_1(object sender, EventArgs e)
        {
            try
            {
                FlightPlan v1 = lista.GetFlightPlan(0);
                FlightPlan v2 = lista.GetFlightPlan(1);

                if (v1 == null || v2 == null)
                {
                    MessageBox.Show("Parámetros no definidos, seleccione en el menú para rellenar los campos");
                }
                else
                {
                    // Pasamos las variables guardadas
                    lista.GuardarEnArchivo(ExportarFlightPlanTextBox.Text, tiempoCiclo, distanciaSeguridad);
                    MessageBox.Show("Archivo guardado correctamente.");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("El formato del archivo no es correcto");
            }            
        }
        



        //Abre tus archivos para importar un plan de vuelo
        private void BttnImportarOrdenador_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogo = new OpenFileDialog();
            dialogo.Filter = "Archivos de texto (*.txt)|*.txt";
            dialogo.Title = "Importar planes de vuelo";

            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FlightPlanList nueva = new FlightPlanList();
                    nueva = nueva.CargarDesdeArchivo(dialogo.FileName);
                    if (nueva != null)
                    {
                        listaImportada = nueva;
                        MessageBox.Show("Archivo cargado correctamente");
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("No se ha podido cargar el archivo");
                    }
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("No hay ningún archivo encontrado");
                }
                catch (FormatException)
                {
                    MessageBox.Show("El formato del archivo no es correcto");
                }
            }
        }


        //Abre tus archivos para exportar un plan de vuelo
        private void BttnExportarOrdenador_Click(object sender, EventArgs e)
        {
            if (lista.GetFlightPlan(0) == null || lista.GetFlightPlan(1) == null)
            {
                MessageBox.Show("Parámetros no definidos, seleccione en el menú para rellenar los campos");
                return;
            }

            SaveFileDialog dialogo = new SaveFileDialog();
            dialogo.Filter = "Archivos de texto (*.txt)|*.txt";
            dialogo.Title = "Exportar planes de vuelo";
            dialogo.FileName = "flightplans";

            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Pasamos las variables guardadas
                    lista.GuardarEnArchivo(dialogo.FileName, tiempoCiclo, distanciaSeguridad);
                    MessageBox.Show("Archivo guardado correctamente.");
                }
                catch (FormatException)
                {
                    MessageBox.Show("El formato del archivo no es correcto");
                }
            }
        }

        private void BtnExportarCambios_Click(object sender, EventArgs e)
        {
            //fichero cambios
            SaveFileDialog dialogo = new SaveFileDialog();
            dialogo.Filter = "Archivos de texto (*.txt)|*.txt";
            dialogo.Title = "Generar Informe de Cambios de Velocidad";
            dialogo.FileName = "InformeCambios.txt";

            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 2. Usamos la BBDD para generar el archivo
                    BBDD db = new BBDD();
                    db.Iniciar();

                    // Pasamos la ruta seleccionada y la lista de aviones que gestiona el formulario
                    db.GenerarInformeCambios(dialogo.FileName, this.lista);

                    db.Cerrar();
                    MessageBox.Show("Informe generado con éxito con los contactos de las aerolíneas.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar el informe: " + ex.Message);
                }
            }

        }
    }
}

