using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Memory
{
    class MemoryLog
    {
        // Store memory value
        public static double MemoryValue;

        // Store Memory log entry
        public String MemoryLogEntry;

        // Constructor for memory log
        public MemoryLog(String MemoryLogEntry)
        {
            this.MemoryLogEntry = MemoryLogEntry;
        }

        // Recall memory
        public static String MemoryRecall()
        {
            return MemoryValue.ToString();
        }

        // Clear memory
        public static double MemoryClear()
        {
            MemoryValue = 0;
            return MemoryValue;
        }

        // Store value in the memory
        public static void MemoryStore(double entryNum)
        {
            MemoryValue = entryNum;
        }

        // Memory subtract
        public static void MemoryMinus(double entryNum)
        {
            MemoryValue -= entryNum;
        }

        // Memory add
        public static void MemoryPlus(double entryNum)
        {
            MemoryValue += entryNum;
        }
    }
}
