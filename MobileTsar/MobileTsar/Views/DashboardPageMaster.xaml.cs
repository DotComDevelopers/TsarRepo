using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ZXing;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace MobileTsar.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class DashboardPageMaster : ContentPage
  {
    public ListView ListView;

    public DashboardPageMaster()
    {
      InitializeComponent();

      BindingContext = new DashboardPageMasterViewModel();
      ListView = MenuItemsListView;
      
    }

    class DashboardPageMasterViewModel : INotifyPropertyChanged
    {
      public ObservableCollection<DashboardPageMenuItem> MenuItems { get; set; }

      public DashboardPageMasterViewModel()
      {
        MenuItems = new ObservableCollection<DashboardPageMenuItem>(new[]
        {
                    new DashboardPageMenuItem { Id = 0, Title = "My Timesheets",TargetType = typeof(TimesheetsPage)},
                    new DashboardPageMenuItem { Id = 1, Title = "New Timesheet",TargetType = typeof(AddNewTimesheetPage)},
                    new DashboardPageMenuItem { Id = 2, Title = "Timesheets List",TargetType = typeof(MyTimesheetsPage)},
                    new DashboardPageMenuItem { Id = 3, Title = "Scanner", TargetType = typeof(ScanPage)}, 
                    new DashboardPageMenuItem { Id = 4, Title = "Client Passwords", TargetType = typeof(ClientPasswordsPage)},                     
                });
      }

      #region INotifyPropertyChanged Implementation
      public event PropertyChangedEventHandler PropertyChanged;
      void OnPropertyChanged([CallerMemberName] string propertyName = "")
      {
        if (PropertyChanged == null)
          return;

        PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
      #endregion
    }
  }
}