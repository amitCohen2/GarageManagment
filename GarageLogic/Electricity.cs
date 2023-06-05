using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class Electricity : EnergySource
    {


        public Electricity(float i_MaxEnergy, float i_CurrentEnergy) : base (i_MaxEnergy, i_CurrentEnergy)
        {
            // Empty constructor
        }
        public void Recharge(float i_AddChargeTime, out float i_newAmount)
        {
            float AddChargeTimeInHoures = convertMinutestoHouers(i_AddChargeTime);
            if (AddChargeTimeInHoures + m_CurrentEnergy <= m_MaxEnergy)
            {
                m_CurrentEnergy += AddChargeTimeInHoures;
                i_newAmount = m_CurrentEnergy;

            }
            else 
            {
                throw new ValueOutOfRangeException(new Exception(), 0, m_MaxEnergy);
            }
        }
        public float convertMinutestoHouers(float i_AddChargeTime)
        {
            return i_AddChargeTime / 60f;
        }
    }
}
