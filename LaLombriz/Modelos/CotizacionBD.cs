using LaLombriz.Clases;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaLombriz.Modelos
{
    public class CotizacionBD
    {

        public virtual List<Cotizacion> getCotizacionesModel(string strConnection, int estatus)
        {
            List<Cotizacion> cotizacion = new List<Cotizacion>();
            string query = "SELECT * FROM `cotizaciones` WHERE ESTATUS = " + estatus + "";
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
                    while (reader.Read()) //asignamos datos 
                    {
                        cotizacion.Add(new Cotizacion(Convert.ToInt32(reader.GetString(0)), Convert.ToInt32(reader.GetString(1)), reader.GetString(2), Convert.ToInt32(reader.GetString(3)), Convert.ToDateTime(reader.GetString(4)), Convert.ToDateTime(reader.GetString(5)), reader.GetString(6), reader.GetString(7)));
                    }
                }
                dbConnection.Close();
                return cotizacion;
            }
            catch (Exception e)
            {
                //Mensaje de error
                Console.WriteLine("Error" + e);
                return cotizacion;
            }
        }

        public virtual Cotizacion getCotizacionModel(string strConnection, int idCotizacion)
        {
            Cotizacion cotizacion = null;
            string query = "SELECT * FROM `cotizaciones` WHERE ID_COTIZACION = " + idCotizacion + "";
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
                    while (reader.Read()) //asignamos datos 
                    {
                        cotizacion = new Cotizacion(Convert.ToInt32(reader.GetString(0)), Convert.ToInt32(reader.GetString(1)), reader.GetString(2), Convert.ToInt32(reader.GetString(3)), Convert.ToDateTime(reader.GetString(4)), Convert.ToDateTime(reader.GetString(5)), reader.GetString(6), reader.GetString(7));
                    }
                }
                dbConnection.Close();
                return cotizacion;
            }
            catch (Exception e)
            {
                //Mensaje de error
                Console.WriteLine("Error" + e);
                return cotizacion;
            }
        }

        public virtual bool sendAnswerModel(string strConnection, int idCotizacion, string respuesta, int idUser, string fecha)
        {
            //Sentencia
            string query = "UPDATE `cotizaciones` SET `ID_ADMINISTRADOR`=" + idUser + " ,`ESTATUS`=1,`FECHA_RESPUESTA`='" + fecha + "',`MSJ_ADMINISTRADOR`='" + respuesta + "' WHERE ID_COTIZACION =" + idCotizacion + "";
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

        public virtual bool sendTextModel(int idUser, string date, string description, string strConnection)
        {

            string query = "INSERT INTO cotizaciones(`ID_CLIENTE`, `ID_ADMINISTRADOR`, `ESTATUS`, `FECHA_CONTACTO`, `FECHA_RESPUESTA`, `MSJ_CLIENTE`,`MSJ_ADMINISTRADOR`) VALUES (" + idUser + ",1, 0, '" + date + "', '1900-01-01', '" + description + "', '')";
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
        public virtual List<Cotizacion> getAllUserCotizacionModel(string strConnection, int idUser,int estatus)
        {
            List<Cotizacion> cotizacion = new List<Cotizacion>();
            string query = "SELECT * FROM `cotizaciones` WHERE ID_CLIENTE = " + idUser + " AND ESTATUS = "+estatus+"";
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
                    while (reader.Read()) //asignamos datos 
                    {
                        cotizacion.Add(new Cotizacion(Convert.ToInt32(reader.GetString(0)), Convert.ToInt32(reader.GetString(1)), reader.GetString(2), Convert.ToInt32(reader.GetString(3)), Convert.ToDateTime(reader.GetString(4)), Convert.ToDateTime(reader.GetString(5)), reader.GetString(6), reader.GetString(7)));
                    }
                }
                dbConnection.Close();
                return cotizacion;
            }
            catch (Exception e)
            {
                //Mensaje de error
                Console.WriteLine("Error" + e);
                return cotizacion;
            }
        }

    }
}