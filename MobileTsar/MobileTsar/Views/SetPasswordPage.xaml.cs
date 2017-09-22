using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileTsar.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileTsar.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SetPasswordPage : ContentPage
  {
    public SetPasswordPage()
    {
      InitializeComponent();
      PasswordSet();
    }

    private void PasswordSet()
    {
      //new user or no password
      if (string.IsNullOrEmpty(Settings.VaultPassword))
      {
        DisplayAlert("Hi", "Please enter details", "ok");
        PasswordEntry.IsVisible = true;
        ValidatePasswordEntry.IsVisible = true;

        EditStackLayout.IsVisible = false;
        NewPasswordStackLayout.IsVisible = true;
        ValidteStackLayout.IsVisible = false;
      }
      else      
      //has password
      if (Settings.VaultPassword!=null|| Settings.VaultPassword!="")
      {        
        DisplayAlert("Hi", "Please enter Your password", "ok");        
        PasswordEntry.IsVisible = true;
        ValidatePasswordEntry.IsVisible = true;


        EditStackLayout.IsVisible = false;
        NewPasswordStackLayout.IsVisible = false;
        ValidteStackLayout.IsVisible = true;
      }
    }




    private void ValidatePasswordEntry_OnTextChanged(object sender, TextChangedEventArgs e)
    {
      if (PasswordEntry.Text == null)
      {
        ErrorLabel.Text = "Please Enter a password";
        ErrorLabel.IsVisible = true;
      }

      if (ValidatePasswordEntry.Text == null)
      {
        ErrorLabel.Text = "";
        ErrorLabel.Text = "Please enter your password again";        
        ErrorLabel.IsVisible = true;
      }
      if (PasswordEntry.Text != ValidatePasswordEntry.Text)
      {
        ErrorLabel.IsVisible = true;
        ErrorLabel.Text = "";
        ErrorLabel.Text = "Please enter the same password";     
      }
      if (PasswordEntry.Text != null && ValidatePasswordEntry.Text != null && PasswordEntry.Text == ValidatePasswordEntry.Text)
      {
        ErrorLabel.IsVisible = false;
        SaveButton.IsVisible = true;
      }

    }

    private void SaveButton_OnClicked(object sender, EventArgs e)
    {
      Settings.VaultPassword = ValidatePasswordEntry.Text;      
      DisplayAlert("Success", "Password Saved", "Ok");
      Application.Current.MainPage = new DashboardPage();
    }

    private void PasswordEntry_OnTextChanged(object sender, TextChangedEventArgs e)
    {
      if (PasswordEntry.Text == null)
      {
        ErrorLabel.Text = "Please Enter a password";
        ErrorLabel.IsVisible = true;
      }
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
      EditStackLayout.IsVisible = false;
      NewPasswordStackLayout.IsVisible = true;
    }

    private void CheckpasswordButton_OnClicked(object sender, EventArgs e)
    {
      if (CheckPasswordEntry.Text==Settings.VaultPassword)
      {
        DisplayAlert("Success", "Password Correct", "Ok");
        CurrentPasswordLabel.Text =$"Your Current Password is: {Settings.VaultPassword}";
        EditStackLayout.IsVisible = true;
        NewPasswordStackLayout.IsVisible = false;
        ValidteStackLayout.IsVisible = false;
      }
      else
      {
        DisplayAlert("Incorrect", "Try Again", "Ok");
      }
    }

    private void ClearButton_OnClicked(object sender, EventArgs e)
    {
      DisplayAlert("Success", "Password Cleared", "Ok");
      Settings.VaultPassword = null;
    }
  }
}