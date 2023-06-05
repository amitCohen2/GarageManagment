using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eCarColor
    {
        White,
        Yellow,
        Red,
        Black
    }


    public enum eNumOfDoors
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5
    }


    public class Car : Vehicle
    {

        public static readonly int sr_NumOfWheels = 5;
        public static readonly float sr_MaxAirPressure = 33f;
        public static readonly float sr_MaxFuelCapacity = 6.4f;
        public static readonly eFuelType sr_FuelType = eFuelType.Octan95;
        public static readonly float sr_MaxBattaryTime = 5.2f;
        eCarColor m_Color;
        eNumOfDoors m_Doors;
        public eCarColor Color { get { return m_Color; } }
        public eNumOfDoors Doors { get { return m_Doors; } }

        public eFuelType FuelType { get { return sr_FuelType; } }

        public Car(string i_ModelName, string i_LicenceNumber, float i_CurrentEnergyPercentage,
               List<Wheel> i_Wheels, EnergySource i_EnergySource, eCarColor i_Color, eNumOfDoors i_Doors) 
            : base(i_ModelName, i_LicenceNumber, i_CurrentEnergyPercentage, i_Wheels, i_EnergySource)
        {
            m_Color = i_Color;
            m_Doors = i_Doors;
        }
    }
}
