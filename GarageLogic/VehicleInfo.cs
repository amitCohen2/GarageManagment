using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eVehicleType
    {
        CarFuel,
        CarElectric,
        Truck,
        MotorcycleFuel,
        MotorcycleElectric

    }
    public class VehicleInfo
    {
        public float m_EnergyPercentage;
        public eVehicleType m_VehicleType;
        public string m_ModelName;
        public string m_LicenseNumber;
        public int m_NumberOfWheels;
        public float m_CurrentAirPressure;
        public string m_WheelManufacturerName;
        public eFuelType m_FuelType;
        public eCarColor m_Color;
        public eNumOfDoors m_Doors;
        public eMotorciclyLicenceType m_LicenseType;
        public int m_EngineCapacity;
        public bool m_IsContainsDangerousMaterials;
        public float m_CargoVolume;
        public float m_CurrentFuelCapacity;
        public float m_RemainingBatteryTime;     
    }


        
            
        
}
