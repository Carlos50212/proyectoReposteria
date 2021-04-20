using LaLombriz.Clases;
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
    public partial class VentasMes : System.Web.UI.Page
    {
        private static string strConnection = "Server=localhost;Database=reposteria;Uid=gio;Pwd=270299GPS";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string getAllSellsMonth()
        {
            Ventas ventas = new Ventas();
            Productos productos = new Productos();
            string date = DateTime.Now.ToString("yyyy-MM-dd");

            List<Ventas> ventasMonth = ventas.getAllSellsMonth(strConnection,date);
            List<string> productosFinal = new List<string>();


            foreach (Ventas venta in ventasMonth)
            {
                Productos producto = productos.getProduct(strConnection, venta.IdProduct);
                productosFinal.Add("{\"id_producto\":" + venta.IdProduct + ", \"nombre\":\"" + producto.Nombre_producto + "\", \"unidades\":" + venta.Unidades + ", \"fecha\":\"" + venta.Fecha.ToString("dd/MM/yyyy") + "\", \"total\":" + venta.Total + "}");
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(productosFinal);

            return json;
        }
    }
}