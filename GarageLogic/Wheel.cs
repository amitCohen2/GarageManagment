using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        string m_ManufacturerName;
        float m_CurrentAirPressure, m_MaxAirPressure;
        public float CurrentAirPressure { get { return m_CurrentAirPressure; } }
        public float MaxAirPressure { get { return m_MaxAirPressure; } }
        public Wheel(string i_ManufactorerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            m_ManufacturerName = i_ManufactorerName;
            m_CurrentAirPressure = i_CurrentAirPressure;
            m_MaxAirPressure = i_MaxAirPressure;
        }
        public void Inflat(float i_AirPressureRequest)
        {
            if (m_CurrentAirPressure + i_AirPressureRequest <= m_MaxAirPressure)
            {
                m_CurrentAirPressure += i_AirPressureRequest;
            }
            else
            {
                throw new ValueOutOfRangeException(new Exception(), 0, m_MaxAirPressure);
            }
        }

        public void InflatToMax()
        {
            m_CurrentAirPressure = m_MaxAirPressure;
        }

    }
}
