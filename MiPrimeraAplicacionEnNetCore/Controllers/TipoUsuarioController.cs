﻿using Microsoft.AspNetCore.Mvc;
using MiPrimeraAplicacionEnNetCore.Clases;
using MiPrimeraAplicacionEnNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiPrimeraAplicacionEnNetCore.Controllers
{
    public class TipoUsuarioController : Controller
    {
        public IActionResult Index(TipoUsuarioCLS oTipoUsuarioCLS)
        {
            List<TipoUsuarioCLS> listaTipoUsuario = new List<TipoUsuarioCLS>();
            using (BDHospitalContext db = new BDHospitalContext())
            {
                listaTipoUsuario = (from tipoUsu in db.TipoUsuarios
                                    where tipoUsu.Bhabilitado == 1
                                    select new TipoUsuarioCLS
                                    {
                                        iidTipoUsuario = tipoUsu.Iidtipousuario,
                                        nombre = tipoUsu.Nombre,
                                        descripcion = tipoUsu.Descripcion
                                    }).ToList();

                if (oTipoUsuarioCLS.nombre == null && oTipoUsuarioCLS.descripcion == null && oTipoUsuarioCLS.iidTipoUsuario == 0)
                {
                    ViewBag.Nombre = "";
                    ViewBag.Descripcion = "";
                    ViewBag.IidTipoUsuario = 0;
                }
                else
                {
                    if(oTipoUsuarioCLS.nombre != null)
                    {
                        listaTipoUsuario = listaTipoUsuario.Where(p => p.nombre.Contains(oTipoUsuarioCLS.nombre)).ToList();

                    }

                    if(oTipoUsuarioCLS.iidTipoUsuario != 0)
                    {
                        listaTipoUsuario = listaTipoUsuario.Where(p => p.iidTipoUsuario == oTipoUsuarioCLS.iidTipoUsuario).ToList();
                    }

                    if(oTipoUsuarioCLS.descripcion != null)
                    {
                        listaTipoUsuario = listaTipoUsuario.Where( p => p.descripcion.Contains(oTipoUsuarioCLS.descripcion)).ToList();
                    }
                    ViewBag.Nombre = oTipoUsuarioCLS.nombre;
                    ViewBag.Descripcion = oTipoUsuarioCLS.descripcion;
                    ViewBag.IidTipoUsuario = oTipoUsuarioCLS.iidTipoUsuario;
                }
            }


                return View(listaTipoUsuario);
        }

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(TipoUsuarioCLS oTipoUsuarioCLS)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(oTipoUsuarioCLS);
                }
                else
                {
                    using (BDHospitalContext db = new BDHospitalContext())
                    {
                        TipoUsuario oTipoUsuario = new TipoUsuario();

                        oTipoUsuario.Nombre = oTipoUsuarioCLS.nombre;
                        oTipoUsuario.Descripcion = oTipoUsuarioCLS.descripcion;
                        oTipoUsuario.Bhabilitado = 1;
                        db.TipoUsuarios.Add(oTipoUsuario);
                        db.SaveChanges();
                    }
                }
            }
            catch(Exception ex)
            {
                return View(oTipoUsuarioCLS);
            }
            return RedirectToActionPreserveMethod("Index");

        }

        [HttpPost]
        public IActionResult Eliminar(int iidTipoUsuario)
        {
            using (BDHospitalContext db = new BDHospitalContext())
            {
                TipoUsuario oTipoUsuario = db.TipoUsuarios.Where(p => p.Iidtipousuario == iidTipoUsuario).First();
                db.TipoUsuarios.Remove(oTipoUsuario);
                db.SaveChanges();
            }
                return RedirectToAction("Index");
        }
    }
}
