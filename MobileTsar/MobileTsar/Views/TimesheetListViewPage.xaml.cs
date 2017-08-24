using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MobileTsar.Helpers;
using MobileTsar.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileTsar.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class TimesheetListViewPage : ContentPage
  {
    public ObservableCollection<string> Items { get; set; }

    public TimesheetListViewPage()
    {
      InitializeComponent();
      GetClientName();
      ClientPicker.Items.Add("Please select a client");
      BindingContext = this;
    }

    async void Handle_ItemTapped(object sender, SelectedItemChangedEventArgs e)
    {
      if (e.SelectedItem == null)
        return;

      await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

      //Deselect Item
      ((ListView)sender).SelectedItem = null;
    }

    private  void ClientPicker_OnSelectedIndexChanged(object sender, EventArgs e)
    {
      if (ClientPicker.SelectedIndex==0)
      {
        GetTimesheets();
      }
      else
      {
        GetTimesheetsForClient();
      }
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

    public async void GetTimesheetsForClient()
    {
      var addlist = new List<string>();
      addlist.Clear();

      var api = new ApiServices();
      var token = Settings.AccessToken;
      var list = await api.GetTimesheetAsync(token);
      //var ret = list.Select(t => t.ClientAddress).ToList();
      var timesheetResults = list.Where(t => t.Id == ClientPicker.SelectedIndex).Select(t => t.TimesheetId).ToList();
    

      foreach (var ts in timesheetResults)
      {
        addlist.Add(ts.ToString());
      }
  
      TimesheetListView.ItemsSource = addlist;
    }

    public async void GetTimesheets()
    {
      var api = new ApiServices();
      var token = Settings.AccessToken;

      var addlist = new List<string>();
      addlist.Clear();

      var list = await api.GetTimesheetAsync(token);
      var ret = list.Select(t => t.TimesheetId).ToList();
      foreach (var ts in ret)
      {
        addlist.Add(ts.ToString());
      }
      TimesheetListView.ItemsSource = addlist;
    }
  }
}