using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace LaLombriz.Clases
{
    public class ProductosPedidos
    {
        private PedidosCliente pedido;
        private Usuario usuario;
        private ArrayList productos;

        public ProductosPedidos() { }
        public ProductosPedidos(PedidosCliente pedido,Usuario usuario,ArrayList productos)
        {
            this.pedido = pedido;
            this.usuario = usuario;
            this.productos = productos;
        }
        public PedidosCliente Pedido { set { this.pedido = value; } get { return pedido; } }
        public Usuario Usuario{ set { this.usuario = value; } get{ return usuario; }}
        public ArrayList Productos{ set { this.productos = value;} get { return productos; }}
    }
}