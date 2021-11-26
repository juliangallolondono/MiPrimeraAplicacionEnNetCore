using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAplicacionEnNetCore.Clases
{
    public class SedeCLS
    {
        [Display(Name = "Id Sede")]
        [Required]
        public int iidSede { get; set; }
        [Display(Name = "Nombre Sede")]
        [Required]
        public string nombreSede { get; set; }
        [Display(Name = "Dirección")]
        [Required]
        public string direccion { get; set; }
    }
}
