using MobileTsar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileTsar.Helpers;
using MobileTsar.Models;
using MobileTsar.Services;
using Xamarin.Forms;

namespace MobileTsar.Views
{
  public partial class AddNewTimesheetPage : ContentPage
  {
    public AddNewTimesheetPage()
    {
      InitializeComponent();
      GetClientName();
      GetConsultantName();
      ClientPicker.Items.Add("Please Select a Client");
      ConsultantPicker.Items.Add("Please Select a Consultant");
      ConsultantPicker.SelectedIndex = 0;
      ClientPicker.SelectedIndex = 0;

    }

    //Uses web service to get tickets and populate drop down
    public async void GetAddress()
    {
      var addlist = new List<string>();
      addlist.Clear();

      var api = new ApiServices();
      var token = Settings.AccessToken;
      var list = await api.GetClientAsync(token);
      //var ret = list.Select(t => t.ClientAddress).ToList();
      var address1 = list.Where(t => t.Id == ClientPicker.SelectedIndex).Select(t=>t.ClientAddress).ToList();
      var address2 = list.Where(t => t.Id == ClientPicker.SelectedIndex).Select(t=>t.ClientAddress2).ToList();

      foreach (var ts in address1)        
      {
        addlist.Add(ts);
      }
      foreach (var ts in address2)
      {
        addlist.Add(ts);
      }
      AddressPicker.ItemsSource = addlist;
    }


    public async void GetConsultantName()
    {
      var api = new ApiServices();
      var token = Settings.AccessToken;

      var list = await api.GetConsultantAsync(token);
      var ret = list.Select(t => t.FirstName).ToList();
      foreach (var ts in ret)
      {
        ConsultantPicker.Items.Add(ts);
      }
    }

    // Uses web service to get cleint name and populate drop down
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

    private async void Button_OnClicked(object sender, EventArgs e)
    {
      await DisplayAlert("Done", "New Timesheet added!", "Close");
            await Navigation.PushAsync(new TimesheetsPage());
    }

    //private void ConsultantPicker_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //  ConsultantText.Text = ConsultantPicker.SelectedItem.ToString();
    //}
    private void ClientPicker_OnSelectedIndexChanged(object sender, EventArgs e)
    {
      GetAddress();
      ClientIdLabel.Text = ClientPicker.SelectedIndex.ToString();
    }

    private void AddressPicker_OnSelectedIndexChanged(object sender, EventArgs e)
    {
      AddressLabel.Text = AddressPicker.SelectedItem.ToString();
    }

    private void ConsultantPicker_OnSelectedIndexChanged(object sender, EventArgs e)
    {
    
      ConsultantIdLabel.Text = ConsultantPicker.SelectedIndex.ToString();
    }

    private async void ValidateBtn_OnClicked(object sender, EventArgs e)
    {
      var isvalid = true;
      if (ConsultantIdLabel.Text == "0")
      {
        isvalid = false;
         await DisplayAlert("Please select a consultant", "Consultant is Required", "OK");
        ConsultantPicker.Focus();
      }
      if (ClientIdLabel.Text == "0")
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
  }
}
