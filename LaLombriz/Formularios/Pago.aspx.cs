using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LaLombriz.Formularios
{
    public partial class Pago : System.Web.UI.Page
    {
        private static string strConnection = "Server=localhost;Database=reposteria;Uid=gio;Pwd=270299GPS";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                transactionAmount.Value = Session["Monto"].ToString();
                correoUser.Value = Session["CORREO_USUARIO"].ToString();
                drawInterfaceCart();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'warning',title: '¡Solo un paso más!',text: 'TOTAL A PAGAR: " + Session["Monto"].ToString() + "  CADENA DE IDS: " + Session["IDs_Prdocutos"].ToString() + "  CADENA DE CANTIDADES: " + Session["Cantidades_Productos"].ToString() + "'})</script>");
            }
        }
        private void drawInterfaceCart()
        {
            int productos = 0;
            string info = " ";
            string cadenaID = Session["IDs_Prdocutos"].ToString();
            string cadenaCantidades = Session["Cantidades_Productos"].ToString();
            string[] idproductos = cadenaID.Split('/');
            string[] cantidadesProd = cadenaCantidades.Split('/');
            productos = Convert.ToInt32(Session["NoProductos"]);
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id='allDescription'>");
            sb.Append("<div class='tableInformation'>");
            sb.Append("<table class='table table-borderless tableNewOrder'>");
            sb.Append("<thead>");
            sb.Append("<tr>");
            sb.Append("<th scope='col' style='width:200px;padding-left: 20px;'>Descripción</th><th scope='col' style='width:200px;text-align:center;'>Precio</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            sb.Append("<tbody>");
            for (int i = 0; i < productos; i++)
            {
                info = getInforProduct(idproductos[i]);
                string[] informacion = info.Split('/');
                sb.Append("<tr id='" + i + 1 + "Order' class='oneProduct'>");
                sb.Append("<td style='padding-left: 20px;'>" + informacion[0] + " - " + informacion[1] +" (x"+ cantidadesProd[i] + ")</td><td style='text-align:center;'> $" + informacion[2] + "</td>");
                sb.Append("</tr>");
                info = " ";
            }
            sb.Append("<tr id='totalOrder'>");
            sb.Append("<td style='text-align:right;'><b>Total: </b></td><td style='text-align:center;'> $Aquí va el total</td>");
            sb.Append("</tr>");
            sb.Append("</tbody>");
            sb.Append("</table>");
            sb.Append("</div>");
            sb.Append("</div>");
            tbProductsOrder.Text = sb.ToString();
        }
        public string getInforProduct(string id) //Recuperamos la info del producto
        {
            string informacion = "";
            string query = "SELECT NOMBRE_PRODUCTO, TAMANIO, PRECIO  FROM productos where ID_PRODUCTO='" + id + "'";
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
                        if (reader.GetString(1) == "")
                        {
                            informacion = reader.GetString(0) + "/" + "-" + "/" + reader.GetString(2);
                        }
                        else
                        {
                            informacion = reader.GetString(0) + "/" + reader.GetString(1) + "/" + reader.GetString(2);
                        }
                        
                    }
                }
                dbConnection.Close();
                return informacion;
            }
            catch (Exception e)
            {
                //Mensjae de error
                Console.WriteLine("Error" + e);
                return " ";
            }
        }

    }
}