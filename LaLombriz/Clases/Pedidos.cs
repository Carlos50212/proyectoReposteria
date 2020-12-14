﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaLombriz.Clases
{
    public class Pedidos
    {
        private int id_pedido;
        private int id_usuario;
        private DateTime fecha_entrega;
        private DateTime fecha_creacion;

        //constructor vacío
        public Pedidos() { }
        //constructor sobrecargado
        public Pedidos(int id_del_pedido, int id_del_usuario, DateTime fecha_de_entrega, DateTime fecha_de_creacion)
        {
            this.id_pedido = id_del_pedido;
            this.id_usuario = id_del_usuario;
            this.fecha_creacion = fecha_de_creacion;
            this.fecha_entrega = fecha_de_entrega;
        }
        //getters y setters
        public int Id_pedido { set { id_pedido = value; } get { return id_pedido; } }
        public int Id_usuario { set { id_usuario = value; } get { return id_usuario; } }
        public DateTime Fecha_entrega { set { fecha_entrega = value; } get { return fecha_entrega; } }
        public DateTime Fecha_creacion { set { fecha_creacion = value; } get { return fecha_creacion; } }

    }
}