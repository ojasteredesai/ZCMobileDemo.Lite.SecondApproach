using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ZCMobileDemo.Lite.Droid
{
    [Activity(Label = "ZCMobileDemo.Lite", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        public override void OnBackPressed()
        {
            if (App.MasterDetailVM == null || App.MasterDetailVM.PageCount < 2)
            {
                if (App.Current.MainPage.StyleId == "datacenter")
                {
                    Process.KillProcess(Android.OS.Process.MyPid());
                }
                else
                {
                    App.Current.MainPage = new MainPage();
                }
            }
            else
            {
                App.MasterDetailVM.PopAsync1();
            }
        }
    }
}

