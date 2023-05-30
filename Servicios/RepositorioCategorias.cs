using Dapper;
using ManejoDePresupuestos.Models;
using Microsoft.Data.SqlClient;

namespace ManejoDePresupuestos.Servicios
{
    public interface IRepositorioCategorias
    {
        Task Actualizar(Categorias categoria);
        Task Borrar(int idCategoria);
        Task Crear(Categorias categoria);
        Task<IEnumerable<Categorias>> Obtener(int usuarioId);
        Task<IEnumerable<Categorias>> Obtener(int usuarioId, TipoOperacion tipoOperacionId);
        Task<Categorias> ObtenerPorId(int idCategoria, int usuarioId);
    }
    public class RepositorioCategorias:IRepositorioCategorias
    {
        private readonly string connectionString;

        public RepositorioCategorias(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
            
        }

        public async Task Crear(Categorias categoria)
        {
            using var connection = new SqlConnection(connectionString);
            var idCategoria = await connection.QuerySingleAsync<int>(@"
                INSERT INTO Categorias(Nombre, TipoOperacionId, UsuarioId) 
                values (@Nombre, @TipoOperacionId, @UsuarioId);
                SELECT SCOPE_IDENTITY();", categoria);
            categoria.idCategoria= idCategoria;
        }

        public async Task<IEnumerable<Categorias>> Obtener(int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Categorias>(@"
                   SELECT * FROM Categorias WHERE UsuarioId = @usuarioId", new { usuarioId });
        }

        public async Task<IEnumerable<Categorias>> Obtener(int usuarioId, TipoOperacion tipoOperacionId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Categorias>(@"
                   SELECT * FROM Categorias WHERE UsuarioId = @usuarioId 
                   AND TipoOperacionId= @tipoOperacionId", new { usuarioId, tipoOperacionId});
        }

        public async Task<Categorias> ObtenerPorId(int idCategoria, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Categorias>(@"
                    SELECT * FROM Categorias WHERE idCategoria = @idCategoria and UsuarioId = @usuarioId;",
                    new {idCategoria, usuarioId});
        }
        public async Task Actualizar(Categorias categoria)
        {
            await using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"
                UPDATE Categorias SET Nombre = @Nombre, TipoOperacionId= @TipoOperacionId WHERE idCategoria= @idCategoria", categoria);

        }
        public async Task Borrar(int idCategoria)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE Categorias WHERE idCategoria = @idCategoria", new { idCategoria });
        }
            
    }
}
