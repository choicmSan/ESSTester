using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LIB_ESS.Model
{
    public sealed class BMS_RBMS : BMS_BASE
    {
        public BMS_RBMS_RACK_VALUES BmsRBMSRackValues { get; } = new BMS_RBMS_RACK_VALUES();
        public BMS_RBMS_DTC_STATE BmsRBMSDTCState { get; } = new BMS_RBMS_DTC_STATE();
        public BMS_RBMS_DTC_ALARM1 BmsRBMSDTCAlarm1 { get; } = new BMS_RBMS_DTC_ALARM1();
        public BMS_RBMS_DTC_ALARM2 BmsRBMSDTCAlarm2 { get; } = new BMS_RBMS_DTC_ALARM2();
        public BMS_RBMS_DTC_ALARM3 BmsRBMSDTCAlarm3 { get; } = new BMS_RBMS_DTC_ALARM3();
        public BMS_RBMS_SYSTEM_FAULT_BITS BmsRBMSSystemFaultBits { get; } = new BMS_RBMS_SYSTEM_FAULT_BITS();
        public BMS_RBMS_RACK_ONLINES BmsRBMSRackOnlines { get; } = new BMS_RBMS_RACK_ONLINES();
        public BMS_RBMS_RACK_DISCHARGEMODE BmsRBMSRackDischargeMode { get; } = new BMS_RBMS_RACK_DISCHARGEMODE();

        /// <summary>
        /// HodingRegister 10516 - 10549 : 34
        /// </summary>
        /// <param name="values"></param>
        public void SetHoldingRegister(ushort[] values)
        {
            Span<ushort> sp = values;
            BmsRBMSRackValues.SetHoldingRegister(sp.Slice(0, 4).ToArray(), sp.Slice(8, 12).ToArray());
            BmsRBMSRackValues.SetHoldingRegister_RackSumDischargeAh(sp[24], sp[25]);
            BmsRBMSRackValues.SetHoldingRegister_DischargeLimit(sp[32], sp[33]);

            BmsRBMSDTCState.SetHoldingRegister(sp[4]);

            BmsRBMSDTCAlarm1.SetHoldingRegister(sp[5]);
            BmsRBMSDTCAlarm2.SetHoldingRegister(sp[6]);
            BmsRBMSDTCAlarm3.SetHoldingRegister(sp[7]);

            BmsRBMSSystemFaultBits.SetHoldingRegister(sp[20]);
            BmsRBMSRackOnlines.SetHoldingRegister(sp[21]);
            BmsRBMSRackDischargeMode.SetHoldingRegister(sp[22]);
        }

    }

    public sealed class BMS_RBMS_RACK_VALUES : BMS_BASE
    {
        private double _rackAvgVolt;
        private double _rackAvgCurr;
        private double _rackSumCurr;
        private double _rackSumPower;

        private double _rackAvgRealSOC;
        private double _rackAvgSOH;
        private double _rackMinRealSOC;
        private double _rackAvgUserSOC;
        private double _rackCellAvgVolt;
        private double _rackCellMinVolt;
        private double _rackCellMaxVolt;
        private double _rackCellDifVolt;
        private double _rackCellAvgTemp;
        private double _rackCellMinTemp;
        private double _rackCellMaxTemp;
        private double _rackCellDifTemp;

        private uint _rackSumDischargeAh;

        private double _dischargeCurrentLimit;
        private double _dischargePowerLimit;

        public double RackAvgVolt { get => _rackAvgVolt; set { SetProperty(ref _rackAvgVolt, value); } }
        public double RackAvgCurr { get => _rackAvgCurr; set { SetProperty(ref _rackAvgCurr, value); } }
        public double RackSumCurr { get => _rackSumCurr; set { SetProperty(ref _rackSumCurr, value); } }
        public double RackSumPower { get => _rackSumPower; set { SetProperty(ref _rackSumPower, value); } }

        public ItemValueType<double> Rack_AvgVolt_Type { get; } = new ItemValueType<double>("RackAvgVolt");
        public ItemValueType<double> Rack_AvgCurr_Type { get; } = new ItemValueType<double>("RackAvgCurr");
        public ItemValueType<double> Rack_SumCurr_Type { get; } = new ItemValueType<double>("RackSumCurr");
        public ItemValueType<double> Rack_SumPower_Type { get; } = new ItemValueType<double>("RackSumPower");

        public double RackAvgRealSOC { get => _rackAvgRealSOC; set { SetProperty(ref _rackAvgRealSOC, value); } }
        public double RackAvgSOH { get => _rackAvgSOH; set { SetProperty(ref _rackAvgSOH, value); } }
        public double RackMinRealSOC { get => _rackMinRealSOC; set { SetProperty(ref _rackMinRealSOC, value); } }
        public double RackAvgUserSOC { get => _rackAvgUserSOC; set { SetProperty(ref _rackAvgUserSOC, value); } }
        public double RackCellAvgVolt { get => _rackCellAvgVolt; set { SetProperty(ref _rackCellAvgVolt, value); } }
        public double RackCellMinVolt { get => _rackCellMinVolt; set { SetProperty(ref _rackCellMinVolt, value); } }
        public double RackCellMaxVolt { get => _rackCellMaxVolt; set { SetProperty(ref _rackCellMaxVolt, value); } }
        public double RackCellDifVolt { get => _rackCellDifVolt; set { SetProperty(ref _rackCellDifVolt, value); } }
        public double RackCellAvgTemp { get => _rackCellAvgTemp; set { SetProperty(ref _rackCellAvgTemp, value); } }
        public double RackCellMinTemp { get => _rackCellMinTemp; set { SetProperty(ref _rackCellMinTemp, value); } }
        public double RackCellMaxTemp { get => _rackCellMaxTemp; set { SetProperty(ref _rackCellMaxTemp, value); } }
        public double RackCellDifTemp { get => _rackCellDifTemp; set { SetProperty(ref _rackCellDifTemp, value); } }

        public ItemValueType<double> Rack_AvgRealSOC_Type { get; } = new ItemValueType<double>("RackAvgRealSOC");
        public ItemValueType<double> Rack_AvgSOH_Type { get; } = new ItemValueType<double>("RackAvgSOH");
        public ItemValueType<double> Rack_MinRealSOC_Type { get; } = new ItemValueType<double>("RackMinRealSOC");
        public ItemValueType<double> Rack_AvgUserSOC_Type { get; } = new ItemValueType<double>("RackAvgUserSOC");
        public ItemValueType<double> Rack_CellAvgVolt_Type { get; } = new ItemValueType<double>("RackCellAvgVolt");
        public ItemValueType<double> Rack_CellMinVolt_Type { get; } = new ItemValueType<double>("RackCellMinVolt");
        public ItemValueType<double> Rack_CellMaxVolt_Type { get; } = new ItemValueType<double>("RackCellMaxVolt");
        public ItemValueType<double> Rack_CellDifVolt_Type { get; } = new ItemValueType<double>("RackCellDifVolt");
        public ItemValueType<double> Rack_CellAvgTemp_Type { get; } = new ItemValueType<double>("RackCellAvgTemp");
        public ItemValueType<double> Rack_CellMinTemp_Type { get; } = new ItemValueType<double>("RackCellMinTemp");
        public ItemValueType<double> Rack_CellMaxTemp_Type { get; } = new ItemValueType<double>("RackCellMaxTemp");
        public ItemValueType<double> Rack_CellDifTemp_Type { get; } = new ItemValueType<double>("RackCellDifTemp");

        public uint RackSumDischargeAh { get => _rackSumDischargeAh; set { SetProperty(ref _rackSumDischargeAh, value); } }
        public ItemValueType<uint> RackSumDischargeAh_Type { get; } = new ItemValueType<uint>("RackSumDischargeAh");

        public double DischargeCurrentLimit { get => _dischargeCurrentLimit; set { SetProperty(ref _dischargeCurrentLimit, value); } }
        public double DischargePowerLimit { get => _dischargePowerLimit; set { SetProperty(ref _dischargePowerLimit, value); } }

        public ItemValueType<double> DischargeCurrentLimit_Type { get; } = new ItemValueType<double>("DischargeCurrentLimit");
        public ItemValueType<double> DischargePowerLimit_Type { get; } = new ItemValueType<double>("DischargePowerLimit");

        /// <summary>
        /// values1 : 10516 - 10519
        /// values2 : 10524 - 10535
        /// </summary>
        /// <param name="values1">index 0 ~ 3 : 4ea </param>
        /// <param name="values2">index 8 ~ 19 : 12ea </param>
        public void SetHoldingRegister(ushort[] values1, ushort[] values2)
        {
            var value0 = values1[0] * 0.1d;
            var value1 = values1[1] * 0.1d;
            var value2 = values1[2] * 0.1d;
            var value3 = values1[3] * 0.1d;

            Rack_AvgVolt_Type.SetValue(value0);
            Rack_AvgCurr_Type.SetValue(value1);
            Rack_SumCurr_Type.SetValue(value2);
            Rack_SumPower_Type.SetValue(value3);

            RackAvgVolt = value0;
            RackAvgCurr = value1;
            RackSumCurr = value2;
            RackSumPower = value3;

            var value20 = values2[0] * 0.1d;
            var value21 = values2[1] * 0.01d;
            var value22 = values2[2] * 0.1d;
            var value23 = values2[3] * 0.1d;

            Rack_AvgRealSOC_Type.SetValue(value20);
            Rack_AvgSOH_Type.SetValue(value21);
            Rack_MinRealSOC_Type.SetValue(value22);
            Rack_AvgUserSOC_Type.SetValue(value23);

            RackAvgRealSOC = value20;
            RackAvgSOH = value21;
            RackMinRealSOC = value22;
            RackAvgUserSOC = value23;

            var value24 = values2[4] * 0.001d;
            var value25 = values2[5] * 0.001d;
            var value26 = values2[6] * 0.001d;
            var value27 = values2[7] * 0.001d;

            Rack_CellAvgVolt_Type.SetValue(value24);
            Rack_CellMinVolt_Type.SetValue(value25);
            Rack_CellMaxVolt_Type.SetValue(value26);
            Rack_CellDifVolt_Type.SetValue(value27);

            RackCellAvgVolt = value24;
            RackCellMinVolt = value25;
            RackCellMaxVolt = value26;
            RackCellDifVolt = value27;

            var value28 = ((short)values2[8]) * 0.1d;
            var value29 = ((short)values2[9]) * 0.1d;
            var value30 = ((short)values2[10]) * 0.1d;
            var value31 = ((short)values2[11]) * 0.1d;

            Rack_CellAvgTemp_Type.SetValue(value28);
            Rack_CellMinTemp_Type.SetValue(value29);
            Rack_CellMaxTemp_Type.SetValue(value30);
            Rack_CellDifTemp_Type.SetValue(value31);

            RackCellAvgTemp = value28;
            RackCellMinTemp = value29;
            RackCellMaxTemp = value30;
            RackCellDifTemp = value31;
        }

        /// <summary>
        /// Holding Register 10540, 10541
        /// index 24, 25
        /// </summary>
        /// <param name="value1">index 24</param>
        /// <param name="value2">index 25</param>
        public void SetHoldingRegister_RackSumDischargeAh(ushort value1, ushort value2)
        {
            var vm = (uint)((value1 << 16) | value2);
            RackSumDischargeAh = vm;
            RackSumDischargeAh_Type.SetValue(vm);
        }

        /// <summary>
        /// Holding Register 10548, 10549
        /// index 32, 33
        /// </summary>
        /// <param name="value1">index 32</param>
        /// <param name="value2">index 33</param>
        public void SetHoldingRegister_DischargeLimit(ushort value1, ushort value2) 
        {
            var v1 = value1 * 0.1d;
            var v2 = value2 * 0.1d;

            DischargeCurrentLimit = v1;
            DischargePowerLimit = v2;

            DischargeCurrentLimit_Type.SetValue(v1);
            DischargePowerLimit_Type.SetValue(v2);
        }
    }

    public sealed class BMS_RBMS_DTC_STATE : BMS_BASE
    {
        private bool _statBMSReady = false;
        private bool _statCHGMode = false;
        private bool _statDCHMode = false;
        private bool _statRelayDCH = false;
        private bool _statRelayPRE = false;
        private bool _statRelayCHG = false;

        public bool STAT_BMS_Ready { get => _statBMSReady; set { SetProperty(ref _statBMSReady, value); } }
        public bool STAT_CHG_Mode { get => _statCHGMode; set { SetProperty(ref _statCHGMode, value); } }
        public bool STAT_DCH_Mode { get => _statDCHMode; set { SetProperty(ref _statDCHMode, value); } }
        public bool STAT_Relay_DCH { get => _statRelayDCH; set { SetProperty(ref _statRelayDCH, value); } }
        public bool STAT_Relay_PRE { get => _statRelayPRE; set { SetProperty(ref _statRelayPRE, value); } }
        public bool STAT_Relay_CHG { get => _statRelayCHG; set { SetProperty(ref _statRelayCHG, value); } }

        public ItemValueType<bool> STAT_BMS_Ready_Type { get; } = new ItemValueType<bool>("STAT_BMS_Ready");
        public ItemValueType<bool> STAT_CHG_Mode_Type { get; } = new ItemValueType<bool>("STAT_CHG_Mode ");
        public ItemValueType<bool> STAT_DCH_Mode_Type { get; } = new ItemValueType<bool>("STAT_DCH_Mode ");
        public ItemValueType<bool> STAT_Relay_DCH_Type { get; } = new ItemValueType<bool>("STAT_Relay_DCH");
        public ItemValueType<bool> STAT_Relay_PRE_Type { get; } = new ItemValueType<bool>("STAT_Relay_PRE");
        public ItemValueType<bool> STAT_Relay_CHG_Type { get; } = new ItemValueType<bool>("STAT_Relay_CHG");

        /// <summary>
        /// Hoding Register 10520
        /// </summary>
        /// <param name="value">index 4</param>
        public void SetHoldingRegister(ushort value)
        {
            var bools = GetBools(value);

            STAT_BMS_Ready_Type.SetValue(bools[0]);
            STAT_CHG_Mode_Type.SetValue(bools[1]);
            STAT_DCH_Mode_Type.SetValue(bools[2]);
            STAT_Relay_DCH_Type.SetValue(bools[4]);
            STAT_Relay_PRE_Type.SetValue(bools[5]);
            STAT_Relay_CHG_Type.SetValue(bools[6]);

            STAT_BMS_Ready = bools[0];
            STAT_CHG_Mode = bools[1];
            STAT_DCH_Mode = bools[2];
            STAT_Relay_DCH = bools[4];
            STAT_Relay_PRE = bools[5];
            STAT_Relay_CHG = bools[6];
        }
    }

    public sealed class BMS_RBMS_DTC_ALARM1 : BMS_BASE
    {
        private bool _packOverVoltageProtectionFault = false;
        private bool _packUnderVoltageProtectionFault = false;
        private bool _chargeOverCurrentFault = false;
        private bool _disChargeOverCurrentFault = false;
        private bool _overSOCFault = false;
        private bool _underSOCFault = false;
        private bool _underSOHFault = false;

        private bool _packOverVoltageProtectionWarning = false;
        private bool _packUnderVoltageProtectionWarning = false;
        private bool _chargeOverCurrentWarning = false;
        private bool _disChargeOverCurrentWarning = false;
        private bool _overSOCWarning = false;
        private bool _underSOCWarning = false;
        private bool _underSOHWarning = false;

        public bool PackOverVoltageProtectionFault { get => _packOverVoltageProtectionFault; set { SetProperty(ref _packOverVoltageProtectionFault, value); } }
        public bool PackUnderVoltageProtectionFault { get => _packUnderVoltageProtectionFault; set { SetProperty(ref _packUnderVoltageProtectionFault, value); } }
        public bool ChargeOverCurrentFault { get => _chargeOverCurrentFault; set { SetProperty(ref _chargeOverCurrentFault, value); } }
        public bool DisChargeOverCurrentFault { get => _disChargeOverCurrentFault; set { SetProperty(ref _disChargeOverCurrentFault, value); } }
        public bool OverSOCFault { get => _overSOCFault; set { SetProperty(ref _overSOCFault, value); } }
        public bool UnderSOCFault { get => _underSOCFault; set { SetProperty(ref _underSOCFault, value); } }
        public bool UnderSOHFault { get => _underSOHFault; set { SetProperty(ref _underSOHFault, value); } }

        public bool PackOverVoltageProtectionWarning { get => _packOverVoltageProtectionWarning; set { SetProperty(ref _packOverVoltageProtectionWarning, value); } }
        public bool PackUnderVoltageProtectionWarning { get => _packUnderVoltageProtectionWarning; set { SetProperty(ref _packUnderVoltageProtectionWarning, value); } }
        public bool ChargeOverCurrentWarning { get => _chargeOverCurrentWarning; set { SetProperty(ref _chargeOverCurrentWarning, value); } }
        public bool DisChargeOverCurrentWarning { get => _disChargeOverCurrentWarning; set { SetProperty(ref _disChargeOverCurrentWarning, value); } }
        public bool OverSOCWarning { get => _overSOCWarning; set { SetProperty(ref _overSOCWarning, value); } }
        public bool UnderSOCWarning { get => _underSOCWarning; set { SetProperty(ref _underSOCWarning, value); } }
        public bool UnderSOHWarning { get => _underSOHWarning; set { SetProperty(ref _underSOHWarning, value); } }

        public ItemValueType<bool> PackOverVoltageProtectionFault_Type { get; } = new ItemValueType<bool>("PackOverVoltageProtectionFault ");
        public ItemValueType<bool> PackUnderVoltageProtectionFault_Type { get; } = new ItemValueType<bool>("PackUnderVoltageProtectionFault");
        public ItemValueType<bool> ChargeOverCurrentFault_Type { get; } = new ItemValueType<bool>("ChargeOverCurrentFault");
        public ItemValueType<bool> DisChargeOverCurrentFault_Type { get; } = new ItemValueType<bool>("DisChargeOverCurrentFault");
        public ItemValueType<bool> OverSOCFault_Type { get; } = new ItemValueType<bool>("OverSOCFault");
        public ItemValueType<bool> UnderSOCFault_Type { get; } = new ItemValueType<bool>("UnderSOCFault");
        public ItemValueType<bool> UnderSOHFault_Type { get; } = new ItemValueType<bool>("UnderSOHFault");

        public ItemValueType<bool> PackOverVoltageProtectionWarning_Type { get; } = new ItemValueType<bool>("PackOverVoltageProtectionWarning");
        public ItemValueType<bool> PackUnderVoltageProtectionWarning_Type { get; } = new ItemValueType<bool>("PackUnderVoltageProtectionWarning");
        public ItemValueType<bool> ChargeOverCurrentWarning_Type { get; } = new ItemValueType<bool>("ChargeOverCurrentWarning");
        public ItemValueType<bool> DisChargeOverCurrentWarning_Type { get; } = new ItemValueType<bool>("DisChargeOverCurrentWarning");
        public ItemValueType<bool> OverSOCWarning_Type { get; } = new ItemValueType<bool>("OverSOCWarning");
        public ItemValueType<bool> UnderSOCWarning_Type { get; } = new ItemValueType<bool>("UnderSOCWarning");
        public ItemValueType<bool> UnderSOHWarning_Type { get; } = new ItemValueType<bool>("UnderSOHWarning");

        /// <summary>
        /// Holding Register 10521
        /// </summary>
        /// <param name="value">index 5</param>
        public void SetHoldingRegister(ushort value)
        {
            var bools = GetBools(value);

            PackOverVoltageProtectionFault = bools[0];
            PackUnderVoltageProtectionFault = bools[1];
            ChargeOverCurrentFault = bools[2];
            DisChargeOverCurrentFault = bools[3];
            OverSOCFault = bools[4];
            UnderSOCFault = bools[5];
            UnderSOHFault = bools[6];

            PackOverVoltageProtectionWarning = bools[8];
            PackUnderVoltageProtectionWarning = bools[9];
            ChargeOverCurrentWarning = bools[10];
            DisChargeOverCurrentWarning = bools[11];
            OverSOCWarning = bools[12];
            UnderSOCWarning = bools[13];
            UnderSOHWarning = bools[14];

            PackOverVoltageProtectionFault_Type.SetValue(bools[0]);
            PackUnderVoltageProtectionFault_Type.SetValue(bools[1]);
            ChargeOverCurrentFault_Type.SetValue(bools[2]);
            DisChargeOverCurrentFault_Type.SetValue(bools[3]);
            OverSOCFault_Type.SetValue(bools[4]);
            UnderSOCFault_Type.SetValue(bools[5]);
            UnderSOHFault_Type.SetValue(bools[6]);

            PackOverVoltageProtectionWarning_Type.SetValue(bools[8]);
            PackUnderVoltageProtectionWarning_Type.SetValue(bools[9]);
            ChargeOverCurrentWarning_Type.SetValue(bools[10]);
            DisChargeOverCurrentWarning_Type.SetValue(bools[11]);
            OverSOCWarning_Type.SetValue(bools[12]);
            UnderSOCWarning_Type.SetValue(bools[13]);
            UnderSOHWarning_Type.SetValue(bools[14]);
        }
    }

    public sealed class BMS_RBMS_DTC_ALARM2 : BMS_BASE
    {
        private bool _cellOverVoltageProtectionFault = false;
        private bool _cellUnderVoltageProtectionFault = false;
        private bool _cellDifferenceVoltageProtectionFault = false;
        private bool _cellOverTemperatureProtectionFault = false;
        private bool _cellUnderTemperatureProtectionFault = false;
        private bool _cellDifferenceTemperatureProtectionFault = false;

        private bool _cellOverVoltageProtectionWarning = false;
        private bool _cellUnderVoltageProtectionWarning = false;
        private bool _cellDifferenceVoltageProtectionWarning = false;
        private bool _cellOverTemperatureProtectionWarning = false;
        private bool _cellUnderTemperatureProtectionWarning = false;
        private bool _cellDifferenceTemperatureProtectionWarning = false;

        public bool CellOverVoltageProtectionFault { get => _cellOverVoltageProtectionFault; set { SetProperty(ref _cellOverVoltageProtectionFault, value); } }
        public bool CellUnderVoltageProtectionFault { get => _cellUnderVoltageProtectionFault; set { SetProperty(ref _cellUnderVoltageProtectionFault, value); } }
        public bool CellDifferenceVoltageProtectionFault { get => _cellDifferenceVoltageProtectionFault; set { SetProperty(ref _cellDifferenceVoltageProtectionFault, value); } }
        public bool CellOverTemperatureProtectionFault { get => _cellOverTemperatureProtectionFault; set { SetProperty(ref _cellOverTemperatureProtectionFault, value); } }
        public bool CellUnderTemperatureProtectionFault { get => _cellUnderTemperatureProtectionFault; set { SetProperty(ref _cellUnderTemperatureProtectionFault, value); } }
        public bool CellDifferenceTemperatureProtectionFault { get => _cellDifferenceTemperatureProtectionFault; set { SetProperty(ref _cellDifferenceTemperatureProtectionFault, value); } }

        public bool CellOverVoltageProtectionWarning { get => _cellOverVoltageProtectionWarning; set { SetProperty(ref _cellOverVoltageProtectionWarning, value); } }
        public bool CellUnderVoltageProtectionWarning { get => _cellUnderVoltageProtectionWarning; set { SetProperty(ref _cellUnderVoltageProtectionWarning, value); } }
        public bool CellDifferenceVoltageProtectionWarning { get => _cellDifferenceVoltageProtectionWarning; set { SetProperty(ref _cellDifferenceVoltageProtectionWarning, value); } }
        public bool CellOverTemperatureProtectionWarning { get => _cellOverTemperatureProtectionWarning; set { SetProperty(ref _cellOverTemperatureProtectionWarning, value); } }
        public bool CellUnderTemperatureProtectionWarning { get => _cellUnderTemperatureProtectionWarning; set { SetProperty(ref _cellUnderTemperatureProtectionWarning, value); } }
        public bool CellDifferenceTemperatureProtectionWarning { get => _cellDifferenceTemperatureProtectionWarning; set { SetProperty(ref _cellDifferenceTemperatureProtectionWarning, value); } }

        public ItemValueType<bool> CellOverVoltageProtectionFault_Type { get; } = new ItemValueType<bool>("CellOverVoltageProtectionFault");
        public ItemValueType<bool> CellUnderVoltageProtectionFault_Type { get; } = new ItemValueType<bool>("CellUnderVoltageProtectionFault");
        public ItemValueType<bool> CellDifferenceVoltageProtectionFault_Type { get; } = new ItemValueType<bool>("CellDifferenceVoltageProtectionFault");
        public ItemValueType<bool> CellOverTemperatureProtectionFault_Type { get; } = new ItemValueType<bool>("CellOverTemperatureProtectionFault");
        public ItemValueType<bool> CellUnderTemperatureProtectionFault_Type { get; } = new ItemValueType<bool>("CellUnderTemperatureProtectionFault");
        public ItemValueType<bool> CellDifferenceTemperatureProtectionFault_Type { get; } = new ItemValueType<bool>("CellDifferenceTemperatureProtectionFault");

        public ItemValueType<bool> CellOverVoltageProtectionWarning_Type { get; } = new ItemValueType<bool>("CellOverVoltageProtectionWarning");
        public ItemValueType<bool> CellUnderVoltageProtectionWarning_Type { get; } = new ItemValueType<bool>("CellUnderVoltageProtectionWarning");
        public ItemValueType<bool> CellDifferenceVoltageProtectionWarning_Type { get; } = new ItemValueType<bool>("CellDifferenceVoltageProtectionWarning");
        public ItemValueType<bool> CellOverTemperatureProtectionWarning_Type { get; } = new ItemValueType<bool>("CellOverTemperatureProtectionWarning");
        public ItemValueType<bool> CellUnderTemperatureProtectionWarning_Type { get; } = new ItemValueType<bool>("CellUnderTemperatureProtectionWarning");
        public ItemValueType<bool> CellDifferenceTemperatureProtectionWarning_Type { get; } = new ItemValueType<bool>("CellDifferenceTemperatureProtectionWarning");

        /// <summary>
        /// Holding Register 10522
        /// </summary>
        /// <param name="value">index 6</param>
        public void SetHoldingRegister(ushort value)
        {
            var bools = GetBools(value);

            CellOverVoltageProtectionFault = bools[0];
            CellUnderVoltageProtectionFault = bools[1];
            CellDifferenceVoltageProtectionFault = bools[2];
            CellOverTemperatureProtectionFault = bools[3];
            CellUnderTemperatureProtectionFault = bools[4];
            CellDifferenceTemperatureProtectionFault = bools[5];

            CellOverVoltageProtectionWarning = bools[8];
            CellUnderVoltageProtectionWarning = bools[9];
            CellDifferenceVoltageProtectionWarning = bools[10];
            CellOverTemperatureProtectionWarning = bools[11];
            CellUnderTemperatureProtectionWarning = bools[12];
            CellDifferenceTemperatureProtectionWarning = bools[13];

            CellOverVoltageProtectionFault_Type.SetValue(bools[0]);
            CellUnderVoltageProtectionFault_Type.SetValue(bools[1]);
            CellDifferenceVoltageProtectionFault_Type.SetValue(bools[2]);
            CellOverTemperatureProtectionFault_Type.SetValue(bools[3]);
            CellUnderTemperatureProtectionFault_Type.SetValue(bools[4]);
            CellDifferenceTemperatureProtectionFault_Type.SetValue(bools[5]);

            CellOverVoltageProtectionWarning_Type.SetValue(bools[8]);
            CellUnderVoltageProtectionWarning_Type.SetValue(bools[9]);
            CellDifferenceVoltageProtectionWarning_Type.SetValue(bools[10]);
            CellOverTemperatureProtectionWarning_Type.SetValue(bools[11]);
            CellUnderTemperatureProtectionWarning_Type.SetValue(bools[12]);
            CellDifferenceTemperatureProtectionWarning_Type.SetValue(bools[13]);

        }
    }

    public sealed class BMS_RBMS_DTC_ALARM3 : BMS_BASE
    {
        private bool _regenOverCurrentFault = false;
        private bool _disChargeOverTemperatureFalut = false;
        private bool _disChargeUnderTemperatureFalut = false;
        private bool _chargeOverPowerFault = false;
        private bool _disChargeOverPowerFault = false;

        private bool _regenOverCurrentWarning = false;
        private bool _disChargeOverTemperatureWarning = false;
        private bool _disChargeUnderTemperatureWarning = false;
        private bool _chargeOverPowerWarning = false;
        private bool _disChargeOverPowerWarning = false;

        public bool RegenOverCurrentFault { get => _regenOverCurrentFault; set { SetProperty(ref _regenOverCurrentFault, value); } }
        public bool DisChargeOverTemperatureFalut { get => _disChargeOverTemperatureFalut; set { SetProperty(ref _disChargeOverTemperatureFalut, value); } }
        public bool DisChargeUnderTemperatureFalut { get => _disChargeUnderTemperatureFalut; set { SetProperty(ref _disChargeUnderTemperatureFalut, value); } }
        public bool ChargeOverPowerFault { get => _chargeOverPowerFault; set { SetProperty(ref _chargeOverPowerFault, value); } }
        public bool DisChargeOverPowerFault { get => _disChargeOverPowerFault; set { SetProperty(ref _disChargeOverPowerFault, value); } }


        public bool RegenOverCurrentWarning { get => _regenOverCurrentWarning; set { SetProperty(ref _regenOverCurrentWarning, value); } }
        public bool DisChargeOverTemperatureWarning { get => _disChargeOverTemperatureWarning; set { SetProperty(ref _disChargeOverTemperatureWarning, value); } }
        public bool DisChargeUnderTemperatureWarning { get => _disChargeUnderTemperatureWarning; set { SetProperty(ref _disChargeUnderTemperatureWarning, value); } }
        public bool ChargeOverPowerWarning { get => _chargeOverPowerWarning; set { SetProperty(ref _chargeOverPowerWarning, value); } }
        public bool DisChargeOverPowerWarning { get => _disChargeOverPowerWarning; set { SetProperty(ref _disChargeOverPowerWarning, value); } }

        public ItemValueType<bool> RegenOverCurrentFault_Type { get; } = new ItemValueType<bool>("RegenOverCurrentFault");
        public ItemValueType<bool> DisChargeOverTemperatureFalut_Type { get; } = new ItemValueType<bool>("DisChargeOverTemperatureFalut");
        public ItemValueType<bool> DisChargeUnderTemperatureFalut_Type { get; } = new ItemValueType<bool>("DisChargeUnderTemperatureFalut");
        public ItemValueType<bool> ChargeOverPowerFault_Type { get; } = new ItemValueType<bool>("ChargeOverPowerFault");
        public ItemValueType<bool> DisChargeOverPowerFault_Type { get; } = new ItemValueType<bool>("DisChargeOverPowerFault");

        public ItemValueType<bool> RegenOverCurrentWarning_Type { get; } = new ItemValueType<bool>("RegenOverCurrentWarning");
        public ItemValueType<bool> DisChargeOverTemperatureWarning_Type { get; } = new ItemValueType<bool>("DisChargeOverTemperatureWarning");
        public ItemValueType<bool> DisChargeUnderTemperatureWarning_Type { get; } = new ItemValueType<bool>("DisChargeUnderTemperatureWarning");
        public ItemValueType<bool> ChargeOverPowerWarning_Type { get; } = new ItemValueType<bool>("ChargeOverPowerWarning");
        public ItemValueType<bool> DisChargeOverPowerWarning_Type { get; } = new ItemValueType<bool>("DisChargeOverPowerWarning");

        /// <summary>
        /// Holding Register 10523
        /// </summary>
        /// <param name="value">index 7</param>
        public void SetHoldingRegister(ushort value)
        {
            var bools = GetBools(value);

            RegenOverCurrentFault = bools[0];
            DisChargeOverTemperatureFalut = bools[1];
            DisChargeUnderTemperatureFalut = bools[2];
            ChargeOverPowerFault = bools[3];
            DisChargeOverPowerFault = bools[4];

            RegenOverCurrentWarning = bools[8];
            DisChargeOverTemperatureWarning = bools[9];
            DisChargeUnderTemperatureWarning = bools[10];
            ChargeOverPowerWarning = bools[11];
            DisChargeOverPowerWarning = bools[12];

            RegenOverCurrentFault_Type.SetValue(bools[0]);
            DisChargeOverTemperatureFalut_Type.SetValue(bools[1]);
            DisChargeUnderTemperatureFalut_Type.SetValue(bools[2]);
            ChargeOverPowerFault_Type.SetValue(bools[3]);
            DisChargeOverPowerFault_Type.SetValue(bools[4]);

            RegenOverCurrentWarning_Type.SetValue(bools[8]);
            DisChargeOverTemperatureWarning_Type.SetValue(bools[9]);
            DisChargeUnderTemperatureWarning_Type.SetValue(bools[10]);
            ChargeOverPowerWarning_Type.SetValue(bools[11]);
            DisChargeOverPowerWarning_Type.SetValue(bools[12]);
        }
    }

    public sealed class BMS_RBMS_SYSTEM_FAULT_BITS : BMS_BASE
    {
        private bool _BmsCommFault;
        private bool _ChgCommFault;
        private bool _RlyDTCFault;
        private bool _PreCHGFault;
        private bool _HassRefFault;
        private bool _DchSHDNFault;
        private bool _ChgSHDNFault;
        private bool _IsoResFault;
        private bool _CvDetecFault;
        private bool _CtDetecFault;
        private bool _PtDetecFault;
        private bool _SwellingFault;

        public bool BmsCommFault { get => _BmsCommFault; set { SetProperty(ref _BmsCommFault, value); } }
        public bool ChgCommFault { get => _ChgCommFault; set { SetProperty(ref _ChgCommFault, value); } }
        public bool RlyDTCFault { get => _RlyDTCFault; set { SetProperty(ref _RlyDTCFault, value); } }
        public bool PreCHGFault { get => _PreCHGFault; set { SetProperty(ref _PreCHGFault, value); } }
        public bool HassRefFault { get => _HassRefFault; set { SetProperty(ref _HassRefFault, value); } }
        public bool DchSHDNFault { get => _DchSHDNFault; set { SetProperty(ref _DchSHDNFault, value); } }
        public bool ChgSHDNFault { get => _ChgSHDNFault; set { SetProperty(ref _ChgSHDNFault, value); } }
        public bool IsoResFault { get => _IsoResFault; set { SetProperty(ref _IsoResFault, value); } }
        public bool CvDetecFault { get => _CvDetecFault; set { SetProperty(ref _CvDetecFault, value); } }
        public bool CtDetecFault { get => _CtDetecFault; set { SetProperty(ref _CtDetecFault, value); } }
        public bool PtDetecFault { get => _PtDetecFault; set { SetProperty(ref _PtDetecFault, value); } }
        public bool SwellingFault { get => _SwellingFault; set { SetProperty(ref _SwellingFault, value); } }

        public ItemValueType<bool> BmsCommFault_Type { get; } = new ItemValueType<bool>("BmsCommFault");
        public ItemValueType<bool> ChgCommFault_Type { get; } = new ItemValueType<bool>("ChgCommFault");
        public ItemValueType<bool> RlyDTCFault_Type { get; } = new ItemValueType<bool>("RlyDTCFault");
        public ItemValueType<bool> PreCHGFault_Type { get; } = new ItemValueType<bool>("PreCHGFault");
        public ItemValueType<bool> HassRefFault_Type { get; } = new ItemValueType<bool>("HassRefFault");
        public ItemValueType<bool> DchSHDNFault_Type { get; } = new ItemValueType<bool>("DchSHDNFault");
        public ItemValueType<bool> ChgSHDNFault_Type { get; } = new ItemValueType<bool>("ChgSHDNFault");
        public ItemValueType<bool> IsoResFault_Type { get; } = new ItemValueType<bool>("IsoResFault");
        public ItemValueType<bool> CvDetecFault_Type { get; } = new ItemValueType<bool>("CvDetecFault");
        public ItemValueType<bool> CtDetecFault_Type { get; } = new ItemValueType<bool>("CtDetecFault");
        public ItemValueType<bool> PtDetecFault_Type { get; } = new ItemValueType<bool>("PtDetecFault");
        public ItemValueType<bool> SwellingFault_Type { get; } = new ItemValueType<bool>("SwellingFault");

        /// <summary>
        /// Holding Register 10536
        /// </summary>
        /// <param name="value">index 20</param>
        public void SetHoldingRegister(ushort value)
        {
            var bools = GetBools(value);

            BmsCommFault = bools[0];
            ChgCommFault = bools[1];
            RlyDTCFault = bools[2];
            PreCHGFault = bools[3];
            HassRefFault = bools[4];
            DchSHDNFault = bools[6];
            ChgSHDNFault = bools[7];
            IsoResFault = bools[8];
            CvDetecFault = bools[9];
            CtDetecFault = bools[10];
            PtDetecFault = bools[11];
            SwellingFault = bools[12];

            BmsCommFault_Type.SetValue(bools[0]);
            ChgCommFault_Type.SetValue(bools[1]);
            RlyDTCFault_Type.SetValue(bools[2]);
            PreCHGFault_Type.SetValue(bools[3]);
            HassRefFault_Type.SetValue(bools[4]);
            DchSHDNFault_Type.SetValue(bools[6]);
            ChgSHDNFault_Type.SetValue(bools[7]);
            IsoResFault_Type.SetValue(bools[8]);
            CvDetecFault_Type.SetValue(bools[9]);
            CtDetecFault_Type.SetValue(bools[10]);
            PtDetecFault_Type.SetValue(bools[11]);
            SwellingFault_Type.SetValue(bools[12]);
        }
    }

    public sealed class BMS_RBMS_RACK_ONLINES : BMS_BASE
    {
        private bool _rack_01_online;
        private bool _rack_02_online;
        private bool _rack_03_online;
        private bool _rack_04_online;
        private bool _rack_05_online;
        private bool _rack_06_online;
        private bool _rack_07_online;
        private bool _rack_01_ChargeMode;
        private bool _rack_02_ChargeMode;
        private bool _rack_03_ChargeMode;
        private bool _rack_04_ChargeMode;
        private bool _rack_05_ChargeMode;
        private bool _rack_06_ChargeMode;
        private bool _rack_07_ChargeMode;

        public bool Rack_01_online { get => _rack_01_online; set { SetProperty(ref _rack_01_online, value); } }
        public bool Rack_02_online { get => _rack_02_online; set { SetProperty(ref _rack_02_online, value); } }
        public bool Rack_03_online { get => _rack_03_online; set { SetProperty(ref _rack_03_online, value); } }
        public bool Rack_04_online { get => _rack_04_online; set { SetProperty(ref _rack_04_online, value); } }
        public bool Rack_05_online { get => _rack_05_online; set { SetProperty(ref _rack_05_online, value); } }
        public bool Rack_06_online { get => _rack_06_online; set { SetProperty(ref _rack_06_online, value); } }
        public bool Rack_07_online { get => _rack_07_online; set { SetProperty(ref _rack_07_online, value); } }
        public bool Rack_01_ChargeMode { get => _rack_01_ChargeMode; set { SetProperty(ref _rack_01_ChargeMode, value); } }
        public bool Rack_02_ChargeMode { get => _rack_02_ChargeMode; set { SetProperty(ref _rack_02_ChargeMode, value); } }
        public bool Rack_03_ChargeMode { get => _rack_03_ChargeMode; set { SetProperty(ref _rack_03_ChargeMode, value); } }
        public bool Rack_04_ChargeMode { get => _rack_04_ChargeMode; set { SetProperty(ref _rack_04_ChargeMode, value); } }
        public bool Rack_05_ChargeMode { get => _rack_05_ChargeMode; set { SetProperty(ref _rack_05_ChargeMode, value); } }
        public bool Rack_06_ChargeMode { get => _rack_06_ChargeMode; set { SetProperty(ref _rack_06_ChargeMode, value); } }
        public bool Rack_07_ChargeMode { get => _rack_07_ChargeMode; set { SetProperty(ref _rack_07_ChargeMode, value); } }

        public ItemValueType<bool> Rack_01_online_Type { get; } = new ItemValueType<bool>("Rack_01_online");
        public ItemValueType<bool> Rack_02_online_Type { get; } = new ItemValueType<bool>("Rack_02_online");
        public ItemValueType<bool> Rack_03_online_Type { get; } = new ItemValueType<bool>("Rack_03_online");
        public ItemValueType<bool> Rack_04_online_Type { get; } = new ItemValueType<bool>("Rack_04_online");
        public ItemValueType<bool> Rack_05_online_Type { get; } = new ItemValueType<bool>("Rack_05_online");
        public ItemValueType<bool> Rack_06_online_Type { get; } = new ItemValueType<bool>("Rack_06_online");
        public ItemValueType<bool> Rack_07_online_Type { get; } = new ItemValueType<bool>("Rack_07_online");
        public ItemValueType<bool> Rack_01_ChargeMode_Type { get; } = new ItemValueType<bool>("Rack_01_ChargeMode");
        public ItemValueType<bool> Rack_02_ChargeMode_Type { get; } = new ItemValueType<bool>("Rack_02_ChargeMode");
        public ItemValueType<bool> Rack_03_ChargeMode_Type { get; } = new ItemValueType<bool>("Rack_03_ChargeMode");
        public ItemValueType<bool> Rack_04_ChargeMode_Type { get; } = new ItemValueType<bool>("Rack_04_ChargeMode");
        public ItemValueType<bool> Rack_05_ChargeMode_Type { get; } = new ItemValueType<bool>("Rack_05_ChargeMode");
        public ItemValueType<bool> Rack_06_ChargeMode_Type { get; } = new ItemValueType<bool>("Rack_06_ChargeMode");
        public ItemValueType<bool> Rack_07_ChargeMode_Type { get; } = new ItemValueType<bool>("Rack_07_ChargeMode");

        /// <summary>
        /// Holding Register 10537
        /// </summary>
        /// <param name="value">index 21</param>
        public void SetHoldingRegister(ushort value)
        {
            var bools = GetBools(value);

            Rack_01_online          = bools[0] ;
            Rack_02_online          = bools[1] ;
            Rack_03_online          = bools[2] ;
            Rack_04_online          = bools[3] ;
            Rack_05_online          = bools[4] ;
            Rack_06_online          = bools[5] ;
            Rack_07_online          = bools[6] ;
            Rack_01_ChargeMode      = bools[8] ;
            Rack_02_ChargeMode      = bools[9] ;
            Rack_03_ChargeMode      = bools[10];
            Rack_04_ChargeMode      = bools[11];
            Rack_05_ChargeMode      = bools[12];
            Rack_06_ChargeMode      = bools[13];
            Rack_07_ChargeMode      = bools[14];

            Rack_01_online_Type.SetValue(bools[0]);
            Rack_02_online_Type.SetValue(bools[1]);
            Rack_03_online_Type.SetValue(bools[2]);
            Rack_04_online_Type.SetValue(bools[3]);
            Rack_05_online_Type.SetValue(bools[4]);
            Rack_06_online_Type.SetValue(bools[5]);
            Rack_07_online_Type.SetValue(bools[6]);
            Rack_01_ChargeMode_Type.SetValue(bools[8]);
            Rack_02_ChargeMode_Type.SetValue(bools[9]);
            Rack_03_ChargeMode_Type.SetValue(bools[10]);
            Rack_04_ChargeMode_Type.SetValue(bools[11]);
            Rack_05_ChargeMode_Type.SetValue(bools[12]);
            Rack_06_ChargeMode_Type.SetValue(bools[13]);
            Rack_07_ChargeMode_Type.SetValue(bools[14]);
        }
    }

    public sealed class BMS_RBMS_RACK_DISCHARGEMODE : BMS_BASE
    {
        private bool _rack_01_DischargeMode;
        private bool _rack_02_DischargeMode;
        private bool _rack_03_DischargeMode;
        private bool _rack_04_DischargeMode;
        private bool _rack_05_DischargeMode;
        private bool _rack_06_DischargeMode;
        private bool _rack_07_DischargeMode;

        public bool Rack_01_DischargeMode { get => _rack_01_DischargeMode; set { SetProperty(ref _rack_01_DischargeMode, value); } }
        public bool Rack_02_DischargeMode { get => _rack_02_DischargeMode; set { SetProperty(ref _rack_02_DischargeMode, value); } }
        public bool Rack_03_DischargeMode { get => _rack_03_DischargeMode; set { SetProperty(ref _rack_03_DischargeMode, value); } }
        public bool Rack_04_DischargeMode { get => _rack_04_DischargeMode; set { SetProperty(ref _rack_04_DischargeMode, value); } }
        public bool Rack_05_DischargeMode { get => _rack_05_DischargeMode; set { SetProperty(ref _rack_05_DischargeMode, value); } }
        public bool Rack_06_DischargeMode { get => _rack_06_DischargeMode; set { SetProperty(ref _rack_06_DischargeMode, value); } }
        public bool Rack_07_DischargeMode { get => _rack_07_DischargeMode; set { SetProperty(ref _rack_07_DischargeMode, value); } }

        public ItemValueType<bool> Rack_01_DischargeMode_Type {get;} = new ItemValueType<bool>("Rack_01_DischargeMode");
        public ItemValueType<bool> Rack_02_DischargeMode_Type {get;} = new ItemValueType<bool>("Rack_02_DischargeMode");
        public ItemValueType<bool> Rack_03_DischargeMode_Type {get;} = new ItemValueType<bool>("Rack_03_DischargeMode");
        public ItemValueType<bool> Rack_04_DischargeMode_Type {get;} = new ItemValueType<bool>("Rack_04_DischargeMode");
        public ItemValueType<bool> Rack_05_DischargeMode_Type {get;} = new ItemValueType<bool>("Rack_05_DischargeMode");
        public ItemValueType<bool> Rack_06_DischargeMode_Type {get;} = new ItemValueType<bool>("Rack_06_DischargeMode");
        public ItemValueType<bool> Rack_07_DischargeMode_Type {get;} = new ItemValueType<bool>("Rack_07_DischargeMode");

        /// <summary>
        /// Holding Register 10538
        /// </summary>
        /// <param name="value">index 22</param>
        public void SetHoldingRegister(ushort value)
        {
            var bools = GetBools(value);

            Rack_01_DischargeMode = bools[0];
            Rack_02_DischargeMode = bools[1];
            Rack_03_DischargeMode = bools[2];
            Rack_04_DischargeMode = bools[3];
            Rack_05_DischargeMode = bools[4];
            Rack_06_DischargeMode = bools[5];
            Rack_07_DischargeMode = bools[6];

            Rack_01_DischargeMode_Type.SetValue(bools[0]);
            Rack_02_DischargeMode_Type.SetValue(bools[1]);
            Rack_03_DischargeMode_Type.SetValue(bools[2]);
            Rack_04_DischargeMode_Type.SetValue(bools[3]);
            Rack_05_DischargeMode_Type.SetValue(bools[4]);
            Rack_06_DischargeMode_Type.SetValue(bools[5]);
            Rack_07_DischargeMode_Type.SetValue(bools[6]);
        }
    }
}
