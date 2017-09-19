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
   public class AddNewConsultantRegisterViewModel
  {
    ApiServices _apiServices = new ApiServices();

  //  public int ConsultantRegisterId { get; set; }

    public DateTime DateTime { get; set; }

    public bool BarcodeScanned { get; set; }

    //Foreign Keys
    public int ConsultantNum { get; set; }


    public ICommand AddCommand
    {
      get
      {
        return new Command(async () =>
        {
         
          var consultantRegister = new ConsultantRegister()
          {

           ConsultantNum = ConsultantNum,
           BarcodeScanned = true,
           DateTime = DateTime
          };

          await _apiServices.PostConsultantRegisterAsync(consultantRegister, Settings.AccessToken);

        });
      }
    }
  }
}
