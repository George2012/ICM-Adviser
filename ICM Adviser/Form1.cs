﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ICM_Adviser
{
    public partial class Form1 : Form
    {
        const int MAX_PL = 9;
        const int MAX_P  = 8;
        const int MAX_M  = 10;

        private string m_filename = "C:\\XMLtest\\test.xml";

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

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dlgValue = saveFileDialog1.ShowDialog();
            if (dlgValue == DialogResult.OK)
            {
                // save
                m_filename = saveFileDialog1.FileName;
                saveXML();
            }
           
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveXML();
        }

        private  void saveXML()
        {
            XmlTextWriter myXmlTextWriter = null;

            myXmlTextWriter = new XmlTextWriter(m_filename, null);
            myXmlTextWriter.Formatting = Formatting.Indented;

            for (int pl = 0; pl < MAX_PL; pl++)
            {
                myXmlTextWriter.WriteStartElement("PL");
                myXmlTextWriter.WriteAttributeString("Value", Convert.ToString(pl));
                for (int p = 0; p < MAX_P; p++)
                {
                    myXmlTextWriter.WriteStartElement("P");
                    myXmlTextWriter.WriteAttributeString("Value", Convert.ToString(p));
                    for (int m = 0; m < MAX_M; m++)
                    {
                        myXmlTextWriter.WriteStartElement("M");
                        myXmlTextWriter.WriteAttributeString("Value", Convert.ToString(m));

                        myXmlTextWriter.WriteElementString("Range", Range[pl, p, m].ToString());

                        myXmlTextWriter.WriteEndElement();
                    }
                    myXmlTextWriter.WriteEndElement();
                }
                myXmlTextWriter.WriteEndElement();
            }

            //Write the XML to file and close the writer
            myXmlTextWriter.Flush();
            myXmlTextWriter.Close();
            if (myXmlTextWriter != null)
                myXmlTextWriter.Close();
        }
    }
}
