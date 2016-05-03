using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Calculator.Libs
{
    class OperationUtils
    {
        // Evaluate C# string with math operators
        // No 3rd party libraries required
        public static String EvaluateExpression(string expression)
        {
            // Cube exponent
            if (expression.Contains("cube"))
            {
                IEnumerable<int> cubeIndeces = IndexOfAll(expression, "cube");

                foreach (int i in cubeIndeces)
                {
                    bool found = false;
                    char c = ')';
                    int startIndex = i;
                    int endIndex = -1;
                    for (int j = i; j < expression.Length && !found; j++)
                    {
                        if(expression[j].Equals(c))
                        {
                            found = true;
                            endIndex = j;
                        }
                    }
                    //MessageBox.Show(expression.Substring(startIndex, endIndex));
                    MessageBox.Show("HI: " + endIndex.ToString());
                }

                

                expression = expression.Replace("cube", "");

                MessageBox.Show(expression);
                //char[] separatingChars = "cube".ToCharArray();
                //string[] words = expression.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                //expression = CreateExponentExpression(words, 3);
            }

            // Square exponent
            if (expression.Contains("sqr"))
            {
                char[] separatingChars = "sqr".ToCharArray();
                string[] words = expression.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                expression = CreateExponentExpression(words, 2);
            }

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

        private static String CreateExponentExpression(string[] words, int exp)
        {
            StringBuilder strBuilder = new StringBuilder();

            foreach (string s in words)
            {
                StringBuilder exponentValue = new StringBuilder();

                for (int i = 1; i < s.IndexOf(')'); i++)
                {
                    exponentValue.Append(s[i]);
                }

                if (exponentValue.Length > 0)
                {
                    string valueExp = "(" + exponentValue.ToString() + ")";

                    if (exp == 3)
                    {
                        strBuilder.Append(s.Replace(valueExp, (valueExp + "*" + valueExp + "*" + valueExp)));
                    }
                    else
                    {
                        strBuilder.Append(s.Replace(valueExp, (valueExp + "*" + valueExp)));
                    }
                }
                else
                {
                    strBuilder.Append(s);
                }
            }
            return strBuilder.ToString();
        }

        public static IEnumerable<int> IndexOfAll(string sourceString, string subString)
        {
            return Regex.Matches(sourceString, subString).Cast<Match>().Select(m => m.Index);
        }
    }
}
