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
            var styleIds = new string[] { "datacenter" , "loginpage", "dashboard" };

            if (App.IsUSerLoggedIn)
            {
                if (App.MasterDetailVM != null && App.MasterDetailVM.PageCount > 1)
                {
                    App.MasterDetailVM.PopAsync1();
                }
                else //if (Array.IndexOf(styleIds, App.MasterDetailVM.Detail.StyleId) > -1)
                {
                    if (App.UserSession.SideContentVisibility)
                    {
                        Process.KillProcess(Android.OS.Process.MyPid());
                    }
                    else
                    {
                        App.UserSession.SideContentVisibility = true;
                    }
                    //else if (App.Current.MainPage.StyleId == "logintypepage")
                    //{
                    //    App.Current.MainPage = new MainPage();
                    //}             
                }
            }
            else
            {
                if (App.MasterDetailVM.InitialPageCount > 1)
                {
                    App.MasterDetailVM.PopAsyncInitialPages();
                }
                else
                {
                    Process.KillProcess(Android.OS.Process.MyPid());
                }
            }
        }
    }
}

