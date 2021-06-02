using LaLombriz.Modelos;
using System;
using System.Collections.Generic;


namespace LaLombriz.Clases
{
    public class PedidosCliente
    {
        private int id_pedido;
        private int id_usuario;
        private DateTime fecha_entrega;
        private DateTime fecha_creacion;
        private double precio;
        private PedidosClienteBD pedidosClienteBD;

        //constructor vacío
        public PedidosCliente() { }
        public PedidosCliente(PedidosClienteBD pedidosClienteBD)
        {
            this.pedidosClienteBD = pedidosClienteBD;
        }
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

        public virtual List<PedidosCliente> getOrders(string strConnection,int estatus)
        {
            List<PedidosCliente> pedidos = this.pedidosClienteBD.getOrdersModel(strConnection, estatus);
            return pedidos;
        }
        public virtual PedidosCliente getOrder(string strConnection,int idPedido)
        {
            PedidosCliente pedidos = this.pedidosClienteBD.getOrderModel(strConnection, idPedido);
            return pedidos;
        }
        public virtual bool deliverOrder(string strConnection,int idOrder)
        {
            return this.pedidosClienteBD.deliverOrderModel(strConnection, idOrder);
        }

        public virtual bool cancelOrder(string strConnection, int idOrder)
        {
            return this.pedidosClienteBD.cancelOrderModel(strConnection, idOrder);
        }
    }
}