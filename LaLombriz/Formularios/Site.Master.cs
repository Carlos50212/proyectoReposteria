using LaLombriz.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LaLombriz
{
    public partial class SiteMaster : MasterPage
    {
        private static string strConnection="Server=localhost;Database=reposteria;Uid=gio;Pwd=270299GPS";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void btnIngresarOnClick(object sender, EventArgs e)
        {
            lblOptions.Text = "Cerrar Sesión";
        }
        public void btnRegistrarOnClick(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario(1,txtNombreUser.Text,txtCorreroRegistro.Text,txtPasswordRegistro.Text,txtNoTel.Text);
            if (usuario.createUser(strConnection))
            {
                Response.Write("Bien");
            }
            else
            {
                Response.Write("No bien");
            }
        }
    }
}