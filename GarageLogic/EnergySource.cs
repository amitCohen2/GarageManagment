using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class EnergySource
    {
        protected float m_MaxEnergy , m_CurrentEnergy;
        public float MaxEnergy { get { return m_MaxEnergy; } }
        public float CurrentEnergy { get { return m_CurrentEnergy; } }
        public EnergySource(float i_MaxEnergy, float i_CurrentEnergy)
        {
            m_MaxEnergy = i_MaxEnergy;
            m_CurrentEnergy = i_CurrentEnergy;
        }
    }
}
