using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ZCMobileDemo.Lite.Model
{
    public class ListDataViewCell : ViewCell
    {
        public ListDataViewCell()
        {

            var label = new Label()
            {
                Font = Font.SystemFontOfSize(NamedSize.Default),
                TextColor = Color.FromHex("#c1eaf6"),
                Margin = new Thickness(3, 0, 0, 0)

            };

            label.SetBinding(Label.TextProperty, new Binding("TextValue"));
            label.SetBinding(Label.ClassIdProperty, new Binding("DataValue"));

            View = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = { label },
                BackgroundColor = Color.FromHex("#01446b")
            };
        }
    }
}
