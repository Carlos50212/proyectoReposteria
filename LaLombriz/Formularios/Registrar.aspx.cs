using LaLombriz.Clases;
using System;
using System.Text.RegularExpressions;
using LaLombriz.Modelos;

namespace LaLombriz.Formularios
{
    public partial class CrearCuenta : System.Web.UI.Page
    {
        private static string strConnection = "Server=sql512.main-hosting.eu; Database=u119388885_reposteria;Uid=u119388885_gio;Pwd=270299Gp$2018";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //Botón regresar a login
        public void btnBackOnClick(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
        //Botón crear cuenta
        public void btnCrearOnClick(object sender, EventArgs e)
        {
            if (CamposVacios())
            {
                //Mensaje de que alguno de los campos se encuentran vacíos
                ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: '¡Oops!',text: 'Todos los campos deben ser llenados'})</script>");
            }
            else
            {
                if (FormatoCorreo(txtUserEmail.Text)) //Validamos el campo de correo
                {
                    //Comprobamos que el contenido del campo pass sea igual
                    if (CompPass(txtUserPasswd.Text, txtConfirmPasswd.Text))
                    {
                        //Las contraseñas coinciden y verificamos que el correo no este registrado previamente
                        Usuario prueba = new Usuario(new UsuariosBD(),txtUserEmail.Text);
                        if (prueba.CorreoDoble(strConnection))
                        {
                            //Alerta o modal avisando que el correo ya está registrado
                           ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Ya existe un usuario con ese correo electrónico'})</script>");
                        }
                        else
                        {
                            string pass = "";
                            Security encriptador = new Security();
                            pass = encriptador.encriptar(txtUserPasswd.Text);
                            Usuario usuario = new Usuario(new UsuariosBD(),txtUserName.Text, txtUserEmail.Text, pass, txtUserPhone.Text);
                            if (usuario.createUser(strConnection))
                            {
                                txtUserName.Text = "";
                                txtUserEmail.Text = "";
                                txtUserPasswd.Text = "";
                                txtConfirmPasswd.Text = "";
                                txtUserPhone.Text = "";
                                //Colocar alerta o mensaje modal de registro exitoso
                                ClientScript.RegisterStartupScript(this.GetType(), "messageSuccess", "<script>Swal.fire({icon: 'success',title: '¡Registro éxitoso!',showConfirmButton: false,timer: 2500})</script>");
                            }
                            else
                            {
                                txtUserName.Text = "";
                                txtUserEmail.Text = "";
                                txtUserPasswd.Text = "";
                                txtConfirmPasswd.Text = "";
                                txtUserPhone.Text = "";
                                //Colocar alerta o mensaje modal de fallo al registrarse
                                ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Lo sentimos, algo salió mal'})</script>");
                            }

                        }
                    }
                    else
                    {
                        //Mensaje de que las contraseñas no coinciden
                        ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: '¡Oops!',text: 'Las contraseñas ingresadas no coinciden'})</script>");
                    }
                }
                else
                {
                    //Mensaje de que el formato del correo no coincide o es incorrecto
                    ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'Lo sentimos',text: 'No podemos reconocer el formato de correo que has ingresado'})</script>");
                }
            }

        }
        //Validar campos vacíos
        public bool CamposVacios()
        {
            if(txtUserName.Text == " " || txtUserEmail.Text =="" || txtUserPhone.Text == "" || txtUserPasswd.Text =="" || txtConfirmPasswd.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Validar las contraseñas
        public bool CompPass(string a, string b)
        {
            if (a == b)
                return true;
            else
                return false;
        }
        //Validamos el correo del usuario
        public bool FormatoCorreo(string a)
        {
            string correo = "", exp = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            correo = a;
            if (Regex.Match(correo, exp).Success)
                return true;
            else
                return false;
        }

    }
}