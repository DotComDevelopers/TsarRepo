using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileTsar.CustomMapsModels;
using MobileTsar.Helpers;
using MobileTsar.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace MobileTsar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomMapPage : ContentPage
    {
        ApiServices _apiServices = new ApiServices();
        
        public CustomMapPage()
        {
            InitializeComponent();
            var customMap = new CustomMap
            {
                IsShowingUser = true,
                MapType = MapType.Hybrid,
                IsVisible = true

            };
            //var position1 = new Position(36.8961, 10.1865);
            //var position2 = new Position(36.891, 10.181);
            //var position3 = new Position(36.892, 10.182);
            //var position4 = new Position(36.893, 10.183);
            //var position5 = new Position(36.891, 10.185);
            //var position6 = new Position(36.892, 10.187);

            //var customPin1 = new CustomPin
            //{
            //    Pin = new Pin
            //    {
            //        Type = PinType.Place,
            //        Position = position1,
            //        Label = "IntilaQ",
            //        Address = "Technopark Elgazala, Tunisia"
            //    },
            //    Url = "www.intilaq.tn",
            //};


            //var customPin2 = new CustomPin
            //{
            //    Pin = new Pin
            //    {
            //        Type = PinType.SearchResult,
            //        Position = position2,
            //        Label = "Telnet R&D",
            //        Address = "Technopark Elgazala, Tunisia"
            //    },
            //    Url = "www.groupe-telnet.com"
            //};

            //var customPin3 = new CustomPin
            //{
            //    Pin = new Pin
            //    {
            //        Type = PinType.SearchResult,
            //        Position = position3,
            //        Label = "Kromberg&Schubert",
            //        Address = "Technopark Elgazala, Tunisia"
            //    },
            //    Url = "www.kromberg-schubert.com"
            //};

            //var customPin4 = new CustomPin
            //{
            //    Pin = new Pin
            //    {
            //        Type = PinType.SearchResult,
            //        Position = position4,
            //        Label = "Via Mobile",
            //        Address = "Technopark Elgazala, Tunisia"
            //    },
            //    Url = "www.kromberg-schubert.com"
            //};

            //var customPin5 = new CustomPin
            //{
            //    Pin = new Pin
            //    {
            //        Type = PinType.SearchResult,
            //        Position = position5,
            //        Label = "Via Mobile",
            //        Address = "Technopark Elgazala, Tunisia"
            //    },
            //    Url = "www.kromberg-schubert.com"
            //};

            //var customPin6 = new CustomPin
            //{
            //    Pin = new Pin
            //    {
            //        Type = PinType.SearchResult,
            //        Position = position6,
            //        Label = "Via Mobile",
            //        Address = "Technopark Elgazala, Tunisia"
            //    },
            //    Url = "www.kromberg-schubert.com"
            //};

            customMap.CustomPins = new List<CustomPin>
            {
                    //customPin1,
                //    customPin2,
                //    customPin3,
                //    customPin4,
                //    customPin5,
                //    customPin6,
            };



            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Position(-29.848212, 30.9224216), Distance.FromKilometers(20)));
            //customMap.MoveToRegion(MapSpan.FromCenterAndRadius(
            //           new Position(36.8961, 10.1865), Distance.FromMiles(0.5)));
            Content = customMap;
        }


        //protected override async void OnAppearing()
        //{

        //    try
        //    {
        //        var customMap = new CustomMap
        //        {
        //            IsShowingUser = true,
        //            MapType = MapType.Hybrid,
        //            IsVisible = true

        //        };

        //       var list = await  _apiServices.GetLocationAsync(Settings.AccessToken);
                
        //        List<CustomPin> annotation = new List<CustomPin>();
        //        for (int i = 0; i <= list.Count; i++)
        //        {

        //            double latitude;
        //            double longitude;

        //            latitude = list[i].Latitude;
        //            longitude = list[i].Longitude;

        //            var pin = new CustomPin
        //            {
        //                Pin = new Pin
        //                {
        //                    Type = PinType.Place,
        //                    Position = new Position(latitude, longitude),
        //                    Label = "Destination",
        //                    Address = ""
        //                },
        //                Id = "hello",
        //                Url = "http://xamarin.com/about/"
        //            };

        //            annotation.Add(pin);

        //            customMap.Pins.Add(pin.Pin);
        //        }
        //        customMap.MoveToRegion(MapSpan.FromCenterAndRadius(
        //                new Position(-29.848212, 30.9224216), Distance.FromKilometers(20)));
        //            Content = customMap;


                
        //    }
        //    catch
        //        (Exception r)
        //    {
        //    }

        //    base.OnAppearing();
        //    }
        }
    }
