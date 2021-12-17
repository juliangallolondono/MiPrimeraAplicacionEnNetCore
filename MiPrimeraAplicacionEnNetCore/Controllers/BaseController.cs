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
    }
}
