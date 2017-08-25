using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TSAR.Models;
using System.IO;

namespace TSAR.Controllers
{
    public class AccomodationFilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET:  AccomodationFiles/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultant consultant = db.Consultants.Find(id);
            if (consultant == null)
            {
                return HttpNotFound();
            }
            return View(consultant);
        }

        // GET:  AccomodationFiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST:  AccomodationFiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file.ContentLength > 0)
                {
                    AccomodationFiles accomodationfiles = new AccomodationFiles
                    {
                        file_name = Path.GetFileName(file.FileName),
                        file_type = Path.GetExtension(Path.GetFileName(file.FileName)),
                        file_bytes = ToBytes(file),
                        accomodationfile_id = Guid.NewGuid()
                    };
                    //var path = Path.Combine(Server.MapPath("~/Attach"), accomodationfiles.file_name);
                    //file.SaveAs(path);
                    db.AccomodationFiles.Add(accomodationfiles);
                    db.SaveChanges();
                    return RedirectToAction("SendMail");
                }
            }

            return View();
        }

        private byte[] ToBytes(HttpPostedFileBase file)
        {
            BinaryReader reader = new BinaryReader(file.InputStream);
            return reader.ReadBytes((int)file.ContentLength);
        }
        //pulls from consultant table
        private IEnumerable<MailAddress> GetConsultantEmails()
        {
            List<Consultant> consultants = db.Consultants.ToList();
            List<MailAddress> mails = new List<MailAddress>();
            foreach (var consultant in consultants)
            {
                mails.Add(new MailAddress(consultant.Email, consultant.FirstName));
            }
            return mails;
        }
        public IEnumerable<AccomodationFiles> GetFiles()
        {
             return db.AccomodationFiles.ToList();
 
        }
        //shows the dropdownlist
        public ActionResult SendMail()
        {
            ViewBag.mails = new SelectList(GetConsultantEmails(), "Address", "DisplayName");
            //ViewBag.files = new SelectList(GetFiles(), "accomodationfile_id", "file_name");
            ViewBag.files = new SelectList(GetFiles(), "file_name", "file_name");
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult SendMail(AccomodationFilesEmail model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        /*send email here*/
        //        /*create file*/
        //        var filename = Path.GetFileName(model.file.FileName);
        //        var filebytes = ConvertToBytes(model.file);
        //        var content = Convert.ToBase64String(filebytes);
        //        var filetype = Path.GetFileName(model.file.ContentType);

        //        var client = new SendGridClient("SG.RVwCIXe_Sq2MQOSVLV6N_Q.ONbbb0J0UIJ3qPWqDIrV2vqrgUJV5Xdmr06nMK2mqms");
        //        var message = new SendGridMessage();

        //        message.AddAttachment(filename, content, null, "attachment", null);
        //        message.From = new EmailAddress("noreply@dotcomdevelopers.com", "DotCom Developers");
        //        message.AddTo(model.Address, "You");
        //        message.Subject = model.Subject;
        //        message.HtmlContent = model.Body;
        //        var response = client.SendEmailAsync(message);
        //        ViewBag.message = "Email sent!";
        //    }
        //    ViewBag.mails = new SelectList(GetConsultantEmails(), "Address", "DisplayName", model.Address);
        //    return View();
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendMail(AccomodationFilesEmail model,string fileName)
        {
            

            using (MailMessage mail = new MailMessage())
            {


                
                mail.From = new MailAddress("dotcomdevelopers19@gmail.com");
                mail.To.Add(model.Consultant_Name);
                mail.Subject = model.Subject;
                mail.Body = model.Message;
                if (model.file != null)
                {
                    fileName = Path.GetFileName(model.file.FileName);
                    mail.Attachments.Add(new Attachment(model.file.InputStream, fileName));
                }
                
                mail.IsBodyHtml = false;
                
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("dotcomdevelopers19@gmail.com", "P@ssword0123");
                smtp.EnableSsl = true;
                try
                {
                    smtp.Send(mail);
                }
                catch
                {
                    ViewBag.Error = "";
                }
            
                ViewBag.mails = new SelectList(GetConsultantEmails(), "Address", "DisplayName");
            
                ViewBag.files = new SelectList(GetFiles(), "file_name", "file_name");
                ViewBag.Message = "Sent";
                return View();
            }

            
        }
        public FileResult GenerateFile(HttpPostedFileBase file)
        {
            var bytes = ToBytes(file);
            MemoryStream ms = new MemoryStream(bytes);
            return File(bytes, file.FileName, "Accomodation Files");
        }
      

      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
