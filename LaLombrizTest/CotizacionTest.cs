using LaLombriz.Modelos;
using NUnit.Framework;
using Moq;
using System;
using LaLombriz.Clases;
using System.Collections.Generic;

namespace LaLombrizTest
{
    [TestFixture]
    public class CotizacionTest
    {
        Cotizacion cotizacion;
        Mock<CotizacionBD> mockCotizacion;
        List<Cotizacion> quotations;
        private static string strConnection = "server=localhost;database=reposteria;uid=gio;pwd=270299GPS";


        [SetUp]
        public void SetUp()
        {
            mockCotizacion = new Mock<CotizacionBD>();
            cotizacion = new Cotizacion(mockCotizacion.Object);
            quotations = buildListQuotations();
        }


        [Test]
        public void shouldReturnAnEmptyListAboutQuotations()
        {
            mockCotizacion.Setup(el => el.getCotizacionesModel(strConnection, 1)).Returns(new List<Cotizacion>());

            List<Cotizacion> result = cotizacion.getCotizaciones(strConnection, 1);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void shouldReturnAListAboutQuotations()
        {

            mockCotizacion.Setup(fl => fl.getCotizacionesModel(strConnection, 1)).Returns(this.quotations);

            List<Cotizacion> result = cotizacion.getCotizaciones(strConnection,1);

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void shouldReturnAnEmptyQuotationObject()
        {
            Cotizacion quotation = null;

            mockCotizacion.Setup(eo => eo.getCotizacionModel(strConnection, 0)).Returns(quotation);

            Cotizacion result = cotizacion.getCotizacion(strConnection, 0);

            Assert.IsNull(result);

        }

        [Test]
        public void shouldReturnANotEmptyQuotationObject()
        {

            mockCotizacion.Setup(fe => fe.getCotizacionModel(strConnection,0)).Returns(new Cotizacion(1, 1, "1", 1, Convert.ToDateTime("1900-01-01"), Convert.ToDateTime("1900-01-01"), "Pruebas unitarias", "Funcionando"));

            Cotizacion result = cotizacion.getCotizacion(strConnection, 0);

            Assert.IsNotNull(result);

        }

        [Test]
        public void shouldReturnFalseWhenSendAnswer()
        {

            mockCotizacion.Setup(fa => fa.sendAnswerModel(strConnection, 1, "Es una prueba", 1, "1900-01-01")).Returns(false);

            bool isSend = cotizacion.sendAnswer(strConnection, 1, "Es una prueba", 1, "1900-01-01");

            Assert.IsFalse(isSend);
        }

        [Test]
        public void shouldReturnTrueWhenSendAnswer()
        {

            mockCotizacion.Setup(fa => fa.sendAnswerModel(strConnection,1, "Es una prueba", 1, "1900-01-01")).Returns(true);

            bool isSend = cotizacion.sendAnswer(strConnection, 1, "Es una prueba", 1, "1900-01-01");

            Assert.IsTrue(isSend);

        }

        private List<Cotizacion> buildListQuotations()
        {
            List<Cotizacion> quotations = new List<Cotizacion>();
            quotations.Add(new Cotizacion(1,1,"1",1,Convert.ToDateTime("1900-01-01"),Convert.ToDateTime("1900-01-01"),"Pruebas unitarias","Funcionando"));
            //quotations.Add(new Cotizacion(2, 1, "1", 1, Convert.ToDateTime("1900-01-01"), Convert.ToDateTime("1900-01-01"), "Pruebas unitarias", "Funcionando"));
            return quotations;
        }

    }
}
