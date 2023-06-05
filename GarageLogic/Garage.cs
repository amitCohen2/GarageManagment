using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        Dictionary<string, VehicleCard> m_VehicleCards;

        public Garage()
        {
            m_VehicleCards = new Dictionary<string, VehicleCard>();
        }

        public List<string> GetListLicenseByStatus(eVehicleStatus i_VehicleStatus, bool i_WholeGarage)
        {
            List<string> sortLicenseList = new List<string>();


            foreach (KeyValuePair<string, VehicleCard> pair in m_VehicleCards)
            {
                if (i_WholeGarage || pair.Value.VehicleStatus.Equals(i_VehicleStatus))
                {
                    sortLicenseList.Add(pair.Key);

                }
            }

            return sortLicenseList;
        }



        public void AddVehicleCard(string i_LicenceNumber, string i_OwnerName, string i_PhoneNumber, eVehicleStatus i_VehicleStatus, VehicleInfo i_VehicleInfo)
        {
            bool isVehicleCardExist = false;
            try
            {
                IsVehicleCardExist(ref isVehicleCardExist, i_LicenceNumber);
            }
            catch (VehicleInGarageException)
            {
                m_VehicleCards[i_LicenceNumber].VehicleStatus = eVehicleStatus.InRepair;
                isVehicleCardExist = false;
                throw new VehicleInGarageException(new Exception(), i_LicenceNumber);
            }

            if (!isVehicleCardExist)
            {
                Vehicle newVehicle = VehicleCreator.CreateNewVehicle(i_VehicleInfo);
                VehicleCard newVehicleCard = new VehicleCard(i_OwnerName, i_PhoneNumber, i_VehicleStatus, newVehicle);
                m_VehicleCards.Add(i_LicenceNumber, newVehicleCard);
            }

        }

        public Vehicle returnVehicle(string i_LicenceNumber, out VehicleCard i_vehicleCard)
        {
           
            Vehicle Vehicle = null;
            bool isVehicleCardExist = false;
            try
            {
                IsVehicleCardNotExist(ref isVehicleCardExist, i_LicenceNumber);
            }
            catch (VehicleInGarageException)
            {
                
                isVehicleCardExist = false;
                throw new VehicleInGarageException(new Exception(), i_LicenceNumber);
            }

            if (m_VehicleCards.TryGetValue(i_LicenceNumber, out i_vehicleCard))
            {
                Vehicle= i_vehicleCard.Vehicle;
            }
            return Vehicle;

        }
        public void IsVehicleCardExist(ref bool i_IsVehicleCardExist, string i_LicenceNumber)
        {
            if (m_VehicleCards.ContainsKey(i_LicenceNumber))
            {
                i_IsVehicleCardExist = true;
                throw new VehicleInGarageException(new Exception(), i_LicenceNumber);
            }
            else
            {
                i_IsVehicleCardExist = false;
            }
        }

        public void IsVehicleCardNotExist(ref bool i_IsVehicleCardNotExist, string i_LicenceNumber)
        {
            if (!m_VehicleCards.ContainsKey(i_LicenceNumber))
            {
                i_IsVehicleCardNotExist = true;
                throw new VehicleDoesNotInGarageException(new Exception(), i_LicenceNumber);//VehicleDoesNotInGarageException
            }
            else
            {
                i_IsVehicleCardNotExist = false;
            }
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, eVehicleStatus i_NewStatus)
        {
            if (false == m_VehicleCards.ContainsKey(i_LicenseNumber))
            {
                throw new VehicleDoesNotInGarageException(new Exception(), i_LicenseNumber);
            }
            else if (m_VehicleCards[i_LicenseNumber].VehicleStatus == i_NewStatus)
            {
                throw new FormatException();
            }
            else
            {
                m_VehicleCards[i_LicenseNumber].VehicleStatus = i_NewStatus;
            }
        }

        public void RefuelVehicle(string i_LicenceNumber, eFuelType i_FuelType, float i_RefuelAmount, out float i_newAmount)
        {
            if (false == m_VehicleCards.ContainsKey(i_LicenceNumber))
            {
                throw new VehicleDoesNotInGarageException(new Exception(), i_LicenceNumber);
            }
            else
            {
                Fuel engineFuel = m_VehicleCards[i_LicenceNumber].Vehicle.Energy as Fuel;
                if (engineFuel == null)
                {
                    throw new FormatException();
                }

                try
                {
                    engineFuel.ReFuel(i_RefuelAmount, i_FuelType, out i_newAmount);
                }
                catch(IncorrectFuelTypeException ex)
                {
                    throw ex;
                }
                catch (ValueOutOfRangeException ex)
                {
                    throw ex;
                }
            }
        }

        public void rechargeVehicle(string i_LicenceNumber, float i_rechargeAmount, out  float i_newAmount)
        {
            if (false == m_VehicleCards.ContainsKey(i_LicenceNumber))
            {
                throw new VehicleDoesNotInGarageException(new Exception(), i_LicenceNumber);
            }
            else
            {
                Electricity engineFuel = m_VehicleCards[i_LicenceNumber].Vehicle.Energy as Electricity;
                if (engineFuel == null)
                {
                    throw new FormatException();
                }

                try
                {
                    engineFuel.Recharge(i_rechargeAmount, out i_newAmount);
                }
                catch (IncorrectFuelTypeException ex)
                {
                    throw ex;
                }
                catch (ValueOutOfRangeException ex)
                {
                    throw ex;
                }
            }
        }


        public void InflateAirToMax(string i_LicenseNumber)
        {
            if (false == m_VehicleCards.ContainsKey(i_LicenseNumber))
            {
                throw new VehicleDoesNotInGarageException(new Exception(), i_LicenseNumber);
            }
            else
            {
                Vehicle currVehicle = m_VehicleCards[i_LicenseNumber].Vehicle;
                List<Wheel> currWheels = currVehicle.Wheels;

                foreach (Wheel wheel in currWheels)
                {
                    wheel.InflatToMax();
                }
            }
        }
    }
}
