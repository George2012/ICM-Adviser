using System;
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

        Dictionary<Decimal, string> Description = new Dictionary<Decimal, string>();


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

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dlgValue = openFileDialog1.ShowDialog();
            if (dlgValue == DialogResult.OK)
            {
                // save
                string filename = openFileDialog1.FileName;
                openXML(filename);
            }
        }

        private void openXML(string i_filename)
        {

        }

        private void editModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.editModeToolStripMenuItem.Checked == true)
            {
                this.editModeToolStripMenuItem.Checked = false;
                this.textBoxRange.ReadOnly       = true;
                this.textBoxDescription.ReadOnly = true;
            }
            else
            {
                this.editModeToolStripMenuItem.Checked = true;
                this.textBoxRange.ReadOnly       = false;
                this.textBoxDescription.ReadOnly = false;
            }
        }

        private void buttonDescription_Click(object sender, EventArgs e)
        {
            Decimal Range = Convert.ToDecimal(this.textBoxRange.Text);
            string  text = textBoxDescription.Text;

            Description.Add(Range, text);
        }

        private void textBoxRange_TextChanged(object sender, EventArgs e)
        {
            Decimal range = Convert.ToDecimal(this.textBoxRange.Text);
            string text;

            if (Description.ContainsKey(range)) // True
            {
                text = Description[range];
            }
            else
            {
                text = "N/A";
            }

            this.textBoxDescription.Text = text;
        }
    }
}
