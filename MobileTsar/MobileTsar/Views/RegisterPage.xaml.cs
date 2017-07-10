using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MobileTsar.Views
{
  public partial class RegisterPage : ContentPage
  {
    public RegisterPage()
    {
      InitializeComponent();
    }

    private async void Button_OnClicked(object sender, EventArgs e)
    {
      await Navigation.PushAsync(new LoginPage());
    }
  }
}
