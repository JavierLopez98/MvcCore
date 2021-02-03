using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Controllers
{
    public class CachingController : Controller
    {
        private IMemoryCache memory;
        public CachingController(IMemoryCache memory)
        {
            this.memory = memory;
        }
        public IActionResult HoraSistema(int? tiempo)
        {
            if (tiempo == null)
            {
                tiempo = 5;
            }
            String fecha = DateTime.Now.ToLongTimeString()
                + ", " + DateTime.Now.ToShortDateString();
            if (this.memory.Get("fecha") == null)
            {
                this.memory.Set("fecha", fecha, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(tiempo.GetValueOrDefault())));
                ViewData["fecha"] = this.memory.Get("fecha");
                ViewData["mensaje"] = "almacenando en Caché " + tiempo.Value + "s";
            }
            else
            {
                fecha = this.memory.Get("fecha").ToString();
                ViewData["mensaje"] = "recuperando de Caché";
                ViewData["fecha"] = fecha;
            }
            return View();
        }
        [ResponseCache(Duration = 15, VaryByQueryKeys =new string[] {"*"})]
        public IActionResult HoraSistemaDistribuida(String dato)
        {
            String fecha = DateTime.Now.ToLongTimeString()
                + ", " + DateTime.Now.ToShortDateString();
            ViewData["fecha"] = fecha;
            return View();
        }
        
    }
}
