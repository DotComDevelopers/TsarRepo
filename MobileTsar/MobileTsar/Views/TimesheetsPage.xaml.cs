using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Android.Telephony;
using MobileTsar.Helpers;
using Xamarin.Forms;
using MobileTsar.Models;
using MobileTsar.ViewModels;

namespace MobileTsar.Views
{
  public partial class TimesheetsPage : ContentPage
  {
      private int id;
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

        private async void SignPage(object sender, ItemTappedEventArgs e)
        {
           var  timesheet = TimesheetListView.SelectedItem as Timesheet;
            if (timesheet != null)
            {
                var mainViewModel = BindingContext as TimesheetsViewModel;
                if (mainViewModel != null)
                {
                    mainViewModel.SelectedTimesheet = timesheet;
                    await Navigation.PushAsync(new TimesheetSign(mainViewModel));
                }
            }
        }
    }
  }

