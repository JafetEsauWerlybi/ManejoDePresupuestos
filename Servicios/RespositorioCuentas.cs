using Dapper;
using ManejoDePresupuestos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ManejoDePresupuestos.Servicios
{
    public interface IRepositorioCuentas
    {
        Task Actualizar(CuentaCreacionVM cuentaEditar);
        Task Borrar(int idCuenta);
        Task<IEnumerable<CuentasClientes>> Buscar(int usuarioId);
        Task CrearCuenta(CuentasClientes cuentasClientes);
        Task<CuentasClientes> ObtenerPorId(int id, int usuarioId);
    }
    public class RespositorioCuentas: IRepositorioCuentas
    {

        private readonly string connectionString;
        public RespositorioCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task CrearCuenta(CuentasClientes cuentasClientes)
        {
            using var connection = new SqlConnection(connectionString);
            var idCuenta = await connection.QuerySingleAsync<int>(@"INSERT INTO CuentasClientes (Nombre, TipoCuentaId, Balance, Descripcion)
                                                                values(@Nombre, @TipoCuentaId, @Balance, @Descripcion);
                                                                SELECT SCOPE_IDENTITY();", cuentasClientes); 
            cuentasClientes.idCuenta = idCuenta;
        }

        public async Task<IEnumerable<CuentasClientes>> Buscar(int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<CuentasClientes>(@"select CuentasClientes.idCuenta, CuentasClientes.Nombre, 
                                                                  CuentasClientes.Balance, TipoCuenta.Nombre AS Tipocuenta
                                                                  from CuentasClientes 
                                                                  inner join TipoCuenta 
                                                                  on TipoCuenta.idTipoCuenta = CuentasClientes.TipoCuentaId
                                                                  where TipoCuenta.UsuarioId= @UsuarioId
                                                                  order by TipoCuenta.Orden", new {usuarioId});
        }

        public async Task<CuentasClientes> ObtenerPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<CuentasClientes>(
                @"select CuentasClientes.idCuenta, CuentasClientes.Nombre, CuentasClientes.Balance, CuentasClientes.Descripcion,
                TipoCuentaId
                from CuentasClientes 
                inner join TipoCuenta 
                on TipoCuenta.idTipoCuenta = CuentasClientes.TipoCuentaId
                where TipoCuenta.UsuarioId = @usuarioId AND CuentasClientes.idCuenta = @id",
                new { id, usuarioId });
        }

        public async Task Actualizar(CuentaCreacionVM cuentaEditar)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE CuentasClientes 
                  set Nombre = @Nombre, TipoCuentaId = @TipoCuentaId, Balance= @Balance, Descripcion= @Descripcion
                  WHERE idCuenta=@idCuenta;", cuentaEditar);
        }
        public async Task Borrar(int idCuenta)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE CuentasClientes WHERE idCuenta = @idCuenta;", new { idCuenta });
        }
    }
}
