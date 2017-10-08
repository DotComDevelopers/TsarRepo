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
   public class NewPostViewModel
  {
    ApiServices _apiServices = new ApiServices();


    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string Body { get; set; }
    public DateTime PostedOn { get; set; }
    public int ConsultantNum { get; set; }
    public string FirstName { get; set; }

    public ICommand AddCommand
    {
      get
      {
        return new Command(async () =>
        {

           var post = new Post()
          {

            Title = Title,
            Body = Body,
            ShortDescription = ShortDescription,
            PostedOn = DateTime.Now,
            FirstName = FirstName,
            ConsultantNum = ConsultantNum,
          };

          await _apiServices.PostPostAsync(post, Settings.AccessToken);

        });
      }
    }
  }
}
