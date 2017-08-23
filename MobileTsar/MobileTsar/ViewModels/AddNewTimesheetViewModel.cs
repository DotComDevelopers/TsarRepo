using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.XamForms.SignaturePad;
using MobileTsar.Helpers;
using MobileTsar.Models;
using MobileTsar.Services;
using MobileTsar.Views;
using Xamarin.Forms;

using Xamarin.Forms.Xaml;

namespace MobileTsar.ViewModels
{
  public class AddNewTimesheetViewModel
  {
    ApiServices _apiServices = new ApiServices();

    public DateTime CaptureDate { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    public string ActivityDescription { get; set; }

    public double Total { get; set; }

    public double Hours { get; set; }

    //public virtual Consultant Consultant { get; set; }
    //public virtual Client Client { get; set; }

    public int Id { get; set; }

    public int ConsultantNum { get; set; }

    public string TicketReference { get; set; }

    public bool SignOff { get; set; }

   // public virtual Travel Travel { get; set; }
    public string MClientAddress { get; set; }
    public string Filename { get; set; }

    public byte[] Signature { get; set; }



    public ICommand AddCommand
    {     
      get
      {
        return new Command(async () =>
        {          
          var timesheet = new Timesheet
          {
          
            ActivityDescription = ActivityDescription,
            Id = Id,
            ConsultantNum = ConsultantNum,
            TicketReference = $"MOBILE {DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Second}{ActivityDescription.Substring(0, 4)}",
            Total = (700 * (EndTime - StartTime).TotalHours),
            CaptureDate = System.DateTime.Now,
            EndTime = EndTime,
            MClientAddress = MClientAddress,
            SignOff = false,
            StartTime = StartTime,
            Filename = null,
            Signature = null,
            Hours = (EndTime - StartTime).TotalHours
          };

          await _apiServices.PostTimesheetAsync(timesheet, Settings.AccessToken);

        });
      }
    }
  }
}
