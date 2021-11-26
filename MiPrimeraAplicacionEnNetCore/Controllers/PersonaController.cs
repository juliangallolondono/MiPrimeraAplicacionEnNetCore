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
                if(oPersonaCLS.iidSexo == 0 || oPersonaCLS.iidSexo == null)
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
                                    on persona.Iidsexo equals sexo.Iidsexo
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

        public IActionResult Agregar()
        {
            llenarSexo();
            return View();
        }
        [HttpPost]
        public IActionResult Guardar(PersonaCLS oPersonaCLS)
        {
            string nombreVista = "";
            int numeroVeces = 0;
            

            try
            {
                llenarSexo();

                using (BDHospitalContext bd = new BDHospitalContext())
                {

                    oPersonaCLS.nombreCompleto = $"{oPersonaCLS.nombre.Trim()} {oPersonaCLS.apPaterno.Trim()} {oPersonaCLS.apMaterno.Trim()}";



                    if (oPersonaCLS.iidPersona == 0)
                    {
                        nombreVista = "Agregar";
                        numeroVeces = bd.Personas.Where(x => x.Nombre.ToLower()+ " " + x.Appaterno.ToLower() + " " + x.Apmaterno.ToLower() == oPersonaCLS.nombreCompleto.ToLower()).Count();
                    }
                    else
                    {
                        nombreVista = "Editar";
                    }

                    if (!ModelState.IsValid || numeroVeces >= 1)
                    {
                        if(numeroVeces>=1) oPersonaCLS.mensajeError =$"{oPersonaCLS.nombreCompleto} ya existe ";
                        return View(nombreVista, oPersonaCLS);
                    }
                    else
                    {

                        Persona oPersona = new Persona();
                        oPersona.Nombre = oPersonaCLS.nombre.Trim();
                        oPersona.Appaterno = oPersonaCLS.apPaterno.Trim();
                        oPersona.Apmaterno = oPersonaCLS.apMaterno.Trim();
                        oPersona.Telefonofijo = oPersonaCLS.telefonoFijo.Trim();
                        oPersona.Telefonocelular = oPersonaCLS.telefonoCelular.Trim();
                        oPersona.Fechanacimiento = oPersonaCLS.fechaNacimiento;
                        oPersona.Email = oPersonaCLS.email.Trim();
                        oPersona.Iidsexo = oPersonaCLS.iidSexo;
                        oPersona.Bhabilitado = 1;
                        bd.Personas.Add(oPersona);
                        bd.SaveChanges();


                    }
                }
               
                
            }
            catch(Exception ex)
            {
                return View(nombreVista, oPersonaCLS);;
            }
            return RedirectToAction("index");
        }
        [HttpPost]
        public IActionResult Eliminar(int iidPersona)
        {
            using (BDHospitalContext db = new())
            {
                Persona persona = db.Personas.Where(x => x.Iidpersona == iidPersona).First();
                persona.Bhabilitado = 0;
                db.SaveChanges();

            }
            return RedirectToAction("Index");

        }

    }
}
