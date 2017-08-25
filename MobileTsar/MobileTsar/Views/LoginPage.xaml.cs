using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileTsar.Helpers;
using MobileTsar.Services;
using MobileTsar.ViewModels;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Xamarin.Forms;

namespace MobileTsar.Views
{
  public partial class LoginPage : ContentPage
  {
    public LoginPage()
    {
      InitializeComponent();
      //DisplayAlert($"{Settings.Username}", $"{Settings.AccessToken}", $"{Settings.Password}");
    }

    private async void GetUsername()
    {
      var api = new ApiServices();
      var token = Settings.AccessToken;

      var list = await api.GetUsernameAsync(token);
      var ret = list.Email;
      UserNameLabel.Text = ret;
    }
    private async void GetTimesheetsButton_OnClicked(object sender, EventArgs e)
    {
     
      if (Settings.AccessToken!="")
      {
        GetUsername();
        await DisplayAlert("Login Success", $"Welcome back {UserNameLabel.Text}", "OK");

        await Navigation.PushModalAsync(new TimesheetsPage());
      }
      else
      {
       
        await DisplayAlert("Connection Error", "Please try again", "Close");
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
