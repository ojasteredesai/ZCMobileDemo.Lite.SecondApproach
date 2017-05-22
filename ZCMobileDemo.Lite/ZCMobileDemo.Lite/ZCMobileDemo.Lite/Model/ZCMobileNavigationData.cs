using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ZCMobileDemo.Lite.Model
{
    public class ZCMobileNavigationData
    {
        #region Public Properties
        public Page CurrentPage { get; set; }
        public Page NextPage { get; set; }
        public string CurrentPageTitle { get; set; }
        public string NextPageTitle { get; set; }
        public object Data { get; set; }
        #endregion
    }
}
