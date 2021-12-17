using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAplicacionEnNetCore.Clases
{
    public class EspecialidadCLS
    {
        [Display(Name="Id Especialidad")]
        [DisplayName("Id de la Especialidad")]
        public int iidespecialidad { get; set; }
        [Display(Name = "Nombre Especialidad")]
        [Required(ErrorMessage ="Ingrese el nombre de la especialidad")]
        [DisplayName("Nombre de la especialidad")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Ingrese la descripción de la especialidad")]
        [Display(Name = "Descripcion")]
        [DisplayName("Descripción de la especialidad")]
        public string description { get; set; }
        public string mensajeError { get; set; }
    }
}
