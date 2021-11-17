using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiPrimeraAplicacionEnNetCore.Clases;
using MiPrimeraAplicacionEnNetCore.Models;

namespace MiPrimeraAplicacionEnNetCore.Controllers
{
    public class PaginaController : Controller
    {
        public IActionResult Index(PaginaCLS oPaginaCLS)
        {
            List<PaginaCLS> listaPaginas = new List<PaginaCLS>();

            using (BDHospitalContext bd = new BDHospitalContext())
            {
                if(String.IsNullOrEmpty(oPaginaCLS.mensaje))
                {
                    listaPaginas = (from pagina in bd.Paginas
                                    where pagina.Bhabilitado == 1
                                    select new PaginaCLS
                                    {
                                        iidPagina = pagina.Iidpagina,
                                        mensaje = pagina.Mensaje,
                                        accion = pagina.Accion,
                                        controller = pagina.Controlador,

                                    }).ToList();
                    
                }
                else
                {
                    listaPaginas = (from pagina in bd.Paginas
                                    where pagina.Bhabilitado == 1
                                    && pagina.Mensaje.Contains(oPaginaCLS.mensaje)
                                    select new PaginaCLS
                                    {
                                        iidPagina = pagina.Iidpagina,
                                        mensaje = pagina.Mensaje,
                                        accion = pagina.Accion, 
                                        controller = pagina.Controlador
                                    }).ToList();

                    
                }

                ViewBag.mensaje = oPaginaCLS.mensaje;

            }

                return View(listaPaginas);
        }

        public IActionResult Agregar()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Agregar(PaginaCLS oPaginaCLS)
        {
            try
            {
                using (BDHospitalContext db = new BDHospitalContext())
                {
                    if(!ModelState.IsValid)
                    {
                        return View(oPaginaCLS);
                    }
                    else
                    {
                        Pagina oPagina = new Pagina();

                        oPagina.Accion = oPaginaCLS.accion;
                        oPagina.Mensaje = oPaginaCLS.mensaje;
                        oPagina.Controlador = oPaginaCLS.controller;
                        oPagina.Bhabilitado = 1;

                        db.Paginas.Add(oPagina);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                return View(oPaginaCLS);
            }
            return RedirectToAction("Index");
        }
    }
}
