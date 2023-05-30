using Dapper;
using ManejoDePresupuestos.Models;
using ManejoDePresupuestos.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ManejoDePresupuestos.Controllers
{
    public class TiposCuentasController: Controller
    {
        private readonly IRepositorioTiposCuentas repositorioTiposCuentas;
        private readonly IServiciosUsuarios serviciosUsuarios;
        private readonly string connectionString;

        public TiposCuentasController(IRepositorioTiposCuentas repositorioTiposCuentas, IServiciosUsuarios serviciosUsuarios)
        {
            this.repositorioTiposCuentas= repositorioTiposCuentas;
            this.serviciosUsuarios = serviciosUsuarios;
        }
        public async Task<IActionResult> Index()
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();
            var tiposCuentas = await repositorioTiposCuentas.ObtenerU(usuarioId);
            return View(tiposCuentas);
        }
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Crear(TipoCuenta tipoCuenta)
        {
            if(!ModelState.IsValid)
            {
                return View(tipoCuenta);
            }
            tipoCuenta.UsuarioId = serviciosUsuarios.ObternerUsuario();
            var yaExisteTipoCuenta = await repositorioTiposCuentas.Existe(tipoCuenta.Nombre, tipoCuenta.UsuarioId);

            if (yaExisteTipoCuenta)
            {
                ModelState.AddModelError(nameof(tipoCuenta.Nombre),
                    $"El nombre {tipoCuenta.Nombre} ya existe.");
                return View(tipoCuenta);    
            }
            await repositorioTiposCuentas.Crear(tipoCuenta);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> Editar(int id)
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();
            var tipoCuenta = await repositorioTiposCuentas.ObtenerPorId(id, usuarioId);

            if(tipoCuenta is null)
            {
                return RedirectToAction("NoEncontrado","Home");
            }
            return View(tipoCuenta);
        }
        [HttpPost]
        public async Task<ActionResult> Editar(TipoCuenta tipoCuenta)
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();
            var tipoCuentaExistencia = await repositorioTiposCuentas.ObtenerPorId(tipoCuenta.idTipoCuenta, usuarioId);
            if(tipoCuentaExistencia is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            await repositorioTiposCuentas.Actualizar(tipoCuenta);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Borrar(int idTipoCuenta)
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();
            var tipoCuentaExiste = await repositorioTiposCuentas.ObtenerPorId(idTipoCuenta, usuarioId);
            
            if(tipoCuentaExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            return View(tipoCuentaExiste);
        }
        [HttpPost]
        public async Task<IActionResult> BorrarTipoCuenta(int idTipoCuenta)
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();
            var tipoCuentaExiste = await repositorioTiposCuentas.ObtenerPorId(idTipoCuenta, usuarioId);

            if (tipoCuentaExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            await repositorioTiposCuentas.Borrar(idTipoCuenta);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> VerificarExisteTipoCuenta(string Nombre)
        {
            var UsuarioId = serviciosUsuarios.ObternerUsuario();
            var yaExisteTipoCuenta =  await repositorioTiposCuentas.Existe(Nombre, UsuarioId);
            if (yaExisteTipoCuenta)
            {
                return Json($"El nombre {Nombre} ya existe");
            }
            return Json(true); 
        }
        [HttpPost]
        public async Task<IActionResult> Ordenar([FromBody] int[] idsTipoCuenta)
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();
            var tiposCuentas = await repositorioTiposCuentas.ObtenerU(usuarioId);
            var idsTiposCuentas = tiposCuentas.Select(x => x.idTipoCuenta);
            var idsTipoCuentasDenegadosPorId = idsTipoCuenta.Except(idsTiposCuentas).ToList();

            if(idsTipoCuentasDenegadosPorId.Count > 0)
            {
                return Forbid();
            }
            var tiposCuentasOrdenados = idsTipoCuenta.Select((valor, indice)=> 
                new TipoCuenta() { idTipoCuenta= valor,Orden=indice + 1 }).AsEnumerable();
            await repositorioTiposCuentas.Ordenar(tiposCuentasOrdenados);
            return Ok();
        }

        
    }
}
