using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace ICM_Adviser
{
    public partial class Form1 : Form
    {
        enum Action { F = 0, L = 1, R = 2 }

        const int MAX_PL = 9;
        const int MAX_P  = 8;
        const int MAX_M  = 10;
        const int MAX_ACTION = 2;

        private string m_filename = "C:\\XMLtest\\test.xml";
        private string m_descriptionFilename = "C:\\XMLtest\\myDescription.xml";

        private Decimal[, ,] Range = new Decimal[MAX_PL, MAX_P, MAX_M];

        private Dictionary<Decimal, string> Description = new Dictionary<Decimal, string>();

        private Action action = Action.F;

        private void resetRange()
        {
             for(int pl = 0 ; pl < MAX_PL ; pl++)
                for(int p = 0 ; p < MAX_P ; p++)
                    for (int m = 0; m < MAX_M; m++)   
                    {
                        Range[pl, p, m] = -1;
                    }           
        }

        private void resetDescription()
        {
            foreach (KeyValuePair<Decimal, string> entry in Description)
            {
                Description.Remove(entry.Key);
            }
        }

        public Form1()
        {
            InitializeComponent();
            resetRange();
            resetDescription();

            openDescriptionXML(m_descriptionFilename);
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

            //Delete existing XML before writing 
            //becouse elsewhere file doesn't updated
            File.Delete(m_filename);

            myXmlTextWriter = new XmlTextWriter(m_filename, null);
            myXmlTextWriter.Formatting = Formatting.Indented;

            myXmlTextWriter.WriteStartElement("Ranges");

            for (int pl = 0; pl < MAX_PL; pl++)
            {
                for (int p = 0; p < MAX_P; p++)
                {
                    for (int m = 0; m < MAX_M; m++)
                    {
                        Decimal range = Range[pl, p, m];

                        if (range != -1)
                        {
                            myXmlTextWriter.WriteStartElement("Range");
                            myXmlTextWriter.WriteAttributeString("PL", Convert.ToString(pl+1));
                            myXmlTextWriter.WriteAttributeString("P", Convert.ToString(p+1));
                            myXmlTextWriter.WriteAttributeString("M", Convert.ToString(m+1));
                            myXmlTextWriter.WriteString(Convert.ToString(range));
                            myXmlTextWriter.WriteEndElement();
                        }
                    }
                }
            }


            myXmlTextWriter.WriteEndElement();

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
           XmlTextReader reader = new XmlTextReader(i_filename);

           while (reader.Read())
           {
               switch (reader.NodeType)
               {
                   case XmlNodeType.Element:
                       {
                           if (reader.Name == "Ranges")
                           {
                               break;
                           }

                           int PL = Convert.ToInt32(reader.GetAttribute("PL"));
                           int P = Convert.ToInt32(reader.GetAttribute("P"));
                           int M = Convert.ToInt32(reader.GetAttribute("M"));

                           reader.Read();
                           Decimal range = Convert.ToDecimal(reader.Value);
                           reader.Read();

                           Range[PL - 1, P - 1, M - 1] = range;
                       }
                       break;
               }
           }
        
           reader.Close();
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

            //Remove range if already exist
            if (Description.ContainsKey(Range))
            {
                Description.Remove(Range);
            }

            Description.Add(Range, text);
        }

        private void textBoxRange_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRange_TextValidated(object sender, EventArgs e)
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

        private void saveAsDescriptionMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dlgValue = saveDescriptionDialog.ShowDialog();
            if (dlgValue == DialogResult.OK)
            {
                // save
                m_descriptionFilename = saveDescriptionDialog.FileName;
                saveDescriptionXML();
            }
        }

        private void saveDescriptionXML()
        {
            //EXAMPLE: itarate throught dictionary

            //foreach(KeyValuePair<String,String> entry in MyDic)
            //{
            //    // do something with entry.Value or entry.Key
            //}



            //TODO:ADD exeption handling for XmlTextWriter
            XmlTextWriter myXmlTextWriter = new XmlTextWriter(m_descriptionFilename, null);
            myXmlTextWriter.Formatting = Formatting.Indented;

            myXmlTextWriter.WriteStartElement("Descriptions");

            foreach (KeyValuePair<Decimal, string> entry in Description)
            {
                myXmlTextWriter.WriteStartElement("Range");
                myXmlTextWriter.WriteAttributeString("Value", Convert.ToString(entry.Key));
                myXmlTextWriter.WriteString(entry.Value);
                myXmlTextWriter.WriteEndElement();
            }

            myXmlTextWriter.WriteEndElement();

            //Write the XML to file and close the writer
            myXmlTextWriter.Flush();
            myXmlTextWriter.Close();
            if (myXmlTextWriter != null)
                myXmlTextWriter.Close();

        }

        private void saveDescriptionMenuItem_Click(object sender, EventArgs e)
        {
            saveDescriptionXML();
        }

        private void openDescriptionMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dlgValue = openDescriptionDialog.ShowDialog();
            if (dlgValue == DialogResult.OK)
            {
                // save
                string filename = openDescriptionDialog.FileName;

                openDescriptionXML(filename);
            }
        }

        private void openDescriptionXML(string i_filename)
        {
            resetDescription();

            //TODO:  handle file not found exception
            XmlTextReader reader = new XmlTextReader(i_filename);
           

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        {
                            if (reader.Name == "Descriptions")
                            {
                                break;
                            }

                            Decimal Range = 0;

                            Range = Convert.ToDecimal(reader.GetAttribute("Value"));

                            bool res;

                            res = reader.Read();
                            string text = reader.Value;
                            res = reader.Read();
                           
                            //Remove range if already exist
                            if (Description.ContainsKey(Range))
                            {
                                Description.Remove(Range);
                            }

                            Description.Add(Range, text);
                        }
                        break;
                }
            }

            reader.Close();
        }

        private void radioButtonF_CheckedChanged(object sender, EventArgs e)
        {
            action = Action.F;
        }

        private void radioButtonL_CheckedChanged(object sender, EventArgs e)
        {
            action = Action.L;
        }

        private void radioButtonR_CheckedChanged(object sender, EventArgs e)
        {
            action = Action.R;
        }
    }
}
