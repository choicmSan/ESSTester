using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ESSTester.UI.Local.Models.Containers;
using LIB_ESS.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace ESSTester.UI.Local.ViewModels
{
    public sealed class ESSTesterWindowViewModel : BaseViewModel
    {
        private readonly string[] topMenus = { "OVERVIEW", "RACK", "ETC", "EXPORT", "SETUP", "EXIT" };

        private ICommand _ClickTopMenu;
        public ICommand ClickTopMenu => _ClickTopMenu ?? (_ClickTopMenu = new RelayCommand<string>(OnClickTopMenu));

        private string _lastTopMenu = "OVERVIEW";
        private List<TopMenuCheck> _IsCheckedTopMenus = new List<TopMenuCheck>();
        public List<TopMenuCheck> IsCheckedTopMenus { get => _IsCheckedTopMenus; set { SetProperty(ref _IsCheckedTopMenus, value); } }

        public ContainerViewModel ViewModels { get; } = new ContainerViewModel();


        public ESSTesterWindowViewModel()
        {
            foreach (var menu in topMenus) { _IsCheckedTopMenus.Add(new TopMenuCheck() { MenuName = menu }); }
            _IsCheckedTopMenus[0].IsChecked = true;


            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TestBMSValues();
        }

        private void OnClickTopMenu(string content)
        {
            if (content is string menu && DoTopMenuIsChecked(menu))
            {
                switch (menu)
                {
                    case "OVERVIEW": break;
                    case "RACK": break;
                    case "ETC": break;
                    case "EXPORT": break;
                    case "SETUP": break;
                    case "EXIT": CloseApplication(); break;
                }
            }
        }

        Random rd = new Random();
        private int index = 1;
        private void TestBMSValues()
        {
            var isAlrm = ViewModels.EssOverviewViewModel.Alarms[0].IsAlarm;
            ViewModels.EssOverviewViewModel.Alarms[0].SetAlarm(!isAlrm);

            ushort[] bmsValues = new ushort[34];
            for (int i = 0; i < bmsValues.Length; i++)
            {
                bmsValues[i] = (ushort)rd.Next();
            }
            CommonData.BmsMain.BmsRBMS.SetHoldingRegister(bmsValues);

            if (!IsTrayUpdating)
            {
                SetTrayValues();
            }
        }

        private bool IsTrayUpdating = false;
        private async void SetTrayValues()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine($"\tSTART RACK UPDATING : [{sw.ElapsedMilliseconds}]");
            IsTrayUpdating = true;

            if (index > 9) index = 1;

            var rack = CommonData.BmsMain.BmsRackList[index++];

            ushort[] rackValues = new ushort[36];
            for (int i = 0; i < rackValues.Length; i++)
            {
                rackValues[i] = (ushort)rd.Next();
            }
            rack.SetBMSRACKHoldingRegister(rackValues);
            await Task.Delay(1000);

            ushort[] trayValues = new ushort[120];
            for (int n = 0; n < 5; n++)
            {
                for (int i = 0; i < trayValues.Length; i++)
                {
                    trayValues[i] = (ushort)rd.Next();
                }
                rack.SetBMSTRAYHoldingRegister(trayValues, (n * 4) + 1, 4);
                await Task.Delay(1000);
            }
            IsTrayUpdating = false;
            Console.WriteLine($"\tEND RACK UPDATED : [{sw.ElapsedMilliseconds}]");
        }

        private bool DoTopMenuIsChecked(string menu)
        {
            if (_lastTopMenu == menu) return false;
            if (menu == "EXIT") return true;
            
            _lastTopMenu = menu;
            foreach (var topMenu in _IsCheckedTopMenus) topMenu.IsChecked = false;
            _IsCheckedTopMenus.FirstOrDefault(mn => mn.MenuName == menu).IsChecked = true;
            return true;
        }

        private void CloseApplication()
        {
            if (MessageBox.Show("종료하시겠습니까?","EXIT",MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            }
            _IsCheckedTopMenus[5].IsChecked = false;
        }

        private ICommand _ClickCharge;
        public ICommand ClickCharge => _ClickCharge ?? (_ClickCharge = new RelayCommand(() =>
        {
            Console.WriteLine("Click Charge");
        }));

        private ICommand _ClickDischarge;
        public ICommand ClickDischarge => _ClickDischarge ?? (_ClickDischarge = new RelayCommand(() =>
        {
            Console.WriteLine("Click Discharge");
        }));

    }

    public class TopMenuCheck : ObservableObject
    {
        public string MenuName { get; set; }

        private bool _IsChecked = false;
        public bool IsChecked
        {
            get { return _IsChecked; }
            set { SetProperty(ref _IsChecked, value); }
        }
    }

}
