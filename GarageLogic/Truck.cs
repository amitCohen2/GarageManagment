using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        public static readonly int sr_NumOfWheels = 14;
        public static readonly float sr_MaxAirPressure = 26;
        public static readonly float sr_MaxFuelCapacity = 135;
        public static readonly eFuelType sr_FuelType = eFuelType.Soler;

        bool m_IsContainsDangerousMaterials;
        float m_TrunkVolume;
        public bool IsContainsDangerousMaterials { get { return m_IsContainsDangerousMaterials; } }
        public eFuelType FuelType { get { return sr_FuelType; } }
        public Truck(
       string i_ModelName, string i_LicenceNumber, float i_CurrentEnergyPercentage,
       List<Wheel> i_Wheels, EnergySource i_EnergySource, bool i_IsContainsDangerousMaterials, float i_TrunkVolume)
    : base(i_ModelName, i_LicenceNumber, i_CurrentEnergyPercentage, i_Wheels, i_EnergySource)
        {
            m_IsContainsDangerousMaterials = i_IsContainsDangerousMaterials;
            m_TrunkVolume = i_TrunkVolume;
        }
    }
}
