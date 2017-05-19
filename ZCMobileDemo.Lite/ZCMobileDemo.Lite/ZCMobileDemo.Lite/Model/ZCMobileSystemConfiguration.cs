using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCMobileDemo.Lite.Model
{
    /// <summary>
    /// ZCMobileSystemConfiguration class
    /// </summary>
    public class ZCMobileSystemConfiguration
    {
        #region Public Properties
        public string SelectedDataCenter { get; set; }
        public bool RememberUser { get; set; }
        public string RememberedUser { get; set; }
        public bool SideContentVisibility { get; set; }
        #endregion
    }
}
