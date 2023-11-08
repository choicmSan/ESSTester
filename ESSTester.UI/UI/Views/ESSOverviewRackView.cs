using LIB_ESS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ESSTester.UI.UI.Views
{
    public class ESSOverviewRackView : UserControl
    {
        static ESSOverviewRackView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ESSOverviewRackView), new FrameworkPropertyMetadata(typeof(ESSOverviewRackView)));
        }



        public BMS_RACK_MAIN BmsRackMain
        {
            get { return (BMS_RACK_MAIN)GetValue(BmsRackMainProperty); }
            set { SetValue(BmsRackMainProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BmsRackMainProperty =
            DependencyProperty.Register("BmsRackMain", typeof(BMS_RACK_MAIN), typeof(ESSOverviewRackView), new PropertyMetadata(OnSet));

        private static void OnSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var nn = e.NewValue as BMS_RACK_MAIN;
        }
    }
}
