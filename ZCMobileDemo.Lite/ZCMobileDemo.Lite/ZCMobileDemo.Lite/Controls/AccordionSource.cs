using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ZCMobileDemo.Lite.Controls
{

    /// <summary>
    /// AccordionSource class
    /// </summary>
    public class AccordionSource
    {
        #region Private Members
        private View _ContentItems;
        #endregion

        #region Properties
        public string HeaderText { get; set; }
        public Color HeaderTextColor { get; set; }
        public Color HeaderBackGroundColor { get; set; }

        public View ContentItems
        {
            get { return _ContentItems; }
            set
            {
                _ContentItems = value;
                if (_ContentItems != null)
                    _ContentItems.BackgroundColor = Color.FromHex("#01446b");
            }
        }
        #endregion
    }
}
