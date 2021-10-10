using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAplicacionEnNetCore.Clases
{
    public class PaginaCLS
    {
        [Display(Name = "ID Página")]
        public int iidPagina { get; set; }
        [Display(Name = "Mensaje")]
        public string mensaje { get; set; }
        [Display(Name = "Acción")]
        public string accion { get; set; }
        [Display(Name = "Controller")]
        public string controller { get; set; }
    }
}
