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
    public partial class Page3 : ContentPage
    {
        #region Constructors
        public Page3()
        {
            InitializeComponent();
            var model = App.ApplicationDataViewModel as Page3ViewModel;
            this.BindingContext = (model != null ? model : (new Page3ViewModel()));
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
                NextPage = new Page4(),
                NextPageTitle = App.PageTitels["page4"]
            };

            App.MasterDetailVM.PushAsync(navigationData);
            App.MasterDetailVM.IsExecuting = false;
        }
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            App.MasterDetailVM.IsExecuting = true;
            var bindingContext = this.BindingContext as Page3ViewModel;
            App.ApplicationDataViewModel = new Page2ViewModel { Messsge1 = bindingContext.Messsge1, Messsge2 = bindingContext.Messsge2 };
            App.MasterDetailVM.PushAsyncPreviousPage(new Page2());
            App.MasterDetailVM.IsExecuting = false;
        }
        #endregion
    }
}
