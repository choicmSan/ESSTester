using LIB_ESS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace ESSTester.UI.Local.ViewModels
{
    public class ESSRackViewModel : BaseViewModel
    {
        private CollectionViewSource bmsCvs = new CollectionViewSource();
        private ObservableCollection<IItemValueType> bmsCol = new ObservableCollection<IItemValueType>();

        private CollectionViewSource rackCvs = new CollectionViewSource();
        private ObservableCollection<IItemValueType> rackCol = new ObservableCollection<IItemValueType>();

        private CollectionViewSource trayCvs = new CollectionViewSource();
        private ObservableCollection<ItemValueTypeGroup> trayCol = new ObservableCollection<ItemValueTypeGroup>();

        public ICollectionView BmsView { get => bmsCvs.View; }

        public ICollectionView RackView { get => rackCvs.View; }

        public ICollectionView TrayView { get => trayCvs.View; }

        public List<RackComboxItem> RackList { get; } = new List<RackComboxItem>();

        private RackComboxItem _SelectedRack;
        public RackComboxItem SelectedRack
        {
            get => _SelectedRack;
            set
            {
                if (_SelectedRack == value) { return; }
                SetProperty(ref _SelectedRack, value);
                InitRackItemVAlueCollections(_SelectedRack.BmsRackMain);
            }
        }

        public BMS_MAIN BmsMain => CommonData.BmsMain;

        public ESSRackViewModel()
        {

            InitBMSItemValueCollections();
            InitRackItemVAlueCollections(BmsMain.BmsRackList[1]);

            foreach (var rack in CommonData.BmsMain.BmsRackList)
            {
                RackList.Add(new RackComboxItem() { BmsRackMain = rack });
            }
            SelectedRack = RackList[0];
        }

        private void InitRackItemVAlueCollections(BMS_RACK_MAIN bmsRackMain)
        {
            rackCol.Clear();

            var rack = bmsRackMain;
            var rackProperties = rack.GetItemValueTypeProperties();
            foreach (var tt in rackProperties)
            {
                //Debug.WriteLine($"{tt.ItemName}:{tt.ItemValue}");
                rackCol.Add(tt);
            }
            rackCvs.Source = rackCol;

            trayCol.Clear();
            var trays = rack.BmsTrayList;
            foreach (var tray in trays)
            {
                {
                    //trayCol.Add(new ItemValueType<string>($"\t\tTRAY [{tray.TrayNum}]") { ItemValue = "-" });
                    //var trayProperties = tray.GetItemValueTypeProperties();
                    //foreach (var tt in trayProperties)
                    //{
                    //    Debug.WriteLine($"{tt.ItemName}:{tt.ItemValue}");
                    //    trayCol.Add(tt);
                    //}
                }
                var trayProperties = tray.GetItemValueTypeProperties();
                foreach (var tt in trayProperties)
                {
                    //Debug.WriteLine($"{tt.ItemName}:{tt.ItemValue}");
                    var itemValueGroup = new ItemValueTypeGroup()
                    {
                        Name = tt.ItemName,
                        ItemValue = tt,
                        ItemGroupType = (ItemGroupType)tray.TrayNum,
                    };
                    trayCol.Add(itemValueGroup);
                }
            }
            trayCvs.Source = trayCol;

            TrayView.GroupDescriptions.Clear();
            PropertyGroupDescription propertyGroup = new PropertyGroupDescription("ItemGroupType");
            TrayView.GroupDescriptions.Add(propertyGroup);
        }

        private void InitBMSItemValueCollections()
        {
            var bmsProperties = BmsMain.GetBmsRBMSProperties();
            foreach (var tt in bmsProperties)
            {
                //Debug.WriteLine($"{tt.ItemName}:{tt.ItemValue}");
                bmsCol.Add(tt);
            }
            bmsCvs.Source = bmsCol;
        }
    }

    public enum ItemGroupType
    {
        Tray01 = 1, Tray02, Tray03, Tray04, Tray05, Tray06, Tray07, Tray08, Tray09, Tray10,
        Tray11, Tray12, Tray13, Tray14, Tray15, Tray16, Tray17, Tray18, Tray19, Tray20,
    }

    public class ItemValueTypeGroup
    {
        public IItemValueType ItemValue { get; set; }
        public string Name { get; set; }
        public ItemGroupType ItemGroupType { get; set; }
    }

    public class RackComboxItem
    {
        private int _RackNum;

        public string Name { get; set; } = "Rack";
        public int RackNum
        {
            get => _RackNum;
            set { _RackNum = value; Name = $"Rack#{_RackNum}"; }
        }

        private BMS_RACK_MAIN _bmsRackMain;
        public BMS_RACK_MAIN BmsRackMain
        {
            get => _bmsRackMain;
            set { _bmsRackMain = value;  RackNum = _bmsRackMain.RackNum; }
        }

    }
}
