using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.CurrentActivity;
using Plugin.Fingerprint;


namespace MobileTsar.Droid
{
  [Activity(Label = "Tsar Mobile", Icon = "@drawable/TransTusks", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
  {
    protected override void OnCreate(Bundle bundle)
    {
      TabLayoutResource = Resource.Layout.Tabbar;
      ToolbarResource = Resource.Layout.Toolbar;
      CrossFingerprint.SetCurrentActivityResolver(() => CrossCurrentActivity.Current.Activity);
      base.OnCreate(bundle);
            Xamarin.FormsMaps.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
      LoadApplication(new App());
    }
  }
}

