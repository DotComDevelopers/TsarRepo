using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileTsar.Helpers;
using MobileTsar.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileTsar.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class NewPostPage : ContentPage
  {
    public NewPostPage()
    {
      InitializeComponent();
      GetUsername();
      GetConsultantId();
    }

    private async void GetConsultantId()
    {
      var api = new ApiServices();
      var token = Settings.AccessToken;
      var list2 = await api.GetUsernameAsync(token);
      var currentuser = list2.Email;
      var list = await api.GetConsultantAsync(token);
      //var ret = list.Select(t => t.FirstName).ToList();
      var username = list.Where(t => t.ConsultantUserName == currentuser).Select(t => t.ConsultantNum).FirstOrDefault();
      ConsultantIdLabel.Text = username.ToString();
      GetConsultantFullname();
    }
    public async void GetConsultantFullname()
    {
      var api = new ApiServices();
      var token = Settings.AccessToken;
      var list2 = await api.GetUsernameAsync(token);
      var currentuser = list2.Email;
      var list = await api.GetConsultantAsync(token);
      //var ret = list.Select(t => t.FirstName).ToList();
      var username = list.Where(t => t.ConsultantUserName == currentuser).Select(t => t.FirstName).FirstOrDefault();
      ConsultantFullNameLabel.Text = username;
    }

    private async void GetUsername()
    {
      var api = new ApiServices();
      var token = Settings.AccessToken;

      var list = await api.GetUsernameAsync(token);
      var ret = list.Email;
      UsernameLabel.Text = ret;
    }

    private async void FinishBtn_OnClicked(object sender, EventArgs e)
    {
      await DisplayAlert("Done", "New Timesheet added!", "Close");
      await Navigation.PopAsync();
    }

    private async void ValidateBtn_OnClicked(object sender, EventArgs e)
    {
      var isvalid = true;
      if (ConsultantIdLabel.Text == null)
      {
        isvalid = false;
        await DisplayAlert("Please try again", "Something went wrong", "OK");
      }
      if (TitleEntry.Text == "")
      {
        isvalid = false;
        await DisplayAlert("Please select a client", "Client is Required", "OK");
        TitleEntry.Focus();
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