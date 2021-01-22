using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MvcCore.Models;
using MvcCore.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCore.Controllers
{
    public class JoyeriasController : Controller
    {
        RepositoryJoyerias repo;
     public JoyeriasController(RepositoryJoyerias repo)
        {
            this.repo = repo;

        }
        public IActionResult Index()
        {
            //String filename = "joyerias.xml";
            //String path = Path.Combine(this.environment.WebRootPath, "documents", filename);
            ////para poder realizar consulta linq to XML
            ////se utiliza el objeto xDocument apuntando a una ruta o a un String que leamos de algun sitio
            //XDocument docxml = XDocument.Load(path);

            //consulta manual
            //List<Joyeria> joyerias = new List<Joyeria>();
            ////descendants es casesensitive
            //var consulta = from datos in docxml.Descendants("joyeria") select datos;
            //foreach(var dato in consulta)
            //{
            //    Joyeria joy = new Joyeria();
            //    joy.Nombre = dato.Element("nombrejoyeria").Value;
            //    joy.Direccion = dato.Element("direccion").Value;
            //    joy.Telefono = dato.Element("telf").Value;
            //    joy.Cif = dato.Attribute("cif").Value;
            //    joyerias.Add(joy);
            //}

            //consulta automatica de creacion de objetos y extraccion
            //var consulta = from datos in docxml.Descendants("joyeria")
            //               select new Joyeria
            //               {
            //                   Nombre = datos.Element("nombrejoyeria").Value,
            //                   Direccion = datos.Element("direccion").Value,
            //                   Telefono = datos.Element("telf").Value,
            //                   Cif = datos.Attribute("cif").Value
            //               };
            //List<Joyeria> joyerias = consulta.ToList();
            List<Joyeria> joyerias = this.repo.GetJoyerias();
            return View(joyerias);
        }
    }
}
