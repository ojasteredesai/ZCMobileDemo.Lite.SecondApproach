using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZCMobileDemo.Lite.Model;

namespace ZCMobileDemo.Lite.Views.Timesheet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page4 : ContentPage
    {
        #region Constructors
        public Page4()
        {
            InitializeComponent();
        }
        #endregion

        #region Private Methods
        private void Button_Clicked(object sender, EventArgs e)
        {
            App.MasterDetailVM.IsExecuting = true;
            var navigationData = new ZCMobileNavigationData
            {
                CurrentPage = this,
                CurrentPageTitle = App.MasterDetailVM.Header1,
                NextPage = new Page5(),
                NextPageTitle = App.PageTitels["page5"]
            };

            App.MasterDetailVM.PushAsync(navigationData);
            App.MasterDetailVM.IsExecuting = false;
        }
        #endregion
    }
}
