using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using MobileTsar.Helpers;
using MobileTsar.Views;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Xamarin.Forms;

namespace MobileTsar
{
  public partial class App : Application
  {
    public App()
    {
      InitializeComponent();
      IsConnectivitySupported();
      SetMainPage();
      
      //MainPage = new NavigationPage(new RegisterPage());
    }

    protected  void SetMainPage()
    {
      if (!string.IsNullOrEmpty(Settings.AccessToken) && IsConnectivitySupported()==false)
      {
        MainPage = new DashboardPage();
        Current.MainPage.DisplayAlert("Hi", "Welcome back", "Close");
      }

      else if (!string.IsNullOrEmpty(Settings.AccessToken) && CrossConnectivity.Current.IsConnected)
      {
        MainPage = new DashboardPage();
        Current.MainPage.DisplayAlert("Hi", "Welcome back", "Close");
      }
      else if (!string.IsNullOrEmpty(Settings.Username) && !string.IsNullOrEmpty(Settings.Password))
      {
        MainPage = new NavigationPage(new LoginPage());
      }
      else
      {
        MainPage = new NavigationPage(new RegisterPage());
      }          
      if (CrossConnectivity.IsSupported)
      {
        if (CrossConnectivity.Current.IsConnected == false)
        {
          MainPage = new NoInternetPage();
        }       
      }

    }

    public bool IsConnectivitySupported()
    {
      return CrossConnectivity.IsSupported;
    }

    protected override void OnStart()
    {
      //var check = CrossConnectivity.Current.IsConnected;
      // Handle when your app starts

      if (IsConnectivitySupported())
      {
        CheckInternet();
      }
    }

    protected override void OnSleep()
    {
      // Handle when your app sleeps
    }

    protected override void OnResume()
    {
      // Handle when your app resumes
      //CheckInternet();
    }

    public bool DoIHaveInternet()
    {
      if (!CrossConnectivity.IsSupported)
        return true;

      return CrossConnectivity.Current.IsConnected;


    }


    /// <summary>
    /// Event handler when connection changes
    /// </summary>
    event ConnectivityChangedEventHandler ConnectivityChanged;
    public class ConnectivityChangedEventArgs : EventArgs
    {
      public bool IsConnected { get; set; }
    }

    public delegate void ConnectivityChangedEventHandler(object sender, ConnectivityChangedEventArgs e);
    public void CheckInternet()
    {
      CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
      {
        
        if (args.IsConnected)
        {  
          SetMainPage();          
        }
        else
        {
          Current.MainPage = new NoInternetPage();
        }
      };
    }
  }
}
