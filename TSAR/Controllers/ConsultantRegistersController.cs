using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using Windows.Graphics.Imaging;
using TSAR.Models;
using ZXing;
using ZXing.Common;

namespace TSAR.Controllers
{
    public class ConsultantRegistersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ConsultantRegisters
        public ActionResult Index()
        {
            var consultantRegisters = db.ConsultantRegisters.Include(c => c.Consultant);
          var today = System.DateTime.Today.ToLongDateString();
          var dates = db.ConsultantRegisters.Select(t => t.DateTime);
         var check = new List<string>();
          foreach (var date in dates)
          {
            var longDateString = date.ToLongDateString();
            check.Add(longDateString);       
          }
         
          var check2 = check.Any(cus => cus.ToString() == today);
          ViewBag.HasSentRegister = check2;
      return View(consultantRegisters.ToList());
        }

        // GET: ConsultantRegisters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsultantRegister consultantRegister = db.ConsultantRegisters.Find(id);
            if (consultantRegister == null)
            {
                return HttpNotFound();
            }
            return View(consultantRegister);
        }

        // GET: ConsultantRegisters/Create
        public ActionResult Create()
        {
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName");
            return View();
        }

        // POST: ConsultantRegisters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConsultantRegisterId,DateTime,Isvalid,ConsultantNum")] ConsultantRegister consultantRegister)
    {

      // instantiate a writer object
      var barcodeWriter = new BarcodeWriter();
      // set the barcode format
      barcodeWriter.Format = BarcodeFormat.QR_CODE;
      barcodeWriter.Options = new EncodingOptions
      {
        Height = 250,Width = 250
      };
      var stream = new MemoryStream();
      //var today = DateTime.Now.GetDateTimeFormats()[3];
      //var today2 = DateTime.Now.GetDateTimeFormats();
      var day = DateTime.Now.Day.ToString();
      var month = DateTime.Now.Month;
      var year = DateTime.Now.Year.ToString();
      var jointoday = day + "-" + month + "-" + year;
    
      // write text and generate a 2-D barcode as a bitmap
      barcodeWriter.Write($"{jointoday}").Save(stream, ImageFormat.Jpeg);
      stream.Position = 0;
      MailMessage mail = new MailMessage();
      mail.From = new MailAddress("dotcomdevelopers19@gmail.com");
      mail.To.Add("devashen@tsar.co.za");
      mail.Subject = "Test";
      string htmlBody = "Please scan this barcode to mark the register";
      htmlBody = htmlBody + "<img src=\"cid:logo\">" + Environment.NewLine;
      mail.IsBodyHtml = true;
      mail.Body = htmlBody;
      AlternateView htmlview = default(AlternateView);
      htmlview = AlternateView.CreateAlternateViewFromString(htmlBody, null, "text/html");
      LinkedResource imageResourceEs = new LinkedResource(stream, "image/jpeg");
      imageResourceEs.ContentId = "logo";
      imageResourceEs.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
      htmlview.LinkedResources.Add(imageResourceEs);
      mail.AlternateViews.Add(htmlview);
      //////

      //set the "from email" address and specify a friendly 'from' name
      mail.From = new MailAddress("dotcomdevelopers19@gmail.com", "Secretary");

      //set the "to" email address
      var consultants = db.Consultants.Select(t=>t.Email).ToList();
      
      foreach (var email in consultants)
      {
        mail.To.Add(email);
      }     
      mail.Subject = $"Clock in for {jointoday}";

      //set the SMTP info
      System.Net.NetworkCredential cred = new System.Net.NetworkCredential("dotcomdevelopers19@gmail.com", "P@ssword0123");
      SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
      smtp.EnableSsl = true;
      smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
      smtp.UseDefaultCredentials = false;
      smtp.Credentials = cred;
      //send the email
      smtp.Send(mail);

      return RedirectToAction("Index");           
           // ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName", consultantRegister.ConsultantNum);
         // return View(consultantRegister);
        }

        // GET: ConsultantRegisters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsultantRegister consultantRegister = db.ConsultantRegisters.Find(id);
            if (consultantRegister == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName", consultantRegister.ConsultantNum);
            return View(consultantRegister);
        }

        // POST: ConsultantRegisters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConsultantRegisterId,DateTime,Isvalid,ConsultantNum")] ConsultantRegister consultantRegister)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consultantRegister).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName", consultantRegister.ConsultantNum);
            return View(consultantRegister);
        }

        // GET: ConsultantRegisters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsultantRegister consultantRegister = db.ConsultantRegisters.Find(id);
            if (consultantRegister == null)
            {
                return HttpNotFound();
            }
            return View(consultantRegister);
        }

        // POST: ConsultantRegisters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ConsultantRegister consultantRegister = db.ConsultantRegisters.Find(id);
            db.ConsultantRegisters.Remove(consultantRegister);
            db.SaveChanges();
            return RedirectToAction("Index");
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
