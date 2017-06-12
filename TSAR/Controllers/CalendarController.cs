using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



using DHTMLX.Scheduler;
using DHTMLX.Common;
using DHTMLX.Scheduler.Data;
using DHTMLX.Scheduler.Controls;


using TSAR.Models;
namespace TSAR.Controllers
{
    [Authorize(Roles = "Admin,Consultant")]
    public class CalendarController : Controller

 {      ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            //Being initialized in that way, scheduler will use CalendarController.Data as a the datasource and CalendarController.Save to process changes
            var schedulerget = new DHXScheduler(this);
            ViewBag.Name = User.Identity.Name;
            schedulerget.Extensions.Add(SchedulerExtensions.Extension.Collision);
            schedulerget.Extensions.Add(SchedulerExtensions.Extension.Limit);
            schedulerget.Config.time_step = 30;
            schedulerget.Config.multi_day = true;
            schedulerget.Config.limit_time_select = true;
            schedulerget.Config.cascade_event_display = true;
            schedulerget.Config.first_hour = 8;
            schedulerget.Config.last_hour = 18;


            schedulerget.LoadData = true;
            schedulerget.EnableDataprocessor = true;
            var conuser = User.Identity.Name;
            var user = (from Consultant c in db.Consultants where c.ConsultantUserName == conuser select c.ConsultantNum).FirstOrDefault();
            ViewBag.TicketRef = (from Ticket t in db.Tickets
                                 where t.Status == "Open Ticket"
                                 && t.ConsultantId == user
                                 select t.TicketReference).FirstOrDefault();



            schedulerget.Lightbox.AddDefaults();




            return View(schedulerget);
        }

        [HttpPost]
        public ActionResult Index(DHXScheduler scheduler)
        {
            //Being initialized in that way, scheduler will use CalendarController.Data as a the datasource and CalendarController.Save to process changes
            // var scheduler = new DHXScheduler(this);
            ViewBag.Name = User.Identity.Name;
            scheduler.Extensions.Add(SchedulerExtensions.Extension.Collision);
            scheduler.Extensions.Add(SchedulerExtensions.Extension.Limit);
            scheduler.Config.time_step = 30;
            scheduler.Config.multi_day = true;
            scheduler.Config.limit_time_select = true;
            scheduler.Config.cascade_event_display = true;
            scheduler.Config.first_hour = 8;
            scheduler.Config.last_hour = 19;


            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;
            var conuser = User.Identity.Name;
            var user = (from Consultant c in db.Consultants where c.ConsultantUserName == conuser select c.ConsultantNum).FirstOrDefault();
            ViewBag.TicketRef = (from Ticket t in db.Tickets
                                 where t.Status == "Open Ticket"
                                 && t.ConsultantId == user
                                 select t.TicketReference).FirstOrDefault();



            scheduler.Lightbox.AddDefaults();



            return View(scheduler);
        }

        public ContentResult Data()
        {
            var data = new SchedulerAjaxData(new ApplicationDbContext().Events);


            return (ContentResult)data;
        }

        public ContentResult Save(int? id, FormCollection actionValues)
        {
            ViewBag.Ticket = (from Ticket t in db.Tickets
                              where t.Status == "Open Ticket"
                              select t.Date).FirstOrDefault();
            var action = new DataAction(actionValues);

            try
            {
                var changedEvent = (Event)DHXEventsHelper.Bind(typeof(Event), actionValues);
                var data = new ApplicationDbContext();


                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        changedEvent.start_date = ViewBag.Ticket;
                        changedEvent.end_date = ViewBag.Ticket;
                        changedEvent.text = "from ticket";
                        data.Events.Add(changedEvent);
                        ViewBag.Name = changedEvent.name;

                        break;
                    case DataActionTypes.Delete:
                        changedEvent = data.Events.SingleOrDefault(ev => ev.id == action.SourceId);
                        data.Events.Remove(changedEvent);
                        break;
                    default:// "update"                          
                        var eventToUpdate = data.Events.SingleOrDefault(ev => ev.id == action.SourceId);
                        DHXEventsHelper.Update(eventToUpdate, changedEvent, new List<string>() { "id" });

                        break;
                }
                data.SaveChanges();
                action.TargetId = changedEvent.id;
            }
            catch
            {
                action.Type = DataActionTypes.Error;
            }
            return (ContentResult)new AjaxSaveResponse(action);
        }




        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,text,start_date,end_date")] Event events)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(events);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(events);
        }
    }
}

