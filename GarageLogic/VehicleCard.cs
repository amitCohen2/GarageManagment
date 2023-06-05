using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eVehicleStatus
    {
        InRepair,
        Repaired,
        Paid
    }
   public class VehicleCard
    {
        string m_OwnerName, m_PhoneNumber;
        eVehicleStatus m_VehicleStatus;
        private Vehicle m_Vehicle;

        public Vehicle Vehicle { get { return m_Vehicle; } }
        //public eVehicleStatus VehicleStatus { get { return m_VehicleStatus; } }
        public eVehicleStatus VehicleStatus { get; set; }
        public string OwnerName { get { return m_OwnerName; } }

        public string PhoneNumber { get { return m_PhoneNumber; } }


        public VehicleCard(string i_OwnerName, string i_PhoneNumber, eVehicleStatus i_VehicleStatus, Vehicle i_Vehicle)
        {
            m_OwnerName = i_OwnerName;
            m_PhoneNumber = i_PhoneNumber;
            m_VehicleStatus = i_VehicleStatus;
            m_Vehicle = i_Vehicle;

        }
    }
}
