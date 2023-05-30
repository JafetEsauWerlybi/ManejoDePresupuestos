namespace ManejoDePresupuestos.Models
{
    public class IndiceCuentasVM
    {
        public string TipoCuenta { get; set; }
        public IEnumerable<CuentasClientes> Cuentas { get; set;}

        public decimal Balance => Cuentas.Sum(x => x.Balance);

    }
}
