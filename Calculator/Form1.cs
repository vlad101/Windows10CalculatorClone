using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Calculator.Controllers;
using Calculator.Mode;
using Calculator.Operations;
using System.Windows.Forms;

namespace Calculator
{
    public partial class FormCalculator : Form
    {
        // Declare calculator modes
        private StandardMode stMode;
        private ScientificMode scMode;
        private ProgrammerMode prMode;

        // Load calculator main form
        public FormCalculator()
        {
            // Initialize main form
            InitializeComponent();

            // Set calculator scientific mode
            this.standardToolStripMenuItem_Click(null, null);

            // Set initial input to 0
            this.textBoxEntry.Text = "0";
        }

        #region Button Events

        // Button number click event
        private void number_button_Click(object sender, EventArgs e)
        {
            // Get the number when the number button is pressed
            Button numBtn = (Button)sender;

            // Append value to an input entry
            // If ariphmetic operation is used, clear the entry text box
            if (this.isAriphmeticOperation())
            {
                stMode.EntryText = "";
            }
            else
            {
                stMode.EntryText = this.textBoxEntry.Text;
            }

            // Update entry text
            this.updateEntryText(numBtn.Text);

            // Clear leading zero
            this.clearZero();

            // Store previous button sender
            stMode.PrevButtonSender = numBtn.Text;

            // Refresgh entry
            this.refreshEntryText();
        }

        // Perform an ariphmetic operation
        private void operation_button_Click(object sender, EventArgs e)
        {
            // Get the number when the number button is pressed
            Button operBtn = (Button)sender;

            // Get operation key
            string operation = operBtn.Text;

            // Store previous button sender
            stMode.PrevButtonSender = operBtn.Text;
            
            // Do the action
            if (stMode.EntryText != null)
            {
                switch (operation)
                {
                    // Ariphmetic operation
                    case "+":
                        stMode.ArithmeticOperation(OperationType.Addition);
                        break;
                    case "-":
                        stMode.ArithmeticOperation(OperationType.Subtraction);
                        break;
                    case "X":
                        stMode.ArithmeticOperation(OperationType.Multiplication);
                        break;
                    case "÷":
                        stMode.ArithmeticOperation(OperationType.Division);
                        break;
                    case "=":
                        if(stMode.EqualsOperation())
                        {
                            this.CreateHistoryLog(stMode.HistoryLogEntry);
                            this.refreshEntryText();
                            stMode.ResultText = "";
                        }
                        break;
                    // Sign change operation
                    case "±":
                        this.setSignedValue();
                        break;
                    // Clear operations
                    case "\u2190": // Leftward arrow
                        this.removeLastChar();
                        break;
                    case "CE":
                        stMode.ClearEntryOperation();
                        break;
                    case "C":
                        stMode.ClearOperation();
                        this.refreshResultText();
                        break;
                    default:
                        MessageBox.Show(operation);
                        break;
                }
            }
            else
            {
                stMode.EntryText = "0";
            }

            this.refreshResultText();
            this.refreshEntryText();
        }

        #endregion

        #region Button (Set signal, clear, remove last char) Methods

        // Remove the last entry text character on "<-" ("\u2190") button press
        private void removeLastChar()
        {

            if (!stMode.EntryText.Equals("0"))
            {
                if (stMode.EntryText.Length <= 1 || (stMode.EntryText.Length == 2 && stMode.EntryText.Contains("-")))
                {
                    stMode.EntryText = "0";
                }
                else
                {
                    stMode.EntryText = stMode.EntryText.Remove((stMode.EntryText.Length - 1), 1);

                    if (stMode.EntryText.Length == 0)
                        stMode.EntryText = "0";
                }
            }
        }

        // Set sign of the entry textbox value
        private void setSignedValue()
        {
            // Do now allow "-0"
            if (!stMode.EntryText.Equals("0"))
            {
                // If entry text contains "-", remove "-" from entry text
                // If entry text does not contain "-", add "-" to entry text
                if (stMode.EntryText.Contains("-"))
                {
                    stMode.EntryText = stMode.EntryText.Substring(1);
                }
                else
                {
                    stMode.EntryText = "-" + stMode.EntryText;
                }
            }
        }

        #endregion

        #region UI entry textbox Methods

        // Clear leading zeros
        private void clearZero()
        {
            // Allow the number start with a single zero
            // Do not allow "00..."
            if (stMode.EntryText.StartsWith("00"))
            {
                stMode.EntryText = "0";
            }

            // Allow leading "0.", and trim all other leading zero combination
            if (stMode.EntryText.Length > 1)
            {
                if (stMode.EntryText.StartsWith("0."))
                {
                    if (stMode.EntryText.Length == 2)
                        stMode.EntryText = "0.";
                }
                else
                {
                    stMode.EntryText = stMode.EntryText.TrimStart('0');
                }
            }

            // Always keep zero positive
            if (stMode.EntryText.Equals("-0"))
            {
                stMode.EntryText = "0";
            }
        }

        // Append a value to the entry textbox
        private void updateEntryText(String numVal)
        {
            // Allow one decimal point
            if (numVal.Equals("."))
            {
                if (!stMode.EntryText.Contains("."))
                    stMode.EntryText = stMode.EntryText + numVal;
            }
            else
            {
                stMode.EntryText = stMode.EntryText + numVal;
            }
        }

        // Update the entry text box value
        private void refreshEntryText()
        {
            if(!stMode.EntryText.EndsWith(".") || !stMode.EntryText.Equals("0"))
                stMode.EntryText = Utils.formatText(stMode.EntryText);
            this.textBoxEntry.Text = stMode.EntryText;
        }

        #endregion

        #region UI entry result Methods

        // Update the result text box value
        private void refreshResultText()
        {
            //if (!stMode.EntryText.EndsWith(".") || !stMode.EntryText.Equals("0"))
            //    stMode.EntryText = Utils.formatText(stMode.EntryText);
            this.textBoxResult.Text = stMode.ResultText;
        }

        #endregion

        #region History Log Methods

        private void CreateHistoryLog(String entry)
        {
            // Create new log
            Button button = new Button();
            
            // Add text
            button.Text = entry;

            // Add style
            this.CreateHistoryLogStyle(button);

            // Attach button to a layout panel
            this.flowLayoutPanelHistory.Controls.Add(button);
        }

        // History log style
        private void CreateHistoryLogStyle(Button buttonLog)
        {
            buttonLog.Width = 183;
            buttonLog.Height = 60;
            buttonLog.Margin = new Padding(0, 0, 0, 0);
            buttonLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonLog.TextAlign = ContentAlignment.MiddleRight;
            //buttonLog.BackColor = Color.LightBlue;
            //if (i % 2 == 0)
            //else
            //button.BackColor = Color.LightPink;
            //button.Tag = i;
        }

        private void flowLayoutPanelHistory_Paint(object sender, PaintEventArgs e)
        {
             this.flowLayoutPanelHistory.Focus();
        }

        #endregion

        #region Set calculator mode methods

        private void standardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stMode = new StandardMode();
            this.labelMode.Text = "STANDARD";
            this.standardToolStripMenuItem.Checked = true;
            this.scietificToolStripMenuItem.Checked = false;
            this.programmerToolStripMenuItem.Checked = false;
        }

        private void scietificToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scMode = new ScientificMode();
            this.labelMode.Text = "SCIENTIFIC";
            this.standardToolStripMenuItem.Checked = false;
            this.scietificToolStripMenuItem.Checked = true;
            this.programmerToolStripMenuItem.Checked = false;
        }

        private void programmerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prMode = new ProgrammerMode();
            this.labelMode.Text = "PROGRAMMER";
            this.standardToolStripMenuItem.Checked = false;
            this.scietificToolStripMenuItem.Checked = false;
            this.programmerToolStripMenuItem.Checked = true;
        }

        #endregion

        #region Utility Methods

        private bool isAriphmeticOperation()
        {
            return stMode.PrevButtonSender.Equals("+") || stMode.PrevButtonSender.Equals("-") ||
            stMode.PrevButtonSender.Equals("X") || stMode.PrevButtonSender.Equals("÷") ||
            stMode.PrevButtonSender.Equals("=");
        }

        #endregion
    }
}