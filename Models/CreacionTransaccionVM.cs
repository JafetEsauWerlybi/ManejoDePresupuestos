using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManejoDePresupuestos.Models
{
    public class CreacionTransaccionVM: Transaccion
    {
        public IEnumerable<SelectListItem> Cuentas { get; set; }
        public IEnumerable<SelectListItem> Categorias { get; set; }


    }
}
