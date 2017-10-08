using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MobileTsar.Helpers;
using MobileTsar.Models;
using MobileTsar.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileTsar.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ForumListViewPage : ContentPage
  {
    

    public ForumListViewPage()
    {
      InitializeComponent(); 
      GetPosts();
   
    }

    async void Handle_ItemTapped(object sender, SelectedItemChangedEventArgs e)
    {
      if (e.SelectedItem == null)
        return;

      await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

      //Deselect Item
      ((ListView)sender).SelectedItem = null;
    }

    public async void GetPosts(string searchtext = null)
    {
      var api = new ApiServices();
      var token = Settings.AccessToken;

      var addlist = new List<string>();
      addlist.Clear();

      var list = await api.GetPostsAsync(token);


      if (string.IsNullOrWhiteSpace(searchtext))
      {
        TimesheetListView.ItemsSource = list;
      }
      else
      {
        TimesheetListView.ItemsSource = list.Where(c => c.Title.ToLower().Contains(searchtext.ToLower())||c.Body.ToLower().Contains(searchtext.ToLower()) || c.ShortDescription.ToLower().Contains(searchtext.ToLower()));
      }

    }
    private void NewPostToolbarItem_OnClicked(object sender, EventArgs e)
    {
      Navigation.PushAsync(new NewPostPage());
    }

    private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
    {
      GetPosts(e.NewTextValue);
    }
  }
}