using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MobileTsar.Helpers;
using MobileTsar.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileTsar.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ForumListViewPage : ContentPage
  {
    public ObservableCollection<string> Items { get; set; }

    public ForumListViewPage()
    {
      InitializeComponent();
      GetPosts();
      Items = new ObservableCollection<string>
            {
                "Item 1",
                "Item 2",
                "Item 3",
                "Item 4",
                "Item 5"
            };

      BindingContext = this;
    }

    async void Handle_ItemTapped(object sender, SelectedItemChangedEventArgs e)
    {
      if (e.SelectedItem == null)
        return;

      await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

      //Deselect Item
      ((ListView)sender).SelectedItem = null;
    }

    private void GetPostsButton_OnClicked(object sender, EventArgs e)
    {
      DisplayAlert("Hi", "Please wait", "ok");
      GetPosts();
    }

    public async void GetPosts()
    {
      var api = new ApiServices();
      var token = Settings.AccessToken;

      var addlist = new List<string>();
      addlist.Clear();

      var list = await api.GetPostsAsync(token);
      //var ret = list.Select(t => t.Title).ToList();
      //foreach (var ts in ret)
      //{
      //  addlist.Add(ts.ToString());
      //}
      TimesheetListView.ItemsSource = list;
    }
    private void NewPostToolbarItem_OnClicked(object sender, EventArgs e)
    {
      Navigation.PushAsync(new NewPostPage());
    }
  }
}