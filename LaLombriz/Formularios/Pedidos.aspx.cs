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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.Mail;

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
            else
            {
                listNewOrders.Clear();
                listOldOrders.Clear();
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
            if (idOrder != "") //El usuario acepto desde el modal y corroboramos el id 
            {
                try
                {
                    getAllOrderInfo(idOrder);
                    //Creamos docuemento 
                    Document document = new Document();
                    //Establecemos margenes 
                    document.SetMargins(60, 60, 40, 40);//Left,Right,Top,Bottom
                    MemoryStream memoryStream = new MemoryStream();
                    //Para este caso, indicamos que la dirección destino será el propio navegador (System.Web.HttpContext.Current.Response.OutputStream)
                    PdfWriter pdf = PdfWriter.GetInstance(document, memoryStream);
                    //Estilos de letra 
                    //Título
                    Font title = FontFactory.GetFont("Arial", 16, Font.BOLD);
                    Font subTitle = FontFactory.GetFont("Arial", 14);
                    Font content = FontFactory.GetFont("Arial", 12);
                    Font tableColumns = FontFactory.GetFont("Arial", 12, Font.BOLD);
                    //Ancho de columnas de tabla información del pedido 
                    float[] cellDataOrder = new float[2];
                    cellDataOrder[0] = 100;
                    cellDataOrder[1] = 22;
                    //Ancho de columas, tabla información del producto
                    float[] cellDataProduct = new float[6];
                    cellDataProduct[0] = 120;
                    cellDataProduct[1] = 120;
                    cellDataProduct[2] = 60;
                    cellDataProduct[3] = 60;
                    cellDataProduct[4] = 60;
                    cellDataProduct[5] = 50;
                    //Ancho de columnas, tabla total
                    float[] cellTotalOrder = new float[2];
                    cellTotalOrder[0] = 10;
                    cellTotalOrder[1] = 8;
                    //Colocamos título y nombre autor
                    document.AddTitle("Detalles de pedido");
                    document.AddCreator("La Lombriz S.A de C.V");
                    //Abrimos el documento 
                    document.Open();
                    //Header
                    //Insertamos logo
                    //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(patimage);
                    string pathImage = Server.MapPath("~/Recursos/") + "imagen.png";
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(pathImage);
                  
                    logo.BorderWidth = 0;
                    //Tamaño 
                    logo.ScaleToFit(80f, 80f);
                    //Posición del logo
                    logo.SetAbsolutePosition(60, 720); //X,Y
                    document.Add(logo);
                    //Header 
                    var titleParagraph = new Paragraph("La Lombriz S.A de C.V", title);
                    titleParagraph.Alignment = 1; //0=Izquierda 1=Centro 2=Derecha
                    document.Add(titleParagraph);
                    var subTitleParagraph = new Paragraph("Una empresa 100% mexicana", subTitle);
                    subTitleParagraph.Alignment = 1;
                    document.Add(subTitleParagraph);
                    var numReport = new Paragraph("Detalles del pedido", content);
                    numReport.Alignment = 1;
                    document.Add(numReport);
                    document.Add(Chunk.NEWLINE);
                    //Tabla datos de pedido
                    PdfPTable tableDataOrder = new PdfPTable(2);
                    //Agregamos celdas 
                    PdfPCell cellIdOrder = new PdfPCell(new Paragraph("No. Pedido:", tableColumns));
                    cellIdOrder.Border = Rectangle.NO_BORDER;
                    cellIdOrder.HorizontalAlignment = 2;
                    tableDataOrder.AddCell(cellIdOrder);
                    PdfPCell cellIdOrderValue = new PdfPCell(new Paragraph(Convert.ToString(pedidoContenido.Pedido.Id_pedido), content));
                    cellIdOrderValue.Border = Rectangle.NO_BORDER;
                    cellIdOrderValue.HorizontalAlignment = 0;
                    tableDataOrder.AddCell(cellIdOrderValue);

                    PdfPCell cellDataCreate = new PdfPCell(new Paragraph("Fecha creación:", tableColumns));
                    cellDataCreate.Border = Rectangle.NO_BORDER;
                    cellDataCreate.HorizontalAlignment = 2;
                    tableDataOrder.AddCell(cellDataCreate);
                    PdfPCell cellDataCreateValue = new PdfPCell(new Paragraph(Convert.ToString(pedidoContenido.Pedido.Fecha_creacion.ToString("dd/MM/yyyy")), content));
                    cellDataCreateValue.Border = Rectangle.NO_BORDER;
                    cellDataCreateValue.HorizontalAlignment = 0;
                    tableDataOrder.AddCell(cellDataCreateValue);

                    PdfPCell cellDataDelivery = new PdfPCell(new Paragraph("Fecha entrega:", tableColumns));
                    cellDataDelivery.Border = Rectangle.NO_BORDER;
                    cellDataDelivery.HorizontalAlignment = 2;
                    tableDataOrder.AddCell(cellDataDelivery);
                    PdfPCell cellDataDeliveryValue = new PdfPCell(new Paragraph(Convert.ToString(pedidoContenido.Pedido.Fecha_entrega.ToString("dd/MM/yyyy")), content));
                    cellDataDeliveryValue.Border = Rectangle.NO_BORDER;
                    cellDataDeliveryValue.HorizontalAlignment = 0;
                    tableDataOrder.AddCell(cellDataDeliveryValue);

                    tableDataOrder.SetWidths(cellDataOrder);
                    tableDataOrder.HorizontalAlignment = 2;
                    document.Add(tableDataOrder);

                    var orderOwner = new Paragraph("Pedido de: " + pedidoContenido.Usuario.Nombre);
                    orderOwner.Alignment = 0;
                    document.Add(orderOwner);
                    document.Add(Chunk.NEWLINE);

                    //Tabla productos del pedido
                    PdfPTable tableProducts = new PdfPTable(6);
                    PdfPCell headerNameProduct = new PdfPCell(new Paragraph("Nombre producto", tableColumns));
                    headerNameProduct.HorizontalAlignment = 1;
                    headerNameProduct.BackgroundColor = new iTextSharp.text.BaseColor(250, 113, 232);
                    tableProducts.AddCell(headerNameProduct);
                    PdfPCell headerDescriptionProduct = new PdfPCell(new Paragraph("Descripción", tableColumns));
                    headerDescriptionProduct.HorizontalAlignment = 1;
                    headerDescriptionProduct.BackgroundColor = new iTextSharp.text.BaseColor(250, 113, 232);
                    tableProducts.AddCell(headerDescriptionProduct);
                    PdfPCell headerSizeProduct = new PdfPCell(new Paragraph("Tamaño", tableColumns));
                    headerSizeProduct.HorizontalAlignment = 1;
                    headerSizeProduct.BackgroundColor = new iTextSharp.text.BaseColor(250, 113, 232);
                    tableProducts.AddCell(headerSizeProduct);
                    PdfPCell headerQuantityProduct = new PdfPCell(new Paragraph("Cantidad", tableColumns));
                    headerQuantityProduct.HorizontalAlignment = 1;
                    headerQuantityProduct.BackgroundColor = new iTextSharp.text.BaseColor(250, 113, 232);
                    tableProducts.AddCell(headerQuantityProduct);
                    PdfPCell headerCostProduct = new PdfPCell(new Paragraph("Precio unitario", tableColumns));
                    headerCostProduct.HorizontalAlignment = 1;
                    headerCostProduct.BackgroundColor = new iTextSharp.text.BaseColor(250, 113, 232);
                    tableProducts.AddCell(headerCostProduct);
                    PdfPCell headerTotalProduct = new PdfPCell(new Paragraph("Total", tableColumns));
                    headerTotalProduct.HorizontalAlignment = 1;
                    headerTotalProduct.BackgroundColor = new iTextSharp.text.BaseColor(250, 113, 232);
                    tableProducts.AddCell(headerTotalProduct);

                    foreach (Productos producto in pedidoContenido.Productos)
                    {
                        PdfPCell cellNameProdcut = new PdfPCell(new Paragraph(producto.Nombre_producto, content));
                        cellNameProdcut.HorizontalAlignment = 1;
                        tableProducts.AddCell(cellNameProdcut);
                        PdfPCell cellDescriptionProdcut = new PdfPCell(new Paragraph(producto.Descripcion, content));
                        cellDescriptionProdcut.HorizontalAlignment = 1;
                        tableProducts.AddCell(cellDescriptionProdcut);
                        PdfPCell cellSizeProdcut = new PdfPCell(new Paragraph(producto.Tamanio, content));
                        cellSizeProdcut.HorizontalAlignment = 1;
                        tableProducts.AddCell(cellSizeProdcut);
                        PdfPCell cellQuantityProdcut = new PdfPCell(new Paragraph(Convert.ToString(producto.Cantidad), content));
                        cellQuantityProdcut.HorizontalAlignment = 1;
                        tableProducts.AddCell(cellQuantityProdcut);
                        PdfPCell cellCostProdcut = new PdfPCell(new Paragraph("$" + producto.Precio, content));
                        cellCostProdcut.HorizontalAlignment = 1;
                        tableProducts.AddCell(cellCostProdcut);
                        PdfPCell cellTotalProdcut = new PdfPCell(new Paragraph("$" + (producto.Cantidad * producto.Precio), content));
                        cellTotalProdcut.HorizontalAlignment = 1;
                        tableProducts.AddCell(cellTotalProdcut);
                    }
                    tableProducts.WidthPercentage = 100f;
                    tableProducts.HorizontalAlignment = 0;
                    tableProducts.SetWidths(cellDataProduct);
                    document.Add(tableProducts);

                    //Tabla total 
                    PdfPTable tableTotal = new PdfPTable(2);
                    PdfPCell cellTotalCostProdcut = new PdfPCell(new Paragraph("Total:", tableColumns));
                    cellTotalCostProdcut.HorizontalAlignment = 2;
                    cellTotalCostProdcut.Border = Rectangle.NO_BORDER;
                    tableTotal.AddCell(cellTotalCostProdcut);
                    PdfPCell cellTotal = new PdfPCell(new Paragraph("$" + pedidoContenido.Pedido.Precio, tableColumns));
                    cellTotal.HorizontalAlignment = 1;
                    tableTotal.AddCell(cellTotal);
                    tableTotal.SetWidths(cellTotalOrder);
                    tableTotal.HorizontalAlignment = 2;
                    tableTotal.WidthPercentage = 24f;
                    document.Add(tableTotal);

                    //Tabla nota 
                    PdfPTable tableNote = new PdfPTable(1);
                    //Espacio entre nota y la tabla
                    tableNote.SpacingBefore = 200;
                    PdfPCell cellTitle = new PdfPCell(new Paragraph("Nota", tableColumns));
                    cellTitle.Border = Rectangle.NO_BORDER;
                    cellTitle.BorderWidthTop = 1f;
                    cellTitle.BorderWidthLeft = 1f;
                    cellTitle.BorderWidthRight = 1f;
                    tableNote.AddCell(cellTitle);
                    PdfPCell cellContent = new PdfPCell(new Paragraph("Los pedidos pueden ser cancelados con un máximo de 10 días antes de la fecha de entrega, favor de tener su número de pedido en mano. Para cualquier duda o aclaración de favor de ponerse en contacto mediante nuestras redes sociales o correo.", content));
                    cellContent.Border = Rectangle.NO_BORDER;
                    cellContent.BorderWidthLeft = 1f;
                    cellContent.BorderWidthRight = 1f;
                    cellContent.BorderWidthBottom = 1f;
                    tableNote.WidthPercentage = 100f;
                    tableNote.AddCell(cellContent);
                    document.Add(tableNote);
                    pdf.CloseStream = false;
                    //Cerramos el archivo
                    document.Close();
                    memoryStream.Position = 0;
                    //Instanciamos de la clase mailmessage, el objeto servirá para agregar las partes de nuestro correo
                    MailMessage mail = new MailMessage();
                    //Indicamos el servidor de correo y puerto con el que trabaja gmail
                    SmtpClient smtpServer = new SmtpClient("smtp.gmail.com", 587)
                    {
                        //Vuelve a nulo el valor de credenciales, esto permitirá usar nuestras propias credenciales
                        UseDefaultCredentials = false,
                        //Se indican las credenciales de la cuenta gmail que ocuparemos para enviar el correo
                        Credentials = new System.Net.NetworkCredential("noreplylalombriz@gmail.com", "lalombrizAP"),
                        //Método de entrega
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        //Habilitar seguridad en smtp
                        EnableSsl = true,
                    };
                    //Creamos el correo
                    //Indicamos de donde viene el correo
                    mail.From = new MailAddress("noreplylalombriz@gmail.com");
                    //Indicamos dirección destino
                    mail.To.Add(Session["CORREO_USUARIO"].ToString());
                    //Asunto
                    mail.Subject = "Cancelación de PEDIDO: " + idOrder;
                    //Cuerpo
                    mail.Attachments.Add(new Attachment(memoryStream, "Cancelacion.pdf"));
                    mail.Body = "Estimado/a  " + pedidoContenido.Usuario.Nombre + " a continuación se adjunta un archivo en formato pdf que contiene la información relacionada al pedido que ha sido cancelado. Este correo se genera de manera automática, favor de no responder.";
                    //Enviamos el email 
                    smtpServer.Send(mail);
                    //Comenzamos con el borrado de datos en la BD
                    if (BorrarProductosPedidos(idOrder)) //Borrado de productos del pedido
                    {
                        if (BorrarPedido(idOrder)) //Borrado del pedido
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'success',title: '¡Todo listo!',text: 'El pedido ha sido cancelado.',showConfirmButton:true}).then(function(){window.location.href='Pedidos.aspx';})</script>");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'warning',title: '¡Oops!',text: 'Algo salió mal'})</script>");
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'warning',title: '¡Oops!',text: 'Algo salió mal'})</script>");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR " + e);

                }
            }
        }
        //Método para borrar pedido de la BD
        public bool BorrarPedido(string idOrder)
        {
            string query = "DELETE FROM pedidos WHERE `ID_PEDIDO` = " + Convert.ToInt32(idOrder) + "";
            //Conexiones 
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;
            try
            {
                //Abrir base de datos
                dbConnection.Open();
                //Insertamos
                MySqlDataReader myReader = cmdDB.ExecuteReader();
                //Cerramos base de datos 
                dbConnection.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
                return false;
            }
        }
        //Método para borrar productos del pedido de la BD
        public bool BorrarProductosPedidos(string idOrder)
        {
            string query = "DELETE FROM productos_pedido WHERE `ID_PEDIDO` = "+Convert.ToInt32(idOrder)+"";
            //Conexiones 
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;
            try
            {
                //Abrir base de datos
                dbConnection.Open();
                //Insertamos
                MySqlDataReader myReader = cmdDB.ExecuteReader();
                //Cerramos base de datos 
                dbConnection.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
                return false;
            }
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
            string pathImage = Server.MapPath("~/Recursos/") + "imagen.png";
            pdfFile.createPDF(pathImage);
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
                    sb.Append("<a id='" + pedido.Id_pedido + "_Detalles' class='dropdown-item' onclick='onClickDetails(this);'>Detalles<img src='../Recursos/seeDetails.png' alt='details'  class='optionsImages'/></a>");
                    if (ComprobarFecha(pedido.Fecha_entrega.ToString("dd/MM/yyyy")))
                    {
                        sb.Append("<a id='" + pedido.Id_pedido + "_Eliminar' class='dropdown-item' onclick='onClickDelete(this)'>Eliminar<img src='../Recursos/delete.png' alt='delete' class='optionsImages'/></a>");
                    }
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
        //Validadmos los días para la cancelación del pedido
        public bool ComprobarFecha(string fecha_entrega)
        {
            DateTime fecha_actual;
            fecha_actual = DateTime.Today;
            string[] datos_creacion = fecha_actual.ToString().Split('/', ' ');
            string[] datos_entrega = fecha_entrega.Split('/');
            int dias_creacion = 0, dias_entrega=0, condicion =0;
            dias_creacion = dias_acum(Convert.ToInt32(datos_creacion[0]), Convert.ToInt32(datos_creacion[1])); //días acumulados hasta la fecha actual
            dias_entrega = dias_acum(Convert.ToInt32(datos_entrega[0]), Convert.ToInt32(datos_entrega[1])); //días acumulados hasta la fecha de entrega
            condicion = dias_entrega - dias_creacion;
            if (condicion >= 10) //Dentro de los limites para cancelar el pedido
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Obtenemos la suma de días acumulados
        public int dias_acum(int dia, int mes)
        {
            int[] dias_acum = { 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334, 365 };
            int resultado = 0;
            if (mes == 1)
            {
                return dia;
            }
            else
            {
                resultado = dias_acum[mes - 2] + dia;
                return resultado;
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
            if (oldOrders.Style["display"] == "none" && listOldOrders.Count > 0)
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
                string query = "SELECT ID_USUARIO,FECHA_ENTREGA,FECHA_CREACION,PRECIO FROM `pedidos` WHERE ID_PEDIDO=" + Convert.ToInt32(idOrder) + "";
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
                            pedido = new PedidosCliente(Convert.ToInt32(idOrder), Convert.ToInt32(reader.GetString(0)), DateTime.Parse(reader.GetString(1)), DateTime.Parse(reader.GetString(2)), Convert.ToDouble(reader.GetString(3)));
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