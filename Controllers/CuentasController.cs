using AutoMapper;
using ManejoDePresupuestos.Models;
using ManejoDePresupuestos.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace ManejoDePresupuestos.Controllers
{
    public class CuentasController : Controller
    {
        private readonly IRepositorioTiposCuentas repositorioTiposCuentas;
        private readonly IServiciosUsuarios serviciosUsuarios;
        private readonly IRepositorioCuentas repositorioCuentas;
        private readonly IMapper mapper;
        private readonly IRepositorioTransacciones repositorioTransacciones;
        private readonly IServicioReportes servicioReportes;

        public CuentasController(IRepositorioTiposCuentas repositorioTiposCuentas, IServiciosUsuarios serviciosUsuarios,
            IRepositorioCuentas repositorioCuentas, IMapper mapper, IRepositorioTransacciones repositorioTransacciones,
            IServicioReportes servicioReportes)
        {
            this.repositorioTiposCuentas = repositorioTiposCuentas;
            this.serviciosUsuarios = serviciosUsuarios;
            this.repositorioCuentas = repositorioCuentas;
            this.mapper = mapper;
            this.repositorioTransacciones = repositorioTransacciones;
            this.servicioReportes = servicioReportes;
        }
        public async Task<IActionResult> Index()
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();
            var cuentasContipoCuentas = await repositorioCuentas.Buscar(usuarioId);
            var modelo = cuentasContipoCuentas
                .GroupBy(x => x.TipoCuenta)
                .Select(grupo => new IndiceCuentasVM
                {
                    TipoCuenta = grupo.Key,
                    Cuentas = grupo.AsEnumerable(),
                }).ToList();
            return View(modelo);
        }
        public async Task<IActionResult> Detalle(int idCuenta, int mes, int año)
        {
             var usuarioId = serviciosUsuarios.ObternerUsuario();
            var cuenta = await repositorioCuentas.ObtenerPorId(idCuenta, usuarioId);
            if (cuenta is null)
            {
                return RedirectToAction("NoEcnontrado","Home");
            }
            
            ViewBag.Cuenta = cuenta.Nombre;
            var modelo = await servicioReportes.ObtenerTransaccionesDetalladasPorCuenta(usuarioId, idCuenta, mes, año, ViewBag);

            return View(modelo);
        }
        [HttpGet]
        public async Task<IActionResult> CrearCuenta()
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();
            var model = new CuentaCreacionVM();
            model.TiposCuentas = await ObtenerTiposCuenta(usuarioId);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CrearCuenta(CuentaCreacionVM cuentaCliente)
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();
            var tipoCuenta = await repositorioTiposCuentas.ObtenerPorId(cuentaCliente.TipoCuentaId, usuarioId);

            if (tipoCuenta is null) {
                return RedirectToAction("NoEncontrado", "Home");
            }
            if (!ModelState.IsValid)
            {
                cuentaCliente.TiposCuentas = await ObtenerTiposCuenta(usuarioId);
                return View(cuentaCliente);
            }

            await repositorioCuentas.CrearCuenta(cuentaCliente);
            return View("Index");
        }
        public async Task<IActionResult> EditarCuenta(int idCuenta)
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();
            var cuenta = await repositorioCuentas.ObtenerPorId(idCuenta, usuarioId);
            if (cuenta is null)
            {
                RedirectToAction("NoEncontrado", "Home");
            }
            var modelo = mapper.Map<CuentaCreacionVM>(cuenta);
            modelo.TiposCuentas = await ObtenerTiposCuenta(usuarioId);
            return View(modelo);
        }
        [HttpPost]
        public async Task<IActionResult> EditarCuenta(CuentaCreacionVM cuentaEditar)
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();
            var cuenta = await repositorioCuentas.ObtenerPorId(cuentaEditar.idCuenta, usuarioId);

            if (cuenta is null)
            {
                return RedirectToAction("NoEcontrado", "Home");
            }
            var tipoCuenta = await repositorioTiposCuentas.ObtenerPorId(cuentaEditar.TipoCuentaId, usuarioId);
            if (tipoCuenta is null)
            {
                return RedirectToAction("NoEcontrado", "Home");
            }
            await repositorioCuentas.Actualizar(cuentaEditar);
            return RedirectToAction("Index");
        }
        private async Task<IEnumerable<SelectListItem>> ObtenerTiposCuenta(int usuarioId)
        {
            var tiposCuentas = await repositorioTiposCuentas.ObtenerU(usuarioId);
            return tiposCuentas.Select(x => new SelectListItem(x.Nombre, x.idTipoCuenta.ToString()));
        }

        [HttpGet]
        public async Task<IActionResult> Borrar(int idCuenta)
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();
            var cuenta = await repositorioCuentas.ObtenerPorId(idCuenta, usuarioId);
            if (cuenta is null)
            {
                return RedirectToAction("NoEcontrado", "Home");
            }
            return View(cuenta);
        }
        [HttpPost]
        public async Task<IActionResult> BorrarCuenta(int idCuenta)
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();
            var cuenta = await repositorioCuentas.ObtenerPorId(idCuenta, usuarioId);
            if (cuenta is null)
            {
                return RedirectToAction("NoEcontrado", "Home");
            }
            await repositorioCuentas.Borrar(idCuenta);
            return RedirectToAction("Index");
        }

        //SECCION 7

        
    }
}
