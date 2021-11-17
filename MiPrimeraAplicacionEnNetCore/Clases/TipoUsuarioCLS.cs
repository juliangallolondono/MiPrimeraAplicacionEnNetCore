using System.ComponentModel.DataAnnotations;

namespace MiPrimeraAplicacionEnNetCore.Clases
{
    public class TipoUsuarioCLS
    {
        [Display(Name ="Id Tipo usuario")]
        public int iidTipoUsuario { get; set; }
        [Display(Name ="Nombre")]
        [Required(ErrorMessage ="Debe ingresar el usuario")]
        public string nombre { get; set; }
        [Display(Name ="Descripción")]
        [Required(ErrorMessage ="Debe ingresar la descripción")]
        public string descripcion { get; set; }
    }
}
