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
    class PedidosTest
    {

        PedidosCliente pedidos;
        Mock<PedidosClienteBD> mockPedidos;
        private static string strConnection = "server=localhost;database=reposteria;uid=gio;pwd=270299GPS";

        [SetUp]
        public void SetUp()
        {
            mockPedidos = new Mock<PedidosClienteBD>();
            pedidos = new PedidosCliente(mockPedidos.Object);
        }

        [Test]
        public void shouldReturnEmptyOrdersList()
        {
            mockPedidos.Setup(Mo => Mo.getOrdersModel(strConnection, 1)).Returns(new List<PedidosCliente>());

            List<PedidosCliente> result = this.pedidos.getOrders(strConnection, 1);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void shouldReturnAnOrdersList()
        {
            mockPedidos.Setup(Mo => Mo.getOrdersModel(strConnection, 1)).Returns(BuildListElement());

            List<PedidosCliente> result = this.pedidos.getOrders(strConnection, 1);

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void shouldNotDeliverOrder()
        {
            mockPedidos.Setup(Mo => Mo.deliverOrderModel(strConnection,6)).Returns(false);

            bool isDelivered = this.pedidos.deliverOrder(strConnection, 6);

            Assert.IsFalse(isDelivered);
        }

        [Test]
        public void shouldDeliverOrder()
        {
            mockPedidos.Setup(Mo => Mo.deliverOrderModel(strConnection,6)).Returns(true);

            bool isDelivered = this.pedidos.deliverOrder(strConnection, 6);

            Assert.IsTrue(isDelivered);
        }

        [Test]
        public void shouldNotCancelOrder()
        {
            mockPedidos.Setup(Mo => Mo.cancelOrderModel(strConnection, 6)).Returns(false);

            bool isCanceled = this.pedidos.cancelOrder(strConnection, 6);

            Assert.IsFalse(isCanceled);
        }

        [Test]
        public void shouldCancelOrder()
        {
            mockPedidos.Setup(Mo => Mo.deliverOrderModel(strConnection, 6)).Returns(true);

            bool isCanceled = this.pedidos.deliverOrder(strConnection, 6);

            Assert.IsTrue(isCanceled);
        }

        private List<PedidosCliente> BuildListElement()
        {
            List<PedidosCliente> orders = new List<PedidosCliente>();
            orders.Add(new PedidosCliente(1,1,Convert.ToDateTime("1900-01-01"), Convert.ToDateTime("1900-01-01"),Convert.ToDouble(10)));
            return orders;
        }
    }
}
