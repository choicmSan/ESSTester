using CommunityToolkit.Mvvm.ComponentModel;
using ESSTester.UI.Local.Models.Containers;
using LIB_ESS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ESSTester.UI.Local.Models
{
    public class CommonAppData : ObservableObject
    {
        private static CommonAppData instance;
        public static CommonAppData Instance => instance ?? (instance = new CommonAppData());


        public Brush OverviewValueTextColor { get; } = (SolidColorBrush)new BrushConverter().ConvertFrom("#33FF33");

        private BMS_MAIN _bmsMain = new BMS_MAIN();
        public BMS_MAIN BmsMain
        {
            get
            {
                return _bmsMain;
            }
        }

    }
}
