using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileTsar.Helpers;
using MobileTsar.Models;
using MobileTsar.Services;
using Xamarin.Forms;

namespace MobileTsar.ViewModels
{
  class AddNewClientPasswordViewModel
  { 
  ApiServices _apiServices = new ApiServices();

  public int ClientPasswordId { get; set; }

  public virtual Client Client { get; set; }
  public int id { get; set; }
  public string Operator { get; set; }
  public string OperatorPassword { get; set; }

  public string CompanyPassword { get; set; }

  public ICommand AddCommand
  {
  get
  {
    return new Command(async () =>
    {
      
      var clientPassword = new ClientPassword
      {
        id = id,
        CompanyPassword = CompanyPassword,
        Operator = Operator,
        OperatorPassword = OperatorPassword,       
      };

      await _apiServices.PostClientPasswordsAsync(clientPassword, Settings.AccessToken);

    });
  }
  }
}
}
