using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LaLombriz.Formularios
{
    public partial class Cotizaciones : System.Web.UI.Page
    {
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
                    if (sendEmail(txtDescription.Text))
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
        public bool sendEmail(string textDescription)
        {
            try
            {
                //Instanciamos de la clase mailmessage, el objeto servirá para agregar las partes de nuestro correo
                MailMessage mail = new MailMessage();
                //Indicamos el servidor de correo y puerto con el que trabaja gmail
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com", 587)
                {
                    //Vuelve a nulo el valor de credenciales, esto permitirá usar nuestras propias credenciales
                    UseDefaultCredentials = false,
                    //Se indican las credenciales de la cuenta gmail que ocuparemos para enviar el correo
                    Credentials = new System.Net.NetworkCredential("noreplylalombriz@gmail.com", "lalombrizAP"),
                    //Método de entrega
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    //Habilitar seguridad en smtp
                    EnableSsl = true,
                };
                //Creamos el correo
                //Indicamos de donde viene el correo
                mail.From = new MailAddress("noreplylalombriz@gmail.com");
                //Indicamos dirección destino
                mail.To.Add("giovocatrece@gmail.com");
                //Asunto
                mail.Subject = "Cotización";
                //Cuerpo
                mail.Body = textDescription;
                //Enviamos el email 
                smtpServer.Send(mail);
                return true;
            }catch(Exception e)
            {
                Console.WriteLine("ERROR " + e);
                return false;
            }
        }
    }
}