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
        private string nombre_producto;
        private string descripcion;
        private string tamanio;
        private double precio;
        private int cantidad;

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
        //getters y setters
        public string Nombre_producto { set { nombre_producto = value; } get { return nombre_producto; } }
        public string Descripcion { set { descripcion = value; } get { return descripcion; } }
        public string Tamanio { set { tamanio = value; } get { return tamanio; } }
        public double Precio { set { precio = value;  } get { return precio; } } 
        public int Cantidad { set { cantidad = value; } get { return cantidad; } }
    }
}