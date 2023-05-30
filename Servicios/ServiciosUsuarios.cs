namespace ManejoDePresupuestos.Servicios
{
    public interface IServiciosUsuarios
    {
        public int ObternerUsuario();
    }
    public class ServiciosUsuarios: IServiciosUsuarios
    {
        public int ObternerUsuario()
        {
            return 1;
        }
    }
}
