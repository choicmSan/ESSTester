using ESSTester.UI.Local.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESSTester.UI.Local.Models.Containers
{
    public class ContainerViewModel
    {
        public ESSOverviewViewModel EssOverviewViewModel { get; } = new ESSOverviewViewModel();
        public ESSRackViewModel EssRackViewModel { get; } = new ESSRackViewModel();
        public ESSEtcViewModel EssEtcViewModel { get; } = new ESSEtcViewModel();
    }
}
