using LaLombriz.Modelos;
using System;
using System.Collections.Generic;

namespace LaLombriz.Clases
{
    public class Cotizacion
    {
        private int idCotizacion;
        private int idCliente;
        private string idAdministrador;
        private int estatus;
        private DateTime fechaContacto;
        private DateTime fechaRespuesta;
        private string mensaje;
        private string respuesta;
        private CotizacionBD cotizacionBD;

        public Cotizacion() { }
        public Cotizacion(CotizacionBD cotizacionBD)
        {
            this.cotizacionBD = cotizacionBD;
        }
        public Cotizacion(int idCotizacion,int idCliente,string idAdministrador,int estatus,DateTime fechaContacto,DateTime fechaRespuesta,string mensaje,string respuesta)
        {
            this.idCotizacion = idCotizacion;
            this.idCliente = idCliente;
            this.idAdministrador = idAdministrador;
            this.estatus = estatus;
            this.fechaContacto = fechaContacto;
            this.fechaRespuesta = fechaRespuesta;
            this.mensaje = mensaje;
            this.respuesta = respuesta;
        }

        public int IdCotizacion { set { idCotizacion = value; } get { return idCotizacion; } }
        public int IdCliente { set { idCliente = value; } get { return idCliente; } }
        public string IdAdministrador { set { idAdministrador = value; } get { return idAdministrador; } }
        public int Estatus { set { estatus = value; } get { return estatus; } }
        public DateTime FechaContacto { set { fechaContacto = value; } get { return fechaContacto; } }
        public DateTime FechaRespuesta { set { fechaRespuesta = value; } get { return fechaRespuesta; } }
        public string Mensaje { set { mensaje = value; } get { return mensaje; } }
        public string Respuesta { set { respuesta = value; } get { return respuesta; } }
        public virtual List<Cotizacion> getCotizaciones(string strConnection, int estatus)
        {
            List<Cotizacion> cotizacion = new List<Cotizacion>();
            cotizacion = this.cotizacionBD.getCotizacionesModel(strConnection,estatus);
            return cotizacion;
        }

        public virtual Cotizacion getCotizacion(string strConnection, int idCotizacion)
        {
            Cotizacion cotizacion = this.cotizacionBD.getCotizacionModel(strConnection, idCotizacion);
            return cotizacion;
        }
        public virtual bool sendAnswer(string strConnection,int idCotizacion,string respuesta, int idUser, string fecha)
        {
            return this.cotizacionBD.sendAnswerModel(strConnection, idCotizacion, respuesta, idUser, fecha);
        }
        public virtual bool sendText(int idUser,string date,string description, string strConnection)
        {
            bool isSaved = this.cotizacionBD.sendTextModel(idUser, date, description, strConnection);


            return isSaved;
        }

        public virtual List<Cotizacion> getAllUserCotizacion(string strConnection,int idUser,int estatus)
        {
            List<Cotizacion> cotizacion = this.cotizacionBD.getAllUserCotizacionModel(strConnection, idUser,estatus);
            return cotizacion;
        }
    }
}