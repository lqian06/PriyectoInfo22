using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace FlightLib
{
    //Definir clase Usuario con propiedades Nombre y Contraseña, con sus respectivos métodos Get y Set
    public class Usuario
    {
        private string nombre;
        private string contrasena;

        public string GetNombre()
        {
            return this.nombre;
        }

        public void SetNombre(string valor)
        {
            this.nombre = valor;
        }

        public string GetContrasena()
        {
            return this.contrasena;
        }

        public void SetContrasena(string valor)
        {
            this.contrasena = valor;
        }
    }

    //Definir clase BBDD
    public class BBDD
    {
        SQLiteConnection cnx; // Variable de conexión a la base de datos
        
        // Método para iniciar la conexión a la base de datos
        public void Iniciar() 
        {
            string dataSource = "Data Source=usuarios.db";
            cnx = new SQLiteConnection(dataSource);
            cnx.Open();
        }

        // Método para cerrar la conexión a la base de datos
        public void Cerrar()
        {
            cnx.Close();
        }

        // Método para obtener todos los clientes de la base de datos
        public DataTable GetClientes()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM clientes";
            SQLiteDataAdapter adp = new SQLiteDataAdapter(sql, this.cnx);
            adp.Fill(dt);
            return dt;
        }

        // Método para validar si un usuario existe en la base de datos
        public bool ValidarUsuario(Usuario user)
        {
            string sql = "SELECT COUNT(*) FROM clientes WHERE usuario = '" + user.GetNombre() + "' AND contraseña = '" + user.GetContrasena() + "'";

            SQLiteCommand cmd = new SQLiteCommand(sql, this.cnx);

            object result = cmd.ExecuteScalar();

            int count = Convert.ToInt32(result);
            return count > 0;
        }

        // Método para guardar un nuevo usuario en la base de datos
        public void GuardarUsuario(Usuario user)
        {
            string nombre = user.GetNombre();
            string contrasena = user.GetContrasena();

            string sql = "INSERT INTO clientes (usuario, contraseña) VALUES ('" + user.GetNombre() + "', '" + user.GetContrasena() + "')";
                
            SQLiteCommand cmd = new SQLiteCommand(sql, this.cnx);
            cmd.ExecuteNonQuery();
        }
    }
}