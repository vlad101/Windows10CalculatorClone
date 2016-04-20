﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Libs
{
    class OperationUtils
    {
        // Evaluate C# string with math operators
        // No 3rd party libraries required
        public static String EvaluateExpression(string expression)
        {
            // If expression contains X, replace it with multiplication sign
            expression = expression.Replace("X", "*");

            // Cannot divide by zero; \u221E is ∞ (infinity)
            if (expression.Contains("\0"))
                return "\u221E";
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
                exc.GetBaseException();
                return "";
            }
        }

        // Set sign of the entry textbox value
        public static String SetSignedValue(String entryText)
        {
            // Do now allow "-0"
            if (!entryText.Equals("0"))
            {
                // If entry text contains "-", remove "-" from entry text
                // If entry text does not contain "-", add "-" to entry text
                if (entryText.Contains("-"))
                {
                    return entryText.Substring(1);
                }
                else
                {
                    return "-" + entryText;
                }
            }
            return entryText;
        }

        // Remove the last entry text character on "<-" ("\u2190") button press
        public static String RemoveLastChar(String entryText)
        {

            if (!entryText.Equals("0"))
            {
                if (entryText.Length <= 1 || (entryText.Length == 2 && entryText.Contains("-")))
                {
                    entryText = "0";
                }
                else
                {
                    entryText = entryText.Remove((entryText.Length - 1), 1);

                    if (entryText.Length == 0)
                    {
                        entryText = "0";
                    }
                }
            }
            return entryText;
        }
    }
}
