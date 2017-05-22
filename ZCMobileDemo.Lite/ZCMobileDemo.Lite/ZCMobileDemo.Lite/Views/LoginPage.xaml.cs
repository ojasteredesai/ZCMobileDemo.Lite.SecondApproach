using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZCMobileDemo.Lite.ViewModels;
using ZCMobileDemo.Lite.Views.Module;

namespace ZCMobileDemo.Lite.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        #region Constructors
        public LoginPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Private Methods
        private void Button_Clicked(object sender, EventArgs e)
        {
            //  App.MasterDetailVM.IsExecuting = true;
            //App.Current.MainPage = MasterDetailControl.Create<MasterDetail, MasterDetailViewModel>();
            App.MasterDetailVM.PushAsync(new Dashboard());
            // App.MasterDetailVM.IsExecuting = false;
        }
        #endregion

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            //App.Current.MainPage = new MainPage(true);
            App.MasterDetailVM.PopAsyncInitialPages();
        }
    }
}
