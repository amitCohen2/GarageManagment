using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eFuelType
    {
        Octan96,
        Octan95,
        Octan98,
        Soler
    }
    class Fuel : EnergySource
    {
        eFuelType m_FuelType;

        public Fuel(float i_MaxEnergy, float i_CurrentEnergy, eFuelType i_FuelType) : base(i_MaxEnergy, i_CurrentEnergy)
        {
            m_FuelType = i_FuelType;
        }

        public void ReFuel(float i_FuelAmountRequest, eFuelType i_FuelType , out float   i_newAmount)
        {

            if (i_FuelAmountRequest + m_CurrentEnergy <= m_MaxEnergy && i_FuelType == m_FuelType)
            {
                m_CurrentEnergy += i_FuelAmountRequest;
                i_newAmount = m_CurrentEnergy;
            }
            else
            {
                if (i_FuelType != m_FuelType)
                {
                    i_newAmount = m_CurrentEnergy;
                    throw new IncorrectFuelTypeException(new Exception(), m_FuelType);
                }
                else 
                {
                    i_newAmount = m_CurrentEnergy;
                    throw new ValueOutOfRangeException(new Exception(), 0, m_MaxEnergy);
                }
            }

        }
    }
}
