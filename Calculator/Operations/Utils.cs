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
        public static String formatText(String entryText)
        {
            if (entryText != null)
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
                return "";
            }
        }
    }
}
