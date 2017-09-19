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
    var c = new App();
      c.CheckInternet();
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

    private async void ValidateButton_OnClicked(object sender, EventArgs e)
    {
      var isvalid = "YES";
      if (TxtEmail.Text == null)
      {
        isvalid = "NO";
        await DisplayAlert("Please enter an email address", "Email is Required", "OK");
        RegisterBtn.IsVisible = false;
        TxtEmail.Focus();
      }
      if (TxtPassword.Text == null)
      {
        isvalid = "NO";
        await DisplayAlert("Please enter a password", "Password is Required", "OK");
        TxtPassword.Focus();
      }
        if (TxtConfPassword.Text != TxtPassword.Text)
        {
          isvalid = "NO";
          await DisplayAlert("Please enter the same password", "Password confirmation is Required", "OK");
          TxtConfPassword.Text = "";
          TxtEmail.Text = "";
          TxtEmail.Focus();
        }
    
     if (isvalid=="YES")
      {
        RegisterBtn.IsVisible = true;
        ValidateButton.IsVisible = false;
        await DisplayAlert("Success", "You may now register", "OK");        
      }

    }
  }
}
