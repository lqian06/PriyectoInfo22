using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SQLite;
using FlightLib;

namespace Interfaz
{
    public partial class FormInicioSesion : Form //Form de inicio de sesión
    {
        public FormInicioSesion() //Iniciar el formulario
        {            
            InitializeComponent();
        }

        private void BotonIniciarSesion_Click(object sender, EventArgs e) // Botón para iniciar sesión
        {
            Usuario user = new Usuario();
            user.SetNombre(textBox1.Text);
            user.SetContrasena(textBox2.Text);

            BBDD miBBDD = new BBDD();
            miBBDD.Iniciar();

            if (miBBDD.ValidarUsuario(user))
            {
                Menu principal = new Menu();
                principal.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("No existe o datos mal escritos.");
            }
            miBBDD.Cerrar();
        }

        private void Botoncrearusuario_Click(object sender, EventArgs e) // Botón para crear un nuevo usuario
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                Usuario nuevo = new Usuario();
                nuevo.SetNombre(textBox1.Text);
                nuevo.SetContrasena(textBox2.Text);

                BBDD miBBDD = new BBDD();
                miBBDD.Iniciar();

                if (miBBDD.ValidarUsuario(nuevo))
                {
                    MessageBox.Show("nombre ya eb uso");
                }
                else
                {
                    miBBDD.GuardarUsuario(nuevo);
                    MessageBox.Show("Registrado");

                    textBox1.Clear();
                    textBox2.Clear();
                }

                miBBDD.Cerrar();
            }
            else
            {
                MessageBox.Show("faltan datos");
            }
        }
    }
}


