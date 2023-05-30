using System.ComponentModel.DataAnnotations;

namespace ManejoDePresupuestos.Models
{
    public class Transaccion
    {
        public int idTransaccion {  get; set; }

        public int UsuarioId { get; set; }
        [Display(Name = "Fecha de la transaccion")]
        [DataType(DataType.Date)]
        public DateTime FechaTransaccion { get; set; } = DateTime.Today;
        public decimal Monto { get; set; }

        [StringLength(maximumLength: 1000, ErrorMessage = "No puedes poner mas de {1} caracteres")]
        public string Nota { get; set; }

        [Range(1, maximum: int.MaxValue, ErrorMessage="Debe de seleccionar una categoria")]
        public int CategoriaId { get; set; }
        [Range(1, maximum: int.MaxValue, ErrorMessage = "Debe de seleccionar una cuenta")]
        public int CuentaId { get; set; }
        public TipoOperacion TipoOperacionId { get; set; } = TipoOperacion.Gasto;


        public string Cuenta { get; set; }
        public string Categoria { get; set; }   
    }
}
