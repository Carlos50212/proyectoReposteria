using LaLombriz.Clases;
using LaLombriz.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LaLombriz.Formularios.Administrador
{
    public partial class Contestadas : System.Web.UI.Page
    {
        private static string strConnection = "Server=localhost;Database=reposteria;Uid=gio;Pwd=270299GPS";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string getCotizacionesContestadas()
        {
            Cotizacion cotizacion = new Cotizacion(new CotizacionBD());
            Usuario user = new Usuario(new UsuariosBD());
            List<Cotizacion> cotizaciones = cotizacion.getCotizaciones(strConnection, 1);

            List<string> cotizacionFinal = new List<string>();
            foreach (Cotizacion cotizacionTmp in cotizaciones)
            {
                Usuario usuario = user.getUser(cotizacionTmp.IdCliente, strConnection);
                cotizacionFinal.Add("{\"id_cotizacion\":" + cotizacionTmp.IdCotizacion + ", \"id_cliente\":" + cotizacionTmp.IdCliente + ", \"id_personal\":" + cotizacionTmp.IdAdministrador + ",\"correo\":\"" + usuario.Correo + "\", \"fecha_contacto\":\"" + cotizacionTmp.FechaContacto.ToString("dd/MM/yyyy") + "\", \"fecha_respuesta\":\"" + cotizacionTmp.FechaRespuesta.ToString("dd/MM/yyyy") + "\", \"mensaje\":\"" + cotizacionTmp.Mensaje + "\", \"respuesta\":\"" + cotizacionTmp.Respuesta + "\"}");
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(cotizacionFinal);

            return json;
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static string getInfoUser(string idCliente, string idAdmin)
        {
            Cotizacion cotizacion = new Cotizacion(new CotizacionBD());
            Usuario user = new Usuario(new UsuariosBD());
            Usuario usuario = user.getUser(Convert.ToInt32(idCliente), strConnection);
            Usuario admin = user.getUser(Convert.ToInt32(idAdmin), strConnection);

            List<string> cadenaFinal = new List<string>();

            cadenaFinal.Add("{\"nombre\":\"" + usuario.Nombre + "\", \"correo\":\"" + usuario.Correo + "\", \"telefono\":\"" + usuario.Telefono + "\"}"); 
            cadenaFinal.Add("{\"nombre\":\"" + admin.Nombre + "\", \"correo\":\"" + admin.Correo + "\", \"telefono\":\"" + admin.Telefono + "\"}");

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(cadenaFinal);

            return json;
        }
    }
}