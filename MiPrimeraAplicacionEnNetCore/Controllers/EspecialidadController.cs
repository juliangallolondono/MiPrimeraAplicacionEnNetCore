using Microsoft.AspNetCore.Mvc;
using MiPrimeraAplicacionEnNetCore.Clases;
using MiPrimeraAplicacionEnNetCore.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using cm =  System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace MiPrimeraAplicacionEnNetCore.Controllers
{
    public class EspecialidadController : BaseController
    {

        public static List<EspecialidadCLS> lista { get; set; }
        

        public FileResult Exportar(string[] nombrePropiedades, string tipoReporte)
        {
            //string[] cabeceras = { "Id Especialidad", "Nombre", "Descripción" };
            //string[] nombrePropiedades = { "iidespecialidad", "nombre", "description" };

            if(tipoReporte == "Excel")
            {
                byte[] buffer = ExportarExcelDatos(nombrePropiedades, lista);
                return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            else if(tipoReporte == "PDF")
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





        public IActionResult Index(EspecialidadCLS oEspecialidadCLS)
        {
            List<EspecialidadCLS> listaEspecialidad = new List<EspecialidadCLS>();

            using (BDHospitalContext db = new BDHospitalContext() )
            {
                if (oEspecialidadCLS.nombre == null || oEspecialidadCLS.nombre == "")
                {
                    listaEspecialidad = (from especialidad in db.Especialidads
                                         where especialidad.Bhabilitado == 1
                                         select new EspecialidadCLS
                                         {
                                             iidespecialidad = especialidad.Iidespecialidad,
                                             nombre = especialidad.Nombre,
                                             description = especialidad.Descripcion
                                         }).ToList();

                    ViewBag.nombreEsPecialidad = "";
                }
                else
                {
                    listaEspecialidad = (from especialidad in db.Especialidads
                                         where especialidad.Bhabilitado == 1
                                         && especialidad.Nombre.Contains(oEspecialidadCLS.nombre)
                                         select new EspecialidadCLS
                                         {
                                             iidespecialidad = especialidad.Iidespecialidad,
                                             nombre = especialidad.Nombre,
                                             description = especialidad.Descripcion
                                         }).ToList();
                    ViewBag.nombreEspecialidad = oEspecialidadCLS.nombre;
                }

                

            }
            lista = listaEspecialidad;
             return View(listaEspecialidad);
        }


        public IActionResult Guardar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(EspecialidadCLS oEspeciliadad)
        {
            int numeroVeces = 0;
            string nombreVista = oEspeciliadad.iidespecialidad == 0 ? "Agregar" : "Editar";
            try
            {
                using (BDHospitalContext db = new BDHospitalContext())
                {
                    if(oEspeciliadad.iidespecialidad == 0) numeroVeces = db.Especialidads.Where(x => x.Nombre.ToLower().Trim() == oEspeciliadad.nombre.ToLower().Trim()).Count();
                    else numeroVeces = db.Especialidads.Where(x => x.Nombre.ToLower().Trim() == oEspeciliadad.nombre.ToLower().Trim() && x.Iidespecialidad != oEspeciliadad.iidespecialidad ).Count();

                    if (!ModelState.IsValid || numeroVeces > 0)
                    {
                        if (numeroVeces >= 1) oEspeciliadad.mensajeError = "El Nombre de la especialidad ya existe";
                        return View(nombreVista, oEspeciliadad);
                    }
                    else
                    {
                        if(oEspeciliadad.iidespecialidad == 0)
                        {
                            
                            Especialidad objeto = new Especialidad();
                            objeto.Nombre = oEspeciliadad.nombre.Trim();
                            objeto.Descripcion = oEspeciliadad.description.Trim();
                            objeto.Bhabilitado = 1;
                            db.Especialidads.Add(objeto);
                            db.SaveChanges();
                        }
                        else
                        {
                            Especialidad objeto = new Especialidad();
                            objeto = db.Especialidads.Where(x => x.Iidespecialidad == oEspeciliadad.iidespecialidad).First();


                            objeto.Nombre = oEspeciliadad.nombre;
                            objeto.Descripcion = oEspeciliadad.description;
                            db.SaveChanges();
                        }
                         
                    }
                }
            }
            catch (Exception ex)
            {
                return View(nombreVista, oEspeciliadad);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {

            EspecialidadCLS oEspecialidad = new();
            using (BDHospitalContext db = new())
            {
                oEspecialidad = (from especialidad in db.Especialidads
                                 where especialidad.Iidespecialidad == id
                                 select new EspecialidadCLS
                                 {
                                     iidespecialidad = especialidad.Iidespecialidad,
                                     nombre = especialidad.Nombre,
                                     description = especialidad.Descripcion
                                 }).First();
            }
                return View(oEspecialidad);
        }

        [HttpPost]
        public IActionResult Eliminar(int iidespecialidad)
        {
            string error;
            try
            {
                using (BDHospitalContext db = new BDHospitalContext() )
                {
                    Especialidad oEspecialidad = db.Especialidads.Where(p => p.Iidespecialidad == iidespecialidad).First();
                    oEspecialidad.Bhabilitado = 0;
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                error = ex.Message;
            }
            
            return RedirectToAction("Index");
        }
        
    }
}
