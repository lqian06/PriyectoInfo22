using FlightLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class FlightPlan
    {
        // Atributos

        string id; // identificador
        string company; // compañia
        double initialx; // coordenada x de la posición inicial
        double initialy; // coordenada y de la posición inicial
        double finalx; // coordenada x de la posición final
        double finaly; // coordenada y de la posición final
        Position initialPosition;
        Position currentPosition; // posicion actual
        Position finalPosition; // posicion final
        double velocidad; //velocidad en km/h
        Stack<Position> historialPosiciones = new Stack<Position>(); //historial de posiciones



        // Constructures
        public FlightPlan(string id, string company, double cpx, double cpy, double fpx, double fpy, double velocidad)
        {
            this.id = id;
            this.company = company;
            this.initialPosition = new Position(cpx, cpy);
            this.currentPosition = new Position(cpx, cpy);
            this.finalPosition = new Position(fpx, fpy);
            this.velocidad = velocidad;
        }

        //||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

        public Position GetCurrentPosition() // devuelve la posición actual del vuelo
        {
            return currentPosition;
        }
        public void SetCurrentPosition(double x, double y) // establece la posición actual del vuelo
        {
            currentPosition = new Position(x, y);
        }

        public void SetID(string id) // establece el identificador del vuelo
        {
            this.id = id;
        }
        public string GetID() // devuelve el identificador del vuelo
        {
            return id;
        }

        public void SetVelocidad(double velocidad) // establece la velocidad del vuelo
        {
            this.velocidad = velocidad;
        }
        public double GetVelocidad() // devuelve la velocidad del vuelo
        {
            return this.velocidad;
        }

        public Position GetFinalPosition() // devuelve la posición final del vuelo
        {
            return this.finalPosition;
        }
        public void SetFinalPosition(double x, double y) // establece la posición final del vuelo
        {
            this.finalPosition = new Position(x, y);
        }

        public Position GetInitialPosition() // devuelve la posición inicial del vuelo
        {
            return this.initialPosition;
        }
        public void SetInitialPosition(double x, double y) // establece la posición inicial del vuelo
        {
            this.initialPosition = new Position(x, y);
        }
        public string GetCompany() // devuelve la compañia del vuelo
        {
            return this.company;
        }        
        
        //||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

        public void Mover(double tiempo) // Mueve el vuelo a la posición correspondiente a viajar durante el tiempo que se recibe como parámetro
        {
            // Guardamos la posición actual en el historial de posiciones
            historialPosiciones.Push(new Position(currentPosition.GetX(), currentPosition.GetY()));

            //Calculamos la distancia recorrida en el tiempo dado
            double distancia = tiempo * this.velocidad / 60;

            //Calculamos las razones trigonométricas
            double hipotenusa = Math.Sqrt((finalPosition.GetX() - currentPosition.GetX()) * (finalPosition.GetX() - currentPosition.GetX()) + (finalPosition.GetY() - currentPosition.GetY()) * (finalPosition.GetY() - currentPosition.GetY()));
            double coseno = (finalPosition.GetX() - currentPosition.GetX()) / hipotenusa;
            double seno = (finalPosition.GetY() - currentPosition.GetY()) / hipotenusa;

            //Caculamos la nueva posición del vuelo
            double x = currentPosition.GetX() + distancia * coseno;
            double y = currentPosition.GetY() + distancia * seno;

            Position nextPosition = new Position(x, y);

            // Modificar MoverVuelo para que no se pase del destino
            if (currentPosition.Distancia(nextPosition) < hipotenusa)
            {
                currentPosition = nextPosition;
            }
            else
            {
                currentPosition = finalPosition;
            }

        }

        public bool HasArrived() // Devuelve true si el vuelo ha llegado a su destino
        {
            bool resultado = false;
            if ((Math.Abs(currentPosition.GetX() - initialPosition.GetX()) >= Math.Abs(finalPosition.GetX() - initialPosition.GetX())) && (Math.Abs(currentPosition.GetY() - initialPosition.GetY()) >= Math.Abs(finalPosition.GetY() - initialPosition.GetY())))
            {
                resultado = true;
            }
            return resultado;
        }

       

        public void EscribeConsola() // escribe en consola los datos del plan de vuelo
        {
            Console.WriteLine("******************************");
            Console.WriteLine("Datos del vuelo: ");
            Console.WriteLine("Identificador: {0}", id);
            Console.WriteLine("Velocidad: {0:F2}", velocidad);
            Console.WriteLine("Posición actual: ({0:F2}, {1:F2})", currentPosition.GetX(), currentPosition.GetY());
            if (this.HasArrived())
            {
                Console.WriteLine("Ha llegado al destino");
            }
            Console.WriteLine("******************************");
        }

        public void Restart() // reinicia el vuelo a su posición inicial y vacía el stack
        {
            this.SetCurrentPosition(initialPosition.GetX(), initialPosition.GetY());
            this.historialPosiciones.Clear();
        }

        public double Distancia(FlightPlan b) // devuelve la distancia entre vuelos
        {
            double d = Math.Sqrt((b.GetCurrentPosition().GetX() - this.GetCurrentPosition().GetX()) * (b.GetCurrentPosition().GetX() - this.GetCurrentPosition().GetX()) + (b.GetCurrentPosition().GetY() - this.GetCurrentPosition().GetY()) * (b.GetCurrentPosition().GetY() - this.GetCurrentPosition().GetY()));
            return d;
        }


        //Devuelve la distancia mínima entres dos vuelos
        public double DistanciaMinima(FlightPlan b) // devuelve la distancia mínima entre dos vuelos
        {
            double tiempoMinimo;
            double distanciaMinima;

            double velocidadA = velocidad / 60;
            double velocidadB = b.GetVelocidad() / 60;

            double hipotenusaA = Math.Sqrt((finalPosition.GetX() - initialPosition.GetX()) * (finalPosition.GetX() - initialPosition.GetX()) + (finalPosition.GetY() - initialPosition.GetY()) * (finalPosition.GetY() - initialPosition.GetY()));
            double hipotenusaB = Math.Sqrt((b.GetFinalPosition().GetX() - b.GetInitialPosition().GetX()) * (b.GetFinalPosition().GetX() - b.GetInitialPosition().GetX()) + (b.GetFinalPosition().GetY() - b.GetInitialPosition().GetY()) * (b.GetFinalPosition().GetY() - b.GetInitialPosition().GetY()));

            double cosenoA = (finalPosition.GetX() - initialPosition.GetX()) / hipotenusaA;
            double senoA = (finalPosition.GetY() - initialPosition.GetY()) / hipotenusaA;
            double cosenoB = (b.GetFinalPosition().GetX() - b.GetInitialPosition().GetX()) / hipotenusaB;
            double senoB = (b.GetFinalPosition().GetY() - b.GetInitialPosition().GetY()) / hipotenusaB;

            tiempoMinimo = -((initialPosition.GetX() - b.GetInitialPosition().GetX()) * (velocidadA * cosenoA - velocidadB * cosenoB) + (initialPosition.GetY() - b.GetInitialPosition().GetY()) * (velocidadA * senoA - velocidadB * senoB)) / ((velocidadA * cosenoA - velocidadB * cosenoB) * (velocidadA * cosenoA - velocidadB * cosenoB) + (velocidadA * senoA - velocidadB * senoB) * (velocidadA * senoA - velocidadB * senoB));

            double posAX = initialPosition.GetX() + velocidadA * cosenoA * tiempoMinimo;
            double posAY = initialPosition.GetY() + velocidadA * senoA * tiempoMinimo;
            double posBX = b.GetInitialPosition().GetX() + velocidadB * cosenoB * tiempoMinimo;
            double posBY = b.GetInitialPosition().GetY() + velocidadB * senoB * tiempoMinimo;

            distanciaMinima = Math.Sqrt((posAX - posBX) * (posAX - posBX) + (posAY - posBY) * (posAY - posBY));

            return distanciaMinima;

        }

        public bool HabraConflicto(FlightPlan b, int distSeguridad)   //Ve si habrá conflico solo entre dos FlightPlans
        {
            if (this.DistanciaMinima(b) <= distSeguridad)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Deshacer() // deshace el último movimiento del vuelo
        {
            if (historialPosiciones.Count > 0)
            {
                this.currentPosition = historialPosiciones.Pop(); //recuperar la última posición del historial y establecerla como la posición actual
            }
        }


        public string Escribirlinea() // escribe en una sola línea los datos del plan de vuelo, separados por espacios
        {
            return id + " " + company + " " + currentPosition.GetX() + " " + currentPosition.GetY() + " " + finalPosition.GetX() + " " + finalPosition.GetY() + " " + velocidad;
        }
    }
}
