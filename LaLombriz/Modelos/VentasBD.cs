using LaLombriz.Clases;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaLombriz.Modelos
{
    public class VentasBD
    {
        public virtual List<Ventas> getAllSellsDayModel(DateTime fecha, string strConnection)
        {
            List<Ventas> ventas = new List<Ventas>();
            string query = "SELECT * FROM ventas WHERE FECHA =  " + fecha + " ORDER BY UNIDADES DESC LIMIT 3";
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
                        ventas.Add(new Ventas(Convert.ToInt32(reader.GetString(0)), Convert.ToInt32(reader.GetString(1)), Convert.ToDouble(reader.GetString(2)), Convert.ToDateTime(reader.GetString(3))));
                    }
                }
                dbConnection.Close();
                return ventas;
            }
            catch (Exception e)
            {
                //Mensjae de error
                Console.WriteLine("Error" + e);
                return ventas;
            }
        }

        public virtual List<Ventas> getAllSellsMonthModel(string strConnection, string date)
        {
            List<Ventas> ventas = new List<Ventas>();
            string query = "SELECT * FROM ventas WHERE MONTH(FECHA) = MONTH('" + date + "') AND YEAR(FECHA) = YEAR('" + date + "') ORDER BY UNIDADES DESC LIMIT 3";
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
                        ventas.Add(new Ventas(Convert.ToInt32(reader.GetString(0)), Convert.ToInt32(reader.GetString(1)), Convert.ToDouble(reader.GetString(2)), Convert.ToDateTime(reader.GetString(3))));
                    }
                }
                dbConnection.Close();
                return ventas;
            }
            catch (Exception e)
            {
                //Mensjae de error
                Console.WriteLine("Error" + e);
                return ventas;
            }
        }

    }
}