using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;


namespace LaLombriz.Clases
{
    public class ProductosPedidos
    {

        private PedidosCliente pedido;
        private Usuario usuario;
        private ArrayList productos;
        private int cantidad;
        private int id_producto;

        public ProductosPedidos() { }
        public ProductosPedidos(PedidosCliente pedido,Usuario usuario,ArrayList productos)
        {
            this.pedido = pedido;
            this.usuario = usuario;
            this.productos = productos;
        }
        public ProductosPedidos(int id_producto,int cantidad)
        {
            this.cantidad = cantidad;
            this.id_producto = id_producto;
        }
        public PedidosCliente Pedido { set { this.pedido = value; } get { return pedido; } }
        public Usuario Usuario{ set { this.usuario = value; } get{ return usuario; }}
        public ArrayList Productos{ set { this.productos = value;} get { return productos; }}
        public int Cantidad { set { this.cantidad = value; } get { return cantidad; } }
        public int Id_producto { set { this.id_producto = value; } get { return id_producto; } }
        public Dictionary<int, string> getAllProducts(string strConnection,int idPedido)
        {
            Dictionary<int, string> productos = new Dictionary<int, string>();
            string query = "SELECT ID_PRODUCTO, CANTIDAD FROM `productos_pedido` WHERE ID_PEDIDO = "+idPedido+"";
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
                        productos.Add(Convert.ToInt32(reader.GetString(0)), reader.GetString(1));
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
        public List<ProductosPedidos> getProductOrder(string strConnection, int idPedido)
        {
            List<ProductosPedidos> productos = new List<ProductosPedidos>();
            string query = "SELECT ID_PRODUCTO, CANTIDAD FROM `productos_pedido` WHERE ID_PEDIDO = " + idPedido + "";
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
                        productos.Add(new ProductosPedidos(Convert.ToInt32(reader.GetString(0)), Convert.ToInt32(reader.GetString(1))));
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

    }
}