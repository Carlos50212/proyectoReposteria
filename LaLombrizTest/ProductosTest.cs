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
    class ProductosTest
    {

        Mock<ProductosModel> mockProducto;
        Productos productos;
        private static string strConnection = "server=localhost;database=reposteria;uid=gio;pwd=270299GPS";

        [SetUp]
        public void SetUp()
        {
            mockProducto = new Mock<ProductosModel>();
            productos = new Productos(mockProducto.Object);
        }

        [Test]
        public void shouldReturnAnEmptyProductsList()
        {
            mockProducto.Setup(Mo => Mo.getAllModel(strConnection)).Returns(new List<Productos>());

            List<Productos> result = this.productos.getAll(strConnection);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void shouldReturnAProductList()
        {
            mockProducto.Setup(Mo => Mo.getAllModel(strConnection)).Returns(buildListElement());

            List<Productos> result = this.productos.getAll(strConnection);

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void shouldReturnANullProductObject()
        {
            Productos product = null;
            mockProducto.Setup(Mo => Mo.getProductModel(strConnection, 1)).Returns(product);

            Productos result = this.productos.getProduct(strConnection, 1);

            Assert.IsNull(result);
        }

        [Test]
        public void shouldReturnANotNullProductObject()
        {
            Productos product = new Productos(1, "Producto prueba", "Es una prueba", "Prueba", 20.0, 0, 0);

            mockProducto.Setup(Mo => Mo.getProductModel(strConnection, 1)).Returns(product);

            Productos result = this.productos.getProduct(strConnection, 1);

            Assert.IsNotNull(result);
        }

        [Test]
        public void shouldNotUpdateTheProduct()
        {
            mockProducto.Setup(Mo => Mo.updateProductModel(strConnection,1,"Esto es una prueba",20.0,0)).Returns(false);

            bool isUpdated = this.productos.updateProduct(strConnection, 1, "Esto es una prueba", 20.0, 0);

            Assert.IsFalse(isUpdated);

        }

        [Test]
        public void shouldUpdateTheProduct()
        {
            mockProducto.Setup(Mo => Mo.updateProductModel(strConnection,1,"Esto es una prueba",20.0,0)).Returns(true);

            bool isUpdated = this.productos.updateProduct(strConnection, 1, "Esto es una prueba", 20.0, 0);

            Assert.IsTrue(isUpdated);
        }

        private List<Productos> buildListElement()
        {
            List<Productos> productos = new List<Productos>();
            productos.Add(new Productos(1,"Producto prueba","Es una prueba","Prueba",20.0,0,0));
            return productos;
        }

    }
}
