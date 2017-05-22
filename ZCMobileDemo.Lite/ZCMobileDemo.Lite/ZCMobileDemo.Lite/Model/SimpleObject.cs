using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCMobileDemo.Lite.Model
{

    public class SimpleObject
    {
        public SimpleObject()
        {
            ChildItemList = new List<ChildItems>();
        }
        public string HeaderText { get; set; }
        public List<ChildItems> ChildItemList { get; set; }
    }
}
