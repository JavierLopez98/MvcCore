using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcCore.Extensions;
using MvcCore.Helpers;
using MvcCore.Interfaces;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Controllers
{
    public class EmpleadosSessionController : Controller
    {
        IRepositoryHospital repo;
        public EmpleadosSessionController(IRepositoryHospital repo)
        {
            this.repo = repo;
        }
        public IActionResult AlmacenarEmpleados(int? idempleado)
        {
            if (idempleado != null)
            {
                List<int> sessionemp;
                List<CarritoEmpleados> carrito;
                if (HttpContext.Session.GetObject<List<int>>("empleados")==null)
                {
                    sessionemp = new List<int>();
                    carrito = new List<CarritoEmpleados>();
                }
                else
                {
                    sessionemp = HttpContext.Session.GetObject<List<int>>("empleados");
                    carrito = HttpContext.Session.GetObject<List<CarritoEmpleados>>("carrito");
                }
                if (sessionemp.Contains(idempleado.Value) == false)
                {
                    carrito.Add(new CarritoEmpleados
                    {
                        cantidad=1,
                        idempleado=idempleado.Value
                    });
                    sessionemp.Add(idempleado.GetValueOrDefault());
                    HttpContext.Session.SetObject("empleados", sessionemp);
                    HttpContext.Session.SetObject("carrito", carrito);
                }
                
                ViewData["mensaje"] = "datos almacenados: " + sessionemp.Count;
            }
            List<Empleado> empleados = this.repo.GetEmpleados();
            return View(empleados);
        }

        public IActionResult MostrarEmpleados(int? eliminar,int? sumar,int?restar)
        {
            List<CarritoEmpleados> carrito = HttpContext.Session.GetObject<List<CarritoEmpleados>>("carrito");
            List<int> sessionemp = HttpContext.Session.GetObject<List<int>>("empleados");
            
            if (sessionemp == null)
            {
                return View();
            }
            else
            {
                if (eliminar != null)
                {
                    sessionemp.Remove(eliminar.Value);
                    
                    HttpContext.Session.SetObject("empleados", sessionemp);
                }
                if (sumar != null)
                {
                    foreach(CarritoEmpleados carro in carrito)
                    {
                        if (carro.idempleado == sumar.Value) carro.cantidad++;
                    }
                    HttpContext.Session.SetObject("carrito", carrito);
                }
                if (restar != null)
                {
                    foreach (CarritoEmpleados carro in carrito)
                    {
                        if (carro.idempleado == sumar) carro.cantidad--;
                    }
                    HttpContext.Session.SetObject("carrito", carrito);
                }
                List<Empleado> empleados = this.repo.GetEmpleadosSession(sessionemp);
                return View(empleados);
            }
            
        }
        [HttpPost]
        public IActionResult MostrarEmpleados(List<int> cantidades)
        {
            List<int> sessionemp = HttpContext.Session.GetObject<List<int>>("empleados");
            List<Empleado> empleados = this.repo.GetEmpleadosSession(sessionemp);
            String emps = Toolkit.SerializaJsonObject(empleados);
            TempData["Empleados"] = emps;
            TempData["cantidades"] = cantidades;
            return RedirectToAction("Pedidos");
        }

        public IActionResult Pedidos()
        {
            ViewData["cantidades"] = TempData["cantidades"];
            List<Empleado> empleados = Toolkit.JsonToObject<List<Empleado>>(TempData["Empleados"].ToString());
            return View(empleados);
            
            
        }
        [HttpPost]
        public IActionResult Pedidos(List<int> cantidades)
        {
            List<int> sessionemp = HttpContext.Session.GetObject<List<int>>("empleados");
            List<Empleado> empleados = this.repo.GetEmpleadosSession(sessionemp);
            //TempData["Empleados"] = empleados; no se puede hacer
            //TempData["cantidades"] = cantidades;


            ViewData["cantidades"] = cantidades;
            return View(empleados);
        }

        public IActionResult MostrarCarrito()
        {
            List<CarritoEmpleados> carrito = HttpContext.Session.GetObject<List<CarritoEmpleados>>("carrito");
            List<int> sessionemp = HttpContext.Session.GetObject<List<int>>("empleados");
            List<Empleado> empleados = this.repo.GetEmpleadosSession(sessionemp);
            return View(empleados);
        }
    }
}
