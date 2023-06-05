using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eMotorciclyLicenceType
    {
        A1,
        A2,
        AA,
        B1
    }

   public class Motorcycle : Vehicle
    {

       

        public static readonly int sr_NumOfWheels = 2;
        public static readonly float sr_MaxAirPressure = 31f;
        public static readonly float sr_MaxFuelCapacity = 6.4f;
        public static readonly eFuelType sr_FuelType = eFuelType.Octan98;
        public static readonly float sr_MaxBattaryTime = 2.6f;
        int m_EngineCapacity;
        eMotorciclyLicenceType m_LicenceType;
        public int EngineCapacity { get { return m_EngineCapacity; } }
        public eFuelType FuelType { get { return sr_FuelType; } }

        public eMotorciclyLicenceType MotorciclyLicenceType { get { return m_LicenceType; } }
        public Motorcycle(string i_ModelName, string i_LicenceNumber, float i_CurrentEnergyPercentage,
                  List<Wheel> i_Wheels, EnergySource i_EnergySource,
                  int i_EngineCapacity, eMotorciclyLicenceType i_LicenceType)
            : base(i_ModelName, i_LicenceNumber, i_CurrentEnergyPercentage, i_Wheels, i_EnergySource)
        {
            m_EngineCapacity = i_EngineCapacity;
            m_LicenceType = i_LicenceType;
        }

    }
}
