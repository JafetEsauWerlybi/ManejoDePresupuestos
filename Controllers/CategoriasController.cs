using ManejoDePresupuestos.Models;
using ManejoDePresupuestos.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace ManejoDePresupuestos.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly IRepositorioCategorias repositorioCategorias;
        private readonly IServiciosUsuarios serviciosUsuarios;

        public CategoriasController(IRepositorioCategorias repositorioCategorias, IServiciosUsuarios serviciosUsuarios)
        {
            this.repositorioCategorias = repositorioCategorias;
            this.serviciosUsuarios = serviciosUsuarios;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();
            var categorias = await repositorioCategorias.Obtener(usuarioId);
            return View(categorias);
        }
        [HttpGet]
        public IActionResult CrearCategorias() { 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CrearCategoria(Categorias categorias)
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();
            if(!ModelState.IsValid)
            {
                return View(categorias);
            }
            categorias.UsuarioId  = usuarioId;
            await repositorioCategorias.Crear(categorias);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Editar(int idCategoria)
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();
            var categoria = await repositorioCategorias.ObtenerPorId(idCategoria, usuarioId);

            if(categoria is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            return View(categoria);
        }
        [HttpPost]
        public async Task<IActionResult> Editar(Categorias categoriasEditar)
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();
            var categoria = await repositorioCategorias.ObtenerPorId(categoriasEditar.idCategoria, usuarioId);
            
            if (!ModelState.IsValid)
            {
                return View(categoriasEditar);
            }
            if (categoria is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            categoriasEditar.UsuarioId = usuarioId;
            await repositorioCategorias.Actualizar(categoriasEditar);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Borrar(int idCategoria)
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();
            var categoria = await repositorioCategorias.ObtenerPorId(idCategoria, usuarioId);

            if (categoria is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> BorrarCategoria(int idCategoria)
        {
            var usuarioId = serviciosUsuarios.ObternerUsuario();
            var categoria = await repositorioCategorias.ObtenerPorId(idCategoria, usuarioId);

            if (categoria is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            
            await repositorioCategorias.Borrar(idCategoria);
            return RedirectToAction("Index");
        }

    }
}
