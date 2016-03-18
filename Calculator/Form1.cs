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
        private ScientificMode scMode;

        public FormCalculator()
        {
            InitializeComponent();

            scMode = new ScientificMode();
            this.labelCalculatorMode.Text = "STANDARD";
        }

        private void number_button_Click(object sender, EventArgs e)
        {
            // Get the number when the number button is pressed
            Button numBtn = (Button)sender;
            MessageBox.Show(numBtn.Text);
        }

        private void operation_button_Click(object sender, EventArgs e)
        {
            // Get the number when the number button is pressed
            Button operBtn = (Button)sender;
            //MessageBox.Show(operBtn.Text);

            string operation = operBtn.Text;
            switch(operation) 
            {
                case "=":
                    if (scMode.EqualsOperation())
                        this.CreateHistoryLog("15.00000 + 9=\n24.0000");
                    break;
                default:
                    MessageBox.Show("Wrong operation");
                    break;
            }
        }

        private void CreateHistoryLog(String entry)
        {
            // Create new log
            Label button = new Label();
            
            // Add text
            button.Text = entry;

            // Add style
            this.CreateLogStyle(button);

            // Attach button to a layout panel
            this.flowLayoutPanelHistory.Controls.Add(button);
        }

        private void CreateLogStyle(Label buttonLog)
        {
            buttonLog.Width = 245;
            buttonLog.Height = 60;
            buttonLog.Margin = new Padding(0, 0, 0, 0);
            buttonLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonLog.TextAlign = ContentAlignment.MiddleRight;
            buttonLog.BackColor = Color.LightBlue;
            //if (i % 2 == 0)
            //else
            //button.BackColor = Color.LightPink;
            //button.Tag = i;
        }
    }
}