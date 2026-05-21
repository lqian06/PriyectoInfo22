using System;
using System.Collections.Generic;

namespace FlightLib
{
    public class FlightPlan
    {
        // ATRIBUTOS 
        private string id;
        private string company;
        private Position initialPosition;
        private Position currentPosition;
        private Position finalPosition;
        private double velocidad;
        private double velocidadOriginal;
        private Stack<Position> historialPosiciones = new Stack<Position>();

        // CONSTRUCTOR
        public FlightPlan(string id, string company, double cpx, double cpy, double fpx, double fpy, double velocidad)
        {
            this.id = id;
            this.company = company;
            this.initialPosition = new Position(cpx, cpy);
            this.currentPosition = new Position(cpx, cpy);
            this.finalPosition = new Position(fpx, fpy);
            this.velocidad = velocidad;
            this.velocidadOriginal = velocidad;
        }

        // GETTERS Y SETTERS 
        public string GetID()
        {
            return id;
        }

        public void SetID(string id)
        {
            this.id = id;
        }

        public string GetCompany()
        {
            return this.company;
        }

        public Position GetInitialPosition()
        {
            return this.initialPosition;
        }

        public void SetInitialPosition(double x, double y)
        {
            this.initialPosition = new Position(x, y);
        }

        public Position GetCurrentPosition()
        {
            return currentPosition;
        }

        public void SetCurrentPosition(double x, double y)
        {
            currentPosition = new Position(x, y);
        }

        public Position GetFinalPosition()
        {
            return this.finalPosition;
        }

        public void SetFinalPosition(double x, double y)
        {
            this.finalPosition = new Position(x, y);
        }

        public double GetVelocidad()
        {
            return this.velocidad;
        }

        public void SetVelocidad(double velocidad)
        {
            this.velocidad = velocidad;
        }

        public double GetVelocidadOriginal()
        {
            return this.velocidadOriginal;
        }

        // mover aviones
        public void Mover(double tiempo)
        {
            historialPosiciones.Push(new Position(currentPosition.GetX(), currentPosition.GetY()));

            double distancia = tiempo * this.velocidad / 60;

            double hipotenusa = Math.Sqrt((finalPosition.GetX() - currentPosition.GetX()) * (finalPosition.GetX() - currentPosition.GetX()) + (finalPosition.GetY() - currentPosition.GetY()) * (finalPosition.GetY() - currentPosition.GetY()));
            double coseno = (finalPosition.GetX() - currentPosition.GetX()) / hipotenusa;
            double seno = (finalPosition.GetY() - currentPosition.GetY()) / hipotenusa;

            double x = currentPosition.GetX() + distancia * coseno;
            double y = currentPosition.GetY() + distancia * seno;

            Position nextPosition = new Position(x, y);

            if (currentPosition.Distancia(nextPosition) < hipotenusa)
            {
                currentPosition = nextPosition;
            }
            else
            {
                currentPosition = finalPosition;
            }
        }

        public bool HasArrived()
        {
            bool resultado = false;
            if ((Math.Abs(currentPosition.GetX() - initialPosition.GetX()) >= Math.Abs(finalPosition.GetX() - initialPosition.GetX())) &&
                (Math.Abs(currentPosition.GetY() - initialPosition.GetY()) >= Math.Abs(finalPosition.GetY() - initialPosition.GetY())))
            {
                resultado = true;
            }
            return resultado;
        }
        public bool HaCambiadoVelocidad()
        {
            return this.velocidad != this.velocidadOriginal;
        }

        // CÁLCULOS 
        public double Distancia(FlightPlan b)
        {
            double d = Math.Sqrt((b.GetCurrentPosition().GetX() - this.GetCurrentPosition().GetX()) * (b.GetCurrentPosition().GetX() - this.GetCurrentPosition().GetX()) + (b.GetCurrentPosition().GetY() - this.GetCurrentPosition().GetY()) * (b.GetCurrentPosition().GetY() - this.GetCurrentPosition().GetY()));
            return d;
        }

        public double DistanciaMinima(FlightPlan b)
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

        public bool HabraConflicto(FlightPlan b, int distSeguridad)
        {
            return this.DistanciaMinima(b) <= distSeguridad;
        }

        // HISTORIAL de posiciones
        public void Deshacer()
        {
            if (historialPosiciones.Count > 0)
            {
                this.currentPosition = historialPosiciones.Pop();
            }
        }

        public void Restart()
        {
            this.SetCurrentPosition(initialPosition.GetX(), initialPosition.GetY());
            this.historialPosiciones.Clear();
        }

        // SALIDA DE DATOS
        public void EscribeConsola()
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

        public string Escribirlinea()
        {
            return id + " " + company + " " + currentPosition.GetX() + " " + currentPosition.GetY() + " " + finalPosition.GetX() + " " + finalPosition.GetY() + " " + velocidad;
        }
    }
}