using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileTsar.Annotations;
using MobileTsar.Helpers;
using MobileTsar.Models;
using MobileTsar.Services;
using Xamarin.Forms;

namespace MobileTsar.ViewModels
{
  public class TimesheetsViewModel : INotifyPropertyChanged
  {
     ApiServices _apiServices = new ApiServices();
    private List<Timesheet> _timesheets;
    //public string AccessToken { get; set; }

    public List<Timesheet> Timesheets
    {
      get { return _timesheets; }
      set
      {
        _timesheets = value; 
        OnPropertyChanged();
      }
    }

    public ICommand GetTimesheetsCommand
    {
      get
      {
        return  new Command(async () =>
        {
          var accesstoken = Settings.AccessToken;
          Timesheets = await _apiServices.GetTimesheetAsync(accesstoken);
        });
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
