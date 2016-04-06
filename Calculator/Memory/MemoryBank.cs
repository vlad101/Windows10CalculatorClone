using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Memory
{
    class MemoryBank
    {
        public static string MemoryValue;

        public static String MemoryClear()
        {
            MemoryValue = null;
            return MemoryValue;
        }

        public static String MemoryStore(String entryText)
        {
            MemoryValue = entryText;
            return MemoryValue;
        }

        public static String MemoryMinus(String entryText)
        {
            // Memory subtract
            if(isValidNumber(entryText))
            {
                double val = Double.Parse(entryText);
                double memoryVal = Double.Parse(MemoryValue);
                memoryVal -= val;
                MemoryValue = memoryVal.ToString();
                return MemoryValue;
            }
            return null;
        }

        public static String MemoryPlus(String entryText)
        {
            // Memory add
            if (isValidNumber(entryText))
            {
                double val = Double.Parse(entryText);
                double memoryVal = Double.Parse(MemoryValue);
                memoryVal += val;
                MemoryValue = memoryVal.ToString();
                return MemoryValue;
            }
            return null;
        }

        private static bool isValidNumber(String entryText)
        {
             double number;
             if (Double.TryParse(entryText, out number))
                return true;
            return false;
        }
    }
}
