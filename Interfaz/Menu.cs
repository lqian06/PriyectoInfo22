using FlightLib;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Interfaz
{
    public partial class Menu : Form
    {
        // ==========================================
        // VARIABLES GLOBALES
        // ==========================================
        FlightPlanList planes = new FlightPlanList();
        int radio = 10;
        int distanciaSeguridad = 0;
        int tiempoCiclo = 0;
        int segundos = 0;
        Grid ventanaGrid;

        private void IniciarSimulacion()
        {
            timer1.Interval = 1000;
            timer1.Start();

            btnParar.Visible = true;
            BtnIniciar.Visible = false;
        }

        // Iniciar el formulario
        public Menu()
        {
            InitializeComponent();
        }

        // Cargar el menú
        private void Menu_Load(object sender, EventArgs e)
        {
            // Ocultamos el mapa de simulación
            panel1.Visible = false;

            // Ocultamos todos los controles inferiores de la simulación
            BtnInfoVuelos.Visible = false;
            RestartSimBtn.Visible = false;
            BttnRetroceder.Visible = false;
            BtnIniciar.Visible = false;
            btnParar.Visible = false;
            BttnAñadirUnCiclo.Visible = false;
            CambiarVelBtn.Visible = false;

            // Ocultamos las etiquetas de estado
            ChocaLabel.Visible = false;
            seguridad.Visible = false;
        }

        // ==========================================
        // BOTONES DEL MENÚ
        // ==========================================

        // Distancias de seguridad y tiempo de ciclo
        private void BttnDistanciaSeguridadCiclo_Click(object sender, EventArgs e)
        {
            DistanciaSeguridadCiclo form3 = new DistanciaSeguridadCiclo();
            form3.ShowDialog();

            this.distanciaSeguridad = form3.GetDistancia();
            this.tiempoCiclo = form3.GetTiempo();

            if (distanciaSeguridad > 0)
            {
                MessageBox.Show("Configuración guardada en el Menú.");

                // Redibujar el panel para que muestre los círculos de seguridad al instante
                panel1.Invalidate();
            }
        }

        // Añadir datos de vuelo de los FlightPlans
        private void BttnAñadirFlightPlan_Click(object sender, EventArgs e)
        {
            AñadirFlightPlan Form2 = new AñadirFlightPlan();
            Form2.ShowDialog();
            if (Form2.HayLista() == 0)
            {
                this.planes = Form2.GetLista();
                MessageBox.Show("Planes de vuelo cargados correctamente");

                // Forzar el repintado inmediato del mapa con los nuevos aviones añadidos
                panel1.Invalidate();

                // Si la tabla de datos externa ya está abierta, la actualizamos también con los nuevos aviones
                ActualizarGridExterno();
            }
        }

        // Botón para importar o exportar un fichero con los datos de los vuelos
        private void ImportarFlightPlan_Click(object sender, EventArgs e)
        {
            Importar__exportar_fichero form4 = new Importar__exportar_fichero();
            form4.SetLista(planes);
            form4.ShowDialog();
            planes = form4.GetLista();

            // Al cargar un archivo, mostramos los aviones cargados inmediatamente
            panel1.Invalidate();
            ActualizarGridExterno();
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

        // ==========================================
        // SIMULADOR
        // ==========================================

        // Función para dibujar la simulacion
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //La idea para dibujar a escala es de un trabajo de 2022

            // DIMENSIONES MÁXIMAS LÓGICAS FIJADAS A 1000 x 500
            float maxLogicoX = 1000f;
            float maxLogicoY = 500f;

            // Dimensiones reales en píxeles de tu panel en este instante
            float anchoPanel = panel1.Width;
            float altoPanel = panel1.Height;

            // Factores de escala (Píxeles reales por cada unidad del sistema lógico)
            float escalaX = anchoPanel / maxLogicoX;
            float escalaY = altoPanel / maxLogicoY;

            for (int i = 0; i < planes.GetNum(); i++)
            {
                FlightPlan vueloI = planes.GetFlightPlan(i);

                // --- COORDENADAS ORIGINALES ---
                Position inicio = vueloI.GetInitialPosition();
                Position fin = vueloI.GetFinalPosition();
                Position actual = vueloI.GetCurrentPosition();

                // --- COORDENADAS ESCALADAS AL PANEL (PÍXELES REALES) ---
                float xIni = (float)inicio.GetX() * escalaX;
                float yIni = (float)inicio.GetY() * escalaY;

                float xFin = (float)fin.GetX() * escalaX;
                float yFin = (float)fin.GetY() * escalaY;

                float xAct = (float)actual.GetX() * escalaX;
                float yAct = (float)actual.GetY() * escalaY;

                // Escalamos la distancia de seguridad utilizando el promedio de escalas para que no se deforme
                float distanciaSeguridadEscalada = distanciaSeguridad * ((escalaX + escalaY) / 2);

                // 1. Dibujar Línea de trayectoria adaptada
                using (Pen lapiz = new Pen(Color.Black, 2))
                {
                    e.Graphics.DrawLine(lapiz, xIni, yIni, xFin, yFin);
                }

                // 2. Lógica de conflicto
                bool enConflicto = false;
                if (distanciaSeguridad > 0)
                {
                    for (int j = 0; j < planes.GetNum(); j++)
                    {
                        if (i != j)
                        {
                            if (vueloI.Distancia(planes.GetFlightPlan(j)) <= distanciaSeguridad)
                            {
                                enConflicto = true;
                                break;
                            }
                        }
                    }
                }

                // 3. Dibujar Círculo de seguridad adaptado
                if (distanciaSeguridad > 0)
                {
                    float xCirculo = xAct - (distanciaSeguridadEscalada / 2);
                    float yCirculo = yAct - (distanciaSeguridadEscalada / 2);

                    if (enConflicto)
                    {
                        using (SolidBrush pintarcirculo = new SolidBrush(Color.FromArgb(80, Color.Red)))
                        {
                            e.Graphics.FillEllipse(pintarcirculo, xCirculo, yCirculo, distanciaSeguridadEscalada, distanciaSeguridadEscalada);
                        }
                    }
                    else
                    {
                        using (Pen lapizSeguro = new Pen(Color.Orange, 1))
                        {
                            e.Graphics.DrawEllipse(lapizSeguro, xCirculo, yCirculo, distanciaSeguridadEscalada, distanciaSeguridadEscalada);
                        }
                    }
                }

                // 4. Dibujar el icono del Avión centrado en la posición escalada
                string iconoAvion = "\u2708";
                using (Font fuenteAvion = new Font("Arial", 14, FontStyle.Bold))
                {
                    e.Graphics.DrawString(iconoAvion, fuenteAvion, Brushes.DarkBlue, xAct - 10, yAct - 12);
                }
            }
        }

        // Función para mover los vuelos en cada Tick del reloj
        private void timer1_Tick(object sender, EventArgs e)
        {
            segundos++;
            if (planes.HabraConflictoLista(distanciaSeguridad))
            {
                ChocaLabel.Text = "Choca";
            }
            else
            {
                ChocaLabel.Text = "No choca";
            }

            bool algunoSinLlegar = false;
            int i = 0;
            while (i < planes.GetNum() && !algunoSinLlegar)
            {
                if (!planes.GetFlightPlan(i).HasArrived())
                {
                    algunoSinLlegar = true;
                }
                i++;
            }

            if (algunoSinLlegar)
            {
                for (int j = 0; j < planes.GetNum(); j++)
                {
                    if (!planes.GetFlightPlan(j).HasArrived())
                        planes.GetFlightPlan(j).Mover(tiempoCiclo);
                }
                panel1.Invalidate();
                ActualizarGridExterno();
            }
            else
            {
                timer1.Stop();
            }
        }

        // Mostrar detalles del vuelo haciendo clic directamente sobre el mapa (Adaptado al espacio 1000x500)
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            float maxLogicoX = 1000f;
            float maxLogicoY = 500f;

            float escalaX = panel1.Width / maxLogicoX;
            float escalaY = panel1.Height / maxLogicoY;

            for (int i = 0; i < planes.GetNum(); i++)
            {
                Position actual = planes.GetFlightPlan(i).GetCurrentPosition();
                Position inicio = planes.GetFlightPlan(i).GetInitialPosition();
                Position destino = planes.GetFlightPlan(i).GetFinalPosition();

                double avionEnPantallaX = actual.GetX() * escalaX;
                double avionEnPantallaY = actual.GetY() * escalaY;

                double dx = e.X - avionEnPantallaX;
                double dy = e.Y - avionEnPantallaY;
                double distanciaAlClic = Math.Sqrt((dx * dx) + (dy * dy));

                if (distanciaAlClic < (radio + 5))
                {
                    timer1.Stop();
                    string estadoVuelo = Convert.ToString(planes.GetFlightPlan(i).HasArrived());
                    MessageBox.Show("Detalles del vuelo:\r\nID: " + planes.GetFlightPlan(i).GetID() + "\r\nCompañía: " + planes.GetFlightPlan(i).GetCompany() + "\r\nVelocidad: "
                        + planes.GetFlightPlan(i).GetVelocidad() + "km/h\r\nPosición Actual: [" + actual.GetX() + "," + actual.GetY()
                        + "]\r\nOrigen: [" + inicio.GetX() + "," + inicio.GetY() + "]\r\nDestino: [" + destino.GetX() + "," + destino.GetY()
                        + "]\r\nEstado: " + estadoVuelo + "\r\nDistancia al destino: " + actual.Distancia(destino));
                }
            }
        }

        // ==========================================
        // BOTONES DE REPRODUCCIÓN Y CONTROL DE TIEMPO
        // ==========================================

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            IniciarSimulacion();
        }

        private void btnParar_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            btnParar.Visible = false;
            BtnIniciar.Visible = true;
        }

        private void BttnAñadirUnCiclo_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            for (int i = 0; i < planes.GetNum(); i++)
            {
                planes.GetFlightPlan(i).Mover(tiempoCiclo);
            }
            panel1.Invalidate();
            ActualizarGridExterno();
        }

        private void BttnRetroceder_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            for (int i = 0; i < planes.GetNum(); i++)
            {
                planes.GetFlightPlan(i).Mover(-tiempoCiclo);
            }
            panel1.Invalidate();
            ActualizarGridExterno();
        }

        private void RestartSimBtn_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            segundos = 0;

            for (int i = 0; i < planes.GetNum(); i++)
            {
                FlightPlan vuelo = planes.GetFlightPlan(i);
                Position inicio = vuelo.GetInitialPosition();
                vuelo.SetCurrentPosition(inicio.GetX(), inicio.GetY());
            }
            panel1.Invalidate();
            ActualizarGridExterno();
        }

        // ==========================================
        // MANEJO DE LA TABLA DE DATOS EXTERNA
        // ==========================================

        private void BtnInfoVuelos_Click(object sender, EventArgs e)
        {
            if (ventanaGrid == null || ventanaGrid.IsDisposed)
            {
                ventanaGrid = new Grid();
                ventanaGrid.Show();
            }
            ventanaGrid.CargarDatos(planes);
        }

        private void ActualizarGridExterno()
        {
            if (ventanaGrid != null && !ventanaGrid.IsDisposed)
            {
                ventanaGrid.ActualizarValores(planes);
            }
        }

        private void GridDatosVuelos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (planes.GetNum() >= 2)
            {
                MessageBox.Show("La distancia entre los vuelos es: " + planes.GetFlightPlan(0).Distancia(planes.GetFlightPlan(1)), "metros.");
            }
        }

        private void CambiarVelBtn_Click(object sender, EventArgs e)
        {
            if (planes.GetNum() > 2)
            {
                MessageBox.Show("Las velocidades se pueden modificar en la tabla de datos a la izquierda");
            }
            else
            {
                try
                {
                    ChangeVelocity formChanging = new ChangeVelocity();
                    formChanging.SetPlanes(planes);
                    formChanging.ShowDialog();
                    planes = formChanging.GetPlanes();

                    timer1.Stop();
                    segundos = 0;
                    MessageBox.Show("Velocidades cambiadas");

                    for (int i = 0; i < planes.GetNum(); i++)
                    {
                        FlightPlan vuelo = planes.GetFlightPlan(i);
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
                planes.GuardarEnArchivo(dialogo.FileName);
                MessageBox.Show("Progreso guardado correctamente.");
                Close();
            }
        }

        // Boton para iniciar/apagar la simulación
        private void BotonEncendidoApagado_Click(object sender, EventArgs e)
        {
            if (!panel1.Visible)
            {
                panel1.Visible = true;

                BtnInfoVuelos.Visible = true;
                RestartSimBtn.Visible = true;
                BttnRetroceder.Visible = true;
                BtnIniciar.Visible = true;
                btnParar.Visible = false;
                BttnAñadirUnCiclo.Visible = true;
                CambiarVelBtn.Visible = true;

                ChocaLabel.Visible = true;
                seguridad.Visible = true;

                BotonEncendidoApagado.BackColor = Color.LightGreen;
                panel1.Invalidate();
            }
            else
            {
                timer1.Stop();
                Menu_Load(sender, e);
                BotonEncendidoApagado.BackColor = SystemColors.Control;
            }
        }
    }
}