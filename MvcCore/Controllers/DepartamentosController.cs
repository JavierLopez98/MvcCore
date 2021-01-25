using Microsoft.AspNetCore.Mvc;
using MvcCore.Interfaces;
using MvcCore.Models;
using MvcCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Controllers
{
    public class DepartamentosController : Controller
    {
        IRepositoryHospital repo;

        public DepartamentosController(IRepositoryHospital repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Departamento> depts = this.repo.GetDepartamentos();
            return View(depts);
        }

        public IActionResult Details(int iddept)
        {
            Departamento dept = this.repo.BuscarDepartamento(iddept);
            return View(dept);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Departamento dept)
        {
            this.repo.InsertarDepartamento(dept.Numero, dept.Nombre, dept.Localidad);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int iddept)
        {
            Departamento dept = this.repo.BuscarDepartamento(iddept);
            return View(dept);
        }
        [HttpPost]
        public IActionResult Edit(Departamento dept)
        {
            this.repo.ModificarDepartamento(dept.Numero, dept.Nombre, dept.Localidad);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int iddept)
        {
            if(iddept!=0)
            this.repo.EliminarDepartamento(iddept);
            return RedirectToAction("Index");
        }

        public IActionResult SeleccionMultiple()
        {
            List<Departamento> departamentos = this.repo.GetDepartamentos();
            List<Empleado> empleados = this.repo.GetEmpleados();
            ViewData["Departamentos"] = departamentos;
            return View(empleados);
        }
        [HttpPost]
        public IActionResult SeleccionMultiple(List<int> iddepartamentos)
        {
            List<Departamento> departamentos = this.repo.GetDepartamentos();
            ViewData["Departamentos"] = departamentos;
            List<Empleado> empleados = this.repo.BuscaEmpleadosDept(iddepartamentos);
            return View(empleados);

        }
        
    }
}
