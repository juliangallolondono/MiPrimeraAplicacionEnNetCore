using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using MiPrimeraAplicacionEnNetCore.Clases;
using MiPrimeraAplicacionEnNetCore.Models;

namespace MiPrimeraAplicacionEnNetCore.Controllers
{
    public class SedeController : BaseController
    {
        public static List<SedeCLS> lista = new();
        public FileResult Exportar(string[] nombrePropiedades, string tipoReporte)
        {
            //string[] cabeceras = { "Id Especialidad", "Nombre", "Descripción" };
            //string[] nombrePropiedades = { "iidespecialidad", "nombre", "description" };

            if (tipoReporte == "Excel")
            {
                byte[] buffer = ExportarExcelDatos(nombrePropiedades, lista);
                return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            else if (tipoReporte == "PDF")
            {
                byte[] buffer = ExportarPDFDatos(nombrePropiedades, lista);
                return File(buffer, "application/pdf");
            }
            else if (tipoReporte == "Word")
            {
                byte[] buffer = ExportarDatosWord(nombrePropiedades, lista);
                return File(buffer, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            }


            return null;
        }

        public IActionResult Index(SedeCLS oSedeCLS)
        {
            List<SedeCLS> listaSede = new List<SedeCLS>();

            using (BDHospitalContext db = new BDHospitalContext())
            {
                if(oSedeCLS.nombreSede == "" || oSedeCLS.nombreSede == null)
                {
                    listaSede = (from sede in db.Sedes
                                 where sede.Bhabilitado == 1
                                 select new SedeCLS
                                 {
                                     iidSede = sede.Iidsede,
                                     nombreSede = sede.Nombre,
                                     direccion = sede.Direccion
                                 }).ToList();

                    ViewBag.nombreSede = "";
                }
                else
                {
                    listaSede = (from sede in db.Sedes
                                 where sede.Bhabilitado == 1
                                 && sede.Nombre.Contains(oSedeCLS.nombreSede)
                                 select new SedeCLS
                                 {
                                     iidSede = sede.Iidsede,
                                     nombreSede = sede.Nombre,
                                     direccion = sede.Direccion
                                 }).ToList();

                    ViewBag.nombreSede = oSedeCLS.nombreSede;
                }
            }
            lista = listaSede;

            return View(listaSede);
        }


        [HttpPost]
        public IActionResult Eliminar(int iidSede)
        {
            using (BDHospitalContext bd = new BDHospitalContext())
            {
                Sede oSede = bd.Sedes.Where(p => p.Iidsede == iidSede).First();
                oSede.Bhabilitado = 0;
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Editar(int id)
        {
            SedeCLS oSedeCLS = new SedeCLS();
            using (BDHospitalContext db = new())
            {
                oSedeCLS = (from sede in db.Sedes
                            where sede.Iidsede == id
                            select new SedeCLS
                            {
                                iidSede = sede.Iidsede,
                                nombreSede = sede.Nombre,
                                direccion = sede.Direccion

                            }).First();
            }

            return View(oSedeCLS);
        }

        [HttpPost]
        public IActionResult Guardar(SedeCLS oSedeCLS)
        {
            string nombreVista = oSedeCLS.iidSede == 0 ? "Agregar" : "Editar";
            
            if(!ModelState.IsValid)
            {
                return View(nombreVista, oSedeCLS);
            }
            else
            {
                using (BDHospitalContext db = new())
                {
                    if (oSedeCLS.iidSede != 0)
                    {
                        Sede oSede = db.Sedes.Where(x=> x.Iidsede == oSedeCLS.iidSede).First();

                        oSede.Nombre = oSedeCLS.nombreSede;
                        oSede.Direccion = oSedeCLS.direccion;
                        db.SaveChanges();

                    }
                }
            }
            return RedirectToAction("Index");
            
        }

     

    }
}
