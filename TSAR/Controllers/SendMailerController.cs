using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSAR.Models;
using System.IO;
using System.Net.Mail;
using System.Data.Entity;
using System.Net;

namespace TSAR.Controllers
{
    public class SendMailerController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();
        // GET: SendMailer
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase fileUploader, MailModel objModelMail)
        {

            if (ModelState.IsValid)
            {
                // Credentials:
                var credentialUserName = "dotcomdevelopers19@gmail.com";
                var sentFrom = "dotcomdevelopers19@gmail.com";
                var pwd = "P@ssword0123";

                using (MailMessage mail = new MailMessage(sentFrom, objModelMail.To))
                {


                    //foreach (var item  in db.Subscriptions.ToList())
                    //{
                    //    objModelMail.To += item.Email + ",";
                    //}

                    mail.Subject = objModelMail.Subject;
                    mail.Body = objModelMail.Body;
                    if (fileUploader != null)
                    {
                        string fileName = Path.GetFileName(fileUploader.FileName);
                        mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
                    }
                    mail.IsBodyHtml = false;
                    System.Net.Mail.SmtpClient client =
            new System.Net.Mail.SmtpClient("smtp.gmail.com");
                    client.Host = "smtp.gmail.com";
                    client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    client.EnableSsl = true;
                    NetworkCredential networkCredential = new NetworkCredential(credentialUserName, pwd);
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(credentialUserName, pwd);

                    client.Port = 587;
                    //smtp.Send(mail);
                    client.Send(mail);

                    ViewBag.Message = "Sent";
                    return View("Index", objModelMail);
                }
            }
            else
            {
                return View();
            }
        }



    }
}
