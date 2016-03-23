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
            this.entryText = this.textBoxEntry.Text;

            // Allow one decimal point
            if (numBtn.Text.Equals("."))
            {
                if(!this.entryText.Contains("."))
                    this.entryText = this.entryText + numBtn.Text;
            }
            else
            {
                this.entryText = this.entryText + numBtn.Text;
            }
           
            // Allow the number start with a single zero
            // Do not allow "00..."
            if (this.entryText.StartsWith("00"))
            {
                this.entryText = "0";
            }

            // Allow leading "0.", and trim all other leading zero combination
            if (this.entryText.Length > 1)
            {
                if (this.entryText.StartsWith("0."))
                {
                    if (this.entryText.Length == 2)
                        this.entryText = "0.";
                }
                else 
                {
                    this.entryText = this.entryText.TrimStart('0');
                }
            }

            // Always keep zero positive
            if (this.entryText.Equals("-0"))
            {
                this.entryText = "0";
            }

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
            if (this.entryText != null)
            {
                switch (operation)
                {
                    case "±":

                        // Do now allow "-0"
                        if (!this.entryText.Equals("0"))
                        {
                            // If entry text contains "-", remove "-" from entry text
                            // If entry text does not contain "-", add "-" to entry text
                            if (this.entryText.Contains("-"))
                            {
                                this.entryText = this.entryText.Substring(1);
                            }
                            else
                            {
                                this.entryText = "-" + this.entryText;
                            }
                        }
                        break;
                    case "=":
                        if (stMode.EqualsOperation())
                            this.CreateHistoryLog("15.00000 + 9=\n24.0000");
                        break;
                    case "<-":

                        if (!this.entryText.Equals("0"))
                        {
                            if (this.entryText.Length <= 1 || (this.entryText.Length == 2 && this.entryText.Contains("-")))
                            {
                                this.entryText = "0";
                            }
                            else
                            {
                                this.entryText = this.entryText.Remove((entryText.Length - 1), 1);

                                if (this.entryText.Length == 0)
                                    this.entryText = "0";
                            }
                        }
                        break;
                    case "CE":
                        this.entryText = "0";
                        break;
                    default:
                        MessageBox.Show("Wrong operation");
                        break;
                }
            }
            else
            {
                this.entryText = "0";
            }

            this.refreshEntryText();
        }

        private void refreshEntryText()
        {
            if(!this.entryText.EndsWith(".") || !this.entryText.Equals("0"))
                this.formatText();
            this.textBoxEntry.Text = this.entryText;
        }

        private void formatText()
        {
            if(this.entryText != null)
            {
                String formatStr;
                int decPlaces = this.entryText.Substring(this.entryText.LastIndexOf('.') + 1).Length;

                if (!this.entryText.EndsWith("."))
                {
                    if (this.entryText.Contains("."))
                    {
                        formatStr = "#,##0.";
                        for (int i = 0; i < decPlaces; i++)
                            formatStr += "0";
                    }
                    else
                    {
                        formatStr = "#,##0";
                    }
                    this.entryText = Convert.ToDecimal(this.entryText).ToString(formatStr);
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
            buttonLog.Width = 245;
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
    }
}