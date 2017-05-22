using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZCMobileDemo.Lite.Views.Module
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        public Dashboard()
        {
            InitializeComponent();
            App.IsUSerLoggedIn = true;
            App.MasterDetailVM.HamburgerVisibility = App.IsUSerLoggedIn;
            App.UserSession.SideContentVisibility = (!App.MasterDetailVM.Isportrait);
        }
    }
}