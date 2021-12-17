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

        public IActionResult Guardar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Eliminar(int iidpagina)
        {
            using (BDHospitalContext db = new())
            {
                Pagina oPagina = db.Paginas.Where(p => p.Iidpagina == iidpagina).First();
                db.Paginas.Remove(oPagina);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Guardar(PaginaCLS oPaginaCLS)
        {
            string nombrePagina="";
            int numeroVecesMensaje =0;

            try
            {
                using (BDHospitalContext db = new BDHospitalContext())
                {
                    
                    if(oPaginaCLS.iidPagina == 0)
                    {
                        nombrePagina = "Agregar";
                        numeroVecesMensaje = db.Paginas.Where(x => x.Mensaje == oPaginaCLS.mensaje).Count();
                    }
                    else
                    {
                        nombrePagina = "Editar";
                        numeroVecesMensaje = db.Paginas.Where(x => x.Mensaje == oPaginaCLS.mensaje && x.Iidpagina != oPaginaCLS.iidPagina).Count();
                    }
                    
                    if(!ModelState.IsValid && numeroVecesMensaje >= 1)
                    {
                        return View(nombrePagina, oPaginaCLS);
                    }
                    else
                    {
                        Pagina oPagina = new Pagina();

                        if (oPaginaCLS.iidPagina == 0)
                        {
                            oPagina.Accion = oPaginaCLS.accion.Trim();
                            oPagina.Mensaje = oPaginaCLS.mensaje.Trim();
                            oPagina.Controlador = oPaginaCLS.controller.Trim();
                            oPagina.Bhabilitado = 1;

                            db.Paginas.Add(oPagina);
                            db.SaveChanges();
                        }
                        else
                        {
                            oPagina = db.Paginas.Where(x=> x.Iidpagina == oPaginaCLS.iidPagina).First();

                            oPagina.Accion = oPaginaCLS.accion;
                            oPagina.Mensaje = oPaginaCLS.mensaje;
                            oPagina.Controlador = oPaginaCLS.controller;
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return View(nombrePagina, oPaginaCLS);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            try
            {
                PaginaCLS oPagina = new();
                using (BDHospitalContext db = new())
                {
                    oPagina = (from pagina in db.Paginas
                                        where pagina.Iidpagina == id
                                        select new PaginaCLS
                                        {
                                            iidPagina = pagina.Iidpagina,
                                            mensaje = pagina.Mensaje,
                                            accion = pagina.Accion,
                                            controller = pagina.Controlador

                                        }).First();
                }

                return View(oPagina);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
    }
}
