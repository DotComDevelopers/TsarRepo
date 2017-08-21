using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileTsar.Helpers;
using MobileTsar.Services;
using MobileTsar.ViewModels;
using Xamarin.Forms;

namespace MobileTsar.Views
{
  public partial class LoginPage : ContentPage
  {
    public LoginPage()
    {
      InitializeComponent();
    }

    private async void GetTimesheetsButton_OnClicked(object sender, EventArgs e)
    {      
      if (Settings.AccessToken!="")
      {
        await DisplayAlert("Login Success", "You are now logged in!", "OK");
        await Navigation.PushModalAsync(new TimesheetsPage());
      }
      else
      {        
        await DisplayAlert("Login Failed", "You are not logged in", "Close");
      }

    }

    private async void SignOutButton_OnClicked(object sender, EventArgs e)
    {
      Settings.Password = "";
      Settings.Username = "";
      Settings.AccessToken = "";

      await Navigation.PushModalAsync(new RegisterPage());
    }
  }
}
