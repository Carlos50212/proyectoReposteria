using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaLombriz.Clases
{
    public class Productos
    {
        private int id;
        private string nombre_producto;
        private string descripcion;
        private string tamanio;
        private float precio;

        //Constructor vacío
        public Productos() { }
        //Constructor sobrecargado
        public Productos(int identificador, string nombredelproducto, string descripcion_prod, string tamanio_prod, float precio_prod)
        {
            this.id = identificador;
            this.nombre_producto = nombredelproducto;
            this.descripcion = descripcion_prod;
            this.tamanio = tamanio_prod;
            this.precio = precio_prod;
        }
        //getters y setters
        public int Id { set { id = value; } get { return id; } }
        public string Nombre_producto { set { nombre_producto = value; } get { return nombre_producto; } }
        public string Descripcion { set { descripcion = value; } get { return descripcion; } }
        public string Tamanio { set { tamanio = value; } get { return tamanio; } }
        public float Precio { set { precio = value;  } get { return precio; } }
        //Metodos 
        public ArrayList getProductsName(string strConnection)
        {
            string query = "SELECT nombre_producto FROM `productos` WHERE tamanio='chico' and (id_producto>0 AND ID_PRODUCTO<16)";
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;
            MySqlDataReader reader;
            ArrayList arrayObjects = new ArrayList();
            try
            {
                dbConnection.Open();
                //Leemos los datos 
                reader = cmdDB.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        arrayObjects.Add(reader.GetString(0));
                    }
                }
                else
                {
                    //Mensaje de error al usuario
                    Console.WriteLine("No hay datos en la tabla");
                }
                dbConnection.Close();
                return arrayObjects;
            }
            catch(Exception e)
            {
                //Mensjae de error
                Console.WriteLine("Error");
                return arrayObjects;
            }
        }
    }
}