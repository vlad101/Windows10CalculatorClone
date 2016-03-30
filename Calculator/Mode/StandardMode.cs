using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Calculator.Controllers;
using Calculator.Operations;

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

        // History Log Entry
        public string HistoryLogEntry { get; set; }

        // Store last operation after equals operation is used
        public string PrevOperatorOnEquals { get; set; }
        public string PrevEntryTextOnEquals { get; set; }

        
        public StandardMode()
        {
            // Declare previous button sender
            this.PrevButtonSender = "";
        }

        #region Ariphmetic Operations Methods

        public bool ArithmeticOperation(OperationType operation)
        {
            // Get the current expression
            String currResultText = this.ResultText + Utils.TrimDouble(this.EntryText);

            // Declare ariphmetic sign
            String operationSign = "";

            // Evaluate expression if it contains more than one ariphmetic operator
            if (this.PerformOperation())
            {
                // Create an expression
                String expression = (this.ResultText + Utils.TrimDouble(this.EntryText)).Trim().Replace(",", "");

                // Evaluate an expression
                this.EntryText = Utils.EvaluateExpression(expression).ToString();
            }
            
            switch(operation)
            {
                case OperationType.Addition:
                    // Set operation
                    this.operationType = OperationType.Addition;
                    // Set operation sign
                    operationSign = " + ";
                    break;
                case OperationType.Subtraction:
                    // Set operation
                    this.operationType = OperationType.Subtraction;
                    // Set operation sign
                    operationSign = " - ";
                    break;
                case OperationType.Multiplication:
                    // Set operation
                    this.operationType = OperationType.Multiplication;
                    // Set operation sign
                    operationSign = " * ";
                    break;
                case OperationType.Division:
                    // Set operation
                    this.operationType = OperationType.Division;
                    // Set operation sign
                    operationSign = " / ";
                    break;
                default:
                    MessageBox.Show("Something went wrong.");
                    return false;
            }

            // Set last operator being used
            this.PrevOperatorOnEquals = operationSign;

            // If previous operation was an ariphmetic operation, update an operation
            // Otherwise append a new operation to an expression
            currResultText = this.UpdateOperation(currResultText, operationSign);

            // Update result entry text box
            this.ResultText = currResultText;

            return true;
        }

        public bool EqualsOperation()
        {
            if (this.EntryText.Length == 0)
                return false;

            // Set last entry text being used
            this.PrevEntryTextOnEquals = Utils.TrimDouble(this.EntryText);

            // Create an expression
            String expression = (this.ResultText + Utils.TrimDouble(this.EntryText));

            // Evaluate expression
            this.EntryText = Utils.EvaluateExpression(expression.Trim().Replace(",", "")).ToString();
            
            // Create a histofy log entry
            this.CreateHistoryLogEntry(expression);
            
            return true;
        }

        // If equals operator used more than once, repeat the action
        public bool RepeatCalcOnEqualsOperator()
        {
            if (this.PrevOperatorOnEquals != null && this.PrevEntryTextOnEquals != null)
            {
                String expression = this.EntryText + " " + this.PrevOperatorOnEquals + " " + this.PrevEntryTextOnEquals;
                this.EntryText = Utils.EvaluateExpression(expression.Trim().Replace(",", ""));
                this.CreateHistoryLogEntry(expression);
                return true;
            }
            return false;
        }

        #endregion

        #region Clear Methods

        public bool ClearEntryOperation()
        {
            // Clear entry data
            this.EntryText = "0";
            return true;
        }

        public bool ClearOperation()
        {
            // Set operation type
            operationType = OperationType.None;

            // Clear entry and result text
            this.ResultText = "";
            this.EntryText = "0";

            // Clear previous operator and entry text
            this.PrevOperatorOnEquals = "";
            this.PrevEntryTextOnEquals = "";

            return true;
        }

        #endregion

        #region Expression Parser Methods

        // Get operation count from the result text text box
        private bool PerformOperation()
        {
            if(this.ResultText != null)
            {
                int count = 0;
                foreach (char c in this.ResultText)
                {
                    if (c == '+' || c == '-'|| c == '*'|| c == '/')
                        count++;
                }
                return (count >= 1) ? true : false;
            }
            return false;
        }

        // If previous button sender was an ariphmetic operation, update an ariphmetic operation
        // Remove a previous operation sign and update it with the new one
        // If previous button sender was not an ariphmetic operation, append a new ariphmetic sign
        private String UpdateOperation(String expression, String operationSign)
        {
            if(this.ResultText != null)
            {
                if (this.IsPrevSenderOperationSign())
                {
                    expression = expression.Remove(expression.Length - 4) + operationSign;
                    return expression;
                }
               
            }
            return expression + operationSign;
        }

        // Check if previous button sender was an ariphmetic operation
        public bool IsPrevSenderOperationSign()
        {
            if (this.PrevButtonSender.Equals("+") || this.PrevButtonSender.Equals("-") ||
                    this.PrevButtonSender.Equals("X") || this.PrevButtonSender.Equals("÷"))
            {
                return true;
            }
            return false;
        }

        #endregion

        #region History Log Entry

        private void CreateHistoryLogEntry(String expression)
        {
            this.HistoryLogEntry = expression + " =\n" + Utils.FormatText(this.EntryText);
        }

        #endregion
    }
}
