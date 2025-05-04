using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Colors;
using Document = iText.Layout.Document;

namespace BlazorApp2.Share
{
    public class PdfGenerator
    {
        public byte[] GeneratePdfFromTable(DetalleSemaforoContainer contenedor)
        {
            if (contenedor?.TotalItems == 0)
            {
                Console.WriteLine("Contenedor vacío");
                return null;
            }

            var stream = new MemoryStream();
            var writer = new PdfWriter(stream);
            writer.SetCloseStream(false);

            try
            {
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                PdfFont fontNormal;
                PdfFont fontBold;
                try
                {
                    fontNormal = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                    fontBold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                }
                catch
                {
                    fontNormal = PdfFontFactory.CreateFont(StandardFonts.COURIER);
                    fontBold = PdfFontFactory.CreateFont(StandardFonts.COURIER_BOLD);
                }

                document.Add(new Paragraph("Reporte de Semáforos")
                    .SetFont(fontBold)
                    .SetFontSize(16)
                    .SetTextAlignment(TextAlignment.CENTER));

                var table = new Table(UnitValue.CreatePercentArray(6)).UseAllAvailableWidth();

                string[] headers = { "Nodo ID", "Dirección", "Cambios", "Vehículos", "Promedio", "Tiempo (s)" };
                foreach (var header in headers)
                {
                    var cell = new Cell()
                        .Add(new Paragraph(header ?? string.Empty)
                        .SetFont(fontBold)
                        .SetFontColor(ColorConstants.WHITE));
                    cell.SetBackgroundColor(new DeviceRgb(33, 150, 243));
                    table.AddHeaderCell(cell);
                }

                for (int i = 0; i < (contenedor?.TotalItems ?? 0); i++)
                {
                    var item = contenedor?.GetItem(i);
                    if (item == null) continue;

                    table.AddCell(SafeCell(item.NodoId.ToString(), fontNormal));
                    table.AddCell(SafeCell(item.DireccionSemaforo, fontNormal));
                    table.AddCell(SafeCell(item.totalCambios.ToString(), fontNormal));
                    table.AddCell(SafeCell(item.SumaCantidadEspera.ToString(), fontNormal));
                    table.AddCell(SafeCell(item.PromedioVehiculosPorCambio.ToString("F2"), fontNormal));
                    table.AddCell(SafeCell(item.TiempoPromedioPorCarro.ToString("F2"), fontNormal));
                }

                document.Add(table);
                document.Close();

                if (stream.Length == 0)
                    throw new InvalidOperationException("El PDF generado está vacío");

                return stream.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error crítico: {ex}");
                return null;
            }
            finally
            {
                writer?.Close();
            }
        }

        private Cell SafeCell(string content, PdfFont font)
        {
            return new Cell().Add(new Paragraph(content ?? "N/D")).SetFont(font);
        }
    }
}
