using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ZCMobileDemo.Lite.Controls
{
    /// <summary>
    /// AccordionButton class
    /// </summary>
    public class AccordionButton : Button
    {
        #region Private Variables
        bool mExpand = false;
        #endregion

        #region Constructors
        public AccordionButton()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand;
            BorderColor = Color.Black;
            BorderRadius = 0;
            BorderWidth = 0;
        }
        #endregion 

        #region Properties
        public bool Expand
        {
            get { return mExpand; }
            set { mExpand = value; }
        }
        public ContentView AssosiatedContent
        { get; set; }
        #endregion
    }
}
