using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ESSTester.UI.UI.Units
{
    public class OverviewValueText : TextBox
    {
        static OverviewValueText()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OverviewValueText), new FrameworkPropertyMetadata(typeof(OverviewValueText)));
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(OverviewValueText), new PropertyMetadata("title"));




        public string Sign
        {
            get { return (string )GetValue(SignProperty); }
            set { SetValue(SignProperty, value); }
        }

        public static readonly DependencyProperty SignProperty =
            DependencyProperty.Register("Sign", typeof(string ), typeof(OverviewValueText), new PropertyMetadata("%"));


    }
}
