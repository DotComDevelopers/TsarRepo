using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileTsar.Annotations;
using MobileTsar.Helpers;
using MobileTsar.Models;
using MobileTsar.Services;
using Plugin.Geolocator;
using Xamarin.Forms;

namespace MobileTsar.ViewModels
{
   public class LocationViewModel : INotifyPropertyChanged
    {
        ApiServices _apiServices = new ApiServices();
        private List<Location> _locations;
      

       
        public List<Location> Locations
        {
            get { return _locations; }
            set
            {
                _locations = value;
                OnPropertyChanged();
            }
        }
        public ICommand GetLocationCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var accesstoken = Settings.AccessToken;
                    Locations = await _apiServices.GetLocationAsync(accesstoken);
                });
            }
        }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Id { get; set; }


        public int ConsultantNum;

        public async void GetConsultantId()
        {
            var api = new ApiServices();
            var token = Settings.AccessToken;
            var list2 = await api.GetUsernameAsync(token);
            var currentuser = list2.Email;
            var list = await api.GetConsultantAsync(token);
            //var ret = list.Select(t => t.FirstName).ToList();
            var username =
                list.Where(t => t.ConsultantUserName == currentuser).Select(t => t.ConsultantNum).FirstOrDefault();
           ConsultantNum = username;
        }
        public ICommand AddLocationCommand
        {
            get
            {
                return new Command(async () =>
                {
                    GetConsultantId();
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 50;
                    var position = await locator.GetPositionAsync(TimeSpan.FromMilliseconds(10000));
                    Location location = new Location
                    {
                       
                        Latitude = position.Latitude,
                        Longitude = position.Longitude,
                        CheckinTime = DateTime.Now,
                        Id = null,
                        ConsultantNum = ConsultantNum,
                       
                    };
                    await _apiServices.PostLocationAsync(location, Settings.AccessToken);

                });
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
