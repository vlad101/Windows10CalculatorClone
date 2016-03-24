using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Operations
{
    class Utils
    {
        // Format the text with commas
        public static void formatText(String entryText)
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
        }
    }
}
