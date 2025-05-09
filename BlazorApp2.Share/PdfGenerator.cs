using System;
using System.IO;
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
        public byte[] GeneratePdfFromContainer(BaseContainer contenedor, string tipoAnalisis)
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

                
                string titulo = tipoAnalisis switch
                {
                    "0" => "Reporte Detallado de Semáforos",
                    "1" => "Intersecciones con Mayor Congestión",
                    "2" => "Análisis de Cuellos de Botella",
                    _ => "Reporte de Semáforos"
                };

                document.Add(new Paragraph(titulo)
                    .SetFont(fontBold)
                    .SetFontSize(16)
                    .SetTextAlignment(TextAlignment.CENTER));

               
                switch (tipoAnalisis)
                {
                    case "0":
                        GenerateDetalleSemaforoTable(document, contenedor as DetalleSemaforoContainer, fontNormal, fontBold);
                        break;
                    case "1":
                        GenerateInterseccionesTable(document, contenedor as InterseccionCongestionadaContainer, fontNormal, fontBold);
                        break;
                    case "2":
                        GenerateCuellosBotellaTable(document, contenedor as CuelloBotellaContainer, fontNormal, fontBold);
                        break;
                }

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

        private void GenerateDetalleSemaforoTable(Document document, DetalleSemaforoContainer contenedor, PdfFont fontNormal, PdfFont fontBold)
        {
            var table = new Table(UnitValue.CreatePercentArray(6)).UseAllAvailableWidth();

            string[] headers = { "Nodo ID", "Dirección", "Cambios", "Vehículos esperados", "Promedio vehículos/cambio", "Tiempo (s)" };
            AddTableHeaders(table, headers, fontBold);

            for (int i = 0; i < 10; i++)
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
        }

        private void GenerateInterseccionesTable(Document document, InterseccionCongestionadaContainer contenedor, PdfFont fontNormal, PdfFont fontBold)
        {
            var table = new Table(UnitValue.CreatePercentArray(4)).UseAllAvailableWidth();

            string[] headers = { "Nodo ID", "Total Vehículos", "Total Cambios", "Indicador Congestión" };
            AddTableHeaders(table, headers, fontBold);

            for (int i = 0; i < 3; i++)
            {
                var item = contenedor?.GetItem(i);
                if (item == null) continue;

                table.AddCell(SafeCell(item.NodoId.ToString(), fontNormal));
                table.AddCell(SafeCell(item.TotalVehiculos.ToString(), fontNormal));
                table.AddCell(SafeCell(item.TotalCambios.ToString(), fontNormal));
                table.AddCell(SafeCell(item.IndicadorCongestion.ToString("F2"), fontNormal));
            }

            document.Add(table);
        }

        private void GenerateCuellosBotellaTable(Document document, CuelloBotellaContainer contenedor, PdfFont fontNormal, PdfFont fontBold)
        {
            var table = new Table(UnitValue.CreatePercentArray(5)).UseAllAvailableWidth();

            string[] headers = { "Nodo ID", "Dirección", "Total Cambios", "Vehículos Esperados", "Indicador Congestión" };
            AddTableHeaders(table, headers, fontBold);

            for (int i = 0; i < 10; i++)
            {
                var item = contenedor?.GetItem(i);
                if (item == null) continue;

                table.AddCell(SafeCell(item.NodoId.ToString(), fontNormal));
                table.AddCell(SafeCell(item.DireccionSemaforo, fontNormal));
                table.AddCell(SafeCell(item.TotalCambios.ToString(), fontNormal));
                table.AddCell(SafeCell(item.SumaCantidadEspera.ToString(), fontNormal));
                table.AddCell(SafeCell(item.IndicadorCongestion.ToString("F2"), fontNormal));
            }

            document.Add(table);
        }

        private void AddTableHeaders(Table table, string[] headers, PdfFont fontBold)
        {
            foreach (var header in headers)
            {
                var cell = new Cell()
                    .Add(new Paragraph(header ?? string.Empty)
                    .SetFont(fontBold)
                    .SetFontColor(ColorConstants.WHITE));
                cell.SetBackgroundColor(new DeviceRgb(33, 150, 243));
                table.AddHeaderCell(cell);
            }
        }

        private Cell SafeCell(string content, PdfFont font)
        {
            return new Cell().Add(new Paragraph(content ?? "N/D")).SetFont(font);
        }
    }
}