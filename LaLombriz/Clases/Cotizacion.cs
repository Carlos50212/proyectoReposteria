using LaLombriz.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace LaLombriz.Clases
{
    public class Cotizacion
    {
        private int idCotizacion;
        private int idCliente;
        private string idAdministrador;
        private int estatus;
        private DateTime fechaContacto;
        private DateTime fechaRespuesta;
        private string mensaje;
        private string respuesta;
        private CotizacionBD cotizacionBD;

        public Cotizacion() { }
        public Cotizacion(CotizacionBD cotizacionBD)
        {
            this.cotizacionBD = cotizacionBD;
        }
        public Cotizacion(int idCotizacion,int idCliente,string idAdministrador,int estatus,DateTime fechaContacto,DateTime fechaRespuesta,string mensaje,string respuesta)
        {
            this.idCotizacion = idCotizacion;
            this.idCliente = idCliente;
            this.idAdministrador = idAdministrador;
            this.estatus = estatus;
            this.fechaContacto = fechaContacto;
            this.fechaRespuesta = fechaRespuesta;
            this.mensaje = mensaje;
            this.respuesta = respuesta;
        }

        public int IdCotizacion { set { idCotizacion = value; } get { return idCotizacion; } }
        public int IdCliente { set { idCliente = value; } get { return idCliente; } }
        public string IdAdministrador { set { idAdministrador = value; } get { return idAdministrador; } }
        public int Estatus { set { estatus = value; } get { return estatus; } }
        public DateTime FechaContacto { set { fechaContacto = value; } get { return fechaContacto; } }
        public DateTime FechaRespuesta { set { fechaRespuesta = value; } get { return fechaRespuesta; } }
        public string Mensaje { set { mensaje = value; } get { return mensaje; } }
        public string Respuesta { set { respuesta = value; } get { return respuesta; } }
        public virtual List<Cotizacion> getCotizaciones(string strConnection, int estatus)
        {
            List<Cotizacion> cotizacion = new List<Cotizacion>();
            cotizacion = this.cotizacionBD.getCotizacionesModel(strConnection,estatus);
            return cotizacion;
        }

        public virtual Cotizacion getCotizacion(string strConnection, int idCotizacion)
        {
            Cotizacion cotizacion = this.cotizacionBD.getCotizacionModel(strConnection, idCotizacion);
            return cotizacion;
        }
        public virtual bool sendAnswer(string strConnection,int idCotizacion,string respuesta, int idUser, string fecha)
        {
            return this.cotizacionBD.sendAnswerModel(strConnection, idCotizacion, respuesta, idUser, fecha);
        }
        public bool sendEmail(string to,string respuesta,string mensaje)
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
                mail.To.Add(to);
                //Asunto
                mail.Subject = "Respuesta cotización";
                //Cuerpo
                mail.Body = "En La Lombriz es un gusto procurar que nuestros clientes siempre reciban la mejor atención, el presente correo es para dar seguimiento a la cotización: \n"+mensaje+ "\n\n" +
                    "A continuación, puede visualizar la respuesta que nuestro equipo le ha otorgado: \n" + respuesta+ "\n";
                //Enviamos el email 
                smtpServer.Send(mail);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR " + e);
                return false;
            }
        }

        public virtual bool sendText(int idUser,string date,string description, string strConnection)
        {
            bool isSaved = this.cotizacionBD.sendTextModel(idUser, date, description, strConnection);


            return isSaved;
        }
    }
}