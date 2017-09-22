using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MobileTsar.Helpers;
using MobileTsar.Models;
using MobileTsar.Services;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace MobileTsar.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ClientPasswordsPage : ContentPage
  {
    public ClientPasswordsPage()
    {
      InitializeComponent();
      VerifyUser();
      GetClientPasswords();
      ClientPicker.Items.Add("All Clients");
      ClientPicker.SelectedIndex = 0;   
      GetClientName();
    }

    private async void VerifyUser()
    {
      var result = await CrossFingerprint.Current.IsAvailableAsync(true);
      if (result)
      {
        
        var auth = await CrossFingerprint.Current.AuthenticateAsync("Prove you have fingers", CancellationToken.None);        
        
        if (auth.Authenticated)
        {
          ValdatePasswordButton.IsVisible = false;
          PasswordEntry.IsVisible = false;
          FingerprintButton.IsVisible = false;
          StackLayout2.IsVisible = true;
        }
        if (auth.Status==FingerprintAuthenticationResultStatus.Failed)
        {
          await DisplayAlert("Authentication Failed", "Try again ", "Close");
        }
        if (auth.Authenticated==false)
        {
          await DisplayAlert("Authentication Failed", "Try again ", "Close");
        }
        if (auth.Status == FingerprintAuthenticationResultStatus.FallbackRequested)
        {
          //ask for password
          PasswordEntry.IsVisible = true;
          FingerprintButton.IsVisible = true;
          PasswordEntry.Focus();
          if (ValdatePasswordButton.Text != null)
          {
            ValdatePasswordButton.IsVisible = true;
          }
        }
        else if (auth.Status==FingerprintAuthenticationResultStatus.Canceled)
        {
          //await DisplayAlert("Authentication Failed", "Try again later", "Close");
        }
     
      }
      else
      {
        //ask for password
        PasswordEntry.IsVisible = true;
        PasswordEntry.Focus();
        if (ValdatePasswordButton.Text!=null)
        {
          ValdatePasswordButton.IsVisible = true;
        }

      }
    }

    private async void GetClientPasswords()
    {
      
      var api = new ApiServices();
      var token = Settings.AccessToken;
      var clientpasswordslist = await api.GetClientPasswordAsync(token);
      PasswordsListView.ItemsSource = clientpasswordslist;
    }

    public async void GetClientName()
    {
      var api = new ApiServices();
      var token = Settings.AccessToken;
      var list = await api.GetClientAsync(token);
      var ret = list.Select(t => t.ClientName).ToList();

      foreach (var ts in ret)
      {
        ClientPicker.Items.Add(ts);
      }
    }


    private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private async void ClientPicker_OnSelectedIndexChanged(object sender, EventArgs e)
    {
      var addlist = new List<string>();
      addlist.Clear();
      var api = new ApiServices();
      var token = Settings.AccessToken;
      var clientpasswordslist = await api.GetClientPasswordAsync(token);
      if (ClientPicker.SelectedIndex == 0)
      {
        PasswordsListView.ItemsSource = clientpasswordslist;
      }
      else
      {
        var newclientpasswordslist = await api.GetClientPasswordAsync(token);
        var client = newclientpasswordslist.Where
          (t => t.id == ClientPicker.SelectedIndex).Select(t => t.id).FirstOrDefault();

        var cv = newclientpasswordslist.Where(t => t.id == client);
        PasswordsListView.ItemsSource = cv;
      }
  

    }    
    private void NewPassToolbarItem_OnClicked(object sender, EventArgs e)
    {
      Navigation.PushAsync(new NewClientPasswordPage());
    }

    private void PasswordsListView_OnRefreshing(object sender, EventArgs e)
    {
      ClientPicker.Items.Clear();  
      GetClientPasswords();
      GetClientName();
      ClientPicker.Items.Add("All Clients");
      ClientPicker.SelectedIndex = 0;
      PasswordsListView.EndRefresh();
    }

    private void ValdatePasswordButton_OnClicked(object sender, EventArgs e)
    {
      if (PasswordEntry.Text==Settings.VaultPassword)
      {
        StackLayout2.IsVisible = true;
        ValdatePasswordButton.IsVisible = false;
        PasswordEntry.IsVisible = false;
        FingerprintButton.IsVisible = false;
      }
      else
      {
        DisplayAlert("Error", "Incorrect Password", "Ok");
      }
    }

    private void PasswordEntry_OnTextChanged(object sender, TextChangedEventArgs e)
    {
      if (PasswordEntry.Text!=null)
      {
        ValdatePasswordButton.IsVisible = true;
      }
      else
      {
        ValdatePasswordButton.IsVisible = false;
      }
      
    }

    private void FingerprintButton_OnClicked(object sender, EventArgs e)
    {
      VerifyUser();
    }
  }
}