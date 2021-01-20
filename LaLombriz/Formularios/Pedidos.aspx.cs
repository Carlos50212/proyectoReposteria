using System;
using System.Collections;
using LaLombriz.Clases;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Services;
using iTextSharp.text;

namespace LaLombriz.Formularios
{
    public partial class Pedidos : System.Web.UI.Page
    {
        private static string strConnection = "Server=localhost;Database=reposteria;Uid=gio;Pwd=270299GPS";
        public static ArrayList listNewOrders = new ArrayList();
        public static ArrayList listOldOrders = new ArrayList();
        public static ProductosPedidos pedidoContenido;
        public static int idNewOrder;
        public static bool isNewOrderSelected = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["CORREO_USUARIO"] != null)
            {
                listNewOrders = getOrdersClient(getIDUser(Session["CORREO_USUARIO"].ToString()), 0);
                listOldOrders = getOrdersClient(getIDUser(Session["CORREO_USUARIO"].ToString()), 1);
            }
            if (!IsPostBack)
            {
                lkNew.CssClass += " option-selected";
                if (listNewOrders.Count != 0)
                {
                    drawInterfaceNewOrder();
                }
                else
                {
                    notNewOrders.Style["display"] = "flex";
                }
            }
        }
        public int getIDUser(string correo)
        {
            int id=0;
            string query = "SELECT ID_USUARIO FROM usuarios where CORREO='" + correo +"'";
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;
            MySqlDataReader reader;
            try
            {
                dbConnection.Open();
                //Leemos los datos 
                reader = cmdDB.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read()) //asignamos datos 
                    {
                        id = Convert.ToInt32(reader.GetString(0));
                    }
                }
                dbConnection.Close();
                return id;
            }
            catch (Exception e)
            {
                //Mensjae de error
                Console.WriteLine("Error" + e);
                return 0;
            }
        }
        public void lkOldOrdersOnClick(object sender, EventArgs args)
        {
            //Se valida si el div que muestra los detalles de los pedidos recientes está visible
            if (detailOrder.Style["display"] == "flex")
            {
                //Se oculta
                detailOrder.Style["display"] ="none";
            }
            else
            {
                //Se valida si el div que muestra todos los pedidos recientes está visible
                if (newOrders.Style["display"] == "flex")
                {
                    //Se oculta
                    newOrders.Style["display"] = "none";
                }
                else
                {
                    //Se oculta div que muestra mensaje de no tienes pedidos recientes
                    notNewOrders.Style["display"] = "none";
                }
            }
            lkNew.CssClass = "lkStyles";
            lkOld.CssClass += " option-selected";
            drawInterfaceOldOrder();
        }
        public void lkNewOrdersOnClick(object sender,EventArgs args)
        {
            notOldOrders.Style["display"] = "none";
            oldOrders.Style["display"] = "none";
            lkOld.CssClass = "lkStyles";
            lkNew.CssClass += " option-selected";
            drawInterfaceNewOrder();
        }
        //Metodo para traer que pedido se verá
        public void seeDetailsOnClick(object sender, EventArgs args)
        {
            string idOrder = Request.Form["hiddenIdDetailsOrder"];
            drawInterfaceDetailOrder(idOrder,0);

        }
        //Metodo para traer que pedido se elimininará
        public void deleteOrderOnClick(object sender,EventArgs args)
        {
            string idOrder = Request.Form["hiddenIdDeleteOrder"];
            Response.Write("PEDIDO A ELIMINAR: "+idOrder);
        }
        //Metodo para traer que pedido entregado se verá
        public void seeDetailsOldOrderOnClick(object sender, EventArgs args)
        {
            string idOrder = Request.Form["hiddenIdDetailOldOrder"];
            drawInterfaceDetailOrder(idOrder, 1);
        }
        //Metodo para crear y descargar pdf
        public void downloadPDFOnClick(object sender, EventArgs args)
        {
            archivoPDF pdfFile = new archivoPDF(pedidoContenido);
            pdfFile.createPDF();
        }
        //Metodo para dibujar los pedidos recientes que tiene el usuario
        public void drawInterfaceNewOrder()
        {
            //Se valida si el div de "no hay pedidos" esta visible y si sigue sin haber pedidos
            if(notNewOrders.Style["display"] == "flex" && listNewOrders.Count>0)
            {
                //Se oculta
                notNewOrders.Style["display"] = "none";
            }
            //Se valida si el div de no hay pedidos no está visible y si sigue sin haber pedidos
            if(notNewOrders.Style["display"]=="none" && listNewOrders.Count==0)
            {
                //Se muestra
                notNewOrders.Style["display"] = "flex";
            }
            //Se valida si el div donde se muestran los pedidos de cada producto está visible
            if (detailOrder.Style["display"] == "flex")
            {
                //Se oculta
                detailOrder.Style["display"] = "none";
            }
            //Se valida si el div de pedidos esta oculto
            if (newOrders.Style["display"] == "none" && listNewOrders.Count > 0)
            {
                //Se muestra y se dibuja
                newOrders.Style["display"] = "flex";

                StringBuilder sb = new StringBuilder();
                foreach (PedidosCliente pedido in listNewOrders)
                {
                    sb.Append("<div id='" + pedido.Id_pedido + "Order'  class='oneOrder'>");
                    sb.Append("<div class='containerOptionsNewOrders'>");
                    sb.Append("<div class='title'>");
                    sb.Append("<b>Número de pedido: </b><span style='color:#757575;'>" + pedido.Id_pedido + "</span>");
                    sb.Append("</div>");
                    sb.Append("<div class='dropdown'>");
                    sb.Append("<button class='btnNewOptions' type='button' id='dropdownMenuButton' data-bs-toggle='dropdown' aria-expanded='false'><img src='../Recursos/menuOptions.png' alt='options' class='imgDotOptions'/></button>");
                    sb.Append("<div class='dropdown-menu' aria-labelledby='dropdownMenuButton'>");
                    sb.Append("<a id='" + pedido.Id_pedido + "_Detalles' class='dropdown-item' onclick='onClickDetails(this);'>Detalles<img src='../Recursos/seeDetails.png' alt='details'  class='optionsImages'/>");
                    sb.Append("<a id='" + pedido.Id_pedido + "_Eliminar' class='dropdown-item' onclick='onClickDelete(this)'>Eliminar<img src='../Recursos/delete.png' alt='delete' class='optionsImages'/></a>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("<div class='tableInformation'>");
                    sb.Append("<table class='table table-borderless tableNewOrder'>");
                    sb.Append("<thead>");
                    sb.Append("<tr>");
                    sb.Append("<th scope='col'>Fecha de creación</th><th scope='col'>Fecha de entrega</th><th scope='col'>Precio</th>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");
                    sb.Append("<tbody>");
                    sb.Append("<tr>");
                    sb.Append("<td>" + pedido.Fecha_creacion.ToString("dd/MM/yyyy") + "</td><td>" + pedido.Fecha_entrega.ToString("dd/MM/yyyy") + "</td><td>$" + pedido.Precio + "</td>");
                    sb.Append("</tr>");
                    sb.Append("</tbody>");
                    sb.Append("</table>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    tbNewOrders.Text = sb.ToString();
                }
            }
        }
        //Metodo para dibujar la interfaz de los pedidos ya entregados 
        public void drawInterfaceOldOrder()
        {
            //Se valida si el div de "no hay pedidos" esta visible y si sigue sin haber pedidos
            if (notOldOrders.Style["display"] == "flex" && listOldOrders.Count > 0)
            {
                //Se oculta
                notOldOrders.Style["display"] = "none";
            }
            //Se valida si el div de no hay pedidos no está visible y si sigue sin haber pedidos
            if (notOldOrders.Style["display"] == "none" && listOldOrders.Count == 0)
            {
                //Se muestra
                notOldOrders.Style["display"] = "flex";
            }
            //Se valida si el div donde se muestran los pedidos de cada producto está visible
            if (detailOrder.Style["display"] == "flex")
            {
                //Se oculta
                detailOrder.Style["display"] = "none";
            }
            //Se valida si el div de pedidos esta oculto
            if (oldOrders.Style["display"] == "none" && listNewOrders.Count > 0)
            {
                //Se muestra y se dibuja
                oldOrders.Style["display"] = "flex";

                StringBuilder sb = new StringBuilder();
                foreach (PedidosCliente pedido in listOldOrders)
                {
                    sb.Append("<div id='" + pedido.Id_pedido + "Order'  class='oneOrder'>");
                    sb.Append("<div class='containerOptionsNewOrders'>");
                    sb.Append("<div class='title'>");
                    sb.Append("<b>Número de pedido: </b><span style='color:#757575;'>" + pedido.Id_pedido + "</span>");
                    sb.Append("</div>");
                    sb.Append("<div class='dropdown'>");
                    sb.Append("<button class='btnNewOptions' type='button' id='dropdownMenuButton' data-bs-toggle='dropdown' aria-expanded='false'><img src='../Recursos/menuOptions.png' alt='options' class='imgDotOptions'/></button>");
                    sb.Append("<div class='dropdown-menu' aria-labelledby='dropdownMenuButton'>");
                    sb.Append("<a id='" + pedido.Id_pedido + "_Detalles' class='dropdown-item' onclick='onClickOldDetails(this);'>Detalles<img src='../Recursos/seeDetails.png' alt='details'  class='optionsImages'/></a>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("<div class='tableInformation'>");
                    sb.Append("<table class='table table-borderless tableNewOrder'>");
                    sb.Append("<thead>");
                    sb.Append("<tr>");
                    sb.Append("<th scope='col'>Fecha de creación</th><th scope='col'>Fecha de entrega</th><th scope='col'>Precio</th>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");
                    sb.Append("<tbody>");
                    sb.Append("<tr>");
                    sb.Append("<td>" + pedido.Fecha_creacion.ToString("dd/MM/yyyy") + "</td><td>" + pedido.Fecha_entrega.ToString("dd/MM/yyyy") + "</td><td>$" + pedido.Precio + "</td>");
                    sb.Append("</tr>");
                    sb.Append("</tbody>");
                    sb.Append("</table>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    tbOldOrders.Text = sb.ToString();
                }
            }
        }
        //Metodo para dibujar la interface con los detalles de la orden
        public void drawInterfaceDetailOrder(string idOrder,int status)
        {
            int contProduct = 1;
            string valueStatus = status == 0? "Por entregar" : "Entregados";
            if (status == 0)
            {
                //Se oculta div que muestra todos los pedidos actuales
                newOrders.Style["display"] = "none";
                //Se muestra div que enseña los productos de ese pedido
                detailOrder.Style["display"] = "flex";
            }
            else
            {
                //Se oculta div que muestra todos los pedidos entregados
                oldOrders.Style["display"] = "none";
                //Se muestra div que enseña los productos de ese pedido
                detailOrder.Style["display"] = "flex";
            }
            getAllOrderInfo(idOrder);
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='row productAllInfoContainer'>");
            sb.Append("<p style='color: #838383;'>"+valueStatus+" > Detalles pedido #" + pedidoContenido.Pedido.Id_pedido + "</p>");
            //Contenedor de productos
            sb.Append("<div class='col-xs-12 col-md-6 productsContainer'>");
            //Contenedor de cada producto, traemos cada producto
            foreach (Productos producto in pedidoContenido.Productos)
            {
                string nameImage = producto.Nombre_producto.Replace(" ", "_");
                sb.Append("<div class='singleProduct'>");
                sb.Append("<div class='headerSingleProduct'>");
                sb.Append("<p>Producto " + contProduct + " de " + pedidoContenido.Productos.Count + "</p>");
                sb.Append("</div>");
                sb.Append("<div class='informationProductContainer row'>");
                //Contenedor imagen
                sb.Append("<div class='imageProduct col-xs-6' style='background-image:url(../Recursos/Menu/" + nameImage + ".jpg);'>");
                sb.Append("</div>");
                //Contenedor información de producto
                sb.Append("<div class='dataContainer col-xs-6'>");
                sb.Append("<h6>Producto</h6>");
                sb.Append("<p>" + producto.Nombre_producto + "</p>");
                sb.Append("<h6>Descripción</h6>");
                sb.Append("<p>" + producto.Descripcion + "</p>");
                sb.Append("<h6>Precio unitario</h6>");
                sb.Append("<p> $" + producto.Precio+"</p>");
                sb.Append("<h6>Cantidad</h6>");
                sb.Append("<p>" + producto.Cantidad + "</p>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                contProduct++;
            }
            sb.Append("</div>");
            //Contenedor información del pedido
            sb.Append("<div class='col-xs-12 col-md-6'>");
            //Contenedor de resumen del pedido
            sb.Append("<div class='detailOrderContainer'>");
            sb.Append("<div class='headerDataProduct'>");
            sb.Append("<h5>Resumen del pedido</h5>");
            sb.Append("</div>");
            sb.Append("<div class='detailContainer'>");
            sb.Append("<table style='width:100%;'>");
            sb.Append("<tr>");
            sb.Append("<td style='text-align: right;'>Total:</td><td>&nbsp$" + pedidoContenido.Pedido.Precio+"</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("</div>");
            sb.Append("</div>");
            //Contenedor de información del cliente
            sb.Append("<div class='detailOrderContainer'>");
            sb.Append("<div class='headerDataProduct'>");
            sb.Append("<h5>Información del cliente</h5>");
            sb.Append("</div>");
            sb.Append("<div class='detailContainer'>");
            sb.Append("<table style='width:100%;'>");
            sb.Append("<tr>");
            sb.Append("<td style='text-align: right;'>Nombre del cliente: </td><td>&nbsp" + pedidoContenido.Usuario.Nombre + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style='text-align: right;'>Correo: </td><td>&nbsp" + pedidoContenido.Usuario.Correo + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style='text-align: right;'>Teléfono: </td><td>&nbsp" + pedidoContenido.Usuario.Telefono + "</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("</div>");
            sb.Append("</div>");
            //Contenedor de nota
            if(status == 0){
                sb.Append("<div class='detailOrderContainer'>");
                sb.Append("<div class='headerDataProduct'>");
                sb.Append("<h5>NOTA</h5>");
                sb.Append("</div>");
                sb.Append("<div class='noteContainer'>");
                sb.Append("<p>Cualquier duda o aclaración sobre su pedido, favor de ponerse en contacto con nosotros mediante nuestras redes sociales, correo electrónico o teléfono.</p>");
                sb.Append("<p>Los pedidos pueden ser cancelados o modificados con un máximo de 10 días antes de la fecha de entrega.</p>");
                sb.Append("<p>Tenga su número de pedido a la mano.</p>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("<div class='downloadButtonContainer'>");
                sb.Append("<a id='btnDownload' onclick='downloadOption();' class='btn btn-primary'>Descargar PDF<img src='../Recursos/download.png' alt='descargar'  class='optionsDownload'/></a>");
                sb.Append("</div>");
            }
            sb.Append("</div>");
            sb.Append("</div>");
            tbOrderDetails.Text = sb.ToString();

        }
        //Metodo para obtener toda la información del pedido
        public void getAllOrderInfo(string idOrder)
        {
            PedidosCliente pedido = getDetailsOrder(idOrder);
            Usuario usuario = getUserInformation(pedido.Id_usuario);
            Dictionary<int,int> productosTemporal = getQuantity(pedido.Id_pedido);
            ArrayList producto = new ArrayList();
            foreach(KeyValuePair<int,int> productoTemporal in productosTemporal)
            {
                producto.Add(getProductDetails(productoTemporal.Key,productoTemporal.Value));
            }
            pedidoContenido = new ProductosPedidos(pedido, usuario, producto);
        }
        //Metodo para traer los pedidos de los clientes
        public ArrayList getOrdersClient(int idUser,int status)
        {
            string query = "SELECT ID_PEDIDO,FECHA_ENTREGA,FECHA_CREACION,PRECIO FROM `pedidos` WHERE (id_usuario=" + idUser + " AND estatus=" +status+")";
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;
            MySqlDataReader reader;
            ArrayList ordersUser = new ArrayList();
            try
            {
                dbConnection.Open();
                //Leemos los datos 
                reader = cmdDB.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ordersUser.Add(new PedidosCliente(Convert.ToInt32(reader.GetString(0)),idUser,DateTime.Parse(reader.GetString(1)), DateTime.Parse(reader.GetString(2)), Convert.ToDouble(reader.GetString(3))));
                    }
                }
                dbConnection.Close();
                return ordersUser;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: " + e);
                return ordersUser;
            }
        }
        //Metodo para traer detalles generales del pedido seleccionado
        public PedidosCliente getDetailsOrder(string idOrder)
        {
            string query = "SELECT ID_USUARIO,FECHA_ENTREGA,FECHA_CREACION,PRECIO FROM `pedidos` WHERE ID_PEDIDO="+Convert.ToInt32(idOrder)+"";
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;
            MySqlDataReader reader;
            PedidosCliente pedido = new PedidosCliente();
            try
            {
                dbConnection.Open();
                //Leemos los datos 
                reader = cmdDB.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        pedido=new PedidosCliente(Convert.ToInt32(idOrder),Convert.ToInt32(reader.GetString(0)), DateTime.Parse(reader.GetString(1)), DateTime.Parse(reader.GetString(2)), Convert.ToDouble(reader.GetString(3)));
                    }
                }
                dbConnection.Close();
                return pedido;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                return pedido;
            }
        }
        //Metodo para traer toda la información respecto al usuario
        public Usuario getUserInformation(int idUser)
        {
            string query = "SELECT NOMBRE_USUARIO,CORREO,TELEFONO FROM `usuarios` WHERE ID_USUARIO=" + idUser + "";
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;
            MySqlDataReader reader;
            Usuario usuario = new Usuario();
            try
            {
                dbConnection.Open();
                //Leemos los datos 
                reader = cmdDB.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        usuario = new Usuario(reader.GetString(0),reader.GetString(1),"",reader.GetString(2));
                    }
                }
                dbConnection.Close();
                return usuario;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                return usuario;
            }
        }
        //Metodo para traer los id  de los productos y sus cantidades de cada pedido
        public Dictionary<int,int> getQuantity(int idOrder)
        {
            string query = "SELECT ID_PRODUCTO,CANTIDAD FROM `productos_pedido` WHERE ID_PEDIDO=" + idOrder + "";
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;
            MySqlDataReader reader;
            Dictionary<int, int> productosTemporal = new Dictionary<int, int>();
            try
            {
                dbConnection.Open();
                //Leemos los datos 
                reader = cmdDB.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        productosTemporal.Add(Convert.ToInt32(reader.GetString(0)), Convert.ToInt32(reader.GetString(1)));
                    }
                }
                dbConnection.Close();
                return productosTemporal;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                return productosTemporal;
            }
        }
        //Metodo para traer la informacion de cada producto
        public Productos getProductDetails(int idProduct,int quantity)
        {
            string query = "SELECT NOMBRE_PRODUCTO,DESCRIPCION,TAMANIO,PRECIO FROM `productos` WHERE ID_PRODUCTO=" + idProduct + "";
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;
            MySqlDataReader reader;
            Productos producto = new Productos();
            try
            {
                dbConnection.Open();
                //Leemos los datos 
                reader = cmdDB.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        producto = new Productos(reader.GetString(0),reader.GetString(1),reader.GetString(2),Convert.ToDouble(reader.GetString(3)),quantity);
                    }
                }
                dbConnection.Close();
                return producto;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                return producto;
            }
        }

    }
}