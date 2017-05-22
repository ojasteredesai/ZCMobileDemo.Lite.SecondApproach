using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZCMobileDemo.Lite.Views;

namespace ZCMobileDemo.Lite
{
    public partial class MainPage : ContentPage
    {
        #region Constructors
        public MainPage()
        {
            InitializeComponent();
        }
        public MainPage(bool show)
        {
            InitializeComponent();
            backButton.IsVisible = show;
        }
        #endregion

        #region Private Methods
        private void Button_Clicked(object sender, EventArgs e)
        {
            App.UserSession.SelectedDataCenter = "US";
            App.Current.Properties[App.SelectedDataCenter] = App.UserSession.SelectedDataCenter;
           // App.Current.MainPage = new LoginTypePage();
            App.MasterDetailVM.PushAsync(new LoginTypePage());
        }
        #endregion

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
        }
    }
}
