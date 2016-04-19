using Calculator.Controls;

namespace Calculator
{
    partial class FormCalculator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanelHistory = new System.Windows.Forms.FlowLayoutPanel();
            this.textBoxEntry = new System.Windows.Forms.TextBox();
            this.menuStripMode = new System.Windows.Forms.MenuStrip();
            this.modeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.standardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scietificToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programmerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyCtrlCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteCtrlVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpF1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutCalculatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelMode = new System.Windows.Forms.Label();
            this.buttonHistory = new System.Windows.Forms.Button();
            this.labelMemory = new System.Windows.Forms.Label();
            this.flowLayoutPanelMemory = new System.Windows.Forms.FlowLayoutPanel();
            this.labelFlowLayoutPanelStatus = new System.Windows.Forms.Label();
            this.buttonClearLogListMemory = new Calculator.Controls.NonFocusButton();
            this.buttonMemoryList = new Calculator.Controls.NonFocusButton();
            this.buttonHistoryList = new Calculator.Controls.NonFocusButton();
            this.mMinusButton = new Calculator.Controls.NonFocusButton();
            this.mPlusButton = new Calculator.Controls.NonFocusButton();
            this.msButton = new Calculator.Controls.NonFocusButton();
            this.mrButton = new Calculator.Controls.NonFocusButton();
            this.mcButton = new Calculator.Controls.NonFocusButton();
            this.buttonClearLogListHistory = new Calculator.Controls.NonFocusButton();
            this.button21 = new Calculator.Controls.NonFocusButton();
            this.button22 = new Calculator.Controls.NonFocusButton();
            this.button23 = new Calculator.Controls.NonFocusButton();
            this.button24 = new Calculator.Controls.NonFocusButton();
            this.button25 = new Calculator.Controls.NonFocusButton();
            this.button16 = new Calculator.Controls.NonFocusButton();
            this.button17 = new Calculator.Controls.NonFocusButton();
            this.button18 = new Calculator.Controls.NonFocusButton();
            this.button19 = new Calculator.Controls.NonFocusButton();
            this.button20 = new Calculator.Controls.NonFocusButton();
            this.button11 = new Calculator.Controls.NonFocusButton();
            this.button12 = new Calculator.Controls.NonFocusButton();
            this.button13 = new Calculator.Controls.NonFocusButton();
            this.button14 = new Calculator.Controls.NonFocusButton();
            this.button15 = new Calculator.Controls.NonFocusButton();
            this.button6 = new Calculator.Controls.NonFocusButton();
            this.button7 = new Calculator.Controls.NonFocusButton();
            this.button8 = new Calculator.Controls.NonFocusButton();
            this.button9 = new Calculator.Controls.NonFocusButton();
            this.button10 = new Calculator.Controls.NonFocusButton();
            this.button5 = new Calculator.Controls.NonFocusButton();
            this.button4 = new Calculator.Controls.NonFocusButton();
            this.button3 = new Calculator.Controls.NonFocusButton();
            this.button2 = new Calculator.Controls.NonFocusButton();
            this.button1 = new Calculator.Controls.NonFocusButton();
            this.menuStripMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxResult
            // 
            this.textBoxResult.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.textBoxResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxResult.Enabled = false;
            this.textBoxResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxResult.Location = new System.Drawing.Point(13, 29);
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxResult.Size = new System.Drawing.Size(371, 24);
            this.textBoxResult.TabIndex = 0;
            this.textBoxResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(403, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(2, 255);
            this.label1.TabIndex = 26;
            // 
            // flowLayoutPanelHistory
            // 
            this.flowLayoutPanelHistory.AutoScroll = true;
            this.flowLayoutPanelHistory.Cursor = System.Windows.Forms.Cursors.Default;
            this.flowLayoutPanelHistory.Location = new System.Drawing.Point(423, 73);
            this.flowLayoutPanelHistory.Name = "flowLayoutPanelHistory";
            this.flowLayoutPanelHistory.Size = new System.Drawing.Size(211, 233);
            this.flowLayoutPanelHistory.TabIndex = 29;
            this.flowLayoutPanelHistory.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel_Paint);
            // 
            // textBoxEntry
            // 
            this.textBoxEntry.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.textBoxEntry.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxEntry.Enabled = false;
            this.textBoxEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxEntry.Location = new System.Drawing.Point(12, 53);
            this.textBoxEntry.Name = "textBoxEntry";
            this.textBoxEntry.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxEntry.Size = new System.Drawing.Size(371, 31);
            this.textBoxEntry.TabIndex = 30;
            this.textBoxEntry.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // menuStripMode
            // 
            this.menuStripMode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modeToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripMode.Location = new System.Drawing.Point(0, 0);
            this.menuStripMode.Name = "menuStripMode";
            this.menuStripMode.Size = new System.Drawing.Size(646, 24);
            this.menuStripMode.Stretch = false;
            this.menuStripMode.TabIndex = 31;
            this.menuStripMode.Text = "menuStripMode";
            // 
            // modeToolStripMenuItem
            // 
            this.modeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.standardToolStripMenuItem,
            this.scietificToolStripMenuItem,
            this.programmerToolStripMenuItem});
            this.modeToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.modeToolStripMenuItem.Text = "Mode";
            // 
            // standardToolStripMenuItem
            // 
            this.standardToolStripMenuItem.Name = "standardToolStripMenuItem";
            this.standardToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.standardToolStripMenuItem.Text = "Standard";
            this.standardToolStripMenuItem.Click += new System.EventHandler(this.standardToolStripMenuItem_Click);
            // 
            // scietificToolStripMenuItem
            // 
            this.scietificToolStripMenuItem.Name = "scietificToolStripMenuItem";
            this.scietificToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.scietificToolStripMenuItem.Text = "Scietific";
            this.scietificToolStripMenuItem.Click += new System.EventHandler(this.scietificToolStripMenuItem_Click);
            // 
            // programmerToolStripMenuItem
            // 
            this.programmerToolStripMenuItem.Name = "programmerToolStripMenuItem";
            this.programmerToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.programmerToolStripMenuItem.Text = "Programmer";
            this.programmerToolStripMenuItem.Click += new System.EventHandler(this.programmerToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyCtrlCToolStripMenuItem,
            this.pasteCtrlVToolStripMenuItem});
            this.editToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // copyCtrlCToolStripMenuItem
            // 
            this.copyCtrlCToolStripMenuItem.Name = "copyCtrlCToolStripMenuItem";
            this.copyCtrlCToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.copyCtrlCToolStripMenuItem.Text = "Copy                          Ctrl+C";
            this.copyCtrlCToolStripMenuItem.Click += new System.EventHandler(this.copyCtrlCToolStripMenuItem_Click);
            // 
            // pasteCtrlVToolStripMenuItem
            // 
            this.pasteCtrlVToolStripMenuItem.Name = "pasteCtrlVToolStripMenuItem";
            this.pasteCtrlVToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.pasteCtrlVToolStripMenuItem.Text = "Paste                         Ctrl+V";
            this.pasteCtrlVToolStripMenuItem.Click += new System.EventHandler(this.pasteCtrlVToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewHelpF1ToolStripMenuItem,
            this.aboutCalculatorToolStripMenuItem});
            this.helpToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // viewHelpF1ToolStripMenuItem
            // 
            this.viewHelpF1ToolStripMenuItem.Name = "viewHelpF1ToolStripMenuItem";
            this.viewHelpF1ToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.viewHelpF1ToolStripMenuItem.Text = "View Help                         F1";
            // 
            // aboutCalculatorToolStripMenuItem
            // 
            this.aboutCalculatorToolStripMenuItem.Name = "aboutCalculatorToolStripMenuItem";
            this.aboutCalculatorToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.aboutCalculatorToolStripMenuItem.Text = "About Calculator";
            // 
            // labelMode
            // 
            this.labelMode.AutoSize = true;
            this.labelMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMode.Location = new System.Drawing.Point(280, 3);
            this.labelMode.Name = "labelMode";
            this.labelMode.Size = new System.Drawing.Size(0, 17);
            this.labelMode.TabIndex = 32;
            // 
            // buttonHistory
            // 
            this.buttonHistory.Location = new System.Drawing.Point(308, 305);
            this.buttonHistory.Name = "buttonHistory";
            this.buttonHistory.Size = new System.Drawing.Size(75, 23);
            this.buttonHistory.TabIndex = 34;
            this.buttonHistory.Text = "History";
            this.buttonHistory.UseVisualStyleBackColor = true;
            this.buttonHistory.Click += new System.EventHandler(this.buttonHistory_Click);
            // 
            // labelMemory
            // 
            this.labelMemory.AutoSize = true;
            this.labelMemory.Location = new System.Drawing.Point(15, 68);
            this.labelMemory.Name = "labelMemory";
            this.labelMemory.Size = new System.Drawing.Size(16, 13);
            this.labelMemory.TabIndex = 40;
            this.labelMemory.Text = "M";
            // 
            // flowLayoutPanelMemory
            // 
            this.flowLayoutPanelMemory.AutoScroll = true;
            this.flowLayoutPanelMemory.Cursor = System.Windows.Forms.Cursors.Default;
            this.flowLayoutPanelMemory.Location = new System.Drawing.Point(423, 73);
            this.flowLayoutPanelMemory.Name = "flowLayoutPanelMemory";
            this.flowLayoutPanelMemory.Size = new System.Drawing.Size(211, 233);
            this.flowLayoutPanelMemory.TabIndex = 30;
            this.flowLayoutPanelMemory.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel_Paint);
            // 
            // labelFlowLayoutPanelStatus
            // 
            this.labelFlowLayoutPanelStatus.AutoSize = true;
            this.labelFlowLayoutPanelStatus.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFlowLayoutPanelStatus.Location = new System.Drawing.Point(423, 55);
            this.labelFlowLayoutPanelStatus.Name = "labelFlowLayoutPanelStatus";
            this.labelFlowLayoutPanelStatus.Size = new System.Drawing.Size(106, 14);
            this.labelFlowLayoutPanelStatus.TabIndex = 44;
            this.labelFlowLayoutPanelStatus.Text = "Flow Layout Status";
            // 
            // buttonClearLogListMemory
            // 
            this.buttonClearLogListMemory.BackgroundImage = global::Calculator.Properties.Resources.GarbageCan;
            this.buttonClearLogListMemory.Location = new System.Drawing.Point(610, 310);
            this.buttonClearLogListMemory.Name = "buttonClearLogListMemory";
            this.buttonClearLogListMemory.Size = new System.Drawing.Size(24, 27);
            this.buttonClearLogListMemory.TabIndex = 43;
            this.buttonClearLogListMemory.UseVisualStyleBackColor = true;
            this.buttonClearLogListMemory.Click += new System.EventHandler(this.buttonClearLogMemory_Click);
            // 
            // buttonMemoryList
            // 
            this.buttonMemoryList.Location = new System.Drawing.Point(484, 29);
            this.buttonMemoryList.Name = "buttonMemoryList";
            this.buttonMemoryList.Size = new System.Drawing.Size(54, 24);
            this.buttonMemoryList.TabIndex = 42;
            this.buttonMemoryList.Text = "Memory";
            this.buttonMemoryList.UseVisualStyleBackColor = true;
            this.buttonMemoryList.Click += new System.EventHandler(this.buttonLogListMemory_Click);
            // 
            // buttonHistoryList
            // 
            this.buttonHistoryList.Location = new System.Drawing.Point(424, 29);
            this.buttonHistoryList.Name = "buttonHistoryList";
            this.buttonHistoryList.Size = new System.Drawing.Size(54, 24);
            this.buttonHistoryList.TabIndex = 41;
            this.buttonHistoryList.Text = "History";
            this.buttonHistoryList.UseVisualStyleBackColor = true;
            this.buttonHistoryList.Click += new System.EventHandler(this.buttonLogListHistory_Click);
            // 
            // mMinusButton
            // 
            this.mMinusButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mMinusButton.Location = new System.Drawing.Point(308, 96);
            this.mMinusButton.Name = "mMinusButton";
            this.mMinusButton.Size = new System.Drawing.Size(75, 35);
            this.mMinusButton.TabIndex = 39;
            this.mMinusButton.Text = "M-";
            this.mMinusButton.UseVisualStyleBackColor = true;
            this.mMinusButton.Click += new System.EventHandler(this.operation_button_Click);
            // 
            // mPlusButton
            // 
            this.mPlusButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mPlusButton.Location = new System.Drawing.Point(234, 96);
            this.mPlusButton.Name = "mPlusButton";
            this.mPlusButton.Size = new System.Drawing.Size(75, 35);
            this.mPlusButton.TabIndex = 38;
            this.mPlusButton.Text = "M+";
            this.mPlusButton.UseVisualStyleBackColor = true;
            this.mPlusButton.Click += new System.EventHandler(this.operation_button_Click);
            // 
            // msButton
            // 
            this.msButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msButton.Location = new System.Drawing.Point(160, 96);
            this.msButton.Name = "msButton";
            this.msButton.Size = new System.Drawing.Size(75, 35);
            this.msButton.TabIndex = 37;
            this.msButton.Text = "MS";
            this.msButton.UseVisualStyleBackColor = true;
            this.msButton.Click += new System.EventHandler(this.operation_button_Click);
            // 
            // mrButton
            // 
            this.mrButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrButton.Location = new System.Drawing.Point(86, 96);
            this.mrButton.Name = "mrButton";
            this.mrButton.Size = new System.Drawing.Size(75, 35);
            this.mrButton.TabIndex = 36;
            this.mrButton.Text = "MR";
            this.mrButton.UseVisualStyleBackColor = true;
            this.mrButton.Click += new System.EventHandler(this.operation_button_Click);
            // 
            // mcButton
            // 
            this.mcButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mcButton.Location = new System.Drawing.Point(12, 96);
            this.mcButton.Name = "mcButton";
            this.mcButton.Size = new System.Drawing.Size(75, 35);
            this.mcButton.TabIndex = 35;
            this.mcButton.TabStop = false;
            this.mcButton.Text = "MC";
            this.mcButton.UseVisualStyleBackColor = true;
            this.mcButton.Click += new System.EventHandler(this.operation_button_Click);
            // 
            // buttonClearLogListHistory
            // 
            this.buttonClearLogListHistory.BackgroundImage = global::Calculator.Properties.Resources.GarbageCan;
            this.buttonClearLogListHistory.Location = new System.Drawing.Point(610, 310);
            this.buttonClearLogListHistory.Name = "buttonClearLogListHistory";
            this.buttonClearLogListHistory.Size = new System.Drawing.Size(24, 27);
            this.buttonClearLogListHistory.TabIndex = 33;
            this.buttonClearLogListHistory.UseVisualStyleBackColor = true;
            this.buttonClearLogListHistory.Click += new System.EventHandler(this.buttonClearLogHistory_Click);
            // 
            // button21
            // 
            this.button21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button21.Location = new System.Drawing.Point(308, 267);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(75, 35);
            this.button21.TabIndex = 25;
            this.button21.Text = "=";
            this.button21.UseVisualStyleBackColor = true;
            this.button21.Click += new System.EventHandler(this.operation_button_Click);
            // 
            // button22
            // 
            this.button22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button22.Location = new System.Drawing.Point(234, 267);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(75, 35);
            this.button22.TabIndex = 24;
            this.button22.Text = ".";
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new System.EventHandler(this.number_button_Click);
            // 
            // button23
            // 
            this.button23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button23.Location = new System.Drawing.Point(160, 267);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(75, 35);
            this.button23.TabIndex = 23;
            this.button23.Text = "0";
            this.button23.UseVisualStyleBackColor = true;
            this.button23.Click += new System.EventHandler(this.number_button_Click);
            // 
            // button24
            // 
            this.button24.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button24.Location = new System.Drawing.Point(86, 267);
            this.button24.Name = "button24";
            this.button24.Size = new System.Drawing.Size(75, 35);
            this.button24.TabIndex = 22;
            this.button24.Text = "±";
            this.button24.UseVisualStyleBackColor = true;
            this.button24.Click += new System.EventHandler(this.operation_button_Click);
            // 
            // button25
            // 
            this.button25.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button25.Location = new System.Drawing.Point(12, 267);
            this.button25.Name = "button25";
            this.button25.Size = new System.Drawing.Size(75, 35);
            this.button25.TabIndex = 21;
            this.button25.Text = "1 / x";
            this.button25.UseVisualStyleBackColor = true;
            this.button25.Click += new System.EventHandler(this.operation_button_Click);
            // 
            // button16
            // 
            this.button16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button16.Location = new System.Drawing.Point(308, 233);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(75, 35);
            this.button16.TabIndex = 20;
            this.button16.Text = "+";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.operation_button_Click);
            // 
            // button17
            // 
            this.button17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button17.Location = new System.Drawing.Point(234, 233);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(75, 35);
            this.button17.TabIndex = 19;
            this.button17.Text = "3";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.number_button_Click);
            // 
            // button18
            // 
            this.button18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button18.Location = new System.Drawing.Point(160, 233);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(75, 35);
            this.button18.TabIndex = 18;
            this.button18.Text = "2";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.number_button_Click);
            // 
            // button19
            // 
            this.button19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button19.Location = new System.Drawing.Point(86, 233);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(75, 35);
            this.button19.TabIndex = 17;
            this.button19.Text = "1";
            this.button19.UseVisualStyleBackColor = true;
            this.button19.Click += new System.EventHandler(this.number_button_Click);
            // 
            // button20
            // 
            this.button20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button20.Location = new System.Drawing.Point(12, 233);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(75, 36);
            this.button20.TabIndex = 16;
            this.button20.Text = "x³";
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Click += new System.EventHandler(this.operation_button_Click);
            // 
            // button11
            // 
            this.button11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button11.Location = new System.Drawing.Point(308, 199);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(75, 35);
            this.button11.TabIndex = 15;
            this.button11.Text = "-";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.operation_button_Click);
            // 
            // button12
            // 
            this.button12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button12.Location = new System.Drawing.Point(234, 199);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(75, 35);
            this.button12.TabIndex = 14;
            this.button12.Text = "6";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.number_button_Click);
            // 
            // button13
            // 
            this.button13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button13.Location = new System.Drawing.Point(160, 199);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(75, 35);
            this.button13.TabIndex = 13;
            this.button13.Text = "5";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.number_button_Click);
            // 
            // button14
            // 
            this.button14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button14.Location = new System.Drawing.Point(86, 199);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(75, 35);
            this.button14.TabIndex = 12;
            this.button14.Text = "4";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.number_button_Click);
            // 
            // button15
            // 
            this.button15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button15.Location = new System.Drawing.Point(12, 199);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(75, 35);
            this.button15.TabIndex = 11;
            this.button15.Text = "x²";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.operation_button_Click);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(308, 165);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 35);
            this.button6.TabIndex = 10;
            this.button6.Text = "X";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.operation_button_Click);
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Location = new System.Drawing.Point(234, 165);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 35);
            this.button7.TabIndex = 9;
            this.button7.Text = "9";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.number_button_Click);
            // 
            // button8
            // 
            this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.Location = new System.Drawing.Point(160, 165);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 35);
            this.button8.TabIndex = 8;
            this.button8.Text = "8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.number_button_Click);
            // 
            // button9
            // 
            this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.Location = new System.Drawing.Point(86, 165);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 35);
            this.button9.TabIndex = 7;
            this.button9.Text = "7";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.number_button_Click);
            // 
            // button10
            // 
            this.button10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button10.Location = new System.Drawing.Point(12, 165);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 34);
            this.button10.TabIndex = 6;
            this.button10.Text = "√";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.operation_button_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(308, 131);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 35);
            this.button5.TabIndex = 5;
            this.button5.Text = "÷";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.operation_button_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(234, 131);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 35);
            this.button4.TabIndex = 4;
            this.button4.Text = "←";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.operation_button_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(160, 131);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 35);
            this.button3.TabIndex = 3;
            this.button3.Text = "C";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.operation_button_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(86, 131);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 35);
            this.button2.TabIndex = 2;
            this.button2.Text = "CE";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.operation_button_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(12, 131);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 35);
            this.button1.TabIndex = 1;
            this.button1.TabStop = false;
            this.button1.Text = "%";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.operation_button_Click);
            // 
            // FormCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 342);
            this.Controls.Add(this.labelFlowLayoutPanelStatus);
            this.Controls.Add(this.buttonClearLogListMemory);
            this.Controls.Add(this.flowLayoutPanelMemory);
            this.Controls.Add(this.buttonMemoryList);
            this.Controls.Add(this.buttonHistoryList);
            this.Controls.Add(this.labelMemory);
            this.Controls.Add(this.mMinusButton);
            this.Controls.Add(this.mPlusButton);
            this.Controls.Add(this.msButton);
            this.Controls.Add(this.mrButton);
            this.Controls.Add(this.mcButton);
            this.Controls.Add(this.buttonHistory);
            this.Controls.Add(this.buttonClearLogListHistory);
            this.Controls.Add(this.labelMode);
            this.Controls.Add(this.textBoxEntry);
            this.Controls.Add(this.flowLayoutPanelHistory);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button21);
            this.Controls.Add(this.button22);
            this.Controls.Add(this.button23);
            this.Controls.Add(this.button24);
            this.Controls.Add(this.button25);
            this.Controls.Add(this.button16);
            this.Controls.Add(this.button17);
            this.Controls.Add(this.button18);
            this.Controls.Add(this.button19);
            this.Controls.Add(this.button20);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.menuStripMode);
            this.MainMenuStrip = this.menuStripMode;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(662, 380);
            this.MinimumSize = new System.Drawing.Size(662, 380);
            this.Name = "FormCalculator";
            this.Text = "Calculator";
            this.Load += new System.EventHandler(this.FormCalculator_Load);
            this.menuStripMode.ResumeLayout(false);
            this.menuStripMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelHistory;
        private System.Windows.Forms.TextBox textBoxEntry;
        private NonFocusButton button1;
        private NonFocusButton button2;
        private NonFocusButton button3;
        private NonFocusButton button4;
        private NonFocusButton button5;
        private NonFocusButton button6;
        private NonFocusButton button7;
        private NonFocusButton button8;
        private NonFocusButton button9;
        private NonFocusButton button10;
        private NonFocusButton button11;
        private NonFocusButton button12;
        private NonFocusButton button13;
        private NonFocusButton button14;
        private NonFocusButton button15;
        private NonFocusButton button16;
        private NonFocusButton button17;
        private NonFocusButton button18;
        private NonFocusButton button19;
        private NonFocusButton button20;
        private NonFocusButton button21;
        private NonFocusButton button22;
        private NonFocusButton button23;
        private NonFocusButton button24;
        private NonFocusButton button25;
        private System.Windows.Forms.MenuStrip menuStripMode;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem standardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scietificToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem programmerToolStripMenuItem;
        private System.Windows.Forms.Label labelMode;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewHelpF1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutCalculatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyCtrlCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteCtrlVToolStripMenuItem;
        private NonFocusButton buttonClearLogListHistory;
        private System.Windows.Forms.Button buttonHistory;
        private NonFocusButton mMinusButton;
        private NonFocusButton mPlusButton;
        private NonFocusButton msButton;
        private NonFocusButton mrButton;
        private NonFocusButton mcButton;
        private System.Windows.Forms.Label labelMemory;
        private NonFocusButton buttonHistoryList;
        private NonFocusButton buttonMemoryList;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMemory;
        private NonFocusButton buttonClearLogListMemory;
        private System.Windows.Forms.Label labelFlowLayoutPanelStatus;
    }
}

