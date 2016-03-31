using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator.Operations
{
    class Utils
    {
        // Format the text with commas
        public static String FormatText(String entryText)
        {
            if (entryText != null && entryText.Length > 0)
            {
                String formatStr;
                int decPlaces = entryText.Substring(entryText.LastIndexOf('.') + 1).Length;

                if (!entryText.EndsWith("."))
                {
                    if (entryText.Contains("."))
                    {
                        formatStr = "#,##0.";
                        for (int i = 0; i < decPlaces; i++)
                            formatStr += "0";
                    }
                    else
                    {
                        formatStr = "#,##0";
                    }
                    
                    entryText = Convert.ToDecimal(entryText).ToString(formatStr);
                }
            }
            return entryText;
        }

        // Display decimal without trailing zeros
        public static string TrimDouble(string temp)
        {
            var value = temp.IndexOf('.') == -1 ? temp : temp.TrimEnd('.', '0');
            return value == string.Empty ? "0" : value;
        }

        // Evaluate C# string with math operators
        // No 3rd party libraries required
        public static String EvaluateExpression(string expression)
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
    }
}
