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
        public string address;
        public List<Location> _locations { get; set; }

        public MapsPage()
        {
            InitializeComponent();
            try
            {
                getLocationlist();
                GetClientName();
            }
            catch(Exception r)
            { }
          

        }

        protected override async void OnAppearing()
        {
           
            try
            {
                //var geoCoder = new Geocoder();
                //var map = new Map(MapSpan.FromCenterAndRadius(
                //    new Position(-29.848212, 30.9224216),
                //    Distance.FromKilometers(20)))
                //{
                //    IsShowingUser = true,
                //    VerticalOptions = LayoutOptions.FillAndExpand,
                //    IsVisible = true
                //};

                //var approximateLocations = await geoCoder.GetPositionsForAddressAsync("102 Chardale Crescent");
                //foreach (var position in approximateLocations)
                //{
                //    map.Pins.Add(new Pin
                //    {
                //        Type = PinType.Place,
                //       Position = new Position(position.Latitude ,position.Longitude),
                //       Label = "My location"
                        
                //    });
                //}
                //map.Pins.Add(new Pin
                    //{
                    //    Type = PinType.Place,
                    //    Position = new Position(-29.848212, 30.9224216),
                    //    Label = "hello"

                    //});


                    //List<Pin> annotation = new List<Pin>();
                    //for (int i=0;i<=_locations.Count;i++)
                    //{
                    //    double lat;
                    //    double longitude;
                    //    int conid;
                    //    DateTime checktime;
                    //    lat = _locations[i].Latitude;
                    //    longitude = _locations[i].Longitude;
                    //    conid = _locations[i].ConsultantNum;
                    //    checktime = _locations[i].CheckinTime;
                    //    var pin= new Pin
                    //    {
                    //        Type = PinType.Place,
                    //        Position = new Position(lat, longitude),
                    //        Label = "This consultant was here" + conid + " on " + checktime
                    //    };
                    //    annotation.Add(pin);
                    //    map.Pins.Add(annotation[i]);
                    //}

                    //foreach (var loc in _locations)
                    //{
                    //    map.Pins.Add(new Pin
                    //    {
                    //        Type = PinType.Place,
                    //        Position = new Position(loc.Latitude, loc.Longitude),
                    //        Label = "This consultant was here" + loc.ConsultantNum + " on " + loc.CheckinTime

                    //    });
                    //}

                    //Content = map;
            }
            catch(Exception r)
            { }
            base.OnAppearing();
        }


        public async void getLocationlist()
        {
            try
            {
                var token = Settings.AccessToken;
                _locations = await _apiServices.GetLocationAsync(token);
            }
            catch (Exception e)
            {
                
            }


        }

        private async void BtnNavigate_OnClicked(object sender, EventArgs e)
        {
            var map = new Map(MapSpan.FromCenterAndRadius(
                    new Position(-29.848212, 30.9224216),
                    Distance.FromKilometers(20)))
            {
                IsShowingUser = true,
                VerticalOptions = LayoutOptions.FillAndExpand,
                IsVisible = true
            };
            var geoCoder = new Geocoder();
            var approximateLocations = await geoCoder.GetPositionsForAddressAsync(address);
            foreach (var position in approximateLocations)
            {
                map.Pins.Add(new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(position.Latitude, position.Longitude),
                    Label ="Client: "+ClientPicker.SelectedItem.ToString()+"At Address: "+address

                });
            }
            Content = map;
        }

        private void ClientPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GetAddress();
        }

        private void AddressPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
          
            address = AddressPicker.SelectedItem.ToString();
            
        }


        public async void GetConsultantId()
        {
            var api = new ApiServices();
            var token = Settings.AccessToken;
            var list2 = await api.GetUsernameAsync(token);
            var currentuser = list2.Email;
            var list = await api.GetConsultantAsync(token);
            //var ret = list.Select(t => t.FirstName).ToList();
            var username = list.Where(t => t.ConsultantUserName == currentuser).Select(t => t.ConsultantNum).FirstOrDefault();
            var con = username.ToString();
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
            //var ret = list.Select(t => t.ClientAddress).ToList();
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
    }
}