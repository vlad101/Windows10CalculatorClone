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
            // Square exponent
            if (expression.Contains("sqr("))
            {
                expression = expression.Replace("sqr(", "square(");
                expression = CreateExponentExpression(expression, 2);
            }

            // Cube exponent
            if (expression.Contains("cube("))
            {
                expression = CreateExponentExpression(expression, 3);
            }

            // Square root (1/2 exponent)
            if (expression.Contains("sqrt("))
            {
                expression = expression.Replace("sqrt(", "squareroot(");
                expression = CreateExponentExpression(expression, 0.5);
            }

            // Negative one exponent [reciprocal]
            if (expression.Contains("reciproc("))
            {
                expression = CreateExponentExpression(expression, -1);
            }
            
            // If expression contains X, replace it with multiplication sign
            expression = expression.Replace("X", "*");

            // Cannot divide by zero; \u221E is ∞ (infinity)
            if (expression.Contains("\0"))
            {
                return "\u221E";
            }
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("expression", typeof(string), expression);
                DataRow row = table.NewRow();
                table.Rows.Add(row);
                if (row["expression"].ToString().Equals("Infinity"))
                {
                    return "Cannot divide by zero";
                }
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

        private static String CreateExponentExpression(String expression, double exponent)
        {
            String expStr = "";
            int padding = -1;

            if (exponent == 0.5)
            {
                expStr = "squareroot";
                padding = 11;
            }
            else if (exponent == 2)
            {
                expStr = "square";
                padding = 7;
            }
            else if (exponent == 3)
            {
                expStr = "cube";
                padding = 5;
            }
            else if (exponent == -1)
            {
                expStr = "reciproc";
                padding = 9;
            }

            List<String> listValuesToExp = new List<string>();

            IEnumerable<int> expIndeces = IndexOfAll(expression, expStr);

            if(expIndeces != null)
            {
                foreach (int i in expIndeces)
                {
                    StringBuilder valueToExp = new StringBuilder();
                    bool found = false;
                    char c = ')';
                    int startIndex = i + padding;

                    for (int j = startIndex; j < expression.Length && !found; j++)
                    {
                        if(expression[j].Equals(c))
                        {
                            found = true;
                        }
                        else
                        {
                            valueToExp.Append(expression[j]);
                        }
                    }
                    listValuesToExp.Add(valueToExp.ToString());
                }

                foreach(String value in listValuesToExp)
                {
                    if (expStr.Equals("sqrt"))
                    {
                        expression = expression.Replace(expStr + "(" + value + ")", Math.Sqrt(Double.Parse(value)).ToString());
                    }
                    else
                    {
                        expression = expression.Replace(expStr + "(" + value + ")", Math.Pow(Double.Parse(value), exponent).ToString());
                    }
                }
            }
            
            return expression;
        }

        public static IEnumerable<int> IndexOfAll(string sourceString, string subString)
        {
            return Regex.Matches(sourceString, subString).Cast<Match>().Select(m => m.Index);
        }
    }
}
