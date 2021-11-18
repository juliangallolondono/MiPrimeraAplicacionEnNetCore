using Microsoft.AspNetCore.Mvc;
using MiPrimeraAplicacionEnNetCore.Clases;
using MiPrimeraAplicacionEnNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAplicacionEnNetCore.Controllers
{
    public class EspecialidadController : Controller
    {
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

                return View(listaEspecialidad);
        }


        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(EspecialidadCLS oEspeciliadad)
        {
            try
            {
                using (BDHospitalContext db = new BDHospitalContext())
                {
                    if(!ModelState.IsValid)
                    {
                        return View(oEspeciliadad);
                    }
                    else
                    {
                        Especialidad objeto = new Especialidad();
                        objeto.Nombre = oEspeciliadad.nombre;
                        objeto.Descripcion = oEspeciliadad.description;
                        objeto.Bhabilitado = 1;
                        db.Especialidads.Add(objeto);   
                        db.SaveChanges();   
                    }
                }
            }
            catch (Exception ex)
            {
                return View(oEspeciliadad);
            }
            return RedirectToAction("Index");
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
