using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcCore.Helpers;
using MvcCore.Interfaces;
using MvcCore.Models;
using MvcCore.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Controllers
{
    public class DepartamentosController : Controller
    {
        IRepositoryHospital repo;
        PathProvider provider;

        public DepartamentosController(IRepositoryHospital repo,PathProvider provider)
        {
            this.repo = repo;
            this.provider = provider;
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
        public async Task<IActionResult> Create(Departamento dept,IFormFile ficheroimagen)
        {
            String filename = ficheroimagen.FileName;
            String ruta =this.provider.MapPath(filename,Folders.Images);
            using (var stream = new FileStream(ruta, FileMode.Create))
            {
                await ficheroimagen.CopyToAsync(stream);
            }
            this.repo.InsertarDepartamento(dept.Numero, dept.Nombre, dept.Localidad, filename);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int iddept)
        {
            Departamento dept = this.repo.BuscarDepartamento(iddept);
            return View(dept);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Departamento dept,IFormFile ficheroImagen)
        {
            String filename = "";
            if (ficheroImagen.Length!=0) {
                filename = ficheroImagen.FileName;
            } else  filename = "defaultimg.png";
            String ruta = this.provider.MapPath(filename, Folders.Images);

            using(var stream =new FileStream(ruta, FileMode.Create))
            {
                await ficheroImagen.CopyToAsync(stream);
            }

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
