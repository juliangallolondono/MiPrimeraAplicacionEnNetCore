using Microsoft.AspNetCore.Mvc;
using MiPrimeraAplicacionEnNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiPrimeraAplicacionEnNetCore.Clases;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MiPrimeraAplicacionEnNetCore.Controllers
{
    public class PersonaController : Controller
    {
        public void llenarSexo()
        {
            List<SelectListItem> listaSexo = new List<SelectListItem>();
            using (BDHospitalContext db = new BDHospitalContext())
            {
                listaSexo = (from sexo in db.Sexos
                            where sexo.Bhabilitado == 1
                            select new SelectListItem
                            {
                                Text = sexo.Nombre,
                                Value = sexo.Iidsexo.ToString()
                            }).ToList();
                listaSexo.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
            }
            ViewBag.listaSexo = listaSexo;
        }
        
        public IActionResult Index(PersonaCLS oPersonaCLS)
        {
            List<PersonaCLS> listaPersona = new List<PersonaCLS>();
            llenarSexo();
            using (BDHospitalContext db = new BDHospitalContext())
            {
                if(oPersonaCLS.iidSexo == 0)
                {
                    listaPersona = (from persona in db.Personas
                                    join sexo in db.Sexos
                                    on persona.Iidsexo equals
                                    sexo.Iidsexo
                                    where persona.Bhabilitado == 1
                                    select new PersonaCLS
                                    {
                                        iidPersona = persona.Iidpersona,
                                        nombreCompleto = persona.Nombre + " " + persona.Appaterno + " " + persona.Apmaterno,
                                        email = persona.Email,
                                        nombreSexo = sexo.Nombre.ToLower()
                                    }).ToList();
                }
                else
                {
                    listaPersona = (from persona in db.Personas
                                    join sexo in db.Sexos
                                    on persona.Iidsexo equals
                                    sexo.Iidsexo
                                    where persona.Bhabilitado == 1
                                    && persona.Iidsexo == oPersonaCLS.iidSexo
                                    select new PersonaCLS
                                    {
                                        iidPersona = persona.Iidpersona,
                                        nombreCompleto = persona.Nombre + " " + persona.Appaterno + " " + persona.Apmaterno,
                                        email = persona.Email,
                                        nombreSexo = sexo.Nombre.ToLower()
                                    }).ToList();
                }
                

            }

                return View(listaPersona);
        }
    }
}
