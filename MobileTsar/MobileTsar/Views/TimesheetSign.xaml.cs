using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.XamForms.SignaturePad;
using Android.Content;
using MobileTsar.Models;
using MobileTsar.Services;
using MobileTsar.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Windows.Input;
using MobileTsar.Helpers;
using SignaturePad.Forms;

namespace MobileTsar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimesheetSign : ContentPage
    {
        
        
        
        

        public TimesheetsViewModel TimesheetsView { get; set; }
        
        public TimesheetSign()
        {

            InitializeComponent();
        }

        

        public TimesheetSign(TimesheetsViewModel mainViewModel)
        {
            
            InitializeComponent();

            TimesheetsView = mainViewModel;
           
            BindingContext = mainViewModel;

            


        }

        private async void Done(object sender, EventArgs e)
        {
           
            try
            {
                var signimage = padView.GetImage(Acr.XamForms.SignaturePad.ImageFormatType.Jpg);
                
                TimesheetsView.Signature = ((MemoryStream)signimage).ToArray();
               
            }
            catch (Exception r)
            {

            }

            await DisplayAlert("Done", "Signed Successfully", "Close");
           
        }


       

        private async void Timesheets(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TimesheetsPage());
        }

       

        }
    }

