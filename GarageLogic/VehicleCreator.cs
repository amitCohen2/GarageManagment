using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public static class VehicleCreator
    {

        public static Vehicle CreateNewVehicle(VehicleInfo i_VehicleInfo)
        {
            Vehicle o_NewVehicle = null;
            List<Wheel> wheelsList = createWheelsList(i_VehicleInfo);
            EnergySource energySource = createEnergySource(i_VehicleInfo);

            if (i_VehicleInfo.m_VehicleType == eVehicleType.CarFuel || i_VehicleInfo.m_VehicleType == eVehicleType.CarElectric)
            {
                o_NewVehicle = new Car(i_VehicleInfo.m_ModelName, i_VehicleInfo.m_LicenseNumber, i_VehicleInfo.m_EnergyPercentage,
                                                           wheelsList, energySource, i_VehicleInfo.m_Color, i_VehicleInfo.m_Doors);
            }
            else if (i_VehicleInfo.m_VehicleType == eVehicleType.MotorcycleFuel || i_VehicleInfo.m_VehicleType == eVehicleType.MotorcycleElectric)
            {
                o_NewVehicle = new Motorcycle(i_VehicleInfo.m_ModelName,i_VehicleInfo.m_LicenseNumber, i_VehicleInfo.m_EnergyPercentage, wheelsList,
                                              energySource,  i_VehicleInfo.m_EngineCapacity, i_VehicleInfo.m_LicenseType);
            }
            else if (i_VehicleInfo.m_VehicleType == eVehicleType.Truck)
            {
                o_NewVehicle = new Truck(i_VehicleInfo.m_ModelName, i_VehicleInfo.m_LicenseNumber, i_VehicleInfo.m_EnergyPercentage, wheelsList, energySource,
                                         i_VehicleInfo.m_IsContainsDangerousMaterials, i_VehicleInfo.m_CargoVolume);
            }
            
            return o_NewVehicle;
        }

        private static List<Wheel> createWheelsList(VehicleInfo i_VehicleInfo)
        {
            List<Wheel> wheelsList = new List<Wheel>();
            int numOfWheels = 0;
            float maxAirPressure = 0;

            if (i_VehicleInfo.m_VehicleType == eVehicleType.CarFuel || i_VehicleInfo.m_VehicleType == eVehicleType.CarElectric)
            {
                numOfWheels = Car.sr_NumOfWheels;
                maxAirPressure = Car.sr_MaxAirPressure;
            }
            else if (i_VehicleInfo.m_VehicleType == eVehicleType.MotorcycleFuel || i_VehicleInfo.m_VehicleType == eVehicleType.MotorcycleElectric)
            {
                numOfWheels = Motorcycle.sr_NumOfWheels;
                maxAirPressure = Motorcycle.sr_MaxAirPressure;
            }
            else if (i_VehicleInfo.m_VehicleType == eVehicleType.Truck)
            {
                numOfWheels = Truck.sr_NumOfWheels;
                maxAirPressure = Truck.sr_MaxAirPressure;
            }
            
            for (int i = 0; i < numOfWheels; i++)
            {
                Wheel newWheel = new Wheel(i_VehicleInfo.m_WheelManufacturerName, i_VehicleInfo.m_CurrentAirPressure, maxAirPressure);
                wheelsList.Add(newWheel);
            }

            return wheelsList;
        }

        private static EnergySource createEnergySource(VehicleInfo i_VehicleInfo)
        {
            EnergySource energySource = null;
            eFuelType fuelType = 0;
            float maxFuelCapacity = 0;
            float maxBatteryTime = 0;


            if (i_VehicleInfo.m_VehicleType == eVehicleType.CarFuel)
            {
                fuelType = Car.sr_FuelType;
                maxFuelCapacity = Car.sr_MaxFuelCapacity;
            }
            else if (i_VehicleInfo.m_VehicleType == eVehicleType.CarElectric)
            {
                maxBatteryTime = Car.sr_MaxBattaryTime;

            }
            else if (i_VehicleInfo.m_VehicleType == eVehicleType.MotorcycleFuel)
            {
                fuelType = Motorcycle.sr_FuelType;
                maxFuelCapacity = Motorcycle.sr_MaxFuelCapacity;
            }
            else if (i_VehicleInfo.m_VehicleType == eVehicleType.MotorcycleElectric)
            {
                maxBatteryTime = Motorcycle.sr_MaxBattaryTime;

            }
            else if (i_VehicleInfo.m_VehicleType == eVehicleType.Truck)
            {
                fuelType = Truck.sr_FuelType;
                maxFuelCapacity = Truck.sr_MaxFuelCapacity;
            }
            

            if (i_VehicleInfo.m_VehicleType == eVehicleType.CarFuel || i_VehicleInfo.m_VehicleType == eVehicleType.MotorcycleFuel || i_VehicleInfo.m_VehicleType == eVehicleType.Truck)
            {
                energySource = new Fuel(maxFuelCapacity, i_VehicleInfo.m_CurrentFuelCapacity, fuelType); 
            }
            else if (i_VehicleInfo.m_VehicleType == eVehicleType.CarElectric || i_VehicleInfo.m_VehicleType == eVehicleType.MotorcycleElectric)
            {
                energySource = new Electricity(maxBatteryTime, i_VehicleInfo.m_RemainingBatteryTime);
            }           

            return energySource;
        }
    }
}
