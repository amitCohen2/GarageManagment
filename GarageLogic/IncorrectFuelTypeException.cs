using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class IncorrectFuelTypeException: Exception
    {
        private eFuelType m_FuelType;

        public eFuelType FuelType
        {
            get { return m_FuelType; }
        }

        public IncorrectFuelTypeException(Exception i_InnerException, eFuelType i_FuelType)
                                        : base($"Incorrect Fuel Type!", i_InnerException)
        {
            m_FuelType = i_FuelType;
        }
    }
}
