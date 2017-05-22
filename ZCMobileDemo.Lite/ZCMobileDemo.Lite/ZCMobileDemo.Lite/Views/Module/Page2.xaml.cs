using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZCMobileDemo.Lite.Model;
using ZCMobileDemo.Lite.ViewModels;

namespace ZCMobileDemo.Lite.Views.Timesheet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page2 : ContentPage
    {
        #region Constructors
        public Page2()
        {
            InitializeComponent();
            var model = App.ApplicationDataViewModel as Page2ViewModel;
            this.BindingContext = (model != null ? model : (new Page2ViewModel()));
        }
        #endregion

        #region Private Methods
        private async void Button_Clicked(object sender, EventArgs e)
        {
            App.MasterDetailVM.IsExecuting = true;
            await Task.Delay(1800);
            var navigationData = new ZCMobileNavigationData
            {
                CurrentPage = this,
                CurrentPageTitle = App.MasterDetailVM.Header1,
                NextPage = new Page3(),
                NextPageTitle = App.PageTitels["page3"]
            };

            App.MasterDetailVM.PushAsync(navigationData);
            App.MasterDetailVM.IsExecuting = false;
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
          //  App.MasterDetailVM.IsExecuting = true;
            var bindingContext = this.BindingContext as Page2ViewModel;
            App.ApplicationDataViewModel = new Page1ViewModel { Messsge1 = bindingContext.Messsge1, Messsge2 = bindingContext.Messsge2 };
            App.MasterDetailVM.PushAsyncPreviousPage(new Page1());
          //  App.MasterDetailVM.IsExecuting = false;
        }
        #endregion
    }
}
