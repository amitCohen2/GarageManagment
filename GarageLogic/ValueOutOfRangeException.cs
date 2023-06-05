using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;

        public float MaxValue
        {
            get { return m_MaxValue; }
        }

        public float MinValue
        {
            get { return m_MinValue; }
        }

        public ValueOutOfRangeException(Exception i_InnerException, float i_MinValue, float i_MaxValue)
                                        : base($"Value Out Of Range! Range should be between {i_MinValue} - {i_MaxValue}", i_InnerException)
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }
    }
}
