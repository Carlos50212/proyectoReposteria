using LaLombriz.Clases;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaLombriz.Modelos
{
    public class PedidosClienteBD
    {

        public virtual List<PedidosCliente> getOrdersModel(string strConnection, int estatus)
        {
            List<PedidosCliente> pedidos = new List<PedidosCliente>();
            string query = "SELECT * FROM `pedidos` WHERE ESTATUS = " + estatus + "";
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
                        pedidos.Add(new PedidosCliente (Convert.ToInt32(reader.GetString(0)),Convert.ToInt32(reader.GetString(1)),Convert.ToDateTime(reader.GetString(2)),Convert.ToDateTime(reader.GetString(3)),Convert.ToDouble(reader.GetString(5))));
                    }
                }
                dbConnection.Close();
                return pedidos;
            }
            catch (Exception e)
            {
                //Mensjae de error
                Console.WriteLine("Error" + e);
                return pedidos;
            }
        }
        public virtual PedidosCliente getOrderModel(string strConnection, int idPedido)
        {
            PedidosCliente pedido = null;
            string query = "SELECT * FROM `pedidos` WHERE  ID_PEDIDO= " + idPedido + "";
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
                        pedido = new PedidosCliente(Convert.ToInt32(reader.GetString(0)), Convert.ToInt32(reader.GetString(1)), Convert.ToDateTime(reader.GetString(2)), Convert.ToDateTime(reader.GetString(3)), Convert.ToDouble(reader.GetString(5)));
                    }
                }
                dbConnection.Close();
                return pedido;
            }
            catch (Exception e)
            {
                //Mensjae de error
                Console.WriteLine("Error" + e);
                return pedido;
            }
        }
        public virtual bool deliverOrderModel(string strConnection, int idOrder)
        {
            string query = "UPDATE `pedidos` SET `ESTATUS`= 1 WHERE ID_PEDIDO =" + idOrder + "";
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

        public virtual bool cancelOrderModel(string strConnection, int idOrder)
        {
            string query = "UPDATE `pedidos` SET `ESTATUS`= 2 WHERE ID_PEDIDO =" + idOrder + "";
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