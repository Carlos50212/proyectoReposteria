using LaLombriz.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

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