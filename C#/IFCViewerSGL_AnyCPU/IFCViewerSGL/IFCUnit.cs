using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using IfcEngine;

namespace IFCViewerSGL
{
    /// <summary>
    /// Describes an unit
    /// </summary>
    public class IFCUnit
    {
        #region Constants

        public const int UNKNOWN = 0;
        public const int ABSORBEDDOSEUNIT = 101;
        public const int AREAUNIT = 102;
        public const int DOSEEQUIVALENTUNIT = 103;
        public const int ELECTRICCAPACITANCEUNIT = 104;
        public const int ELECTRICCHARGEUNIT = 105;
        public const int ELECTRICCONDUCTANCEUNIT = 106;
        public const int ELECTRICCURRENTUNIT = 107;
        public const int ELECTRICRESISTANCEUNIT = 108;
        public const int ELECTRICVOLTAGEUNIT = 109;
        public const int ENERGYUNIT = 110;
        public const int FORCEUNIT = 111;
        public const int FREQUENCYUNIT = 112;
        public const int ILLUMINANCEUNIT = 113;
        public const int INDUCTANCEUNIT = 114;
        public const int LENGTHUNIT = 115;
        public const int LUMINOUSFLUXUNIT = 116;
        public const int LUMINOUSINTENSITYUNIT = 117;
        public const int MAGNETICFLUXDENSITYUNIT = 118;
        public const int MAGNETICFLUXUNIT = 119;
        public const int MASSUNIT = 120;
        public const int PLANEANGLEUNIT = 121;
        public const int POWERUNIT = 122;
        public const int PRESSUREUNIT = 123;
        public const int RADIOACTIVITYUNIT = 124;
        public const int SOLIDANGLEUNIT = 125;
        public const int THERMODYNAMICTEMPERATUREUNIT = 126;
        public const int TIMEUNIT = 127;
        public const int VOLUMEUNIT = 128;
        public const int USERDEFINED = 129;

        #endregion // Constants

        #region Fields

        /// <summary>
        /// Type
        /// </summary>
        int _iType = 0;

        /// <summary>
        /// Type
        /// </summary>
        string _strType = string.Empty;

        /// <summary>
        /// Prefix
        /// </summary>
        string _strPrefix = string.Empty;

        /// <summary>
        /// Name
        /// </summary>
        string _strName = string.Empty;

        #endregion // Fields

        #region Methods

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="strPrefix"></param>
        /// <param name="strName"></param>
        public IFCUnit(string strType, string strPrefix, string strName)
        {
            ConvertType(strType);
            ConvertPrefix(strPrefix);
            ConvertName(strName);
        }

        /// <summary>
        /// Getter
        /// </summary>
        public int Type
        {
            get
            {
                return _iType;
            }
        }

        /// <summary>
        /// Getter
        /// </summary>
        public string TypeAsStr
        {
            get
            {
                return _strType;
            }
        }

        /// <summary>
        /// Getter
        /// </summary>
        public string Prefix
        {
            get
            {
                return _strPrefix;
            }
        }

        /// <summary>
        /// Getter
        /// </summary>
        public string Name
        {
            get
            {
                return _strName;
            }
        }

        /// <summary>
        /// Getter
        /// </summary>
        public string Unit
        {
            get
            {
                string strUnit = _strPrefix;
                if (!string.IsNullOrEmpty(strUnit))
                {
                    strUnit += " ";
                }

                strUnit += _strName;

                return strUnit;
            }
        }

        /// <summary>
        /// Loads the units
        /// </summary>
        /// <param name="iModel"></param>
        /// <param name="iProject"></param>
        /// <returns></returns>
        public static Dictionary<string, IFCUnit> LoadUnits(long iModel, long iProject)
        {
            Dictionary<string, IFCUnit> dicUnits = new Dictionary<string, IFCUnit>();

            IntPtr unitsInContext;
            IfcEngineAnyCPU.sdaiGetAttrBN(iProject, "UnitsInContext", IfcEngineAnyCPU.sdaiINSTANCE, out unitsInContext);

            IntPtr units;
            IfcEngineAnyCPU.sdaiGetAttrBN((long)unitsInContext, "Units", IfcEngineAnyCPU.sdaiAGGR, out units);

            long iUnitsCount = IfcEngineAnyCPU.sdaiGetMemberCount((long)units);
            for (long iUnit = 0; iUnit < iUnitsCount; iUnit++)
            {
                long iUnitInstance;
                IfcEngineAnyCPU.engiGetAggrElement((long)units, iUnit, IfcEngineAnyCPU.sdaiINSTANCE, out iUnitInstance);

                if (IsInstanceOf(iModel, iUnitInstance, "IFCCONVERSIONBASEDUNIT"))
                {
                    long iConversionFactorInstance = 0;
                    IfcEngineAnyCPU.sdaiGetAttrBN(iUnitInstance, "ConversionFactor", IfcEngineAnyCPU.sdaiINSTANCE, out iConversionFactorInstance);

                    if (iConversionFactorInstance != 0)
                    {
                        long iUnitComponentInstance;
                        IfcEngineAnyCPU.sdaiGetAttrBN(iConversionFactorInstance, "UnitComponent", IfcEngineAnyCPU.sdaiINSTANCE, out iUnitComponentInstance);

                        if (IsInstanceOf(iModel, iUnitComponentInstance, "IFCSIUNIT"))
                        {
                            IntPtr unitType;
                            IfcEngineAnyCPU.sdaiGetAttrBN(iUnitComponentInstance, "UnitType", IfcEngineAnyCPU.sdaiUNICODE, out unitType);

                            string strUnitType = string.Empty;
                            if (unitType != IntPtr.Zero)
                            {
                                strUnitType = Marshal.PtrToStringUni(unitType);
                            }

                            IntPtr prefix;
                            IfcEngineAnyCPU.sdaiGetAttrBN(iUnitComponentInstance, "Prefix", IfcEngineAnyCPU.sdaiUNICODE, out prefix);

                            string strPrefix = string.Empty;
                            if (prefix != IntPtr.Zero)
                            {
                                strPrefix = Marshal.PtrToStringUni(prefix);
                            }

                            IntPtr name;
                            IfcEngineAnyCPU.sdaiGetAttrBN(iUnitComponentInstance, "Name", IfcEngineAnyCPU.sdaiUNICODE, out name);

                            string strName = string.Empty;
                            if (name != IntPtr.Zero)
                            {
                                strName = Marshal.PtrToStringUni(name);
                            }

                            IFCUnit ifcUnit = new IFCUnit(strUnitType, strPrefix, strName);
                            dicUnits[ifcUnit.TypeAsStr] = ifcUnit;
                        }                        
                    }
                } // if (IsInstanceOf(iModel, iUnitInstance, "IFCCONVERSIONBASEDUNIT"))
                else
                {
                    if (IsInstanceOf(iModel, iUnitInstance, "IFCSIUNIT"))
                    {
                        IntPtr unitType;
                        IfcEngineAnyCPU.sdaiGetAttrBN(iUnitInstance, "UnitType", IfcEngineAnyCPU.sdaiUNICODE, out unitType);

                        string strUnitType = string.Empty;
                        if (unitType != IntPtr.Zero)
                        {
                            strUnitType = Marshal.PtrToStringUni(unitType);
                        }

                        IntPtr prefix;
                        IfcEngineAnyCPU.sdaiGetAttrBN(iUnitInstance, "Prefix", IfcEngineAnyCPU.sdaiUNICODE, out prefix);

                        string strPrefix = string.Empty;
                        if (prefix != IntPtr.Zero)
                        {
                            strPrefix = Marshal.PtrToStringUni(prefix);
                        }

                        IntPtr name;
                        IfcEngineAnyCPU.sdaiGetAttrBN(iUnitInstance, "Name", IfcEngineAnyCPU.sdaiUNICODE, out name);

                        string strName = string.Empty;
                        if (name != IntPtr.Zero)
                        {
                            strName = Marshal.PtrToStringUni(name);
                        }

                        IFCUnit ifcUnit = new IFCUnit(strUnitType, strPrefix, strName);
                        dicUnits[ifcUnit.TypeAsStr] = ifcUnit;
                    } // if (IsInstanceOf(iModel, iUnitInstance, "IFCSIUNIT"))
                } // if (IsInstanceOf(iModel, iUnitInstance, "IFCSIUNIT"))
            } // for (long iUnit = ...

            return dicUnits;
        }

        /// <summary>
        /// Converter
        /// </summary>
        /// <param name="strType"></param>
        /// <returns></returns>
        private void ConvertType(string strType)
        {
            if (strType == ".ABSORBEDDOSEUNIT.")
            {
                _iType = ABSORBEDDOSEUNIT;
                _strType = "ABSORBEDDOSEUNIT";

		        return;
            }

            if (strType == ".AREAUNIT.")
            {
                _iType = AREAUNIT;
		        _strType = "AREAUNIT";

		        return;
            }

            if (strType == ".DOSEEQUIVALENTUNIT.")
            {
                _iType = DOSEEQUIVALENTUNIT;
		        _strType = "DOSEEQUIVALENTUNIT";

		        return;
            }

            if (strType == ".ELECTRICCAPACITANCEUNIT.")
            {
                _iType = ELECTRICCAPACITANCEUNIT;
		        _strType = "ELECTRICCAPACITANCEUNIT";

		        return;
            }

            if (strType == ".ELECTRICCHARGEUNIT.")
            {
                _iType = ELECTRICCHARGEUNIT;
		        _strType = "ELECTRICCHARGEUNIT";

		        return;
            }

            if (strType == ".ELECTRICCONDUCTANCEUNIT.")
            {
                _iType = ELECTRICCONDUCTANCEUNIT;
		        _strType = "ELECTRICCONDUCTANCEUNIT";

		        return;
            }

            if (strType == ".ELECTRICCURRENTUNIT.")
            {
                _iType = ELECTRICCURRENTUNIT;
		        _strType = "ELECTRICCURRENTUNIT";

		        return;
            }

            if (strType == ".ELECTRICRESISTANCEUNIT.")
            {
                _iType = ELECTRICRESISTANCEUNIT;
		        _strType = "ELECTRICRESISTANCEUNIT";

		        return;
            }

            if (strType == ".ELECTRICVOLTAGEUNIT.")
            {
                _iType = ELECTRICVOLTAGEUNIT;
		        _strType = "ELECTRICVOLTAGEUNIT";

		        return;
            }

            if (strType == ".ENERGYUNIT.")
            {
                _iType = ENERGYUNIT;
		        _strType = "ENERGYUNIT";

		        return;
            }

            if (strType == ".FORCEUNIT.")
            {
                _iType = FORCEUNIT;
		        _strType = "FORCEUNIT";

		        return;
            }

            if (strType == ".FREQUENCYUNIT.")
            {
                _iType = FREQUENCYUNIT;
		        _strType = "FREQUENCYUNIT";

		        return;
            }

            if (strType == ".ILLUMINANCEUNIT.")
            {
                _iType = ILLUMINANCEUNIT;
		        _strType = "ILLUMINANCEUNIT";

		        return;
            }

            if (strType == ".INDUCTANCEUNIT.")
            {
                _iType = INDUCTANCEUNIT;
		        _strType = "INDUCTANCEUNIT";

		        return;
            }

            if (strType == ".LENGTHUNIT.")
            {
                _iType = LENGTHUNIT;
		        _strType = "LENGTHUNIT";

		        return;
            }

            if (strType == ".LUMINOUSFLUXUNIT.")
            {
                _iType = LUMINOUSFLUXUNIT;
		        _strType = "LUMINOUSFLUXUNIT";

		        return;
            }

            if (strType == ".LUMINOUSINTENSITYUNIT.")
            {
                _iType = LUMINOUSINTENSITYUNIT;
		        _strType = "LUMINOUSINTENSITYUNIT";

		        return;
            }

            if (strType == ".MAGNETICFLUXDENSITYUNIT.")
            {
                _iType = MAGNETICFLUXDENSITYUNIT;
		        _strType = "MAGNETICFLUXDENSITYUNIT";

		        return;
            }

            if (strType == ".MAGNETICFLUXUNIT.")
            {
                _iType = MAGNETICFLUXUNIT;
		        _strType = "MAGNETICFLUXUNIT";

		        return;
            }

            if (strType == ".MASSUNIT.")
            {
                _iType = MASSUNIT;
		        _strType = "MASSUNIT";

		        return;
            }

            if (strType == ".PLANEANGLEUNIT.")
            {
                _iType = PLANEANGLEUNIT;
		        _strType = "PLANEANGLEUNIT";

		        return;
            }

            if (strType == ".POWERUNIT.")
            {
                _iType = POWERUNIT;
		        _strType = "POWERUNIT";

		        return;
            }

            if (strType == ".PRESSUREUNIT.")
            {
                _iType = PRESSUREUNIT;
		        _strType = "PRESSUREUNIT";

		        return;
            }

            if (strType == ".RADIOACTIVITYUNIT.")
            {
                _iType = RADIOACTIVITYUNIT;
		        _strType = "RADIOACTIVITYUNIT";

		        return;
            }

            if (strType == ".SOLIDANGLEUNIT.")
            {
                _iType = SOLIDANGLEUNIT;
		        _strType = "SOLIDANGLEUNIT";

		        return;
            }

            if (strType == ".THERMODYNAMICTEMPERATUREUNIT.")
            {
                _iType = THERMODYNAMICTEMPERATUREUNIT;
		        _strType = "THERMODYNAMICTEMPERATUREUNIT";

		        return;
            }

            if (strType == ".TIMEUNIT.")
            {
                _iType = TIMEUNIT;
		        _strType = "TIMEUNIT";

		        return;
            }

            if (strType == ".VOLUMEUNIT.")
            {
                _iType = VOLUMEUNIT;
		        _strType = "VOLUMEUNIT";

		        return;
            }

            if (strType == ".USERDEFINED.")
            {
                _iType = USERDEFINED;
		        _strType = "USERDEFINED";

		        return;
            }

            _iType = UNKNOWN;
	        _strType = "UNKNOWN";
        }

        /// <summary>
        /// Converter
        /// </summary>
        /// <param name="strType"></param>
        /// <returns></returns>
        private void ConvertPrefix(string strPrefix)
        {
            if (string.IsNullOrEmpty(strPrefix))
            {
                return;
            }

            if (strPrefix == ".EXA.")
            {
                _strPrefix = "Exa";

                return;
            }

            if (strPrefix == ".PETA.")
            {
                _strPrefix = "Peta";

                return;
            }

            if (strPrefix == ".TERA.")
            {
                _strPrefix = "Tera";

                return;
            }

            if (strPrefix == ".GIGA.")
            {
                _strPrefix = "Giga";

                return;
            }

            if (strPrefix == ".MEGA.")
            {
                _strPrefix = "Mega";

                return;
            }

            if (strPrefix == ".KILO.")
            {
                _strPrefix = "Kilo";

                return;
            }

            if (strPrefix == ".HECTO.")
            {
                _strPrefix = "Hecto";

                return;
            }

            if (strPrefix == ".DECA.")
            {
                _strPrefix = "Deca";

                return;
            }

            if (strPrefix == ".DECI.")
            {
                _strPrefix = "Deci";

                return;
            }

            if (strPrefix == ".CENTI.")
            {
                _strPrefix = "Centi";

                return;
            }

            if (strPrefix == ".MILLI.")
            {
                _strPrefix = "Milli";

                return;
            }

            if (strPrefix == ".MICRO.")
            {
                _strPrefix = "Micro";

                return;
            }

            if (strPrefix == ".NANO.")
            {
                _strPrefix = "Nano";

                return;
            }

            if (strPrefix == ".PICO.")
            {
                _strPrefix = "Pico";

                return;
            }

            if (strPrefix == ".FEMTO.")
            {
                _strPrefix = "Femto";

                return;
            }

            if (strPrefix == ".ATTO.")
            {
                _strPrefix = "Atto";

                return;
            }
        }

        /// <summary>
        /// Converter
        /// </summary>
        /// <param name="strType"></param>
        /// <returns></returns>
        private void ConvertName(string strName)
        {
            if (strName == ".AMPERE.")
            {
                _strName = "Ampere";

                return;
            }

            if (strName == ".BECQUEREL.")
            {
                _strName = "Becquerel";

                return;
            }

            if (strName == ".CANDELA.")
            {
                _strName = "Candela";

                return;
            }

            if (strName == ".COULOMB.")
            {
                _strName = "Coulomb";

                return;
            }

            if (strName == ".CUBIC_METRE.")
            {
                _strName = "Cubic Metre";

                return;
            }

            if (strName == ".DEGREE_CELSIUS.")
            {
                _strName = "Degree Celcius";

                return;
            }

            if (strName == ".FARAD.")
            {
                _strName = "Farad";

                return;
            }

            if (strName == ".GRAM.")
            {
                _strName = "Gram";

                return;
            }

            if (strName == ".GRAY.")
            {
                _strName = "Gray";

                return;
            }

            if (strName == ".HENRY.")
            {
                _strName = "Henry";

                return;
            }

            if (strName == ".HERTZ.")
            {
                _strName = "Hertz";

                return;
            }

            if (strName == ".JOULE.")
            {
                _strName = "Joule";

                return;
            }

            if (strName == ".KELVIN.")
            {
                _strName = "Kelvin";

                return;
            }

            if (strName == ".LUMEN.")
            {
                _strName = "Lumen";

                return;
            }

            if (strName == ".LUX.")
            {
                _strName = "Lux";

                return;
            }

            if (strName == ".METRE.")
            {
                _strName = "Metre";

                return;
            }

            if (strName == ".MOLE.")
            {
                _strName = "Mole";

                return;
            }

            if (strName == ".NEWTON.")
            {
                _strName = "Newton";

                return;
            }

            if (strName == ".OHM.")
            {
                _strName = "Ohm";

                return;
            }

            if (strName == ".PASCAL.")
            {
                _strName = "Pascal";

                return;
            }

            if (strName == ".RADIAN.")
            {
                _strName = "Radian";

                return;
            }

            if (strName == ".SECOND.")
            {
                _strName = "Second";

                return;
            }

            if (strName == ".SIEMENS.")
            {
                _strName = "Siemens";

                return;
            }

            if (strName == ".SIEVERT.")
            {
                _strName = "Sievert";

                return;
            }

            if (strName == ".SQUARE_METRE.")
            {
                _strName = "Square Metre";

                return;
            }

            if (strName == ".STERADIAN.")
            {
                _strName = "Steradian";

                return;
            }

            if (strName == ".TESLA.")
            {
                _strName = "Tesla";

                return;
            }

            if (strName == ".VOLT.")
            {
                _strName = "Volt";

                return;
            }

            if (strName == ".WATT.")
            {
                _strName = "Watt";

                return;
            }

            if (strName == ".WEBER.")
            {
                _strName = "Weber";

                return;
            }

            _strName = strName;
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="iInstance"></param>
        /// <param name="strType"></param>
        /// <returns></returns>
        private static bool IsInstanceOf(long iModel, long iInstance, string strType)
        {
            if (IfcEngineAnyCPU.sdaiGetInstanceType(iInstance) == IfcEngineAnyCPU.sdaiGetEntity(iModel, strType))
            {
                return true;
            }

            return false;
        }  

        #endregion // Methods
    }
}
