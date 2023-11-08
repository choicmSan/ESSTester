using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ESSTester.UI.UI.Views
{
    public class ESSEtcView : UserControl
    {
        static ESSEtcView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ESSEtcView), new FrameworkPropertyMetadata(typeof(ESSEtcView)));
        }
    }
}
