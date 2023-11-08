using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB_ESS.Model
{
    public class BMS_TRAY_LIST : IEnumerable<BMS_TRAY_MAIN>
    {
        private ObservableCollection<BMS_TRAY_MAIN> _trayList = new ObservableCollection<BMS_TRAY_MAIN>();

        public BMS_TRAY_LIST(int trayCount)
        {
            for (int i = 1; i <= trayCount; i++)
                _trayList.Add(new BMS_TRAY_MAIN() { TrayNum = i });
        }

        public BMS_TRAY_MAIN this[int trayNum] => _trayList.FirstOrDefault(tr => tr.TrayNum == trayNum);

        public IEnumerator<BMS_TRAY_MAIN> GetEnumerator()
        {
            return ((IEnumerable<BMS_TRAY_MAIN>)_trayList).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_trayList).GetEnumerator();
        }
    }

    public sealed class BMS_TRAY_MAIN : BMS_BASE
    {
        private int _trayNum = -1;
        public int TrayNum 
        { 
            get => _trayNum; 
            set { SetProperty(ref _trayNum, value); } 
        }

        public BMS_TRAY_CELL_VOLT BmsTrayCellVolt { get; } = new BMS_TRAY_CELL_VOLT();
        public BMS_TRAY_TEMPERATURE BmsTrayTemperatrue { get; } = new BMS_TRAY_TEMPERATURE();
        public BMS_TRAY_WARNING_CODE BmsTrayWarningCode { get; } = new BMS_TRAY_WARNING_CODE();
        public BMS_TRAY_FAULT_CODE BmsTrayFaultCode { get; } = new BMS_TRAY_FAULT_CODE();
        public BMS_TRAY_VALUES BmsTrayValues { get; } = new BMS_TRAY_VALUES();

        /// <summary>
        /// Holding Register - 30ea
        /// </summary>
        /// <param name="values"></param>
        public void SetHoldingRegister(ushort[] values)
        {
            System.Span<ushort> tmp = new System.Span<ushort>(values);

            BmsTrayCellVolt.SetHoldingRegister(tmp.Slice(0, 12).ToArray());
            BmsTrayTemperatrue.SetHoldingRegister(tmp.Slice(12, 5).ToArray());
            BmsTrayWarningCode.SetHoldingRegister(values[17]);
            BmsTrayFaultCode.SetHoldingRegister(values[18]);
            BmsTrayValues.SetHoldingRegister(tmp.Slice(19, 9).ToArray());
        }

        public List<IItemValueType> GetItemValueTypeProperties()
        {
            var items = new List<IItemValueType>();

            items.AddRange(BmsTrayCellVolt.GetItemValueTypePropertyList());
            items.AddRange(BmsTrayTemperatrue.GetItemValueTypePropertyList());
            items.AddRange(BmsTrayWarningCode.GetItemValueTypePropertyList());
            items.AddRange(BmsTrayFaultCode.GetItemValueTypePropertyList());
            items.AddRange(BmsTrayValues.GetItemValueTypePropertyList());

            return items;
        }

        
    }

    public sealed class BMS_TRAY_CELL_VOLT : BMS_BASE
    {
        private double _tray_Cell_Voltage_01;
        private double _tray_Cell_Voltage_02;
        private double _tray_Cell_Voltage_03;
        private double _tray_Cell_Voltage_04;
        private double _tray_Cell_Voltage_05;
        private double _tray_Cell_Voltage_06;
        private double _tray_Cell_Voltage_07;
        private double _tray_Cell_Voltage_08;
        private double _tray_Cell_Voltage_09;
        private double _tray_Cell_Voltage_10;
        private double _tray_Cell_Voltage_11;
        private double _tray_Cell_Voltage_12;

        public double Tray_Cell_Voltage_01 { get => _tray_Cell_Voltage_01; set { SetProperty(ref _tray_Cell_Voltage_01, value); } }
        public double Tray_Cell_Voltage_02 { get => _tray_Cell_Voltage_02; set { SetProperty(ref _tray_Cell_Voltage_02, value); } }
        public double Tray_Cell_Voltage_03 { get => _tray_Cell_Voltage_03; set { SetProperty(ref _tray_Cell_Voltage_03, value); } }
        public double Tray_Cell_Voltage_04 { get => _tray_Cell_Voltage_04; set { SetProperty(ref _tray_Cell_Voltage_04, value); } }
        public double Tray_Cell_Voltage_05 { get => _tray_Cell_Voltage_05; set { SetProperty(ref _tray_Cell_Voltage_05, value); } }
        public double Tray_Cell_Voltage_06 { get => _tray_Cell_Voltage_06; set { SetProperty(ref _tray_Cell_Voltage_06, value); } }
        public double Tray_Cell_Voltage_07 { get => _tray_Cell_Voltage_07; set { SetProperty(ref _tray_Cell_Voltage_07, value); } }
        public double Tray_Cell_Voltage_08 { get => _tray_Cell_Voltage_08; set { SetProperty(ref _tray_Cell_Voltage_08, value); } }
        public double Tray_Cell_Voltage_09 { get => _tray_Cell_Voltage_09; set { SetProperty(ref _tray_Cell_Voltage_09, value); } }
        public double Tray_Cell_Voltage_10 { get => _tray_Cell_Voltage_10; set { SetProperty(ref _tray_Cell_Voltage_10, value); } }
        public double Tray_Cell_Voltage_11 { get => _tray_Cell_Voltage_11; set { SetProperty(ref _tray_Cell_Voltage_11, value); } }
        public double Tray_Cell_Voltage_12 { get => _tray_Cell_Voltage_12; set { SetProperty(ref _tray_Cell_Voltage_12, value); } }

        public ItemValueType<double> Tray_Cell_Voltage_01_Type { get; } = new ItemValueType<double>("Tray_Cell_Voltage_01");
        public ItemValueType<double> Tray_Cell_Voltage_02_Type { get; } = new ItemValueType<double>("Tray_Cell_Voltage_02");
        public ItemValueType<double> Tray_Cell_Voltage_03_Type { get; } = new ItemValueType<double>("Tray_Cell_Voltage_03");
        public ItemValueType<double> Tray_Cell_Voltage_04_Type { get; } = new ItemValueType<double>("Tray_Cell_Voltage_04");
        public ItemValueType<double> Tray_Cell_Voltage_05_Type { get; } = new ItemValueType<double>("Tray_Cell_Voltage_05");
        public ItemValueType<double> Tray_Cell_Voltage_06_Type { get; } = new ItemValueType<double>("Tray_Cell_Voltage_06");
        public ItemValueType<double> Tray_Cell_Voltage_07_Type { get; } = new ItemValueType<double>("Tray_Cell_Voltage_07");
        public ItemValueType<double> Tray_Cell_Voltage_08_Type { get; } = new ItemValueType<double>("Tray_Cell_Voltage_08");
        public ItemValueType<double> Tray_Cell_Voltage_09_Type { get; } = new ItemValueType<double>("Tray_Cell_Voltage_09");
        public ItemValueType<double> Tray_Cell_Voltage_10_Type { get; } = new ItemValueType<double>("Tray_Cell_Voltage_10");
        public ItemValueType<double> Tray_Cell_Voltage_11_Type { get; } = new ItemValueType<double>("Tray_Cell_Voltage_11");
        public ItemValueType<double> Tray_Cell_Voltage_12_Type { get; } = new ItemValueType<double>("Tray_Cell_Voltage_12");


        public void SetHoldingRegister(ushort[] values)
        {
            Tray_Cell_Voltage_01 = values[0] * 0.001d;
            Tray_Cell_Voltage_02 = values[1] * 0.001d;
            Tray_Cell_Voltage_03 = values[2] * 0.001d;
            Tray_Cell_Voltage_04 = values[3] * 0.001d;
            Tray_Cell_Voltage_05 = values[4] * 0.001d;
            Tray_Cell_Voltage_06 = values[5] * 0.001d;
            Tray_Cell_Voltage_07 = values[6] * 0.001d;
            Tray_Cell_Voltage_08 = values[7] * 0.001d;
            Tray_Cell_Voltage_09 = values[8] * 0.001d;
            Tray_Cell_Voltage_10 = values[9] * 0.001d;
            Tray_Cell_Voltage_11 = values[10] * 0.001d;
            Tray_Cell_Voltage_12 = values[11] * 0.001d;

            Tray_Cell_Voltage_01_Type.SetValue((short)values[0] * 0.001d);
            Tray_Cell_Voltage_02_Type.SetValue((short)values[1] * 0.001d);
            Tray_Cell_Voltage_03_Type.SetValue((short)values[2] * 0.001d);
            Tray_Cell_Voltage_04_Type.SetValue((short)values[3] * 0.001d);
            Tray_Cell_Voltage_05_Type.SetValue((short)values[4] * 0.001d);
            Tray_Cell_Voltage_06_Type.SetValue((short)values[5] * 0.001d);
            Tray_Cell_Voltage_07_Type.SetValue((short)values[6] * 0.001d);
            Tray_Cell_Voltage_08_Type.SetValue((short)values[7] * 0.001d);
            Tray_Cell_Voltage_09_Type.SetValue((short)values[8] * 0.001d);
            Tray_Cell_Voltage_10_Type.SetValue((short)values[9] * 0.001d);
            Tray_Cell_Voltage_11_Type.SetValue((short)values[10] * 0.001d);
            Tray_Cell_Voltage_12_Type.SetValue((short)values[11] * 0.001d);
        }
    }

    public sealed class BMS_TRAY_TEMPERATURE : BMS_BASE
    {
        private double _tray_Temp_1;
        private double _tray_Temp_2;
        private double _tray_Temp_3;
        private double _tray_Temp_4;
        private double _tray_Temp_5;

        public double Tray_Temp_1 { get => _tray_Temp_1; set { SetProperty(ref _tray_Temp_1, value); } }
        public double Tray_Temp_2 { get => _tray_Temp_2; set { SetProperty(ref _tray_Temp_2, value); } }
        public double Tray_Temp_3 { get => _tray_Temp_3; set { SetProperty(ref _tray_Temp_3, value); } }
        public double Tray_Temp_4 { get => _tray_Temp_4; set { SetProperty(ref _tray_Temp_4, value); } }
        public double Tray_Temp_5 { get => _tray_Temp_5; set { SetProperty(ref _tray_Temp_5, value); } }

        public ItemValueType<double> Tray_Temp_1_Type { get; } = new ItemValueType<double>("Tray_Temp_1");
        public ItemValueType<double> Tray_Temp_2_Type { get; } = new ItemValueType<double>("Tray_Temp_2");
        public ItemValueType<double> Tray_Temp_3_Type { get; } = new ItemValueType<double>("Tray_Temp_3");
        public ItemValueType<double> Tray_Temp_4_Type { get; } = new ItemValueType<double>("Tray_Temp_4");
        public ItemValueType<double> Tray_Temp_5_Type { get; } = new ItemValueType<double>("Tray_Temp_5");

        public void SetHoldingRegister(ushort[] values)
        {
            Tray_Temp_1 = values[0] * 0.1d;
            Tray_Temp_2 = values[1] * 0.1d;
            Tray_Temp_3 = values[2] * 0.1d;
            Tray_Temp_4 = values[3] * 0.1d;
            Tray_Temp_5 = values[4] * 0.1d;

            Tray_Temp_1_Type.SetValue((short)values[0] * 0.1d);
            Tray_Temp_2_Type.SetValue((short)values[1] * 0.1d);
            Tray_Temp_3_Type.SetValue((short)values[2] * 0.1d);
            Tray_Temp_4_Type.SetValue((short)values[3] * 0.1d);
            Tray_Temp_5_Type.SetValue((short)values[4] * 0.1d);
        }
    }

    public sealed class BMS_TRAY_WARNING_CODE : BMS_BASE
    {
        private bool _cell_Over_Volt_Warning;
        private bool _cell_Under_Volt_Warning;
        private bool _cell_Volt_Deviation_Warning;
        private bool _cell_Over_Temp_Warning;
        private bool _cell_Under_Temp_Warning;
        private bool _cell_Temp_Deviation_Warning;
        private bool _tray_Over_Volt_Warning;
        private bool _tray_Under_Volt_Warning;
        private bool _reserved1;
        private bool _reserved2;
        private bool _tray_OFF_GAS_Warnning;
        private bool _reserved3;
        private bool _tray_CAN1_comm_fail_Fault;
        private bool _tray_CAN2_comm_fail_Fault;
        private bool _reserved4;
        private bool _reserved5;

        public bool Cell_Over_Volt_Warning { get => _cell_Over_Volt_Warning; set { SetProperty(ref _cell_Over_Volt_Warning, value); } }
        public bool Cell_Under_Volt_Warning { get => _cell_Under_Volt_Warning; set { SetProperty(ref _cell_Under_Volt_Warning, value); } }
        public bool Cell_Volt_Deviation_Warning { get => _cell_Volt_Deviation_Warning; set { SetProperty(ref _cell_Volt_Deviation_Warning, value); } }
        public bool Cell_Over_Temp_Warning { get => _cell_Over_Temp_Warning; set { SetProperty(ref _cell_Over_Temp_Warning, value); } }
        public bool Cell_Under_Temp_Warning { get => _cell_Under_Temp_Warning; set { SetProperty(ref _cell_Under_Temp_Warning, value); } }
        public bool Cell_Temp_Deviation_Warning { get => _cell_Temp_Deviation_Warning; set { SetProperty(ref _cell_Temp_Deviation_Warning, value); } }
        public bool Tray_Over_Volt_Warning { get => _tray_Over_Volt_Warning; set { SetProperty(ref _tray_Over_Volt_Warning, value); } }
        public bool Tray_Under_Volt_Warning { get => _tray_Under_Volt_Warning; set { SetProperty(ref _tray_Under_Volt_Warning, value); } }
        public bool Reserved1 { get => _reserved1; set { SetProperty(ref _reserved1, value); } }
        public bool Reserved2 { get => _reserved2; set { SetProperty(ref _reserved2, value); } }
        public bool Tray_OFF_GAS_Warnning { get => _tray_OFF_GAS_Warnning; set { SetProperty(ref _tray_OFF_GAS_Warnning, value); } }
        public bool Reserved3 { get => _reserved3; set { SetProperty(ref _reserved3, value); } }
        public bool Tray_CAN1_comm_fail_Fault { get => _tray_CAN1_comm_fail_Fault; set { SetProperty(ref _tray_CAN1_comm_fail_Fault, value); } }
        public bool Tray_CAN2_comm_fail_Fault { get => _tray_CAN2_comm_fail_Fault; set { SetProperty(ref _tray_CAN2_comm_fail_Fault, value); } }
        public bool Reserved4 { get => _reserved4; set { SetProperty(ref _reserved4, value); } }
        public bool Reserved5 { get => _reserved5; set { SetProperty(ref _reserved5, value); } }

        public ItemValueType<bool> Cell_Over_Volt_Warning_Type { get; } = new ItemValueType<bool>("Cell_Over_Volt_Warning");
        public ItemValueType<bool> Cell_Under_Volt_Warning_Type { get; } = new ItemValueType<bool>("Cell_Under_Volt_Warning");
        public ItemValueType<bool> Cell_Volt_Deviation_Warning_Type { get; } = new ItemValueType<bool>("Cell_Volt_Deviation_Warning");
        public ItemValueType<bool> Cell_Over_Temp_Warning_Type { get; } = new ItemValueType<bool>("Cell_Over_Temp_Warning");
        public ItemValueType<bool> Cell_Under_Temp_Warning_Type { get; } = new ItemValueType<bool>("Cell_Under_Temp_Warning");
        public ItemValueType<bool> Cell_Temp_Deviation_Warning_Type { get; } = new ItemValueType<bool>("Cell_Temp_Deviation_Warning");
        public ItemValueType<bool> Tray_Over_Volt_Warning_Type { get; } = new ItemValueType<bool>("Tray_Over_Volt_Warning");
        public ItemValueType<bool> Tray_Under_Volt_Warning_Type { get; } = new ItemValueType<bool>("Tray_Under_Volt_Warning");
        //public ItemValueType<bool> Reserved1_Type { get; } = new ItemValueType<bool>("Reserved1");
        //public ItemValueType<bool> Reserved2_Type { get; } = new ItemValueType<bool>("Reserved2");
        public ItemValueType<bool> Tray_OFF_GAS_Warnning_Type { get; } = new ItemValueType<bool>("Tray_OFF_GAS_Warnning");
        //public ItemValueType<bool> Reserved3_Type { get; } = new ItemValueType<bool>("Reserved3");
        public ItemValueType<bool> Tray_CAN1_comm_fail_Fault_Type { get; } = new ItemValueType<bool>("Tray_CAN1_comm_fail_Fault");
        public ItemValueType<bool> Tray_CAN2_comm_fail_Fault_Type { get; } = new ItemValueType<bool>("Tray_CAN2_comm_fail_Fault");
        //public ItemValueType<bool> Reserved4_Type { get; } = new ItemValueType<bool>("Reserved4");
        //public ItemValueType<bool> Reserved5_Type { get; } = new ItemValueType<bool>("Reserved5");

        public void SetHoldingRegister(ushort value)
        {
            Cell_Over_Volt_Warning = (value & 0x0001) != 0;
            Cell_Under_Volt_Warning = (value & 0x0002) != 0;
            Cell_Volt_Deviation_Warning = (value & 0x0004) != 0;
            Cell_Over_Temp_Warning = (value & 0x0008) != 0;
            Cell_Under_Temp_Warning = (value & 0x0010) != 0;
            Cell_Temp_Deviation_Warning = (value & 0x0020) != 0;
            Tray_Over_Volt_Warning = (value & 0x0040) != 0;
            Tray_Under_Volt_Warning = (value & 0x0080) != 0;
            //Reserved1 = (value & 0x0100) != 0;
            //Reserved2 = (value & 0x0200) != 0;
            Tray_OFF_GAS_Warnning = (value & 0x0400) != 0;
            //Reserved3 = (value & 0x0800) != 0;
            Tray_CAN1_comm_fail_Fault = (value & 0x1000) != 0;
            Tray_CAN2_comm_fail_Fault = (value & 0x2000) != 0;
            //Reserved4 = (value & 0x4000) != 0;
            //Reserved5 = (value & 0x8000) != 0;

            Cell_Over_Volt_Warning_Type.SetValue((value & 0x0001) != 0);
            Cell_Under_Volt_Warning_Type.SetValue((value & 0x0002) != 0);
            Cell_Volt_Deviation_Warning_Type.SetValue((value & 0x0004) != 0);
            Cell_Over_Temp_Warning_Type.SetValue((value & 0x0008) != 0);
            Cell_Under_Temp_Warning_Type.SetValue((value & 0x0010) != 0);
            Cell_Temp_Deviation_Warning_Type.SetValue((value & 0x0020) != 0);
            Tray_Over_Volt_Warning_Type.SetValue((value & 0x0040) != 0);
            Tray_Under_Volt_Warning_Type.SetValue((value & 0x0080) != 0);
            //Reserved1_Type.SetValue((value & 0x0100) != 0);
            //Reserved2_Type.SetValue((value & 0x0200) != 0);
            Tray_OFF_GAS_Warnning_Type.SetValue((value & 0x0400) != 0);
            //Reserved3_Type.SetValue((value & 0x0800) != 0);
            Tray_CAN1_comm_fail_Fault_Type.SetValue((value & 0x1000) != 0);
            Tray_CAN2_comm_fail_Fault_Type.SetValue((value & 0x2000) != 0);
            //Reserved4_Type.SetValue((value & 0x4000) != 0);
            //Reserved5_Type.SetValue((value & 0x8000) != 0);
        }
    }

    public sealed class BMS_TRAY_FAULT_CODE : BMS_BASE
    {
        private bool _cell_Over_Volt_Fault;
        private bool _cell_Under_Volt_Fault;
        private bool _cell_Volt_Deviation_Fault;
        private bool _cell_Over_Temp_Fault;
        private bool _cell_Under_Temp_Fault;
        private bool _cell_Temp_Deviation_Fault;
        private bool _tray_Over_Volt_Fault;
        private bool _tray_Under_Volt_Fault;
        private bool _reserved1;
        private bool _reserved2;
        private bool _tray_OFF_GAS_Fault;
        private bool _tray_WATER_Fault;
        private bool _reserved3;
        private bool _reserved4;
        private bool _tray_Module_comm_fail_Fault;
        private bool _reserved5;

        public bool Cell_Over_Volt_Fault { get => _cell_Over_Volt_Fault; set { SetProperty(ref _cell_Over_Volt_Fault, value); } }
        public bool Cell_Under_Volt_Fault { get => _cell_Under_Volt_Fault; set { SetProperty(ref _cell_Under_Volt_Fault, value); } }
        public bool Cell_Volt_Deviation_Fault { get => _cell_Volt_Deviation_Fault; set { SetProperty(ref _cell_Volt_Deviation_Fault, value); } }
        public bool Cell_Over_Temp_Fault { get => _cell_Over_Temp_Fault; set { SetProperty(ref _cell_Over_Temp_Fault, value); } }
        public bool Cell_Under_Temp_Fault { get => _cell_Under_Temp_Fault; set { SetProperty(ref _cell_Under_Temp_Fault, value); } }
        public bool Cell_Temp_Deviation_Fault { get => _cell_Temp_Deviation_Fault; set { SetProperty(ref _cell_Temp_Deviation_Fault, value); } }
        public bool Tray_Over_Volt_Fault { get => _tray_Over_Volt_Fault; set { SetProperty(ref _tray_Over_Volt_Fault, value); } }
        public bool Tray_Under_Volt_Fault { get => _tray_Under_Volt_Fault; set { SetProperty(ref _tray_Under_Volt_Fault, value); } }
        public bool Reserved1 { get => _reserved1; set { SetProperty(ref _reserved1, value); } }
        public bool Reserved2 { get => _reserved2; set { SetProperty(ref _reserved2, value); } }
        public bool Tray_OFF_GAS_Fault { get => _tray_OFF_GAS_Fault; set { SetProperty(ref _tray_OFF_GAS_Fault, value); } }
        public bool Tray_WATER_Fault { get => _tray_WATER_Fault; set { SetProperty(ref _tray_WATER_Fault, value); } }
        public bool Reserved3 { get => _reserved3; set { SetProperty(ref _reserved3, value); } }
        public bool Reserved4 { get => _reserved4; set { SetProperty(ref _reserved4, value); } }
        public bool Tray_Module_comm_fail_Fault { get => _tray_Module_comm_fail_Fault; set { SetProperty(ref _tray_Module_comm_fail_Fault, value); } }
        public bool Reserved5 { get => _reserved5; set { SetProperty(ref _reserved5, value); } }

        public ItemValueType<bool> Cell_Over_Volt_Fault_Type { get; } = new ItemValueType<bool>("Cell_Over_Volt_Fault");
        public ItemValueType<bool> Cell_Under_Volt_Fault_Type { get; } = new ItemValueType<bool>("Cell_Under_Volt_Fault");
        public ItemValueType<bool> Cell_Volt_Deviation_Fault_Type { get; } = new ItemValueType<bool>("Cell_Volt_Deviation_Fault");
        public ItemValueType<bool> Cell_Over_Temp_Fault_Type { get; } = new ItemValueType<bool>("Cell_Over_Temp_Fault");
        public ItemValueType<bool> Cell_Under_Temp_Fault_Type { get; } = new ItemValueType<bool>("Cell_Under_Temp_Fault");
        public ItemValueType<bool> Cell_Temp_Deviation_Fault_Type { get; } = new ItemValueType<bool>("Cell_Temp_Deviation_Fault");
        public ItemValueType<bool> Tray_Over_Volt_Fault_Type { get; } = new ItemValueType<bool>("Tray_Over_Volt_Fault");
        public ItemValueType<bool> Tray_Under_Volt_Fault_Type { get; } = new ItemValueType<bool>("Tray_Under_Volt_Fault");
        //public ItemValueType<bool> Reserved1_Type { get; } = new ItemValueType<bool>("Reserved1");
        //public ItemValueType<bool> Reserved2_Type { get; } = new ItemValueType<bool>("Reserved2");
        public ItemValueType<bool> Tray_OFF_GAS_Fault_Type { get; } = new ItemValueType<bool>("Tray_OFF_GAS_Fault");
        public ItemValueType<bool> Tray_WATER_Fault_Type { get; } = new ItemValueType<bool>("Tray_WATER_Fault");
        //public ItemValueType<bool> Reserved3_Type { get; } = new ItemValueType<bool>("Reserved3");
        //public ItemValueType<bool> Reserved4_Type { get; } = new ItemValueType<bool>("Reserved4");
        public ItemValueType<bool> Tray_Module_comm_fail_Faul_Type { get; } = new ItemValueType<bool>("Tray_Module_comm_fail_Faul");
        //public ItemValueType<bool> Reserved5_Type { get; } = new ItemValueType<bool>("Reserved5");

        public void SetHoldingRegister(ushort value)
        {
            Cell_Over_Volt_Fault = (value & 0x0001) != 0;
            Cell_Under_Volt_Fault = (value & 0x0002) != 0;
            Cell_Volt_Deviation_Fault = (value & 0x0004) != 0;
            Cell_Over_Temp_Fault = (value & 0x0008) != 0;
            Cell_Under_Temp_Fault = (value & 0x0010) != 0;
            Cell_Temp_Deviation_Fault = (value & 0x0020) != 0;
            Tray_Over_Volt_Fault = (value & 0x0040) != 0;
            Tray_Under_Volt_Fault = (value & 0x0080) != 0;
            Reserved1 = (value & 0x0100) != 0;
            Reserved2 = (value & 0x0200) != 0;
            Tray_OFF_GAS_Fault = (value & 0x0400) != 0;
            Tray_WATER_Fault = (value & 0x0800) != 0;
            Reserved3 = (value & 0x1000) != 0;
            Reserved4 = (value & 0x2000) != 0;
            Tray_Module_comm_fail_Fault = (value & 0x4000) != 0;
            Reserved5 = (value & 0x8000) != 0;

            Cell_Over_Volt_Fault_Type.SetValue((value & 0x0001) != 0);
            Cell_Under_Volt_Fault_Type.SetValue((value & 0x0002) != 0);
            Cell_Volt_Deviation_Fault_Type.SetValue((value & 0x0004) != 0);
            Cell_Over_Temp_Fault_Type.SetValue((value & 0x0008) != 0);
            Cell_Under_Temp_Fault_Type.SetValue((value & 0x0010) != 0);
            Cell_Temp_Deviation_Fault_Type.SetValue((value & 0x0020) != 0);
            Tray_Over_Volt_Fault_Type.SetValue((value & 0x0040) != 0);
            Tray_Under_Volt_Fault_Type.SetValue((value & 0x0080) != 0);
            //Reserved1_Type.SetValue((value & 0x0100) != 0);
            //Reserved2_Type.SetValue((value & 0x0200) != 0);
            Tray_OFF_GAS_Fault_Type.SetValue((value & 0x0400) != 0);
            Tray_WATER_Fault_Type.SetValue((value & 0x0800) != 0);
            //Reserved3_Type.SetValue((value & 0x1000) != 0);
            //Reserved4_Type.SetValue((value & 0x2000) != 0);
            Tray_Module_comm_fail_Faul_Type.SetValue((value & 0x4000) != 0);
            //Reserved5_Type.SetValue((value & 0x8000) != 0);
        }
    }

    public sealed class BMS_TRAY_VALUES : BMS_BASE
    {

        private double _tray_Total_Module_Voltage;
        private double _tray_Average_Cell_Voltage;
        private double _tray_Maximum_Cell_Voltage;
        private double _tray_Minimum_Cell_Voltage;
        private double _tray_Difference_Cell_Voltage;
        private double _tray_Average_Cell_Temperature;
        private double _tray_Maximum_Cell_Temperature;
        private double _tray_Minimum_Cell_Temperature;
        private double _tray_Difference_Cell_Temperature;

        public double Tray_Total_Module_Voltage { get => _tray_Total_Module_Voltage; set { SetProperty(ref _tray_Total_Module_Voltage, value); } }
        public double Tray_Average_Cell_Voltage { get => _tray_Average_Cell_Voltage; set { SetProperty(ref _tray_Average_Cell_Voltage, value); } }
        public double Tray_Maximum_Cell_Voltage { get => _tray_Maximum_Cell_Voltage; set { SetProperty(ref _tray_Maximum_Cell_Voltage, value); } }
        public double Tray_Minimum_Cell_Voltage { get => _tray_Minimum_Cell_Voltage; set { SetProperty(ref _tray_Minimum_Cell_Voltage, value); } }
        public double Tray_Difference_Cell_Voltage { get => _tray_Difference_Cell_Voltage; set { SetProperty(ref _tray_Difference_Cell_Voltage, value); } }
        public double Tray_Average_Cell_Temperature { get => _tray_Average_Cell_Temperature; set { SetProperty(ref _tray_Average_Cell_Temperature, value); } }
        public double Tray_Maximum_Cell_Temperature { get => _tray_Maximum_Cell_Temperature; set { SetProperty(ref _tray_Maximum_Cell_Temperature, value); } }
        public double Tray_Minimum_Cell_Temperature { get => _tray_Minimum_Cell_Temperature; set { SetProperty(ref _tray_Minimum_Cell_Temperature, value); } }
        public double Tray_Difference_Cell_Temperature { get => _tray_Difference_Cell_Temperature; set { SetProperty(ref _tray_Difference_Cell_Temperature, value); } }

        public ItemValueType<double> Tray_Total_Module_Voltage_Type { get; } = new ItemValueType<double>("Tray_Total_Module_Voltage");
        public ItemValueType<double> Tray_Average_Cell_Voltage_Type { get; } = new ItemValueType<double>("Tray_Average_Cell_Voltage");
        public ItemValueType<double> Tray_Maximum_Cell_Voltage_Type { get; } = new ItemValueType<double>("Tray_Maximum_Cell_Voltage");
        public ItemValueType<double> Tray_Minimum_Cell_Voltage_Type { get; } = new ItemValueType<double>("Tray_Minimum_Cell_Voltage");
        public ItemValueType<double> Tray_Difference_Cell_Voltage_Type { get; } = new ItemValueType<double>("Tray_Difference_Cell_Voltage");
        public ItemValueType<double> Tray_Average_Cell_Temperature_Type { get; } = new ItemValueType<double>("Tray_Average_Cell_Temperature");
        public ItemValueType<double> Tray_Maximum_Cell_Temperature_Type { get; } = new ItemValueType<double>("Tray_Maximum_Cell_Temperature");
        public ItemValueType<double> Tray_Minimum_Cell_Temperature_Type { get; } = new ItemValueType<double>("Tray_Minimum_Cell_Temperature");
        public ItemValueType<double> Tray_Difference_Cell_Temperature_Type { get; } = new ItemValueType<double>("Tray_Difference_Cell_Temperature");

        public void SetHoldingRegister(ushort[] values)
        {
            Tray_Total_Module_Voltage = values[0] * 0.01;

            Tray_Average_Cell_Voltage = values[1] * 0.001;
            Tray_Maximum_Cell_Voltage = values[2] * 0.001;
            Tray_Minimum_Cell_Voltage = values[3] * 0.001;
            Tray_Difference_Cell_Voltage = values[4] * 0.001;

            Tray_Average_Cell_Temperature = values[5] * 0.1;
            Tray_Maximum_Cell_Temperature = values[6] * 0.1;
            Tray_Minimum_Cell_Temperature = values[7] * 0.1;
            Tray_Difference_Cell_Temperature = values[8] * 0.1;

            Tray_Total_Module_Voltage_Type.SetValue((short)values[0] * 0.01d);

            Tray_Average_Cell_Voltage_Type.SetValue((short)values[1] * 0.001d);
            Tray_Maximum_Cell_Voltage_Type.SetValue((short)values[2] * 0.001d);
            Tray_Minimum_Cell_Voltage_Type.SetValue((short)values[3] * 0.001d);
            Tray_Difference_Cell_Voltage_Type.SetValue((short)values[4] * 0.001d);

            Tray_Average_Cell_Temperature_Type.SetValue((short)values[5] * 0.1d);
            Tray_Maximum_Cell_Temperature_Type.SetValue((short)values[6] * 0.1d);
            Tray_Minimum_Cell_Temperature_Type.SetValue((short)values[7] * 0.1d);
            Tray_Difference_Cell_Temperature_Type.SetValue((short)values[8] * 0.1d);
        }
    }
}
