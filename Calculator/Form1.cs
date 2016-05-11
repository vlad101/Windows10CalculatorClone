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
using Calculator.Memory;
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

        // Store flow layout status
        private FlowLayoutPanelStatus FlowLayoutPanelStatus;

        // Create counter for memory log
        private int MemoryLogCount = 0;

        // Store memory log
        MemoryLog memoryLog = new MemoryLog();

        // Load calculator main form
        public FormCalculator()
        {
            // Initialize main form
            InitializeComponent();

            // Set calculator mode
            this.SetMode();

            // Set history flow layout panel
            this.FlowLayoutPanelStatus = FlowLayoutPanelStatus.History;

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
            // If entry text is an invalid input, clear result and entry text
            // If the prev operation button was power, clear result and entry text and allow power operations
            if (this.Mode.EntryText.Equals("Invalid Input") || 
                    this.Mode.EntryText.Equals("Cannot divide by zero") || 
                        this.Mode.EntryText.Equals("NaN") ||
                            !this.Mode.allowExpOps)
            {
                this.Mode.ResultText = "";
                this.Mode.EntryText = "";
                this.Mode.allowExpOps = true;
            }
            
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

            // Operation is memory or exponent can be performed even when no data entered
            if (this.Mode.EntryText == null)
            {
                this.Mode.EntryText = "0";
            }

            // If entry text is an invalid input, clear result and entry text
            // If the prev operation button was power, clear result and entry text and allow power operations
            if (this.Mode.EntryText.Equals("Invalid Input") 
                    || this.Mode.EntryText.Equals("Cannot divide by zero")
                        || this.Mode.EntryText.Equals("NaN"))
            {
                this.Mode.ResultText = "";
                this.Mode.EntryText = "";
                this.Mode.allowExpOps = true;
            }

            // Get last updated memory log entry id
            String lastMemoryId = DataMemory.LastInsertedMemoryEntryId.ToString();

            // Do the action
            if (this.Mode.EntryText != null)
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
                        // Set flow layout status to history
                        this.FlowLayoutPanelStatus = FlowLayoutPanelStatus.History;

                        // Perform equals operation
                        this.PerformEqualsOperation();
                        break;
                    // Exponent operations
                    case "x\xB2": // square

                        // Perform Exponent operation
                        this.PerformExponentOperation(2);

                        // Refresh entry textbox
                        this.refreshEntryText();

                        // Refresh result textbox
                        this.refreshResultText();

                        // Set exponent operation
                        operation = "Power";
                        
                        break;
                    case "x\xB3": // cube
                        
                        // Perform Exponent operation
                        this.PerformExponentOperation(3);

                        // Refresh entry textbox
                        this.refreshEntryText();

                        // Refresh result textbox
                        this.refreshResultText();

                        // Set exponent operation
                        operation = "Power";

                        break;

                    case "1 / x":
                        // Perform Exponent operation
                        this.PerformExponentOperation(-1);

                        // Refresh entry textbox
                        this.refreshEntryText();

                        // Refresh result textbox
                        this.refreshResultText();

                        // Set exponent operation
                        operation = "Power";

                        break;

                    case "\u221A": // square root

                        double num = -1;

                        if(Double.TryParse(this.Mode.EntryText, out num))
                        {
                            num = Double.Parse(this.Mode.EntryText);
                        }

                        // Do not allow negative values with square root
                        if (this.Mode.EntryText != null)
                        {
                            if (num >= 0)
                            {
                                // Perform Exponent operation
                                this.PerformExponentOperation(0.5);

                                // Set exponent operation
                                operation = "Power";
                            }
                            else
                            {
                                // Set result text entry
                                this.Mode.ResultText = this.Mode.ResultText + "sqrt(" + this.Mode.EntryText + ")";

                                // Set entry text entry
                                this.Mode.EntryText = "Invalid Input";

                                // Allow exponent operation
                                this.Mode.allowExpOps = false;
                            }
                        }

                        // Refresh entry textbox
                        this.refreshEntryText();

                        // Refresh result textbox
                        this.refreshResultText();

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
                    // Memory operations
                    case "MC":
                        if (DeleteMemoryEntry())
                        {
                            this.buttonClearLogMemory_Click(null, null);
                            // Clear memory
                            MemoryLog.MemoryClear();
                            // Set memory to false
                            this.SetMemoryStatus(false);
                        }
                        break;
                    case "MR":
                        // Memory recall
                        this.Mode.EntryText = Libs.FormatUtils.FormatText(this.GetMemoryEntryById(lastMemoryId));
                        // Refresh entry and result textbox
                        if (this.Mode.EntryText != null)
                        {
                            this.refreshEntryText();
                        }
                        break;
                    case "MS":
                        // Set flow layout status to memory
                        this.FlowLayoutPanelStatus = FlowLayoutPanelStatus.Memory;

                        if(this.Mode.EntryText != null && !this.Mode.EntryText.Equals("0"))
                        {
                            double number;
                            if(Double.TryParse(this.Mode.EntryText.Replace(",",""), out number))
                            {
                                // Memory store operation
                                int memoryStoreResult = this.memoryLog.MemoryStore(number);
                                if (memoryStoreResult != -1)
                                {
                                    // Create memory log
                                    String memoryLogStr = this.FormatMemoryLogEntry(memoryStoreResult.ToString(), Libs.FormatUtils.FormatText(number.ToString()));

                                    if (memoryLogStr != null)
                                    {
                                        this.CreateLog(Libs.FormatUtils.FormatText(memoryLogStr));
                                        // Set memory to true
                                        this.SetMemoryStatus(true);
                                    }
                                }
                            }
                        }
                        break;
                    case "M+":
                        // Set flow layout status to history
                        this.FlowLayoutPanelStatus = FlowLayoutPanelStatus.Memory;
                        
                        // If last inserted memory entry id is -1, insert data into memory
                        // If last inserted memory entry id is -1, update memory plus operation
                        if (lastMemoryId.Equals("-1"))
                        {
                            // Memory Store operation
                            this.operation_button_Click(this.msButton, null);

                            // Set memory to true
                            this.SetMemoryStatus(true);
                        }
                        else
                        {
                            // Get the last memory control
                            Control control = this.flowLayoutPanelMemory.Controls[this.flowLayoutPanelMemory.Controls.Count - 4];

                            double value;

                            if (Double.TryParse(this.Mode.EntryText, out value))
                            {
                                String expression = control.Text.Replace(",", "") + " + " + value.ToString().Replace(",", "");
                                String answer = Libs.OperationUtils.EvaluateExpression(expression);
                                if (this.UpdateMemoryEntryById(lastMemoryId, answer))
                                {
                                    control.Text = Libs.FormatUtils.FormatText(answer);
                                }
                            }
                        }
                        break;
                    case "M-":
                        // Set flow layout status to history
                        this.FlowLayoutPanelStatus = FlowLayoutPanelStatus.Memory;

                        // If last inserted memory entry id is -1, insert data into memory
                        // If last inserted memory entry id is -1, update memory minus operation
                        if (lastMemoryId.Equals("-1"))
                        {
                            // Memory Store operation
                            this.operation_button_Click(this.msButton, null);

                            // Set memory to true
                            this.SetMemoryStatus(true);
                        }
                        else
                        {

                            // Get the last memory control
                            Control control = this.flowLayoutPanelMemory.Controls[this.flowLayoutPanelMemory.Controls.Count - 4];

                            double value;

                            if (Double.TryParse(this.Mode.EntryText, out value))
                            {
                                String expression = control.Text.Replace(",", "") + " - " + value.ToString().Replace(",", "");
                                String answer = Libs.OperationUtils.EvaluateExpression(expression);
                                if (this.UpdateMemoryEntryById(lastMemoryId, answer))
                                {
                                    control.Text = Libs.FormatUtils.FormatText(answer);
                                }
                            }
                        }
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
            this.Mode.PrevButtonSender = operation;
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

        // Update the flow layout panel status
        private void refreshFlowLayoutStatus(FlowLayoutPanelStatus flowLayoutStatus)
        {
            // Get history log count
            int historyLogCount = this.flowLayoutPanelHistory.Controls.Count;
            
            // Get memory log count, 4 button = 1 memory log
            int memoryLogCount = this.flowLayoutPanelMemory.Controls.Count / 4;

            switch(flowLayoutStatus)
            {
                case FlowLayoutPanelStatus.History:
                    if (historyLogCount > 0)
                    {
                        this.labelFlowLayoutPanelStatus.Text = historyLogCount + " history log(s) added";
                    }
                    else
                    {                        
                        if(this.flowLayoutPanelHistory.Visible)
                        {
                            this.labelFlowLayoutPanelStatus.Text = "There's no history yet";
                    
                        }
                    }
                    break;
                case FlowLayoutPanelStatus.Memory:
                    if (memoryLogCount > 0)
                    {
                        this.labelFlowLayoutPanelStatus.Text = memoryLogCount + " memory log(s) added";
                    }
                    else
                    {
                        if(this.flowLayoutPanelMemory.Visible)
                        {
                            this.labelFlowLayoutPanelStatus.Text = "There's nothing saved in memory";
                        }
                    }
                    break;
            }
        }

        #endregion

        #region UI Memory Methods

        private void SetMemoryStatus(bool status)
        {
            if (status)
            {
                // Show memory "M"
                this.labelMemory.Text = "M";
                // Enable MC and MR buttons
                this.mcButton.Enabled = true;
                this.mrButton.Enabled = true;
            }
            else 
            {
                // Hide memory
                this.labelMemory.Text = "";
                // Disable MC and MR buttons
                this.mcButton.Enabled = false;
                this.mrButton.Enabled = false;

                // Set last inserted memory entry id to -1
                DataMemory.LastInsertedMemoryEntryId = -1;
            }
        }

        #endregion

        #region History and Memory Log Methods

        private void CreateLog(String entry)
        {
            // Create new log
            NonFocusButton button = new NonFocusButton();
            
            // Add style
            this.CreateLogStyle(button);

            // Set click event and set clear button
            switch (this.FlowLayoutPanelStatus)
            { 
                case FlowLayoutPanelStatus.History:

                    // Add text
                    button.Text = entry;

                    // Attach button to a layout panel
                    this.flowLayoutPanelHistory.Controls.Add(button);

                    // Set history log event
                    button.Click += HistoryLogEntry_Click;

                    // Show clear log list button
                    if (!this.buttonHistoryList.Enabled && this.buttonMemoryList.Enabled)
                    {
                        // Refresh flow layout status text
                        this.refreshFlowLayoutStatus(FlowLayoutPanelStatus.History);

                        // Show clear log button
                        this.buttonClearLogListHistory.Visible = true;
                    }
                    
                    //this.buttonClearLogListMemory.Visible = false;

                    break;

                case Libs.FlowLayoutPanelStatus.Memory:

                    if(entry.Split('=').Length > 1)
                    {
                        // Get entry id and entry log
                        String entryId = entry.Split('=')[0];
                        String entryLog = entry.Split('=')[1];

                        // Set last inserted memory entry id
                        DataMemory.LastInsertedMemoryEntryId = Int32.Parse(entryId);

                        // Add text
                        button.Text = Libs.FormatUtils.FormatText(entryLog);

                        // Attach button to a layout panel
                        this.flowLayoutPanelMemory.Controls.Add(button);

                        // Set memory log event
                        button.Click += MemoryLogEntry_Click;

                        // Add button MC to memory log
                        NonFocusButton buttonMC = new NonFocusButton();
                        this.CreateMemoryLogStyle(buttonMC);
                        buttonMC.Text = "MC";
                        buttonMC.Name = "memoryLogEntryId" + entryId;
                        buttonMC.Click += MemoryLogEntryButtonMC_Click;
                        this.flowLayoutPanelMemory.Controls.Add(buttonMC);

                        // Add button M+ to memory log
                        NonFocusButton buttonMPlus = new NonFocusButton();
                        this.CreateMemoryLogStyle(buttonMPlus);
                        buttonMPlus.Text = "M+";
                        buttonMPlus.Name = "memoryLogEntryId" + entryId;
                        buttonMPlus.Click += MemoryLogEntryButtonMPlus_Click;
                        this.flowLayoutPanelMemory.Controls.Add(buttonMPlus);
                    
                        // Add button M- to memory log
                        NonFocusButton buttonMMinus = new NonFocusButton();
                        this.CreateMemoryLogStyle(buttonMMinus);
                        buttonMMinus.Text = "M-";
                        buttonMMinus.Name = "memoryLogEntryId" + entryId;
                        buttonMMinus.Click += MemoryLogEntryButtonMMinus_Click;
                        this.flowLayoutPanelMemory.Controls.Add(buttonMMinus);

                        this.MemoryLogCount++;

                        // Show clear log list button
                        if (this.buttonHistoryList.Enabled  && !this.buttonMemoryList.Enabled)
                        {
                            this.buttonClearLogListMemory.Visible = true;
                        }
                        
                        //this.buttonClearLogListHistory.Visible = false;

                        // Refresh flow layout status text
                        if (this.buttonHistoryList.Enabled && !this.buttonMemoryList.Enabled)
                        {
                            this.refreshFlowLayoutStatus(FlowLayoutPanelStatus.Memory);
                        }
                    }

                    break;
            }
        }

        private void buttonClearLogHistory_Click(object sender, EventArgs e)
        {
            // Delete history log entry from db
            if (this.DeleteLogDB(FlowLayoutPanelStatus.History))
            {
                this.RemoveFlowLayoutControls(FlowLayoutPanelStatus.History);
            }

            // Refresh flow layout status text
            this.refreshFlowLayoutStatus(FlowLayoutPanelStatus.History);
        }

        private void buttonClearLogMemory_Click(object sender, EventArgs e)
        {
            // Delete memory log entry from db
            if (this.DeleteLogDB(FlowLayoutPanelStatus.Memory))
            {
                // Remove memory log list
                this.RemoveFlowLayoutControls(FlowLayoutPanelStatus.Memory);

                // Set memory to false
                this.SetMemoryStatus(false);

                // Set last updated memory id to -1
                DataMemory.LastInsertedMemoryEntryId = -1;
            }

            // Refresh flow layout status text
            this.refreshFlowLayoutStatus(FlowLayoutPanelStatus.Memory);
        }

        private void MemoryLogEntryButtonMC_Click(object sender, EventArgs e)
        {
            NonFocusButton clickedButton = (NonFocusButton)sender;
            
            // Get button name
            String buttonName = (string)clickedButton.Name;

            // Delete memory log entry along with 3 buttons
            int indexControl = this.flowLayoutPanelMemory.Controls.IndexOf((Control)sender);

            // Find and extract digit memory log id from button name
            String memoryId = string.Join("", buttonName.ToCharArray().Where(Char.IsDigit));

            if(this.DeleteMemoryEntryById(memoryId) != -1)
            {
                for (int i = 0; i < 4; i++)
                {
                    this.flowLayoutPanelMemory.Controls.Remove(this.flowLayoutPanelMemory.Controls[indexControl - 1]);
                }
            }

            // If flow layout panel has no more memory entries, hide clear log list memory button
            if (this.flowLayoutPanelMemory.Controls.Count == 0)
            {
                this.buttonClearLogListMemory.Visible = false;
                this.SetMemoryStatus(false);

                // Set last updated memory id to -1
                DataMemory.LastInsertedMemoryEntryId = -1;
            }

            // Refresh flow layout status text
            this.refreshFlowLayoutStatus(FlowLayoutPanelStatus.Memory);
        }

        private void MemoryLogEntryButtonMPlus_Click(object sender, EventArgs e)
        {
            NonFocusButton clickedButton = (NonFocusButton)sender;

            // Get button name
            String buttonName = (string)clickedButton.Name;

            // Find and extract digit memory log id from button name
            String memoryId = string.Join("", buttonName.ToCharArray().Where(Char.IsDigit));

            // Delete memory log entry along with 3 buttons
            int indexControl = this.flowLayoutPanelMemory.Controls.IndexOf((Control)sender);

            Control control = this.flowLayoutPanelMemory.Controls[indexControl - 2];

            double value;

            if (Double.TryParse(this.Mode.EntryText, out value))
            {
                String expression = control.Text.Replace(",", "") + " + " + value.ToString().Replace(",", "");

                String answer = Libs.OperationUtils.EvaluateExpression(expression);
                
                if (this.UpdateMemoryEntryById(memoryId, answer))
                {
                    control.Text = Libs.FormatUtils.FormatText(answer);
                }
            }
        }

        private void MemoryLogEntryButtonMMinus_Click(object sender, EventArgs e)
        {
            NonFocusButton clickedButton = (NonFocusButton)sender;

            // Get button name
            String buttonName = (string)clickedButton.Name;

            // Find and extract digit memory log id from button name
            String memoryId = string.Join("", buttonName.ToCharArray().Where(Char.IsDigit));

            // Delete memory log entry along with 3 buttons
            int indexControl = this.flowLayoutPanelMemory.Controls.IndexOf((Control)sender);

            Control control = this.flowLayoutPanelMemory.Controls[indexControl - 3];

            double value;

            if (Double.TryParse(this.Mode.EntryText, out value))
            {
                String expression = control.Text.Replace(",", "") + "-" + value.ToString().Replace(",", "");
                
                String answer = Libs.OperationUtils.EvaluateExpression(expression);

                if (this.UpdateMemoryEntryById(memoryId, answer))
                {
                    control.Text = Libs.FormatUtils.FormatText(answer);
                }
            }
        }

        // Display history in entry and result textbox
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

        // Display history in entry and result textbox
        private void MemoryLogEntry_Click(object sender, EventArgs e)
        {
            NonFocusButton clickedButton = (NonFocusButton)sender;

            // Get button name
            String entry = (string)clickedButton.Text;

            // Set memory log entry
            this.Mode.EntryText = entry;

            // Set result entry
            this.Mode.ResultText = "";

            // Refresh entry and result entry
            this.refreshEntryText();
            this.refreshResultText();
        }

        // Flow layout panel control log style
        private void CreateLogStyle(NonFocusButton buttonLog)
        {
            buttonLog.Width = 183;
            buttonLog.Height = 40;
            buttonLog.Margin = new Padding(3, 0, 0, 3);
            buttonLog.Font = new Font("Microsoft Sans Serif", 9.00F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            buttonLog.TextAlign = ContentAlignment.MiddleRight;
            buttonLog.FlatStyle = FlatStyle.Flat;
            buttonLog.FlatAppearance.BorderColor = Color.Gray;
        }

        // Memory Flow layout panel control log style
        private void CreateMemoryLogStyle(NonFocusButton buttonLog)
        {
            buttonLog.Width = 59;
            buttonLog.Height = 23;
            buttonLog.Margin = new Padding(3, 0, 0, 5);
            buttonLog.Font = new Font("Microsoft Sans Serif", 7.00F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            buttonLog.TextAlign = ContentAlignment.MiddleCenter;
            buttonLog.FlatStyle = FlatStyle.Flat;
            buttonLog.FlatAppearance.BorderColor = Color.Gray;
        }

        // Make the FlowLayoutPanel scroll with the mouse wheel
        private void flowLayoutPanel_Paint(object sender, PaintEventArgs e)
        {
            // FlowLayoutPanel will scroll only if it has focus
            this.flowLayoutPanelHistory.Focus();
            this.flowLayoutPanelMemory.Focus();
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

        // Insert history log entry into database
        private bool InsertHistoryLogDB()
        {
            DataHistory sql = new DataHistory();

            if (this.Mode.HistoryLog == null)
                return false;

            return sql.InsertHistoryEntry(this.Mode.HistoryLog.HistoryLogEntry);
        }

        // Delete memory log entry by id
        private int DeleteMemoryEntryById(String memoryEntryId)
        {
            DataMemory sql = new DataMemory();
            return sql.DeleteMemoryEntryById(memoryEntryId);
        }

        // Delete all memory log entries
        private bool DeleteMemoryEntry()
        {
            DataMemory sql = new DataMemory();
            return sql.DeleteMemoryEntry();
        }

        // Update memory log entry by id
        private bool UpdateMemoryEntryById(String memoryEntryId, String memoryEntry)
        {
            DataMemory sql = new DataMemory();
            return sql.UpdateMemoryEntry(memoryEntryId, memoryEntry);
        }

        // Get memory log entry by id
        private String GetMemoryEntryById(String memoryEntryId)
        {
            DataMemory sql = new DataMemory();
            return sql.GetMemoryEntryById(memoryEntryId);
        }

        // Load history log entry from a database
        private void LoadLogList()
        {
            // Load database history logs
            DataHistory sqlHistory = new DataHistory();
            Dictionary<int, HistoryLog> dictHistoryLog = sqlHistory.GetHistoryEntryList();

            foreach (KeyValuePair<int, HistoryLog> entry in dictHistoryLog)
            {
                // Set default flow layout panel to history
                this.FlowLayoutPanelStatus = FlowLayoutPanelStatus.History;
                String historyLogStr = entry.Value.HistoryLogEntry;
                if (historyLogStr != null)
                {
                    this.CreateLog(historyLogStr);
                }
            }

            // Load database memory logs
            DataMemory sqlMemory = new DataMemory();
            Dictionary<int, MemoryLog> dictMemoryLog = sqlMemory.GetMemoryEntryList();

            foreach (KeyValuePair<int, MemoryLog> entry in dictMemoryLog)
            {
                // Set default flow layout panel to memory
                this.FlowLayoutPanelStatus = FlowLayoutPanelStatus.Memory;
                // Create memory log
                String memoryLogStr = this.FormatMemoryLogEntry(entry.Key.ToString(), entry.Value.MemoryLogEntry);
                if (memoryLogStr != null)
                {
                    this.CreateLog(Libs.FormatUtils.FormatText(memoryLogStr));
                }
            }

            // If dictionary memory log contains logs, enable MC, MR buttons
            if (dictMemoryLog.Count > 0)
                this.SetMemoryStatus(true);
            else
                this.SetMemoryStatus(false);

            // Set default flow layout panel to history
            this.FlowLayoutPanelStatus = FlowLayoutPanelStatus.History;

            // Show history delete button
            if (this.flowLayoutPanelHistory.Controls.Count > 0)
            {
                this.buttonClearLogListHistory.Visible = true;
            }
            else
            {
                this.buttonClearLogListHistory.Visible = false;
            }
            
            // Hide memory delete button
            this.buttonClearLogListMemory.Visible = false;
        }

        // Delete all history log entries from a database
        private bool DeleteLogDB(FlowLayoutPanelStatus flowLayoutStatus)
        {
            bool result = false;
            switch (flowLayoutStatus)
            {
                case FlowLayoutPanelStatus.History:
                    DataHistory sqlHistory = new DataHistory();
                    result = sqlHistory.DeleteHistoryEntry();
                    break;
                case FlowLayoutPanelStatus.Memory:
                    DataMemory sqlMemory = new DataMemory();
                    result = sqlMemory.DeleteMemoryEntry();
                    break;
            }
            return result;
        }

        // Show or hide history; set axis, y-axis are the same for both
        private void SetFormSize(int xAxis)
        {
            // Set calculator dimensions
            this.MaximumSize = new System.Drawing.Size(xAxis, 390);
            this.MinimumSize = new System.Drawing.Size(xAxis, 390);

            // Save the default value
            Properties.Settings.Default.showHistory = this.ShowHistory;
            Properties.Settings.Default.Save();
        }

        #endregion

        #region Flow Layout Panel Methods

        private void LoadFlowLayoutPanel() 
        {
            switch(this.FlowLayoutPanelStatus)
            {
                case FlowLayoutPanelStatus.History:

                    // Disable history button, enable memory button
                    this.buttonHistoryList.Enabled = false;
                    this.buttonMemoryList.Enabled = true;

                    // Load flow layout panel history
                    this.flowLayoutPanelHistory.Show();
                    this.flowLayoutPanelMemory.Hide();

                    // Refresh flow layout status text
                    if (!this.buttonHistoryList.Enabled && this.buttonMemoryList.Enabled)
                    {
                        this.refreshFlowLayoutStatus(FlowLayoutPanelStatus.History);
                    }

                    break;
                case FlowLayoutPanelStatus.Memory:

                    // Enable history button, disable memory button
                    this.buttonHistoryList.Enabled = true;
                    this.buttonMemoryList.Enabled = false;

                    // Load flow layout panel history
                    this.flowLayoutPanelMemory.Show();
                    this.flowLayoutPanelHistory.Hide();

                    // Refresh flow layout status text
                    if (this.buttonHistoryList.Enabled && !this.buttonMemoryList.Enabled)
                    {
                        this.refreshFlowLayoutStatus(FlowLayoutPanelStatus.Memory);
                    }

                    break;
            }
        }

        // Click button displays history list in a flow layout panel
        private void buttonLogListHistory_Click(object sender, EventArgs e)
        {
            // Enable history button, disable memory button
            this.buttonHistoryList.Enabled = false;
            this.buttonMemoryList.Enabled = true;

            // Load flow layout panel memory
            this.flowLayoutPanelHistory.Show();
            this.flowLayoutPanelMemory.Hide();

            // Hide clear memory button, show clear history button
            if (this.flowLayoutPanelHistory.Controls.Count > 0)
            {
                this.buttonClearLogListHistory.Visible = true;
            }
            else
            {
                this.buttonClearLogListHistory.Visible = false;
            }

            // Hide clear memory button
            this.buttonClearLogListMemory.Visible = false;

            // Refresh flow layout status text
            if (!this.buttonHistoryList.Enabled && this.buttonMemoryList.Enabled)
            {
                this.refreshFlowLayoutStatus(FlowLayoutPanelStatus.History);
            }
        }

        // Click button displays history list in a flow layout panel
        private void buttonLogListMemory_Click(object sender, EventArgs e)
        {
            // Disable history button, enable memory button
            this.buttonHistoryList.Enabled = true;
            this.buttonMemoryList.Enabled = false;

            // Load flow layout panel memory
            this.flowLayoutPanelMemory.Show();
            this.flowLayoutPanelHistory.Hide();

            // Show clear memory button, hide clear history button
            if (this.flowLayoutPanelMemory.Controls.Count > 0)
            {
                this.buttonClearLogListMemory.Visible = true;
            }
            else
            {
                this.buttonClearLogListMemory.Visible = false;
            }

            // Hide clear log history button
            this.buttonClearLogListHistory.Visible = false;

            // Refresh flow layout status text
            this.refreshFlowLayoutStatus(FlowLayoutPanelStatus.Memory);
        }

        // Hide flowlayout panel and its controls
        private void RemoveFlowLayoutControls(FlowLayoutPanelStatus flowLayoutStatus)
        {
            switch (flowLayoutStatus)
            {
                case FlowLayoutPanelStatus.History:

                    // Delete button controls from a flow layout panel history
                    this.flowLayoutPanelHistory.Controls.Clear();

                    // Hide button clear history button
                    this.buttonClearLogListHistory.Visible = false;

                    // Remove vertical scroll bar
                    this.flowLayoutPanelHistory.VerticalScroll.Visible = false;

                    break;
                case FlowLayoutPanelStatus.Memory:
                    
                    // Delete button controls from a flow layout panel history
                    this.flowLayoutPanelMemory.Controls.Clear();

                    // Hide clear memory button
                    this.buttonClearLogListMemory.Visible = false;

                    // Remove vertical scroll bar
                    this.flowLayoutPanelMemory.VerticalScroll.Visible = false;

                    // Set memory label status to nothing
                    this.labelMemory.Text = "";

                    break;
            }

        }

        #endregion

        #region Perfom Ariphmetic, Exponent and Equals Oper

        // Perform ariphmetic operation
        private void PerformAriphmeticOperation(OperationType operation)
        {
            // Set ariphmetic operation
            this.Mode.ArithmeticOperation(operation);

            // Refresh entry and result textboxes
            this.refreshEntryText();
            this.refreshResultText();

            // Clear entry text value
            this.Mode.EntryText = "";

            // Allow exp operation
            this.Mode.allowExpOps = true;
        }

        // Perform exponent operation
        private void PerformExponentOperation(double exp)
        {
            String expStr = "";

            if (exp == 2)
            {
                expStr = "sqr";
            }
            else if (exp == 3)
            {
                expStr = "cube";
            }
            else if (exp == 0.5)
            {
                expStr = "sqrt";
            }
            else if (exp == -1)
            {
                expStr = "reciproc";
            }

            this.Mode.EntryText = FormatUtils.TrimDouble(this.Mode.EntryText);
            
            if(this.Mode.allowExpOps)
            {
                if (this.Mode.EntryText.Length == 0)
                {
                    // Allow exp operation
                    this.Mode.allowExpOps = false;

                    // Set result entry text
                    this.Mode.ResultText = this.Mode.ResultText + expStr + "(" + 0 + ")";

                    // Set entry text to the cube entry text value
                    this.Mode.EntryText = "0";
                }
                else
                {
                    if (!this.Mode.PrevButtonSender.Equals("Power"))
                    {
                        if (this.isLastCharAriphmeticOperation() || this.Mode.EntryText != null)
                        {
                            // Set result entry text
                            this.Mode.ResultText = this.Mode.ResultText + expStr + "(" + this.Mode.EntryText + ")";

                            // Set entry text to the exponent entry text value
                            if (expStr != "sqrt")
                            {
                                // Exponent
                                this.Mode.EntryText = Math.Pow(double.Parse(this.Mode.EntryText.Replace(",", "")), exp).ToString();
                            }
                            else
                            {
                                // Square root
                                this.Mode.EntryText = Math.Sqrt(double.Parse(this.Mode.EntryText.Replace(",", ""))).ToString();
                            }

                            // Allow exp operation
                            this.Mode.allowExpOps = false;
                        }
                    }
                }
            }
        }

        // Perform equals operation
        private void PerformEqualsOperation()
        {
            // If the last operator was equals operation, repeat the computattion
            if (this.Mode.PrevButtonSender.Equals("=") || this.Mode.PrevButtonSender.Equals("History"))
            {
                if (this.Mode.EntryText != null && this.Mode.EntryText.Trim().Length != 0)
                {
                    // Repeat a previous operation
                    if (this.Mode.RepeatCalcOnEqualsOperator())
                    {
                        // Create a history log
                        this.CreateLog(Mode.HistoryLog.HistoryLogEntry);
                    }

                    // Allow exp operation
                    this.Mode.allowExpOps = true;
                }
            }
            else
            {
                // Calculate expression
                if (this.Mode.ResultText != null && this.Mode.ResultText.Trim().Length != 0)
                {
                    if (this.Mode.EqualsOperation())
                    {
                        if (this.Mode.HistoryLog != null)
                        {
                            // Create a history log
                            this.CreateLog(this.Mode.HistoryLog.HistoryLogEntry);
                        }
                        // Clear result text value
                        this.Mode.ResultText = "";

                        // Refresh result textbox
                        this.refreshResultText();

                        // Allow exp operation
                        this.Mode.allowExpOps = true;
                    }
                }
            }

            // Refresh entry textbox
            this.refreshEntryText();

            // Insert data to the database
            this.InsertHistoryLogDB();
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
            this.buttonClearLogListHistory.Visible = false;

            // Add Key Event Handlers
            this.KeyPreview = true;
            this.KeyDown += OnKeyDown;

            this.KeyPress += OnKeyPress;

            // Set show history on form load
            this.DoShowHistory();

            // Load history log entries
            this.LoadLogList();

            // Load flat layout panel history
            this.LoadFlowLayoutPanel();
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

        // Determine if the last entry text character is an operator sign
        private bool isLastCharAriphmeticOperation()
        {
            try 
            {
                string c = this.Mode.ResultText.Trim()[this.Mode.ResultText.Trim().Length - 1].ToString();
                return c.Equals("+") || c.Equals("-") || c.Equals("X") || c.Equals("÷") || c.Equals("=");
            } catch(Exception e)
            {
                return true;
            }
        }

        // Format entry memory log as entry id, entry log pair.l Ex: EntryId=EntryLog
        private String FormatMemoryLogEntry(String entryId, String entryValue)
        {
            StringBuilder memoryLogStr = new System.Text.StringBuilder();
            memoryLogStr.Append(String.Format("{0}={1}", entryId, entryValue));
            return memoryLogStr.ToString();
        }

        #endregion
    }
}