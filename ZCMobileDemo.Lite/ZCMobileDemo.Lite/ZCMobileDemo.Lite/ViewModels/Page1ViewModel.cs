using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCMobileDemo.Lite.ViewModels
{
    public class Page1ViewModel : BaseViewModel
    {
        #region Private Members
        private string message1 = "Message 1 - Page 1";
        private string message2 = "Message 2 - Page 1";
        #endregion

        #region Public Properties
        public string Messsge1
        {
            get
            {
                return message1;
            }
            set
            {
                message1 = value;
                RaisePropertyChanged();
            }
        }

        public string Messsge2
        {
            get
            {
                return message2;
            }
            set
            {
                message2 = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}
