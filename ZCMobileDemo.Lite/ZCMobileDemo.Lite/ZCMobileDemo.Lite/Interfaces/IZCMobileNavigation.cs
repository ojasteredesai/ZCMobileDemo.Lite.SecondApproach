using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ZCMobileDemo.Lite.Interfaces
{
    public interface IZCMobileNavigation : INavigation
    {
        void RemoveAllPages();
        Task PushAsync1(Page page);
        Task PushAsync1(Page page, bool animated);
        Task<Page> PushAsyncPreviousPage(Page previousPage = null);
        Task<Page> PopAsync1();
        Task<Page> PopAsync1(bool animated);
        void AdjustScreenOnOrientationChange(bool orientationChanges = false);
    }
}
