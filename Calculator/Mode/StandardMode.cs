using System;
using System.Collections.Generic;
using System.Data;
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

        // HistoryLogEntry
        public string HistoryLogEntry { get; set; }
        
        public StandardMode()
        {
            this.PrevButtonSender = "";
        }

        #region Ariphmetic Operations Methods

        public bool ArithmeticOperation(OperationType operation)
        {
            // Evaluate expression if it contains more than one ariphmetic operator
            if (this.PerformOperation())
            {
                // Create an expression
                String expression = (this.ResultText + this.EntryText).Trim().Replace(",", "");

                // Evaluate an expression
                this.EntryText = this.EvaluateExpression(expression).ToString();
            }
            
            switch(operation)
            {
                case OperationType.Addition:
                    this.operationType = OperationType.Addition;
                    this.ResultText += this.EntryText + " + ";
                    break;
                case OperationType.Subtraction:
                    this.operationType = OperationType.Subtraction;
                    this.ResultText += this.EntryText + " - ";
                    break;
                case OperationType.Multiplication:
                    this.operationType = OperationType.Multiplication;
                    this.ResultText += this.EntryText + " * ";
                    break;
                case OperationType.Division:
                    this.operationType = OperationType.Division;
                    this.ResultText += this.EntryText + " / ";
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

            // Create an expression
            String expression = (this.ResultText + this.EntryText).Trim().Replace(",","");

            // Evaluate expression
            this.EntryText = this.EvaluateExpression(expression).ToString();
            
            // Create a histofy log entry
            this.CreateHistoryLogEntry(expression);
            
            return true;
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

            return true;
        }

        #endregion

        #region Expression Parser Methods

        // Evaluate C# string with math operators
        // No 3rd party libraries required
        private String EvaluateExpression(string expression)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("expression", typeof(string), expression);
                DataRow row = table.NewRow();
                table.Rows.Add(row);
                return (string)row["expression"];
            }
            catch (Exception exc)
            {
                return "Error";
            }
        }

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
                return (count > 1) ? true : false;
            }
            return false;
        }

        #endregion

        #region History Log Entry

        private void CreateHistoryLogEntry(String expression)
        {
            this.HistoryLogEntry = expression + " =\n" + this.EntryText;
        }

        #endregion
    }
}
