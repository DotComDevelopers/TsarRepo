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
                    new DashboardPageMenuItem { Id = 0, IsVisible = true, Title = "My Timesheets",TargetType = typeof(TimesheetsPage)},
                    new DashboardPageMenuItem { Id = 1, IsVisible = true, Title = "New Timesheet",TargetType = typeof(AddNewTimesheetPage)},
                    new DashboardPageMenuItem { Id = 2, IsVisible = true, Title = "Timesheets List",TargetType = typeof(MyTimesheetsPage)},
                    new DashboardPageMenuItem { Id = 3, IsVisible = true, Title = "Clock In", TargetType = typeof(ScanPage)}, 
                    new DashboardPageMenuItem { Id = 4, IsVisible = true, Title = "Client Passwords", TargetType = typeof(ClientPasswordsPage)},
                    new DashboardPageMenuItem { Id = 5, IsVisible = true, Title = "Navigate To Client", TargetType = typeof(MapsPage)},                   
                    new DashboardPageMenuItem { Id = 6, IsVisible = true, Title = "Set Password", TargetType = typeof(SetPasswordPage)},
                    new DashboardPageMenuItem { Id = 8, IsVisible = true, Title = "Forums", TargetType = typeof(ForumListViewPage)},
                    new DashboardPageMenuItem { Id = 7, IsVisible = false, Title = "New client password", TargetType = typeof(NewClientPasswordPage)},
                     new DashboardPageMenuItem { Id = 8, IsVisible = true, Title = "Tsar Bot", TargetType = typeof(TsarBot)},
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