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
      var logvm = new LoginViewModel();
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
      ProgressBar.IsVisible = true;
      LoadingLabel.IsVisible = true;
      while (Settings.AccessToken=="")
      {
       await ProgressBar.ProgressTo(0.8, 2500, Easing.BounceOut);
      }
      await ProgressBar.ProgressTo(1, 1, Easing.CubicInOut);
      GetUsername();

      await DisplayAlert("Login Success", $"Welcome back {UserNameLabel.Text}", "OK");    
      await Navigation.PushModalAsync(new DashboardPage());

    }

    public double SetComplete()
    {
      return Settings.AccessToken=="" ? 0.8 : 1;
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
