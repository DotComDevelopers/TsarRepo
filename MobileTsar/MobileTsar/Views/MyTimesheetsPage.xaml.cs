using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MobileTsar.Helpers;
using MobileTsar.Models;
using MobileTsar.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileTsar.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class MyTimesheetsPage : ContentPage
  {
    public ObservableCollection<Timesheet> Items { get; set; }

    public MyTimesheetsPage()
    {
      InitializeComponent();
      GetTimesheets();
      Items = new ObservableCollection<Timesheet>();
      BindingContext = this;
    }
    public async void GetTimesheets()
    {
      var api = new ApiServices();
      var token = Settings.AccessToken;
      var list = await api.GetTimesheetAsync(token);

      var usernamelist2 = await api.GetUsernameAsync(token);
      var currentuser = usernamelist2.Email;
      var consultantlist = await api.GetConsultantAsync(token);
      //var ret = list.Select(t => t.FirstName).ToList();
      var username = consultantlist.Where(t => t.ConsultantUserName == currentuser).Select(t => t.ConsultantNum).FirstOrDefault();
      //ConsultantIdLabel.Text = username.ToString();

      //var ret = list.Select(t => t.TimesheetId).ToList();     
      foreach (var timesheet in list.ToList().Where(t=>t.SignOff==false && t.ConsultantNum==username))
       {
        Items.Add(timesheet);
      };

    }


    async void Handle_ItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
    {
      var timesheet = itemTappedEventArgs.Item as Timesheet;
      if (itemTappedEventArgs.Item == null)
        return;

      if (timesheet != null) await DisplayAlert($"Timesheet : {timesheet.TimesheetId}", $"{timesheet.ActivityDescription}", "OK");

      //Deselect Item
      ((ListView)sender).SelectedItem = null;
    }
  }
}