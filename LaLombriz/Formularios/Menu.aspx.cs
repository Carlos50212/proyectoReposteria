using LaLombriz.Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LaLombriz.Formularios
{
    public partial class Menu : System.Web.UI.Page
    {
        private static string strConnection = "Server=localhost;Database=reposteria;Uid=gio;Pwd=270299GPS";
        protected void Page_Load(object sender, EventArgs e)
        {
            productsContainer.Visible = false;
            //optionsContainer.Visible = false;
        }
        //Boton pasteles
        public void btnCakeOnClick(object sender, EventArgs e)
        {
            showSecondForm();
            ArrayList arrayData = new ArrayList();
            ArrayList arraySize = new ArrayList();
            Productos producto = new Productos();
            arrayData=producto.getProducts(strConnection,1,15);
            arraySize =producto.getSize(strConnection,1,15);
            if (arrayData.Count != 0)
            {
                for(int contObject = 0; contObject < arrayData.Count; contObject++)
                {
                    ddlProducts.Items.Add(arrayData[contObject].ToString());
                }
                for(int contSize = 0; contSize < arraySize.Count; contSize++)
                {
                    ddlSize.Items.Add(arraySize[contSize].ToString());
                }
            }
            else
            {
                Response.Write("ERROR");
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
        //Metodo para mostrar tabla de productos
        public void drawTable(DropDownList ddlProducts,DropDownList ddlSize,bool product)
        {
            /*
            if (product)
            {
                DataTable tableProduct = new DataTable();
                tableProduct.Columns.Add("Productos", typeof(DropDownList));
                tableProduct.Columns.Add("Descripción", typeof(string));
                tableProduct.Columns.Add("Tamaño", typeof(DropDownList));
                tableProduct.Columns.Add("Precio", typeof(string));
                DataRow row = tableProduct.NewRow();
                row = tableProduct.NewRow();
                row["Productos"] = ddlProducts;
                row["Descripción"] = "_";
                row["Tamaño"] = ddlSize;
                row["Precio"] = "_";
                tableProduct.Rows.Add(row);
                tbtSelection.DataSource = tableProduct;
                tbtSelection.DataBind();
            }*/
        }
    }
}