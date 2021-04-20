using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LaLombriz.Clases
{
    public class Productos
    {
        private int idProducto;
        private string nombre_producto;
        private string descripcion;
        private string tamanio;
        private double precio;
        private int cantidad;
        private int stock;

        //Constructor vacío
        public Productos() { }
        //Constructor sobrecargado
        public Productos(string nombredelproducto, string descripcion_prod, string tamanio_prod, double precio_prod,int cantidad)
        {
            this.nombre_producto = nombredelproducto;
            this.descripcion = descripcion_prod;
            this.tamanio = tamanio_prod;
            this.precio = precio_prod;
            this.cantidad = cantidad;
        }
        public Productos(int idProducto,string nombredelproducto, string descripcion_prod, string tamanio_prod, double precio_prod,int stock, int cantidad)
        {
            this.idProducto = idProducto;
            this.nombre_producto = nombredelproducto;
            this.descripcion = descripcion_prod;
            this.tamanio = tamanio_prod;
            this.precio = precio_prod;
            this.stock = stock;
            this.cantidad = cantidad;
        }
        //getters y setters
        public int IdProducto { set { idProducto = value; } get { return idProducto; } }
        public string Nombre_producto { set { nombre_producto = value; } get { return nombre_producto; } }
        public string Descripcion { set { descripcion = value; } get { return descripcion; } }
        public string Tamanio { set { tamanio = value; } get { return tamanio; } }
        public double Precio { set { precio = value;  } get { return precio; } } 
        public int Cantidad { set { cantidad = value; } get { return cantidad; } }
        public int Stock { set { stock = value; } get { return stock; } }
        public Productos getProduct(string strConnection,int idProducto)
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
                        producto = new Productos(reader.GetString(1), reader.GetString(2), reader.GetString(3), Convert.ToDouble(reader.GetString(4)),0);
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
        public List<Productos> getAll(string strConnection)
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
                        productos.Add(new Productos(Convert.ToInt32(reader.GetString(0)),reader.GetString(1), reader.GetString(2), reader.GetString(3), Convert.ToDouble(reader.GetString(4)),Convert.ToInt32(reader.GetString(5)),0));
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
        public bool updateProduct(string strConnection, int idProducto, string descripcion, double precio, int stock)
        {
            //Sentencia
            string query = "UPDATE `productos` SET `DESCRIPCION`='"+descripcion+"',`PRECIO`="+precio+",`STOCK`="+stock+" WHERE ID_PRODUCTO = "+idProducto+"";
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