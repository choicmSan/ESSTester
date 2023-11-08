using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LIB_ESS.Model
{
    public class BMS_BASE : ObservableObject
    {
        protected uint MergeTwoUShort(ushort v1, ushort v2)
        {
            var v = (v1 << 16) | v2;
            return (uint)v;
        }

        protected void GetByteValue(ushort value, out byte value1, out byte value2)
        {
            var v1 = (value >> 8);
            var v2 = (value & 0xFF);
            value1 = (byte)v1;
            value2 = (byte)v2;
        }

        protected bool[] GetBools(ushort value)
        {
            bool[] bools = new bool[16];
            byte mask = 0x0001;
            for (int i = 0; i < 16; i++)
                bools[i] = (value & (mask << i)) != 0;
            return bools;
        }

        protected List<IItemValueType> InstanceGetItemValueTypePropertyList<T>(T obj) where T : class
        {
            var propertyInfos = obj.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                             .Where(pt => pt.GetValue(obj).GetType().Name == typeof(ItemValueType<>).Name)
                                             .Select(pt => pt.GetValue(obj) as IItemValueType)
                                             .ToList();
            return propertyInfos;
        }

        public List<IItemValueType> GetItemValueTypePropertyList()
        {
            var propertyInfos = this.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                             .Where(pt => pt.GetValue(this).GetType().Name == typeof(ItemValueType<>).Name)
                                             .Select(pt => pt.GetValue(this) as IItemValueType)
                                             .ToList();
            return propertyInfos;
        }
    }


    public interface IItemValueType
    {
        string ItemName { get; set; }
        string ItemValue { get; set; }
    }

    public class ItemValueType<T> : ObservableObject, IItemValueType
    {
        private string _itemName;
        private string _itemValue;

        public string ItemName { get => _itemName; set { SetProperty(ref _itemName, value); } }
        public string ItemValue { get => _itemValue; set { SetProperty(ref _itemValue, value); } }

        private T _value;
        public ItemValueType(string itemName)
        {
            ItemName = itemName;
        }

        public void SetValue(T value)
        {
            _value = value;
            ItemValue = _value.ToString();
        }

    }

    public class BMS_MAIN : ObservableObject
    {
        public const int BMS_RACK_COUNT = 9;
        public BMS_RBMS BmsRBMS { get; } = new BMS_RBMS();
        public BMS_RACK_LIST BmsRackList { get; } = new BMS_RACK_LIST(BMS_RACK_COUNT);

        public List<IItemValueType> GetBmsRBMSProperties()
        {
            List<IItemValueType> items = new List<IItemValueType>();
            items.AddRange(InstanceGetItemValueTypePropertyList(BmsRBMS.BmsRBMSRackValues));
            items.AddRange(InstanceGetItemValueTypePropertyList(BmsRBMS.BmsRBMSDTCState));
            items.AddRange(InstanceGetItemValueTypePropertyList(BmsRBMS.BmsRBMSDTCAlarm1));
            items.AddRange(InstanceGetItemValueTypePropertyList(BmsRBMS.BmsRBMSDTCAlarm2));
            items.AddRange(InstanceGetItemValueTypePropertyList(BmsRBMS.BmsRBMSDTCAlarm3));
            items.AddRange(InstanceGetItemValueTypePropertyList(BmsRBMS.BmsRBMSSystemFaultBits));
            items.AddRange(InstanceGetItemValueTypePropertyList(BmsRBMS.BmsRBMSRackOnlines));
            items.AddRange(InstanceGetItemValueTypePropertyList(BmsRBMS.BmsRBMSRackDischargeMode));
            return items;
        }

        protected List<IItemValueType> InstanceGetItemValueTypePropertyList<T>(T obj) where T : class
        {
            var propertyInfos = obj.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                             .Where(pt => pt.GetValue(obj).GetType().Name == typeof(ItemValueType<>).Name)
                                             .Select(pt => pt.GetValue(obj) as IItemValueType)
                                             .ToList();
            return propertyInfos;
        }

    }


}
