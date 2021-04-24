using LaLombriz.Clases;
using LaLombriz.Modelos;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaLombrizTest
{
    [TestFixture]
    class VentasTest
    {
        Mock<VentasBD> mockVentas;
        Ventas ventas;
        private static string strConnection = "server=localhost;database=reposteria;uid=gio;pwd=270299GPS";

        [SetUp]
        public void SetUp()
        {
            mockVentas = new Mock<VentasBD>();
            ventas = new Ventas(mockVentas.Object);
        }

        [Test]
        public void ShouldNotReturnASellsDayList()
        {
            mockVentas.Setup(Mo => Mo.getAllSellsDayModel(Convert.ToDateTime("1900-01-01"),strConnection)).Returns(new List<Ventas>());

            List<Ventas> result = this.ventas.getAllSellsDay(Convert.ToDateTime("1900-01-01"), strConnection);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void ShouldReturnASellsDayList()
        {
            mockVentas.Setup(Mo => Mo.getAllSellsDayModel(Convert.ToDateTime("1900-01-01"), strConnection)).Returns(buildListElement());

            List<Ventas> result = this.ventas.getAllSellsDay(Convert.ToDateTime("1900-01-01"), strConnection);

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void ShouldNotReturnASellsMonthList()
        {
            mockVentas.Setup(Mo => Mo.getAllSellsMonthModel(strConnection,"1900-01-01")).Returns(new List<Ventas>());

            List<Ventas> result = this.ventas.getAllSellsMonth(strConnection,"1900-01-01");

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void ShouldReturnASellsMonthList()
        {
            mockVentas.Setup(Mo => Mo.getAllSellsMonthModel(strConnection,"1900-01-01")).Returns(buildListElement());

            List<Ventas> result = this.ventas.getAllSellsMonth(strConnection,"1900-01-01");

            Assert.That(result.Count, Is.EqualTo(1));
        }

        private List<Ventas> buildListElement()
        {
            List<Ventas> sells = new List<Ventas>();

            sells.Add(new Ventas(1,1,10.0,Convert.ToDateTime("1900-01-01")));

            return sells;
        }

    }
}
