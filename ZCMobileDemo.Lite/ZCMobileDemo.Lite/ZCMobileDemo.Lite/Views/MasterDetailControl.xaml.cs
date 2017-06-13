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
        public static Page Create<TView, TViewModel>(bool userLoggedIn = true, Page page = null) where TView : MasterDetailControl, new()
            where TViewModel : MasterDetailControlViewModel, new()
        {
            return Create<TView, TViewModel>(new TViewModel(), userLoggedIn, page);
        }

        public static Page Create<TView, TViewModel>(TViewModel viewModel, bool userLoggedIn = true, Page page = null) where TView : MasterDetailControl, new()
            where TViewModel : MasterDetailControlViewModel
        {
            try
            {
                //This condition sets the visibility of the side bar as per device orientation. For portrait mode side content is not shown.
                //if (userLoggedIn)
                //{
                //    App.IsUSerLoggedIn = true;
                //    App.UserSession.SideContentVisibility = (!viewModel.Isportrait);
                //}
                //else
                //{
                //    App.UserSession.SideContentVisibility = false;
                //}
                var masterDetail = new TView();
                // var navigationPage = new NavigationPage(masterDetail);
                var navigationPage = masterDetail;
                viewModel.SetNavigation(navigationPage.Navigation);
                viewModel.Header = (userLoggedIn ? "Dashboard" : "Login Page");
                viewModel.RightButton = string.Empty;
                masterDetail.BindingContext = viewModel;
                App.MasterDetailVM = viewModel;
                if (userLoggedIn)
                {
                    App.MasterDetailVM.PushAsync(new Dashboard());
                }
                else
                {
                    App.UserSession.SideContentVisibility = false;
                    App.MasterDetailVM.PushAsync(page);
                }
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
            var MDV = this.BindingContext as MasterDetailViewModel;
            App.MasterDetailVM.IsExecuting = true;
            App.UserSession.SideContentVisibility = (!App.UserSession.SideContentVisibility);
            OnPropertyChanged("SideContentVisible");
            if (App.UserSession.SideContentVisibility)
            {
                App.MasterDetailVM.DetailContainerAndHeaderGridRowLeft = 262;
                App.MasterDetailVM.DetailContainerWidth = 375;
                App.MasterDetailVM.HeaderGridRowWidth = 375;

            }
            else
            {
                App.MasterDetailVM.DetailContainerAndHeaderGridRowLeft = 0;
                App.MasterDetailVM.DetailContainerWidth = 0;
                App.MasterDetailVM.HeaderGridRowWidth = 375;

            }
            //AdjustApplicationUI();
            App.MasterDetailVM.IsExecuting = false;

            //double totalDetailContainerWidth = 0;
            //double detailContainerTop = (Device.Idiom == TargetIdiom.Tablet) ? 64 : 44;


            //App.UserSession.SideContentVisibility = (!App.UserSession.SideContentVisibility);
            //if (App.UserSession.SideContentVisibility)
            //{

            //    if (Device.Idiom == TargetIdiom.Phone || (App.Current.MainPage.Height > App.Current.MainPage.Width))
            //    {
            //        App.MasterDetailVM.SideContentWidth = (Device.Idiom == TargetIdiom.Tablet) ? (App.Current.MainPage.Width * 0.5) : (App.Current.MainPage.Width * 0.3);
            //        totalDetailContainerWidth = (App.Current.MainPage.Width - App.MasterDetailVM.SideContentWidth);
            //        DetailContainer.LayoutTo(new Rectangle(App.MasterDetailVM.SideContentWidth, detailContainerTop, App.Current.MainPage.Width, App.Current.MainPage.Height), 50, null);
            //        headerGridRow.LayoutTo(new Rectangle(App.MasterDetailVM.SideContentWidth, 0, App.Current.MainPage.Width, App.Current.MainPage.Height), 50, null);
            //    }
            //    else
            //    {
            //        App.MasterDetailVM.SideContentWidth = App.Current.MainPage.Width * 0.7;
            //        totalDetailContainerWidth = (App.Current.MainPage.Width - App.MasterDetailVM.SideContentWidth);
            //        if (App.MasterDetailVM.PageCount > 1)
            //        {
            //            DetailContainer.LayoutTo(new Rectangle(App.MasterDetailVM.SideContentWidth, detailContainerTop, (totalDetailContainerWidth / 2), App.Current.MainPage.Height), 50, null);
            //            DetailContainer1.LayoutTo(new Rectangle(((totalDetailContainerWidth / 2) + App.MasterDetailVM.SideContentWidth), detailContainerTop, (totalDetailContainerWidth / 2), App.Current.MainPage.Height), 50, null);
            //        }
            //        else
            //        {
            //            DetailContainer.LayoutTo(new Rectangle(App.MasterDetailVM.SideContentWidth, detailContainerTop, totalDetailContainerWidth, App.Current.MainPage.Height), 50, null);
            //        }

            //        headerGridRow.LayoutTo(new Rectangle(App.MasterDetailVM.SideContentWidth, 0, totalDetailContainerWidth, App.Current.MainPage.Height), 50, null);
            //    }
            //    OnPropertyChanged("SideContentVisible");
            //}
            //else
            //{
            //    if (Device.Idiom == TargetIdiom.Phone || (App.Current.MainPage.Height > App.Current.MainPage.Width))
            //    {
            //        DetailContainer.LayoutTo(new Rectangle(0, detailContainerTop, App.Current.MainPage.Width, App.Current.MainPage.Height), 50, null);
            //    }
            //    else
            //    {
            //        if (App.MasterDetailVM.PageCount > 1)
            //        {
            //            DetailContainer.LayoutTo(new Rectangle(0, detailContainerTop, App.Current.MainPage.Width / 2, App.Current.MainPage.Height), 50, null);
            //            DetailContainer1.LayoutTo(new Rectangle(App.Current.MainPage.Width / 2, detailContainerTop, App.Current.MainPage.Width / 2, App.Current.MainPage.Height), 50, null);
            //        }
            //        else
            //        {
            //            DetailContainer.LayoutTo(new Rectangle(0, detailContainerTop, App.Current.MainPage.Width, App.Current.MainPage.Height), 50, null);
            //        }

            //    }
            //    headerGridRow.LayoutTo(new Rectangle(0, 0, App.Current.MainPage.Width, App.Current.MainPage.Height), 50, null);
            //    OnPropertyChanged("SideContentVisible");

            //}


        }
        void TapGestureRecognizerBack_Tapped(object sender, EventArgs e)
        {
            App.MasterDetailVM.IsExecuting = true;
            App.MasterDetailVM.PopAsync1();
            App.MasterDetailVM.IsExecuting = false;
        }
        //Handle Application UI to resolve Shrink Issue
        void AdjustApplicationUI()
        {
            if (App.UserSession.SideContentVisibility)
            {

                if (Device.Idiom == TargetIdiom.Phone || (App.Current.MainPage.Height > App.Current.MainPage.Width))
                {
                    App.MasterDetailVM.SideContentWidth = (Device.Idiom == TargetIdiom.Tablet) ? (App.Current.MainPage.Width * 0.5) : (App.Current.MainPage.Width * 0.3);
                    App.MasterDetailVM.TotalDetailContainerWidth = (App.Current.MainPage.Width - App.MasterDetailVM.SideContentWidth);
                    App.MasterDetailVM.DetailContainerAndHeaderGridRowLeft = App.MasterDetailVM.SideContentWidth;
                    App.MasterDetailVM.DetailContainerWidth = App.Current.MainPage.Width;
                    App.MasterDetailVM.HeaderGridRowWidth = App.Current.MainPage.Width;
                }
                else
                {
                    App.MasterDetailVM.SideContentWidth = App.Current.MainPage.Width * 0.7;
                    App.MasterDetailVM.TotalDetailContainerWidth = (App.Current.MainPage.Width - App.MasterDetailVM.SideContentWidth);
                    if (App.MasterDetailVM.PageCount > 1)
                    {
                        App.MasterDetailVM.DetailContainerAndHeaderGridRowLeft = App.MasterDetailVM.SideContentWidth;
                        App.MasterDetailVM.DetailContainerWidth = App.MasterDetailVM.TotalDetailContainerWidth / 2;
                        App.MasterDetailVM.DetailContainer1Left = ((App.MasterDetailVM.TotalDetailContainerWidth / 2) + App.MasterDetailVM.SideContentWidth);
                        App.MasterDetailVM.DetailContainer1Width = (App.MasterDetailVM.TotalDetailContainerWidth / 2);
                    }
                    else
                    {
                        App.MasterDetailVM.DetailContainerAndHeaderGridRowLeft = App.MasterDetailVM.SideContentWidth;
                        App.MasterDetailVM.DetailContainerWidth = App.MasterDetailVM.TotalDetailContainerWidth;

                    }
                    App.MasterDetailVM.HeaderGridRowWidth = App.MasterDetailVM.TotalDetailContainerWidth;
                }

            }
            else
            {
                if (Device.Idiom == TargetIdiom.Phone || (App.Current.MainPage.Height > App.Current.MainPage.Width))
                {
                    App.MasterDetailVM.DetailContainerAndHeaderGridRowLeft = 0;
                    App.MasterDetailVM.DetailContainerWidth = App.Current.MainPage.Width;
                }
                else
                {
                    if (App.MasterDetailVM.PageCount > 1)
                    {
                        App.MasterDetailVM.DetailContainerAndHeaderGridRowLeft = 0;
                        App.MasterDetailVM.DetailContainerWidth = App.Current.MainPage.Width / 2;
                        App.MasterDetailVM.DetailContainer1Left = App.Current.MainPage.Width / 2;
                        App.MasterDetailVM.DetailContainer1Width = App.Current.MainPage.Width / 2;
                    }
                    else
                    {
                        App.MasterDetailVM.DetailContainerAndHeaderGridRowLeft = 0;
                        App.MasterDetailVM.DetailContainerWidth = App.Current.MainPage.Width;
                    }

                }
                App.MasterDetailVM.HeaderGridRowWidth = App.Current.MainPage.Width;
            }
        }
        #endregion
    }
}
