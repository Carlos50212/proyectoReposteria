using LaLombriz.Clases;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaLombriz.Modelos
{
    public class UsuariosBD
    {
        public virtual bool createUserModel(string strConnection,string nombre,string correo,string pass,string telefono)
        {
            //Sentencia
            string query = "INSERT INTO usuarios(`nombre_usuario`, `correo`, `password`, `telefono`) VALUES ('" + nombre + "', '" + correo + "', '" + pass + "', '" + telefono + "')";
            //string query = "INSERT INTO usuarios(`id_usuario`,`nombre_usuario`, `correo`, `password`, `telefono`) VALUES ('','" + this.nombre + "', '" + this.correo + "', '" + this.pass + "', '" + this.telefono + "')";
            //Conexiones 
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;

            try
            {
                //Abrir base de datos
                dbConnection.Open();
                //Insertamos
                MySqlDataReader myReader = cmdDB.ExecuteReader();
                //Cerramos base de datos 
                dbConnection.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
                return false;
            }
        }

        public virtual bool IniciarSesionModel(string mail, string contra, string strConnection)
        {
            Usuario user;
            //Sentencia
            string query = "SELECT NOMBRE_USUARIO, CORREO, TELEFONO,ID_USUARIO FROM usuarios  WHERE correo='" + mail + "' AND password='" + contra + "'";
            //Conexiones 
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;
            MySqlDataReader reader;
            try
            {
                dbConnection.Open();
                //Leemos los datos 
                reader = cmdDB.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read()) //asignamos datos a los atributos de la clase 
                    {
                        user = new Usuario(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                    }
                    dbConnection.Close();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                //Mensjae de error
                Console.WriteLine("Error" + e);
                return false;
            }
        }

        public virtual bool CorreoDobleModel(string strConnection,string correo)
        {
            string correocomparar = "";
            //Sentencia
            string query = "SELECT CORREO FROM usuarios  WHERE correo='" + correo + "'";
            //Conexiones 
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;
            MySqlDataReader reader;
            try
            {
                dbConnection.Open();
                //Leemos los datos 
                reader = cmdDB.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read()) //asignamos datos a los atributos de la clase 
                    {
                        correocomparar = reader.GetString(0);
                    }
                }
                dbConnection.Close();
                if (correocomparar == correo)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                //Mensjae de error
                Console.WriteLine("Error" + e);
                return false;
            }
        }

        public virtual Usuario getUserModel(int idUsuario, string strConnection)
        {
            Usuario user = new Usuario();
            //Sentencia
            string query = "SELECT * FROM usuarios  WHERE ID_USUARIO=" + idUsuario + "";
            //Conexiones 
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;
            MySqlDataReader reader;
            try
            {
                dbConnection.Open();
                //Leemos los datos 
                reader = cmdDB.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read()) //asignamos datos a los atributos de la clase 
                    {
                        user = new Usuario(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                    }
                }
                dbConnection.Close();
                return user;
            }
            catch (Exception e)
            {
                //Mensjae de error
                Console.WriteLine("Error" + e);
                return user;
            }
        }
        public virtual int getTypeUserModel(string strConnection, int idUsuario)
        {
            int typeUser = 0;
            //Sentencia
            string query = "SELECT `TIPO_USUARIO` FROM `rol` WHERE ID_USUARIO =" + idUsuario + "";
            //Conexiones 
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;
            MySqlDataReader reader;
            try
            {
                dbConnection.Open();
                //Leemos los datos 
                reader = cmdDB.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read()) //asignamos datos a los atributos de la clase 
                    {
                        typeUser = Convert.ToInt32(reader.GetString(0));
                    }
                }
                dbConnection.Close();
                return typeUser;
            }
            catch (Exception e)
            {
                //Mensjae de error
                Console.WriteLine("Error" + e);
                return typeUser;
            }
        }

    }
}