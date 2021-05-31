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
    public partial class VentasDia : System.Web.UI.Page
    {
        private static string strConnection = "Server=localhost;Database=reposteria;Uid=gio;Pwd=270299GPS";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string getAllSellsDay()
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd");
            Ventas ventas = new Ventas(new VentasBD());
            Productos productos = new Productos(new ProductosModel());

            List<Ventas> ventasDay = ventas.getAllSellsDay(fecha,strConnection);
            List<string> productosFinal = new List<string>();


            foreach (Ventas venta in ventasDay)
            {
                Productos producto = productos.getProduct(strConnection,venta.IdProduct);
                productosFinal.Add("{\"id_producto\":" + venta.IdProduct + ", \"nombre\":\"" + producto.Nombre_producto + "\", \"unidades\":" + venta.Unidades + ", \"fecha\":\"" + venta.Fecha.ToString("dd/MM/yyyy") + "\", \"total\":" + venta.Total + "}");
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(productosFinal);

            return json;
        }
    }
}