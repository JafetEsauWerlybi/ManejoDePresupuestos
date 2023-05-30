using AutoMapper;
using ManejoDePresupuestos.Models;

namespace ManejoDePresupuestos.Servicios
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CuentasClientes, CuentaCreacionVM>();
            CreateMap <TransaccionActualizacionVM, Transaccion>().ReverseMap();
        }
    }
}
