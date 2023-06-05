using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleInGarageException : Exception
    {
        private string m_LicenceNumber;

        public string LicenceNumber
        {
            get { return m_LicenceNumber; }
        }

        public VehicleInGarageException(Exception i_InnerException, string i_LicenceNumber) : base($"Vehicle is existing already!", i_InnerException)
        {
            m_LicenceNumber = i_LicenceNumber;
        }
    }
}
