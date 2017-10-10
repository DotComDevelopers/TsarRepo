using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileTsar.Helpers;
using MobileTsar.Models;
using MobileTsar.Services;
using MobileTsar.ViewModels;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;
//using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace MobileTsar.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ScanPage : ContentPage
  {
    public ScanPage()
    {
      InitializeComponent();
      CheckLocation();

    }

    private async void CheckLocation()
    {
      var locator = CrossGeolocator.Current;
      //locator.DesiredAccuracy = 500;
      var position = await locator.GetPositionAsync(TimeSpan.FromMilliseconds(10000));
      try
      {
        var addresses = await locator.GetAddressesForPositionAsync(position);
        var address = addresses.FirstOrDefault();

        if (address == null)
        {
          //Console.WriteLine("No address found for position.");
          await DisplayAlert("Alert", $"No address found for position.", "OK");
        }
        else
        {
          ReverseGeocodedOutputLabel.Text = $"Addresss:{address.SubThoroughfare} {address.Thoroughfare} {address.Locality}";
          if (ReverseGeocodedOutputLabel.Text == "Addresss:41 Richefond Circle Umhlanga")
          {
            await DisplayAlert("Alert", $"You're at the office", "OK");
          }
          else
          if (ReverseGeocodedOutputLabel.Text == "Addresss:11 Nollsworth Crescent Umhlanga")
          {
            await DisplayAlert("Alert", $"You're at the  old office", "OK");
          }
          else
          if (ReverseGeocodedOutputLabel.Text == "Addresss:69 Aurora Road Bluff")
          {
            await DisplayAlert("Alert", $"You're at Home", "OK");
          }
          else
          if (ReverseGeocodedOutputLabel.Text == "Addresss:7 Ritson Road Berea")
          {
            await DisplayAlert("Alert", $"You're at campus", "OK");
          }
          if (ReverseGeocodedOutputLabel.Text == "Addresss:6 Winterton Walk Berea")
          {
            await DisplayAlert("Alert", $"You're at campus", "OK");
          }
          else
          {
            await DisplayAlert($"Your Location",
              $"Current Location:{address.SubThoroughfare} {address.Thoroughfare} {address.Locality} {address.CountryName}",
              "OK");
          }
        }
      }
      catch (Exception ex)
      {
        await DisplayAlert("Alert", $"Unable to get address: {ex}", "OK");
        //Debug.WriteLine("Unable to get address: " + ex);
      }
    }

    public async void GetConsultantId()
    {
      var api = new ApiServices();
      var token = Settings.AccessToken;
      var list2 = await api.GetUsernameAsync(token);
      var currentuser = list2.Email;
      var list = await api.GetConsultantAsync(token);     
      var username = list.Where(t => t.ConsultantUserName == currentuser).Select(t => t.ConsultantNum).FirstOrDefault();
      //ConsultantIdLabel.Text = username.ToString();
    }

   

    private async void ScanButton_OnClicked(object sender, EventArgs e)
    {
      ActivityIndicator.IsRunning = true;
      ActivityIndicator.IsVisible = true;
      var scanpage = new ZXingScannerPage();
      var api = new ApiServices();
      var token = Settings.AccessToken;
      var list2 = await api.GetUsernameAsync(token);
      var currentuser = list2.Email;
      var list = await api.GetConsultantAsync(token);
      var consultantNum = list.Where(t => t.ConsultantUserName == currentuser).Select(t => t.ConsultantNum).FirstOrDefault();
      var register = await api.GetConsultantRegisterAsync(Settings.AccessToken);
      var consultantsRegisters = register.Where(t => t.ConsultantNum == consultantNum).ToList();
      var condates = consultantsRegisters.Select(t => t.DateTime.Date);
 
  
        var datestocheck = new List<string>();

        foreach (var c in condates)
        {
          var d = c.Day + "-" + c.Month + "-" + c.Year;
          datestocheck.Add(d);
        }
        ActivityIndicator.IsRunning = false;
        ActivityIndicator.IsVisible = false;
        var scanned = true;

        var todayday = DateTime.Now.Day.ToString();
        var todaymonth = DateTime.Now.Month;
        var todayyear = DateTime.Now.Year.ToString();
        var todayjointoday = todayday + "-" + todaymonth + "-" + todayyear;
        var valid = datestocheck.Contains(todayjointoday);
        if (valid == false)
        {
          scanned = false;
        }
        else
        {
          scanned = true;
        }

      if (scanned==true)
      {
        await DisplayAlert("Oops", $"You already clocked in for today", "OK");
        Application.Current.MainPage = new DashboardPage();
      }
      else
    if  (ReverseGeocodedOutputLabel.Text != "Addresss:6 Winterton Walk Berea")   
      {
        await DisplayAlert("Sorry", "You need to be at the office", "Ok");
        Application.Current.MainPage = new DashboardPage();
      }
      else      
      // check if user is at office/campus
      if (ReverseGeocodedOutputLabel.Text == "Addresss:41 Richefond Circle Umhlanga" || ReverseGeocodedOutputLabel.Text == "Addresss:7 Ritson Road Berea"||ReverseGeocodedOutputLabel.Text== "Addresss:6 Winterton Walk Berea" && scanned == false)
      {
        scanpage.OnScanResult += (result) =>
        {
          //Stop scanning
          scanpage.IsScanning = false;

          //Pop the Page andshow the result
          Device.BeginInvokeOnMainThread(() =>
          {
            var day = DateTime.Now.Day.ToString();
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year.ToString();
            var jointoday = day + "-" + month + "-" + year;
            Navigation.PopAsync();
            var vm = new AddNewConsultantRegisterViewModel();
            var scanneddate = result.ToString();
            if (scanneddate == jointoday && scanned == false)
            {
              vm.DateTime = DateTime.Now;
              vm.BarcodeScanned = true;
              vm.ConsultantNum = consultantNum;
              vm.AddCommand.Execute(vm);


              DisplayAlert("Thank You", "Successfully clocked in", "OK");
              Application.Current.MainPage = new DashboardPage();
            }
            else
            if (scanned)
            {
              DisplayAlert("Oops", $"You already scanned this barcode", "OK");
            }
            else
            {
              DisplayAlert("Invalid Barcode", $"The barcode you scanned was {result.Text}", "OK");
            }

          });
        };
        //Navigate to scanner page
        await Navigation.PushAsync(scanpage);
      }

    }

    //below code for encoding a new barcode

    //private async void EncodeButton_OnClicked(object sender, EventArgs e)
    //{
    //  ZXingBarcodeImageView barcode;
    //  barcode = new ZXingBarcodeImageView
    //  {
    //    HorizontalOptions = LayoutOptions.FillAndExpand,
    //    VerticalOptions = LayoutOptions.FillAndExpand,
    //  };
    //  barcode.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE;
    //  barcode.BarcodeOptions.Width = 300;
    //  barcode.BarcodeOptions.Height = 300;
    //  barcode.BarcodeOptions.Margin = 10;
    //  var value =barcode.BarcodeValue = $"{System.DateTime.Now}";


    //  Content = barcode;

    //  await DisplayAlert("Scanned", $"{value}", "OK");
    //}

    private async void LocationButton_OnClicked(object sender, EventArgs e)
    {
   
      var locator = CrossGeolocator.Current;
      //locator.DesiredAccuracy = 500;
      var position = await locator.GetPositionAsync(TimeSpan.FromMilliseconds(10000));      
      try
      {
        var addresses = await locator.GetAddressesForPositionAsync(position);
        var address = addresses.FirstOrDefault();

        if (address == null)
        {
          //Console.WriteLine("No address found for position.");
          await DisplayAlert("Alert", $"No address found for position.", "OK");
        }
        else
        {
          ReverseGeocodedOutputLabel.Text =$"Addresss:{address.SubThoroughfare} {address.Thoroughfare} {address.Locality}";
          if (ReverseGeocodedOutputLabel.Text == "Addresss:41 Richefond Circle Umhlanga")
          {
            await DisplayAlert("Alert", $"You're at the office", "OK");
          }
          if (ReverseGeocodedOutputLabel.Text == "Addresss:11 Nollsworth Crescent Umhlanga")
          {
            await DisplayAlert("Alert", $"You're at the  old office", "OK");
          }
          if (ReverseGeocodedOutputLabel.Text == "Addresss:69 Aurora Road Bluff")
          {
            await DisplayAlert("Alert", $"You're at Home", "OK");
          }
          else
          {
            await DisplayAlert($"Your Location",
              $"Current Location:{address.SubThoroughfare} {address.Thoroughfare} {address.Locality} {address.CountryName}",
              "OK");
          }
        }
      }
      catch (Exception ex)
      {
        await DisplayAlert("Alert", $"Unable to get address: {ex}" , "OK");
        //Debug.WriteLine("Unable to get address: " + ex);
      }
    }
    

    
  }
}