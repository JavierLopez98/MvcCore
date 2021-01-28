using Microsoft.AspNetCore.Mvc;
using MvcCore.Helpers;
using MvcCore.Models;
using MvcCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Controllers
{
    public class UsuariosController : Controller
    {
        RepositoryUsuarios repo;

        public UsuariosController(RepositoryUsuarios repo)
        {
            this.repo = repo;
        }
            public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registrar(int Id, String nombre, String User,String Password)
        {

            this.repo.InsertarUsuario(Id, nombre, User, Password);
            ViewData["Mensaje"] = "datos almacenados";
            return View();
        }

        public IActionResult Credenciales()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Credenciales(String username,String Password)
        {
            return View();
        }
    }
}
