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
        public IActionResult Index()
        {
            List<PaginaCLS> listaPaginas = new List<PaginaCLS>();

            using (BDHospitalContext bd = new BDHospitalContext())
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

                return View(listaPaginas);
        }
    }
}
