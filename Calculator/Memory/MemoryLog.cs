using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Database;

namespace Calculator.Memory
{
    class MemoryLog
    {
        // Store memory value
        public static double MemoryValue;

        // Store Memory log entry
        public String MemoryLogEntry;

        // Constructors for memory log
        public MemoryLog(){}

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
        public int MemoryStore(double entryNum)
        {
            MemoryValue = entryNum;
            return this.InsertMemoryLog();
        }

        // Memory subtract
        public void MemoryMinus(double entryNum)
        {
            MemoryValue -= entryNum;
        }

        // Memory add
        public void MemoryPlus(double entryNum)
        {
            MemoryValue += entryNum;
        }

        // Insert data to database
        private int InsertMemoryLog()
        {
            DataMemory dataMemory = new DataMemory();
            return dataMemory.InsertMemoryEntry(MemoryValue.ToString());
        }
    }
}
