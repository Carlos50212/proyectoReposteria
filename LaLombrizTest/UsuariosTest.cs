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
    class UsuariosTest
    {
        Mock<UsuariosBD> mockUser;
        Usuario user;
        private static string strConnection = "server=localhost;database=reposteria;uid=gio;pwd=270299GPS";

        [SetUp]
        public void SetUp()
        {
            mockUser = new Mock<UsuariosBD>();
            user = new Usuario(mockUser.Object, "Usuario prueba", "prueba@email.com", "prueba123", "1111111111");
        }

        [Test]
        public void ShouldNotCreateUser()
        {
            mockUser.Setup(Mo => Mo.createUserModel(strConnection,"Usuario prueba","prueba@email.com","prueba123","1111111111")).Returns(false);

            bool isCreated = this.user.createUser(strConnection);

            Assert.IsFalse(isCreated);
        }

        [Test]
        public void ShouldCreateUser()
        {
            mockUser.Setup(Mo => Mo.createUserModel(strConnection,user.Nombre,user.Correo,user.Pass,user.Telefono)).Returns(true);

            bool isCreated = this.user.createUser(strConnection);

            Assert.IsTrue(isCreated);
        }

        [Test]
        public void ShouldNotLogIn()
        {
            mockUser.Setup(Mo => Mo.IniciarSesionModel("prueba@email.com", "prueba123", strConnection)).Returns(false);

            bool isLogin = this.user.IniciarSesion("prueba@email.com", "prueba123", strConnection);

            Assert.IsFalse(isLogin);
        }

        [Test]
        public void ShouldLogIn()
        {
            mockUser.Setup(Mo => Mo.IniciarSesionModel("prueba@email.com", "prueba123", strConnection)).Returns(true);

            bool isLogin = this.user.IniciarSesion("prueba@email.com", "prueba123", strConnection);

            Assert.IsTrue(isLogin);
        }

        [Test]
        public void ShouldNotBeNewUser()
        {
            mockUser.Setup(Mo => Mo.CorreoDobleModel(strConnection,this.user.Correo)).Returns(true);

            bool isUsed = this.user.CorreoDoble(strConnection);

            Assert.IsTrue(isUsed);
        }

        [Test]
        public void ShouldBeNewUser()
        {
            mockUser.Setup(Mo => Mo.CorreoDobleModel(strConnection, "prueba@email.com")).Returns(false);

            bool isUsed = this.user.CorreoDoble(strConnection);

            Assert.IsFalse(isUsed);
        }

        [Test]
        public void shouldReturnEmptyUser()
        {
            Usuario user = null;

            mockUser.Setup(Mo => Mo.getUserModel(1,strConnection)).Returns(user);

            Usuario result = this.user.getUser(1, strConnection);

            Assert.IsNull(result);
        }

        [Test]
        public void shouldReturnAUser()
        {

            mockUser.Setup(Mo => Mo.getUserModel(1, strConnection)).Returns(buildUserElement());

            Usuario result = this.user.getUser(1, strConnection);

            Assert.IsNotNull(result);
        }

        [Test]
        public void shouldNotReturnTypeUser()
        {
            mockUser.Setup(Mo => Mo.getTypeUserModel(strConnection,1)).Returns(0);

            int result = this.user.getTypeUser(strConnection, 1);

            Assert.AreEqual(result,0);
        }

        [Test]
        public void shouldReturnTypeUser()
        {
            mockUser.Setup(Mo => Mo.getTypeUserModel(strConnection, 1)).Returns(1);

            int result = this.user.getTypeUser(strConnection, 1);

            Assert.AreEqual(result, 1);
        }

        private Usuario buildUserElement() {
            Usuario user = new Usuario("Usuario prueba","prueba@email.com","1111111111",1);
            return user;
        }

    }
}
