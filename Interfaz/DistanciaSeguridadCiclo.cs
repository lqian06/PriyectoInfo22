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
    public partial class DistanciaSeguridadCiclo : Form
    {
        //Iniciar el formulario
        int tiempoCiclo;
        double distanciaSeguridad;

        public DistanciaSeguridadCiclo()
        {
            InitializeComponent();
        }


        //Gets
        public int GetDistancia()
        {
            if (string.IsNullOrEmpty(TextDistanciaSeguridad.Text)) return 0;
            try { return Convert.ToInt32(TextDistanciaSeguridad.Text); }
            catch { return 0; }
        }

        public int GetTiempo() //Devuelve el tiempo de ciclo introducido por el usuario, o 0 si no se ha introducido nada o si el formato es incorrecto
        {
            if (string.IsNullOrEmpty(TextTiempoCiclo.Text)) return 0;
            try { return Convert.ToInt32(TextTiempoCiclo.Text); }
            catch { return 0; }
        }

        //Sets
        public void SetDistancia(double distancia)
        {
            distanciaSeguridad = distancia;
        }

        public void SetTiempo(int tiempo) 
        {
            tiempoCiclo = tiempo;
        }


        //Botón del tiempo de ciclo
        private bool EstructuraCorrecta = false;
        private void TextTiempoCiclo_TextChanged(object sender, EventArgs e)
        {
            if (EstructuraCorrecta)
            {
                return;
            }
            int res;
            try
            {
                res = Convert.ToInt32(TextTiempoCiclo.Text);
                return;
            }
            catch (FormatException)
            {
                EstructuraCorrecta = true;
                MessageBox.Show("Escriba un número entero en el tiempo de ciclo");
                TextTiempoCiclo.Text = "";
                EstructuraCorrecta = false;
                return;
            }

        }


        //Botón de la distancia de seguridad
        private void TextDistanciaSeguridad_TextChanged(object sender, EventArgs e)
        {
            if (EstructuraCorrecta)
            {
                return;
            }

            int res;
            try
            {
                res = Convert.ToInt32(TextDistanciaSeguridad.Text);
                return;
            }
            catch (FormatException)
            {
                EstructuraCorrecta = true;
                MessageBox.Show("Escriba un número entero para la distancia de seguridad");
                TextDistanciaSeguridad.Text = "";
                EstructuraCorrecta = false;
                return;
            }
        }


        //Botón de autorrellenar
        private void btnAutorellenar_Click(object sender, EventArgs e)
        {
            TextDistanciaSeguridad.Text = "20";
            TextTiempoCiclo.Text = "10";
        }


        // Botón de guardado
        private void bttnGuardar_Click(object sender, EventArgs e) 
        {
            try
            {
                tiempoCiclo = Convert.ToInt32(TextTiempoCiclo.Text);
                distanciaSeguridad = Convert.ToDouble(TextDistanciaSeguridad.Text);
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al convertir los valores. Asegúrese de poner números válidos.");
            }            
        }
    }
}
