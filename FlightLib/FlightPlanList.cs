using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlightLib
{
    public class FlightPlanList 
    {
        FlightPlan[] vector = new FlightPlan[100];
        int number = 0;

        public FlightPlanList() // constructor que inicializa el vector y el número de elementos
        {
            FlightPlan[] vector = new FlightPlan[100];
            int number = 0;
        }

        public int AddFlightPlan(FlightPlan p) // método que añade un FlightPlan al vector
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
       
        public int GetNum() // método que devuelve el número de elementos que hay en el vector
        {
            return number;
        }

        public FlightPlan GetFlightPlan(int i) // método que devuelve el FlightPlan que hay en la posición i del vector
        {
            if (i < 0 || i >= number)
            { return null; }

            else
            {
                return vector[i];
            }
        }

        public void Mover(double tiempo) // método que mueve todos los FlightPlan del vector
        {
            int i = 0;
            while (i < number)
            {
                vector[i].Mover(tiempo);
                i++;
            }

        }
        public void EscribeConsola()// método que escribe en consola todos los FlightPlan del vector
        {
            int i = 0;
            while (i < number)
            {
                vector[i].EscribeConsola();
                i++;
            }

        }
      
        public double DistanciasMinimas()   // Devuelve la distancia mínima en toda la lista
        {
            double minima = vector[0].DistanciaMinima(vector[1]);
            int i = 0;
            while (i < number)
            {
                //Para no repetir pares, vector [0] con vector [1], [2], etc. Luego vector [1] con vector [2], [3], etc.
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
       
        public bool HabraConflictoLista(int distSeguridad)  // Ve si habrá conflicto entre todos los FlightPlan de la lista, con el mismo bucle que el de DistanciasMinimas().
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

        //Calcula la velocidad para que no haya conflicto
        public bool ResolverConflicto(int distanciaSegura, int velmin)
        {
            double[] velocidadesOriginales = new double[100];
            int i = 0;
            while (i < number)
            {
                velocidadesOriginales[i] = vector[i].GetVelocidad();
                i++;
            }

            i = 0;
            while (i < number && HabraConflictoLista(distanciaSegura))
            {
                // Primero intenta bajando
                while (vector[i].GetVelocidad() > velmin && HabraConflictoLista(distanciaSegura))
                {
                    vector[i].SetVelocidad(vector[i].GetVelocidad() - 5);
                }

                // Si no se resolvió bajando, restaura ese vuelo y prueba subiendo
                if (HabraConflictoLista(distanciaSegura))
                {
                    vector[i].SetVelocidad(velocidadesOriginales[i]);
                    while (vector[i].GetVelocidad() < 1000 && HabraConflictoLista(distanciaSegura))
                    {
                        vector[i].SetVelocidad(vector[i].GetVelocidad() + 5);
                    }
                }

                // Si tampoco se resolvió subiendo, restaura y pasa al siguiente
                if (HabraConflictoLista(distanciaSegura))
                {
                    vector[i].SetVelocidad(velocidadesOriginales[i]);
                }

                i++;
            }

            if (!HabraConflictoLista(distanciaSegura))
            {
                return true;
            }
            else
            {
                int j = 0;
                while (j < number)
                {
                    vector[j].SetVelocidad(velocidadesOriginales[j]);
                    j++;
                }
                return false;
            }
        }

        public FlightPlanList CargarDesdeArchivo(string nombreArchivo)  //Leer fichero y cargarlo en el vector, devuelve el vector con los datos cargados
        {
            FlightPlanList lista = new FlightPlanList();
            try
            {
                StreamReader r = new StreamReader(nombreArchivo);
                string linea = r.ReadLine();

                while (linea != null && lista.GetNum() < 100)
                {
                    string[] datos = linea.Split(' ');

                    if (datos.Length >= 7) // tiene que tener al menos 7 elementos para ser un FlightPlan válido
                    {
                        FlightPlan f = new FlightPlan(datos[0], datos[1], Convert.ToDouble(datos[2]), Convert.ToDouble(datos[3]), Convert.ToDouble(datos[4]), Convert.ToDouble(datos[5]), Convert.ToDouble(datos[6]));
                        lista.AddFlightPlan(f);
                    }
                    linea = r.ReadLine();
                }
                r.Close();
                return lista;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void GuardarEnArchivo(string nombreArchivo)// crea o sobrescribe el archivo, si exist
        {
            StreamWriter write = new StreamWriter(nombreArchivo);
            int i = 0;
            while (i < number)
            {
                write.WriteLine(vector[i].Escribirlinea());
                i++;
            }
            write.Close();
        }

    }
}

