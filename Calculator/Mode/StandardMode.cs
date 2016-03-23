using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Mode
{
    class StandardMode
    {
        // Current entry text
        public string EntryText { get; set; }

        // Addition (summand1 + summand2 = sum)
        private string summand1 { get; set; }
        private string summand2 { get; set; }

        // Subtraction (minuend - subtrahend = difference)
        private string minuend { get; set; }
        private string subtrahend { get; set; }

        // Multiplication (multiplier x multiplicand = product)
        private string multiplier { get; set; }
        private string multiplicand { get; set; }

        // Division (divident/divisor = quotient)
        private string divident { get; set; }
        private string divisor { get; set; }

        public bool AdditionOperation()
        {
            this.summand1 = this.EntryText;
            return true;
        }

        public bool SubtractionOperation()
        {
            this.minuend = this.EntryText;
            return true;
        }

        public bool MultiplicationOperation()
        {
            this.multiplier = this.EntryText;
            return true;
        }

        public bool DivisionOperation()
        {
            this.divident = this.EntryText;
            return true;
        }

        public bool EqualsOperation()
        {
            return true;
        }
    }
}
