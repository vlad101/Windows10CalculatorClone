using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Calculator.Controllers;

namespace Calculator.Mode
{
    class StandardMode
    {
        // Operation type
        public OperationType operationType = OperationType.None;

        // Current entry text
        public string EntryText { get; set; }

        // Current result text
        public string ResultText { get; set; }

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

        public bool ArithmeticOperation(OperationType operation)
        {
            switch(operation)
            {
                case OperationType.Addition:
                    this.operationType = OperationType.Addition;
                    this.summand1 = this.EntryText;
                    break;
                case OperationType.Subtraction:
                    this.operationType = OperationType.Subtraction;
                    this.subtrahend = this.EntryText;
                    break;
                case OperationType.Multiplication:
                    this.operationType = OperationType.Multiplication;
                    this.multiplier = this.EntryText;
                    break;
                case OperationType.Division:
                    this.operationType = OperationType.Division;
                    this.divident = this.EntryText;
                    break;
                default:
                    MessageBox.Show("Something went wrong.");
                    return false;
            }
            return true;
        }

        public bool EqualsOperation()
        {
            if (this.EntryText.Length == 0)
                return false;

            switch(this.operationType)
            {
                case OperationType.Addition:
                    this.summand2 = this.EntryText;
                    this.EntryText = this.summand1 + this.summand2;
                    this.summand1 = "";
                    this.summand2 = "";
                    break;
                case OperationType.Subtraction:
                    this.subtrahend = this.EntryText;
                    // minuend - subtrahend = difference
                    break;
                case OperationType.Multiplication:
                    this.multiplicand = this.EntryText;
                    // multiplier x multiplicand = product
                    break;
                case OperationType.Division:
                    this.divisor = this.EntryText;
                    // divident/divisor = quotient
                    break;
                case OperationType.None:
                    // Do nothing
                    break;
                default:
                    MessageBox.Show("Something went wrong.");
                    return false;
            }

            this.operationType = OperationType.Equals;

            return true;
        }

        public bool ClearEntryOperation()
        {
            // Clear entry data depending on operation
            switch (this.operationType)
            {
                case OperationType.Addition:
                    this.summand2 = "";
                    break;
                case OperationType.Subtraction:
                    this.subtrahend = "";
                    break;
                case OperationType.Multiplication:
                    this.multiplicand = "";
                    break;
                case OperationType.Division:
                    this.divisor = "";
                    break;
                case OperationType.None:
                    this.summand1 = "";
                    this.minuend = "";
                    this.multiplier = "";
                    this.divident = "";
                    this.EntryText = "0";
                    break;
                default:
                    MessageBox.Show("Wrong Operation.");
                    return false;
            }
            return true;
        }

        public bool ClearOperation()
        {
            // Set operation type
            operationType = OperationType.None;

            // Clear variables
            this.summand1 = "";
            this.summand2 = "";
            this.minuend = "";
            this.subtrahend = "";
            this.multiplier = "";
            this.multiplicand = "";
            this.divident = "";
            this.divisor = "";
            this.EntryText = "0";

            return true;
        }
    }
}
