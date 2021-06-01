using MySql.Data.MySqlClient;
using System;
using LaLombriz.Clases;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Resource.Payment;
using System.Net;

namespace LaLombriz.Formularios
{
    public partial class Pago : System.Web.UI.Page
    {
        private static string strConnection = "Server=localhost;Database=reposteria;Uid=gio;Pwd=270299GPS";
        protected void Page_Load(object sender, EventArgs e)
        {
            string valor = "";
            valor = Convert.ToString(Request.QueryString["status"]);
            if (valor == "approved")
            {
                mensajeInicio.Text = "¡Gracias por tu compra!";
                GuardarPedido();
            }
            if(valor == null)
            {
                mensajeInicio.Text = "Verifica tu pedido";
                correo.Value = Session["CORREO_USUARIO"].ToString();
                drawInterfaceCart();
                cargarDatosAPI();
            }

        }
        private void drawInterfaceCart()
        {
            int productos = 0;
            string info = " ";
            double total = 0;
            double subtotal = 0;
            string cadenaID = Session["IDs_Productos"].ToString();
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
            sb.Append("<th scope='col' style='width:100px;padding-left: 20px;'>Descripción</th><th scope='col' style='width:200px;text-align:center;'>Precio</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            sb.Append("<tbody>");
            for (int i = 0; i < productos; i++)
            {
                double num = 0;
                info = getInforProduct(idproductos[i]);
                info = descuentosProd(info,idproductos[i]);
                string[] informacion = info.Split('/');
                num = Convert.ToInt32(cantidadesProd[i]) * Convert.ToDouble(informacion[2]);
                sb.Append("<tr id='" + i + 1 + "Order' class='oneProduct'>");
                sb.Append("<td style='padding-left: 20px;'>" + informacion[0] + " - " + informacion[1] +" (x"+ cantidadesProd[i] + ")</td><td style='text-align:center;'> $" + Convert.ToString(num) + "</td>");
                sb.Append("</tr>");
                total += Convert.ToDouble(cantidadesProd[i]) * Convert.ToDouble(informacion[2]);
                info = " ";
            }
            subtotal = total;
            if (total > 300)
            {
                total = total - (total * .2);
            }
            Session["Monto"] = Convert.ToString(total);
            string aux = "";
            aux = Session["Monto"].ToString();
            decimal valor = Decimal.Parse(aux, System.Globalization.CultureInfo.InvariantCulture);
            aux = valor.ToString("0.00");
            Session["Monto"] = aux;
            //transactionAmount.Value = Session["Monto"].ToString();
            sb.Append("<tr id='subOrder'>");
            sb.Append("<td style='text-align:right;'><b>Subtotal: </b></td><td style='text-align:center;'> $" + subtotal + "</td>");
            if (subtotal > 300)
            {
                sb.Append("<td style='text-align:left;width:20px;'><b>-20%</b></td>");
            }
            sb.Append("</tr>");
            sb.Append("<tr id='totalOrder'>");
            sb.Append("<td style='text-align:right;'><b>Total: </b></td><td style='text-align:center;'> $"+Session["Monto"].ToString()+"</td>");
            sb.Append("</tr>");
            sb.Append("</tbody>");
            sb.Append("</table>");
            sb.Append("</div>");
            sb.Append("</div>");
            tbProductsOrder.Text = sb.ToString();
            transactionAmount.Value = Session["Monto"].ToString();
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
        public string descuentosProd(string cadena, string id)
        {
            string[] modificaciones;
            string resultado=" ";
            int identificador;
            double precio;
            modificaciones = cadena.Split('/');
                identificador = Convert.ToInt32(id);
                if(identificador>=82 && identificador <= 86) //Paquetes
                {
                    precio = Convert.ToDouble(modificaciones[2]) * .5;
                    modificaciones[2] = Convert.ToString(precio);
                    resultado = modificaciones[0] + "/" + "-" + "/"+ modificaciones[2];
                }else if ((identificador>=1 && identificador<=15) && modificaciones[1] == "Grande")
                {
                    precio = Convert.ToDouble(modificaciones[2]) - (Convert.ToDouble(modificaciones[2]) * .25);
                    modificaciones[2] = Convert.ToString(precio);
                    resultado = modificaciones[0] + "/" + modificaciones[1] +"/" + modificaciones[2];
                }else
                {
                    resultado = modificaciones[0] + "/" + modificaciones[1] + "/" + modificaciones[2];
                }
            return resultado;
        }
        public void cargarDatosAPI()
        {
            string link = "https://www.mercadopago.com.mx/integrations/v1/web-tokenize-checkout.js";
            //Page Script
            string pago = "";
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            pago = "<script src='" + link + "' data-public-key='TEST-0f250235-1165-43d7-be1c-4c8078ecd7e8' data-transaction-amount='" + Session["Monto"] + "'></script>";
            pagoscript.Text = pago;
        }
        public void GuardarPedido()
        {
            int lastid = 0, iduser = 0;
            lastid = RecuperarIDPedido() + 1; //Recuperamos ultímo id de pedido registrado
            iduser = getIDUser(Session["CORREO_USUARIO"].ToString()); //Recuperamos el id del usuario
            string fecha_creacion = "";
            fecha_creacion = DateTime.Today.ToString(); //Recuperamos fecha actual del sistema
            string[] datos_fecha = fecha_creacion.Split('/', ' '); //Fragmentamos la fecha
            fecha_creacion = datos_fecha[2] + "-" + datos_fecha[1] + "-" + datos_fecha[0]; //Nuevo formato de fecha
            if (GuardarPedido(lastid, iduser, Session["FechaEntrega"].ToString(), fecha_creacion, Convert.ToDecimal(Session["Monto"]) ,0) == true)
            {
                //Lógica para guardar cada producto del pedido
                int quantity = 0, a = 0, veces=0, identificador=0, oldquantity=0;
                veces = Convert.ToInt32(Session["NoProductos"]); // Cantidad productos comprados
                string cadenaID = Session["IDs_Productos"].ToString(); //String de IDs
                string cadenaCantidades = Session["Cantidades_Productos"].ToString(); //String de cantidades
                string[] idproductos = cadenaID.Split('/'); //IDs separados
                string[] cantidadesProd = cadenaCantidades.Split('/'); //Cantidades separadas

                for(int i=0; i<veces; i++)
                {
                    identificador = Convert.ToInt32(idproductos[i]);
                    quantity = Convert.ToInt32(cantidadesProd[i]);
                    GuardarProductoPedido(lastid, identificador, quantity); //Guardamos cada producto relacionado al pedido
                    oldquantity = RecuperarQuantity(identificador);
                    if(oldquantity!=-1)
                    ActualizarStock(identificador, oldquantity, quantity); //Actualizamos el stock del producto descontando la cantidad comprada del mismo
                }
                Session["isDone"] = 1;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "messageSuccess", "<script>Swal.fire({icon: 'success',title: '¡Gracias por tu compra!',text: 'Tu pedido ha sido generado, puedes checarlo en tu Sección de Pedidos por Entregar'}).then(function () {window.location.href = '/Formularios/Menu.aspx?metric=1asd';})</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "messageError", "<script>Swal.fire({icon: 'error',title: '¡Oops!',text: 'Lo sentimos, algo salió mal al procesar tu pedido.'})</script>");
            }

        }
        public int RecuperarIDPedido()
        {
            int contador = 0;
            string query = "SELECT DISTINCT ID_PEDIDO FROM `pedidos`";
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
                    while (reader.Read())
                    {
                        contador = Convert.ToInt32(reader.GetString(0));
                    }
                }
                dbConnection.Close();
                return contador;
            }
            catch (Exception e)
            {
                //Mensjae de error
                Console.WriteLine("Error" + e);
                return -1;
            }
        }
        public int getIDUser(string correo)
        {
            int id = 0;
            string query = "SELECT ID_USUARIO FROM usuarios where CORREO='" + correo + "'";
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
                        id = Convert.ToInt32(reader.GetString(0));
                    }
                }
                dbConnection.Close();
                return id;
            }
            catch (Exception e)
            {
                //Mensjae de error
                Console.WriteLine("Error" + e);
                return 0;
            }
        }
        public bool GuardarPedido(int id_pedido, int id_usuario, string fe, string fc, decimal precio, int estatus)
        {
            string query = "INSERT INTO pedidos (`id_pedido`,`id_usuario`, `fecha_entrega`, `fecha_creacion`, `precio`, `estatus`) VALUES ('" + id_pedido + "','" + id_usuario + "','" + fe + "', '" + fc + "', '" + precio + "', '" + estatus + "')";
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
        public void GuardarProductoPedido(int pedido, int producto, int cantidad)
        {
            string query = "INSERT INTO productos_pedido(`id_pedido`,`id_producto`, `cantidad`) VALUES ('" + pedido + "','" + producto + "', '" + cantidad + "')";
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
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }
        }
        public int RecuperarQuantity(int identificador)
        {
            int valor = 0;
            string query = "SELECT STOCK FROM `productos` WHERE ID_PRODUCTO= "+identificador+"";
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
                    while (reader.Read())
                    {
                        valor = Convert.ToInt32(reader.GetString(0));
                    }
                }
                dbConnection.Close();
                return valor;
            }
            catch (Exception e)
            {
                //Mensjae de error
                Console.WriteLine("Error" + e);
                return -1;
            }
        }
        public void ActualizarStock(int identificador, int oldquantity, int quantity)
        {
            int newquantity = oldquantity - quantity;
            //Sentencia
            string query = "UPDATE `productos` SET `STOCK`=" + newquantity + " WHERE ID_PRODUCTO = " + identificador+ "";
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
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }
        }

    }
}