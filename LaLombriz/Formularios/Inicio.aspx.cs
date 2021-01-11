using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LaLombriz.Formularios
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //Boton menu
        public void btnMenuOnClick(object sender, EventArgs e)
        {
            Response.Redirect("Menu.aspx");
        }
        //Boton pedidos
        public void btnPedidosOnClick(object sender, EventArgs e)
        {
            Response.Redirect("Pedidos.aspx");
        }
        //Boton cotizacion
        public void btnCotizacionOnClick(object sender,EventArgs e)
        {
            Response.Redirect("Cotizaciones.aspx");
        }
        //Boton galeria
        public void btnGaleriaOnClick(object sender, EventArgs e)
        {
            Response.Redirect("Galeria.aspx");

        }
    }
}