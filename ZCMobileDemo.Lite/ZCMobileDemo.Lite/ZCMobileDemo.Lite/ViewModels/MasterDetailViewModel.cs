using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using ZCMobileDemo.Lite.Controls;
using ZCMobileDemo.Lite.Model;
using ZCMobileDemo.Lite.Views;
using ZCMobileDemo.Lite.Views.Timesheet;

namespace ZCMobileDemo.Lite.ViewModels
{
    public class MasterDetailViewModel : MasterDetailControlViewModel
    {
        #region Private Members
        private ObservableCollection<AccordionSource> checking;
        #endregion
        #region Constructors
        public MasterDetailViewModel()
        {

        }
        #endregion

        #region Public Properties
        public ObservableCollection<AccordionSource> Checking
        {
            get { return checking ?? (checking = new ObservableCollection<AccordionSource>()); }
            set { checking = value; RaisePropertyChanged(); }
        }
        #endregion

        #region Public Methods
        public List<AccordionSource> GetSampleData()
        {
            var vResult = new List<AccordionSource>();
            var accordianObject = PreparedObject();
            foreach (var item in accordianObject)
            {

                Grid gd = new Grid();
                // gd.BackgroundColor = Color.FromHex("#01446b");
                if (item.ChildItemList.Count > 0)
                {
                    foreach (var child in item.ChildItemList)
                    {
                        gd.RowDefinitions.Add(new RowDefinition { Height = 25 });
                        gd.RowSpacing = 1;
                        gd.ColumnSpacing = 1; 
                        gd.Margin = new Thickness(2, 0, 0, 0);
                    }
                    if (item.ChildItemList.Any(q => q.BubbleCount > 0))
                    {
                        gd.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                        Device.OnPlatform(iOS: () =>
                        {
                            gd.ColumnDefinitions.Add(new ColumnDefinition { Width = 25 });
                        }, Android: () =>
                        {
                            gd.ColumnDefinitions.Add(new ColumnDefinition { Width = 30 });
                        });


                    }
                    int rowCount = 0;
                    foreach (var actual in item.ChildItemList)
                    {
                        Label lbl = new Label();
                        lbl.Text = actual.TextValue;
                        //lbl.HeightRequest = 30;
                        lbl.StyleId = actual.DataValue;
                        lbl.Margin = new Thickness(2, 0, 0, 0);
                        lbl.TextColor = Color.FromHex("#c1eaf6");
                        TapGestureRecognizer tg = new TapGestureRecognizer();
                        tg.Tapped += (ea, sa) =>
                        {
                            var label = ea as Label;
                            if (label.Text != "Logout")
                            {
                                RemoveAllPages();
                                //Header = "Page 1";
                                //RightButton = "...";
                                //var page = new Page1();
                                var navigationData = new ZCMobileNavigationData
                                {
                                    CurrentPage = null,
                                    CurrentPageTitle = string.Empty,
                                    NextPage = new Page1(),
                                    NextPageTitle = App.PageTitels["page1"]
                                };

                                PushAsync(navigationData);
                            }
                            else
                            {
                                App.Current.MainPage = new LoginPage();
                            }

                            if (App.MasterDetailVM.Isportrait)
                            {
                                App.UserSession.SideContentVisibility = (!App.UserSession.SideContentVisibility);
                                RaisePropertyChanged("SideContentVisible");
                            }
                        };

                        lbl.GestureRecognizers.Add(tg);
                        Grid.SetRow(lbl, rowCount);
                        if (actual.BubbleCount > 0)
                        {
                            BoxView bx = new BoxView();
                            bx.HeightRequest = 5;
                            bx.WidthRequest = 5;

                            bx.BackgroundColor = Color.White;
                            Grid.SetColumn(bx, 1);
                            Grid.SetRow(bx, rowCount);

                            gd.Children.Add(bx);

                            Label bubblecount = new Label();
                            //bubblecount.HeightRequest = 10;
                            //bubblecount.WidthRequest = 10;
                            bubblecount.Text = actual.BubbleCount.ToString();
                            bubblecount.VerticalTextAlignment = TextAlignment.Center;
                            bubblecount.HorizontalOptions = LayoutOptions.Center;
                            bubblecount.VerticalOptions = LayoutOptions.Center;

                            Grid.SetColumn(bubblecount, 1);
                            Grid.SetRow(bubblecount, rowCount);
                            gd.Children.Add(bubblecount);
                        }
                        gd.Children.Add(lbl);
                        rowCount++;
                    }

                    vResult.Add(new AccordionSource
                    {
                        HeaderText = item.HeaderText,
                        HeaderTextColor = Color.FromHex("#c1eaf6"),
                        HeaderBackGroundColor = Color.FromHex("#3c9ece"),
                        ContentItems = gd
                    });
                }
                else
                {
                    vResult.Add(new AccordionSource
                    {
                        HeaderText = item.HeaderText,
                        HeaderTextColor = Color.FromHex("#c1eaf6"),
                        HeaderBackGroundColor = Color.FromHex("#3c9ece"),
                        ContentItems = gd,                       
                    });
                }
            }

            return vResult;
        }
        #endregion

        #region Private Methods
        private List<SimpleObject> PreparedObject()
        {
            var dummyData = new List<SimpleObject>();
            SimpleObject obj = new SimpleObject();
            obj.HeaderText = "Submissions";
            obj.ChildItemList.Add(new ChildItems { TextValue = "Manage Submissions", DataValue = "MS1" });
            dummyData.Add(obj);

            obj = new SimpleObject();
            obj.HeaderText = "Timesheet";
            obj.ChildItemList.Add(new ChildItems { TextValue = "View Timesheet", DataValue = "T1" });
            obj.ChildItemList.Add(new ChildItems { TextValue = "Create Timesheet", DataValue = "T2", BubbleCount = 5 });
            dummyData.Add(obj);

            obj = new SimpleObject();
            obj.HeaderText = "Expense";
            obj.ChildItemList.Add(new ChildItems { TextValue = "View Expense Report", DataValue = "E1" });
            obj.ChildItemList.Add(new ChildItems { TextValue = "Create Expense Report", DataValue = "E2" });
            obj.ChildItemList.Add(new ChildItems { TextValue = "Approve Expense Report", DataValue = "E3", BubbleCount = 2 });
            dummyData.Add(obj);

            obj = new SimpleObject();
            obj.HeaderText = "Engagement";
            obj.ChildItemList.Add(new ChildItems { TextValue = "View Engagement", DataValue = "Eg1" });

            dummyData.Add(obj);

            obj = new SimpleObject();
            obj.HeaderText = "Payment";
            obj.ChildItemList.Add(new ChildItems { TextValue = "View Payment History", DataValue = "P1" });
            dummyData.Add(obj);

            obj = new SimpleObject();
            obj.HeaderText = "Dossier";
            obj.ChildItemList.Add(new ChildItems { TextValue = "Information", DataValue = "D1" });
            obj.ChildItemList.Add(new ChildItems { TextValue = "Security", DataValue = "D2" });
            obj.ChildItemList.Add(new ChildItems { TextValue = "Contact Us", DataValue = "D3" });
            dummyData.Add(obj);

            obj = new SimpleObject();
            obj.HeaderText = "Dashboard";
            obj.ChildItemList.Add(new ChildItems { TextValue = "Dashboard", DataValue = "D1" });
            dummyData.Add(obj);

            obj = new SimpleObject();
            obj.HeaderText = "Logout";
            obj.ChildItemList.Add(new ChildItems { TextValue = "Logout", DataValue = "D1" });
            dummyData.Add(obj);

            return dummyData;
        }
        #endregion
    }

}