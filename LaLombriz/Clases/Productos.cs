using LaLombriz.Modelos;
using System.Collections.Generic;

namespace LaLombriz.Clases
{
    public class Productos
    {
        private int idProducto;
        private string nombre_producto;
        private string descripcion;
        private string tamanio;
        private double precio;
        private int cantidad;
        private int stock;
        private ProductosModel productoBD;

        //Constructor vacío
        public Productos() { }
        //Constructor sobrecargado
        public Productos(string nombredelproducto, string descripcion_prod, string tamanio_prod, double precio_prod,int cantidad)
        {
            this.nombre_producto = nombredelproducto;
            this.descripcion = descripcion_prod;
            this.tamanio = tamanio_prod;
            this.precio = precio_prod;
            this.cantidad = cantidad;
        }
        public Productos(int idProducto,string nombredelproducto, string descripcion_prod, string tamanio_prod, double precio_prod,int stock, int cantidad)
        {
            this.idProducto = idProducto;
            this.nombre_producto = nombredelproducto;
            this.descripcion = descripcion_prod;
            this.tamanio = tamanio_prod;
            this.precio = precio_prod;
            this.stock = stock;
            this.cantidad = cantidad;
        }
        public Productos(ProductosModel productoBD)
        {
            this.productoBD = productoBD;
        }
        //getters y setters
        public int IdProducto { set { idProducto = value; } get { return idProducto; } }
        public string Nombre_producto { set { nombre_producto = value; } get { return nombre_producto; } }
        public string Descripcion { set { descripcion = value; } get { return descripcion; } }
        public string Tamanio { set { tamanio = value; } get { return tamanio; } }
        public double Precio { set { precio = value;  } get { return precio; } } 
        public int Cantidad { set { cantidad = value; } get { return cantidad; } }
        public int Stock { set { stock = value; } get { return stock; } }
        public virtual Productos getProduct(string strConnection,int idProducto)
        {
            Productos producto = this.productoBD.getProductModel(strConnection, idProducto);
            return producto;
        }
        public virtual List<Productos> getAll(string strConnection)
        {
            List<Productos> productos = this.productoBD.getAllModel(strConnection);
            return productos;
        }
        public virtual bool updateProduct(string strConnection, int idProducto, string descripcion, double precio, int stock)
        {
            return this.productoBD.updateProductModel(strConnection, idProducto, descripcion, precio, stock);
        }
    }
}