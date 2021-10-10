using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiPrimeraAplicacionEnNetCore.Clases;
using MiPrimeraAplicacionEnNetCore.Models;

namespace MiPrimeraAplicacionEnNetCore.Controllers
{
    public class MedicamentoController : Controller
    {
        public IActionResult Index()
        {
            List<MedicamentosCLS> listaMedicamentos = new List<MedicamentosCLS>();

            using (BDHospitalContext bd = new BDHospitalContext())
            {
                listaMedicamentos = ( from medicamento in bd.Medicamentos
                                      join formaFarmaceutica in bd.FormaFarmaceuticas
                                      on medicamento.Iidformafarmaceutica equals formaFarmaceutica.Iidformafarmaceutica
                                      where medicamento.Bhabilitado == 1
                                      select new MedicamentosCLS { 
                                          iidMedicamento = medicamento.Iidmedicamento,
                                          nombre = medicamento.Nombre,
                                          precio = medicamento.Precio,
                                          stock = medicamento.Stock,
                                          nombreFormaFarmaceutica = formaFarmaceutica.Nombre
                                      }).ToList();
            }

                return View(listaMedicamentos);

        }
    }
}
