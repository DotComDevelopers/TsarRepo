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
  public class PostsViewModel
  {
    ApiServices _apiServices = new ApiServices();

    private Post _selectedPost = new Post();

    private List<Post> _posts;
    //public string AccessToken { get; set; }

    public Post SelectedTimesheet
    {
      get { return _selectedPost; }
      set
      {
        _selectedPost = value;
        OnPropertyChanged();
      }

    }
    public List<Post> Posts
    {
      get { return _posts; }
      set
      {
        _posts = value;
        OnPropertyChanged();
      }
    }

    public ICommand GetPostsCommand
    {
      get
      {
        return new Command(async () =>
        {
          var accesstoken = Settings.AccessToken;
          Posts = await _apiServices.GetPostsAsync(accesstoken);
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
