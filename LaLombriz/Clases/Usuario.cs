using LaLombriz.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaLombriz.Clases
{
    public class Usuario
    {
        public int idUsuario;
        private string nombre;
        private string correo;
        private string pass;
        private string telefono;

        private UsuariosBD userBD;

        public Usuario() { }  //constructor vacio
        public Usuario(string nombre, string correo, string pass, string telefono, int idUsuario)  //constructor sobrecargado
        {
            this.nombre = nombre;
            this.correo = correo;
            this.pass = pass;
            this.telefono = telefono;
            this.idUsuario = idUsuario;
        }
        public Usuario(UsuariosBD userBD)
        {
            this.userBD = userBD;
        }
        public Usuario(UsuariosBD userBD, string nombre, string correo, string pass, string telefono)
        {
            this.userBD = userBD;
            this.nombre = nombre;
            this.correo = correo;
            this.pass = pass;
            this.telefono = telefono;
        }
        public Usuario(string correo, string pass) //constructor para inicio sesión
        {
            this.correo = correo;
            this.pass = pass;
        }
        public Usuario(UsuariosBD userBD,string correo) //constructor para validación
        {
            this.userBD = userBD;
            this.correo = correo;
        }
        public Usuario (string nombre, string correo,string pass, string telefono)  //constructor sobrecargado
        {
            
            this.nombre = nombre;
            this.correo = correo;
            this.pass = pass;
            this.telefono = telefono;
        }
        //getters y setters, atributos 

        public int IdUsuario { set { idUsuario = value; } get { return idUsuario; } }
        public string Nombre { set { nombre = value; } get { return nombre; } }
        public string Correo { set { correo = value; } get { return correo; } }
        public string Pass { set { pass = value; } get { return pass; } }
        public string Telefono { set { telefono = value; } get { return telefono; } }
        //Metodos 
        public virtual bool createUser(string strConnection)
        {
            return this.userBD.createUserModel(strConnection,this.Nombre,this.Correo,this.Pass,this.Telefono);
        }
        public virtual  Usuario IniciarSesion(string mail, string contra, string strConnection)
        {
            return this.userBD.IniciarSesionModel(mail, contra, strConnection);
        }
        public virtual bool CorreoDoble(string strConnection)
        {
            return this.userBD.CorreoDobleModel(strConnection,this.correo);
        }
        public virtual Usuario getUser(int idUsuario,string strConnection)
        {
            Usuario user = this.userBD.getUserModel(idUsuario, strConnection);
            return user;
        }
        public virtual int getTypeUser(string strConnection, int idUsuario)
        {
            int typeUser = this.userBD.getTypeUserModel(strConnection, idUsuario);

            return typeUser;
        }


    }
}