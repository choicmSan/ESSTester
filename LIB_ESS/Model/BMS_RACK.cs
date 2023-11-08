using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB_ESS.Model
{
    public sealed class BMS_RACK_LIST : IEnumerable<BMS_RACK_MAIN>
    {
        private ObservableCollection<BMS_RACK_MAIN> _rackList = new ObservableCollection<BMS_RACK_MAIN>();
        public ObservableCollection<BMS_RACK_MAIN> RackList => _rackList;

        public BMS_RACK_LIST(int rackCount)
        {
            for (int i = 1; i <= rackCount; i++)
            {
                _rackList.Add(new BMS_RACK_MAIN() { RackNum = i });
            }
        }

        public BMS_RACK_MAIN this[int rackNum]
        {
            get { var _ = _rackList.FirstOrDefault(rk => rk.RackNum == rackNum); return _; }
        }

        public BMS_RACK_MAIN GetBMSRackMain(int rackNum)
        {
            return this[rackNum];
        }

        public IEnumerator<BMS_RACK_MAIN> GetEnumerator()
        {
            return ((IEnumerable<BMS_RACK_MAIN>)_rackList).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_rackList).GetEnumerator();
        }
    }

    public sealed class BMS_RACK_MAIN : BMS_BASE
    {
        public const int BMS_TRAY_COUNT = 20;

        private int _rackNum = -1;
        public int RackNum
        {
            get => _rackNum;
            set { SetProperty(ref _rackNum, value); }
        }

        public BMS_RACK_SYSTEM_RTC BmsRackSystemRTC { get; } = new BMS_RACK_SYSTEM_RTC();
        public BMS_RACK_BATTERY_OPERATION_STATUS BmsRackBatteryOperationStatus { get; } = new BMS_RACK_BATTERY_OPERATION_STATUS();
        public BMS_RACK_BATTERY_WARNING_CODE BmsRackBatteryWarningCode { get; } = new BMS_RACK_BATTERY_WARNING_CODE();
        public BMS_RACK_BATTERY_PROTECTION_FAULT_CODE BmsRackBatteryProtectionFaultCode { get; } = new BMS_RACK_BATTERY_PROTECTION_FAULT_CODE();
        public BMS_RACK_TRAY_COMM_FAULT_CODE BmsRackTrayCommFaultCode { get; } = new BMS_RACK_TRAY_COMM_FAULT_CODE();
        public BMS_RACK_VALUE_DATAS BmsRackValueDatas { get; } = new BMS_RACK_VALUE_DATAS();

        public BMS_TRAY_LIST BmsTrayList { get; } = new BMS_TRAY_LIST(BMS_TRAY_COUNT);

        /// <summary>
        /// Holding Register 21000 - 21035 : 36 ea 
        /// </summary>
        /// <param name="values">index 0 ~ 35</param>
        public void SetBMSRACKHoldingRegister(ushort[] values)
        {
            System.Span<ushort> rackValues = values;
            BmsRackSystemRTC.SetHoldingRegister(rackValues.Slice(0, 5).ToArray());
            BmsRackBatteryOperationStatus.SetHoldingRegister(values[5]);
            BmsRackBatteryWarningCode.SetHoldingRegister(values[6]);
            BmsRackBatteryProtectionFaultCode.SetHoldingRegister(values[7]);
            BmsRackTrayCommFaultCode.SetHoldingRegister(rackValues.Slice(8, 2).ToArray());
            BmsRackValueDatas.SetHoldingRegister(rackValues.Slice(10, 26).ToArray());
        }

        public void SetBMSTRAYHoldingRegister(ushort[] values, int startRackNum, int numberOfRack)
        {
            int startAddr = 0;
            System.Span<ushort> trayValues = values;
            for (int i = startRackNum; i < startRackNum + numberOfRack; i++)
            {
                var BmsTray = BmsTrayList[i];
                if (BmsTray != null)
                {
                    var trayValue = trayValues.Slice(startAddr, 30).ToArray();
                    BmsTray.SetHoldingRegister(trayValue);
                    startAddr += 30;
                }
            }
        }

        public List<IItemValueType> GetItemValueTypeProperties()
        {
            var items = new List<IItemValueType>();
            items.AddRange(BmsRackSystemRTC.GetItemValueTypePropertyList());
            items.AddRange(BmsRackBatteryOperationStatus.GetItemValueTypePropertyList());
            items.AddRange(BmsRackBatteryWarningCode.GetItemValueTypePropertyList());
            items.AddRange(BmsRackBatteryProtectionFaultCode.GetItemValueTypePropertyList());
            items.AddRange(BmsRackTrayCommFaultCode.GetItemValueTypePropertyList());
            items.AddRange(BmsRackValueDatas.GetItemValueTypePropertyList());
            return items;
        }


    }

    public sealed class BMS_RACK_SYSTEM_RTC : BMS_BASE
    {
        private byte _system_RTC_Centry;
        private byte _system_RTC_Year;
        private byte _system_RTC_Month;
        private byte _system_RTC_Date;
        private byte _system_RTC_Hour;
        private byte _system_RTC_Minute;
        private byte _system_RTC_Second;
        private byte _dummy;

        private byte _numberOfTraysAndCells;
        private byte _numberOfTrays;
        private byte _numberOfCells;

        public ItemValueType<byte> System_RTC_Centry_Type { get; } = new ItemValueType<byte>("System_RTC_Centry");
        public ItemValueType<byte> System_RTC_Year_Type { get; } = new ItemValueType<byte>("System_RTC_Year");
        public ItemValueType<byte> System_RTC_Month_Type { get; } = new ItemValueType<byte>("System_RTC_Month");
        public ItemValueType<byte> System_RTC_Date_Type { get; } = new ItemValueType<byte>("System_RTC_Month");
        public ItemValueType<byte> System_RTC_Hour_Type { get; } = new ItemValueType<byte>("System_RTC_Hour");
        public ItemValueType<byte> System_RTC_Minute_Type { get; } = new ItemValueType<byte>("System_RTC_Minute");
        public ItemValueType<byte> System_RTC_Second_Type { get; } = new ItemValueType<byte>("System_RTC_Second");

        public ItemValueType<byte> NumberOfTryas_Type { get; } = new ItemValueType<byte>("NumberOfTrays");
        public ItemValueType<byte> NumberOfCells_Type { get; } = new ItemValueType<byte>("NumberOfCells");

        public byte System_RTC_Centry { get => _system_RTC_Centry; set { SetProperty(ref _system_RTC_Centry, value); } }
        public byte System_RTC_Year { get => _system_RTC_Year; set { SetProperty(ref _system_RTC_Year, value); } }
        public byte System_RTC_Month { get => _system_RTC_Month; set { SetProperty(ref _system_RTC_Month, value); } }
        public byte System_RTC_Date { get => _system_RTC_Date; set { SetProperty(ref _system_RTC_Date, value); } }
        public byte System_RTC_Hour { get => _system_RTC_Hour; set { SetProperty(ref _system_RTC_Hour, value); } }
        public byte System_RTC_Minute { get => _system_RTC_Minute; set { SetProperty(ref _system_RTC_Minute, value); } }
        public byte System_RTC_Second { get => _system_RTC_Second; set { SetProperty(ref _system_RTC_Second, value); } }

        public byte NumberOfTrays { get => _numberOfTrays; set { SetProperty(ref _numberOfTrays, value); } }
        public byte NumberOfCells { get => _numberOfCells; set { SetProperty(ref _numberOfCells, value); } }

        public void SetHoldingRegister(ushort[] values)
        {
            GetByteValue(values[0], out var centry, out var year);
            GetByteValue(values[1], out var month, out var date);
            GetByteValue(values[2], out var hour, out var minute);
            GetByteValue(values[3], out var second, out var dmy);
            GetByteValue(values[4], out var trays, out var cells);


            System_RTC_Centry_Type.SetValue(centry);
            System_RTC_Year_Type.SetValue(year);

            System_RTC_Month_Type.SetValue(month);
            System_RTC_Date_Type.SetValue(date);

            System_RTC_Hour_Type.SetValue(hour);
            System_RTC_Minute_Type.SetValue(minute);

            System_RTC_Second_Type.SetValue(second);

            NumberOfTryas_Type.SetValue(trays);
            NumberOfCells_Type.SetValue(cells);


            System_RTC_Centry = centry;
            System_RTC_Year = year;

            System_RTC_Month = month;
            System_RTC_Date = date;

            System_RTC_Hour = hour;
            System_RTC_Minute = minute;

            System_RTC_Second = second;

            NumberOfTrays = trays;
            NumberOfCells = cells;
        }
    }

    public sealed class BMS_RACK_BATTERY_OPERATION_STATUS : BMS_BASE
    {
        private bool _operationg_Mode_User;         //User(1), Auto(0)
        private bool _precharge_SW_On;              //On(1), Off(0) 
        private bool _discharge_SW_On;              //On(1), Off(0)
        private bool _charge_SW_On;                 //On(1), Off(0)
        //private bool _reserved1;
        //private bool _reserved2;
        private bool _cell_Balancing_On;            //On(1), Off(0)
        private bool _battery_Fault;                //Fault(1), Normal(0)
        private bool _battery_Protection_Fault;     //Fault(1), Normal(0)
        private byte _battery_Mode;                 //Standby(0), Charging(1), Discharging(2)
        private bool _battery_Ready;                //Ready(1), Not Ready(0)
        private bool _battery_Fully_Charged;        //?
        private bool _battery_Full_Discharged;      //?
        //private bool _reserved3;
        //private bool _reserved4;

        public ItemValueType<bool> Operationg_Mode_User_Type { get; } = new ItemValueType<bool>("Operationg_Mode_User");
        public ItemValueType<bool> Precharge_SW_On_Type { get; } = new ItemValueType<bool>("Precharge_SW_On");
        public ItemValueType<bool> Discharge_SW_On_Type { get; } = new ItemValueType<bool>("Discharge_SW_On");

        public ItemValueType<bool> Charge_SW_On_Type { get; } = new ItemValueType<bool>("Charge_SW_On");
        public ItemValueType<bool> Cell_Balancing_On_Type { get; } = new ItemValueType<bool>("Cell_Balancing_On");
        public ItemValueType<bool> Battery_Fault_Type { get; } = new ItemValueType<bool>("Battery_Fault");
        public ItemValueType<bool> Battery_Protection_Fault_Type { get; } = new ItemValueType<bool>("Battery_Protection_Fault");
        public ItemValueType<byte> Battery_Mode_Type { get; } = new ItemValueType<byte>("Battery_Mode");
        public ItemValueType<bool> Battery_Ready_Type { get; } = new ItemValueType<bool>("Battery_Ready");
        public ItemValueType<bool> Battery_Fully_Charged_Type { get; } = new ItemValueType<bool>("Battery_Fully_Charged");
        public ItemValueType<bool> Battery_Full_Discharged_Type { get; } = new ItemValueType<bool>("Battery_Full_Discharged");

        public bool Operationg_Mode_User { get => _operationg_Mode_User; set { SetProperty(ref _operationg_Mode_User, value); } }
        public bool Precharge_SW_On { get => _precharge_SW_On; set { SetProperty(ref _precharge_SW_On, value); } }
        public bool Discharge_SW_On { get => _discharge_SW_On; set { SetProperty(ref _discharge_SW_On, value); } }
        public bool Charge_SW_On { get => _charge_SW_On; set { SetProperty(ref _charge_SW_On, value); } }
        //public bool reserved1 { get => _reserved1; set { SetProperty(ref _reserved1, value); } }
        //public bool reserved2 { get => _reserved2; set { SetProperty(ref _reserved2, value); } }
        public bool Cell_Balancing_On { get => _cell_Balancing_On; set { SetProperty(ref _cell_Balancing_On, value); } }
        public bool Battery_Fault { get => _battery_Fault; set { SetProperty(ref _battery_Fault, value); } }
        public bool Battery_Protection_Fault { get => _battery_Protection_Fault; set { SetProperty(ref _battery_Protection_Fault, value); } }
        public byte Battery_Mode { get => _battery_Mode; set { SetProperty(ref _battery_Mode, value); } }
        public bool Battery_Ready { get => _battery_Ready; set { SetProperty(ref _battery_Ready, value); } }
        public bool Battery_Fully_Charged { get => _battery_Fully_Charged; set { SetProperty(ref _battery_Fully_Charged, value); } }
        public bool Battery_Full_Discharged { get => _battery_Full_Discharged; set { SetProperty(ref _battery_Full_Discharged, value); } }
        //public bool reserved3 { get => _reserved3; set { SetProperty(ref _reserved3, value); } }
        //public bool reserved4 { get => _reserved4; set { SetProperty(ref _reserved4, value); } }

        public void SetHoldingRegister(ushort value)
        {
            Operationg_Mode_User_Type.SetValue((value & 0x0001) != 0);
            Precharge_SW_On_Type.SetValue((value & 0x0002) != 0);
            Discharge_SW_On_Type.SetValue((value & 0x0004) != 0);
            Charge_SW_On_Type.SetValue((value & 0x0008) != 0);
            Cell_Balancing_On_Type.SetValue((value & 0x0040) != 0);
            Battery_Fault_Type.SetValue((value & 0x0080) != 0);
            Battery_Protection_Fault_Type.SetValue((value & 0x0100) != 0);
            Battery_Mode_Type.SetValue((byte)(((value & 0x0200) | (value & 0x0400)) >> 9));
            Battery_Ready_Type.SetValue((value & 0x0800) != 0);
            Battery_Fully_Charged_Type.SetValue((value & 0x1000) != 0);
            Battery_Full_Discharged_Type.SetValue((value & 0x2000) != 0);


            Operationg_Mode_User = (value & 0x0001) != 0;
            Precharge_SW_On = (value & 0x0002) != 0;
            Discharge_SW_On = (value & 0x0004) != 0;
            Charge_SW_On = (value & 0x0008) != 0;
            //reserved1 = (value & 0x0010) != 0;
            //reserved2 = (value & 0x0020) != 0;
            Cell_Balancing_On = (value & 0x0040) != 0;
            Battery_Fault = (value & 0x0080) != 0;

            Battery_Protection_Fault = (value & 0x0100) != 0;
            Battery_Mode = (byte)(((value & 0x0200) | (value & 0x0400)) >> 9);
            Battery_Ready = (value & 0x0800) != 0;

            Battery_Fully_Charged = (value & 0x1000) != 0;
            Battery_Full_Discharged = (value & 0x2000) != 0;
            //reserved3 = (value & 0x4000) != 0;
            //reserved4 = (value & 0x8000) != 0;
        }
    }

    public sealed class BMS_RACK_BATTERY_WARNING_CODE : BMS_BASE
    {
        private bool _cell_Over_Volt_Warning;
        private bool _cell_Unser_Volt_Warning;
        private bool _cell_Volt_Deviation_Warning;
        private bool _cell_Over_Temp_Warning;
        private bool _cell_Under_Temp_Warning;
        private bool _cell_Temp_Deviation_Warning;
        private bool _battery_Over_Volt_Warning;
        private bool _battery_Under_Volt_Warning;
        private bool _battery_Over_Charging_Current_Warning;
        private bool _battery_Over_Discharging_Current_Warning;
        private bool _low_SOC_Warning;
        //private bool _reserved1;
        //private bool _reserved2;
        //private bool _reserved3;
        private bool _rack_Off_Gas_Fault;
        private bool _rack_Water_Fault;

        public ItemValueType<bool> Cell_Over_Volt_Warning_Type { get; } = new ItemValueType<bool>("Cell_Over_Volt_Warning");
        public ItemValueType<bool> Cell_Unser_Volt_Warning_Type { get; } = new ItemValueType<bool>("Cell_Unser_Volt_Warning");
        public ItemValueType<bool> Cell_Volt_Deviation_Warning_Type { get; } = new ItemValueType<bool>("Cell_Volt_Deviation_Warning");
        public ItemValueType<bool> Cell_Over_Temp_Warning_Type { get; } = new ItemValueType<bool>("Cell_Over_Temp_Warning");
        public ItemValueType<bool> Cell_Under_Temp_Warning_Type { get; } = new ItemValueType<bool>("Cell_Under_Temp_Warning");
        public ItemValueType<bool> Cell_Temp_Deviation_Warning_Type { get; } = new ItemValueType<bool>("Cell_Temp_Deviation_Warning");
        public ItemValueType<bool> Battery_Over_Volt_Warning_Type { get; } = new ItemValueType<bool>("Battery_Over_Volt_Warning");
        public ItemValueType<bool> Battery_Under_Volt_Warning_Type { get; } = new ItemValueType<bool>("Battery_Under_Volt_Warning");
        public ItemValueType<bool> Battery_Over_Charging_Current_Warning_Type { get; } = new ItemValueType<bool>("Battery_Over_Charging_Current_Warning");
        public ItemValueType<bool> Battery_Over_Discharging_Current_Warning_Type { get; } = new ItemValueType<bool>("Battery_Over_Discharging_Current_Warning");
        public ItemValueType<bool> Low_SOC_Warning_Type { get; } = new ItemValueType<bool>("Low_SOC_Warning");
        public ItemValueType<bool> Rack_Off_Gas_Fault_Type { get; } = new ItemValueType<bool>("Rack_Off_Gas_Fault");
        public ItemValueType<bool> Rack_Water_Fault_Type { get; } = new ItemValueType<bool>("Rack_Water_Fault");


        public bool Cell_Over_Volt_Warning { get => _cell_Over_Volt_Warning; set { SetProperty(ref _cell_Over_Volt_Warning, value); } }
        public bool Cell_Unser_Volt_Warning { get => _cell_Unser_Volt_Warning; set { SetProperty(ref _cell_Unser_Volt_Warning, value); } }
        public bool Cell_Volt_Deviation_Warning { get => _cell_Volt_Deviation_Warning; set { SetProperty(ref _cell_Volt_Deviation_Warning, value); } }
        public bool Cell_Over_Temp_Warning { get => _cell_Over_Temp_Warning; set { SetProperty(ref _cell_Over_Temp_Warning, value); } }
        public bool Cell_Under_Temp_Warning { get => _cell_Under_Temp_Warning; set { SetProperty(ref _cell_Under_Temp_Warning, value); } }
        public bool Cell_Temp_Deviation_Warning { get => _cell_Temp_Deviation_Warning; set { SetProperty(ref _cell_Temp_Deviation_Warning, value); } }
        public bool Battery_Over_Volt_Warning { get => _battery_Over_Volt_Warning; set { SetProperty(ref _battery_Over_Volt_Warning, value); } }
        public bool Battery_Under_Volt_Warning { get => _battery_Under_Volt_Warning; set { SetProperty(ref _battery_Under_Volt_Warning, value); } }
        public bool Battery_Over_Charging_Current_Warning { get => _battery_Over_Charging_Current_Warning; set { SetProperty(ref _battery_Over_Charging_Current_Warning, value); } }
        public bool Battery_Over_Discharging_Current_Warning { get => _battery_Over_Discharging_Current_Warning; set { SetProperty(ref _battery_Over_Discharging_Current_Warning, value); } }
        public bool Low_SOC_Warning { get => _low_SOC_Warning; set { SetProperty(ref _low_SOC_Warning, value); } }
        //public bool reserved1 { get => _reserved1; set { SetProperty(ref _reserved1, value); } }
        //public bool reserved2 { get => _reserved2; set { SetProperty(ref _reserved2, value); } }
        //public bool reserved3 { get => _reserved3; set { SetProperty(ref _reserved3, value); } }        
        public bool Rack_Off_Gas_Fault { get => _rack_Off_Gas_Fault; set { SetProperty(ref _rack_Off_Gas_Fault, value); } }
        public bool Rack_Water_Fault { get => _rack_Water_Fault; set { SetProperty(ref _rack_Water_Fault, value); } }

        public void SetHoldingRegister(ushort value)
        {
            Cell_Over_Volt_Warning_Type.SetValue((value & 0x0001) != 0);
            Cell_Unser_Volt_Warning_Type.SetValue((value & 0x0002) != 0);
            Cell_Volt_Deviation_Warning_Type.SetValue((value & 0x0004) != 0);
            Cell_Over_Temp_Warning_Type.SetValue((value & 0x0008) != 0);

            Cell_Under_Temp_Warning_Type.SetValue((value & 0x0010) != 0);
            Cell_Temp_Deviation_Warning_Type.SetValue((value & 0x0020) != 0);
            Battery_Over_Volt_Warning_Type.SetValue((value & 0x0040) != 0);
            Battery_Under_Volt_Warning_Type.SetValue((value & 0x0080) != 0);

            Battery_Over_Charging_Current_Warning_Type.SetValue((value & 0x0100) != 0);
            Battery_Over_Discharging_Current_Warning_Type.SetValue((value & 0x0200) != 0);
            Low_SOC_Warning_Type.SetValue((value & 0x0400) != 0);
            Rack_Off_Gas_Fault_Type.SetValue((value & 0x4000) != 0);
            Rack_Water_Fault_Type.SetValue((value & 0x8000) != 0);



            Cell_Over_Volt_Warning = (value & 0x0001) != 0;
            Cell_Unser_Volt_Warning = (value & 0x0002) != 0;
            Cell_Volt_Deviation_Warning = (value & 0x0004) != 0;
            Cell_Over_Temp_Warning = (value & 0x0008) != 0;

            Cell_Under_Temp_Warning = (value & 0x0010) != 0;
            Cell_Temp_Deviation_Warning = (value & 0x0020) != 0;
            Battery_Over_Volt_Warning = (value & 0x0040) != 0;
            Battery_Under_Volt_Warning = (value & 0x0080) != 0;

            Battery_Over_Charging_Current_Warning = (value & 0x0100) != 0;
            Battery_Over_Discharging_Current_Warning = (value & 0x0200) != 0;
            Low_SOC_Warning = (value & 0x0400) != 0;
            //reserved1 = (value & 0x0800) != 0;

            //reserved2 = (value & 0x1000) != 0;
            //reserved3 = (value & 0x2000) != 0;            
            Rack_Off_Gas_Fault = (value & 0x4000) != 0;
            Rack_Water_Fault = (value & 0x8000) != 0;
        }
    }

    public sealed class BMS_RACK_BATTERY_PROTECTION_FAULT_CODE : BMS_BASE
    {
        private bool _cell_Over_Volt_Fault;
        private bool _cell_Under_Volt_Fault;
        private bool _cell_Volt_Deviation_Fault;
        private bool _cell_Over_Temp_Fault;
        private bool _cell_Under_Temp_Fault;
        private bool _cell_Temp_Deviation_Fault;
        private bool _battery_Over_Volt_Fault;
        private bool _battery_Under_Volt_Fault;
        private bool _battery_Over_Charging_Current_Fault;
        private bool _battery_Over_Discharging_Current_Fault;
        //private bool _reserved1;
        private bool _positive_Fuse_Blown;
        private bool _negative_Fuse_Blown;
        private bool _top_Relay_Blown;
        private bool _bot_Rlay_Blown;
        private bool _disconnect_Off;

        public ItemValueType<bool> Cell_Over_Volt_Fault_Type { get; } = new ItemValueType<bool>("Cell_Over_Volt_Fault");
        public ItemValueType<bool> Cell_Under_Volt_Fault_Type { get; } = new ItemValueType<bool>("Cell_Under_Volt_Fault");
        public ItemValueType<bool> Cell_Volt_Deviation_Fault_Type { get; } = new ItemValueType<bool>("Cell_Volt_Deviation_Fault");
        public ItemValueType<bool> Cell_Over_Temp_Fault_Type { get; } = new ItemValueType<bool>("Cell_Over_Temp_Fault");
        public ItemValueType<bool> Cell_Under_Temp_Fault_Type { get; } = new ItemValueType<bool>("Cell_Under_Temp_Fault");
        public ItemValueType<bool> Cell_Temp_Deviation_Fault_Type { get; } = new ItemValueType<bool>("Cell_Temp_Deviation_Fault");
        public ItemValueType<bool> Battery_Over_Volt_Fault_Type { get; } = new ItemValueType<bool>("Battery_Over_Volt_Fault");
        public ItemValueType<bool> Battery_Under_Volt_Fault_Type { get; } = new ItemValueType<bool>("Battery_Under_Volt_Fault");
        public ItemValueType<bool> Battery_Over_Charging_Current_Fault_Type { get; } = new ItemValueType<bool>("Battery_Over_Charging_Current_Fault");
        public ItemValueType<bool> Battery_Over_Discharging_Current_Fault_Type { get; } = new ItemValueType<bool>("Battery_Over_Discharging_Current_Fault");
        public ItemValueType<bool> Positive_Fuse_Blown_Type { get; } = new ItemValueType<bool>("Positive_Fuse_Blown");
        public ItemValueType<bool> Negative_Fuse_Blown_Type { get; } = new ItemValueType<bool>("Negative_Fuse_Blown");
        public ItemValueType<bool> Top_Relay_Blown_Type { get; } = new ItemValueType<bool>("Top_Relay_Blown");
        public ItemValueType<bool> Bot_Rlay_Blown_Type { get; } = new ItemValueType<bool>("Bot_Rlay_Blown");
        public ItemValueType<bool> Disconnect_Off_Type { get; } = new ItemValueType<bool>("Disconnect_Off");

        public bool Cell_Over_Volt_Fault { get => _cell_Over_Volt_Fault; set { SetProperty(ref _cell_Over_Volt_Fault, value); } }
        public bool Cell_Under_Volt_Fault { get => _cell_Under_Volt_Fault; set { SetProperty(ref _cell_Under_Volt_Fault, value); } }
        public bool Cell_Volt_Deviation_Fault { get => _cell_Volt_Deviation_Fault; set { SetProperty(ref _cell_Volt_Deviation_Fault, value); } }
        public bool Cell_Over_Temp_Fault { get => _cell_Over_Temp_Fault; set { SetProperty(ref _cell_Over_Temp_Fault, value); } }
        public bool Cell_Under_Temp_Fault { get => _cell_Under_Temp_Fault; set { SetProperty(ref _cell_Under_Temp_Fault, value); } }
        public bool Cell_Temp_Deviation_Fault { get => _cell_Temp_Deviation_Fault; set { SetProperty(ref _cell_Temp_Deviation_Fault, value); } }
        public bool Battery_Over_Volt_Fault { get => _battery_Over_Volt_Fault; set { SetProperty(ref _battery_Over_Volt_Fault, value); } }
        public bool Battery_Under_Volt_Fault { get => _battery_Under_Volt_Fault; set { SetProperty(ref _battery_Under_Volt_Fault, value); } }
        public bool Battery_Over_Charging_Current_Fault { get => _battery_Over_Charging_Current_Fault; set { SetProperty(ref _battery_Over_Charging_Current_Fault, value); } }
        public bool Battery_Over_Discharging_Current_Fault { get => _battery_Over_Discharging_Current_Fault; set { SetProperty(ref _battery_Over_Discharging_Current_Fault, value); } }
        //public bool reserved1 { get => _reserved1; set { SetProperty(ref _reserved1, value); } }
        public bool Positive_Fuse_Blown { get => _positive_Fuse_Blown; set { SetProperty(ref _positive_Fuse_Blown, value); } }
        public bool Negative_Fuse_Blown { get => _negative_Fuse_Blown; set { SetProperty(ref _negative_Fuse_Blown, value); } }
        public bool Top_Relay_Blown { get => _top_Relay_Blown; set { SetProperty(ref _top_Relay_Blown, value); } }
        public bool Bot_Rlay_Blown { get => _bot_Rlay_Blown; set { SetProperty(ref _bot_Rlay_Blown, value); } }
        public bool Disconnect_Off { get => _disconnect_Off; set { SetProperty(ref _disconnect_Off, value); } }

        public void SetHoldingRegister(ushort value)
        {
            Cell_Over_Volt_Fault_Type.SetValue((value & 0x0001) != 0);
            Cell_Under_Volt_Fault_Type.SetValue((value & 0x0002) != 0);
            Cell_Volt_Deviation_Fault_Type.SetValue((value & 0x0004) != 0);
            Cell_Over_Temp_Fault_Type.SetValue((value & 0x0008) != 0);
            Cell_Under_Temp_Fault_Type.SetValue((value & 0x0010) != 0);
            Cell_Temp_Deviation_Fault_Type.SetValue((value & 0x0020) != 0);
            Battery_Over_Volt_Fault_Type.SetValue((value & 0x0040) != 0);
            Battery_Under_Volt_Fault_Type.SetValue((value & 0x0080) != 0);
            Battery_Over_Charging_Current_Fault_Type.SetValue((value & 0x0100) != 0);
            Battery_Over_Discharging_Current_Fault_Type.SetValue((value & 0x0200) != 0);
            Positive_Fuse_Blown_Type.SetValue((value & 0x0800) != 0);
            Negative_Fuse_Blown_Type.SetValue((value & 0x1000) != 0);
            Top_Relay_Blown_Type.SetValue((value & 0x2000) != 0);
            Bot_Rlay_Blown_Type.SetValue((value & 0x4000) != 0);
            Disconnect_Off_Type.SetValue((value & 0x8000) != 0);



            Cell_Over_Volt_Fault = (value & 0x0001) != 0;
            Cell_Under_Volt_Fault = (value & 0x0002) != 0;
            Cell_Volt_Deviation_Fault = (value & 0x0004) != 0;
            Cell_Over_Temp_Fault = (value & 0x0008) != 0;

            Cell_Under_Temp_Fault = (value & 0x0010) != 0;
            Cell_Temp_Deviation_Fault = (value & 0x0020) != 0;
            Battery_Over_Volt_Fault = (value & 0x0040) != 0;
            Battery_Under_Volt_Fault = (value & 0x0080) != 0;

            Battery_Over_Charging_Current_Fault = (value & 0x0100) != 0;
            Battery_Over_Discharging_Current_Fault = (value & 0x0200) != 0;
            //reserved1 = (value & 0x0400) != 0;
            Positive_Fuse_Blown = (value & 0x0800) != 0;

            Negative_Fuse_Blown = (value & 0x1000) != 0;
            Top_Relay_Blown = (value & 0x2000) != 0;
            Bot_Rlay_Blown = (value & 0x4000) != 0;
            Disconnect_Off = (value & 0x8000) != 0;
        }
    }

    public sealed class BMS_RACK_TRAY_COMM_FAULT_CODE : BMS_BASE
    {
        private bool _tray_1_Fault;
        private bool _tray_2_Fault;
        private bool _tray_3_Fault;
        private bool _tray_4_Fault;
        private bool _tray_5_Fault;
        private bool _tray_6_Fault;
        private bool _tray_7_Fault;
        private bool _tray_8_Fault;
        private bool _tray_9_Fault;
        private bool _tray_10_Fault;
        //private bool _reserved11;
        //private bool _reserved12;
        //private bool _reserved13;
        //private bool _reserved14;
        //private bool _reserved15;
        //private bool _reserved16;
        private bool _tray_11_Fault;
        private bool _tray_12_Fault;
        private bool _tray_13_Fault;
        private bool _tray_14_Fault;
        private bool _tray_15_Fault;
        private bool _tray_16_Fault;
        private bool _tray_17_Fault;
        private bool _tray_18_Fault;
        private bool _tray_19_Fault;
        private bool _tray_20_Fault;
        //private bool _reserved21;
        //private bool _reserved22;
        //private bool _reserved23;
        //private bool _reserved24;
        //private bool _reserved25;
        //private bool _reserved26;

        public ItemValueType<bool> Tray_1_Fault_Type { get; } = new ItemValueType<bool>("Tray_1_Fault");
        public ItemValueType<bool> Tray_2_Fault_Type { get; } = new ItemValueType<bool>("Tray_2_Fault");
        public ItemValueType<bool> Tray_3_Fault_Type { get; } = new ItemValueType<bool>("Tray_3_Fault");
        public ItemValueType<bool> Tray_4_Fault_Type { get; } = new ItemValueType<bool>("Tray_4_Fault");
        public ItemValueType<bool> Tray_5_Fault_Type { get; } = new ItemValueType<bool>("Tray_5_Fault");
        public ItemValueType<bool> Tray_6_Fault_Type { get; } = new ItemValueType<bool>("Tray_6_Fault");
        public ItemValueType<bool> Tray_7_Fault_Type { get; } = new ItemValueType<bool>("Tray_7_Fault");
        public ItemValueType<bool> Tray_8_Fault_Type { get; } = new ItemValueType<bool>("Tray_8_Fault");
        public ItemValueType<bool> Tray_9_Fault_Type { get; } = new ItemValueType<bool>("Tray_9_Fault");
        public ItemValueType<bool> Tray_10_Fault_Type { get; } = new ItemValueType<bool>("Tray_10_Fault");
        public ItemValueType<bool> Tray_11_Fault_Type { get; } = new ItemValueType<bool>("Tray_11_Fault");
        public ItemValueType<bool> Tray_12_Fault_Type { get; } = new ItemValueType<bool>("Tray_12_Fault");
        public ItemValueType<bool> Tray_13_Fault_Type { get; } = new ItemValueType<bool>("Tray_13_Fault");
        public ItemValueType<bool> Tray_14_Fault_Type { get; } = new ItemValueType<bool>("Tray_14_Fault");
        public ItemValueType<bool> Tray_15_Fault_Type { get; } = new ItemValueType<bool>("Tray_15_Fault");
        public ItemValueType<bool> Tray_16_Fault_Type { get; } = new ItemValueType<bool>("Tray_16_Fault");
        public ItemValueType<bool> Tray_17_Fault_Type { get; } = new ItemValueType<bool>("Tray_17_Fault");
        public ItemValueType<bool> Tray_18_Fault_Type { get; } = new ItemValueType<bool>("Tray_18_Fault");
        public ItemValueType<bool> Tray_19_Fault_Type { get; } = new ItemValueType<bool>("Tray_19_Fault");
        public ItemValueType<bool> Tray_20_Fault_Type { get; } = new ItemValueType<bool>("Tray_20_Fault");


        public bool Tray_1_Fault { get => _tray_1_Fault; set { SetProperty(ref _tray_1_Fault, value); } }
        public bool Tray_2_Fault { get => _tray_2_Fault; set { SetProperty(ref _tray_2_Fault, value); } }
        public bool Tray_3_Fault { get => _tray_3_Fault; set { SetProperty(ref _tray_3_Fault, value); } }
        public bool Tray_4_Fault { get => _tray_4_Fault; set { SetProperty(ref _tray_4_Fault, value); } }
        public bool Tray_5_Fault { get => _tray_5_Fault; set { SetProperty(ref _tray_5_Fault, value); } }
        public bool Tray_6_Fault { get => _tray_6_Fault; set { SetProperty(ref _tray_6_Fault, value); } }
        public bool Tray_7_Fault { get => _tray_7_Fault; set { SetProperty(ref _tray_7_Fault, value); } }
        public bool Tray_8_Fault { get => _tray_8_Fault; set { SetProperty(ref _tray_8_Fault, value); } }
        public bool Tray_9_Fault { get => _tray_9_Fault; set { SetProperty(ref _tray_9_Fault, value); } }
        public bool Tray_10_Fault { get => _tray_10_Fault; set { SetProperty(ref _tray_10_Fault, value); } }
        //public bool reserved11 { get => _reserved11; set { SetProperty(ref _reserved11, value); } }
        //public bool reserved12 { get => _reserved12; set { SetProperty(ref _reserved12, value); } }
        //public bool reserved13 { get => _reserved13; set { SetProperty(ref _reserved13, value); } }
        //public bool reserved14 { get => _reserved14; set { SetProperty(ref _reserved14, value); } }
        //public bool reserved15 { get => _reserved15; set { SetProperty(ref _reserved15, value); } }
        //public bool reserved16 { get => _reserved16; set { SetProperty(ref _reserved16, value); } }
        public bool Tray_11_Fault { get => _tray_11_Fault; set { SetProperty(ref _tray_11_Fault, value); } }
        public bool Tray_12_Fault { get => _tray_12_Fault; set { SetProperty(ref _tray_12_Fault, value); } }
        public bool Tray_13_Fault { get => _tray_13_Fault; set { SetProperty(ref _tray_13_Fault, value); } }
        public bool Tray_14_Fault { get => _tray_14_Fault; set { SetProperty(ref _tray_14_Fault, value); } }
        public bool Tray_15_Fault { get => _tray_15_Fault; set { SetProperty(ref _tray_15_Fault, value); } }
        public bool Tray_16_Fault { get => _tray_16_Fault; set { SetProperty(ref _tray_16_Fault, value); } }
        public bool Tray_17_Fault { get => _tray_17_Fault; set { SetProperty(ref _tray_17_Fault, value); } }
        public bool Tray_18_Fault { get => _tray_18_Fault; set { SetProperty(ref _tray_18_Fault, value); } }
        public bool Tray_19_Fault { get => _tray_19_Fault; set { SetProperty(ref _tray_19_Fault, value); } }
        public bool Tray_20_Fault { get => _tray_20_Fault; set { SetProperty(ref _tray_20_Fault, value); } }
        //public bool reserved21 { get => _reserved21; set { SetProperty(ref _reserved21, value); } }
        //public bool reserved22 { get => _reserved22; set { SetProperty(ref _reserved22, value); } }
        //public bool reserved23 { get => _reserved23; set { SetProperty(ref _reserved23, value); } }
        //public bool reserved24 { get => _reserved24; set { SetProperty(ref _reserved24, value); } }
        //public bool reserved25 { get => _reserved25; set { SetProperty(ref _reserved25, value); } }
        //public bool reserved26 { get => _reserved26; set { SetProperty(ref _reserved26, value); } }

        public void SetHoldingRegister(ushort[] values)
        {
            //var sht = 0;
            //Tray_1_Fault = (values[0] & (0x01 << sht++)) == 1;
            //Tray_2_Fault = (values[0] & (0x01 << sht++)) == 1;
            //Tray_3_Fault = (values[0] & (0x01 << sht++)) == 1;
            //Tray_4_Fault = (values[0] & (0x01 << sht++)) == 1;
            //Tray_5_Fault = (values[0] & (0x01 << sht++)) == 1;
            //Tray_6_Fault = (values[0] & (0x01 << sht++)) == 1;
            //Tray_7_Fault = (values[0] & (0x01 << sht++)) == 1;
            //Tray_8_Fault = (values[0] & (0x01 << sht++)) == 1;
            //Tray_9_Fault = (values[0] & (0x01 << sht++)) == 1;
            //Tray_10_Fault = (values[0] & (0x01 << sht++)) == 1;
            //reserved11 = (values[0] & (0x01 << sht++)) == 1;
            //reserved12 = (values[0] & (0x01 << sht++)) == 1;
            //reserved13 = (values[0] & (0x01 << sht++)) == 1;
            //reserved14 = (values[0] & (0x01 << sht++)) == 1;
            //reserved15 = (values[0] & (0x01 << sht++)) == 1;
            //reserved16 = (values[0] & (0x01 << sht++)) == 1;

            //sht = 0;
            //Tray_11_Fault = (values[1] & (0x01 << sht++)) == 1;
            //Tray_12_Fault = (values[1] & (0x01 << sht++)) == 1;
            //Tray_13_Fault = (values[1] & (0x01 << sht++)) == 1;
            //Tray_14_Fault = (values[1] & (0x01 << sht++)) == 1;
            //Tray_15_Fault = (values[1] & (0x01 << sht++)) == 1;
            //Tray_16_Fault = (values[1] & (0x01 << sht++)) == 1;
            //Tray_17_Fault = (values[1] & (0x01 << sht++)) == 1;
            //Tray_18_Fault = (values[1] & (0x01 << sht++)) == 1;
            //Tray_19_Fault = (values[1] & (0x01 << sht++)) == 1;
            //Tray_20_Fault = (values[1] & (0x01 << sht++)) == 1;
            //reserved21 = (values[1] & (0x01 << sht++)) == 1;
            //reserved22 = (values[1] & (0x01 << sht++)) == 1;
            //reserved23 = (values[1] & (0x01 << sht++)) == 1;
            //reserved24 = (values[1] & (0x01 << sht++)) == 1;
            //reserved25 = (values[1] & (0x01 << sht++)) == 1;
            //reserved26 = (values[1] & (0x01 << sht++)) == 1;

            Tray_1_Fault_Type.SetValue((values[0] & 0x0001) != 0);
            Tray_2_Fault_Type.SetValue((values[0] & 0x0002) != 0);
            Tray_3_Fault_Type.SetValue((values[0] & 0x0004) != 0);
            Tray_4_Fault_Type.SetValue((values[0] & 0x0008) != 0);
            Tray_5_Fault_Type.SetValue((values[0] & 0x0010) != 0);
            Tray_6_Fault_Type.SetValue((values[0] & 0x0020) != 0);
            Tray_7_Fault_Type.SetValue((values[0] & 0x0040) != 0);
            Tray_8_Fault_Type.SetValue((values[0] & 0x0080) != 0);
            Tray_9_Fault_Type.SetValue((values[0] & 0x0100) != 0);
            Tray_10_Fault_Type.SetValue((values[0] & 0x0200) != 0);
            Tray_11_Fault_Type.SetValue((values[1] & 0x0001) != 0);
            Tray_12_Fault_Type.SetValue((values[1] & 0x0002) != 0);
            Tray_13_Fault_Type.SetValue((values[1] & 0x0004) != 0);
            Tray_14_Fault_Type.SetValue((values[1] & 0x0008) != 0);
            Tray_15_Fault_Type.SetValue((values[1] & 0x0010) != 0);
            Tray_16_Fault_Type.SetValue((values[1] & 0x0020) != 0);
            Tray_17_Fault_Type.SetValue((values[1] & 0x0040) != 0);
            Tray_18_Fault_Type.SetValue((values[1] & 0x0080) != 0);
            Tray_19_Fault_Type.SetValue((values[1] & 0x0100) != 0);
            Tray_20_Fault_Type.SetValue((values[1] & 0x0200) != 0);



            Tray_1_Fault = (values[0] & 0x0001) != 0;
            Tray_2_Fault = (values[0] & 0x0002) != 0;
            Tray_3_Fault = (values[0] & 0x0004) != 0;
            Tray_4_Fault = (values[0] & 0x0008) != 0;

            Tray_5_Fault = (values[0] & 0x0010) != 0;
            Tray_6_Fault = (values[0] & 0x0020) != 0;
            Tray_7_Fault = (values[0] & 0x0040) != 0;
            Tray_8_Fault = (values[0] & 0x0080) != 0;

            Tray_9_Fault = (values[0] & 0x0100) != 0;
            Tray_10_Fault = (values[0] & 0x0200) != 0;
            //reserved11 = (values[0] & 0x0400) != 0;
            //reserved12 = (values[0] & 0x0800) != 0;

            //reserved13 = (values[0] & 0x1000) != 0;
            //reserved14 = (values[0] & 0x2000) != 0;
            //reserved15 = (values[0] & 0x4000) != 0;
            //reserved16 = (values[0] & 0x8000) != 0;

            Tray_11_Fault = (values[1] & 0x0001) != 0;
            Tray_12_Fault = (values[1] & 0x0002) != 0;
            Tray_13_Fault = (values[1] & 0x0004) != 0;
            Tray_14_Fault = (values[1] & 0x0008) != 0;

            Tray_15_Fault = (values[1] & 0x0010) != 0;
            Tray_16_Fault = (values[1] & 0x0020) != 0;
            Tray_17_Fault = (values[1] & 0x0040) != 0;
            Tray_18_Fault = (values[1] & 0x0080) != 0;

            Tray_19_Fault = (values[1] & 0x0100) != 0;
            Tray_20_Fault = (values[1] & 0x0200) != 0;
            //reserved21 = (values[1] & 0x0400) != 0;
            //reserved22 = (values[1] & 0x0800) != 0;

            //reserved23 = (values[1] & 0x1000) != 0;
            //reserved24 = (values[1] & 0x2000) != 0;
            //reserved25 = (values[1] & 0x4000) != 0;
            //reserved26 = (values[1] & 0x8000) != 0;
        }
    }

    public sealed class BMS_RACK_VALUE_DATAS : BMS_BASE
    {
        private double _rack_Soc;
        private double _rack_Soh;
        private double _rack_Bat_Volt;
        private double _rack_Bat_Current;
        private double _rack_Ups_Volt;
        private ushort _reserved;
        private uint _rack_Charging_Power;
        private uint _rack_Discharging_Power;
        private double _rack_Average_Cell_Volt;
        private double _rack_Max_Cell_Volt;
        private double _rack_Min_Cell_Volt;
        private double _rack_Difference_Cell_Volt;
        //private ushort _rack_Max_Volt_Tray_Cell_Location;
        private byte _rack_Max_Volt_Tray_Location;
        private byte _rack_Max_Volt_Cell_Location;

        //private ushort _rack_Min_Volt_Tray_Cell_Location;
        private byte _rack_Min_Volt_Tray_Location;
        private byte _rack_Min_Volt_Cell_Location;

        private double _rack_Average_Cell_Temp;
        private double _rack_Max_Cell_Temp;
        private double _rack_Min_Cell_Temp;
        private double _rack_Difference_Cell_Temp;
        //private ushort _rack_Max_Temp_Tray_Sensor_Location;
        private byte _rack_Max_Temp_Tray_Location;
        private byte _rack_Max_Temp_Sensor_Location;

        //private ushort _rack_Min_Temp_Tray_Sensor_Location;
        private byte _rack_Min_Temp_Tray_Location;
        private byte _rack_Min_Temp_Sensor_Location;

        private uint _rack_Charging_Current_Hour;
        private uint _rack_Discharging_Current_Hour;

        public ItemValueType<double> Rack_Soc_Type { get; } = new ItemValueType<double>("Rack_Soc");
        public ItemValueType<double> Rack_Soh_Type { get; } = new ItemValueType<double>("Rack_Soh");
        public ItemValueType<double> Rack_Bat_Volt_Type { get; } = new ItemValueType<double>("Rack_Bat_Volt");
        public ItemValueType<double> Rack_Bat_Current_Type { get; } = new ItemValueType<double>("Rack_Bat_Current");
        public ItemValueType<double> Rack_Ups_Volt_Type { get; } = new ItemValueType<double>("Rack_Ups_Volt");
        //public ItemValueType<ushort> Reserved_Type { get; } = new ItemValueType<ushort>("Reserved");
        public ItemValueType<uint> Rack_Charging_Power_Type { get; } = new ItemValueType<uint>("Rack_Charging_Power");
        public ItemValueType<uint> Rack_Discharging_Power_Type { get; } = new ItemValueType<uint>("Rack_Discharging_Power");
        public ItemValueType<double> Rack_Average_Cell_Volt_Type { get; } = new ItemValueType<double>("Rack_Average_Cell_Volt");
        public ItemValueType<double> Rack_Max_Cell_Volt_Type { get; } = new ItemValueType<double>("Rack_Max_Cell_Volt");
        public ItemValueType<double> Rack_Min_Cell_Volt_Type { get; } = new ItemValueType<double>("Rack_Min_Cell_Volt");
        public ItemValueType<double> Rack_Difference_Cell_Volt_Type { get; } = new ItemValueType<double>("Rack_Difference_Cell_Volt");
        //public ItemValueType<ushort> Rack_Max_Volt_Tray_Cell_Location_Type { get; } = new ItemValueType<ushort>("Rack_Max_Volt_Tray_Cell_Location");
        public ItemValueType<byte> Rack_Max_Volt_Tray_Location_Type { get; } = new ItemValueType<byte>("Rack_Max_Volt_Tray_Location");
        public ItemValueType<byte> Rack_Max_Volt_Cell_Location_Type { get; } = new ItemValueType<byte>("Rack_Max_Volt_Cell_Location");
        //public ItemValueType<ushort> Rack_Min_Volt_Tray_Cell_Location_Type { get; } = new ItemValueType<ushort>("Rack_Min_Volt_Tray_Cell_Location");
        public ItemValueType<byte> Rack_Min_Volt_Tray_Location_Type { get; } = new ItemValueType<byte>("Rack_Min_Volt_Tray_Location");
        public ItemValueType<byte> Rack_Min_Volt_Cell_Location_Type { get; } = new ItemValueType<byte>("Rack_Min_Volt_Cell_Location");
        public ItemValueType<double> Rack_Average_Cell_Temp_Type { get; } = new ItemValueType<double>("Rack_Average_Cell_Temp");
        public ItemValueType<double> Rack_Max_Cell_Temp_Type { get; } = new ItemValueType<double>("Rack_Max_Cell_Temp");
        public ItemValueType<double> Rack_Min_Cell_Temp_Type { get; } = new ItemValueType<double>("Rack_Min_Cell_Temp");
        public ItemValueType<double> Rack_Difference_Cell_Temp_Type { get; } = new ItemValueType<double>("Rack_Difference_Cell_Temp");
        //public ItemValueType<ushort> Rack_Max_Temp_Tray_Sensor_Location_Type { get; } = new ItemValueType<ushort>("Rack_Max_Temp_Tray_Sensor_Location");
        public ItemValueType<byte> Rack_Max_Temp_Tray_Location_Type { get; } = new ItemValueType<byte>("Rack_Max_Temp_Tray_Location");
        public ItemValueType<byte> Rack_Max_Temp_Sensor_Location_Type { get; } = new ItemValueType<byte>("Rack_Max_Temp_Sensor_Location");
        //public ItemValueType<ushort> Rack_Min_Temp_Tray_Sensor_Location_Type { get; } = new ItemValueType<ushort>("Rack_Min_Temp_Tray_Sensor_Location");
        public ItemValueType<byte> Rack_Min_Temp_Tray_Location_Type { get; } = new ItemValueType<byte>("Rack_Min_Temp_Tray_Location");
        public ItemValueType<byte> Rack_Min_Temp_Sensor_Location_Type { get; } = new ItemValueType<byte>("Rack_Min_Temp_Sensor_Location");
        public ItemValueType<uint> Rack_Charging_Current_Hour_Type { get; } = new ItemValueType<uint>("Rack_Charging_Current_Hour");
        public ItemValueType<uint> Rack_Discharging_Current_Hour_Type { get; } = new ItemValueType<uint>("Rack_Discharging_Current_Hour");

        public double Rack_Soc { get => _rack_Soc; 
            set { SetProperty(ref _rack_Soc, value); } } //  * 0.1
        public double Rack_Soh { get => _rack_Soh; set { SetProperty(ref _rack_Soh, value); } } // * 0.1
        public double Rack_Bat_Volt { get => _rack_Bat_Volt; set { SetProperty(ref _rack_Bat_Volt, value); } } // * 0.1
        public double Rack_Bat_Current { get => _rack_Bat_Current; set { SetProperty(ref _rack_Bat_Current, value); } } // * 0.1
        public double Rack_Ups_Volt { get => _rack_Ups_Volt; set { SetProperty(ref _rack_Ups_Volt, value); } }  // * 0.1
        public ushort Reserved { get => _reserved; set { SetProperty(ref _reserved, value); } }
        public uint Rack_Charging_Power { get => _rack_Charging_Power; set { SetProperty(ref _rack_Charging_Power, value); } }
        public uint Rack_Discharging_Power { get => _rack_Discharging_Power; set { SetProperty(ref _rack_Discharging_Power, value); } }
        public double Rack_Average_Cell_Volt { get => _rack_Average_Cell_Volt; set { SetProperty(ref _rack_Average_Cell_Volt, value); } } // * 0.001
        public double Rack_Max_Cell_Volt { get => _rack_Max_Cell_Volt; set { SetProperty(ref _rack_Max_Cell_Volt, value); } }   // * 0.001
        public double Rack_Min_Cell_Volt { get => _rack_Min_Cell_Volt; set { SetProperty(ref _rack_Min_Cell_Volt, value); } }   // * 0.001
        public double Rack_Difference_Cell_Volt { get => _rack_Difference_Cell_Volt; set { SetProperty(ref _rack_Difference_Cell_Volt, value); } }  // * 0.001
        //public ushort Rack_Max_Volt_Tray_Cell_Location { get => _rack_Max_Volt_Tray_Cell_Location; set { SetProperty(ref _rack_Max_Volt_Tray_Cell_Location, value); } }
        public byte Rack_Max_Volt_Tray_Location { get => _rack_Max_Volt_Tray_Location; set { SetProperty(ref _rack_Max_Volt_Tray_Location, value); } }
        public byte Rack_Max_Volt_Cell_Location { get => _rack_Max_Volt_Cell_Location; set { SetProperty(ref _rack_Max_Volt_Cell_Location, value); } }

        //public ushort Rack_Min_Volt_Tray_Cell_Location { get => _rack_Min_Volt_Tray_Cell_Location; set { SetProperty(ref _rack_Min_Volt_Tray_Cell_Location, value); } }
        public byte Rack_Min_Volt_Tray_Location { get => _rack_Min_Volt_Tray_Location; set { SetProperty(ref _rack_Min_Volt_Tray_Location, value); } }
        public byte Rack_Min_Volt_Cell_Location { get => _rack_Min_Volt_Cell_Location; set { SetProperty(ref _rack_Min_Volt_Cell_Location, value); } }

        public double Rack_Average_Cell_Temp { get => _rack_Average_Cell_Temp; set { SetProperty(ref _rack_Average_Cell_Temp, value); } }   // * 0.1
        public double Rack_Max_Cell_Temp { get => _rack_Max_Cell_Temp; set { SetProperty(ref _rack_Max_Cell_Temp, value); } }   // * 0.1
        public double Rack_Min_Cell_Temp { get => _rack_Min_Cell_Temp; set { SetProperty(ref _rack_Min_Cell_Temp, value); } }   // * 0.1
        public double Rack_Difference_Cell_Temp { get => _rack_Difference_Cell_Temp; set { SetProperty(ref _rack_Difference_Cell_Temp, value); } }  // * 0.1
        //public ushort Rack_Max_Temp_Tray_Sensor_Location { get => _rack_Max_Temp_Tray_Sensor_Location; set { SetProperty(ref _rack_Max_Temp_Tray_Sensor_Location, value); } }
        public byte Rack_Max_Temp_Tray_Location { get => _rack_Max_Temp_Tray_Location; set { SetProperty(ref _rack_Max_Temp_Tray_Location, value); } }
        public byte Rack_Max_Temp_Sensor_Location { get => _rack_Max_Temp_Sensor_Location; set { SetProperty(ref _rack_Max_Temp_Sensor_Location, value); } }

        //public ushort Rack_Min_Temp_Tray_Sensor_Location { get => _rack_Min_Temp_Tray_Sensor_Location; set { SetProperty(ref _rack_Min_Temp_Tray_Sensor_Location, value); } }
        public byte Rack_Min_Temp_Tray_Location { get => _rack_Min_Temp_Tray_Location; set { SetProperty(ref _rack_Min_Temp_Tray_Location, value); } }
        public byte Rack_Min_Temp_Sensor_Location { get => _rack_Min_Temp_Sensor_Location; set { SetProperty(ref _rack_Min_Temp_Sensor_Location, value); } }


        public uint Rack_Charging_Current_Hour { get => _rack_Charging_Current_Hour; set { SetProperty(ref _rack_Charging_Current_Hour, value); } }
        public uint Rack_Discharging_Current_Hour { get => _rack_Discharging_Current_Hour; set { SetProperty(ref _rack_Discharging_Current_Hour, value); } }

        public void SetHoldingRegister(ushort[] values)
        {
            Rack_Soc_Type.SetValue((short)values[0] * 0.1);
            Rack_Soh_Type.SetValue((short)values[1] * 0.1);
            Rack_Bat_Volt_Type.SetValue((short)values[2] * 0.1);
            Rack_Bat_Current_Type.SetValue((short)values[3] * 0.1);
            Rack_Ups_Volt_Type.SetValue((short)values[4] * 0.1);
            Rack_Charging_Power_Type.SetValue(MergeTwoUShort(values[6], values[7]));
            Rack_Discharging_Power_Type.SetValue(MergeTwoUShort(values[8], values[9]));
            Rack_Average_Cell_Volt_Type.SetValue((short)values[10] * 0.001);
            Rack_Max_Cell_Volt_Type.SetValue((short)values[11] * 0.001);
            Rack_Min_Cell_Volt_Type.SetValue((short)values[12] * 0.001);
            Rack_Difference_Cell_Volt_Type.SetValue((short)values[13] * 0.001);

            byte v1, v2;
            GetByteValue(values[14], out v1, out v2);
            Rack_Max_Volt_Tray_Location_Type.SetValue(v1);
            Rack_Max_Volt_Cell_Location_Type.SetValue(v2);

            GetByteValue(values[15], out v1, out v2);
            Rack_Min_Volt_Tray_Location_Type.SetValue(v1);
            Rack_Min_Volt_Cell_Location_Type.SetValue(v2);

            Rack_Average_Cell_Temp_Type.SetValue((short)values[16] * 0.1);
            Rack_Max_Cell_Temp_Type.SetValue((short)values[17] * 0.1);
            Rack_Min_Cell_Temp_Type.SetValue((short)values[18] * 0.1);
            Rack_Difference_Cell_Temp_Type.SetValue((short)values[19] * 0.1);

            GetByteValue(values[20], out v1, out v2);
            Rack_Max_Temp_Tray_Location_Type.SetValue(v1);
            Rack_Max_Temp_Sensor_Location_Type.SetValue(v2);

            GetByteValue(values[21], out v1, out v2);
            Rack_Min_Temp_Tray_Location_Type.SetValue(v1);
            Rack_Min_Temp_Sensor_Location_Type.SetValue(v2);

            Rack_Charging_Current_Hour_Type.SetValue(MergeTwoUShort(values[22], values[23]));
            Rack_Discharging_Current_Hour_Type.SetValue(MergeTwoUShort(values[24], values[25]));


            Rack_Soc = values[0] * 0.1;
            Rack_Soh = values[1] * 0.1;
            Rack_Bat_Volt = values[2] * 0.1;
            Rack_Bat_Current = values[3] * 0.1;
            Rack_Ups_Volt = values[4] * 0.1;
            _reserved = values[5];

            Rack_Charging_Power = MergeTwoUShort(values[6], values[7]);
            Rack_Discharging_Power = MergeTwoUShort(values[8], values[9]);

            Rack_Average_Cell_Volt = values[10] * 0.001;
            Rack_Max_Cell_Volt = values[11] * 0.001;
            Rack_Min_Cell_Volt = values[12] * 0.001;
            Rack_Difference_Cell_Volt = values[13] * 0.001;

            GetByteValue(values[14], out v1, out v2);
            Rack_Max_Volt_Tray_Location = v1;
            Rack_Max_Volt_Cell_Location = v2;

            GetByteValue(values[15], out v1, out v2);
            Rack_Min_Volt_Tray_Location = v1;
            Rack_Min_Volt_Cell_Location = v2;

            Rack_Average_Cell_Temp = values[16] * 0.1;
            Rack_Max_Cell_Temp = values[17] * 0.1;
            Rack_Min_Cell_Temp = values[18] * 0.1;
            Rack_Difference_Cell_Temp = values[19] * 0.1;

            GetByteValue(values[20], out v1, out v2);
            Rack_Max_Temp_Tray_Location = v1;
            Rack_Max_Temp_Sensor_Location = v2;

            GetByteValue(values[21], out v1, out v2);
            Rack_Min_Temp_Tray_Location = v1;
            Rack_Min_Temp_Sensor_Location = v2;

            Rack_Charging_Current_Hour = MergeTwoUShort(values[22], values[23]);
            Rack_Discharging_Current_Hour = MergeTwoUShort(values[24], values[25]);
        }
    }
}
