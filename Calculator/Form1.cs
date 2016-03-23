using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Calculator.Mode;
using System.Windows.Forms;

namespace Calculator
{
    public partial class FormCalculator : Form
    {
        private StandardMode stMode;
        private ScientificMode scMode;
        private ProgrammerMode prMode;

        // Current user entry
        private string entryText;

        public FormCalculator()
        {
            InitializeComponent();

            // Set calculator scientific mode
            this.standardToolStripMenuItem_Click(null, null);

            // Set initial input to 0
            this.textBoxEntry.Text = "0";
        }

        private void number_button_Click(object sender, EventArgs e)
        {
            // Get the number when the number button is pressed
            Button numBtn = (Button)sender;

            // Append value to an input entry
            stMode.EntryText = this.textBoxEntry.Text;

            // Allow one decimal point
            if (numBtn.Text.Equals("."))
            {
                if(!stMode.EntryText.Contains("."))
                    stMode.EntryText = stMode.EntryText + numBtn.Text;
            }
            else
            {
                stMode.EntryText = stMode.EntryText + numBtn.Text;
            }

            this.clearZero();

            // Refresgh entry
            this.refreshEntryText();
        }

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
                    case "±":

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
                        break;
                    case "=":
                        if (stMode.EqualsOperation())
                            this.CreateHistoryLog("15.00000 + 9=\n24.0000");
                        break;
                    case "<-":

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
                        break;
                    case "CE":
                        stMode.EntryText = "0";
                        break;
                    default:
                        MessageBox.Show("Wrong operation");
                        break;
                }
            }
            else
            {
                stMode.EntryText = "0";
            }

            this.refreshEntryText();
        }

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

        private void refreshEntryText()
        {
            if(!stMode.EntryText.EndsWith(".") || !stMode.EntryText.Equals("0"))
                this.formatText();
            this.textBoxEntry.Text = stMode.EntryText;
        }

        private void formatText()
        {
            if(stMode.EntryText != null)
            {
                String formatStr;
                int decPlaces = stMode.EntryText.Substring(stMode.EntryText.LastIndexOf('.') + 1).Length;

                if (!stMode.EntryText.EndsWith("."))
                {
                    if (stMode.EntryText.Contains("."))
                    {
                        formatStr = "#,##0.";
                        for (int i = 0; i < decPlaces; i++)
                            formatStr += "0";
                    }
                    else
                    {
                        formatStr = "#,##0";
                    }
                    stMode.EntryText = Convert.ToDecimal(stMode.EntryText).ToString(formatStr);
                }
            }
        }

        private void CreateHistoryLog(String entry)
        {
            // Create new log
            Button button = new Button();
            
            // Add text
            button.Text = entry;

            // Add style
            this.CreateLogStyle(button);

            // Attach button to a layout panel
            this.flowLayoutPanelHistory.Controls.Add(button);
        }

        private void CreateLogStyle(Button buttonLog)
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

        // Allow scrolling flow layout panel history using mousewheel
        private void flowLayoutPanelHistory_Paint(object sender, PaintEventArgs e)
        {
            this.flowLayoutPanelHistory.Focus();
        }

        private void viewHelpF1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}