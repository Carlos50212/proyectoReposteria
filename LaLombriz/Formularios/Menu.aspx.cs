using LaLombriz.Clases;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LaLombriz.Formularios
{
    public partial class Menu : System.Web.UI.Page
    {
        private static string strConnection = "Server=localhost;Database=reposteria;Uid=gio;Pwd=270299GPS";
        private static List<Productos> smallProducts = new List<Productos>();
        private static List<Productos> mediumProducts = new List<Productos>();
        private static List<Productos> bigProducts = new List<Productos>();
        public static int index = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                productsContainer.Visible = false;
                tableCake.Visible = false;
            }
        }
        //Boton pasteles
        public void btnCakeOnClick(object sender, EventArgs e)
        {
            showSecondForm();
            tableCake.Visible = true;
            smallProducts=getProducts(0, 15,"chico");
            mediumProducts = getProducts(0, 15, "mediano");
            bigProducts = getProducts(0, 15, "grande");
            if (smallProducts.Count > 0 && mediumProducts.Count>0 && bigProducts.Count>0)
            {
                showAllTable(smallProducts,1,mediumProducts,2,bigProducts,3);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Ocurrió un error, vuelve a intentarlo más tarde'})</script>");
            }

        }
        //Boton macarons
        public void btnBurgerOnClick(object sender, EventArgs e)
        {
            showSecondForm();
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
        public void showSecondForm()
        {
            optionsContainer.Visible = false;
            productsContainer.Visible = true;
        }
        //Metodo para traer productos y llenar gridview
        public List<Productos> getProducts(int min, int max,string size)
        {
            string query = "SELECT * FROM `productos` WHERE (id_producto BETWEEN " + min + " AND " + max + ") AND tamanio='"+size+"'";
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;
            MySqlDataReader reader;
            List<Productos> listProduct = new List<Productos>();
            try
            {
                dbConnection.Open();
                //Leemos los datos 
                reader = cmdDB.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        listProduct.Add(new Productos(Convert.ToInt32(reader.GetString(0)), reader.GetString(1), reader.GetString(2),reader.GetString(3),float.Parse(reader.GetString(4),CultureInfo.InvariantCulture.NumberFormat)));
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
        //Metodo para dibujar las tablas
        public void showAllTable(List<Productos> smallProducts,int firstFlag, List<Productos> mediumProducts,int secondFlag, List<Productos> bigProducts,int thirdFlag)
        {
            drawSizeTable(smallProducts,firstFlag);
            drawSizeTable(mediumProducts,secondFlag);
            drawSizeTable(bigProducts, thirdFlag);
        }
        //Dibuja tablas de productos que tengan varios tamaños
        public void drawSizeTable(List<Productos> productos,int flag)
        {
            if(flag==1)
            {
                methodDrawTable(tbtSmallSizeG, productos);
            }
            if (flag == 2)
            {
                methodDrawTable(tbtMediumSizeP, productos);
            }
            if (flag == 3)
            {
                methodDrawTable(tbtBigSizeP, productos);
            }
        }
        //Metodo para dibujar tabla
        public void methodDrawTable(GridView gridview, List<Productos> productos)
        {
            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[2] { new DataColumn("Producto", typeof(string)), new DataColumn("Precio", typeof(float)) });
            DataRow row;
            StringBuilder btnSee = new StringBuilder();
            btnSee.Append("<button type='button' id='btnSeeDescription' class='btn btn-link' data-bs-toggle='modal' data-bs-target='#showDescription'>Ver</button>");
            for (int contList = 0; contList < productos.Count; contList++)
            {
                row = table.NewRow();
                row["Producto"] = productos[contList].Nombre_producto;
                row["Precio"] = productos[contList].Precio;
                table.Rows.Add(row);
            }
            gridview.DataSource = table;
            gridview.DataBind();
        }
        //Ver que fila se seleccionó
        public void selecRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "seeSmall")
            {
                index = Convert.ToInt32(e.CommandArgument.ToString());
                showDescription(smallProducts[index].Nombre_producto, smallProducts[index].Descripcion);
            }
            if(e.CommandName == "seeMediumCake")
            {
                index = Convert.ToInt32(e.CommandArgument.ToString());
                showDescription(mediumProducts[index].Nombre_producto, mediumProducts[index].Descripcion);
            }
            if(e.CommandName == "seeBigCake")
            {
                index = Convert.ToInt32(e.CommandArgument.ToString());
                showDescription(bigProducts[index].Nombre_producto, bigProducts[index].Descripcion);
            }
        }
        public void showDescription(string nombre,string description)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "description", "<script>Swal.fire({title: '"+nombre+"',text: '"+description+"'})</script>");
        }
    }
}