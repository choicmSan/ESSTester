using CommunityToolkit.Mvvm.ComponentModel;
using ESSTester.UI.Local.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESSTester.UI.Local.ViewModels
{
    public abstract class BaseViewModel : ObservableObject
    {
        public CommonAppData CommonData  => CommonAppData.Instance;
    }

    
}
