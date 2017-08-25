using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Android.Telephony;
using MobileTsar.Helpers;
using Xamarin.Forms;
using MobileTsar.Models;
using MobileTsar.Services;
using MobileTsar.ViewModels;

namespace MobileTsar.Views
{
  public partial class TimesheetsPage : ContentPage
  {
      private int id;
    public TimesheetsPage()
    {
          
      InitializeComponent();
      GetUsername();
    }
    private async void GetUsername()
    {
      var api = new ApiServices();
      var token = Settings.AccessToken;

      var list = await api.GetUsernameAsync(token);
      var ret = list.Email;
      UsernameLabel.Text = $"Hello {ret}";
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

        private async void SignPage(object sender, ItemTappedEventArgs e)
        {
           var  timesheet = TimesheetListView.SelectedItem as Timesheet;
          if (timesheet.SignOff==true)
          {        
            await DisplayAlert("Oops!", "Timesheet Has been signed already", "Close");
          }
          else
          {
            if (timesheet == null) return;
            var mainViewModel = BindingContext as TimesheetsViewModel;
            if (mainViewModel == null) return;
            mainViewModel.SelectedTimesheet = timesheet;
            await Navigation.PushAsync(new TimesheetSign(mainViewModel));
        
      }
    
        }

    private async void TimesheetsList(object sender, EventArgs e)
    {
      await Navigation.PushAsync(new TimesheetListViewPage());
    }

    private void GetTimesheetsButton_OnClicked(object sender, EventArgs e)
    {
      GetTimesheetsButton.IsVisible = false;
    }
  }
  }

