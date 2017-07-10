using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileTsar.Helpers;
using MobileTsar.Services;
using Xamarin.Forms;

namespace MobileTsar.ViewModels
{
  public class LoginViewModel
  {
    private ApiServices _apiServices = new ApiServices();

    public string Username { get; set; }
    public string Password { get; set; }

    public ICommand LoginCommand
    {
      get
      {
        return new Command(async () =>

        {
         var accesstoken = await _apiServices.LoginAsync(Username, Password);
          Settings.AccessToken = accesstoken;
        });
      }
    }

    public LoginViewModel()
    {
      Username = Settings.Username;
      Password = Settings.Password;
    }
  }
}
