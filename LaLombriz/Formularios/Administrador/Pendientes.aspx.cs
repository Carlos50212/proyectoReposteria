using LaLombriz.Clases;
using LaLombriz.Modelos;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace LaLombriz.Formularios.Administrador
{
    public partial class CPendientes : System.Web.UI.Page
    {
        private static string strConnection = "Server=sql512.main-hosting.eu; Database=u119388885_reposteria;Uid=u119388885_gio;Pwd=270299Gp$2018";
        private static int idUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            idUser = Convert.ToInt32(Session["ID_USUARIO"]);
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string getCotizacionesPendientes()
        {
            Cotizacion cotizacion = new Cotizacion(new CotizacionBD());
            Usuario user = new Usuario(new UsuariosBD());
            List<Cotizacion> cotizaciones = cotizacion.getCotizaciones(strConnection,0);

            List<string> cotizacionFinal = new List<string>();
            foreach (Cotizacion cotizacionTmp in cotizaciones)
            {
                Usuario usuario = user.getUser(cotizacionTmp.IdCliente, strConnection);
                cotizacionFinal.Add("{\"id_cotizacion\":" + cotizacionTmp.IdCotizacion + ", \"id_cliente\":" + cotizacionTmp.IdCliente + ", \"correo\":\"" + usuario.Correo + "\", \"fecha_contacto\":\"" + cotizacionTmp.FechaContacto.ToString("dd/MM/yyyy") + "\", \"mensaje\":\"" + cotizacionTmp.Mensaje + "\"}");
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(cotizacionFinal);

            return json;
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static  Boolean sendAnswer(string idCotizacion, string respuesta)
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd");
            Usuario usuario = new Usuario(new UsuariosBD());
            Cotizacion cotizacion = new Cotizacion(new CotizacionBD());
            bool isSaved = cotizacion.sendAnswer(strConnection,Convert.ToInt32(idCotizacion),respuesta,idUser,fecha);
            Cotizacion cotizacionInfor = cotizacion.getCotizacion(strConnection, Convert.ToInt32(idCotizacion));
            Usuario client = usuario.getUser(cotizacionInfor.IdCliente,strConnection);
            return isSaved;
        }
    }
}