using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace MiPrimeraAplicacionEnNetCore.Clases
{
    public class MedicamentosCLS
    {
        [Display(Name = "ID Medicamento")]
        public int? iidMedicamento { get; set; }
        [Display(Name = "Nombre Medicamento")]
        [Required(ErrorMessage ="Ingrese nombre del medicamento")]
        public string nombre { get; set; }
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "Ingrese el precio del medicamento")]
        public decimal? precio { get; set; }
        [Display(Name = "Stock")]
        [Range(0,1000, ErrorMessage ="Debe estar en el rango de 0 a 1000")]
        [Required(ErrorMessage = "Ingrese stock del medicamento")]
        public int? stock { get; set; }
        [Display(Name = "Forma farmacéutica")]
        public string nombreFormaFarmaceutica { get; set; }
        [Required(ErrorMessage ="Ingrese la forma farmaceutica medicamento")]
        [Display(Name ="Seleccione forma farmaceutica")]
        public int? iidFormaFarmaceutica { get; set; }

        //Información adicional
        [Display(Name = "Concentración")]
        public string concentracion { get; set; }
        [Display(Name ="Presentación")]
        public string presentacion { get; set; }
    }
}
