using System;
using LaLombriz.Clases;
using LaLombriz.Modelos;

namespace LaLombriz.Formularios
{
    public partial class Cotizaciones : System.Web.UI.Page
    {
        private static string strConnection = "Server=sql512.main-hosting.eu; Database=u119388885_reposteria;Uid=u119388885_gio;Pwd=270299Gp$2018";
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