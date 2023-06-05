using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleDoesNotInGarageException : Exception
    {
            
        private string m_LicenceNumber;

        public string LicenceNumber
        {
            get { return m_LicenceNumber; }
        }

        public VehicleDoesNotInGarageException(Exception i_InnerException, string i_LicenceNumber) : base($"Vehicle Does Not Exist!", i_InnerException)
        {
            m_LicenceNumber = i_LicenceNumber;
        }
    
    }
}
