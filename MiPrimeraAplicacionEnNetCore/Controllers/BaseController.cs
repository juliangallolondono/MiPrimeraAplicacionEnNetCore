using Microsoft.AspNetCore.Mvc;
using MiPrimeraAplicacionEnNetCore.Clases;
using MiPrimeraAplicacionEnNetCore.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using cm = System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;

namespace MiPrimeraAplicacionEnNetCore.Controllers
{
    public class BaseController : Controller
    {
        public byte[] ExportarExcelDatos<T>(string[] nombrePropiedades, List<T> lista)
        {
            using (MemoryStream ms = new())
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage ep = new())
                {
                    ep.Workbook.Worksheets.Add("Hoja");
                    ExcelWorksheet ew = ep.Workbook.Worksheets[0];

                    Dictionary<string, string> diccionary = cm.TypeDescriptor.GetProperties(typeof(T)).Cast<cm.PropertyDescriptor>().ToDictionary(p => p.Name, p => p.DisplayName);

                    for (int i = 0; i < nombrePropiedades.Length; i++)
                    {
                        ew.Cells[1, i + 1].Value = diccionary[nombrePropiedades[i]];
                        ew.Column(i + 1).Width = 50;
                    }

                    int fila = 2;
                    int col = 1;

                    foreach (object item in lista)
                    {
                        col = 1;
                        foreach (string propiedad in nombrePropiedades)
                        {
                            ew.Cells[fila, col].Value =
                                item.GetType().GetProperty(propiedad).GetValue(item).ToString();
                            col++;
                        }
                        fila++;
                    }


                    ep.SaveAs(ms);

                    byte[] buffer = ms.ToArray();

                    return buffer;
                }
            }
        }

        public byte[] ExportarPDFDatos<T>(string[] nombrePropiedades, List<T> lista)
        {
            using (MemoryStream ms = new())
            {
                Dictionary<string, string> diccionary = cm.TypeDescriptor.GetProperties(typeof(T)).Cast<cm.PropertyDescriptor>().ToDictionary(p => p.Name, p => p.DisplayName);

                PdfWriter writer = new(ms);

                using (var pdfDoc = new PdfDocument(writer))
                {
                    Document doc = new Document(pdfDoc);
                    Paragraph c1 = new Paragraph("Reporte en PDF");
                    c1.SetFontSize(20);
                    c1.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    doc.Add(c1);

                    Table table = new(nombrePropiedades.Length);
                    Cell celda;

                    for (int i = 0; i < nombrePropiedades.Length; i++)
                    {
                        celda = new Cell();
                        celda.Add(new Paragraph(diccionary[nombrePropiedades[i]]));
                        table.AddHeaderCell(celda);
                    }

                    foreach (object item in lista)
                    {
                        foreach (string propiedad in nombrePropiedades)
                        {
                            celda = new();
                            celda.Add(new Paragraph(item.GetType().GetProperty(propiedad).GetValue(item).ToString()));
                            table.AddCell(celda);
                        }
                    }

                    doc.Add(table);
                    doc.Close();
                    writer.Close();

                }

                return ms.ToArray();
            }
        }

        public byte[] ExportarDatosWord<T>(string[] nombrePropiedades, List<T> lista)
        {
            using (MemoryStream ms = new())
            {
                WordDocument document = new();
                WSection section = document.AddSection() as WSection;
                section.PageSetup.Margins.All = 72;
                section.PageSetup.PageSize = new Syncfusion.Drawing.SizeF(612, 792);
                IWParagraph paragraph = section.AddParagraph();
                paragraph.ApplyStyle("Normal");
                paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;

                WTextRange textRange = paragraph.AppendText("Reporte en word") as WTextRange;
                textRange.CharacterFormat.FontSize = 20f;
                textRange.CharacterFormat.FontName = "Calibri";
                textRange.CharacterFormat.TextColor = Syncfusion.Drawing.Color.Blue;

                document.Save(ms, FormatType.Docx);
                return ms.ToArray();



            }
        }


    }
}
