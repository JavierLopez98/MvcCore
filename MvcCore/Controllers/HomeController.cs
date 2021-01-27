using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MvcCore.Helpers;

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
            byte[] entrada;
            byte[] salida;

            //conversor
            UnicodeEncoding encoding = new UnicodeEncoding();

            //cifrador
            SHA1Managed sha = new SHA1Managed();
            entrada = encoding.GetBytes(contenido);
            salida = sha.ComputeHash(entrada);
            String res = encoding.GetString(salida);
            

            if (accion.ToLower() == "cifrar") ViewData["Resultado"] = res;
            else if (accion.ToLower() == "comparar")
            {
                if (resultado != res) ViewData["Mensaje"] = "<h1>No son iguales</h1>";
                else ViewData["Mensaje"] = "<h1>Iguales</h1>";
            }
            return View();
        }
    }
}
