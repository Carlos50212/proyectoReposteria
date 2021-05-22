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
    public partial class Inventarios : System.Web.UI.Page
    {
        private static string strConnection = "Server=localhost;Database=reposteria;Uid=gio;Pwd=270299GPS";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string getInventario()
        {
            Productos producto = new Productos(new ProductosModel());

            List<Productos> productos = producto.getAll(strConnection);


            List<string> productosFinal = new List<string>();
            foreach (Productos productoTmp in productos)
            {
                productosFinal.Add("{\"id_producto\":" + productoTmp.IdProducto + ", \"nombre\":\"" + productoTmp.Nombre_producto + "\", \"descripcion\":\"" +productoTmp.Descripcion + "\", \"tamanio\":\"" + productoTmp.Tamanio + "\", \"precio\":" + productoTmp.Precio + ", \"stock\":" + productoTmp.Stock + "}");
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(productosFinal);

            return json;
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static bool updateProduct(string idProducto,string descripcion, string precio,string disponible)
        {
            Productos producto = new Productos();

            bool isUpdated = producto.updateProduct(strConnection, Convert.ToInt32(idProducto), descripcion, Convert.ToDouble(precio), Convert.ToInt32(disponible));

            return isUpdated;
        }
    }
}