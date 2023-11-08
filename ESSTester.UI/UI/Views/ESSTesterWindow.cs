using System.Windows;
using System.Windows.Controls;

namespace ESSTester.UI.UI.Views
{

    public class ESSTesterWindow : Window
    {
        static ESSTesterWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ESSTesterWindow), new FrameworkPropertyMetadata(typeof(ESSTesterWindow)));
        }



        //public WindowStartupLocation WindowStartupLocation
        //{
        //    get { return (WindowStartupLocation)GetValue(WindowStartupLocationProperty); }
        //    set { SetValue(WindowStartupLocationProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for WindowStartupLocation.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty WindowStartupLocationProperty =
        //    DependencyProperty.Register("WindowStartupLocation", typeof(WindowStartupLocation), typeof(ownerclass), new PropertyMetadata(0));


    }
}
