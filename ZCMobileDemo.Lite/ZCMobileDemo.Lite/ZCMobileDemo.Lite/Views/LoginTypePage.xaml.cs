using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZCMobileDemo.Lite.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginTypePage : ContentPage
    {
        #region Constructor
        public LoginTypePage()
        {
            InitializeComponent();
        }
        #endregion

        #region Private Methods
        private void Button_Clicked(object sender, EventArgs e)
        {
            //App.Current.MainPage = new LoginPage();
            App.MasterDetailVM.PushAsync(new LoginPage());
        }
        #endregion
    }
}