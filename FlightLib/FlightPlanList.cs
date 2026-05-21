using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace FlightLib
{
    public class FlightPlanList
    {
        // ATRIBUTOS
        private FlightPlan[] vector = new FlightPlan[100];
        private int number = 0;
        private int tiempoCiclo = 0;
        private int distanciaSeguridad = 0;

        // SETTERS Y GETTERS
        public int GetNum()
        {
            return number;
        }

        public FlightPlan GetFlightPlan(int i)
        {
            if (i < 0 || i >= number)
            {
                return null;
            }
            else
            {
                return vector[i];
            }
        }

        public int GetTiempoCiclo()
        {
            return this.tiempoCiclo;
        }

        public void SetTiempoCiclo(int tiempoCiclo)
        {
            this.tiempoCiclo = tiempoCiclo;
        }

        public int GetDistanciaSeguridad()
        {
            return this.distanciaSeguridad;
        }

        public void SetDistanciaSeguridad(int distanciaSeguridad)
        {
            this.distanciaSeguridad = distanciaSeguridad;
        }

        // CONSTRUCTOR
        public FlightPlanList()
        {
            this.vector = new FlightPlan[100];
            this.number = 0;
        }

        // COLISION Y DISTANCIAS
        public double DistanciasMinimas()
        {
            if (number < 2) return 0.0;

            double minima = vector[0].DistanciaMinima(vector[1]);
            int i = 0;
            while (i < number)
            {
                int j = i + 1;
                while (j < number)
                {
                    double d = vector[i].DistanciaMinima(vector[j]);
                    if (d < minima)
                        minima = d;
                    j++;
                }
                i++;
            }
            return minima;
        }

        public bool HabraConflictoLista(int distSeguridad)
        {
            int i = 0;
            while (i < number)
            {
                int j = i + 1;
                while (j < number)
                {
                    if (vector[i].HabraConflicto(vector[j], distSeguridad))
                        return true;
                    j++;
                }
                i++;
            }
            return false;
        }

        // SIMULACION
        public int AddFlightPlan(FlightPlan p)
        {
            if (number == 100)
            {
                return -1;
            }
            else
            {
                vector[number] = p;
                number++;
                return 0;
            }
        }

        public void Mover(double tiempo)
        {
            int i = 0;
            while (i < number)
            {
                vector[i].Mover(tiempo);
                i++;
            }
        }

        public bool MoverAvionesEnCurso(int tiempoCiclo)
        {
            bool algunoSeMovio = false;
            int i = 0;
            while (i < number)
            {
                if (!vector[i].HasArrived())
                {
                    vector[i].Mover(tiempoCiclo);
                    algunoSeMovio = true;
                }
                i++;
            }
            return algunoSeMovio;
        }

        public void DeshacerTodos()
        {
            int i = 0;
            while (i < number)
            {
                vector[i].Deshacer();
                i++;
            }
        }

        public void ReiniciarTodos()
        {
            int i = 0;
            while (i < number)
            {
                vector[i].Restart();
                i++;
            }
        }

        public void EscribeConsola()
        {
            int i = 0;
            while (i < number)
            {
                vector[i].EscribeConsola();
                i++;
            }
        }

        // CONFLICTO Y SOLUCION
        public bool ResolverConflicto(int distanciaSegura, int velmin)
        {
            double[] velocidadesOriginales = new double[100];
            int i = 0;
            while (i < number)
            {
                velocidadesOriginales[i] = vector[i].GetVelocidad();
                i++;
            }

            if (!HabraConflictoLista(distanciaSegura))
            {
                return true;
            }

            bool seModificoAlgo = true;

            while (HabraConflictoLista(distanciaSegura) && seModificoAlgo)
            {
                seModificoAlgo = false;
                i = 1;

                while (i < number && HabraConflictoLista(distanciaSegura))
                {
                    int j = 0;
                    while (j < i && HabraConflictoLista(distanciaSegura))
                    {
                        if (vector[i].HabraConflicto(vector[j], distanciaSegura))
                        {
                            while (vector[i].HabraConflicto(vector[j], distanciaSegura) && vector[i].GetVelocidad() > velmin)
                            {
                                vector[i].SetVelocidad(vector[i].GetVelocidad() - 1);
                                seModificoAlgo = true;
                            }

                            while (vector[i].HabraConflicto(vector[j], distanciaSegura) && vector[j].GetVelocidad() > velmin)
                            {
                                vector[j].SetVelocidad(vector[j].GetVelocidad() - 1);
                                seModificoAlgo = true;
                            }
                        }
                        j++;
                    }
                    i++;
                }
            }

            if (!HabraConflictoLista(distanciaSegura))
            {
                return true;
            }
            else
            {
                int jBackup = 0;
                while (jBackup < number)
                {
                    vector[jBackup].SetVelocidad(velocidadesOriginales[jBackup]);
                    jBackup++;
                }
                return false;
            }
        }

        // CARGAR Y GUARDAR ARCHIVOS
        public FlightPlanList CargarDesdeArchivo(string nombreArchivo)
        {
            try
            {
                this.number = 0;
                this.vector = new FlightPlan[100];

                using (StreamReader r = new StreamReader(nombreArchivo))
                {
                    string primeraLinea = r.ReadLine();

                    if (primeraLinea != null)
                    {
                        string[] config = primeraLinea.Split(' ');
                        if (config.Length >= 2)
                        {
                            this.SetTiempoCiclo(Convert.ToInt32(config[0]));
                            this.SetDistanciaSeguridad(Convert.ToInt32(config[1]));
                        }
                    }

                    string linea = r.ReadLine();
                    while (linea != null && this.number < 100)
                    {
                        if (string.IsNullOrWhiteSpace(linea))
                        {
                            linea = r.ReadLine();
                            continue;
                        }

                        string[] datos = linea.Split(' ');

                        if (datos.Length >= 7)
                        {
                            FlightPlan f = new FlightPlan(
                                datos[0],
                                datos[1],
                                Convert.ToDouble(datos[2]),
                                Convert.ToDouble(datos[3]),
                                Convert.ToDouble(datos[4]),
                                Convert.ToDouble(datos[5]),
                                Convert.ToDouble(datos[6])
                            );
                            this.AddFlightPlan(f);
                        }
                        linea = r.ReadLine();
                    }
                }
                return this;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void GuardarEnArchivo(string nombreArchivo, int tiempoCiclo, int distanciaSeguridad)
        {
            using (StreamWriter write = new StreamWriter(nombreArchivo))
            {
                write.WriteLine(tiempoCiclo + " " + distanciaSeguridad);

                int i = 0;
                while (i < number)
                {
                    write.WriteLine(vector[i].Escribirlinea());
                    i++;
                }
            }
        }

        public List<FlightPlan> GetAvionesConCambios()
        {
            List<FlightPlan> afectados = new List<FlightPlan>();
            int i = 0;
            while (i < number)
            {
                if (vector[i].HaCambiadoVelocidad())
                {
                    afectados.Add(vector[i]);
                }
                i++;
            }
            return afectados;
        }

 
    }
    
}