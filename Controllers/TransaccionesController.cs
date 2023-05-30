using AutoMapper;
using ManejoDePresupuestos.Models;
using ManejoDePresupuestos.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Reflection;

namespace ManejoDePresupuestos.Controllers
{
    public class TransaccionesController: Controller
    {
        private readonly IServiciosUsuarios serviciosUsuarios;
        private readonly IRepositorioCuentas repositorioCuentas;
        private readonly IRepositorioCategorias repositorioCategorias;
        private readonly IRepositorioTransacciones repositorioTransacciones;
        private readonly IMapper mapper;
        private readonly IServicioReportes servicioReportes;

        public TransaccionesController(IServiciosUsuarios serviciosUsuarios, IRepositorioCuentas repositorioCuentas,
            IRepositorioCategorias repositorioCategorias, IRepositorioTransacciones repositorioTransacciones,
            IMapper mapper, IServicioReportes servicioReportes)
        {
            this.serviciosUsuarios = serviciosUsuarios;
            this.repositorioCuentas = repositorioCuentas;
            this.repositorioCategorias = repositorioCategorias;
            this.repositorioTransacciones = repositorioTransacciones;
            this.mapper = mapper;
            this.servicioReportes = servicioReportes;
        }
        public async Task<IActionResult> Index(int mes, int año)
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();
            var modelo = await servicioReportes.ObtenerTransaccionesDetalladas(usuarioId, mes, año, ViewBag);
            
            return View(modelo);
        }
        public async Task<IActionResult> Crear()
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();
            var modelo = new CreacionTransaccionVM();
            modelo.Cuentas = await ObtenerCuentas(usuarioId);
            modelo.Categorias = await ObtenerCategorias(usuarioId, modelo.TipoOperacionId);
            return View(modelo);
        }
        [HttpPost]
        
        public async Task<IActionResult> Crear(CreacionTransaccionVM modelo)
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();
            if (!ModelState.IsValid)
            {
                modelo.Cuentas = await ObtenerCuentas(usuarioId);
                modelo.Categorias = await ObtenerCategorias(usuarioId, modelo.TipoOperacionId);
                return View(modelo);
            }
            var cuenta = await repositorioCuentas.ObtenerPorId(modelo.CuentaId, usuarioId);
            if(cuenta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            var categoria = await repositorioCategorias.ObtenerPorId(modelo.CategoriaId, usuarioId);
            if(categoria is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            modelo.UsuarioId = usuarioId;
            if (modelo.TipoOperacionId == TipoOperacion.Gasto)
            {
                modelo.Monto *= -1;
            }
            await repositorioTransacciones.Crear(modelo);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id, string urlRetorno = null)
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();
            var transaccion = await repositorioTransacciones.ObtenerPorId(id, usuarioId);

            if (transaccion is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            var modelo = mapper.Map<TransaccionActualizacionVM>(transaccion);

            modelo.MontoAnterior = modelo.Monto;

            if (modelo.TipoOperacionId == TipoOperacion.Gasto)
            {
                modelo.MontoAnterior = modelo.Monto * -1;
            }

            modelo.CuentaAnteriorId = transaccion.CuentaId;
            modelo.Categorias = await ObtenerCategorias(usuarioId, transaccion.TipoOperacionId);
            modelo.Cuentas = await ObtenerCuentas(usuarioId);
            modelo.urlRetorno = urlRetorno;
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(TransaccionActualizacionVM modelo)
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();

            if (!ModelState.IsValid)
            {
                modelo.Cuentas = await ObtenerCuentas(usuarioId);
                modelo.Categorias = await ObtenerCategorias(usuarioId, modelo.TipoOperacionId);
                return View(modelo);
            }

            var cuenta = await repositorioCuentas.ObtenerPorId(modelo.CuentaId, usuarioId);

            if (cuenta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            var categoria = await repositorioCategorias.ObtenerPorId(modelo.CategoriaId, usuarioId);

            if (categoria is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            var transaccion = mapper.Map<Transaccion>(modelo);

            if (modelo.TipoOperacionId == TipoOperacion.Gasto)
            {
                transaccion.Monto *= -1;
            }

            await repositorioTransacciones.Actualizar(transaccion,
                modelo.MontoAnterior, modelo.CuentaAnteriorId);
            if (string.IsNullOrEmpty(modelo.urlRetorno))
            {
                return RedirectToAction("Index");

            }
            return LocalRedirect(modelo.urlRetorno);

        }

        [HttpPost]
        public async Task<IActionResult> Borrar(int idTransaccion, string urlRetorno = null)
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();

            var transaccion = await repositorioTransacciones.ObtenerPorId(idTransaccion, usuarioId);

            if (transaccion is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioTransacciones.Borrar(idTransaccion);
            if (string.IsNullOrEmpty(urlRetorno))
            {
                return RedirectToAction("Index");

            }
            return LocalRedirect(urlRetorno);


        }

        private async Task<IEnumerable<SelectListItem>> ObtenerCuentas(int usuarioId)
        {
            var cuentas = await repositorioCuentas.Buscar(usuarioId);
            return cuentas.Select(x => new SelectListItem(x.Nombre, x.idCuenta.ToString()));
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerCategorias(int usuarioId,
            TipoOperacion tipoOperacion)
        {
            var categorias = await repositorioCategorias.Obtener(usuarioId, tipoOperacion);
            return categorias.Select(x => new SelectListItem(x.Nombre, x.idCategoria.ToString()));
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerCategorias([FromBody] TipoOperacion tipoOperacion)
        {
            var usuarioId =serviciosUsuarios.ObternerUsuario();
            var categorias = await ObtenerCategorias(usuarioId, tipoOperacion);
            return Ok(categorias);
        }
    }
}
