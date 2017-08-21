using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileTsar.Helpers;
using Plugin.Fingerprint;
using Xamarin.Forms;

namespace MobileTsar.Views
{
  public partial class RegisterPage : ContentPage
  {
    public RegisterPage()
    {
      InitializeComponent();
    }

    private async void LoginButton_OnClicked(object sender, EventArgs e)
    {
      
      //await DisplayAlert("Success", "Successfully Registered", "Close");
    
        await Navigation.PushAsync(new LoginPage());
      
      
    }

    private async void FingerButton_OnClicked(object sender, EventArgs e)
    {
      var result = await CrossFingerprint.Current.IsAvailableAsync(true);
      if (result)
      {
        var auth = await CrossFingerprint.Current.AuthenticateAsync("Authenticate to proceed to login");
        if (auth.Authenticated)
        {
          await Navigation.PushAsync(new LoginPage());
        }
        else
        {
          await DisplayAlert("Authentication Failed", "Try again later", "Close");
        }
      }
    }
  }
}
