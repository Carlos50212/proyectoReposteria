using LaLombriz.Clases;
using LaLombriz.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaLombriz.Formularios
{
    public partial class Respondidas : System.Web.UI.Page
    {
        private static string strConnection = "Server=sql512.main-hosting.eu; Database=u119388885_reposteria;Uid=u119388885_gio;Pwd=270299Gp$2018";
        protected void Page_Load(object sender, EventArgs e)
        {
            drawInterface();
        }
        public void drawInterface()
        {
            int idUser = Convert.ToInt32(Session["ID_USUARIO"]);
            Cotizacion c = new Cotizacion(new CotizacionBD());
            List<Cotizacion> cotizaciones = c.getAllUserCotizacion(strConnection, idUser,1);
            StringBuilder sb = new StringBuilder();

            if (cotizaciones.Count < 0)
            {
                foreach (Cotizacion cotizacion in cotizaciones)
                {
                    sb.Append("<div id='" + cotizacion.IdCotizacion + "Cotizacion'  class='oneCotizacion'>");
                    sb.Append("<div class='containerOptionsCotizaciones'>");
                    sb.Append("<div class='title'>");
                    sb.Append("<b>Número de cotización: </b><span style='color:#757575;'>" + cotizacion.IdCotizacion + "</span>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("<div class='tableInformationCotizacion'>");
                    sb.Append("<table class='table table-borderless tableCotizacion'>");
                    sb.Append("<thead>");
                    sb.Append("<tr>");
                    sb.Append("<th scope='col'>Fecha de creación</th><th scope='col'>Fecha de respuesta</th><th scope='col'>Mensaje enviado</th><th scope='col'>Respuesta</th>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");
                    sb.Append("<tbody>");
                    sb.Append("<tr>");
                    sb.Append("<td>" + cotizacion.FechaContacto.ToString("dd/MM/yyyy") + "</td><td>" + cotizacion.FechaRespuesta.ToString("dd/MM/yyyy") + "</td><td><textarea class='form-control txStyleMessage'  rows='6' id='message' readonly='readonly'>" + cotizacion.Mensaje + "</textarea></td><td><textarea class='form-control txStyleMessage'  rows='6' id='answer' readonly='readonly'>" + cotizacion.Respuesta + "</textarea></td>");
                    sb.Append("</tr>");
                    sb.Append("</tbody>");
                    sb.Append("</table>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    ltCotizacionesRespondidas.Text = sb.ToString();
                }
            }
            else
            {
                contestadas.Style["display"] = "none";
                notCotizaciones.Style["display"] = "flex";
                notCotizaciones.Style["justify-content"] = "center";
            }
        }
    }
}