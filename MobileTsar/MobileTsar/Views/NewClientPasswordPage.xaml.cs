using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileTsar.Helpers;
using MobileTsar.Models;
using MobileTsar.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileTsar.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class NewClientPasswordPage : ContentPage
  {
    public NewClientPasswordPage()
    {
      InitializeComponent();
      GetClientName();
      ClientPicker.Items.Add("Please Select a Client");
      ClientPicker.SelectedIndex = 0;  
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

    private async void ClientPicker_OnSelectedIndexChanged(object sender, EventArgs e)
    {
      GetClientName();

      var s = sender.ToString();
      
    
      var api = new ApiServices();
      var token = Settings.AccessToken;
      var xx = ClientPicker.SelectedIndex;
      var list = await api.GetClientAsync(token);
      if (xx !=0)
      {
        var ee = ClientPicker.SelectedItem as Client;
        var ded = ClientPicker.SelectedItem.ToString();
        var clientId = list.Where(t =>t.ClientName == ded).Select(t => t.Id).FirstOrDefault();
        if (ded != null) ClientEntry.Text = clientId.ToString();
      }
   

    }

    private async void ValidateBtn_OnClicked(object sender, EventArgs e)
    {
      var isvalid = true;
      if (OperatorEntry.Text == null)
      {
        isvalid = false;
        await DisplayAlert("Please enter operator", "Operator can't be blank", "OK");
        OperatorEntry.Focus();
      }
      if (OperatorPasswordEntry.Text == null)
      {
        isvalid = false;
        await DisplayAlert("Please enter operator password", "Operator Password can't be blank", "OK");
        OperatorPasswordEntry.Focus();
      }
      if (CompanyPasswordEntry.Text == null)
      {
        isvalid = false;
        await DisplayAlert("Please enter company password", "Company Password can't be blank", "OK");
        CompanyPasswordEntry.Focus();
      }
      if (ClientPicker.SelectedIndex == 0)
      {
        isvalid = false;    
        await DisplayAlert("Please select a client", "Client is Required", "OK");
        ClientPicker.Focus();
      }
      else
      {
        if (isvalid == true)
        {
          
          FinishBtn.IsVisible = true;
          ValidateBtn.IsVisible = false;
        }
      }

    }

    private async void Button_OnClicked(object sender, EventArgs e)
    {
      await DisplayAlert("Done", "New passwords added!", "Close");
      await Navigation.PopAsync();
    }
  }
}