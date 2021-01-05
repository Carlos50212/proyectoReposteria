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
        private static string strConnection = "Server=localhost;Database=reposteria;Uid=gio;Pwd=270299GPS";
        private static string product, size, quantityProduct;
        private static List<string> productsName = new List<string>();
        private static List<string> descriptionProduct = new List<string>();
        private static Dictionary<string, string> pricestProduct = new Dictionary<string, string>();
        private static Dictionary<string, string> pricesSpecialProduct = new Dictionary<string, string>();
        private static Dictionary<int, Dictionary<string, string>> specialProductsName = new Dictionary<int, Dictionary<string, string>>();
        private static Dictionary<string, Dictionary<string,string>> productsInfo = new Dictionary<string, Dictionary<string, string>>();
        private static Dictionary<Dictionary<string, string>, Dictionary<string, string>> specialProductsInfo = new Dictionary<Dictionary<string, string>, Dictionary<string, string>>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                productsContainer.Visible = false;
                //tableCake.Visible = false;
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
            productsName = getProducts(0, 15);
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
            productsName = getProducts(16, 23);
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
            productsName = getProducts(52, 56);
            descriptionProduct = getDescriptionProducts(52, 56);
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
            productsName = getProducts(24,44);
            //Se traen los productos "anormales" aquellos que se llamen igual, pero difieran en descripción
            specialProductsName = getSpecialProducts(45,51);
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
            //Se valida que haya datos en el diccionario
            if (specialProductsName.Count > 0)
            {
                //Se recorre el diccionario
                foreach(KeyValuePair<int,Dictionary<string,string>> specialProduct in specialProductsName)
                {
                    //Se recorre el diccionario que se encuentra en la posicion "value" del diccionario padre
                    foreach (KeyValuePair<string,string> secondDictionary in specialProduct.Value)
                    {
                        //Se buscan los precios del producto
                        pricesSpecialProduct = getPricesProduct(secondDictionary.Key);
                        //Se valida que haya datos en la lista
                        if (pricesSpecialProduct.Count > 0)
                        {
                            //Se hace una lista donde se agregar el KeyValuePair a dicha lista "nota: se hace esto por que, secondDictionary el cual es el diccionario que tiene los datos de un producto en específico, es de tipo KeyValuePairs y no hay una conversión directa de KeyValuePairs a Dictionary"
                            var list = new List<KeyValuePair<string, string>> { secondDictionary };
                            //Se convierte la lista obtenida a diccionario, aplicando funciones lambda decimos cual es la llave y cual es el valor (primer valor del KeyValuePairs es la llave y el segundo el valor). 
                            var dictionary = list.ToDictionary(x => x.Key, x => x.Value);
                            //Se agrega el diccionario obtenido y el diccionario de precios al diccionario de productos en general (ese diccionario se usará para imprimir los datos en la interfaz)
                            specialProductsInfo.Add(dictionary, pricesSpecialProduct);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Ocurrió un error, vuelve a intentarlo más tarde'})</script>");
                        }
                    }
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Ocurrió un error, vuelve a intentarlo más tarde'})</script>");
            }
            //Se dibuja la interfaz "especial" para aquellos productos que tengan descripción y nombre
            drawSpecialInterface();
            //Se dibuja la interfaz para los productos "normales"
            drawInterface(0, descriptionProduct);
        }
        public void btnAddOnClick(object sender, EventArgs e)
        {
            //Trae producto que se seleccionó
            string hiddenValue = Request.Form["hidden"];
            Response.Write("Nombre producto: "+product+" Tamaño seleccionado: "+size+" Cantidad: "+quantityProduct);
        }
        //Boton modal tiendas de dulces
        public void btnAddPackOnClick(object sender, EventArgs e)
        {
            string hiddenValue = Request.Form["hiddenPack"];
            Response.Write(hiddenValue);
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
                sb.Append("<h2>" + product.Key + "</h2>");
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
                    sb.Append("<button id='"+nameImage+"' type='button' class='btn btn-primary' onclick='getID(this)'>Agregar</button>");
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
                    sb.Append("<button id='" + nameImage + "' type='button' class='btn btn-primary' onclick='getIDPack(this)'>Agregar</button>");
                }
                sb.Append("</div>");
                sb.Append("</div>");
                //Se asigna la interfaz a la literal
                ltProduct.Text = sb.ToString();
            }   
        }
        //Metodo para dibujar interfaz de productos "otros"
        public void drawSpecialInterface()
        {
            StringBuilder sb = new StringBuilder();
            //Recorre el diccionario que trae dos diccionarios, el primer diccionario trae "nombre producto" y "descripción", el segundo diccionario trae "tamaños(grande, mediano, chico)" y su "precio" respectivo
            foreach (KeyValuePair<Dictionary<string,string>, Dictionary<string, string>> productInfo in specialProductsInfo)
            {
                //Se lee el primer diccionario (nombre producto, descripción)
                foreach(KeyValuePair<string,string> product in productInfo.Key)
                {
                    //Del nombre producto, reemplazamos todos los espacios por un _
                    string nameImage = product.Key.Replace(" ", "_");
                    //Se concatena nameImage con un "." y se reemplazan los espacios de la descripción, con un _
                    string nameProduct = nameImage +"."+product.Value.Replace(" ","_");
                    //Creamos tarjetas
                    sb.Append("<div id='" + product.Key + "' class='infoProduct row col-xs-12 col-md-6'>");
                    sb.Append("<div id='image-container'class='imageContainer col-xs-12 col-md-6' style='background-image:url(../Recursos/Menu/" + nameImage + ".jpg);'>");
                    sb.Append("</div>");
                    sb.Append("<div id='info-container' class='productContainer col-xs-12 col-md-6'>");
                    //Mostramos nombre de producto
                    sb.Append("<h2>" + product.Key + "</h2>");
                    sb.Append("<h4>Descripción</h4>");
                    //Mostramos descripción del producto
                    sb.Append("<p style='text-align:justify;'>" + product.Value + "</p>");
                    //Mostramos precios del producto
                    sb.Append("<h4>Precios</h4>");
                    sb.Append("</br>");
                    //Recorremos el segundo diccionario (tamaño,precio)
                    foreach (KeyValuePair<string, string> prices in productInfo.Value)
                    {
                        ddlTamanio.Items.Add(prices.Key);
                        //Imprimimos tamaño y precio
                        sb.Append("<span style='float:left'>" + prices.Key + "</span><span style='float:right'>" + prices.Value + "</span></br></br>");
                    }
                    sb.Append("<button id='" + nameProduct + "' type='button' class='btn btn-primary' onclick='getID(this)'>Agregar</button>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    //Agregamos la interfaz a la literal
                    ltProductSpecial.Text = sb.ToString();
                }
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
            string query = "SELECT NOMBRE_PRODUCTO,DESCRIPCION FROM productos where (ID_PRODUCTO BETWEEN "+val1+" AND "+ val2 + ")";
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