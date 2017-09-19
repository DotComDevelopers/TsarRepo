using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileTsar.Helpers;
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
      var scanpage = new ZXingScannerPage();
      var api = new ApiServices();
      var token = Settings.AccessToken;
      var list2 = await api.GetUsernameAsync(token);
      var currentuser = list2.Email;
      var list = await api.GetConsultantAsync(token);
      var consultantNum = list.Where(t => t.ConsultantUserName == currentuser).Select(t => t.ConsultantNum).FirstOrDefault();
      scanpage.OnScanResult += (result) =>
      {
        //Stop scanning
        scanpage.IsScanning = false;

        //Pop the Page andshow the result
        Device.BeginInvokeOnMainThread(() =>
        {
          Navigation.PopAsync();
          var vm = new AddNewConsultantRegisterViewModel();
          var today = System.DateTime.Now.GetDateTimeFormats()[0];
          var today2 = System.DateTime.Now.GetDateTimeFormats();

          //need to add a get call to check if already scanned barcode for the day 
          var date = result.ToString();
          if (date==today)
          {
            vm.DateTime=DateTime.Now;
            vm.BarcodeScanned = true;
            vm.ConsultantNum = consultantNum;
            vm.AddCommand.Execute(vm);

            var s = result.ToString();
            DisplayAlert("Thank You", result.Text, "OK");
          }
          else
          {
            DisplayAlert("Already marked the register", "Try again tomorrow ", "OK");
          }
          
         
        });
      };
      //Navigate to scanner page
      await Navigation.PushAsync(scanpage);
    }

    private async void EncodeButton_OnClicked(object sender, EventArgs e)
    {
      ZXingBarcodeImageView barcode;
      barcode = new ZXingBarcodeImageView
      {
        HorizontalOptions = LayoutOptions.FillAndExpand,
        VerticalOptions = LayoutOptions.FillAndExpand,
      };
      barcode.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE;
      barcode.BarcodeOptions.Width = 300;
      barcode.BarcodeOptions.Height = 300;
      barcode.BarcodeOptions.Margin = 10;
      var value =barcode.BarcodeValue = $"{System.DateTime.Now}";
     

      Content = barcode;

      await DisplayAlert("Scanned", $"{value}", "OK");
    }
  }
}