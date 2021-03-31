using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LaLombriz.Formularios
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //Boton iniciar sesión
        public void btnCreateOnClick(object sender, EventArgs e)
        {
            Response.Redirect("Registrar.aspx");
        }
    }
}