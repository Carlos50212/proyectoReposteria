using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace LaLombriz.Clases
{
    public class ProductosPedidos
    {
        private Usuario usuarios;
        private Productos[] producto;

        public ProductosPedidos() { }
        public ProductosPedidos(Usuario usuarios,Productos[] producto)
        {
            this.usuarios = usuarios;
            this.producto = producto;
        }
        public Usuario Usuarios{ set { this.usuarios = value; } get{ return usuarios; }}
        public Productos[] Producto{ set { this.producto = value;} get { return producto; }}
    }
}