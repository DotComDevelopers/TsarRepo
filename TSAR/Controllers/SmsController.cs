using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using TSAR.SmsHelper;

namespace TSAR.Controllers
{
    public class SmsController : Controller
    {
        // GET: Sms
        //public  ActionResult SmsView()
        //{
        //    return View();
        //}

        public void SmsView()
        {
            
            var restCliennt = new TwilioSmsRestClient();
          var result =   restCliennt.SendMessage("","");
        }

    }
}