using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaLombriz.Formularios
{
    public partial class Menu : System.Web.UI.Page
    {
        private static string strConnection = "Server=localhost;Database=reposteria;Uid=gio;Pwd=270299GPS";
        private static List<string> productsName = new List<string>();
        private static Dictionary<string, string> pricestProduct = new Dictionary<string, string>();
        private static Dictionary<string, Dictionary<string,string>> productsInfo = new Dictionary<string, Dictionary<string, string>>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                productsContainer.Visible = false;
                //tableCake.Visible = false;
            }
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
            drawInterface();
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
            drawInterface();
        }
        //Boton mesas de dulces
        public void btnPackOnClick(object sender,EventArgs e)
        {
            showSecondForm();
        }
        //Boton otros
        public void btnOtherOnClick(object sender, EventArgs e)
        {
            showSecondForm();
        }
        public void btnAddOnClick(object sender, EventArgs e)
        {
            //Trae producto que se seleccionó
            string hiddenValue = Request.Form["hidden"];
            Response.Write(hiddenValue);
        }
        public void showSecondForm()
        {
            optionsContainer.Visible = false;
            productsContainer.Visible = true;
        }
        public void clearCollections()
        {
            productsName.Clear();
            productsInfo.Clear();
            pricestProduct.Clear();
        }
        //Método para mostrar productos
        public void drawInterface()
        {
            StringBuilder sb = new StringBuilder();
            foreach(KeyValuePair<string,Dictionary<string,string>> product in productsInfo)
            {
                string nameImage = product.Key.Replace(" ", "_");
                sb.Append("<div id='"+product.Key+"' class='infoProduct row col-xs-12 col-md-6'>");
                sb.Append("<div id='image-container'class='imageContainer col-xs-12 col-md-6' style='background-image:url(../Recursos/Menu/"+nameImage+".jpg);'>");
                sb.Append("</div>");
                sb.Append("<div id='info-container' class='productContainer col-xs-12 col-md-6'>");
                sb.Append("<h2>" + product.Key + "</h2>");
                sb.Append("<h4>Precios</h4>");
                sb.Append("</br>");
                foreach (KeyValuePair<string,string> prices in product.Value)
                {
                    sb.Append("<span style='float:left'>" + prices.Key+"</span><span style='float:right'>"+prices.Value+ "</span></br></br>");
                }
                sb.Append("<button id='"+nameImage+ "' type='button' class='btn btn-primary' onclick='getID(this)'>Agregar</button>");
                sb.Append("</div>");
                sb.Append("</div>");
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
                        //listProduct.Add(new Productos(Convert.ToInt32(reader.GetString(0)), reader.GetString(1), reader.GetString(2),reader.GetString(3),float.Parse(reader.GetString(4),CultureInfo.InvariantCulture.NumberFormat)));
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
    }
}