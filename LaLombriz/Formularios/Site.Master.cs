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
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void btnIngresarOnClick(object sender, EventArgs e)
        {
            lblOptions.Text = "Cerrar Sesión";
        }
    }
}