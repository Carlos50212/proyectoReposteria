using System;
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
    }
}