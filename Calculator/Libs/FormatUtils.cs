using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator.Libs
{
    class FormatUtils
    {
        // Format the text with commas
        public static String FormatText(String entryText)
        {
            try
            {
                // Cannot divide by zero; \u221E is ∞ (infinity)
                if (entryText.Contains("\u221E"))
                {
                    return "Cannot divide by zero";
                }

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
            }
            catch
            { }
            return entryText;
        }

        // Display decimal without trailing zeros
        public static string TrimDouble(string temp)
        {
            var value = temp.IndexOf('.') == -1 ? temp : temp.TrimEnd('.', '0');
            return value == string.Empty ? "0" : value;
        }

        // Append a value to the entry textbox
        public static String updateEntryText(String entryText, String numVal)
        {
            // Allow one decimal point
            if (numVal.Equals("."))
            {
                if (!entryText.Contains("."))
                    entryText = entryText + numVal;
            }
            else
            {
                entryText = entryText + numVal;
            }
            return entryText;
        }

        // Clear leading zeros
        public static String clearZero(String entryText)
        {
            // Allow the number start with a single zero
            // Do not allow "00..."
            if (entryText.StartsWith("00"))
            {
                // Allow leading "0.", and trim all other leading zero combination
                if (entryText.Length > 1)
                {
                    entryText = "0";
                }

                if(entryText.StartsWith("0."))
                {
                    if (entryText.Length == 2)
                        entryText = "0.";
                }
                else
                {
                    entryText = entryText.TrimStart('0');
                }
            }

            // Always keep zero positive
            if (entryText.Equals("-0"))
            {
                entryText = "0";
            }
            return entryText;
        }
    }
}
