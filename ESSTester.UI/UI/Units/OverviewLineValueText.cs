using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ESSTester.UI.UI.Units
{
    public class OverviewLineValueText : TextBox
    {
        static OverviewLineValueText()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OverviewLineValueText), new FrameworkPropertyMetadata(typeof(OverviewLineValueText)));
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(OverviewLineValueText), new PropertyMetadata("title"));



        public string Sign
        {
            get { return (string)GetValue(SignProperty); }
            set { SetValue(SignProperty, value); }
        }

        public static readonly DependencyProperty SignProperty =
            DependencyProperty.Register("Sign", typeof(string), typeof(OverviewLineValueText), new PropertyMetadata("%"));
    }
}
