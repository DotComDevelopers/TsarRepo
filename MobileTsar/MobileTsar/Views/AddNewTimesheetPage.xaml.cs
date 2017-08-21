using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MobileTsar.Views
{
  public partial class AddNewTimesheetPage : ContentPage
  {
    public AddNewTimesheetPage()
    {
      InitializeComponent();
    }

    private async void Button_OnClicked(object sender, EventArgs e)
    {
      await DisplayAlert("Done", "New Timesheet added!", "Close");
      await Navigation.PushAsync(new TimesheetsPage());
    }
  }
}
