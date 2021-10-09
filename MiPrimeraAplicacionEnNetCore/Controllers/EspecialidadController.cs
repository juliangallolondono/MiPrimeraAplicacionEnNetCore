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
        public IActionResult Index()
        {
            List<EspecialidadCLS> listaEspecialidad = new List<EspecialidadCLS>();

            using (BDHospitalContext db = new BDHospitalContext() )
            {
                listaEspecialidad = (from especialidad in db.Especialidads
                                     where especialidad.Bhabilitado == 1
                                     select new EspecialidadCLS
                                     {
                                         iidespecialidad = especialidad.Iidespecialidad,
                                         nombre = especialidad.Nombre,
                                         description = especialidad.Descripcion
                                     }).ToList();

            }

                return View(listaEspecialidad);
        }
    }
}
