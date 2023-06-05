using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{

    public class Vehicle
    {
        string m_ModelName;
        readonly string mr_LicenceNumber;
        float m_CurrentEnergyPercentage;
        List<Wheel> m_Wheels;
        EnergySource m_EnergySource;
       

        public List<Wheel> Wheels { get { return m_Wheels; } set { m_Wheels = value; } }
        public EnergySource Energy {  get { return m_EnergySource; } }
        public string LicenceNumber { get { return mr_LicenceNumber; } }
        public string ModelName { get { return m_ModelName; } }
        public Vehicle(string i_ModelName, string i_LicenceNumber, float i_CurrentEnergyPercentage, List<Wheel> i_Wheels, EnergySource i_EnergySource)
        {
            m_ModelName = i_ModelName;
            mr_LicenceNumber = i_LicenceNumber;
            m_CurrentEnergyPercentage = i_CurrentEnergyPercentage;
            m_Wheels = i_Wheels;
            m_EnergySource = i_EnergySource;
        }
      
    }


}
