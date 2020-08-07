using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult LandingPage()
        {
            ViewBag.Message = "Landing Page";

            return View();
        }

        [HttpPost]
        public ActionResult Contacto(FormCollection formCollection)
        {
            string name = formCollection["name"];
            string email = formCollection["email"];
            string msj = formCollection["message"];


            //Definir un objeto Mail
            MailMessage correo = new MailMessage();
            correo.To.Add(new MailAddress("olaldeinc@gmail.com"));
            correo.From = new MailAddress("olaldeinc@gmail.com");
            correo.Subject = "Contacto del portafolio web ( " + DateTime.Now.ToString("dd / MMM / yyy hh:mm:ss") + " ) ";
            correo.Body = "La persona "+ name +" quiere contactar contigo<br>Correo:"+ email +"<br>Mensaje:<br>"+msj;
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;


            //Definir un objeto SmtpClient
            string senderEmail = System.Configuration.ConfigurationManager.AppSettings["smtpUserEmail"].ToString();
            string senderPassword = System.Configuration.ConfigurationManager.AppSettings["smtpPassword"].ToString();

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);

            smtp.Port = 587; // 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Host = "smtp.gmail.com";
            //smtp.UseDefaultCredentials = false;


            string output = null;

            //enviamos correo
            try
            {
                smtp.Send(correo);
                correo.Dispose();
                output = "Corre electrónico fue enviado satisfactoriamente.";
            }
            catch (Exception ex)
            {
                output = "Error enviando correo electrónico: " + ex.Message;
            }

            Console.WriteLine(output);

            ViewData["nombre"] = name;
            ViewData["correo"] = email;
            ViewData["msj"] = msj;
            ViewData["output"] = output;
            return View();
        }
    }
}