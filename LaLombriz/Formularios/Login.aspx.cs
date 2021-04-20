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
                    Usuario us1 = new Usuario();
                    Security encripta = new Security();
                    string pass = "";
                    pass = encripta.encriptar(txtPasswdLogin.Text); //Encriptamos la contraseña ingresada para la BD 
                    if (us1.IniciarSesion(txtCorreoLogin.Text, pass, strConnection))
                    {
                        //Datos asignados a la clase en base al inicio de sesión
                        txtCorreoLogin.Text = "";
                        txtPasswdLogin.Text = "";
                        Session["CORREO_USUARIO"] = us1.Correo;
                        Session["ID_USUARIO"] = us1.IdUsuario;
                        if(us1.getTypeUser(strConnection,us1.IdUsuario) == 1)
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
    }
}