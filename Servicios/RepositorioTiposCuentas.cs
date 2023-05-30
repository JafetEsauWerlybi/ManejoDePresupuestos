using Dapper;
using ManejoDePresupuestos.Models;
using Microsoft.Data.SqlClient;

namespace ManejoDePresupuestos.Servicios
{
    public interface IRepositorioTiposCuentas
    {
        Task Actualizar(TipoCuenta tipoCuenta);
        Task Crear(TipoCuenta tipoCuenta);
        Task<bool> Existe(string Nombre, int UsuarioId);
        Task<IEnumerable<TipoCuenta>> ObtenerU(int usuarioId);
        Task<TipoCuenta> ObtenerPorId(int id, int usuarioId);
        Task Borrar(int idTipoCuenta);
        Task Ordenar(IEnumerable<TipoCuenta> tipoCuentasOrdenados);
    }
    public class RepositorioTiposCuentas: IRepositorioTiposCuentas
    {
        private readonly string connectionString;
        public RepositorioTiposCuentas(IConfiguration configuration) {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task Crear(TipoCuenta tipoCuenta)
        {

            tipoCuenta.UsuarioId = 1;

            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>("SP_InsertarTipoCuenta",
                                                            new {usuarioId = tipoCuenta.UsuarioId, nombre= tipoCuenta.Nombre}, 
                                                            commandType:System.Data.CommandType.StoredProcedure);
            tipoCuenta.idTipoCuenta =  id;
        }

        public async Task<bool> Existe(string Nombre, int UsuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(
                                        $"SELECT 1 FROM TipoCuenta WHERE Nombre = @Nombre and UsuarioId=@UsuarioId;",
                                        new { Nombre, UsuarioId });
            return existe == 1;
        }
        public async Task<IEnumerable<TipoCuenta>> ObtenerU(int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
                                                return await connection.QueryAsync<TipoCuenta>(@"Select idTipoCuenta, Nombre, Orden from TipoCuenta 
                                                where UsuarioId = @usuarioId ORDER BY Orden", new {usuarioId} );
        }
        public async Task Actualizar(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection( connectionString);
            await connection.ExecuteAsync(@"UPDATE TipoCuenta set Nombre=@Nombre WHERE idTipoCuenta=@idTipoCuenta", tipoCuenta);
        }
        public async Task<TipoCuenta> ObtenerPorId(int idTipoCuenta, int UsuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<TipoCuenta>(@"Select idTipoCuenta, Nombre, Orden
                                                                          from TipoCuenta where idTipoCuenta= @idTipoCuenta
                                                                            and UsuarioId = @UsuarioId", new {idTipoCuenta, UsuarioId});
        }
        public async Task Borrar(int idTipoCuenta)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE TipoCuenta WHERE idTipoCuenta = @idTipoCuenta", new {idTipoCuenta});  
        }

        public async Task Ordenar(IEnumerable<TipoCuenta> tipoCuentasOrdenados)
        {
            var query = "UPDATE TipoCuenta SET Orden = @Orden WHERE idTipoCuenta= @idTipoCuenta;";
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(query, tipoCuentasOrdenados);
        }
    }
}
