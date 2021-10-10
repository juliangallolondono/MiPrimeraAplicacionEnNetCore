using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAplicacionEnNetCore.Clases
{
    public class EspecialidadCLS
    {
        [Display(Name="Id Especialidad")]
        public int iidespecialidad { get; set; }
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Descripcion")]
        public string description { get; set; }
    }
}
