using LaLombriz.Clases;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaLombriz.Modelos
{
    public class ProductosModel
    {
        public virtual Productos getProductModel(string strConnection, int idProducto)
        {
            Productos producto = new Productos();
            string query = "SELECT * FROM `productos` WHERE ID_PRODUCTO = " + idProducto + "";
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
                        producto = new Productos(reader.GetString(1), reader.GetString(2), reader.GetString(3), Convert.ToDouble(reader.GetString(4)), 0);
                    }
                }
                dbConnection.Close();
                return producto;
            }
            catch (Exception e)
            {
                //Mensjae de error
                Console.WriteLine("Error" + e);
                return producto;
            }
        }

        public virtual List<Productos> getAllModel(string strConnection)
        {
            List<Productos> productos = new List<Productos>();
            string query = "SELECT * FROM `productos`";
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
                        productos.Add(new Productos(Convert.ToInt32(reader.GetString(0)), reader.GetString(1), reader.GetString(2), reader.GetString(3), Convert.ToDouble(reader.GetString(4)), Convert.ToInt32(reader.GetString(5)), 0));
                    }
                }
                dbConnection.Close();
                return productos;
            }
            catch (Exception e)
            {
                //Mensjae de error
                Console.WriteLine("Error" + e);
                return productos;
            }
        }

        public virtual bool updateProductModel(string strConnection, int idProducto, string descripcion, double precio, int stock)
        {
            //Sentencia
            string query = "UPDATE `productos` SET `DESCRIPCION`='" + descripcion + "',`PRECIO`=" + precio + ",`STOCK`=" + stock + " WHERE ID_PRODUCTO = " + idProducto + "";
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


    }
}