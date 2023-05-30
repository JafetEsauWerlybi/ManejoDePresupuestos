using System.ComponentModel.DataAnnotations;

namespace ManejoDePresupuestos.Models
{
    public class Categorias
    {
        public int idCategoria { get; set; }

        [Required (ErrorMessage = "El campo nombre debe de estar lleno")]
        [StringLength (maximumLength: 150, ErrorMessage ="No puede ser de más de {1} caracteres")]


        public string Nombre { get; set; }
        [Display(Name ="Tipo de operación")]
        public TipoOperacion TipoOperacionId { get; set; }

        public int UsuarioId { get; set; }
    }
}
