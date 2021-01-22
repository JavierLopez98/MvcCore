using Microsoft.AspNetCore.Mvc;
using MvcCore.Models;
using MvcCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Controllers
{
    public class AlumnosController : Controller
    {
        RepositoryAlumno repo;
        public AlumnosController(RepositoryAlumno repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Alumno> alumnos = this.repo.GetAlumnos();
            return View(alumnos);
        }
        public IActionResult Details(int idalumno)
        {
            Alumno alumno = this.repo.BuscarAlumno(idalumno);
            return View(alumno);
        }
        public IActionResult Delete(int idalumno)
        {
            this.repo.EliminarAlumno(idalumno);
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Alumno alumno)
        {
            this.repo.InsertarAlumno(alumno.IdAlumno, alumno.Nombre, alumno.Apellido, alumno.Nota);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int idalumno)
        {
            Alumno alumno = this.repo.BuscarAlumno(idalumno);
            return View(alumno);
        }
        [HttpPost]
        public IActionResult Edit(Alumno alumno)
        {
            this.repo.ModificarAlumno(alumno.IdAlumno, alumno.Nombre, alumno.Apellido, alumno.Nota);
            return RedirectToAction("Index");
        }
    }
}
