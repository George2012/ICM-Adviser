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
        enum Action : int { F = 0, L = 1, R = 2 }

        //2160 permutations
        const int MAX_PL = 9  + 1;     // 2 to 9
        const int MAX_P  = 8  + 1; // 0 to 8
        const int MAX_M  = 10 + 1;    // 1 to 10 
        const int MAX_ACTION = 2;

        private string m_filename = "C:\\XMLtest\\Ranges.xml";
        private string m_descriptionFilename = "C:\\XMLtest\\Descriptions.xml";
     
        private Decimal[, , ,] Range = new Decimal[MAX_PL, MAX_P, MAX_M, MAX_ACTION];

        private Dictionary<Decimal, string> Description = new Dictionary<Decimal, string>();

        private Action action = Action.F;

        private bool m_isInitialized = false;

        private void resetRange()
        {
             for(int pl = 0 ; pl < MAX_PL ; pl++)
                for(int p = 0 ; p < MAX_P ; p++)
                    for (int m = 0; m < MAX_M; m++)  
                        for(int a = Convert.ToInt32(Action.F); a < Convert.ToInt32(Action.R) ; a++)
                        {
                            Range[pl, p, m , a] = -1;
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

            openXML(m_filename);
            openDescriptionXML(m_descriptionFilename);

            listBoxPL.SelectedIndex = 0;
            listBoxP.SelectedIndex  = 0;
            listBoxM.SelectedIndex  = 0;

            m_isInitialized = true;
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            updateData();
        }

        private void updateData()
        {
            int PL = Convert.ToInt32(listBoxPL.SelectedItem);
            int P = Convert.ToInt32(listBoxP.SelectedItem);
            int M = Convert.ToInt32(listBoxM.SelectedItem);

            int A = Convert.ToInt32(action);

            decimal res = Range[PL , P , M , A];
            this.textBoxRange.Text = res.ToString();

            UpdateDescription();
        }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            //int PL = Convert.ToInt32(this.numericUpDownPL.Value);
            //int P = Convert.ToInt32(this.numericUpDownP.Value);
            //int M = Convert.ToInt32(this.numericUpDownM.Value);


            int PL = Convert.ToInt32(listBoxPL.SelectedItem);
            int P  = Convert.ToInt32(listBoxP.SelectedItem);
            int M  = Convert.ToInt32(listBoxM.SelectedItem);

            int A = Convert.ToInt32(action);

            decimal res = Convert.ToDecimal(this.textBoxRange.Text);

            Range[PL , P , M  , A ] = res;
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
                        for (int a = Convert.ToInt32(Action.F); a < Convert.ToInt32(Action.R); a++)
                        {
                            Decimal range = Range[pl, p, m , a];

                            if (range != -1)
                            {
                                myXmlTextWriter.WriteStartElement("Range");
                                myXmlTextWriter.WriteAttributeString("PL", Convert.ToString(pl + 1));
                                myXmlTextWriter.WriteAttributeString("P", Convert.ToString(p + 1));
                                myXmlTextWriter.WriteAttributeString("M", Convert.ToString(m + 1));

                                string A ="";

                                switch (a)
                                {
                                    case 0:
                                        A = "F";
                                        break;
                                    case 1:
                                        A = "L";
                                        break;
                                    case 2:
                                        A = "R";
                                        break;
                                }

                                myXmlTextWriter.WriteAttributeString("Action", A);
                                myXmlTextWriter.WriteString(Convert.ToString(range));
                                myXmlTextWriter.WriteEndElement();
                            }
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
                           string A_str = reader.GetAttribute("Action");

                           int A = 0;

                           switch (A_str)
                           {
                               case "F":
                                   A = 0;
                                   break;
                               case "L":
                                   A = 1;
                                   break;
                               case "R":
                                   A = 2;
                                   break;
                           }

                           reader.Read();
                           Decimal range = Convert.ToDecimal(reader.Value);
                           reader.Read();

                           Range[PL , P , M  , A] = range;
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
                this.buttonSave.Visible = false;
                this.buttonDescription.Visible = false;
            }
            else
            {
                this.editModeToolStripMenuItem.Checked = true;
                this.textBoxRange.ReadOnly       = false;
                this.textBoxDescription.ReadOnly = false;
                this.buttonSave.Visible = true;
                this.buttonDescription.Visible = true;
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
            UpdateDescription();
        }

        private void UpdateDescription()
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

            if (action == Action.F)
            {
                updateData();
            }
        }

        private void radioButtonL_CheckedChanged(object sender, EventArgs e)
        {
            action = Action.L;

            if (action == Action.L)
            {
                updateData();
            }
        }

        private void radioButtonR_CheckedChanged(object sender, EventArgs e)
        {
            if (action == Action.R)
            {
                updateData();
            }
        }

        private void listBoxPL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(m_isInitialized == true)
            {
                 updateData();
            }
        }

        private void listBoxP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(m_isInitialized == true)
            {
                 updateData();
            }
        }

        private void listBoxM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(m_isInitialized == true)
            {
                 updateData();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
