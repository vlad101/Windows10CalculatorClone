﻿using System;
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
            
            // Do the action
            if (stMode.EntryText != null)
            {
                switch (operation)
                {
                    // Ariphmetic operation
                    case "+":
                        this.PerformAriphmeticOperation(OperationType.Addition);
                        break;
                    case "-":
                        this.PerformAriphmeticOperation(OperationType.Subtraction);
                        break;
                    case "X":
                        this.PerformAriphmeticOperation(OperationType.Multiplication);
                        break;
                    case "÷":
                        this.PerformAriphmeticOperation(OperationType.Division);
                        break;
                    case "=":
                        this.PerformEqualsOperation();
                        break;
                    // Sign change operation
                    case "±":
                        this.PerformChangeSign();
                        break;
                    // Clear operations
                    case "\u2190": // Leftward arrow
                        // Remove a last character
                        this.removeLastChar();
                        this.refreshEntryText();
                        break;
                    case "CE":
                        stMode.ClearEntryOperation();
                        // Refresh entry textbox
                        this.refreshEntryText();
                        break;
                    case "C":
                        stMode.ClearOperation();
                        // Refresh entry and result textboxes
                        this.refreshEntryText();
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

            // Check entry text box
            this.CheckInputOnOperation(operation);

            // Store previous button sender
            stMode.PrevButtonSender = operBtn.Text;
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
                    {
                        stMode.EntryText = "0";
                    }
                }
            }
        }

        #endregion

        #region UI entry textbox Methods

        private void CheckInputOnOperation(String operation)
        {
            // If no entry text value, use zero followed by an operation sign
            if (stMode.EntryText.Equals("0") && this.textBoxEntry.Text.Length != 0)
            {
                if (operation.Equals("+") || operation.Equals("-") ||
                    operation.Equals("X")  || operation.Equals("÷"))
                {
                    stMode.ResultText = "0 " + operation + " ";
                }
                this.refreshResultText();
            }

            // If entry text value equals ".", prepend it with a "0"
            if (stMode.EntryText.Equals("."))
            {
                stMode.ResultText = "0.";
            }
        }

        // Clear leading zeros
        private void clearZero()
        {
            // Allow the number start with a single zero
            // Do not allow "00..."
            if (stMode.EntryText.StartsWith("00"))
            {
                // Allow leading "0.", and trim all other leading zero combination
                if (stMode.EntryText.Length > 1)
                {
                    stMode.EntryText = "0";
                }

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
            if(stMode.EntryText != null && stMode.EntryText.Length > 0)
            {
                if(!stMode.EntryText.EndsWith(".") || !stMode.EntryText.Equals("0"))
                    stMode.EntryText = Utils.FormatText(stMode.EntryText);
                this.textBoxEntry.Text = stMode.EntryText;
            }
        }

        #endregion

        #region UI entry result Methods

        // Update the result text box value
        private void refreshResultText()
        {
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
            // Color control
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

        #region Perfom Ariphmetic and Equals Operations

        // Perform ariphmetic operation
        private void PerformAriphmeticOperation(OperationType operation)
        {
            stMode.ArithmeticOperation(operation);
            // Refresh entry and result textboxes
            this.refreshEntryText();
            this.refreshResultText();
            // Clear entry text value
            stMode.EntryText = "";
        }

        // Perform equals operation
        private void PerformEqualsOperation()
        {
            // If the last operator was equals operation, repeat the computattion
            if(stMode.PrevButtonSender.Equals("="))
            {
                if (stMode.EntryText != null && stMode.EntryText.Trim().Length != 0)
                {
                    // Repeat a previous operation
                    if(stMode.RepeatCalcOnEqualsOperator())
                    {
                        // Refresh entry textbox
                        this.refreshEntryText();
                        // Create a history log
                        this.CreateHistoryLog(stMode.HistoryLogEntry);
                    }
                }
            }
            else
            {
                // Calculate expression
                if (stMode.ResultText != null && stMode.ResultText.Trim().Length != 0)
                {
                    if(stMode.EqualsOperation())
                    {
                        // Create a history log
                        this.CreateHistoryLog(stMode.HistoryLogEntry);
                        // Clear result text value
                        stMode.ResultText = "";
                        // Refresh entry and result textboxes
                        this.refreshEntryText();
                        // Refresh result textbox
                        this.refreshResultText();
                    }
                }
            }
        }


        // Change sign of the entry text box
        private void PerformChangeSign()
        {
            // Change the sign only if the button was non opeartion sign
            if (!stMode.IsPrevSenderOperationSign())
            {
                // Change the sign of the entry textbox
                stMode.EntryText = Utils.SetSignedValue(stMode.EntryText);

                // Refresh entry textbox
                this.refreshEntryText();
            }
        }

        #endregion

        #region Set calculator mode methods

        private void standardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create new Standard Mode instance
            stMode = new StandardMode();
            // Clear operation and refresh text
            stMode.ClearOperation();
            this.refreshEntryText();
            this.refreshResultText();
            // Define Standard Mode
            this.labelMode.Text = "STANDARD";
            this.standardToolStripMenuItem.Checked = true;
            this.scietificToolStripMenuItem.Checked = false;
            this.programmerToolStripMenuItem.Checked = false;
        }

        private void scietificToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create new Scientific Mode instance
            scMode = new ScientificMode();
            // Clear operation and refresh text
            scMode.ClearOperation(this.stMode);
            this.refreshEntryText();
            this.refreshResultText();
            // Define Scientific Mode
            this.labelMode.Text = "SCIENTIFIC";
            this.standardToolStripMenuItem.Checked = false;
            this.scietificToolStripMenuItem.Checked = true;
            this.programmerToolStripMenuItem.Checked = false;
        }

        private void programmerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create new Programmer Mode instance
            prMode = new ProgrammerMode();
            // Clear operation and refresh text
            prMode.ClearOperation(this.stMode);
            this.refreshEntryText();
            this.refreshResultText();
            // Define Programmer Mode
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

        #region UI Main Form Load Methods

        //Load main form
        private void FormCalculator_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            // Set copy shortcut
            if (e.Control && e.KeyCode == Keys.C)
            {
                this.copyCtrlCToolStripMenuItem_Click(null, null);
            }

            // Set paste shortcut
            if (e.Control && e.KeyCode == Keys.V)
            {
                this.pasteCtrlVToolStripMenuItem_Click(null, null);
            }
        }

        #endregion

        #region Copy/Paste Methods

        // Copy data to Clipboard
        private void copyCtrlCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.textBoxEntry.Text);
        }

        // Paste data from Clipboard
        private void pasteCtrlVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String clipBoardText = Clipboard.GetText();
            double number;

            if (clipBoardText != null && clipBoardText.Length > 0)
            {
                if (Double.TryParse(clipBoardText, out number))
                {
                    this.textBoxEntry.Text = Utils.FormatText(number.ToString());
                    stMode.EntryText = Utils.FormatText(number.ToString());
                }
            }
        }

        #endregion
    }
}