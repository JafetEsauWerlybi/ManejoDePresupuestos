using ManejoDePresupuestos.Models.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ManejoDePresupuestos.Models
{
    public class CuentasClientes
    {
        public int idCuenta { get; set; }


        [Required (ErrorMessage="El campo {0} es requerido")]
        [StringLength(maximumLength: 50)]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        [Display(Name="Tipo cuenta")]
        public int TipoCuentaId { get; set; }

        public decimal Balance { get; set; }
        [StringLength(maximumLength: 250)]
        public string Descripcion { get; set; } 

        public string TipoCuenta { get;set; }
    }
}
