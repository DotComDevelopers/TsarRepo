using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MobileTsar.Helpers;
using MobileTsar.Views;
using Xamarin.Forms;

namespace MobileTsar
{
  public partial class App : Application
  {
    public App()
    {
      InitializeComponent();
      SetMainPage();
      //MainPage = new NavigationPage(new RegisterPage());
    }

    protected  void SetMainPage()
    {
      if (!string.IsNullOrEmpty(Settings.AccessToken))
      {
        MainPage = new DashboardPage();
      }
      else if (!string.IsNullOrEmpty(Settings.Username) && !string.IsNullOrEmpty(Settings.Password))
      {
        MainPage = new NavigationPage(new LoginPage());
      }
      else
      {
        MainPage = new NavigationPage(new RegisterPage());
      }

    }

    protected override void OnStart()
    {
      // Handle when your app starts
    }

    protected override void OnSleep()
    {
      // Handle when your app sleeps
    }

    protected override void OnResume()
    {
      // Handle when your app resumes
    }
  }
}
