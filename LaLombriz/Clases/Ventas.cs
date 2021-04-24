using LaLombriz.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaLombriz.Clases
{
    public class Ventas
    {
        private int idProducto;
        private int unidades;
        private double total;
        private DateTime fecha;

        private VentasBD ventasDB;

        public Ventas() { }
        public Ventas(VentasBD ventasBD)
        {
            this.ventasDB = ventasBD;
        }
        public Ventas(int idProducto, int unidades, double total, DateTime fecha)
        {
            this.idProducto = idProducto;
            this.unidades = unidades;
            this.total = total;
            this.fecha = fecha; 
        }

        public int IdProduct { set { idProducto = value; } get { return idProducto; } }
        public int Unidades { set { unidades = value; } get { return unidades; } }
        public double Total { set { total = value; } get { return total; } }
        public DateTime Fecha { set { fecha = value; } get { return fecha; } }

        public virtual List<Ventas> getAllSellsDay(DateTime fecha, string strConnection)
        {
            List<Ventas> ventas = this.ventasDB.getAllSellsDayModel(fecha, strConnection);
            return ventas;
        }
        public virtual List<Ventas> getAllSellsMonth(string strConnection,string date)
        {
            List<Ventas> ventas = this.ventasDB.getAllSellsMonthModel(strConnection, date);
            return ventas;
        }

    }
}