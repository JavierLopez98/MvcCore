using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MvcCore.Extensions;
using MvcCore.Helpers;
using MvcCore.Models;


namespace MvcCore.Controllers
{
    public class HomeController : Controller
    {
        PathProvider provider;
        IConfiguration configuration;
        FileUploader uploader;
        MailSender sender;
        

        public HomeController(PathProvider provider,IConfiguration configuration,FileUploader uploader,MailSender sender)
        {
            this.provider = provider;
            this.configuration = configuration;
            this.uploader = uploader;
            this.sender = sender;
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SubirFichero()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubirFichero(IFormFile fichero)
        {
            await this.uploader.UploadFileAsync(fichero, Folders.Images);
            //String filename = fichero.FileName;
            //String ruta = this.provider.MapPath(filename,Folders.Images);
            //using (var stream= new FileStream(ruta,FileMode.Create))
            //{
            //    await fichero.CopyToAsync(stream);
            //}

            ViewData["mensaje"] = "Fichero subido";
            return View();
        }

        public IActionResult EjemploMail()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> EjemploMail(String receptor,String asunto,String mensaje,IFormFile fichero)
        {

            //MailMessage mail = new MailMessage();
            //String usermail = this.configuration["usuariomail"];
            //String passwordmail = this.configuration["passwordmail"];
            //mail.From = new MailAddress(usermail);
            //mail.To.Add(new MailAddress(receptor));
            //mail.Subject = asunto;
            //mail.Body = mensaje;
            //mail.IsBodyHtml = true;
            //mail.Priority = MailPriority.Normal;
             
            if (fichero != null)
            {
                //String filename = fichero.FileName;
                //String path = this.provider.MapPath(filename, Folders.Temporal);
                //using (var stream = new FileStream(path, FileMode.Create))
                //{
                //    await fichero.CopyToAsync(stream);

                //}
                String path =await this.uploader.UploadFileAsync(fichero, Folders.Temporal);
                this.sender.SendEmail(receptor, asunto, mensaje, path);
            }
            else
            {
                this.sender.SendEmail(receptor, asunto, mensaje);
            }
                //    Attachment attachment = new Attachment(path);
                //    mail.Attachments.Add(attachment);
                //}
                //this.sender.SendEmail(mail);

                //String smtpserver = this.configuration["host"];
                //int port = int.Parse( this.configuration["port"]);
                //bool ssl = bool.Parse(this.configuration["ssl"]);
                //bool defaultcredentials = bool.Parse(this.configuration["defaultcredentials"]);
                //SmtpClient smtpclient = new SmtpClient();
                //smtpclient.Host = smtpserver;
                //smtpclient.Port = port;
                //smtpclient.EnableSsl = ssl;
                //smtpclient.UseDefaultCredentials = defaultcredentials;
                //NetworkCredential usercredential = new NetworkCredential(usermail, passwordmail);
                //smtpclient.Credentials = usercredential;
                //smtpclient.Send(mail);
                ViewData["Mensaje"] = "Mensaje enviado";
            return View();
        }

        public IActionResult CifradoHash()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CifradoHash(String contenido, String resultado,String accion)
        {

            String res = CypherService.EncriptarTextoBasico(contenido);

            if (accion.ToLower() == "cifrar") ViewData["Resultado"] = res;
            else if (accion.ToLower() == "comparar")
            {
                if (resultado != res) ViewData["Mensaje"] = "<h1>No son iguales</h1>";
                else ViewData["Mensaje"] = "<h1>Iguales</h1>";
            }
            return View();
        }

        public IActionResult CifradoHashEficiente()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CifradoHashEficiente(String contenido,int iteraciones,String resultado,String accion,String salt)
        {
            //String res = CypherService.CifrarContenido(contenido, iteraciones, salt);
            String res = "";
            if (accion.ToLower() == "cifrar") ViewData["Resultado"] = res;
            else if (accion.ToLower() == "comparar")
            {
                if (resultado != res) ViewData["Mensaje"] = "<h1>No son iguales</h1>";
                else ViewData["Mensaje"] = "<h1>Iguales</h1>";
            }
            return View();
        }

        public IActionResult EjemploSession(String accion)
        {
            if (accion == "almacenar")
            {
                Persona person = new Persona();
                person.Nombre = "Alumno";
                person.Edad = 27;
                person.Hora = DateTime.Now.ToLongDateString();
                //byte[] data=Toolkit.ObjectToByteArray(person);
                //HttpContext.Session.Set("persona", data);
                String data = Toolkit.SerializaJsonObject(person);
                HttpContext.Session.SetString("Persona", data);
                ViewData["Mensaje"] = "Datos almacenados en Session: " + DateTime.Now.ToLongTimeString();
            }
            else if (accion == "mostrar")
            {
                //byte[] data = HttpContext.Session.Get("persona");
                //Persona person = Toolkit.ByteArrayToObject(data) as Persona;
                String data = HttpContext.Session.GetString("Persona");
                Persona person = Toolkit.JsonToObject<Persona>(data);
                ViewData["autor"] = person.Nombre + ", Edad: " + person.Edad;
                ViewData["hora"] = person.Hora;
                ViewData["Mensaje"] = "Mostrando datos";
            }
                return View();
        }

        public IActionResult AlmacenarMultipleSession(String accion)
        {
            if (accion == "almacenar")
            {
                List<Persona> personas = new List<Persona>();
                Persona person1 = new Persona();
                person1.Nombre = "Alumno1";
                person1.Edad = 27;
                person1.Hora = DateTime.Now.ToLongDateString();
                personas.Add(person1);
                Persona person2 = new Persona();
                person2.Nombre = "Alumno2";
                person2.Edad = 23;
                person2.Hora = DateTime.Now.ToLongDateString();
                personas.Add(person2);
                //byte[] data = Toolkit.ObjectToByteArray(personas);
                //HttpContext.Session.Set("personas", data);
                //String data = Toolkit.SerializaJsonObject(personas);
                //HttpContext.Session.SetString("personas", data);

                HttpContext.Session.SetObject("Personas", personas);

                ViewData["mensaje"] = "Almacenado " + DateTime.Now.ToLongDateString();
                
            }
            else if (accion == "mostrar")
            {
                //byte[] data = HttpContext.Session.Get("personas");
                //List<Persona> personas = Toolkit.ByteArrayToObject(data) as List<Persona>;
                //String data = HttpContext.Session.GetString("personas");
                //List<Persona> personas = Toolkit.JsonToObject<List<Persona>>(data);
                List<Persona> personas = HttpContext.Session.GetObject<List<Persona>>("Personas");
                ViewData["mensaje"] = "recuperando datos de Session";
                return View(personas);
            }
            return View();
        }

        
    }
}
