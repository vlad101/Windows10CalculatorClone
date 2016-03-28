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

        // Store button sender
        public string PrevButtonSender { get; set; }

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

        public StandardMode()
        {
            this.PrevButtonSender = "";
        }

        #region Ariphmetic Operations Methods

        public bool ArithmeticOperation(OperationType operation)
        {
            switch(operation)
            {
                case OperationType.Addition:
                    this.operationType = OperationType.Addition;
                    this.summand1 = this.EntryText;
                    this.ResultText = this.summand1 + " + ";
                    break;
                case OperationType.Subtraction:
                    this.operationType = OperationType.Subtraction;
                    this.minuend = this.EntryText;
                    this.ResultText = this.minuend + " - ";
                    break;
                case OperationType.Multiplication:
                    this.operationType = OperationType.Multiplication;
                    this.multiplier = this.EntryText;
                    this.ResultText = this.multiplier + " X ";
                    break;
                case OperationType.Division:
                    this.operationType = OperationType.Division;
                    this.divident = this.EntryText;
                    this.ResultText = this.divident + " ÷ ";
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
                    //this.ResultText = this.minuend + " - " + this.subtrahend + " =";
                    break;
                case OperationType.Subtraction:
                    this.subtrahend = this.EntryText;
                    //this.ResultText = this.minuend + " - " + this.subtrahend + " =";
                    break;
                case OperationType.Multiplication:
                    this.multiplicand = this.EntryText;
                    //this.ResultText = this.multiplier + " X " + this.multiplicand + " =";
                    break;
                case OperationType.Division:
                    this.divisor = this.EntryText;
                    //this.ResultText = this.divident + " ÷ " + this.divisor + " =";
                    break;
                case OperationType.None:
                    // Do nothing
                    break;
                default:
                    MessageBox.Show("Something went wrong.");
                    return false;
            }

            this.doAriphmeticOperation();

            return true;
        }

        private void doAriphmeticOperation()
        {
            switch (this.operationType)
            {
                case OperationType.Addition:
                    this.add();
                    break;
                case OperationType.Subtraction:
                    this.subtract();
                    break;
                case OperationType.Multiplication:
                    this.multiply();
                    break;
                case OperationType.Division:
                    this.divide();
                    break;
                default:
                    MessageBox.Show("Something went wrong.");
                    break;
            }
        }

        private void add()
        {
            // Remove all comma occurences from the value strings
            this.summand1 = this.summand1.Replace(",", "");
            this.summand2 = this.summand2.Replace(",", "");

            // Is the value integer or double?
            if (this.summand1.Contains(".") || this.summand2.Contains("."))
            {
                // Process as double
                this.EntryText = (Double.Parse(this.summand1) + Double.Parse(this.summand2)).ToString();
            }
            else
            {
                // Process as integer
                this.EntryText = (Int32.Parse(this.summand1) + Int32.Parse(this.summand2)).ToString();
            }

            // Update result textbox
            this.ResultText = this.summand1 + " + " + this.summand2 + " =";

            // Clear data
            this.summand1 = "";
            this.summand2 = "";
        }

        private void subtract()
        {
            // Remove all comma occurences from the value strings
            this.minuend = this.minuend.Replace(",", "");
            this.subtrahend = this.subtrahend.Replace(",", "");

            // Is the value integer or double?
            if (this.minuend.Contains(".") || this.subtrahend.Contains("."))
            {
                // Process as double
                this.EntryText = (Double.Parse(this.minuend) - Double.Parse(this.subtrahend)).ToString();
            }
            else
            {
                // Process as integer
                this.EntryText = (Int32.Parse(this.minuend) - Int32.Parse(this.subtrahend)).ToString();
            }

            // Update result textbox
            this.ResultText = this.minuend + " - " + this.subtrahend + " =";

            // Clear data
            this.minuend = "";
            this.subtrahend = "";
        }

        private void multiply()
        {
            // Remove all comma occurences from the value strings
            this.multiplier = this.multiplier.Replace(",", "");
            this.multiplicand = this.multiplicand.Replace(",", "");

            // Is the value integer or double?
            if (this.multiplier.Contains(".") || this.multiplicand.Contains("."))
            {
                // Process as double
                this.EntryText = (Double.Parse(this.multiplier) * Double.Parse(this.multiplicand)).ToString();
            }
            else
            {
                // Process as integer
                this.EntryText = (Int32.Parse(this.multiplier) * Int32.Parse(this.multiplicand)).ToString();
            }

            // Update result textbox
            this.ResultText = this.multiplier + " X " + this.multiplicand + " =";

            // Clear data
            this.multiplier = "";
            this.multiplicand = "";
        }

        private void divide()
        {
            // Remove all comma occurences from the value strings
            this.divident = this.divident.Replace(",", "");
            this.divisor = this.divisor.Replace(",", "");

            // Is the value integer or double?
            if (this.divident.Contains(".") || this.divisor.Contains("."))
            {
                // Process as double
                this.EntryText = (Double.Parse(this.divident) / Double.Parse(this.divisor)).ToString();
            }
            else
            {
                // Process as integer
                this.EntryText = (Int32.Parse(this.divident) / Int32.Parse(this.divisor)).ToString();
            }

            // Update result textbox
            this.ResultText = this.divident + " ÷ " + this.divisor + " =";

            // Clear data
            this.divident = "";
            this.divisor = "";
        }

        #endregion

        #region Clear Methods

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
                    break;
                default:
                    MessageBox.Show("Wrong Operation.");
                    return false;
            }
            this.EntryText = "0";
            
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
            this.ResultText = "";
            this.EntryText = "0";

            return true;
        }

        #endregion
    }
}
