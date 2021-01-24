using LaLombriz.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Text;
using System.Net.Mail;
using MySql.Data.MySqlClient;

namespace LaLombriz
{
    public partial class SiteMaster : MasterPage
    {
        private static string strConnection="Server=localhost;Database=reposteria;Uid=gio;Pwd=270299GPS";
        Usuario us1 = new Usuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblOptions.Text = "Iniciar Sesión";
                //tableCake.Visible = false;
            }
            if(lblOptions.Text=="Iniciar Sesión")
            {
                Session["CORREO_USUARIO"] = null;
            }
            if (Session["CORREO_USUARIO"] != null)
            {
                lblOptions.Text = "Cerrar Sesión";
            }
        }
        public void btnIngresarOnClick(object sender, EventArgs e)
        {
                if (CamposVaciosInicio())
                {
                    //colocar alerta o modal de campos vacíos 
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Favor de llenar todos los campos'})</script>"); 
                }
                else
                {
                    if (ExpCorreo(txtCorreoI.Text))
                    {
                        Security encripta = new Security();
                        string pass = "";
                        pass = encripta.encriptar(txtContraseniaI.Text); //Encriptamos la contraseña ingresada para la BD 
                        if (us1.IniciarSesion(txtCorreoI.Text, pass, strConnection))
                        {
                            //Datos asignados a la clase en base al inicio de sesión
                            txtCorreoI.Text = "";
                            txtContraseniaI.Text = "";
                            Session["CORREO_USUARIO"] = us1.Correo;
                            lblOptions.Text = "Cerrar Sesión";
                        }
                        else
                        {
                            //mostramos alerta o modal de datos incorrectos o usuario no registrado
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Usuario o contraseña incorrectos'})</script>");
                        }
                    }
                    else
                    {
                        //colocar alerta o modal de formato de correo incorrecto
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Formato de correo incorrecto'})</script>");
                    }
                }
        }
        public void btnRegistrarOnClick(object sender, EventArgs e)
        {
            if (CamposVacios())
            {
                //Colocar mensaje de alerta o modal indicando que alguno de los campos se encuentra vacío 
                Page.ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Favor de llenar todos los campos'})</script>");
            }
            else
            {
                if (ExpCorreo(txtCorreroRegistro.Text)) //Verificamos la validez del campo correo electronico
                {
                    //validación para el correo ya ingresado (evitar correos dobles) 
                    Usuario prueba = new Usuario(txtCorreroRegistro.Text);
                    if (prueba.CorreoDoble(strConnection))
                    {
                        //Alerta o modal avisando que el correo ya está registrado
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Ya existe un usuario con ese correo electrónico'})</script>");
                    }
                    else
                    {
                        string pass = "";
                        Security encriptador = new Security();
                        pass = encriptador.encriptar(txtPasswordRegistro.Text);
                        Usuario usuario = new Usuario(txtNombreUser.Text, txtCorreroRegistro.Text, pass, txtNoTel.Text);
                        if (usuario.createUser(strConnection))
                        {
                            txtNombreUser.Text = "";
                            txtCorreroRegistro.Text = "";
                            txtPasswordRegistro.Text = "";
                            txtNoTel.Text = "";
                            //Colocar alerta o mensaje modal de registro exitoso
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "messageSuccess", "<script>Swal.fire({icon: 'success',title: '¡Registro éxitoso!',showConfirmButton: false,timer: 2500})</script>");
                        }
                        else
                        {
                            txtNombreUser.Text = "";
                            txtCorreroRegistro.Text = "";
                            txtPasswordRegistro.Text = "";
                            txtNoTel.Text = "";
                            //Colocar alerta o mensaje modal de fallo al registrarse
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Lo sentimos, algo salió mal'})</script>");
                        }
                    }
                }
                else
                {
                    //Mostramos mensaje de error de formato en el correo
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Formato de correo incorrecto'})</script>");
                }
            }
        }
        //Método on click para recuperar contraseña
        public void btnRecoverOnClick(object sender, EventArgs args)
        {
            string token = generateToken();
            int idUser = getIdUser(txtRecoverPass.Text);
            if (idUser > 0)
            {
                if (sendEmail(token, txtRecoverPass.Text))
                {
                    if(saveRecoverData(idUser, token))
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
        //Metodo para generar token
        private string generateToken()
        {
            string caracteres = "abcdefghijqlmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();
            for(int cont = 0; cont <10 ; cont++)
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
        private bool saveRecoverData(int idUser,string token)
        {
            string query = "INSERT INTO recuperacion (`id_usuario`,`token`) VALUES ('" + idUser + "','" + token+ "')";
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
                mail.Body = "Para recuperar tu contraseña, da click en el siguiente enlace: "+texto;
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
        public bool ExpCorreo(string a) //Verificamos la validez del campo correo electronico
        {
            string correo = "", exp = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            correo = a;
            if (Regex.Match(correo, exp).Success)
                return true;
            else
                return false;
        }
        public bool CamposVacios() //Función para validar campos vacíos al registrar usuario
        {
            if (txtNombreUser.Text == "" || txtCorreroRegistro.Text == "" || txtPasswordRegistro.Text == "" || txtNoTel.Text == "")
                return true;
            else
                return false;
        }
        public bool CamposVaciosInicio() //Función para validar campos vacíos al iniciar sesión
        {
            if (txtCorreoI.Text == "" || txtContraseniaI.Text == "")
                return true;
            else
                return false;
        }
    }
}