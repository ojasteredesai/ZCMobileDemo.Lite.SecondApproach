﻿using System;
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
    public partial class Page1 : ContentPage
    {
        #region Constructors
        public Page1()
        {

            InitializeComponent();
            firstBoxView.WidthRequest = App.Current.MainPage.Width / 2;
            secondBoxView.WidthRequest = App.Current.MainPage.Width / 2;
            var model = App.ApplicationDataViewModel as Page1ViewModel;
            this.BindingContext = (model != null ? model : (new Page1ViewModel()));
        }
        #endregion

        #region Private Methods
        private async void Button_Clicked(object sender, EventArgs e)
        {
            App.MasterDetailVM.IsExecuting = true;
            await Task.Delay(1800);
            var bindingContext = this.BindingContext as Page1ViewModel;
            App.ApplicationDataViewModel = new Page2ViewModel { Messsge1 = bindingContext.Messsge1, Messsge2 = bindingContext.Messsge2 };

            var navigationData = new ZCMobileNavigationData
            {
                CurrentPage = this,
                CurrentPageTitle = App.MasterDetailVM.Header,
                NextPage = new Page2(),
                NextPageTitle = App.PageTitels["page2"]
            };

            App.MasterDetailVM.PushAsync(navigationData);
            App.MasterDetailVM.IsExecuting = false;
        }
        #endregion
    }
}
