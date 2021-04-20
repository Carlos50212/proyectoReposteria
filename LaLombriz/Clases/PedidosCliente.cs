using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaLombriz.Clases
{
    public class PedidosCliente
    {
        private int id_pedido;
        private int id_usuario;
        private DateTime fecha_entrega;
        private DateTime fecha_creacion;
        private double precio;

        //constructor vacío
        public PedidosCliente() { }
        //constructor sobrecargado
        public PedidosCliente(int id_del_pedido, int id_del_usuario, DateTime fecha_de_entrega, DateTime fecha_de_creacion,double precio)
        {
            this.id_pedido = id_del_pedido;
            this.id_usuario = id_del_usuario;
            this.fecha_creacion = fecha_de_creacion;
            this.fecha_entrega = fecha_de_entrega;
            this.precio = precio;
        }
        //getters y setters
        public int Id_pedido { set { id_pedido = value; } get { return id_pedido; } }
        public int Id_usuario { set { id_usuario = value; } get { return id_usuario; } }
        public DateTime Fecha_entrega { set { fecha_entrega = value; } get { return fecha_entrega; } }
        public DateTime Fecha_creacion { set { fecha_creacion = value; } get { return fecha_creacion; } }
        public double Precio { set { precio = value; } get { return precio; } }

        public List<PedidosCliente> getOrders(string strConnection,int estatus)
        {
            List<PedidosCliente> pedidos = new List<PedidosCliente>();
            string query = "SELECT * FROM `pedidos` WHERE ESTATUS = "+estatus+"";
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
                        pedidos.Add(new PedidosCliente { id_pedido = Convert.ToInt32(reader.GetString(0)),id_usuario = Convert.ToInt32(reader.GetString(1)),fecha_entrega = Convert.ToDateTime(reader.GetString(2)), fecha_creacion= Convert.ToDateTime(reader.GetString(3)),precio = Convert.ToDouble(reader.GetString(5)) });
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
        public bool deliverOrder(string strConnection,int idOrder)
        {
            string query = "UPDATE `pedidos` SET `ESTATUS`= 1 WHERE ID_PEDIDO ="+idOrder+"";
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

        public bool cancelOrder(string strConnection, int idOrder)
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