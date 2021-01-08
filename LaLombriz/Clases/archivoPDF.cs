using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LaLombriz.Clases
{
    public class archivoPDF
    {
        private ProductosPedidos pedido;
        public archivoPDF(ProductosPedidos pedido)
        {
            this.pedido = pedido;
        }
        public void createPDF()
        {
            //Creamos docuemento 
            Document document = new Document();
            //Establecemos margenes 
            document.SetMargins(10,10,10,10);//Left,Right,Top,Bottom
            try
            {
                //Indicamos donde se guardará el pdf y con que nombre, FileMode.Create = Crea archivo, si existe, sobreescribe
                PdfWriter pdf = PdfWriter.GetInstance(document, new FileStream("C:\\Users\\Gio\\Documents\\pdfPruebas\\Detalles pedido "+pedido.Pedido.Id_pedido+".pdf",FileMode.Create));
                //Colocamos título y nombre autor
                document.AddTitle("Detalles de pedido");
                document.AddCreator("La Lombriz S.A de C.V");
                //Abrimos el documento 
                document.Open();
                document.Add(new Paragraph("Aquí va el título"));
                //Cerramos el archivo
                document.Close();


            }catch(Exception ex)
            {
                Console.WriteLine("ERROR: " + ex);
            }
        }
    }
}