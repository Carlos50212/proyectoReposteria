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
    
    public partial class NewOrders : System.Web.UI.Page
    {
        private static string strConnection = "Server=localhost;Database=reposteria;Uid=gio;Pwd=270299GPS";

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string getAllNewOrders()
        {
            PedidosCliente pedido = new PedidosCliente(new PedidosClienteBD());
            List<PedidosCliente> pedidos = pedido.getOrders(strConnection,0);

            List<string> pedidosFinal = new List<string>();
            foreach(PedidosCliente pedidoTemp in pedidos)
            {
                pedidosFinal.Add("{\"id_pedido\":"+pedidoTemp.Id_pedido+", \"id_usuario\":" + pedidoTemp.Id_usuario+ ", \"fecha_entrega\":\"" + pedidoTemp.Fecha_entrega.ToString("dd/MM/yyyy")+ "\", \"fecha_creacion\":\"" + pedidoTemp.Fecha_creacion.ToString("dd/MM/yyyy") + "\", \"precio\":" + pedidoTemp.Precio+"}");
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
            Dictionary<int,string> productosId = productoPedido.getAllProducts(strConnection, Convert.ToInt32(idPedido));

            Productos productos = new Productos(new ProductosModel());
            Productos producto = new Productos();

            List<string> productosFinal = new List<string>();

            foreach (KeyValuePair<int, string> productoD in productosId){
                producto = productos.getProduct(strConnection, productoD.Key);

                productosFinal.Add("{\"nombre_producto\":\"" + producto.Nombre_producto + "\", \"descripcion\":\"" + producto.Descripcion + "\", \"tamanio\":\"" + producto.Tamanio + "\", \"cantidad\":" + productoD.Value + ", \"id_producto\":" + productoD.Key + "}");
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(productosFinal);

            return json;

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static bool deliverOrder(int idPedido)
        {
            PedidosCliente pedidoCliente = new PedidosCliente(new PedidosClienteBD());
            ProductosPedidos productosPedidos = new ProductosPedidos();
            Productos productoC = new Productos(new ProductosModel());
            Ventas ventas = new Ventas(new VentasBD());
            string fecha = DateTime.Now.ToString("yyyy-MM-dd");
            double priceUnit = 0;
            bool isSaved = false;
            bool isNew = true;

            pedidoCliente.deliverOrder(strConnection, idPedido);
            PedidosCliente pedido = pedidoCliente.getOrder(strConnection, idPedido);
            List<ProductosPedidos> productos = productosPedidos.getProductOrder(strConnection, pedido.Id_pedido);
            List<Ventas> sellsPerDay = ventas.getAllSellsDayWD(fecha, strConnection);


            foreach(ProductosPedidos producto in productos)
            {
                Productos prod = productoC.getProduct(strConnection, producto.Id_producto);
                string subcadenaPastel = prod.Nombre_producto.Substring(0, 6);
                string subcadenaPaq = prod.Nombre_producto.Substring(0, 7);
                priceUnit = prod.Precio;
                if (subcadenaPastel == "Pastel" && prod.Tamanio == "Grande")
                {
                    priceUnit = prod.Precio - (prod.Precio * .25);
                }
                else
                {
                    if (subcadenaPaq == "Paquete")
                    {
                        priceUnit = prod.Precio - (prod.Precio * .50);
                    }
                }

                foreach (Ventas venta in sellsPerDay)
                {
                    if(producto.Id_producto == venta.IdProduct)
                    {
                        int total = producto.Cantidad + venta.Unidades;
                        double totalPrice = venta.Total + (priceUnit * producto.Cantidad);
                        ventas.updateProduct(producto.Id_producto,fecha,total,totalPrice, strConnection);
                        isNew = false;
                    }
                }
                if (isNew)
                {
                    double totalPriceNew = priceUnit * producto.Cantidad;
                    ventas.newProduct(producto.Id_producto, fecha, producto.Cantidad, totalPriceNew, strConnection);
                }
            }
            isSaved = true;
            return isSaved; 

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static bool cancelOrder(int idPedido)
        {
            PedidosCliente pedidoCliente = new PedidosCliente(new PedidosClienteBD());

            bool isCancelled = pedidoCliente.cancelOrder(strConnection, idPedido);

            return isCancelled;
        }

    }
}