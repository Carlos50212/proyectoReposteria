using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LaLombriz.Formularios
{
    public partial class Galeria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showGallery();
            }
        }
        public void showGallery()
        {
            StringBuilder sb = new StringBuilder();
            for (int numImage = 1; numImage <= 11; numImage++)
            {
                sb.Append("<div class='galleryItem'>");
                sb.Append("<img id='imagen"+numImage+"' src='/Recursos/Galeria/imagen" + numImage + ".jpg' alt='' class='galleryImg imgEffect' onclick='getImage(this);'/>");
                sb.Append("</div>");
            }
            galleryGrid.Text = sb.ToString();
        }
    }
}