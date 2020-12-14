using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaLombriz.Clases
{
    public class Usuario
    {
        private int id;
        private string nombre;
        private string correo;
        private int telefono;

        public Usuario() { }  //constructor vacio
        public Usuario (int id, string nombre, string correo, int telefono)  //constructor sobrecargado
        {
            this.id = id;
            this.nombre = nombre;
            this.correo = correo;
            this.telefono = telefono;
        }
        //getters y setters, atributos 
        public int Id { set { id = value; } get { return id; } }
        public string Nombre { set { nombre = value; } get { return nombre; } }
        public string Correo { set { correo = value; } get { return correo; } }
        public string Telefono { set { telefono = value; } get { return telefono; } }


    }
}