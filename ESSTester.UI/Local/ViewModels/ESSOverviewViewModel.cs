using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LIB_ESS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

namespace ESSTester.UI.Local.ViewModels
{
    public class ESSOverviewViewModel : BaseViewModel
    {
        public List<AlarmItem> Alarms { get; } = new List<AlarmItem>();

        public ESSOverviewViewModel()
        {
            CreateAlarmItems();
        }

        //public BMS_RACK_MAIN[] BmsRackMain => CommonData.BmsMain.BmsRackList.RackList.ToArray();

        private void CreateAlarmItems()
        {
            Alarms.Add(new AlarmItem() { Name = "BMS" });
            Alarms.Add(new AlarmItem() { Name = "RACK #1" });
            Alarms.Add(new AlarmItem() { Name = "RACK #2" });
            Alarms.Add(new AlarmItem() { Name = "RACK #3" });
            Alarms.Add(new AlarmItem() { Name = "RACK #4" });
            Alarms.Add(new AlarmItem() { Name = "RACK #5" });
            Alarms.Add(new AlarmItem() { Name = "RACK #6" });
            Alarms.Add(new AlarmItem() { Name = "RACK #7" });
            Alarms.Add(new AlarmItem() { Name = "RACK #8" });
            Alarms.Add(new AlarmItem() { Name = "RACK #9" });
            Alarms.Add(new AlarmItem() { Name = "ETC" });
        }

        private ICommand _SelectionAlarmCommand;
        public ICommand SelectionAlarmCommand => _SelectionAlarmCommand ?? (_SelectionAlarmCommand = new RelayCommand<AlarmItem>((item) =>
        {
            Console.WriteLine($"{item.Name}, {item.IsAlarm}, {item.StateColor}");
            //To-Do : Page Changing

        }));

    }

    public class AlarmItem : ObservableObject
    {
        private readonly Color alarmColor = Colors.Red;
        private readonly Color noneColor = Colors.Green;

        private bool _isAlarm = false;

        public string Name { get; set; }
        public bool IsAlarm { get => _isAlarm; set => SetAlarm(value); }

        private Brush _StateColor;
        public Brush StateColor { get => _StateColor; set { SetProperty(ref _StateColor, value); } }

        public AlarmItem()
        {
            SetAlarm(false);
        }

        public void SetAlarm(bool isAlarm)
        {
            _isAlarm = isAlarm;
            if (isAlarm) StateColor = new SolidColorBrush(alarmColor);
            else StateColor = new SolidColorBrush(noneColor);
        }
    }
}
