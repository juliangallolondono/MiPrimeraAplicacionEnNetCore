using System.ComponentModel.DataAnnotations;

namespace MiPrimeraAplicacionEnNetCore.Clases
{
    public class TipoUsuarioCLS
    {
        [Display(Name ="Id Tipo usuario")]
        public int iidTipoUsuario { get; set; }
        [Display(Name ="Nombre")]
        public string nombre { get; set; }
        [Display(Name ="Descripción")]
        public string descripcion { get; set; }
    }
}
