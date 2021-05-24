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
    public partial class CancelOrders : System.Web.UI.Page
    {
        private static string strConnection = "Server=localhost;Database=reposteria;Uid=gio;Pwd=270299GPS";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string getAllCancelOrders()
        {
            PedidosCliente pedido = new PedidosCliente(new PedidosClienteBD());
            List<PedidosCliente> pedidos = pedido.getOrders(strConnection,2);

            List<string> pedidosFinal = new List<string>();
            foreach (PedidosCliente pedidoTemp in pedidos)
            {
                pedidosFinal.Add("{\"id_pedido\":" + pedidoTemp.Id_pedido + ", \"id_usuario\":" + pedidoTemp.Id_usuario + ", \"fecha_entrega\":\"" + pedidoTemp.Fecha_entrega.ToString("dd/MM/yyyy") + "\", \"fecha_creacion\":\"" + pedidoTemp.Fecha_creacion.ToString("dd/MM/yyyy") + "\", \"precio\":" + pedidoTemp.Precio + "}");
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(pedidosFinal);

            return json;
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static string getAllProducts(string idPedido)
        {
            ProductosPedidos productoPedido = new ProductosPedidos();
            Dictionary<int, string> productosId = productoPedido.getAllProducts(strConnection, Convert.ToInt32(idPedido));

            Productos productos = new Productos(new ProductosModel());
            Productos producto = new Productos();

            List<string> productosFinal = new List<string>();

            foreach (KeyValuePair<int, string> productoD in productosId)
            {
                producto = productos.getProduct(strConnection, productoD.Key);

                productosFinal.Add("{\"nombre_producto\":\"" + producto.Nombre_producto + "\", \"descripcion\":\"" + producto.Descripcion + "\", \"tamanio\":\"" + producto.Tamanio + "\", \"cantidad\":" + productoD.Value + ", \"id_producto\":" + productoD.Key + "}");
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(productosFinal);

            return json;

        }

    }
}