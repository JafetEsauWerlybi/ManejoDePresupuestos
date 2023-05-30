using Dapper;
using ManejoDePresupuestos.Models;
using Microsoft.Data.SqlClient;

namespace ManejoDePresupuestos.Servicios
{
    public interface IRepositorioTransacciones
    {
        Task Actualizar(Transaccion transaccion, decimal montoAnterior, int cuentaAnterior);
        Task Borrar(int idTransaccion);
        Task Crear(Transaccion transaccion);
        Task<IEnumerable<Transaccion>> ObtenerPorCuentaId(ObtenerTransaccionesPorCuenta modelo);
        Task<Transaccion> ObtenerPorId(int idTransaccion, int usuarioId);
        Task<IEnumerable<Transaccion>> ObtenerPorUsuarioId(ParametroObtenerTransaccionesPorUsuario modelo);
    }
    public class RepositorioTransacciones: IRepositorioTransacciones    
    {
        private readonly string connectionString;

        public RepositorioTransacciones(IConfiguration configuration)
        {
                this.connectionString = configuration.GetConnectionString("DefaultConnection");

        }
        public async Task Crear(Transaccion transaccion)
        {
            using var connection = new SqlConnection(connectionString);
            var idTransaccion = await connection.QuerySingleAsync<int>(@"Transacciones_Insertar", 
                                                   new {transaccion.UsuarioId, transaccion.FechaTransaccion, transaccion.Monto,
                                                   transaccion.CategoriaId, transaccion.CuentaId, transaccion.Nota},
                                                   commandType: System.Data.CommandType.StoredProcedure);
            transaccion.idTransaccion= idTransaccion;
        }
        public async Task Actualizar(Transaccion transaccion, decimal montoAnterior, int cuentaAnteriorId)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("TransaccionesActualizar",
                new
                {
                    transaccion.idTransaccion,
                    transaccion.FechaTransaccion,
                    transaccion.Monto,
                    transaccion.CategoriaId,
                    transaccion.CuentaId,
                    transaccion.Nota,
                    montoAnterior,
                    cuentaAnteriorId 
                }, commandType: System.Data.CommandType.StoredProcedure);

        }

        public async Task<Transaccion> ObtenerPorId(int idTransaccion, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Transaccion>(@"SELECT Transacciones.*, cat.TipoOperacionId
                FROM Transacciones
                INNER JOIN Categorias cat
                ON cat.idCategoria = Transacciones.CategoriaId
                WHERE Transacciones.idTransaccion = @idTransaccion AND Transacciones.UsuarioId = @usuarioId",
                        new {idTransaccion, usuarioId});
        }

        public async Task Borrar(int idTransaccion)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("Transacciones_Borrar", new { idTransaccion }, commandType: System.Data.CommandType.StoredProcedure);


        }

        //Seccion 7
        public async Task<IEnumerable<Transaccion>> ObtenerPorCuentaId(ObtenerTransaccionesPorCuenta modelo)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Transaccion>
                 (@"SELECT t.idTransaccion, t.Monto, t.FechaTransaccion, c.Nombre as Categoria,
                    cu.Nombre as Cuenta, c.TipoOperacionId
                    FROM Transacciones t
                    INNER JOIN Categorias c
                    ON c.idCategoria = t.CategoriaId
                    INNER JOIN CuentasClientes cu
                    ON cu.idCuenta = t.CuentaId
                    WHERE t.CuentaId = @CuentaId AND t.UsuarioId = @UsuarioId
                    AND FechaTransaccion BETWEEN @FechaInicio AND @FechaFin", modelo);
        }


        public async Task<IEnumerable<Transaccion>> ObtenerPorUsuarioId(ParametroObtenerTransaccionesPorUsuario modelo)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Transaccion>
                 (@"SELECT t.idTransaccion, t.Monto, t.FechaTransaccion, c.Nombre as Categoria,
                    cu.Nombre as Cuenta, c.TipoOperacionId
                    FROM Transacciones t
                    INNER JOIN Categorias c
                    ON c.idCategoria = t.CategoriaId
                    INNER JOIN CuentasClientes cu
                    ON cu.idCuenta = t.CuentaId
                    WHERE t.UsuarioId = @UsuarioId
                    AND FechaTransaccion BETWEEN @FechaInicio AND @FechaFin
                    ORDER BY t.FechaTransaccion DESC", modelo);
        }
    }
}
