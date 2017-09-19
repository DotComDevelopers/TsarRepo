using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileTsar.Helpers;
using MobileTsar.Models;
using MobileTsar.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace MobileTsar.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class MapsPage : ContentPage
  {
    ApiServices _apiServices = new ApiServices();

    public List<Location> _locations { get; set; }

    public MapsPage()
    {
      InitializeComponent();
      GetClientName();
    }

    private async void BtnNavigate_OnClicked(object sender, EventArgs e)
    {

      var map = new Map(MapSpan.FromCenterAndRadius(
              new Position(-29.848212, 30.9224216),
              Distance.FromKilometers(30)))
      {
        IsShowingUser = true,
        VerticalOptions = LayoutOptions.FillAndExpand,
        IsVisible = true
      };
      var geoCoder = new Geocoder();
      var approximateLocations = await geoCoder.GetPositionsForAddressAsync(AddressPicker.SelectedItem.ToString());
      foreach (var position in approximateLocations)
      {
        map.Pins.Add(new Pin
        {
          Type = PinType.Place,
          Position = new Position(position.Latitude, position.Longitude),
          Label = "Client: " + ClientPicker.SelectedItem + " At Address: " + AddressPicker.SelectedItem

        });

        map.MoveToRegion(MapSpan.FromCenterAndRadius(
        new Position(position.Latitude, position.Longitude), Distance.FromKilometers(20)));
      }
      Content = map;
      Title = "Navigate to " + ClientPicker.SelectedItem;

    }

    private void ClientPicker_OnSelectedIndexChanged(object sender, EventArgs e)
    {
      GetAddress();

      btnNavigate.Text = "Navigate to " + ClientPicker.SelectedItem;
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

    public async void GetAddress()
    {
      var addlist = new List<string>();
      addlist.Clear();

      var api = new ApiServices();
      var token = Settings.AccessToken;
      var list = await api.GetClientAsync(token);
      var address1 = list.Where(t => t.Id == ClientPicker.SelectedIndex).Select(t => t.ClientAddress).ToList();
      var address2 = list.Where(t => t.Id == ClientPicker.SelectedIndex).Select(t => t.ClientAddress2).ToList();

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

    private void AddressPicker_OnSelectedIndexChanged(object sender, EventArgs e)
    {
      btnNavigate.IsVisible = true;
    }
  }
}