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
            for(int i=0; i<productos; i++)
            {
                info = getInforProduct(idproductos[i]);
                string[] informacion = info.Split('/');
                sb.Append("<div id='" + i+1 + "Order'  class='oneProduct'>");
                sb.Append("<div class='containerDeleteOption'>");
                sb.Append("<div class='tableInformation'>");
                sb.Append("<table class='table table-borderless tableNewOrder'>");
                sb.Append("<thead>");
                sb.Append("<tr>");
                sb.Append("<th scope='col' style='width:200px'>Nombre de producto</th><th scope='col' style='width:200px'>Tamaño</th><th scope='col' style='width:200px'>Cantidad</th><th scope='col' style='width:200px'>Precio</th>");
                sb.Append("</tr>");
                sb.Append("</thead>");
                sb.Append("<tbody>");
                sb.Append("<tr>");
                sb.Append("<td>" + informacion[0] + "</td><td>" + informacion[1] + "</td><td>" + cantidadesProd[i] + "pz</td><td> $" + informacion[2] + "</td>");
                sb.Append("</tr>");
                sb.Append("</tbody>");
                sb.Append("</table>");
                sb.Append("</div>");
                sb.Append("</div>");
                tbProductsOrder.Text = sb.ToString();
                info = " ";
            }
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