using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LaLombriz.Formularios
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            productsContainer.Visible = false;
            //optionsContainer.Visible = false;
        }
        //Boton pasteles
        public void btnCakeOnClick(object sender, EventArgs e)
        {
            optionsContainer.Visible = false;
            productsContainer.Visible = true;
        }
        //Boton macarons
        public void btnBurgerOnClick(object sender, EventArgs e)
        {
            optionsContainer.Visible = false;
            productsContainer.Visible = true;
        }
        //Boton mesas de dulces
        public void btnPackOnClick(object sender,EventArgs e)
        {
            optionsContainer.Visible = false;
            productsContainer.Visible = true;
        }
        //Boton otros
        public void btnOtherOnClick(object sender, EventArgs e)
        {
            optionsContainer.Visible = false;
            productsContainer.Visible = true;
        }
    }
}