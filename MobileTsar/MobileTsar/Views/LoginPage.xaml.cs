using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MobileTsar.Views
{
  public partial class LoginPage : ContentPage
  {
    public LoginPage()
    {
      InitializeComponent();
    }

    private async void Button_OnClicked(object sender, EventArgs e)
    {
      await Navigation.PushModalAsync(new TimesheetsPage());
    }
  }
}
