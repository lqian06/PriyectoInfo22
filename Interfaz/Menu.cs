using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FlightLib;

namespace Interfaz
{
    public partial class Menu : Form
    {
        // ATRIBUTOS
        private FlightPlanList planes = new FlightPlanList();
        private int radio = 10;
        private int distanciaSeguridad = 0;
        private int tiempoCiclo = 0;
        private int segundos = 0;
        private Grid ventanaGrid;

        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            // Ocultar componentes visuales al arrancar el menú principal
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

        // CONFIGURACIÓN Y CARGA DE DATOS
        private void BtnDistanciaSeguridadCiclo_Click(object sender, EventArgs e)
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

        private void BtnAnadirFlightPlan_Click(object sender, EventArgs e)
        {
            AñadirFlightPlan Form2 = new AñadirFlightPlan();
            Form2.ShowDialog();

            if (Form2.HayLista() == 0)
            {
                this.planes = Form2.GetLista();
                planes.ReiniciarTodos();

                MessageBox.Show("Planes de vuelo cargados correctamente");
                panel1.Invalidate();
                ActualizarGridExterno();
            }
        }

        private void BtnImportarFlightPlan_Click(object sender, EventArgs e)
        {
            Importar__exportar_fichero form4 = new Importar__exportar_fichero();
            form4.SetLista(planes, this.tiempoCiclo, this.distanciaSeguridad);
            form4.ShowDialog();

            planes = form4.GetLista();

            if (planes != null)
            {
                this.tiempoCiclo = planes.GetTiempoCiclo();
                this.distanciaSeguridad = planes.GetDistanciaSeguridad();
                planes.ReiniciarTodos();

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
                    MessageBox.Show("¡Alerta! Habrá un conflicto entre los vuelos.");
                else
                    MessageBox.Show("No habrá ningún conflicto entre los vuelos.");
            }
            else
            {
                MessageBox.Show("Error: No hay datos de vuelos.");
            }
        }

        // Ciclos y Ticks
        private void BtnIniciar_Click(object sender, EventArgs e)
        {
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
                    int velocidadMinimaPermitida = 10;
                    bool resuelto = planes.ResolverConflicto(distanciaSeguridad, velocidadMinimaPermitida);

                    if (resuelto)
                    {
                        MessageBox.Show("El conflicto ha sido resuelto con éxito recalculando las velocidades.", "Solución Aplicada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ActualizarGridExterno();
                        panel1.Invalidate();
                    }
                    else
                    {
                        MessageBox.Show("No ha sido posible resolver el conflicto de forma segura. Procediendo con precaución.", "Solución Fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            timer1.Interval = 1000;
            timer1.Start();

            btnParar.Visible = true;
            BtnIniciar.Visible = false;
        }

        private void BtnParar_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            btnParar.Visible = false;
            BtnIniciar.Visible = true;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            segundos++;

            // Refrescar indicadores del Form
            if (planes.HabraConflictoLista(distanciaSeguridad))
                ChocaLabel.Text = "Alerta: Conflicto Detectado";
            else
                ChocaLabel.Text = "Espacio Aéreo Seguro";

            bool enMovimiento = false;
            int i = 0;
            while (i < planes.GetNum())
            {
                if (!planes.GetFlightPlan(i).HasArrived())
                {
                    planes.GetFlightPlan(i).Mover(tiempoCiclo);
                    enMovimiento = true;
                }
                i++;
            }

            if (enMovimiento)
            {
                panel1.Invalidate();
                ActualizarGridExterno();
            }
            else
            {
                timer1.Stop();
            }
        }

        private void BtnAnadirUnCiclo_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            planes.Mover(tiempoCiclo);
            segundos++;
            panel1.Invalidate();
            ActualizarGridExterno();
        }

        private void BtnRetroceder_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            planes.DeshacerTodos();

            if (segundos > 0) segundos--;

            panel1.Invalidate();
            ActualizarGridExterno();
        }

        private void BtnRestartSim_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            segundos = 0;
            planes.ReiniciarTodos();

            panel1.Invalidate();
            ActualizarGridExterno();
            MessageBox.Show("Simulación reiniciada e historiales vaciados.");
        }

        // Pintar
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            float maxLogicoX = 1000f;
            float maxLogicoY = 500f;
            float escalaX = panel1.Width / maxLogicoX;
            float escalaY = panel1.Height / maxLogicoY;

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

                float distSeguridadEscalada = distanciaSeguridad * ((escalaX + escalaY) / 2);

                using (Pen lapiz = new Pen(Color.Black, 2))
                {
                    e.Graphics.DrawLine(lapiz, xIni, yIni, xFin, yFin);
                }

                bool enConflicto = false;
                if (distanciaSeguridad > 0)
                {
                    for (int j = 0; j < planes.GetNum(); j++)
                    {
                        if (i != j && vueloI.Distancia(planes.GetFlightPlan(j)) <= distanciaSeguridad)
                        {
                            enConflicto = true;
                            break;
                        }
                    }
                }

                if (distanciaSeguridad > 0)
                {
                    float xCirculo = xAct - (distSeguridadEscalada / 2);
                    float yCirculo = yAct - (distSeguridadEscalada / 2);

                    if (enConflicto)
                    {
                        using (SolidBrush pintarcirculo = new SolidBrush(Color.FromArgb(80, Color.Red)))
                        {
                            e.Graphics.FillEllipse(pintarcirculo, xCirculo, yCirculo, distSeguridadEscalada, distSeguridadEscalada);
                        }
                    }
                    else
                    {
                        using (Pen lapizSeguro = new Pen(Color.Orange, 1))
                        {
                            e.Graphics.DrawEllipse(lapizSeguro, xCirculo, yCirculo, distSeguridadEscalada, distSeguridadEscalada);
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

        private void Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            float maxLogicoX = 1000f;
            float maxLogicoY = 500f;
            float escalaX = panel1.Width / maxLogicoX;
            float escalaY = panel1.Height / maxLogicoY;

            for (int i = 0; i < planes.GetNum(); i++)
            {
                FlightPlan fp = planes.GetFlightPlan(i);
                Position actual = fp.GetCurrentPosition();

                double avionEnPantallaX = actual.GetX() * escalaX;
                double avionEnPantallaY = actual.GetY() * escalaY;

                double dx = e.X - avionEnPantallaX;
                double dy = e.Y - avionEnPantallaY;
                double distanciaAlClic = Math.Sqrt((dx * dx) + (dy * dy));

                if (distanciaAlClic < (radio + 5))
                {
                    timer1.Stop();
                    string estadoVuelo = Convert.ToString(fp.HasArrived());
                    MessageBox.Show($"Detalles del vuelo:\r\nID: {fp.GetID()}\r\nCompañía: {fp.GetCompany()}\r\nVelocidad: {fp.GetVelocidad()}km/h\r\nPosición Actual: [{actual.GetX()},{actual.GetY()}]\r\nOrigen: [{fp.GetInitialPosition().GetX()},{fp.GetInitialPosition().GetY()}]\r\nDestino: [{fp.GetFinalPosition().GetX()},{fp.GetFinalPosition().GetY()}]\r\nEstado: {estadoVuelo}\r\nDistancia al destino: {actual.Distancia(fp.GetFinalPosition())}");
                }
            }
        }

        // grid
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

        private void BtnCambiarVel_Click(object sender, EventArgs e)
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
                    planes.ReiniciarTodos();
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
                planes.ReiniciarTodos();
                Menu_Load(sender, e);
                BotonEncendidoApagado.BackColor = SystemColors.Control;
            }
        }
    }
}