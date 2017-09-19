using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileTsar.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class NoInternetPage : ContentPage
  {
    public NoInternetPage()
    {
      InitializeComponent();
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
      var c = new App();
      c.CheckInternet();
    }
  }
}