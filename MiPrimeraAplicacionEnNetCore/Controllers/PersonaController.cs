using Microsoft.AspNetCore.Mvc;
using MiPrimeraAplicacionEnNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiPrimeraAplicacionEnNetCore.Clases;

namespace MiPrimeraAplicacionEnNetCore.Controllers
{
    public class PersonaController : Controller
    {
        public IActionResult Index()
        {
            List<PersonaCLS> listaPersona = new List<PersonaCLS>();
            using (BDHospitalContext db = new BDHospitalContext())
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

                return View(listaPersona);
        }
    }
}
