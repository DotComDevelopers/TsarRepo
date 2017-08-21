using MobileTsar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileTsar.Helpers;
using MobileTsar.Models;
using MobileTsar.Services;
using Xamarin.Forms;

namespace MobileTsar.Views
{
  public partial class AddNewTimesheetPage : ContentPage
  {
    public AddNewTimesheetPage()
    {
      InitializeComponent();

      GetTicketref();
      GetClientName();
      GetConsultantName();

    }

    // Uses web service to get tickets and populate drop down
    public async void GetTicketref()
    {
      var api = new ApiServices();
      var token = Settings.AccessToken;
      var list = await api.GetTimesheetAsync(token);
      var ret = list.Select(t => t.TicketReference).ToList();
      foreach (var ts in ret)
      {
        TicketPicker.Items.Add(ts);
      }
    }
    public async void GetConsultantName()
    {
      var api = new ApiServices();
      var token = Settings.AccessToken;

      var list = await api.GetConsultantAsync(token);
      var ret = list.Select(t => t.FirstName).ToList();
      foreach (var ts in ret)
      {
        ConsultantPicker.Items.Add(ts);
      }
    }

    // Uses web service to get cleint name and populate drop down
    public async void GetClientName()
    {
      var api = new ApiServices();
      var token = Settings.AccessToken;
      var list = await api.GetClientAsync(token);
      var ret = list.Select(t => t.ClientName).ToList();
      foreach (var ts in ret)
      {
        ClientPicker.Items.Add(ts);
      }
    }

    private async void Button_OnClicked(object sender, EventArgs e)
    {
            await DisplayAlert("Done", "New Timesheet added!", "Close");
            //await Navigation.PushAsync(new TimesheetSign());
    }

  }
}
