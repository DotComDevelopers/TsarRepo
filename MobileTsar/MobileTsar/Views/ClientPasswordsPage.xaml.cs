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
  public partial class ClientPasswordsPage : ContentPage
  {
    public ClientPasswordsPage()
    {
      InitializeComponent();
      GetClientPasswords();
      ClientPicker.Items.Add("Please Select a Client");
      ClientPicker.SelectedIndex = 0;
      GetClientName();
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
      if (ClientPicker.SelectedIndex==0)
      {
        PasswordsListView.ItemsSource = clientpasswordslist;
      }
      var client = clientpasswordslist.Where(t => t.id == ClientPicker.SelectedIndex).Select(t => t.Client).ToList();
      PasswordsListView.ItemsSource = client;
      
    }

  }
}