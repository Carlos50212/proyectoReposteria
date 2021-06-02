using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
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
        public void createPDF(string pathImage)
        {
            //Creamos docuemento 
            Document document = new Document();
            //Establecemos margenes 
            document.SetMargins(60,60,40,40);//Left,Right,Top,Bottom
            try
            {
                //Indicamos donde se guardará el pdf y con que nombre, FileMode.Create = Crea archivo, si existe, sobreescribe
                //PdfWriter pdf = PdfWriter.GetInstSGance(document, new FileStream("C:\\Users\\Gio\\Documents\\pdfPruebas\\Detalles pedido " + pedido.Pedido.Id_pedido + ".pdf",FileMode.Create));
                //Para este caso, indicamos que la dirección destino será el propio navegador (System.Web.HttpContext.Current.Response.OutputStream)
                PdfWriter pdf = PdfWriter.GetInstance(document, System.Web.HttpContext.Current.Response.OutputStream);
                //Estilos de letra 
                //Título
                Font title = FontFactory.GetFont("Arial", 16, Font.BOLD);
                Font subTitle = FontFactory.GetFont("Arial", 14);
                Font content = FontFactory.GetFont("Arial", 12);
                Font tableColumns = FontFactory.GetFont("Arial", 12, Font.BOLD);
                //Ancho de columnas de tabla información del pedido 
                float[] cellDataOrder = new float[2];
                cellDataOrder[0] = 100;
                cellDataOrder[1] = 22;
                //Ancho de columas, tabla información del producto
                float[] cellDataProduct = new float[6];
                cellDataProduct[0] = 120;
                cellDataProduct[1] = 120;
                cellDataProduct[2] = 60;
                cellDataProduct[3] = 60;
                cellDataProduct[4] = 60;
                cellDataProduct[5] = 50;
                //Ancho de columnas, tabla total
                float[] cellTotalOrder = new float[2];
                cellTotalOrder[0] = 10;
                cellTotalOrder[1] = 8;
                //Colocamos título y nombre autor
                document.AddTitle("Detalles de pedido");
                document.AddCreator("La Lombriz S.A de C.V");
                //Abrimos el documento 
                document.Open();

                //document.Add(new Paragraph("Aquí va el título"));

                //Header
                //Insertamos logo
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(pathImage);
                logo.BorderWidth = 0;
                //Tamaño 
                logo.ScaleToFit(80f, 80f);
                //Posición del logo
                logo.SetAbsolutePosition(60, 720); //X,Y
                document.Add(logo);
                //Header 
                var titleParagraph = new Paragraph("La Lombriz S.A de C.V",title);
                titleParagraph.Alignment = 1; //0=Izquierda 1=Centro 2=Derecha
                document.Add(titleParagraph);
                var subTitleParagraph = new Paragraph("Una empresa 100% mexicana",subTitle);
                subTitleParagraph.Alignment = 1;
                document.Add(subTitleParagraph);
                var numReport = new Paragraph("Detalles del pedido",content);
                numReport.Alignment = 1;
                document.Add(numReport);
                document.Add(Chunk.NEWLINE);
                //Tabla datos de pedido
                PdfPTable tableDataOrder = new PdfPTable(2);
                //Agregamos celdas 
                PdfPCell cellIdOrder = new PdfPCell(new Paragraph("No. Pedido:", tableColumns));
                cellIdOrder.Border = Rectangle.NO_BORDER;
                cellIdOrder.HorizontalAlignment = 2;
                tableDataOrder.AddCell(cellIdOrder);
                PdfPCell cellIdOrderValue = new PdfPCell(new Paragraph(Convert.ToString(pedido.Pedido.Id_pedido),content));
                cellIdOrderValue.Border = Rectangle.NO_BORDER;
                cellIdOrderValue.HorizontalAlignment = 0;
                tableDataOrder.AddCell(cellIdOrderValue);

                PdfPCell cellDataCreate = new PdfPCell(new Paragraph("Fecha creación:", tableColumns));
                cellDataCreate.Border = Rectangle.NO_BORDER;
                cellDataCreate.HorizontalAlignment = 2;
                tableDataOrder.AddCell(cellDataCreate);
                PdfPCell cellDataCreateValue = new PdfPCell(new Paragraph(Convert.ToString(pedido.Pedido.Fecha_creacion.ToString("dd/MM/yyyy")), content));
                cellDataCreateValue.Border = Rectangle.NO_BORDER;
                cellDataCreateValue.HorizontalAlignment = 0;
                tableDataOrder.AddCell(cellDataCreateValue);

                PdfPCell cellDataDelivery = new PdfPCell(new Paragraph("Fecha entrega:", tableColumns));
                cellDataDelivery.Border = Rectangle.NO_BORDER;
                cellDataDelivery.HorizontalAlignment = 2;
                tableDataOrder.AddCell(cellDataDelivery);
                PdfPCell cellDataDeliveryValue = new PdfPCell(new Paragraph(Convert.ToString(pedido.Pedido.Fecha_entrega.ToString("dd/MM/yyyy")), content));
                cellDataDeliveryValue.Border = Rectangle.NO_BORDER;
                cellDataDeliveryValue.HorizontalAlignment = 0;
                tableDataOrder.AddCell(cellDataDeliveryValue);

                tableDataOrder.SetWidths(cellDataOrder);
                tableDataOrder.HorizontalAlignment = 2;
                document.Add(tableDataOrder);

                var orderOwner = new Paragraph("Pedido de: " + pedido.Usuario.Nombre);
                orderOwner.Alignment = 0;
                document.Add(orderOwner);
                document.Add(Chunk.NEWLINE);

                //Tabla productos del pedido
                PdfPTable tableProducts = new PdfPTable(6);
                PdfPCell headerNameProduct = new PdfPCell(new Paragraph("Nombre producto",tableColumns));
                headerNameProduct.HorizontalAlignment = 1;
                headerNameProduct.BackgroundColor = new iTextSharp.text.BaseColor(250, 113, 232);
                tableProducts.AddCell(headerNameProduct);
                PdfPCell headerDescriptionProduct = new PdfPCell(new Paragraph("Descripción", tableColumns));
                headerDescriptionProduct.HorizontalAlignment = 1;
                headerDescriptionProduct.BackgroundColor = new iTextSharp.text.BaseColor(250, 113, 232);
                tableProducts.AddCell(headerDescriptionProduct);
                PdfPCell headerSizeProduct = new PdfPCell(new Paragraph("Tamaño", tableColumns));
                headerSizeProduct.HorizontalAlignment = 1;
                headerSizeProduct.BackgroundColor = new iTextSharp.text.BaseColor(250, 113, 232);
                tableProducts.AddCell(headerSizeProduct);
                PdfPCell headerQuantityProduct = new PdfPCell(new Paragraph("Cantidad", tableColumns));
                headerQuantityProduct.HorizontalAlignment = 1;
                headerQuantityProduct.BackgroundColor = new iTextSharp.text.BaseColor(250, 113, 232);
                tableProducts.AddCell(headerQuantityProduct);
                PdfPCell headerCostProduct = new PdfPCell(new Paragraph("Precio unitario", tableColumns));
                headerCostProduct.HorizontalAlignment = 1;
                headerCostProduct.BackgroundColor = new iTextSharp.text.BaseColor(250, 113, 232);
                tableProducts.AddCell(headerCostProduct);
                PdfPCell headerTotalProduct = new PdfPCell(new Paragraph("Total", tableColumns));
                headerTotalProduct.HorizontalAlignment = 1;
                headerTotalProduct.BackgroundColor = new iTextSharp.text.BaseColor(250, 113, 232);
                tableProducts.AddCell(headerTotalProduct);

                foreach (Productos producto in pedido.Productos)
                {
                    PdfPCell cellNameProdcut = new PdfPCell(new Paragraph(producto.Nombre_producto, content));
                    cellNameProdcut.HorizontalAlignment = 1;
                    tableProducts.AddCell(cellNameProdcut);
                    PdfPCell cellDescriptionProdcut = new PdfPCell(new Paragraph(producto.Descripcion, content));
                    cellDescriptionProdcut.HorizontalAlignment = 1;
                    tableProducts.AddCell(cellDescriptionProdcut);
                    PdfPCell cellSizeProdcut = new PdfPCell(new Paragraph(producto.Tamanio, content));
                    cellSizeProdcut.HorizontalAlignment = 1;
                    tableProducts.AddCell(cellSizeProdcut);
                    PdfPCell cellQuantityProdcut = new PdfPCell(new Paragraph(Convert.ToString(producto.Cantidad), content));
                    cellQuantityProdcut.HorizontalAlignment = 1;
                    tableProducts.AddCell(cellQuantityProdcut);
                    PdfPCell cellCostProdcut = new PdfPCell(new Paragraph("$"+producto.Precio, content));
                    cellCostProdcut.HorizontalAlignment = 1;
                    tableProducts.AddCell(cellCostProdcut);
                    PdfPCell cellTotalProdcut = new PdfPCell(new Paragraph("$"+(producto.Cantidad * producto.Precio), content));
                    cellTotalProdcut.HorizontalAlignment = 1;
                    tableProducts.AddCell(cellTotalProdcut);
                }
                tableProducts.WidthPercentage = 100f;
                tableProducts.HorizontalAlignment = 0;
                tableProducts.SetWidths(cellDataProduct);
                document.Add(tableProducts);

                //Tabla total 
                PdfPTable tableTotal = new PdfPTable(2);
                PdfPCell cellTotalCostProdcut = new PdfPCell(new Paragraph("Total:", tableColumns));
                cellTotalCostProdcut.HorizontalAlignment = 2;
                cellTotalCostProdcut.Border = Rectangle.NO_BORDER;
                tableTotal.AddCell(cellTotalCostProdcut);
                PdfPCell cellTotal = new PdfPCell(new Paragraph("$"+pedido.Pedido.Precio, tableColumns));
                cellTotal.HorizontalAlignment = 1;
                tableTotal.AddCell(cellTotal);
                tableTotal.SetWidths(cellTotalOrder);
                tableTotal.HorizontalAlignment = 2;
                tableTotal.WidthPercentage = 24f;
                document.Add(tableTotal);

                //Tabla nota 
                PdfPTable tableNote = new PdfPTable(1);
                //Espacio entre nota y la tabla
                tableNote.SpacingBefore = 200;
                PdfPCell cellTitle = new PdfPCell(new Paragraph("Nota", tableColumns));
                cellTitle.Border = Rectangle.NO_BORDER;
                cellTitle.BorderWidthTop = 1f;
                cellTitle.BorderWidthLeft = 1f;
                cellTitle.BorderWidthRight = 1f;
                tableNote.AddCell(cellTitle);
                PdfPCell cellContent = new PdfPCell(new Paragraph("Los pedidos pueden ser cancelados o modificados con un máximo de 10 días antes de la fecha de entrega, favor de tener su número de pedido en mano. Para cualquier duda o aclaración de favor de ponerse en contacto mediante nuestras redes sociales o correo.",content));
                cellContent.Border = Rectangle.NO_BORDER;
                cellContent.BorderWidthLeft = 1f;
                cellContent.BorderWidthRight = 1f;
                cellContent.BorderWidthBottom = 1f;
                tableNote.WidthPercentage = 100f;
                tableNote.AddCell(cellContent);
                document.Add(tableNote);
                //Cerramos el archivo
                document.Close();
                //Se indica que el tipo de archivo
                HttpContext.Current.Response.ContentType = "application/pdf";
                //Se agrega el nombre del archivo 
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=Detalles Pedido.pdf");
                //Se escribe el documento en el archivo creado 
                System.Web.HttpContext.Current.Response.Write(document);
                //Se limpia memoria 
                HttpContext.Current.Response.Flush();
                //Se cierra
                HttpContext.Current.Response.End();

            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR: " + ex);
            }
        }
        
    }
}