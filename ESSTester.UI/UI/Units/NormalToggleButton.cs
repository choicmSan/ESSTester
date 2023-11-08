using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ESSTester.UI.UI.Units
{
    public class NormalToggleButton : RadioButton
    {
        static NormalToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NormalToggleButton), new FrameworkPropertyMetadata(typeof(NormalToggleButton)));
        }
    }
}
