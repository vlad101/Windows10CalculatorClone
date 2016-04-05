using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Globalization;
using System.Windows.Forms;
using Calculator.Controls;
using Calculator.Controllers;
using Calculator.History;
using Calculator.Libs;
using Calculator.Mode;
using Calculator.Database;

namespace Calculator
{
    public partial class FormCalculator : Form
    {
        // Declare calculator modes
        private StandardMode Mode;

        // Show history entry log
        private bool ShowHistory = false;

        // Load calculator main form
        public FormCalculator()
        {
            // Initialize main form
            InitializeComponent();

            // Set calculator mode
            this.SetMode();

            // Set initial input to 0
            this.textBoxEntry.Text = "0";
        }

        #region Number and Operation Button Events

        // Button number click event
        private void number_button_Click(object sender, EventArgs e)
        {
            // Get the number when the number button is pressed
            Button numBtn = (Button)sender;

            // Append value to an input entry
            // If ariphmetic operation is used, clear the entry text box
            if (this.isAriphmeticOperation())
            {
               this.Mode.EntryText = "";
            }
            else
            {
               this.Mode.EntryText = this.textBoxEntry.Text;
            }

            // Update entry text
            this.Mode.EntryText = FormatUtils.updateEntryText(this.Mode.EntryText, numBtn.Text);

            // Clear leading zero
            this.Mode.EntryText = FormatUtils.clearZero(this.Mode.EntryText);

            // If history log entry is selected, set result textbox to none
            if (this.Mode.PrevButtonSender.Equals("History"))
            {
                // Clear operation and refresh text
                this.Mode.EntryText = numBtn.Text;
                this.Mode.ResultText = "";
                this.refreshResultText();
            }

            // Store previous button sender
            this.Mode.PrevButtonSender = numBtn.Text;

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
            if (Mode.EntryText != null)
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
                        this.Mode.EntryText = OperationUtils.RemoveLastChar(this.Mode.EntryText);
                        this.refreshEntryText();
                        break;
                    case "CE":
                       this.Mode.ClearEntryOperation();
                        // Refresh entry textbox
                        this.refreshEntryText();
                        break;
                    case "C":
                       this.Mode.ClearOperation();
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
               this.Mode.EntryText = "0";
            }

            // Check entry text box
            this.CheckInputOnOperation(operation);

            // Store previous button sender
           this.Mode.PrevButtonSender = operBtn.Text;
        }

        #endregion

        #region UI entry textbox Methods

        private void CheckInputOnOperation(String operation)
        {
            // If no entry text value, use zero followed by an operation sign
            if (this.Mode.EntryText.Equals("0") && this.textBoxEntry.Text.Length != 0)
            {
                if (operation.Equals("+") || operation.Equals("-") ||
                    operation.Equals("X")  || operation.Equals("÷"))
                {
                   this.Mode.ResultText = "0 " + operation + " ";
                }
                this.refreshResultText();
            }

            // If entry text value equals ".", prepend it with a "0"
            if (this.Mode.EntryText.Equals("."))
            {
               this.Mode.ResultText = "0.";
            }
        }

        // Update the entry and result textbox value
        private void refreshEntryText()
        {
            if (this.Mode.EntryText != null && this.Mode.EntryText.Length > 0)
            {
                if (this.Mode.EntryText.Equals("."))
                {
                    this.Mode.EntryText = "0.";
                }
                if (!this.Mode.EntryText.EndsWith(".") || !this.Mode.EntryText.Equals("0"))
                {
                    this.Mode.EntryText = FormatUtils.FormatText(this.Mode.EntryText);
                }

                // If entry textbox data is long, convert to scientific notation
                this.Mode.EntryText = this.Mode.EntryText.ToString(CultureInfo.InvariantCulture);

                this.textBoxEntry.Text = this.Mode.EntryText;
            }
        }

        // Update the result text box value
        private void refreshResultText()
        {
            this.textBoxResult.Text =this.Mode.ResultText;
        }

        #endregion

        #region History Log Methods

        private void CreateHistoryLog(String entry)
        {
            // Create new log
            NonFocusButton button = new NonFocusButton();

            // Add text
            button.Text = entry;
            
            // Add style
            this.CreateHistoryLogStyle(button);

            // Attach button to a layout panel
            this.flowLayoutPanelHistory.Controls.Add(button);

            // Set click event
            button.Click += HistoryLogEntry_Click;

            // Show Clear History Button
            this.buttonClearHistory.Visible = true;
        }

        private void buttonClearHistory_Click(object sender, EventArgs e)
        {
            // Delete history olg entry from db
            if (this.DeleteHistoryLogDB())
            {
                // Delete button controls from a flow layout panel history
                this.flowLayoutPanelHistory.VerticalScroll.Visible = false;
                this.flowLayoutPanelHistory.Controls.Clear();
                this.buttonClearHistory.Visible = false;            
            }
        }

        // Display result in result textbox
        private void HistoryLogEntry_Click(object sender, EventArgs e)
        {
            NonFocusButton clickedButton = (NonFocusButton)sender;
            HistoryLog HistoryLog = new HistoryLog((string)clickedButton.Text);
            if(HistoryLog != null && HistoryLog.HistoryLogEntry.Contains("="))
            {
                double number;
                String historyLogStr = HistoryLog.HistoryLogEntry;
                string[] historyLogArr = historyLogStr.Split('=');
                if (historyLogArr.Length == 2)
                {
                    String result = historyLogArr[0];
                    String entry = historyLogArr[1].Trim();

                    if (result != null)
                    {
                        this.Mode.ResultText = result;
                        this.refreshResultText();
                    }
                    
                    if (Double.TryParse(entry, out number))
                    {
                       this.Mode.EntryText = number.ToString();
                        this.refreshEntryText();
                    }
                }
            }

            // Store previous button sender as history
            this.Mode.PrevButtonSender = "History";
        }

        // History log style
        private void CreateHistoryLogStyle(NonFocusButton buttonLog)
        {
            buttonLog.Width = 183;
            buttonLog.Height = 60;
            buttonLog.Margin = new Padding(3, 0, 0, 3);
            buttonLog.Font = new Font("Microsoft Sans Serif", 10.00F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            buttonLog.TextAlign = ContentAlignment.MiddleRight;
            buttonLog.FlatStyle = FlatStyle.Flat;
            buttonLog.FlatAppearance.BorderColor = Color.Gray;
        }

        // Make the FlowLayoutPanel scroll with the mouse wheel
        private void flowLayoutPanelHistory_Paint(object sender, PaintEventArgs e)
        {
            // FlowLayoutPanel will scroll only if it has focus
            this.flowLayoutPanelHistory.Focus();
        }

        // Show or hide history
        private void buttonHistory_Click(object sender, EventArgs e)
        {
            // This will change the Form's Width and Height to show the history, respectively.
            if (this.ShowHistory)
            {
                this.SetFormSize(662);
                this.ShowHistory = false;
            }
            else
            {
                this.SetFormSize(412);
                this.ShowHistory = true;
            }
        }

        // Show/Hide history on load
        private void DoShowHistory()
        {
            this.ShowHistory = Properties.Settings.Default.showHistory;
            this.buttonHistory_Click(null, null);
        }

        // Show or hide history; set axis, y-axis are the same for both
        private void SetFormSize(int xAxis)
        {
            this.MaximumSize = new System.Drawing.Size(xAxis, 355);
            this.MinimumSize = new System.Drawing.Size(xAxis, 355);

            // Save the default value
            Properties.Settings.Default.showHistory = this.ShowHistory;
            Properties.Settings.Default.Save();
        }

        #endregion

        #region Perfom Ariphmetic and Equals Operations

        // Perform ariphmetic operation
        private void PerformAriphmeticOperation(OperationType operation)
        {
           this.Mode.ArithmeticOperation(operation);
            // Refresh entry and result textboxes
            this.refreshEntryText();
            this.refreshResultText();
            // Clear entry text value
           this.Mode.EntryText = "";
        }

        // Perform equals operation
        private void PerformEqualsOperation()
        {
            // If the last operator was equals operation, repeat the computattion
            if (Mode.PrevButtonSender.Equals("=") || Mode.PrevButtonSender.Equals("History"))
            {
                if (Mode.EntryText != null &&this.Mode.EntryText.Trim().Length != 0)
                {
                    // Repeat a previous operation
                    if(Mode.RepeatCalcOnEqualsOperator())
                    {
                        // Refresh entry textbox
                        this.refreshEntryText();
                        // Create a history log
                        this.CreateHistoryLog(Mode.HistoryLog.HistoryLogEntry);

                        // Insert data to the database
                        this.InsertHistoryLogDB();
                    }
                }
            }
            else
            {
                // Calculate expression
                if (Mode.ResultText != null && this.Mode.ResultText.Trim().Length != 0)
                {
                    if(Mode.EqualsOperation())
                    {
                        if (this.Mode.HistoryLog != null)
                        {
                            // Create a history log
                            this.CreateHistoryLog(this.Mode.HistoryLog.HistoryLogEntry);
                        }
                        // Clear result text value
                        this.Mode.ResultText = "";

                        // Insert data to the database
                        this.InsertHistoryLogDB();

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
            if (!Mode.IsPrevSenderOperationSign())
            {
                // Change the sign of the entry textbox
               this.Mode.EntryText = OperationUtils.SetSignedValue(Mode.EntryText);

                // Refresh entry textbox
                this.refreshEntryText();
            }
        }

        #endregion

        #region Set calculator mode methods

        private void SetMode()
        {
            String mode = Properties.Settings.Default.mode;

            switch(mode)
            {
                case "Standard":
                    this.standardToolStripMenuItem_Click(null, null);
                    break;
                case "Scientific":
                    this.scietificToolStripMenuItem_Click(null, null);
                    break;
                case "Programmer":
                    this.scietificToolStripMenuItem_Click(null, null);
                    break;
                default:
                    this.standardToolStripMenuItem_Click(null, null);
                    break;
            }
        }

        // Set Standard Mode
        private void standardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SetCalculatorMode(CalculatorMode.StandardMode);
        }

        // Set Scientific Mode
        private void scietificToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SetCalculatorMode(CalculatorMode.ScietificMode);
        }

        // Set Programmer Mode
        private void programmerToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.SetCalculatorMode(CalculatorMode.ProgrammerMode);
        }


        // Set calculator mode to standard, scientific, programmer
        private void SetCalculatorMode(CalculatorMode calculatorMode)
        {
            // Set menu tool strip to false
            this.standardToolStripMenuItem.Checked = false;
            this.scietificToolStripMenuItem.Checked = false;
            this.programmerToolStripMenuItem.Checked = false;

            // Clear operation and refresh text
            if(this.Mode != null)
            {
                this.Mode.ClearOperation();
                this.refreshEntryText();
                this.refreshResultText();
            }

            // Define calculator mode
            String mode = "";

            switch (calculatorMode)
            {
                case CalculatorMode.StandardMode:
                    Mode = new StandardMode();
                    mode = "Standard";
                    this.labelMode.Text = mode;
                    this.standardToolStripMenuItem.Checked = true;
                    break;
                case CalculatorMode.ScietificMode:
                    this.Mode = new ScientificMode();
                    mode = "Scientific";
                    this.labelMode.Text = mode;
                    this.scietificToolStripMenuItem.Checked = true;
                    break;
                case CalculatorMode.ProgrammerMode:
                    this.Mode = new ProgrammerMode();
                    mode = "Programmer";
                    this.labelMode.Text = mode;
                    this.programmerToolStripMenuItem.Checked = true;
                    break;
                default:
                    mode = "Standard";
                    this.labelMode.Text = mode;
                    break;
            }

            // Set and save calculator mode
            Properties.Settings.Default.mode = mode;
            Properties.Settings.Default.Save();
        }

        #endregion

        #region UI Main Form Load Methods

        //Load main form
        private void FormCalculator_Load(object sender, EventArgs e)
        {
            // Hide Clear History Button
            this.buttonClearHistory.Visible = false;

            // Add Key Event Handlers
            this.KeyPreview = true;
            this.KeyDown += OnKeyDown;

            this.KeyPress += OnKeyPress;

            // Set show history on form load
            this.DoShowHistory();

            // Load history log entries
            this.LoadHistoryLogList();
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

            // Set equals operation key press
            if (e.KeyCode == Keys.Return)
            {
                this.operation_button_Click(this.button21, null);
            }

            // Set backspace operation key press
            if(e.KeyCode == Keys.Back)
            {
                this.operation_button_Click(this.button4, null);
            }
        }

        // Set digit and operation key presses
        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            String Key = e.KeyChar.ToString();

            switch (Key)
            {
                case "0":
                    this.number_button_Click(this.button23, null);
                    break;
                case "1":
                    this.number_button_Click(this.button19, null);
                    break;
                case "2":
                    this.number_button_Click(this.button18, null);
                    break;
                case "3":
                    this.number_button_Click(this.button17, null);
                    break;
                case "4":
                    this.number_button_Click(this.button14, null);
                    break;
                case "5":
                    this.number_button_Click(this.button13, null);
                    break;
                case "6":
                    this.number_button_Click(this.button12, null);
                    break;
                case "7":
                    this.number_button_Click(this.button9, null);
                    break;
                case "8":
                    this.number_button_Click(this.button8, null);
                    break;
                case "9":
                    this.number_button_Click(this.button7, null);
                    break;
                case "+":
                    this.operation_button_Click(this.button16, null);
                    break;
                case "-":
                    this.operation_button_Click(this.button11, null);
                    break;
                case "*":
                    this.operation_button_Click(this.button6, null);
                    break;
                case "/":
                    this.operation_button_Click(this.button5, null);
                    break;
                case "%":
                    this.operation_button_Click(this.button1, null);
                    break;
                case ".":
                    this.number_button_Click(this.button22, null);
                    break;
                case "←":
                    this.operation_button_Click(this.button19, null);
                    break;
                default:
                    break;
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
                    this.textBoxEntry.Text = FormatUtils.FormatText(number.ToString());
                   this.Mode.EntryText = FormatUtils.FormatText(number.ToString());
                }
            }
        }

        #endregion

        #region Utility Methods

        // Determine if the previous button click is an operation
        private bool isAriphmeticOperation()
        {
            return this.Mode.PrevButtonSender.Equals("+") || this.Mode.PrevButtonSender.Equals("-") ||
            this.Mode.PrevButtonSender.Equals("X") || this.Mode.PrevButtonSender.Equals("÷") ||
            this.Mode.PrevButtonSender.Equals("=");
        }

        // Insert history log entry into database
        private void InsertHistoryLogDB()
        {
            Data sql = new Data();
            sql.InsertHistoryEntry(Mode.HistoryLog.HistoryLogEntry);
        }

        // Load history log entry from a database
        private void LoadHistoryLogList()
        {
            Data sql = new Data();
            Dictionary<int, HistoryLog> dictHistoryLog = sql.GetHistoryEntryList();

            foreach (KeyValuePair<int, HistoryLog> entry in dictHistoryLog)
            {
                String historyLogStr = entry.Value.HistoryLogEntry;
                if (historyLogStr != null)
                {
                    this.CreateHistoryLog(historyLogStr);
                }
            }
        }

        // Delete all history log entries from a database
        private bool DeleteHistoryLogDB()
        {
            Data sql = new Data();
            return sql.DeleteHistoryEntry();
        }

        #endregion
    }
}