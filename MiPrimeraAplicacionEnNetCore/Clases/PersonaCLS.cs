using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraAplicacionEnNetCore.Clases
{
    public class PersonaCLS
    {
        [Display(Name ="Id Persona")]
        public int iidPersona { get; set; }
        [Display(Name ="Ingrese Nombre")]
        [Required(ErrorMessage ="Debe ingresar el nombre")]
        public string nombre { get; set; }
        [Required(ErrorMessage ="Debe ingresar el apellido paterno")]
        [Display(Name ="Ingrese Apellido Paterno")]
        public string  apPaterno { get; set; }
        [Display(Name = "Ingrese Apellido Materno")]
        [Required(ErrorMessage ="Debe ingresar el apellido materno")]
        public string apMaterno { get; set; }
        [Required(ErrorMessage ="Debe ingresar el número teléfonico")]
        [MinLength(8, ErrorMessage ="Lóngitud mínima 8 caracteres")]
        [Display(Name ="Teléfono fijo")]
        public string telefonoFijo { get; set; }
        [Display(Name ="Teléfono celular")]
        public string telefonoCelular { get; set; }
        [DataType(DataType.Date, ErrorMessage ="El formato de fecha no es correcto")]
        [Display(Name ="Fecha de nacimiento")]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        [Required(ErrorMessage ="Debe ingresar la fecha de nacimiento")]
        public DateTime? fechaNacimiento { get; set; }
        
        [Display(Name = "Nombre Completo")]
        public string nombreCompleto { get; set; }
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage ="El email debe ser valido")]
        public string email { get; set; }
        [Display(Name = "Sexo")]
        public string nombreSexo { get; set; }
        [Required(ErrorMessage = "Seleccione un sexo")]
        [Display(Name = "Seleccione una opción")]
        public int? iidSexo { get; set; }
    }
}
