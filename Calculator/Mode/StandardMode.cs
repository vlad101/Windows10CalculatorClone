﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Calculator.Controllers;
using Calculator.History;
using Calculator.Libs;

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

        // Store history log
        public HistoryLog HistoryLog { get; set; }

        // Store the value to check if exp operations can be used
        public bool allowExpOps { get; set; }

        // Store last operation after equals operation is used
        public string PrevOperatorOnEquals { get; set; }
        public string PrevEntryTextOnEquals { get; set; }

        
        public StandardMode()
        {
            // Declare previous button sender
            this.PrevButtonSender = "";

            // Allow exponents
            this.allowExpOps = true;
        }

        #region Ariphmetic Operations Methods

        public bool ArithmeticOperation(OperationType operation)
        {
            // History operation
            bool isHistory = this.PrevButtonSender.Equals("History");

            // Exponent operation
            bool isPower = this.PrevButtonSender.Equals("Power");

            // Get the current expression
            String currResultText = "";

            // If previous operation is history, append the sign to an expresion; else append append operation followed by the entry text value
            if (isHistory || isPower)
            {
                currResultText = this.ResultText;
            }
            else
                currResultText = this.ResultText + FormatUtils.TrimDouble(this.EntryText);

            // Declare ariphmetic sign
            String operationSign = "";

            // Evaluate expression if it contains more than one ariphmetic operator
            if (this.PerformOperation())
            {
                // Create an expression
                String expression = "";

                if (isPower)
                {
                    expression = (this.ResultText).Trim().Replace(",", "");
                }
                else
                {
                    expression = (this.ResultText + FormatUtils.TrimDouble(this.EntryText)).Trim().Replace(",", "");
                }

                // Evaluate an expression
                this.EntryText = OperationUtils.EvaluateExpression(expression).ToString();
            }
            
            // Set the operation
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

            // If history remove a leading space in front of an operation sign
            if (isHistory)
            {
                operationSign = operationSign.Substring(1);
            }

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
            this.PrevEntryTextOnEquals = FormatUtils.TrimDouble(this.EntryText);

            // Exponent operation
            bool isPower = this.PrevButtonSender.Equals("Power");

            // Create an expression
            String expression = "";

            if (isPower)
            {
                expression = this.ResultText;
            }
            else
            {
                expression = this.ResultText + FormatUtils.TrimDouble(this.EntryText);
            }

            // Evaluate expression
            this.EntryText = OperationUtils.EvaluateExpression(expression.Trim().Replace(",", "")).ToString();

            // Create a histofy log entry
            if (!this.EntryText.Contains("\u221E"))
            {
                this.CreateHistoryLogEntry(expression);
            }

            return true;
        }

        // If equals operator used more than once, repeat the action
        public bool RepeatCalcOnEqualsOperator()
        {
            if (this.PrevOperatorOnEquals != null && this.PrevEntryTextOnEquals != null)
            {
                String expression = this.EntryText + " " + this.PrevOperatorOnEquals + " " + this.PrevEntryTextOnEquals;
                this.EntryText = OperationUtils.EvaluateExpression(expression.Trim().Replace(",", ""));
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
            HistoryLog = new HistoryLog(expression + " =\n" + FormatUtils.FormatText(this.EntryText));
        }

        #endregion
    }
}
