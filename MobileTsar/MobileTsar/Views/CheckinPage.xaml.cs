using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileTsar.Helpers;
using MobileTsar.Models;
using MobileTsar.Services;
using MobileTsar.ViewModels;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileTsar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckinPage : ContentPage
    {


        public int Consultantid;

        public CheckinPage()
        {
            InitializeComponent();
            //GetConsultantId();
        }


        private async void Checkin(object sender, EventArgs e)
        {
            var locationview = BindingContext as LocationViewModel;
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync(TimeSpan.FromMilliseconds(10000));
            lblLat.Text = position.Latitude.ToString();
            lblLong.Text = position.Longitude.ToString();

            //if (locationview != null)
            //{
            //    locationview.Latitude = position.Latitude;
            //    locationview.Longitude = position.Longitude;
            //    locationview.ConsultantNum = Consultantid;
            //}


        }




        //public async void GetConsultantId()
        //{
        //    var api = new ApiServices();
        //    var token = Settings.AccessToken;
        //    var list2 = await api.GetUsernameAsync(token);
        //    var currentuser = list2.Email;
        //    var list = await api.GetConsultantAsync(token);
        //    //var ret = list.Select(t => t.FirstName).ToList();
        //    var username =
        //        list.Where(t => t.ConsultantUserName == currentuser).Select(t => t.ConsultantNum).FirstOrDefault();
        //    Consultantid= username;
        //}
    }
}
