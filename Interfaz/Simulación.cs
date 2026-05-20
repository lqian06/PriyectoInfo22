using FlightLib;
using Interfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Interfaz
{
    public partial class Simulación : Form // Formulario principal de la interfaz, donde se muestra la simulación de los vuelos
    {

        // Iniciar el formulario
        public Simulación()
        {
            InitializeComponent();
        }

        // Variables globales
        FlightPlanList ListaVuelos = new FlightPlanList();
        int radio = 10;
        int distSeguridad;
        int tCiclo;
        int segundos;
        Grid ventanaGrid;


        // Función para establecer los vuelos, la distancia de seguridad y el tiempo de ciclo del menú
        public void SetVuelos(FlightPlanList lista, int ds, int tc)
        {
            this.ListaVuelos=lista;
            this.distSeguridad = ds;
            this.tCiclo = tc;

        }


        // Función para dibujar los vuelos, las líneas de trayectoria y los círculos de seguridad
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            for (int i = 0; i < ListaVuelos.GetNum(); i++)
            {
                Position inicio = ListaVuelos.GetFlightPlan(i).GetInitialPosition();
                Position fin = ListaVuelos.GetFlightPlan(i).GetFinalPosition();
                Position actual = ListaVuelos.GetFlightPlan(i).GetCurrentPosition();

                //Línea
                using (Pen lapiz = new Pen(Color.Black, 2))
                {
                    e.Graphics.DrawLine(lapiz, (int)inicio.GetX(), (int)inicio.GetY(), (int)fin.GetX(), (int)fin.GetY());
                }


                //Círculo
                FlightPlanList distancias = new FlightPlanList();
                for (int j = 0; j < ListaVuelos.GetNum(); j++)
                {
                    if (ListaVuelos.GetFlightPlan(j) != ListaVuelos.GetFlightPlan(i))
                    {
                        distancias.AddFlightPlan(ListaVuelos.GetFlightPlan(j));
                    }
                }

                for (int k = 0; k < distancias.GetNum(); k++) // Para cada vuelo diferente al actual, se dibuja un círculo de seguridad alrededor del vuelo actual. Si la distancia entre el vuelo actual y el otro vuelo es mayor que el doble de la distancia de seguridad, el círculo se dibuja en naranja. Si la distancia es menor o igual al doble de la distancia de seguridad, el círculo se dibuja en rojo.
                {
                    if (ListaVuelos.GetFlightPlan(i).Distancia(distancias.GetFlightPlan(k)) > (distSeguridad))
                    {
                        Pen lapizSeguro = new Pen(Color.FromArgb(100, Color.Orange), 1);
                        e.Graphics.DrawEllipse(lapizSeguro, (int)actual.GetX() - distSeguridad/2, (int)actual.GetY() - distSeguridad / 2, distSeguridad, distSeguridad);
                        lapizSeguro.Dispose();
                    }
                    else
                    {
                        SolidBrush pintarcirculo = new SolidBrush(Color.Red);
                        e.Graphics.FillEllipse(pintarcirculo, (float)actual.GetX() - distSeguridad/2, (float)actual.GetY() - distSeguridad / 2, distSeguridad, distSeguridad);
                        pintarcirculo.Dispose();
                    }
                }

                // Avión
                string iconoAvion = "\u2708";
                using (Font fuenteAvion = new Font("Arial", 14, FontStyle.Bold))
                {
                    e.Graphics.DrawString(iconoAvion, fuenteAvion, Brushes.Black, (float)actual.GetX() - 11, (float)actual.GetY() - 11);
                }
            }
        }


        // Función para mostrar la distancia entre los vuelos al hacer click en el grid
        private void GridDatosVuelos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("La distancia entre los vuelos es: " + ListaVuelos.GetFlightPlan(0).Distancia(ListaVuelos.GetFlightPlan(1)), "metros.");
        }


        // Función para mostrar los detalles del vuelo al hacer click en el panel
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < ListaVuelos.GetNum(); i++)
            {
                Position actual = ListaVuelos.GetFlightPlan(i).GetCurrentPosition();
                Position inicio = ListaVuelos.GetFlightPlan(i).GetInitialPosition();
                Position destino = ListaVuelos.GetFlightPlan(i).GetFinalPosition();

                double dx = e.X - actual.GetX();
                double dy = e.Y - actual.GetY();
                double distanciaAlClic = Math.Sqrt((dx * dx) + (dy * dy));

                if (distanciaAlClic < (radio + 5))
                {
                    timer1.Stop();
                    string estadoVuelo = Convert.ToString(ListaVuelos.GetFlightPlan(i).HasArrived());
                    MessageBox.Show("Detalles del vuelo:\r\nID: " + ListaVuelos.GetFlightPlan(i).GetID() + "\r\nCompañía: " + ListaVuelos.GetFlightPlan(i).GetCompany() + "\r\nVelocidad: "
                        + ListaVuelos.GetFlightPlan(i).GetVelocidad() + "km/h\r\nPosición Actual: [" + actual.GetX() + "," + actual.GetY()
                        + "]\r\nOrigen: [" + inicio.GetX() + "," + inicio.GetY() + "]\r\nDestino: [" + destino.GetX() + "," + destino.GetY()
                        + "]\r\nEstado: " + estadoVuelo + "\r\nDistancia al destino: " + actual.Distancia(destino));
                }
            }
        }


        // Función para mover los vuelos cada segundo.
        private void timer1_Tick(object sender, EventArgs e)
        {
            segundos++;
            if (ListaVuelos.HabraConflictoLista(distSeguridad))
            {
                ChocaLabel.Text = "Choca";
            }
            else
            {
                ChocaLabel.Text = "No choca";
            }
            bool algunoSinLlegar= false;
            int i = 0;
            while (i< ListaVuelos.GetNum() && !algunoSinLlegar)
            {
                if (!ListaVuelos.GetFlightPlan(i).HasArrived())
                {
                    algunoSinLlegar = true;
                }
                i++;
            }

            if (algunoSinLlegar)
            {
                for (int j = 0; j < ListaVuelos.GetNum(); j++)
                {
                    if (!ListaVuelos.GetFlightPlan(j).HasArrived())
                        ListaVuelos.GetFlightPlan(j).Mover(tCiclo);
                }
                panel1.Invalidate();
                ActualizarGridExterno();
            }
            else
            {
                timer1.Stop();
            }
        }


        // Botón iniciar simulación
        private void BttnIniciar_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();
            btnParar.Visible = true;
            BttnIniciar.Visible = false;
        }


        // Botón para parar la simulación
        private void btnParar_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            btnParar.Visible = false;
            BttnIniciar.Visible = true;

        }


        //Dónde está este botón?????????
        private void button5_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            segundos = 0;

            for (int i = 0; i < ListaVuelos.GetNum(); i++)
            {
                FlightPlan vuelo = ListaVuelos.GetFlightPlan(i);
                Position inicio = vuelo.GetInitialPosition();

                vuelo.SetCurrentPosition(inicio.GetX(), inicio.GetY());

            }

            panel1.Invalidate();
            ActualizarGridExterno();
        }



        // Botón añadir 1 ciclo
        private void BttnAñadirUnCiclo_Click(object sender, EventArgs e)
        {
            timer1.Stop();

            for (int i = 0; i < ListaVuelos.GetNum(); i++)
            {
                ListaVuelos.GetFlightPlan(i).Mover(tCiclo);
            }
            panel1.Invalidate();
            ActualizarGridExterno();
        }

        //boton para retroceder 1 ciclo
        private void BttnRetroceder_Click(object sender, EventArgs e)
        {
            timer1.Stop();

            for (int i = 0; i < ListaVuelos.GetNum(); i++)
            {
                ListaVuelos.GetFlightPlan(i).Mover(-tCiclo);
            }
            panel1.Invalidate();

            ActualizarGridExterno();
        }


        // Función para mostrar los conflictos entre vuelos al cargar el formulario
        private void FormLinea_Load(object sender, EventArgs e)
        {
            if (ListaVuelos.GetNum() < 2) return;

            panel1.Invalidate(); // Para dibujar los vuelos al cargar el formulario

            //arreglar conflicto
            if (ListaVuelos.HabraConflictoLista(distSeguridad))
            {
                CambiarVelocidadPregunta formPregunta = new CambiarVelocidadPregunta();
                formPregunta.SetPlanes(ListaVuelos, distSeguridad);
                formPregunta.ShowDialog();

                if (formPregunta.GetConflictoResuelto())
                {
                    ListaVuelos = formPregunta.GetPlanes();
                    ChocaLabel.Text = "No choca";
                    panel1.Invalidate();
                    ActualizarGridExterno();
                }
            }
        }
        
        //abrir grid con información de los vuelos
        private void BtnInfoVuelos_Click(object sender, EventArgs e)
        {
            if (ventanaGrid == null || ventanaGrid.IsDisposed)
            {
                ventanaGrid = new Grid();
                ventanaGrid.Show();
            }
            ventanaGrid.CargarDatos(ListaVuelos);
        }

        //Actualizar el grid con la información de los vuelos cada vez que se mueven
        private void ActualizarGridExterno()
        {
            if (ventanaGrid != null && !ventanaGrid.IsDisposed)
            {
                ventanaGrid.ActualizarValores(ListaVuelos);
            }
        }

        //Botón de reiniciar simulación
        private void RestartSimBtn_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            segundos = 0;

            for (int i = 0; i < ListaVuelos.GetNum(); i++)
            {
                FlightPlan vuelo = ListaVuelos.GetFlightPlan(i);
                Position inicio = vuelo.GetInitialPosition();
                vuelo.SetCurrentPosition(inicio.GetX(), inicio.GetY());
            }
            panel1.Invalidate();
            ActualizarGridExterno();
        }


        // Botón para cambiar la velocidad de los vuelos en el momento
        private void CambiarVelBtn_Click(object sender, EventArgs e)
        {
            //Para que sea editable en la tabla cuando son más de 2 FlightPlans
            if (ListaVuelos.GetNum() > 2)
            {
                MessageBox.Show("Las velocidades se pueden modificar en la tabla de datos a la izquierda");
            }
            else
            {
                try
                {
                    ChangeVelocity formChanging = new ChangeVelocity();
                    formChanging.SetPlanes(ListaVuelos);
                    formChanging.ShowDialog();
                    ListaVuelos = formChanging.GetPlanes();

                    timer1.Stop();
                    segundos = 0;
                    MessageBox.Show("Velocidades cambiadas");

                    for (int i = 0; i < ListaVuelos.GetNum(); i++)
                    {
                        FlightPlan vuelo = ListaVuelos.GetFlightPlan(i);
                        vuelo.SetCurrentPosition(vuelo.GetInitialPosition().GetX(), vuelo.GetInitialPosition().GetY());
                    }
                    panel1.Invalidate();
                    ActualizarGridExterno();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Por favor, ingrese un número válido para la velocidad.");
                }
            }

        }

        private void BttnArchivoGuardar_Click_1(object sender, EventArgs e)
        {
            timer1.Stop();
            SaveFileDialog dialogo = new SaveFileDialog();
            dialogo.Filter = "Archivos de texto (*.txt)|*.txt";
            dialogo.Title = "Guardar progreso";
            dialogo.FileName = "progreso";

            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                ListaVuelos.GuardarEnArchivo(dialogo.FileName);
                MessageBox.Show("Progreso guardado correctamente.");
                Close();
            }
        }
    }
}

