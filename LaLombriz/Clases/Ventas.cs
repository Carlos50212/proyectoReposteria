using LaLombriz.Modelos;
using System;
using System.Collections.Generic;

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

        public virtual List<Ventas> getAllSellsDay(string fecha, string strConnection)
        {
            List<Ventas> ventas = this.ventasDB.getAllSellsDayModel(fecha, strConnection);
            return ventas;
        }
        public virtual List<Ventas> getAllSellsDayWD(string fecha, string strConnection)
        {
            List<Ventas> ventas = this.ventasDB.getAllSellsDayModelWD(fecha, strConnection);
            return ventas;
        }
        public virtual List<Ventas> getAllSellsMonth(string strConnection,string date)
        {
            List<Ventas> ventas = this.ventasDB.getAllSellsMonthModel(strConnection, date);
            return ventas;
        }

        public virtual bool updateProduct(int idProducto,string fecha, int total, double totalPrice, string strConnection)
        {

            return this.ventasDB.updateProductModel(idProducto, fecha, total, totalPrice, strConnection);
        }

        public virtual bool newProduct(int idProducto, string fecha, int total, double totalPrice, string strConnection)
        {

            return this.ventasDB.newProductModel(idProducto, fecha, total, totalPrice, strConnection);
        }

    }
}