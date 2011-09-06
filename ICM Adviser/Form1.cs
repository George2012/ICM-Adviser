using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ICM_Adviser
{
    public partial class Form1 : Form
    {
        const int MAX_PL = 9;
        const int MAX_P  = 8;
        const int MAX_M  = 10;

        private Decimal[, ,] Range = new Decimal[MAX_PL, MAX_P, MAX_M];

        private void resetRange()
        {
             for(int pl = 0 ; pl < MAX_PL ; pl++)
                for(int p = 0 ; p < MAX_P ; p++)
                    for (int m = 0; m < MAX_M; m++)   
                    {
                        Range[pl, p, m] = -1;
                    }           
        }

        public Form1()
        {
            InitializeComponent();
            resetRange();
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            int PL = Convert.ToInt32(this.numericUpDownPL.Value);
            int P = Convert.ToInt32(this.numericUpDownP.Value);
            int M = Convert.ToInt32(this.numericUpDownM.Value);

            decimal res = Range[PL - 1, P - 1, M - 1];
            this.textBoxRange.Text = res.ToString();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            int PL = Convert.ToInt32(this.numericUpDownPL.Value);
            int P = Convert.ToInt32(this.numericUpDownP.Value);
            int M = Convert.ToInt32(this.numericUpDownM.Value);

            decimal res = Convert.ToDecimal(this.textBoxRange.Text);

            Range[PL - 1, P - 1, M - 1] = res;
        }
    }
}
