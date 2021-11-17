﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiPrimeraAplicacionEnNetCore.Clases;
using MiPrimeraAplicacionEnNetCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MiPrimeraAplicacionEnNetCore.Controllers
{
    public class MedicamentoController : Controller
    {
        public List<SelectListItem> ListaFormaFarmaceutica()
        {
            List<SelectListItem> listaFormaFarmaceutica = new List<SelectListItem>();

            using (BDHospitalContext db = new BDHospitalContext())
            {
                listaFormaFarmaceutica = (from formaFarmaceutica in db.FormaFarmaceuticas
                                       where formaFarmaceutica.Bhabilitado == 1
                                       select new SelectListItem
                                       {
                                           Text = formaFarmaceutica.Nombre,
                                           Value = formaFarmaceutica.Iidformafarmaceutica.ToString()
                                       }).ToList();
                listaFormaFarmaceutica.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
            }

            return listaFormaFarmaceutica;
        }

        public IActionResult Agregar()
        {
            ViewBag.listaFormaFarmaceutica = ListaFormaFarmaceutica();
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(MedicamentosCLS oMedicamentoCLS)
        {
            try
            {
                using (BDHospitalContext db = new BDHospitalContext())
                {
                    if (!ModelState.IsValid)
                    {
                        ViewBag.listaFormaFarmaceutica = ListaFormaFarmaceutica();
                        return View(oMedicamentoCLS);
                    }
                    else
                    {
                        Medicamento oMedicamento = new Medicamento();

                        oMedicamento.Nombre = oMedicamentoCLS.nombre;
                        oMedicamento.Concentracion = oMedicamentoCLS.concentracion;
                        oMedicamento.Precio  = oMedicamentoCLS.precio;
                        oMedicamento.Stock = oMedicamentoCLS.stock;
                        oMedicamento.Iidformafarmaceutica = oMedicamentoCLS.iidFormaFarmaceutica;
                        oMedicamento.Presentacion = oMedicamentoCLS.presentacion;
                        oMedicamento.Bhabilitado = 1;

                        db.Medicamentos.Add(oMedicamento);
                        db.SaveChanges();

                    }
                }
            } catch(Exception ex)
            {
                return View(oMedicamentoCLS);
            }
            return RedirectToAction("Index");
        }
        
        public IActionResult Index(MedicamentosCLS oMedicamentoCLS)
        {
            List<MedicamentosCLS> listaMedicamentos = new List<MedicamentosCLS>();
            ViewBag.listaForma = ListaFormaFarmaceutica();

            using (BDHospitalContext bd = new BDHospitalContext())
            {
                if(oMedicamentoCLS.iidFormaFarmaceutica == 0 ||
                    oMedicamentoCLS.iidFormaFarmaceutica == null)
                {
                    listaMedicamentos = (from medicamento in bd.Medicamentos
                                         join formaFarmaceutica in bd.FormaFarmaceuticas
                                         on medicamento.Iidformafarmaceutica equals formaFarmaceutica.Iidformafarmaceutica
                                         where medicamento.Bhabilitado == 1
                                         select new MedicamentosCLS
                                         {
                                             iidMedicamento = medicamento.Iidmedicamento,
                                             nombre = medicamento.Nombre,
                                             precio = medicamento.Precio,
                                             stock = medicamento.Stock,
                                             nombreFormaFarmaceutica = formaFarmaceutica.Nombre
                                         }).ToList();
                }
                else
                {
                    listaMedicamentos = (from medicamento in bd.Medicamentos
                                         join formaFarmaceutica in bd.FormaFarmaceuticas
                                         on medicamento.Iidformafarmaceutica equals formaFarmaceutica.Iidformafarmaceutica
                                         where medicamento.Bhabilitado == 1
                                         && medicamento.Iidformafarmaceutica == oMedicamentoCLS.iidFormaFarmaceutica
                                         select new MedicamentosCLS
                                         {
                                             iidMedicamento = medicamento.Iidmedicamento,
                                             nombre = medicamento.Nombre,
                                             precio = medicamento.Precio,
                                             stock = medicamento.Stock,
                                             nombreFormaFarmaceutica = formaFarmaceutica.Nombre
                                         }).ToList();
                }
                
                
            }

                return View(listaMedicamentos);

        }
    }
}
