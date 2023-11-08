using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ESSTester.UI.UI.Units
{
    public class MainTopValueControl : TextBox
    {
        static MainTopValueControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MainTopValueControl), new FrameworkPropertyMetadata(typeof(MainTopValueControl)));
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(MainTopValueControl), new PropertyMetadata("title"));



        public string Sign
        {
            get { return (string)GetValue(SignProperty); }
            set { SetValue(SignProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Sign.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SignProperty =
            DependencyProperty.Register("Sign", typeof(string), typeof(MainTopValueControl), new PropertyMetadata("%"));



    }
}
