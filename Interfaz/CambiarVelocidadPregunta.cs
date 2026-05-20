using FlightLib;
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
    public partial class CambiarVelocidadPregunta : Form 
    {
        FlightPlanList planes = new FlightPlanList();
        int velmin = 1;
        int distanciaSegura;


        // Inicializa el formulario
        public CambiarVelocidadPregunta() 
        {
            InitializeComponent();
        }


        // Recibe la lista de planes y la distancia segura para el conflicto
        public void SetPlanes(FlightPlanList lista, int distanciaSeg) 
        {
            this.planes = lista;
            distanciaSegura = distanciaSeg;
        }


        //Getters
        public FlightPlanList GetPlanes() 
        {
            return planes;
        }
        public double GetVelocidadSugerida() 
        {
            return planes.GetFlightPlan(0).GetVelocidad();
        }

        // Si el usuario no quiere cambiar la velocidad, se cierra el formulario
        private void bttnNo_Click(object sender, EventArgs e) 
        {
            Close();
        }


        // Variable para indicar si el conflicto se resolvió o no
        private bool conflictoResuelto = false;
        public bool GetConflictoResuelto() // devuelve si el conflicto se resolvió o no
        {
            return conflictoResuelto;
        }

        // Método para cambiar la velocidad del avión y resolver el conflicto
        private void SiCambiarVelBtn_Click(object sender, EventArgs e)
        {
            if (planes.ResolverConflicto(distanciaSegura, velmin))
            {
                conflictoResuelto = true;
                MessageBox.Show("Conflicto resuelto cambiando velocidades.");
            }
            else
            {
                conflictoResuelto = false;
                MessageBox.Show("No se pudo resolver. Velocidad mínima alcanzada: " + velmin );
            }
            Close();
        }

    }
}

