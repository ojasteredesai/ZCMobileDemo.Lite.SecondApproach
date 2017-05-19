using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZCMobileDemo.Lite.Interfaces;
using ZCMobileDemo.Lite.Model;
using ZCMobileDemo.Lite.Views;

namespace ZCMobileDemo.Lite.ViewModels
{
    /// <summary>
    /// MasterDetailControlViewModel class.
    /// </summary>
    public class MasterDetailControlViewModel : BaseViewModel, IZCMobileNavigation
    {
        #region Private Members
        private string header;
        private string header1;
        private string rightButton;
        private string rightButton1;
        private Page detail, detail1;
        private INavigation navigation;
        private Stack<Page> pages = new Stack<Page>();
        private bool secondContentVisibility = false;
        private bool backButtonVisibility = false;
        private int detailGridColSpan = 2;
        private int detailGridHeaderColSpan = 4;
        private const int BACK_BUTTON_PAGE_COUNT = 1;
        private const int SECOND_CONTENT_PAGE_COUNT = 1;
        private bool isExecuting = false;
        #endregion

        #region Public Properties
        #region Detail Container properties
        public Page Detail
        {
            get { return detail; }
            set
            {
                if (detail != value || pages.Count == 0)
                {
                    if (Detail != null && (pages.Any() && pages.Any(x => x.StyleId == value.StyleId)))
                    {
                        pages.Pop();
                    }
                    if (value != null)
                    {
                        pages.Push(value);
                    }
                    detail = value;
                    RaisePropertyChanged();
                }

                //This will maintain visibility of second detail page.
                GetSecondContentVisibility();
            }
        }

        public Page Detail1
        {
            get { return detail1; }
            set
            {
                if (detail1 != value)
                {
                    //  if (Detail1 != null && Detail1.StyleId != value.StyleId)
                    if (Detail1 != null && (pages.Any() && pages.Any(x => x.StyleId == value.StyleId)))
                    {
                        pages.Pop();
                    }
                    if (value != null)
                    {
                        pages.Push(value);
                        GetSecondContentVisibility();
                    }
                    detail1 = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region Right Button and Header Properties
        public string Header
        {
            get { return header; }
            set { header = value; RaisePropertyChanged(); }
        }

        public string Header1
        {
            get { return header1; }
            set { header1 = value; RaisePropertyChanged(); }
        }

        public string RightButton
        {
            get { return rightButton; }
            set { rightButton = value; RaisePropertyChanged(); }
        }

        public string RightButton1
        {
            get { return rightButton1; }
            set { rightButton1 = value; RaisePropertyChanged(); }
        }
        #endregion

        #region Navigation Properties
        public bool Isportrait
        {
            get
            {
                return (App.Current.MainPage.Height > App.Current.MainPage.Width);
            }
        }
        public IReadOnlyList<Page> ModalStack { get { return navigation.ModalStack; } }

        public IReadOnlyList<Page> NavigationStack
        {
            get
            {
                if (pages.Count == 0)
                {
                    return navigation.NavigationStack;
                }
                var implPages = navigation.NavigationStack;
                MasterDetailControl master = null;
                var beforeMaster = implPages.TakeWhile(d =>
                {
                    master = d as MasterDetailControl;
                    return master != null || d.GetType() == typeof(MasterDetailControl);
                }).ToList();
                beforeMaster.AddRange(pages);
                beforeMaster.AddRange(implPages.Where(d => !beforeMaster.Contains(d)
                    && d != master));
                return new ReadOnlyCollection<Page>(navigation.NavigationStack.ToList());
            }
        }

        /// <summary>
        /// Gets the page count of the stack.
        /// </summary>
        public int PageCount
        {
            get
            {
                return pages.Count;
            }
        }
        #endregion

        #region Visibility Control and Grid ColumnSpan Properties
        public bool SecondContentVisibility
        {
            get
            {
                return secondContentVisibility;
            }
            set
            {
                secondContentVisibility = value;
                RaisePropertyChanged();
            }
        }
        public bool BackButtonVisibility
        {
            get
            {
                return backButtonVisibility;
            }
            set
            {
                backButtonVisibility = value;
                RaisePropertyChanged();
            }
        }
        public int DetailGridColSpan
        {
            get
            {
                return detailGridColSpan;
            }
            set
            {
                detailGridColSpan = value;
                RaisePropertyChanged();
            }
        }
        public int DetailGridHeaderColSpan
        {
            get
            {
                return detailGridHeaderColSpan;
            }
            set
            {
                detailGridHeaderColSpan = value;
                RaisePropertyChanged();
            }
        }

        public bool IsExecuting
        {
            get
            {
                return isExecuting;
            }
            set
            {
                isExecuting = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsPageEnabled));
            }
        }
        public bool IsPageEnabled
        {
            get
            {
                return !IsExecuting;
            }
        }

        #endregion
        #endregion

        #region Push and Pop Methods
        public void PushAsync(ZCMobileNavigationData navigationData)
        {
            if (Isportrait || pages.Count == 0) // This is for potrait mode
            {
                Header = navigationData.NextPageTitle;
                PushAsync(navigationData.NextPage);
            }
            else//This is for landscape mode
            {
                Header = navigationData.CurrentPageTitle;
                Header1 = navigationData.NextPageTitle;
                PushAsync(navigationData.CurrentPage);
                PushAsync1(navigationData.NextPage);
            }
        }

        public Task PushAsync(Page page)
        {
            Detail = page;
            return Task.FromResult(page);
        }
        public Task PushAsync1(Page page)
        {
            Detail1 = page;
            return Task.FromResult(page);
        }

        public Task PushAsync(Page page, bool animated)
        {
            Detail = page;
            return Task.FromResult(page);
        }

        public Task PushAsync1(Page page, bool animated)
        {
            Detail1 = page;
            return Task.FromResult(page);
        }

        /// <summary>
        /// This method handles submit event when the submitted data is to the previous page.
        /// PopAsync is not useful as it retreives the previous page and setting updated binding context can not be set.
        /// </summary>
        /// <param name="previousPage"></param>
        /// <returns></returns>
        public Task<Page> PushAsyncPreviousPage(Page previousPage = null)
        {
            Page page = null;
            Page page1 = null;
            //if (pages.Count > 0)
            // {
            if (Isportrait && pages.Count > BACK_BUTTON_PAGE_COUNT)
            {
                pages.Pop();
                pages.Pop();
                Header = App.PageTitels[previousPage.StyleId];
                PushAsync(previousPage);
                //   RaisePropertyChanged("Detail");
            }
            else if (pages.Count > BACK_BUTTON_PAGE_COUNT)
            {
                if (pages.Count == 2)
                {
                    page1 = pages.Pop();
                    page = pages.Pop();
                    Header = App.PageTitels[page.StyleId];
                    PushAsync(previousPage);
                }
                else
                {
                    pages.Pop();
                    page = pages.Pop();
                    page1 = pages.Pop();
                    Header = App.PageTitels[page1.StyleId];
                    Header1 = App.PageTitels[page.StyleId];
                    PushAsync(page1);
                    PushAsync1(previousPage);
                }
                //    RaisePropertyChanged("Detail");
            }
            else//if (pages != null && pages.Count == 1)
            {
                page = pages.Pop();
                Header = App.PageTitels[page.StyleId];
                PushAsync(Detail);
                //   RaisePropertyChanged("Detail");
                // PushAsync1(Detail);
            }
            //_detail = page;
            //  RaisePropertyChanged("Detail");
            //  }
            return page != null ? Task.FromResult(page) : navigation.PopAsync();
        }

        public Task<Page> PopAsync1()
        {
            Page page = null;
            Page page1 = null;
            //if (pages.Count > 0)
            // {
            if (Isportrait && pages.Count > BACK_BUTTON_PAGE_COUNT)
            {
                pages.Pop();
                page = pages.Pop();
                Header = App.PageTitels[page.StyleId];
                PushAsync(page);
            }
            else if (pages.Count > BACK_BUTTON_PAGE_COUNT)
            {
                if (pages.Count == 2)
                {
                    page1 = pages.Pop();
                    page = pages.Pop();
                    Header = App.PageTitels[page.StyleId];
                    PushAsync(page);
                }
                else
                {
                    pages.Pop();
                    page = pages.Pop();
                    page1 = pages.Pop();
                    Header = App.PageTitels[page1.StyleId];
                    Header1 = App.PageTitels[page.StyleId];
                    PushAsync(page1);
                    PushAsync1(page);
                }
            }
            else
            {
                page = pages.Pop();
                Header = App.PageTitels[page.StyleId];
                PushAsync(Detail);
            }
            return page != null ? Task.FromResult(page) : navigation.PopAsync();
        }

        public Task<Page> PopAsync()
        {
            Page page = null;
            if (pages.Count > 0)
            {
                page = pages.Pop();
                detail = page;
                RaisePropertyChanged("Detail");
            }
            return page != null ? Task.FromResult(page) : navigation.PopAsync();
        }

        public Task<Page> PopAsync(bool animated)
        {
            Page page = null;
            if (pages.Count > 0)
            {
                page = pages.Pop();
                detail = page;
                RaisePropertyChanged("Detail");
            }
            return page != null ? Task.FromResult(page) : navigation.PopAsync(animated);
        }

        public Task<Page> PopAsync1(bool animated)
        {
            Page page = null;
            if (pages.Count > 0)
            {
                page = pages.Pop();
                detail1 = page;
                RaisePropertyChanged("Detail1");
            }
            return page != null ? Task.FromResult(page) : navigation.PopAsync(animated);
        }

        public void InsertPageBefore(Page page, Page before)
        {
            if (pages.Contains(before))
            {
                var list = pages.ToList();
                var indexOfBefore = list.IndexOf(before);
                list.Insert(indexOfBefore, page);
                pages = new Stack<Page>(list);
            }
            else
            {
                navigation.InsertPageBefore(page, before);
            }
        }

        public Task<Page> PopModalAsync()
        {
            return navigation.PopModalAsync();
        }

        public Task<Page> PopModalAsync(bool animated)
        {
            return navigation.PopModalAsync(animated);
        }

        public Task PopToRootAsync()
        {
            var firstPage = navigation.NavigationStack[0];
            if (firstPage is MasterDetailControl
                || firstPage.GetType() == typeof(MasterDetailControl))
            {
                pages = new Stack<Page>(new[] { pages.FirstOrDefault() });
                return Task.FromResult(firstPage);
            }
            return navigation.PopToRootAsync();
        }

        public Task PopToRootAsync(bool animated)
        {
            var firstPage = navigation.NavigationStack[0];
            if (firstPage is MasterDetailControl
                || firstPage.GetType() == typeof(MasterDetailControl))
            {
                pages = new Stack<Page>(new[] { pages.FirstOrDefault() });
                return Task.FromResult(firstPage);
            }
            return navigation.PopToRootAsync(animated);
        }

        public Task PushModalAsync(Page page)
        {
            return navigation.PushModalAsync(page);
        }

        public Task PushModalAsync(Page page, bool animated)
        {
            return navigation.PushModalAsync(page, animated);
        }

        public void RemovePage(Page page)
        {
            if (pages.Contains(page))
            {
                var list = pages.ToList();
                list.Remove(page);
                pages = new Stack<Page>(list);
            }
            navigation.RemovePage(page);
        }

        public void RemoveAllPages()
        {
            if (pages.Count > 0)
            {
                pages.Clear();
            }
        }

        public void SetNavigation(INavigation navigation)
        {
            this.navigation = navigation;
        }
        #endregion

        #region Orientation change handling methods
        public void AdjustScreenOnOrientationChange(bool orientationChanges = false)
        {
            this.GetSecondContentVisibility(orientationChanges);
        }
        #endregion

        #region Private Methods
        private void GetSecondContentVisibility(bool orientationChanges = false)
        {
            App.MasterDetailVM.SecondContentVisibility = (!Isportrait && pages.Count > SECOND_CONTENT_PAGE_COUNT);
            App.MasterDetailVM.BackButtonVisibility = (Device.OS == TargetPlatform.iOS && pages.Count > BACK_BUTTON_PAGE_COUNT);
            App.MasterDetailVM.DetailGridColSpan = ((!Isportrait && pages.Count > SECOND_CONTENT_PAGE_COUNT) ? 1 : 2);
            App.MasterDetailVM.DetailGridHeaderColSpan = ((!Isportrait && pages.Count > SECOND_CONTENT_PAGE_COUNT) ? 1 : 4);
        }
        #endregion
    }
}
