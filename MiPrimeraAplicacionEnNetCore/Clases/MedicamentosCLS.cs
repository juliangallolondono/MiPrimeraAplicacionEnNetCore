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
        public string nombre { get; set; }
        [Display(Name = "Precio")]
        public decimal? precio { get; set; }
        [Display(Name = "Stock")]
        public int? stock { get; set; }
        [Display(Name = "Forma farmacéutica")]
        public string nombreFormaFarmaceutica { get; set; }
    }
}
