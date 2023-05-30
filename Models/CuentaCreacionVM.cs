using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManejoDePresupuestos.Models
{
    public class CuentaCreacionVM: CuentasClientes
    {
        public IEnumerable<SelectListItem> TiposCuentas { get; set; }
    }
}
