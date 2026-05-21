using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightLib;

namespace FlightLib
{
    // USUARIO
    public class Usuario
    {
        private string nombre;
        private string contrasena;

        public string GetNombre()
        {
            return this.nombre;
        }

        public void SetNombre(string nombreusuario)
        {
            this.nombre = nombreusuario;
        }

        public string GetContrasena()
        {
            return this.contrasena;
        }

        public void SetContrasena(string contrasenausuario)
        {
            this.contrasena = contrasenausuario;
        }
    }

    // COMPAÑÍA 
    public class Compania
    {
        private string nombre;
        private string telefono;
        private string correo;

        public string GetNombre()
        {
            return this.nombre;
        }

        public void SetNombre(string nombrecompania)
        {
            this.nombre = nombrecompania;
        }

        public string GetTelefono()
        {
            return this.telefono;
        }

        public void SetTelefono(string telefonocompania)
        {
            this.telefono = telefonocompania;
        }

        public string GetCorreo()
        {
            return this.correo;
        }

        public void SetCorreo(string correocompania)
        {
            this.correo = correocompania;
        }

        public Compania(string nombre, string telefono, string correo)
        {
            this.nombre = nombre;
            this.telefono = telefono;
            this.correo = correo;
        }
    }

    // BASE DE DATOS
    public class BBDD
    {
        private SQLiteConnection cnx; // Variable de conexión a la base de datos

        // CONEXIÓN
        public void Iniciar()
        {
            
            string dataSource = "Data Source=usuarios.db";
            cnx = new SQLiteConnection(dataSource);
            cnx.Open();

        }

        public void Cerrar()
        {
            if (cnx != null && cnx.State == ConnectionState.Open)
            {
                cnx.Close();
            }
        }

        // GESTIÓN DE USUARIOS
        public DataTable GetClientes()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM clientes";
            SQLiteDataAdapter adp = new SQLiteDataAdapter(sql, this.cnx);
            adp.Fill(dt);
            return dt;
        }

        public bool ValidarUsuario(Usuario user)
        {
            string sql = "SELECT COUNT(*) FROM clientes WHERE usuario = @usuario AND contraseña = @contrasena";

            SQLiteCommand cmd = new SQLiteCommand(sql, this.cnx);
            cmd.Parameters.AddWithValue("@usuario", user.GetNombre());
            cmd.Parameters.AddWithValue("@contrasena", user.GetContrasena());

            object result = cmd.ExecuteScalar();
            int count = Convert.ToInt32(result);
            return count > 0;
        }

        public void GuardarUsuario(Usuario user)
        {
            string sql = "INSERT INTO clientes (usuario, contraseña) VALUES (@usuario, @contrasena)";

            SQLiteCommand cmd = new SQLiteCommand(sql, this.cnx);
            cmd.Parameters.AddWithValue("@usuario", user.GetNombre());
            cmd.Parameters.AddWithValue("@contrasena", user.GetContrasena());

            cmd.ExecuteNonQuery();
        }

        // GESTIÓN DE COMPAÑÍAS
        
        public List<Compania> ObtenerTodasLasCompanias()
        {
            List<Compania> lista = new List<Compania>();
            string sql = "SELECT * FROM companias";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, this.cnx))
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new Compania(
                        reader["nombre"].ToString(),
                        reader["telefono"].ToString(),
                        reader["correo"].ToString()
                    ));
                }
            }
            return lista;
        }

        public Compania GetCompaniaPorNombre(string nombre)
        {
            string sql = "SELECT * FROM companias WHERE nombre = @nombre";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, this.cnx))
            {
                cmd.Parameters.AddWithValue("@nombre", nombre);
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Compania(
                            reader["nombre"].ToString(),
                            reader["telefono"].ToString(),
                            reader["correo"].ToString()
                        );
                    }
                }
            }
            return null; // Si no existe
        }

        public void GuardarCompania(Compania comp)
        {
            string sql = "INSERT OR REPLACE INTO companias (nombre, telefono, correo) VALUES (@nombre, @telefono, @correo)";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, this.cnx))
            {
                cmd.Parameters.AddWithValue("@nombre", comp.GetNombre());
                cmd.Parameters.AddWithValue("@telefono", comp.GetTelefono());
                cmd.Parameters.AddWithValue("@correo", comp.GetCorreo());
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarCompania(string nombreCompania)
        {
            string sql = "DELETE FROM companias WHERE nombre = @nombre";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, this.cnx))
            {
                cmd.Parameters.AddWithValue("@nombre", nombreCompania);
                cmd.ExecuteNonQuery();
            }
        }

        public void GenerarInformeCambios(string ruta, FlightPlanList lista)
        {

            List<FlightPlan> cambiados = lista.GetAvionesConCambios();

            using (StreamWriter sw = new StreamWriter(ruta))
            {
                foreach (FlightPlan fp in cambiados)
                {
                    Compania c = this.GetCompaniaPorNombre(fp.GetCompany());

                    string correo = (c != null) ? c.GetCorreo() : "N/A";
                    string telefono = (c != null) ? c.GetTelefono() : "N/A";

                    sw.WriteLine($"{fp.GetCompany()};{correo};{telefono};{fp.GetID()};{fp.GetVelocidadOriginal()};{fp.GetVelocidad()}");
                }
            }
        }

    }
}