using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ESSTester.UI.UI.Units
{

    public class NormalButton : Button
    {
        static NormalButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NormalButton), new FrameworkPropertyMetadata(typeof(NormalButton)));
        }


        public Brush MouseOverColor
        {
            get { return (Brush)GetValue(MouseOverColorProperty); }
            set { SetValue(MouseOverColorProperty, value); }
        }
        
        public static readonly DependencyProperty MouseOverColorProperty =
            DependencyProperty.Register("MouseOverColor", typeof(Brush), typeof(NormalButton), new PropertyMetadata(new SolidColorBrush(Colors.WhiteSmoke)));


    }
}
