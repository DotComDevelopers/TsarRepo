using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    private async void ScanButton_OnClicked(object sender, EventArgs e)
    {
      var scanpage = new ZXingScannerPage();
      scanpage.OnScanResult += (result) =>
      {
        //Stop scanning
        scanpage.IsScanning = false;

        //Pop the Page andshow the result
        Device.BeginInvokeOnMainThread(() =>
        {
          Navigation.PopAsync();
          DisplayAlert("Barcode Scanned", result.Text, "OK");
        });
      };
      //Navigate to scanner page
      await Navigation.PushAsync(scanpage);
    }
  }
}