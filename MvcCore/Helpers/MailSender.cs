using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MvcCore.Helpers
{
    public class MailSender
    {
        FileUploader uploader;
        IConfiguration configuration;
        public MailSender(FileUploader uploader,IConfiguration configuration)
        {
            this.uploader = uploader;
            this.configuration = configuration;
        }

        public void SendEmail(String receptor,String asunto,String mensaje,String filepath)
        {
            MailMessage mail = new MailMessage();
            String usermail = this.configuration["usuariomail"];
            String passwordmail = this.configuration["passwordmail"];
            mail.From = new MailAddress(usermail);
            mail.To.Add(new MailAddress(receptor));
            mail.Subject = asunto;
            mail.Body = mensaje;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            Attachment attachment = new Attachment(filepath);
            mail.Attachments.Add(attachment);
            String smtpserver = this.configuration["host"];
            int port = int.Parse(this.configuration["port"]);
            bool ssl = bool.Parse(this.configuration["ssl"]);
            bool defaultcredentials = bool.Parse(this.configuration["defaultcredentials"]);
            SmtpClient smtpclient = new SmtpClient();
            smtpclient.Host = smtpserver;
            smtpclient.Port = port;
            smtpclient.EnableSsl = ssl;
            smtpclient.UseDefaultCredentials = defaultcredentials;
            NetworkCredential usercredential = new NetworkCredential(usermail, passwordmail);
            smtpclient.Credentials = usercredential;
            smtpclient.Send(mail);
            
        }
        public void SendEmail(String receptor, String asunto, String mensaje)
        {
            MailMessage mail = new MailMessage();
            String usermail = this.configuration["usuariomail"];
            String passwordmail = this.configuration["passwordmail"];
            mail.From = new MailAddress(usermail);
            mail.To.Add(new MailAddress(receptor));
            mail.Subject = asunto;
            mail.Body = mensaje;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            String smtpserver = this.configuration["host"];
            int port = int.Parse(this.configuration["port"]);
            bool ssl = bool.Parse(this.configuration["ssl"]);
            bool defaultcredentials = bool.Parse(this.configuration["defaultcredentials"]);
            SmtpClient smtpclient = new SmtpClient();
            smtpclient.Host = smtpserver;
            smtpclient.Port = port;
            smtpclient.EnableSsl = ssl;
            smtpclient.UseDefaultCredentials = defaultcredentials;
            NetworkCredential usercredential = new NetworkCredential(usermail, passwordmail);
            smtpclient.Credentials = usercredential;
            smtpclient.Send(mail);

        }
    }
}
