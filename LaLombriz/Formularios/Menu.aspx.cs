using MySql.Data.MySqlClient;
using System.Collections;
using System;
using LaLombriz.Clases;
using System.Collections.Generic;
using System.Net.Mail;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace LaLombriz.Formularios
{
    public partial class Menu : System.Web.UI.Page
    {
        private int contadorP = 1;
        private bool isCartOptionActivated = false;
        private static string strConnection = "Server=localhost;Database=reposteria;Uid=gio;Pwd=270299GPS";
        public static ArrayList listNewOrders = new ArrayList();
        private static List<string> productsName = new List<string>();
        private static List<string> descriptionProduct = new List<string>();
        private static Dictionary<string, string> pricestProduct = new Dictionary<string, string>();
        private static Dictionary<string, string> pricesSpecialProduct = new Dictionary<string, string>();
        private static Dictionary<int, Dictionary<string, string>> specialProductsName = new Dictionary<int, Dictionary<string, string>>();
        private static Dictionary<string, Dictionary<string,string>> productsInfo = new Dictionary<string, Dictionary<string, string>>();
        private static Dictionary<Dictionary<string, string>, Dictionary<string, string>> specialProductsInfo = new Dictionary<Dictionary<string, string>, Dictionary<string, string>>();
        private static Dictionary<int,string[]> carroProductos = new Dictionary<int,string[]>();
        public static ProductosPedidos pedidoContenido;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                productsContainer.Visible = false;
                lblConteoCarro.Text = "0";
                //tableCake.Visible = false;
            }
            if (Session["NoProductos"] != null)
            {
                lblConteoCarro.Text = ((int)Session["NoProductos"]).ToString();
            }
            if (isCartOptionActivated)
            {
                
            }
            
        }
        //Boton pasteles
        public void btnCakeOnClick(object sender, EventArgs e)
        {
            clearCollections();
            showSecondForm();
            productsName = getProducts(1, 15);
            if (productsName.Count > 0)
            {
                foreach (string product in productsName)
                {
                    pricestProduct = getPricesProduct(product);
                    if (pricestProduct.Count > 0)
                    {
                        productsInfo.Add(product, pricestProduct);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Ocurrió un error, vuelve a intentarlo más tarde'})</script>");
                    }
                    }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Ocurrió un error, vuelve a intentarlo más tarde'})</script>");
            }
            drawInterface(0,descriptionProduct);
        }
        //Boton macarons
        public void btnBurgerOnClick(object sender, EventArgs e)
        {
            clearCollections();
            showSecondForm();
            productsName = getProducts(16, 39);
            if (productsName.Count > 0)
            {
                foreach (string product in productsName)
                {
                    pricestProduct = getPricesProduct(product);
                    if (pricestProduct.Count > 0)
                    {
                        productsInfo.Add(product, pricestProduct);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Ocurrió un error, vuelve a intentarlo más tarde'})</script>");
                    }
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Ocurrió un error, vuelve a intentarlo más tarde'})</script>");
            }
            drawInterface(0,descriptionProduct);
        }
        //Boton mesas de dulces
        public void btnPackOnClick(object sender,EventArgs e)
        {
            clearCollections();
            showSecondForm();
            productsName = getProducts(82, 86);
            descriptionProduct = getDescriptionProducts(82, 86);
            if (productsName.Count > 0 && descriptionProduct.Count>0)
            {
                foreach (string product in productsName)
                {
                    pricestProduct = getPricesProduct(product);
                    if (pricestProduct.Count > 0)
                    {
                        productsInfo.Add(product, pricestProduct);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Ocurrió un error, vuelve a intentarlo más tarde'})</script>");
                    }
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Ocurrió un error, vuelve a intentarlo más tarde'})</script>");
            }
            drawInterface(2,descriptionProduct);
        }
        //Boton otros
        public void btnOtherOnClick(object sender, EventArgs e)
        {
            clearCollections();
            showSecondForm();
            //Se traen los productos "normales" aquellos que no tengan descripción.
            productsName = getProducts(40,81);
            //Se valida que haya datos en la lista
            if (productsName.Count > 0)
            {
                //Se recorre la lista
                foreach (string product in productsName)
                {
                    //Se traen los precios de ese producto
                    pricestProduct = getPricesProduct(product);
                    //Se valida que haya datos en la lista de precios (hay una t de más xd)
                    if (pricestProduct.Count > 0)
                    {
                        //Se agrega el nombre del producto y sus precios al diccionario.
                        productsInfo.Add(product, pricestProduct);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Ocurrió un error, vuelve a intentarlo más tarde'})</script>");
                    }
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Ocurrió un error, vuelve a intentarlo más tarde'})</script>");
            }
            //Se dibuja la interfaz para los productos "normales"
            drawInterface(0, descriptionProduct);
        }
        //Metodo onclick para agregar los productos al carrito 
        public void btnAddOnClick(object sender, EventArgs e)
        {
            //Traemos valores del front 
            string product = Request.Form["hiddenIdProduct"];
            string size = Request.Form["hiddenSizeProduct"];
            string quantity = Request.Form["hiddenQuantityProduct"];
            if (size == "undefined") //El usuario pudo no haber escogido un tamaño o estamos frente a un paquete
            {
                int discriminador = 0;
                discriminador = getIDProduct(product, "Grande");
                if (discriminador != 0) // El producto si tiene un tamaño que el usuario puede escoger
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: '¡Oops!',text: 'Por favor, selecciona el tamaño del producto.'})</script>");
                }
                else if (discriminador == 0) // El producto es un paquete
                {
                    //La variable del nombre guardada en product
                    size = "-";
                    Agregar(product, size, quantity,1);
                }
            }
            else
            {
                Agregar(product, size, quantity,0);
            }
            
        }
        public void Agregar (string product, string size, string quantity, int caso) //Función para guardar el producto en el diccionario
        {
            string[] productInformation = new string[3];
            productInformation[0] = product;
            productInformation[1] = size;
            productInformation[2] = quantity;
            int idProduct = 0;
            if (caso == 0)
            {
                idProduct = getIDProduct(product, size);
            }
            else
            {
                idProduct = getIDProductPaquete(product);
            }
            int identificador = 0, cantidad = 0, duplicado = 0;
            foreach (KeyValuePair<int, string[]> producto in carroProductos) //Calculamos el total a pagar 
            {
                identificador = producto.Key;
                cantidad = Convert.ToInt32(producto.Value[2]);
                if (idProduct == identificador) //Vemos si el producto ya se encuentra en el carro
                {
                    cantidad = cantidad + Convert.ToInt32(quantity); //Obtenemos la nueva cantidad de productos 
                    producto.Value[2] = cantidad.ToString(); //Guardamos la cantidad dentro del producto correspondiente
                    duplicado = 1; //Asignamos el valor de uno indicando que el producto ya existia en el carro 
                }
            }
            if (duplicado == 0) //El producto agregado no existia en el carro y se guarda
            {
                //Se agrega el identificador del producto y el diccionario temporal al diccionario principal
                carroProductos.Add(idProduct, productInformation);
                //Código para aumentar el número mostrado al lado del carrito
                if (Session["NoProductos"] != null)
                {
                    contadorP = (int)Session["NoProductos"] + 1;
                }
                lblConteoCarro.Text = contadorP.ToString();
                Session["NoProductos"] = contadorP;
            }
        }
        public int getIDProductPaquete(string product) //Función para recuperar el id del paquete
        {
            int id = 0;
            string query = "SELECT ID_PRODUCTO FROM productos where NOMBRE_PRODUCTO='" + product + "'";
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
        //Método para abrir el carrito 
        public void btnSeeCarOptionOnClick(object sender, EventArgs args)
        {
            if (Session["NoProductos"] == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: '¡Oops!',text: 'No hay productos en tu carrito de compras.'})</script>");
            }
            else
            {
                //Indicamos que la vista del carrito se está visualizando 
                isCartOptionActivated = true;
                //Se muestra la vista
                if (detailCart.Style["display"] == "none")
                {
                    detailCart.Style["display"] = "flex";
                    products.Style["display"] = "none";
                }
                drawInterfaceCart();
            }
            
        }
        //Metodo para eliminar el pedido 
        public void btnDeleteOnClick(object sender, EventArgs args)
        {
            string idProduct = Request.Form["hiddenIdProduct"];
            //Quitamos producto del diccionario
            carroProductos.Remove(Convert.ToInt32(idProduct));
            int aux = 0;
            aux = (int)Session["NoProductos"]-1; //Descontamos una unidad al contador de productos 
            if (aux == 0) //Eliminamos todos los pedidos
                Session["NoProductos"] = null;
            else //Aún queda al menos un producto
                Session["NoProductos"] = aux;
            
            if (carroProductos.Count > 0)
            {
                lblConteoCarro.Text = aux.ToString();
                drawInterfaceCart();
            }
            else
            {
                lblConteoCarro.Text = "0"; //Por motivos esteticos pintamos el cero de manera inmediata antes del refresh de la página
                detailCart.Style["display"] = "none";
                notProductsCart.Style["display"] = "flex";
            }
        }
        //Metodo para retornar al menu de productos
        public void btnReturnMenuOnlick(object sender, EventArgs args)
        {
            isCartOptionActivated = false;
            Response.Redirect("Menu.aspx");
        }
        public int getIDProduct(string nombre, string tamaño) //Recuperamos el ID del producto que se añadió al carro
        {
            int id = 0;
            string query = "SELECT ID_PRODUCTO FROM productos where NOMBRE_PRODUCTO='" + nombre+ "' AND TAMANIO='" + tamaño+"'";
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
        //Metodo para mostrar formulario de productos seleccionado
        public void showSecondForm()
        {
            optionsContainer.Visible = false;
            productsContainer.Visible = true;
        }
        //Metodo para limpiar todas las colecciones creadas
        public void clearCollections()
        {
            productsName.Clear();
            productsInfo.Clear();
            pricestProduct.Clear();
            descriptionProduct.Clear();
            pricesSpecialProduct.Clear();
            specialProductsInfo.Clear();
            specialProductsName.Clear();
        }
        //Método para mostrar productos
        public void drawInterface(int numProduct,List<string>descriptions)
        {
            int contProduct = 0,contTamanio=0;
            StringBuilder sb = new StringBuilder();
            foreach(KeyValuePair<string,Dictionary<string,string>> product in productsInfo)
            {
                string nameImage = product.Key.Replace(" ", "_");
                sb.Append("<div id='"+product.Key+"' class='infoProduct row col-xs-12 col-md-6'>");
                sb.Append("<div id='image-container'class='imageContainer col-xs-12 col-md-6' style='background-image:url(../Recursos/Menu/"+nameImage+".jpg);'>");
                sb.Append("</div>");
                sb.Append("<div id='info-container' class='productContainer col-xs-12 col-md-6'>");
                sb.Append("<div style='min-height:120px;'>");
                sb.Append("<h2>" + product.Key + "</h2>");
                sb.Append("</div>");
                //Se valida si el producto es de la categoría de paquetes
                if (numProduct==2)
                {
                    //Se agrega la parte de descripción y se aumenta en uno el contador
                    sb.Append("<h4>Descripción</h4>");
                    sb.Append("<p style='text-align:justify;'>"+descriptions[contProduct]+"</p>");
                    contProduct++;
                }
                //Se valida si el producto es macarons, pasteles u otros "normales"
                if (numProduct == 0)
                {
                    sb.Append("<h4>Precios</h4>");
                    sb.Append("</br>");
                    foreach (KeyValuePair<string, string> prices in product.Value)
                    {
                        sb.Append("<span style='float:left'>");
                        sb.Append("<input class='form-check-input' type='radio' name='"+nameImage+"' id='"+nameImage+""+contTamanio+"' value='"+prices.Key+"'>");
                        sb.Append("<label class='form-check-label' for='"+nameImage+""+contTamanio+"'>");
                        sb.Append("&nbsp"+prices.Key);
                        sb.Append("</label></span>");
                        sb.Append("<span style='float:right'>" + prices.Value + "</span></br></br>");
                        contTamanio++;
                    }
                    sb.Append("<h4>Cantidad</h4>");
                    sb.Append("<input type='number' id='"+nameImage+"Quantity' value='1' min='1' max='1000' step='1' class='quantity'/></br></br>");
                    sb.Append("<div class='addToCart'>");
                    sb.Append("<button id='"+nameImage+"' type='button' class='btn btn-primary' onclick='getID(this)'>Agregar</button>");
                    sb.Append("</div>");
                }
                else
                {
                    //Si no, es de la categoría paquetes
                    sb.Append("<h4>Precio</h4>");
                    foreach (KeyValuePair<string, string> prices in product.Value)
                    {
                        sb.Append("<p>" + prices.Value + "</p>");
                    }
                    sb.Append("<h4>Cantidad</h4>");
                    sb.Append("<input type='number'  id='"+nameImage+"Quantity' value='1' min='1' max='1000' step='1' class='quantity'/></br></br>");
                    sb.Append("<div class='addToCart'>");
                    sb.Append("<button id='" + nameImage + "' type='button' class='btn btn-primary' onclick='getID(this)'>Agregar</button>");
                    sb.Append("</div>");
                }
                sb.Append("</div>");
                sb.Append("</div>");
                //Se asigna la interfaz a la literal
                ltProduct.Text = sb.ToString();
            }   
        }
        //Botón para comprar 
        public void btnComprar(object sender, EventArgs args)
        {
            if (Session["CORREO_USUARIO"] == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'warning',title: '¡Solo un paso más!',text: 'Debes iniciar sesión para poder confirmar la compra'})</script>");
            }
            else
            {
                if (calendario.Value != "") //Fecha vacía?
                {
                    //Validamos que la fecha elegida sea mayor a la fecha actual
                    string fecha_validar = "";
                    fecha_validar = calendario.Value; //Recuperamos la fecha introducida por el cliente
                    string[] dato_aux = fecha_validar.Split('/', ' '); //Fragmentamos la fecha
                    fecha_validar = dato_aux[2] + "/" + dato_aux[0] + "/" + dato_aux[1]; //Nuevo formato de fecha
                    if (FechaValida(fecha_validar))
                    {
                        //Verificamos que el cliente haya dado clic en el botón de Aceptar
                        string confirmacion = Request.Form["hiddenIdAddOrder"];
                        if (confirmacion == "1") //El usuario confirma el pedido
                        {
                            int identificador = 0, lastid = 0, iduser = 0, cantidad = 0, i = 0;
                            float total = 0, precio = 0;
                            string fecha_entrega = "", fecha_creacion = "";
                            lastid = RecuperarIDPedido() + 1; //Recuperamos ultímo id de pedido registrado
                            iduser = getIDUser(Session["CORREO_USUARIO"].ToString()); //Recuperamos el id del usuario
                            foreach (KeyValuePair<int, string[]> producto in carroProductos) //Calculamos el total a pagar 
                            {
                                identificador = producto.Key;
                                cantidad = Convert.ToInt32(producto.Value[2]);
                                precio = getPrecio(identificador);
                                total = total + (precio * cantidad);
                                //i++;
                            }
                            i = (int)Session["NoProductos"];
                            fecha_creacion = DateTime.Today.ToString(); //Recuperamos fecha actual del sistema
                            string[] datos_fecha = fecha_creacion.Split('/', ' '); //Fragmentamos la fecha
                            fecha_creacion = datos_fecha[2] + "-" + datos_fecha[1] + "-" + datos_fecha[0]; //Nuevo formato de fecha
                            fecha_entrega = calendario.Value; //Recuperamos la fecha introducida por el cliente
                            string[] fragmentador = fecha_entrega.Split('/', ' '); //Fragmentamos la fecha
                            fecha_entrega = fragmentador[2] + "-" + fragmentador[0] + "-" + fragmentador[1]; //Nuevo formato de fecha

                            if (GuardarPedido(lastid, iduser, fecha_entrega, fecha_creacion, total, 0) == true) //Creamos el pedido
                            {
                                int quantity = 0, a = 0;
                                int[] identificadores = new int[i];
                                foreach (KeyValuePair<int, string[]> producto in carroProductos)
                                {
                                    identificador = producto.Key;
                                    quantity = Convert.ToInt32(producto.Value[2]);
                                    GuardarProductoPedido(lastid, identificador, quantity); //Guardamos cada producto relacionado al pedido
                                    identificadores[a] = identificador; //Guardamos cada id de productos dentro del carro para eliminarlos
                                    a++;
                                }
                                for (int j = 0; j < a; j++)
                                {
                                    carroProductos.Remove(identificadores[j]);
                                    int aux = 0;
                                    aux = (int)Session["NoProductos"] - 1; //Descontamos una unidad al contador de productos 
                                    if (aux == 0) //Eliminamos todos los productos
                                        Session["NoProductos"] = null;
                                    else //Aún queda al menos un producto
                                        Session["NoProductos"] = aux;
                                }
                                lblConteoCarro.Text = "0"; //Por motivos esteticos pintamos el cero de manera inmediata antes del refresh de la página
                                detailCart.Style["display"] = "none";
                                notProductsCart.Style["display"] = "flex";
                                //Creación de PDF y envío por correo
                                getAllOrderInfo(lastid.ToString());
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
                                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("C:\\Users\\Gio\\Documents\\proyectosDotNet\\LaLombriz\\LaLombriz\\Recursos\\imagen.png");
                                //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("C:\\Users\\CARLOS\\proyecto\\LaLombriz\\Recursos\\imagen.png");
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
                                PdfPCell cellContent = new PdfPCell(new Paragraph("Los pedidos pueden ser cancelados o modificados con un máximo de 10 días antes de la fecha de entrega, favor de tener su número de pedido en mano. Para cualquier duda o aclaración de favor de ponerse en contacto mediante nuestras redes sociales o correo.", content));
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
                                mail.Subject = "Confirmación de pedido #" + lastid;
                                //Cuerpo
                                mail.Attachments.Add(new Attachment(memoryStream, "Pedido" + lastid + ".pdf"));
                                mail.Body = "Hola  " + pedidoContenido.Usuario.Nombre + " muchas gracias por tu compra. A continuación se adjunta un archivo en formato pdf que contiene la información relacionada al pedido que has realizado hace unos momentos, nos comunicaremos contigo a la brevedad. Este correo se genera de manera automática, favor de no responder.";
                                //Enviamos el email 
                                smtpServer.Send(mail);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'success',title: '¡Gracias por tu compra!',text: 'Tu pedido ha sido registrado, te haremos llegar un correo electrónico con la información del pedido.'})</script>");
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: '¡Oops!',text: 'Algo salió mal al registrar tu pedido, lo sentimos.'})</script>");
                            }

                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: '¡Oops!',text: 'Algo salió mal al procesar tu pedido, lo sentimos.'})</script>");
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'warning',title: '¡Oops!',text: 'Por favor ingresa una fecha valida.'})</script>");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'warning',title: '¡Oops!',text: 'Por favor ingresa una fecha valida.'})</script>");
                }
            }
        }
        //Validación de fecha ingresada
        public bool FechaValida(string fecha_entrega)
        {
            DateTime fecha_actual;
            fecha_actual = DateTime.Today;
            string[] datos_creacion = fecha_actual.ToString().Split('/', ' ');
            string[] datos_entrega = fecha_entrega.Split('/',' ');
            int dias_creacion = 0, dias_entrega = 0, condicion = 0;
            dias_creacion = dias_acum(Convert.ToInt32(datos_creacion[0]), Convert.ToInt32(datos_creacion[1])); //días acumulados hasta la fecha actual
            dias_entrega = dias_acum(Convert.ToInt32(datos_entrega[2]), Convert.ToInt32(datos_entrega[1])); //días acumulados hasta la fecha de entrega
            condicion = dias_entrega - dias_creacion;
             if (condicion > 0) //Fecha valida
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
        public bool GuardarPedido(int id_pedido, int id_usuario, string fe, string fc, float precio, int estatus)
        {
            string query = "INSERT INTO pedidos (`id_pedido`,`id_usuario`, `fecha_entrega`, `fecha_creacion`, `precio`, `estatus`) VALUES ('"+id_pedido+"','" + id_usuario + "','" + fe + "', '" + fc + "', '" + precio + "', '" + estatus + "')";
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
        public void GuardarProductoPedido(int pedido, int producto, int cantidad)
        {
            string query = "INSERT INTO productos_pedido(`id_pedido`,`id_producto`, `cantidad`) VALUES ('"+pedido+"','" + producto + "', '" + cantidad + "')";
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
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }
        }
        public int RecuperarIDPedido()
        {
            int contador = 0;
            string query = "SELECT DISTINCT ID_PEDIDO FROM `pedidos`";
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
                    while (reader.Read())
                    {
                        contador= Convert.ToInt32(reader.GetString(0));
                    }
                }
                dbConnection.Close();
                return contador;
            }
            catch (Exception e)
            {
                //Mensjae de error
                Console.WriteLine("Error" + e);
                return -1;
            }
        }
        public int getIDUser(string correo)
        {
            int id = 0;
            string query = "SELECT ID_USUARIO FROM usuarios where CORREO='" + correo + "'";
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
        public float getPrecio(int id)
        {
            float precio = 0;
            string query = "SELECT PRECIO FROM productos where ID_PRODUCTO='" + id + "'";
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
                        precio = Convert.ToSingle(reader.GetString(0));
                    }
                }
                dbConnection.Close();
                return precio;
            }
            catch (Exception e)
            {
                //Mensjae de error
                Console.WriteLine("Error" + e);
                return 0;
            }
        }
        //Metodo para dibujar interfaz del carrito 
        private void drawInterfaceCart()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<int, string[]> producto in carroProductos)
            {
                sb.Append("<div id='" + producto.Key + "Order'  class='oneProduct'>");
                sb.Append("<div class='containerDeleteOption'>");
                sb.Append("<div class='dropdown'>");
                sb.Append("<button class='btnDeleteOption' type='button' id='"+producto.Key+"' onclick='deleteProduct(this)'><img src='../Recursos/delete.png' alt='eliminar' class='imgDotOptions'/></button>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("<div class='tableInformation'>");
                sb.Append("<table class='table table-borderless tableNewOrder'>");
                sb.Append("<thead>");
                sb.Append("<tr>");
                sb.Append("<th scope='col' style='width:403px'>Nombre de producto</th><th scope='col' style='width:182px'>Tamaño</th><th scope='col' style='width:195px'>Cantidad</th>");
                sb.Append("</tr>");
                sb.Append("</thead>");
                sb.Append("<tbody>");
                sb.Append("<tr>");
                sb.Append("<td>" + producto.Value[0] + "</td><td>" + producto.Value[1] + "</td><td>" + producto.Value[2] + "pz</td>");
                sb.Append("</tr>");
                sb.Append("</tbody>");
                sb.Append("</table>");
                sb.Append("</div>");
                sb.Append("</div>");
                tbProductsCart.Text = sb.ToString();
            }
        }   
        //Metodo para traer productos y llenar gridview
        public List<string> getProducts(int min, int max)
        {
            string query = "SELECT DISTINCT NOMBRE_PRODUCTO FROM `productos` WHERE (id_producto BETWEEN " + min + " AND " + max + ")";
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;
            MySqlDataReader reader;
            List<string> listProduct = new List<string>();
            try
            {
                dbConnection.Open();
                //Leemos los datos 
                reader = cmdDB.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        listProduct.Add(reader.GetString(0));
                    }
                }
                dbConnection.Close();
                return listProduct;
            }
            catch (Exception e)
            {
                //Mensjae de error
                Console.WriteLine("Error" + e);
                return listProduct;
            }
        }
        //Metodo para traer productos y su descripcion de la sección "otros"
        public Dictionary<int,Dictionary<string,string>> getSpecialProducts(int val1, int val2)
        {
            //Contador para llave de deccionario
            int contProducts = 0;
            string query = "SELECT NOMBRE_PRODUCTO,DESCRIPCION FROM productos where (ID_PRODUCTO = "+val1+") OR (ID_PRODUCTO ="+ val2 + ")";
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;
            MySqlDataReader reader;
            //Se crea diccionario conformado por un entero (llave) y otro diccionario, ese segundo esta conformado por el nombre de producto y la descripción del mismo
            Dictionary<int, Dictionary<string,string>> listSpecialProduct = new Dictionary<int,Dictionary<string,string>>();
            try
            {
                dbConnection.Open();
                //Leemos los datos 
                reader = cmdDB.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Creamos un diccionario "temporal" en el cual se le asigna el nombre del producto y la descripción
                        Dictionary<string, string> temporaryDictionary = new Dictionary<string, string>();
                        temporaryDictionary.Add(reader.GetString(0),reader.GetString(1));
                        //Se agrega el contador y el diccionario temporal al diccionario principal
                        listSpecialProduct.Add(contProducts,temporaryDictionary);
                        contProducts++;

                    }
                }
                dbConnection.Close();
                return listSpecialProduct;
            }
            catch (Exception e)
            {
                //Mensjae de error
                Console.WriteLine("Error" + e);
                return listSpecialProduct;
            }
        }
        //Metodo para traer precios de los productos
        public Dictionary<string,string> getPricesProduct(string nameProduct)
        {
            string query = "SELECT TAMANIO,PRECIO FROM `productos` WHERE NOMBRE_PRODUCTO='" + nameProduct + "'";
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;
            MySqlDataReader reader;
            Dictionary<string,string> listCost = new Dictionary<string, string>();
            try
            {
                dbConnection.Open();
                //Leemos los datos 
                reader = cmdDB.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        listCost.Add(reader.GetString(0),reader.GetString(1));
                    }
                }
                dbConnection.Close();
                return listCost;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error " + e);
                return listCost;
            }
        }
        //Metodo para traer descripciones de productos
        public List<string> getDescriptionProducts(int min,int max)
        {
            string query = "SELECT DESCRIPCION FROM `productos` WHERE (id_producto BETWEEN " + min + " AND " + max + ")";
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;
            MySqlDataReader reader;
            List<string> listDescription = new List<string>();
            try
            {
                dbConnection.Open();
                //Leemos los datos 
                reader = cmdDB.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        listDescription.Add(reader.GetString(0));
                    }
                }
                dbConnection.Close();
                return listDescription;
            }
            catch (Exception e)
            {
                //Mensjae de error
                Console.WriteLine("Error" + e);
                return listDescription;
            }
        }
        //Metodo para obtener toda la información del pedido
        public void getAllOrderInfo(string idOrder)
        {
            PedidosCliente pedido = getDetailsOrder(idOrder);
            Usuario usuario = getUserInformation(pedido.Id_usuario);
            Dictionary<int, int> productosTemporal = getQuantity(pedido.Id_pedido);
            ArrayList producto = new ArrayList();
            foreach (KeyValuePair<int, int> productoTemporal in productosTemporal)
            {
                producto.Add(getProductDetails(productoTemporal.Key, productoTemporal.Value));
            }
            pedidoContenido = new ProductosPedidos(pedido, usuario, producto);
        }
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
                        usuario = new Usuario(reader.GetString(0), reader.GetString(1), "", reader.GetString(2));
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
        public Dictionary<int, int> getQuantity(int idOrder)
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
        public Productos getProductDetails(int idProduct, int quantity)
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
                        producto = new Productos(reader.GetString(0), reader.GetString(1), reader.GetString(2), Convert.ToDouble(reader.GetString(3)), quantity);
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