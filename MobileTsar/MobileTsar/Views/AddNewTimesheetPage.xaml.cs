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

    private void Button_OnClicked(object sender, EventArgs e)
    {
      DisplayAlert("Done", "New Timesheet added!", "Close");
      Navigation.PushAsync(new TimesheetsPage());
    }
  }
}
