using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LaLombriz.Clases;
using LaLombriz.Modelos;

namespace LaLombriz.Formularios
{
    public partial class Cotizaciones : System.Web.UI.Page
    {
        private static string strConnection = "Server=localhost;Database=reposteria;Uid=gio;Pwd=270299GPS";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void btnEnviarCorreoOnClick(object sender, EventArgs e)
        {
            if(Session["CORREO_USUARIO"] != null)
            {
                //Verificamos si el textArea esta vacío o no
                if (txtDescription.Text != "")
                {
                    //Metodo para enviar correo   
                    if (sendText(txtDescription.Text))
                    {
                        //Mostramos mensaje de error
                        ClientScript.RegisterStartupScript(this.GetType(), "messageSuccess", "<script>Swal.fire({icon: 'success',title: 'Cotización enviada, en breve nos comunicaremos contigo',showConfirmButton: false,timer: 2500})</script>");
                        txtDescription.Text = "";
                    }
                    else
                    {
                        //Mostramos mensaje de error
                        ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Ocurrió un error, vuelve a intentarlo más tarde'})</script>");
                    }
                }
                else
                {
                    //Mostramos mensaje de error
                    ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Favor de llenar todos los campos'})</script>");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: '¡Oops!',text: 'Debes iniciar sesión antes'})</script>");
            }
        }
        //Metodo para enviar correo
        public bool sendText(string textDescription)
        {
            Cotizacion cotizacion = new Cotizacion(new CotizacionBD());
            int idUser = Convert.ToInt32(Session["ID_USUARIO"]);
            string date = DateTime.Now.ToString("yyyy-MM-dd");

            return cotizacion.sendText(idUser, date,textDescription,strConnection);
        }
    }
}