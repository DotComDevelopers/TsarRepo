using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileTsar.Helpers;
using MobileTsar.Models;
using MobileTsar.Services;
using MobileTsar.ViewModels;
using Xamarin.Forms;
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
 

      var datestocheck=new List<string>();

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
     // var today = DateTime.Now.GetDateTimeFormats()[5];
     // var formts = DateTime.Now.GetDateTimeFormats();
      var valid = datestocheck.Contains(todayjointoday);
      if (valid==false)
      {
        scanned=false;
      }
     if (valid)
      {
        scanned = true;
      }
  
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
            vm.DateTime=DateTime.Now;
            vm.BarcodeScanned = true;
            vm.ConsultantNum = consultantNum;
            vm.AddCommand.Execute(vm);

            //var s = result.ToString();
            DisplayAlert("Thank You", "Successfully clocked in", "OK");
         
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

  }
}