using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LaLombriz.Formularios
{
    public partial class Recuperacion : System.Web.UI.Page
    {
        private static string strConnection = "Server=localhost;Database=reposteria;Uid=gio;Pwd=270299GPS";
        protected void Page_Load(object sender, EventArgs e)
        {
            string token = getToken();
            if (token.Length > 0)
            {
                int idUser = getIdUser(token);
                if (idUser > 0)
                {
                    string email = getEmail(idUser);
                    if (email.Length > 0)
                    {
                        txtCorreo.Text = email;
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Lo sentimos, algo salió mal'})</script>");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Lo sentimos, algo salió mal'})</script>");
                }

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: 'ERROR',text: 'Lo sentimos, algo salió mal'})</script>");
            }
        }
        //Metodo que recupera el token
        private string getToken()
        {
            string parametro = Request.QueryString["tk"];
            return parametro;
        }
        //Método para obtener el id del usuario
        private int getIdUser(string token)
        {
            string query = "SELECT ID_USUARIO FROM `recuperacion` WHERE TOKEN='" + token + "'";
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;
            MySqlDataReader reader;
            int idUser=0; 
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
        //Metodo para obtener el correo del usuario
        private string getEmail(int idUser)
        {
            string query = "SELECT CORREO FROM `usuarios` WHERE ID_USUARIO=" + idUser;
            MySqlConnection dbConnection = new MySqlConnection(strConnection);
            MySqlCommand cmdDB = new MySqlCommand(query, dbConnection);
            cmdDB.CommandTimeout = 60;
            MySqlDataReader reader;
            string email = "";
            try
            {
                dbConnection.Open();
                //Leemos los datos 
                reader = cmdDB.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        email = reader.GetString(0);

                    }
                }
                dbConnection.Close();
                return email;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                return email;
            }
        }
    }
}