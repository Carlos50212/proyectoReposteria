using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;

namespace LaLombriz.Formularios
{
    public partial class Menu : System.Web.UI.Page
    {
        private int contadorP = 1;
        private static string strConnection = "Server=localhost;Database=reposteria;Uid=gio;Pwd=270299GPS";
        private static string product, size, quantityProduct;
        private static List<string> productsName = new List<string>();
        private static List<string> descriptionProduct = new List<string>();
        private static Dictionary<string, string> pricestProduct = new Dictionary<string, string>();
        private static Dictionary<string, string> pricesSpecialProduct = new Dictionary<string, string>();
        private static Dictionary<int, Dictionary<string, string>> specialProductsName = new Dictionary<int, Dictionary<string, string>>();
        private static Dictionary<string, Dictionary<string,string>> productsInfo = new Dictionary<string, Dictionary<string, string>>();
        private static Dictionary<Dictionary<string, string>, Dictionary<string, string>> specialProductsInfo = new Dictionary<Dictionary<string, string>, Dictionary<string, string>>();
        private static Dictionary<int, Dictionary<string, string>> carroProductos = new Dictionary<int, Dictionary<string, string>>();
    
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                productsContainer.Visible = false;
                lblConteoCarro.Text = "(0)";
                //tableCake.Visible = false;
            }
            if (Session["NoProductos"] != null)
            {
                lblConteoCarro.Text = ((int)Session["NoProductos"]).ToString();
            }
        }
        //Metodo para traer productos seleccionados desde el lado del cliente
        [WebMethod]
        public static string getInformationSelected(string nameProduct,string sizeSelected,string quantity)
        {
            product = nameProduct;
            size = sizeSelected;
            quantityProduct = quantity;
            return "Correct";
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
        public void btnAddOnClick(object sender, EventArgs e) //Botón añadir de las mesas de dulces
        {
            //Trae producto que se seleccionó
            string hiddenValue = Request.Form["hidden"];
            //Response.Write("Nombre producto: "+product+" Tamaño seleccionado: "+size+" Cantidad: "+quantityProduct);
            Dictionary<string, string> temporaryDictionary = new Dictionary<string, string>();
            temporaryDictionary.Add(size, quantityProduct);
            int idProduct = 0;
            idProduct = getIDProduct(product, size);
            //Se agrega el identificador del producto y el diccionario temporal al diccionario principal
            carroProductos.Add(idProduct, temporaryDictionary);
            //Código para aumentar el número mostrado al lado del carrito
            if (Session["NoProductos"] != null)
            {
                contadorP = (int)Session["NoProductos"] + 1;
            }
            lblConteoCarro.Text = "("+contadorP.ToString()+")";
            Session["NoProductos"] = contadorP;
            //Modal para corroborar que se metieron los datos de manera correcta
            Page.ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ID_Producto "+ idProduct+". Nombre Producto: "+ product +" Tamaño: "+ size +" Cantidad: "+quantityProduct+"',text: 'Bienvenido '})</script>");
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
        //Boton modal tiendas de dulces
        public void btnAddPackOnClick(object sender, EventArgs e) //Botón de añadir para productos excepto mesas de dulces
        {
            string hiddenValue = Request.Form["hiddenPack"];
            //Response.Write(hiddenValue);
            Dictionary<string, string> temporaryDictionay = new Dictionary<string, string>();
            temporaryDictionay.Add(size, quantityProduct);
            int idProduct = 0;
            idProduct = getIDProduct(product, size);
            //Agregamos el identificador del producto y el diccionario temporal al diccionario principal
            carroProductos.Add(idProduct, temporaryDictionay);
            if (Session["NoProductos"] != null)
            {
                contadorP = (int)Session["NoProductos"] + 1;
            }
            lblConteoCarro.Text = "(" + contadorP.ToString() + ")";
            Session["NoProductos"] = contadorP;
            //Modal para corroborar que se metieron los datos de manera correcta
            Page.ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ID_Producto " + idProduct + ". Nombre Producto: " + product + " Tamaño: " + size + " Cantidad: " + quantityProduct + "',text: 'Bienvenido '})</script>");
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
                    sb.Append("<button id='" + nameImage + "' type='button' class='btn btn-primary' onclick='getIDPack(this)'>Agregar</button>");
                    sb.Append("</div>");
                }
                sb.Append("</div>");
                sb.Append("</div>");
                //Se asigna la interfaz a la literal
                ltProduct.Text = sb.ToString();
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
    }
}