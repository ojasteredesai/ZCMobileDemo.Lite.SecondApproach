using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZCMobileDemo.Lite.ViewModels;

namespace ZCMobileDemo.Lite.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailControl 
    {
        #region Private Members
        private bool secondDetailPageVisible = false;
        #endregion

        #region Public Bindable Properties

        public static readonly BindableProperty SideContentProperty = BindableProperty.Create("SideContent",
            typeof(Xamarin.Forms.View), typeof(MasterDetailControl), null, propertyChanged: (bindable, value, newValue) =>
            {
                var control = (MasterDetailControl)bindable;
                control.SideContentContainer.Children.Clear();
                control.SideContentContainer.Children.Add(control.SideContent);
            });


        public BindableProperty DetailProperty = BindableProperty.Create("Detail",
            typeof(ContentPage), typeof(MasterDetailControl),
            propertyChanged: (bindable, value, newValue) =>
            {
                var masterPage = (MasterDetailControl)bindable;
                masterPage.DetailContainer.Content = newValue != null ?
                    ((ContentPage)newValue).Content : null;
                if (masterPage.DetailContainer.Content != null)
                {
                    masterPage.DetailContainer.Content.BindingContext = newValue != null ? (newValue as Page).BindingContext : null;
                }

                masterPage.OnPropertyChanged("SideContentVisible");

            });


        public BindableProperty DetailProperty1 = BindableProperty.Create("Detail1",
            typeof(ContentPage), typeof(MasterDetailControl),
            propertyChanged: (bindable, value, newValue) =>
            {
                var masterPage = (MasterDetailControl)bindable;
                masterPage.DetailContainer1.Content = newValue != null ?
                    ((ContentPage)newValue).Content : null;
                if (masterPage.DetailContainer1.Content != null)
                {
                    masterPage.DetailContainer1.Content.BindingContext = newValue != null ? (newValue as Page).BindingContext : null;
                }

                masterPage.OnPropertyChanged("SideContentVisible");

            });

        public View SideContent
        {
            get { return (Xamarin.Forms.View)GetValue(SideContentProperty); }
            set { SetValue(SideContentProperty, value); }
        }

        public bool SideContentVisible
        {
            get
            {
                return App.UserSession.SideContentVisibility;
            }
        }

        public bool SecondDetailPageVisible
        {
            get
            {
                return secondDetailPageVisible;
            }
            set
            {
                secondDetailPageVisible = value;
            }
        }
        #endregion

        #region Constructors
        public MasterDetailControl()
        {
            InitializeComponent();
            SetBinding(DetailProperty, new Binding("Detail", BindingMode.TwoWay));
            SetBinding(DetailProperty1, new Binding("Detail1", BindingMode.TwoWay));
        }
        #endregion

        #region Public Methods
        public static Page Create<TView, TViewModel>() where TView : MasterDetailControl, new()
            where TViewModel : MasterDetailControlViewModel, new()
        {
            return Create<TView, TViewModel>(new TViewModel());
        }

        public static Page Create<TView, TViewModel>(TViewModel viewModel) where TView : MasterDetailControl, new()
            where TViewModel : MasterDetailControlViewModel
        {
            try
            {
                //This condition sets the visibility of the side bar as per device orientation. For portrait mode side content is not shown.
                App.UserSession.SideContentVisibility = (!viewModel.Isportrait);
                var masterDetail = new TView();
              //  var navigationPage = new NavigationPage(masterDetail);
                var navigationPage = masterDetail;
                viewModel.SetNavigation(navigationPage.Navigation);
                viewModel.Header = "Dashboard";
                viewModel.RightButton = string.Empty;
                masterDetail.BindingContext = viewModel;
                App.MasterDetailVM = viewModel;
                return navigationPage;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return null;

            }

        }
        #endregion

        #region Private Methods
        void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            App.MasterDetailVM.IsExecuting = true;
            App.UserSession.SideContentVisibility = (!App.UserSession.SideContentVisibility);
            OnPropertyChanged("SideContentVisible");
            App.MasterDetailVM.IsExecuting = false;
        }

        void TapGestureRecognizerBack_Tapped(object sender, EventArgs e)
        {
            App.MasterDetailVM.IsExecuting = true;
            App.MasterDetailVM.PopAsync1();
            App.MasterDetailVM.IsExecuting = false;
        }
        #endregion
    }
}
