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
        [Required(ErrorMessage ="Debe Ingresar un mensaje")]
        public string mensaje { get; set; }
        [Display(Name = "Acción")]
        [Required(ErrorMessage ="Debe ingresar una acción")]
        public string accion { get; set; }
        [Display(Name = "Controller")]
        [Required(ErrorMessage ="Debe ingresar un controlador")]
        [MinLength(3, ErrorMessage ="La lóngitud mínima es de 3")]
        [MaxLength(100, ErrorMessage ="La lóngitud máxima es de 100")]
        public string controller { get; set; }
    }
}
