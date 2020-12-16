using LaLombriz.Clases;
using System;
using System.Collections;
using System.Collections.Generic;
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
            Productos producto = new Productos();
            arrayData=producto.getProductsName(strConnection);
            if (arrayData.Count != 0)
            {
                Response.Write("Hay datos");
            }
            else
            {
                Response.Write("ERROR");
            }
            //ArrayList que almacena todos los datos obtenidos de la consulta 

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
    }
}