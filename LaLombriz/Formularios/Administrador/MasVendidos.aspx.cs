using LaLombriz.Clases;
using LaLombriz.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LaLombriz.Formularios.Administrador
{
    public partial class MasVendidos : System.Web.UI.Page
    {
        private static string strConnection = "Server=localhost;Database=reposteria;Uid=gio;Pwd=270299GPS";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string getAllSells()
        {
            //int sizeDay = 3;
            //int sizeMonth = 3;
            string fecha = DateTime.Now.ToString("yyyy-MM-dd");
            Ventas ventas = new Ventas(new VentasBD());
            Ventas ventaTmp = new Ventas();
            Productos productos = new Productos(new ProductosModel());
            string date = DateTime.Now.ToString("yyyy-MM-dd");

            List<Ventas> ventasMonth = ventas.getAllSellsMonth(strConnection, date);
            List<Ventas> ventasDay = ventas.getAllSellsDay(fecha, strConnection);

            //List<Ventas> sellPerMonth = new List<Ventas>();
            //List<Ventas> sellPerDay = new List<Ventas>();

            List<string> productosDay = new List<string>();
            List<string> productosMonth = new List<string>();

            List<string> productosFinal = new List<string>();

            /*
            if (ventasDay.Count < 3)
            {
                sizeDay = ventasDay.Count;
            }

            for(int i = sizeDay; i >0; i++)
            {
                int cantidadDia = 0;
                int position = 0;
                if (ventasDay.Count > 0)
                {
                    foreach (Ventas venta in ventasDay)
                    {
                        if (venta.Unidades >= cantidadDia)
                        {
                            ventaTmp = venta;
                            cantidadDia = venta.Unidades;
                            position++;
                        }
                    }
                    sellPerDay.Add(ventaTmp);
                    ventasDay.RemoveAt(position - 1);
                }
            }

            if (ventasMonth.Count < 3)
            {
                sizeMonth = ventasMonth.Count;
            }

            for (int i = sizeMonth; i>0; i++)
            {
                int cantidadMes = 0;
                int position = 0;
                if (ventasMonth.Count > 0)
                {
                    foreach (Ventas venta in ventasMonth)
                    {
                        if (venta.Unidades >= cantidadMes)
                        {
                            ventaTmp = venta;
                            cantidadMes = venta.Unidades;
                            position++;
                        }
                    }
                    sellPerMonth.Add(ventaTmp);
                    ventasMonth.RemoveAt(position - 1);
                }
            }*/

            for(int i = 0; i < ventasDay.Count; i++)
            {
                Productos productoDia = productos.getProduct(strConnection, ventasDay[i].IdProduct);
                productosDay.Add("{\"id_producto\":" + ventasDay[i].IdProduct + ", \"nombre\":\"" + productoDia.Nombre_producto + "\", \"unidades\":" + ventasDay[i].Unidades + ", \"fecha\":\"" + ventasDay[i].Fecha.ToString("dd/MM/yyyy") + "\", \"total\":" + ventasDay[i].Total + "}");
            }

            for(int i = 0; i < ventasMonth.Count; i++)
            {
                Productos productoMes = productos.getProduct(strConnection, ventasMonth[i].IdProduct);
                productosMonth.Add("{\"id_producto\":" + ventasMonth[i].IdProduct + ", \"nombre\":\"" + productoMes.Nombre_producto + "\", \"unidades\":" + ventasMonth[i].Unidades + ", \"fecha\":\"" + ventasMonth[i].Fecha.ToString("dd/MM/yyyy") + "\", \"total\":" + ventasMonth[i].Total + "}");
            }

            var jsonSerialiser = new JavaScriptSerializer();

            var productsDayFinal = jsonSerialiser.Serialize(productosDay);
            var productsMonthFinal = jsonSerialiser.Serialize(productosMonth);

            productosFinal.Add("{\"productosDia\":"+ productsDayFinal + ",\"productosMes\":"+ productsMonthFinal + "}");


            var json = jsonSerialiser.Serialize(productosFinal);

            return json;
        }
    }
}