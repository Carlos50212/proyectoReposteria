using System;
using LaLombriz.Clases;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Text;
using LaLombriz.Modelos;
using System.Net.Mail;

namespace LaLombriz.Formularios
{
    public partial class Login : System.Web.UI.Page
    {
        private static string strConnection = "Server=localhost;Database=reposteria;Uid=gio;Pwd=270299GPS";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //Boton crear cuenta
        public void btnCreateOnClick(object sender, EventArgs e)
        {
            Response.Redirect("Registrar.aspx");
        }
        //Boton Iniciar Sesión
        public void btnRecoverOnClick(object sender, EventArgs args)
        {
            string token = generateToken();
            int idUser = getIdUser(txtRecoverPass.Text);
            if (idUser > 0)
            {
                if (sendEmail(token, txtRecoverPass.Text))
                {
                    if (saveRecoverData(idUser, token))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "messageSuccess", "<script>Swal.fire({icon: 'success',title: 'Se le ha enviado un mensaje de verificación a su correo',showConfirmButton: true})</script>");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Lo sentimos, algo salió mal'})</script>");
                    }

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Lo sentimos, algo salió mal'})</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Lo sentimos, algo salió mal'})</script>");
            }
        }
        public void btnIngresarOnClick(object sender, EventArgs e)
        {
            if (CamposVacios())
            {
                //Colocar alerta de campos vacíos
            }
            else
            {
                if (ExpCorreo(txtCorreoLogin.Text))
                {
                    Usuario us1 = new Usuario(new UsuariosBD());
                    Usuario user = new Usuario(new UsuariosBD());

                    Security encripta = new Security();
                    string pass = "";
                    pass = encripta.encriptar(txtPasswdLogin.Text); //Encriptamos la contraseña ingresada para la BD 

                    user = us1.IniciarSesion(txtCorreoLogin.Text, pass, strConnection);

                    if (user != null)
                    {
                        //Datos asignados a la clase en base al inicio de sesión
                        txtCorreoLogin.Text = "";
                        txtPasswdLogin.Text = "";
                        Session["CORREO_USUARIO"] = user.Correo;
                        Session["ID_USUARIO"] = user.IdUsuario;
                        if(us1.getTypeUser(strConnection,user.IdUsuario) == 1)
                        {
                            Response.Redirect("Administrador/NewOrders.aspx");
                        }
                        else
                        {
                            Response.Redirect("Inicio.aspx");
                        }
                    }
                    else
                    {
                        //mostramos alerta o modal de datos incorrectos o usuario no registrado
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Usuario o contraseña incorrectos'})</script>");
                    }
                }
                else //formato incorrecto de correo
                {
                    //colocar alerta o modal de formato de correo incorrecto
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Formato de correo incorrecto'})</script>");
                }
            }
        }
        public bool CamposVacios() //Campos Vacíos
        {
           if(txtCorreoLogin.Text=="" || txtPasswdLogin.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ExpCorreo(string a) //Verificamos la validez del campo correo electronico
        {
            string correo = "", exp = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            correo = a;
            if (Regex.Match(correo, exp).Success)
                return true;
            else
                return false;
        }
        //Metodo para generar token
        private string generateToken()
        {
            string caracteres = "abcdefghijqlmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();
            for (int cont = 0; cont < 10; cont++)
            {
                sb.Append(caracteres[rnd.Next(caracteres.Length)]);
            }
            return sb.ToString();
        }
        //Metodo que regresa el id del usuario
        private int getIdUser(string correo)
        {
            string query = "SELECT ID_USUARIO FROM `usuarios` WHERE CORREO='" + correo + "'";
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;
            MySqlDataReader reader;
            int idUser = 0;
            try
            {
                dbConnection.Open();
                //Leemos los datos 
                reader = cmdDB.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        idUser = Convert.ToInt32(reader.GetString(0));

                    }
                }
                dbConnection.Close();
                return idUser;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                return idUser;
            }
        }
        //Metodo para almacenar token
        private bool saveRecoverData(int idUser, string token)
        {
            string query = "INSERT INTO recuperacion (`id_usuario`,`token`) VALUES ('" + idUser + "','" + token + "')";
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
        //Método para enviar token mediante correo
        public bool sendEmail(string token, string correo)
        {
            try
            {
                //Instanciamos de la clase mailmessage, el objeto servirá para agregar las partes de nuestro correo
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
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
                mail.To.Add(correo);
                //Asunto
                mail.Subject = "Recuperación de contraseña";
                //Cuerpo
                string texto = String.Format("<a href='{0}'>{0}</a>", "https://localhost:44393/Formularios/Recuperacion.aspx?tk=" + token);
                mail.Body = "Para recuperar tu contraseña, da click en el siguiente enlace: " + texto;
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