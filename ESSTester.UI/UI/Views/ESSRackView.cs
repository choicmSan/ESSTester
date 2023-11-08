using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ESSTester.UI.UI.Views
{
    public class ESSRackView : UserControl
    {
        static ESSRackView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ESSRackView), new FrameworkPropertyMetadata(typeof(ESSRackView)));
        }
    }
}
