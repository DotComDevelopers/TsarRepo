using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileTsar.Helpers;
using Xamarin.Forms;

namespace MobileTsar.Views
{
  public partial class TimesheetsPage : ContentPage
  {
    public TimesheetsPage()
    {
      InitializeComponent();
    }

    private async void Button_OnClicked(object sender, EventArgs e)
    {
       await Navigation.PushAsync(new AddNewTimesheetPage());
    }

    private async  void LogOffButton_OnClicked(object sender, EventArgs e)
    {
     
      await DisplayAlert("Success", "Logged Off", "Close");
      await Navigation.PushModalAsync(new LoginPage());
      Settings.AccessToken = "";
    }
  }
}
