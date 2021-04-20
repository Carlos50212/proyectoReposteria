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

        public Cotizacion() { }
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
        public List<Cotizacion> getCotizaciones(string strConnection, int estatus)
        {
            List<Cotizacion> cotizacion = new List<Cotizacion>();
            string query = "SELECT * FROM `cotizaciones` WHERE ESTATUS = " + estatus + "";
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;
            MySqlDataReader reader;
            try
            {
                dbConnection.Open();
                //Leemos los datos 
                reader = cmdDB.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read()) //asignamos datos 
                    {
                        cotizacion.Add(new Cotizacion(Convert.ToInt32(reader.GetString(0)), Convert.ToInt32(reader.GetString(1)), reader.GetString(2), Convert.ToInt32(reader.GetString(3)), Convert.ToDateTime(reader.GetString(4)), Convert.ToDateTime(reader.GetString(5)), reader.GetString(6), reader.GetString(7)));
                    }
                }
                dbConnection.Close();
                return cotizacion;
            }
            catch (Exception e)
            {
                //Mensaje de error
                Console.WriteLine("Error" + e);
                return cotizacion;
            }
        }
        public Cotizacion getCotizacion(string strConnection, int idCotizacion)
        {
            Cotizacion cotizacion =new Cotizacion();
            string query = "SELECT * FROM `cotizaciones` WHERE ID_COTIZACION = " + idCotizacion + "";
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;
            MySqlDataReader reader;
            try
            {
                dbConnection.Open();
                //Leemos los datos 
                reader = cmdDB.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read()) //asignamos datos 
                    {
                        cotizacion = new Cotizacion(Convert.ToInt32(reader.GetString(0)), Convert.ToInt32(reader.GetString(1)), reader.GetString(2), Convert.ToInt32(reader.GetString(3)), Convert.ToDateTime(reader.GetString(4)), Convert.ToDateTime(reader.GetString(5)), reader.GetString(6), reader.GetString(7));
                    }
                }
                dbConnection.Close();
                return cotizacion;
            }
            catch (Exception e)
            {
                //Mensaje de error
                Console.WriteLine("Error" + e);
                return cotizacion;
            }
        }
        public bool sendAnswer(string strConnection,int idCotizacion,string respuesta, int idUser, string fecha)
        {
            //Sentencia
            string query = "UPDATE `cotizaciones` SET `ID_ADMINISTRADOR`="+idUser+" ,`ESTATUS`=1,`FECHA_RESPUESTA`='"+fecha+"',`MSJ_ADMINISTRADOR`='"+respuesta+"' WHERE ID_COTIZACION ="+idCotizacion+"";
            //Conexiones 
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;

            try
            {
                //Abrir base de datos
                dbConnection.Open();
                //Insertamos
                MySqlDataReader myReader = cmdDB.ExecuteReader();
                //Cerramos base de datos 
                dbConnection.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
                return false;
            }
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
    }
}