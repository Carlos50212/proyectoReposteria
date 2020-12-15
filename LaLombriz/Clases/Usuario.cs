using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaLombriz.Clases
{
    public class Usuario
    {
        private int id;
        private string nombre;
        private string correo;
        private string pass;
        private string telefono;

        public Usuario() { }  //constructor vacio
        public Usuario (int id, string nombre, string correo,string pass, string telefono)  //constructor sobrecargado
        {
            this.id = id;
            this.nombre = nombre;
            this.correo = correo;
            this.pass = pass;
            this.telefono = telefono;
        }
        //getters y setters, atributos 
        public int Id { set { id = value; } get { return id; } }
        public string Nombre { set { nombre = value; } get { return nombre; } }
        public string Correo { set { correo = value; } get { return correo; } }
        public string Pass { set { pass = value; } get { return pass; } }
        public string Telefono { set { telefono = value; } get { return telefono; } }
        //Metodos 
        public bool createUser(string strConnection)
        {
            //Sentencia
            string query = "INSERT INTO usuarios(`id_usuario`, `nombre_usuario`, `correo`, `password`, `telefono`) VALUES (" + this.id+", '"+this.nombre+"', '"+this.correo+"', '"+this.pass+"', '"+this.telefono+"')";
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
                return false;
            }
        }


    }
}