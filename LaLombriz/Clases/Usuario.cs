using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaLombriz.Clases
{
    public class Usuario
    {
        
        private string nombre;
        private string correo;
        private string pass;
        private string telefono;

        public Usuario() { }  //constructor vacio
        public Usuario(string correo, string pass) //constructor para inicio sesión
        {
            this.correo = correo;
            this.pass = pass;
        }
        public Usuario(string correo) //constructor para validación
        {
            this.correo = correo;
        }
        public Usuario (string nombre, string correo,string pass, string telefono)  //constructor sobrecargado
        {
            
            this.nombre = nombre;
            this.correo = correo;
            this.pass = pass;
            this.telefono = telefono;
        }
        //getters y setters, atributos 
        
        public string Nombre { set { nombre = value; } get { return nombre; } }
        public string Correo { set { correo = value; } get { return correo; } }
        public string Pass { set { pass = value; } get { return pass; } }
        public string Telefono { set { telefono = value; } get { return telefono; } }
        //Metodos 
        public bool createUser(string strConnection)
        {
            //Sentencia
            string query = "INSERT INTO usuarios(`nombre_usuario`, `correo`, `password`, `telefono`) VALUES ('"+this.nombre+"', '"+this.correo+"', '"+this.pass+"', '"+this.telefono+"')";
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
            }catch(Exception e)
            {
                Console.WriteLine("Error " + e);
                return false;
            }
        }
        public bool IniciarSesion(string mail, string contra, string strConnection)
        {
            //Sentencia
            string query = "SELECT NOMBRE_USUARIO, CORREO, TELEFONO FROM usuarios  WHERE correo='" + mail + "' AND password='" + contra + "'";
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
                        nombre = reader.GetString(0);
                        correo = reader.GetString(1);
                        telefono = reader.GetString(2);
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
        public bool CorreoDoble(string strConnection)
        {
            string correocomparar = "";
            //Sentencia
            string query = "SELECT CORREO FROM usuarios  WHERE correo='" + this.correo + "'";
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

    }
}