using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.History
{
    class HistoryLog
    {
        public string HistoryLogEntry { get; set; }

        public HistoryLog(String HistoryLogEntry)
        {
            this.HistoryLogEntry = HistoryLogEntry;
        }
    }
}
