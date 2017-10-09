using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.DynamicData;
using System.Web.Helpers;
using System.Web.Mvc;
using TSAR.Models;

namespace TSAR.Controllers
{
    public class TargetController : Controller
    {
        // GET: Target
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult TargetChart()
        {
            ViewBag.Status = (from r in db.Timesheets
                select r.FullName).Distinct();


            return View();
        }


        //This controller returns an jpeg image you can use bmp.
        public ActionResult Chart(string Status)
        {
            WebImage image;
            var results = (from c in db.Timesheets select c);


            // gets the records grouped based on Year and Month. 

            var groupedByMonth = results
                .Where(a => a.FullName == Status)
                .OrderByDescending(x => x.CaptureDate)
                .GroupBy(x => new { x.CaptureDate.Year, x.CaptureDate.Month }).ToList();


            // gets the months names in a list
            List<string> monthNames = groupedByMonth

                .Select(a => a.FirstOrDefault().CaptureDate.ToString("MMMM"))
                .ToList();
            // gets the total hours per month
            List<double> hoursPerMonth = groupedByMonth
                .Select(a => a.Sum(p => p.Hours))
                .ToList();


            ArrayList xValue = new ArrayList(monthNames);

            ArrayList yValue = new ArrayList(hoursPerMonth);


            if (Status != null)
            {

                image = new Chart(width: 800, height: 400, theme: ChartTheme.Blue)
                    .AddTitle("Consultant targets for the month")

                    .AddSeries("Default", chartType: "Column", xValue: xValue, yValues: yValue)
                    //adding default line chart
                    .AddSeries(chartType: "line",

                        yValues: new[] { 110, 110, 110, 110, 110 })

                    .ToWebImage("jpeg");
            }

            else
            {

                image = new Chart(width: 800, height: 400, theme: ChartTheme.Blue)
                    .AddTitle("Chart")
                    .AddSeries("Default", chartType: "Column", xValue: new[] { "Jan", "Feb", "May" }, yValues: new[] { 20, 40, 90 })
                    .ToWebImage("jpeg");

            }

            return File(image.GetBytes(), "image/jpeg");

        }


        public ActionResult Chart1()
        {
            WebImage image;
            var results = (from c in db.Timesheets select c);


            // gets the records grouped based on Year and Month. 

            var groupedByMonth = results

                .OrderByDescending(x => x.FullName)
                .GroupBy(x => new { x.FullName }).ToList();


            // gets the months names in a list
            List<string> monthNames = groupedByMonth

                .Select(a => a.FirstOrDefault().FullName)
                .ToList();
            // gets the total hours per month
            List<double> hoursPerMonth = groupedByMonth
                .Select(a => a.Sum(p => p.Hours))
                .ToList();


            ArrayList xValue = new ArrayList(monthNames);

            ArrayList yValue = new ArrayList(hoursPerMonth);





            image = new Chart(width: 800, height: 400, theme: ChartTheme.Green)
                .AddTitle("Total hours worked by each consultant")

                .AddSeries("Default", chartType: "line", xValue: xValue, yValues: yValue)



                .ToWebImage("jpeg");


            return File(image.GetBytes(), "image/jpeg");

        }
    }
}