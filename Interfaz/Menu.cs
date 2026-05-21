using FlightLib;
using System;
using System.Collections.Generic;
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
            // 1. Verificamos si habrá algún conflicto antes de encender el reloj
            if (planes.HabraConflictoLista(distanciaSeguridad))
            {
                DialogResult respuesta = MessageBox.Show(
                    "¡Se ha detectado un conflicto potencial entre los vuelos de la lista!\n" +
                    "¿Desea que el sistema intente ajustar las velocidades automáticamente?",
                    "Conflicto Detectado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (respuesta == DialogResult.Yes)
                {
                    // Intentamos resolverlo estableciendo una velocidad mínima permitida de 100 km/h
                    int velocidadMinimaPermitida = 10;
                    bool resuelto = planes.ResolverConflicto(distanciaSeguridad, velocidadMinimaPermitida);

                    if (resuelto)
                    {
                        MessageBox.Show(
                            "El conflicto ha sido resuelto con éxito recalculando las velocidades corporativas.",
                            "Solución Aplicada",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                        // Refrescamos los componentes visuales para que muestren los nuevos datos de velocidad
                        ActualizarGridExterno();
                        panel1.Invalidate();
                    }
                    else
                    {
                        MessageBox.Show(
                            "No ha sido posible resolver el conflicto de forma segura reduciendo las velocidades hasta los límites mínimos permitidos. Procediendo con precaución.",
                            "Solución Fallida",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }

            // 2. Iniciamos el temporizador de la simulación de forma habitual
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
            panel1.Visible = false;

            BtnInfoVuelos.Visible = false;
            RestartSimBtn.Visible = false;
            BttnRetroceder.Visible = false;
            BtnIniciar.Visible = false;
            btnParar.Visible = false;
            BttnAñadirUnCiclo.Visible = false;
            CambiarVelBtn.Visible = false;

            ChocaLabel.Visible = false;
            seguridad.Visible = false;
        }

        // ==========================================
        // BOTONES DEL MENÚ
        // ==========================================

        private void BttnDistanciaSeguridadCiclo_Click(object sender, EventArgs e)
        {
            DistanciaSeguridadCiclo form3 = new DistanciaSeguridadCiclo();
            form3.ShowDialog();

            this.distanciaSeguridad = form3.GetDistancia();
            this.tiempoCiclo = form3.GetTiempo();

            if (distanciaSeguridad > 0)
            {
                MessageBox.Show("Configuración guardada en el Menú.");
                panel1.Invalidate();
            }
        }

        private void BttnAñadirFlightPlan_Click(object sender, EventArgs e)
        {
            AñadirFlightPlan Form2 = new AñadirFlightPlan();
            Form2.ShowDialog();
            if (Form2.HayLista() == 0)
            {
                this.planes = Form2.GetLista();
                // Reiniciamos los planes cargados por seguridad para vaciar sus pilas internas
                planes.ReiniciarTodos();
                MessageBox.Show("Planes de vuelo cargados correctamente");

                panel1.Invalidate();
                ActualizarGridExterno();
            }
        }

        private void ImportarFlightPlan_Click(object sender, EventArgs e)
        {
            Importar__exportar_fichero form4 = new Importar__exportar_fichero();

            form4.SetLista(planes, this.tiempoCiclo, this.distanciaSeguridad);
            form4.ShowDialog();

            planes = form4.GetLista();

            if (planes != null)
            {
                this.tiempoCiclo = planes.TiempoCiclo;
                this.distanciaSeguridad = planes.DistanciaSeguridad;
                planes.ReiniciarTodos(); // Limpiamos las pilas internas al importar un escenario nuevo

                MessageBox.Show($"Datos cargados. Velocidad de simulación: {tiempoCiclo}s, Distancia de seguridad: {distanciaSeguridad}m");
            }

            panel1.Invalidate();
            ActualizarGridExterno();
        }

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
        // SIMULADOR (Pintado y Clics)
        // ==========================================

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            float maxLogicoX = 1000f; 
            float maxLogicoY = 500f;
            float anchoPanel = panel1.Width;
            float altoPanel = panel1.Height;

            float escalaX = anchoPanel / maxLogicoX;
            float escalaY = altoPanel / maxLogicoY;

            for (int i = 0; i < planes.GetNum(); i++)
            {
                FlightPlan vueloI = planes.GetFlightPlan(i);

                Position inicio = vueloI.GetInitialPosition();
                Position fin = vueloI.GetFinalPosition();
                Position actual = vueloI.GetCurrentPosition();

                float xIni = (float)inicio.GetX() * escalaX;
                float yIni = (float)inicio.GetY() * escalaY;
                float xFin = (float)fin.GetX() * escalaX;
                float yFin = (float)fin.GetY() * escalaY;
                float xAct = (float)actual.GetX() * escalaX;
                float yAct = (float)actual.GetY() * escalaY;

                float distanciaSeguridadEscalada = distanciaSeguridad * ((escalaX + escalaY) / 2);

                using (Pen lapiz = new Pen(Color.Black, 2))
                {
                    e.Graphics.DrawLine(lapiz, xIni, yIni, xFin, yFin);
                }

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

                string iconoAvion = "\u2708";
                using (Font fuenteAvion = new Font("Arial", 14, FontStyle.Bold))
                {
                    e.Graphics.DrawString(iconoAvion, fuenteAvion, Brushes.DarkBlue, xAct - 10, yAct - 12);
                }
            }
        }

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

        // ÚNICO MÉTODO TICK (CORREGIDO Y SIN DUPLICAR)
        private void timer1_Tick(object sender, EventArgs e)
        {
            segundos++;
            if (planes.HabraConflictoLista(distanciaSeguridad))
                ChocaLabel.Text = "Choca";
            else
                ChocaLabel.Text = "No choca";

            bool algunoSinLlegar = false;
            int i = 0;
            while (i < planes.GetNum() && !algunoSinLlegar)
            {
                if (!planes.GetFlightPlan(i).HasArrived())
                    algunoSinLlegar = true;
                i++;
            }

            if (algunoSinLlegar)
            {
                for (int j = 0; j < planes.GetNum(); j++)
                {
                    if (!planes.GetFlightPlan(j).HasArrived())
                    {
                        // Mover() ya se encarga de guardar automáticamente el historial con Push()
                        planes.GetFlightPlan(j).Mover(tiempoCiclo);
                    }
                }
                panel1.Invalidate();
                ActualizarGridExterno();
            }
            else
            {
                timer1.Stop();
            }
        }

        // Botón Añadir un ciclo manual
        private void BttnAñadirUnCiclo_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            for (int i = 0; i < planes.GetNum(); i++)
            {
                planes.GetFlightPlan(i).Mover(tiempoCiclo);
            }
            segundos++;
            panel1.Invalidate();
            ActualizarGridExterno();
        }

        // Botón Retroceder (Pop)
        private void BttnRetroceder_Click(object sender, EventArgs e)
        {
            timer1.Stop();

            // Llama a Deshacer() de la clase FlightPlanList que creamos antes
            planes.DeshacerTodos();

            if (segundos > 0)
            {
                segundos--;
            }

            panel1.Invalidate();
            ActualizarGridExterno();
        }

        // Botón Reiniciar
        private void RestartSimBtn_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            segundos = 0;

            // Devuelve los aviones a su origen y limpia las pilas
            planes.ReiniciarTodos();

            panel1.Invalidate();
            ActualizarGridExterno();
            MessageBox.Show("Simulación reiniciada e historiales vaciados.");
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
                    planes.ReiniciarTodos(); // Limpiamos historiales si alteramos velocidades
                    MessageBox.Show("Velocidades cambiadas");

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
                planes.GuardarEnArchivo(dialogo.FileName, this.tiempoCiclo, this.distanciaSeguridad);
                MessageBox.Show("Progreso guardado correctamente.");
                Close();
            }
        }

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
                planes.ReiniciarTodos(); // Limpia los Stacks al apagar la pantalla de simulación
                Menu_Load(sender, e);
                BotonEncendidoApagado.BackColor = SystemColors.Control;
            }
        }
    }
}