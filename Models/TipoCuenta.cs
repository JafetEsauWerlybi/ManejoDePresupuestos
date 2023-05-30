using ManejoDePresupuestos.Models.Validaciones;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ManejoDePresupuestos.Models
{
    public class TipoCuenta
    {
        public int idTipoCuenta { get; set; }
        [Required(ErrorMessage ="El campo de {0} es requerido")]
        [PrimeraLetraMayuscula]
        [Remote(action: "VerificarExisteTipoCuenta", controller:"TiposCuentas")]
        public string Nombre { get; set; }

        public int UsuarioId { get; set; }
        public int Orden { get; set; }

        
    }
}
