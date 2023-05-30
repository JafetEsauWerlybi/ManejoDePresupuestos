namespace ManejoDePresupuestos.Models
{
    public class TransaccionActualizacionVM: CreacionTransaccionVM
    {
        public int CuentaAnteriorId { set; get; }

        public decimal MontoAnterior { set; get;}

        public string urlRetorno { set; get; }
    }
}
