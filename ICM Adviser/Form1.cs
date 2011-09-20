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
using System.Security.Cryptography;

namespace ICM_Adviser
{
    public partial class Form1 : Form
    {
        enum Action : int { F = 0, L = 1, R = 2 }

        //2160 permutations
        const int MAX_PL = 9 + 1;     // 2 to 9
        const int MAX_P = 8 + 1; // 0 to 8
        const int MAX_M = 15 + 1;    // 1 to 10 
        const int MAX_ACTION = 2 + 1;

        private string m_ICM_Filename;

        private string m_ChipEV_Filename;
        private string m_descriptionFilename;

        private string m_save_file = "";

        private string encryptionKey =   @"George81";

        private Decimal[, , ,] Range = new Decimal[MAX_PL, MAX_P, MAX_M, MAX_ACTION];

        private Dictionary<Decimal, string> Description = new Dictionary<Decimal, string>();

        private Action action = Action.F;

        private bool m_isInitialized = false;

        private void resetRange()
        {
            for (int pl = 0; pl < MAX_PL; pl++)
                for (int p = 0; p < MAX_P; p++)
                    for (int m = 0; m < MAX_M; m++)
                        for (int a = Convert.ToInt32(Action.F); a < Convert.ToInt32(Action.R); a++)
                        {
                            Range[pl, p, m, a] = -1;
                        }
        }

        private void resetDescription()
        {
            Description.Clear();
        }

        public Form1()
        {
            InitializeComponent();

            ReadConfiguration();

            //switchToICMMode();
            switchToChipEVMode();

            changeEditMode(false);

            openDescriptionXML(m_descriptionFilename);

            listBoxPL.SelectedIndex = 0;
            listBoxP.SelectedIndex = 0;
            listBoxM.SelectedIndex = 0;

            m_isInitialized = true;

            updateData();
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

            decimal res = Range[PL, P, M, A];
            this.textBoxRange.Text = res.ToString();

            UpdateDescription();
        }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveRange();
        }

        private void SaveRange()
        {
            int PL = Convert.ToInt32(listBoxPL.SelectedItem);
            int P = Convert.ToInt32(listBoxP.SelectedItem);
            int M = Convert.ToInt32(listBoxM.SelectedItem);

            int A = Convert.ToInt32(action);

            decimal res = Convert.ToDecimal(this.textBoxRange.Text);

            Range[PL, P, M, A] = res;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dlgValue = saveFileDialog1.ShowDialog();
            if (dlgValue == DialogResult.OK)
            {
                // save
                m_save_file = saveFileDialog1.FileName;
                saveXML();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveXML();
        }

        private void saveXML()
        {
            XmlTextWriter myXmlTextWriter = null;

            //Delete existing XML before writing 
            //becouse elsewhere file doesn't updated
            File.Delete(m_save_file);

            myXmlTextWriter = new XmlTextWriter(m_save_file, null);
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
                            Decimal range = Range[pl, p, m, a];

                            if (range != -1)
                            {
                                myXmlTextWriter.WriteStartElement("Range");
                                myXmlTextWriter.WriteAttributeString("PL", Convert.ToString(pl));
                                myXmlTextWriter.WriteAttributeString("P", Convert.ToString(p));
                                myXmlTextWriter.WriteAttributeString("M", Convert.ToString(m));

                                string A = "";

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


            EncryptFile(m_save_file, "E" + m_save_file);
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
            resetRange();

            XmlTextReader reader;

            if (File.Exists(i_filename))
            {
                reader = new XmlTextReader(i_filename);
            }
            else
            {
                //Try to read Encrypted File
                if(File.Exists("E" + i_filename))
                {
                    DecryptFile("E" + i_filename, "D" + i_filename);
                    reader = new XmlTextReader("D" + i_filename);
                }
                else
                {
                   
                    MessageBox.Show("Failed to read ranges file!", "error");
                    return;
                }
            }
      
            //TODO:
            //handle XML in wrong format

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

                            //TODO handle invalid index exception
                            Range[PL, P, M, A] = range;
                        }
                        break;
                }
            }

            reader.Close();

            if (File.Exists("D" + i_filename))
            {
                File.Delete("D" + i_filename);
            }
        }

        private void editModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeEditMode(!this.editModeToolStripMenuItem.Checked);
        }

        private void changeEditMode(bool i_state)
        {
            if (i_state == true)
            {
                this.editModeToolStripMenuItem.Checked = true;
                this.textBoxRange.ReadOnly = false;
                this.textBoxDescription.ReadOnly = false;
                this.buttonSave.Visible = true;
                this.buttonDescription.Visible = true;
            }
            else
            {
                this.editModeToolStripMenuItem.Checked = false;
                this.textBoxRange.ReadOnly = true;
                this.textBoxDescription.ReadOnly = true;
                this.buttonSave.Visible = false;
                this.buttonDescription.Visible = false;
            }

            //  this.textBoxDescription.ForeColor = Color.White;
        }

        private void buttonDescription_Click(object sender, EventArgs e)
        {
            save_description();
        }

        private void save_description()
        {
            Decimal Range = Convert.ToDecimal(this.textBoxRange.Text);
            string text = textBoxDescription.Text;

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
            SaveRange();
            UpdateDescription();
        }

        private void UpdateDescription()
        {
            if (disableDecsriptionUpdateToolStripMenuItem.Checked == true)
            {
                return;
            }

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

            XmlTextReader reader;
            try
            {
                reader = new XmlTextReader(i_filename);
            }
            catch
            {
                MessageBox.Show("Failed to read Description File!", "Error");
                return;
            }

            //TODO:
            // handle XML in wrong format
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
            action = Action.R;

            if (action == Action.R)
            {
                updateData();
            }
        }

        private void listBoxPL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_isInitialized == true)
            {
                updateData();
            }
        }

        private void listBoxP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_isInitialized == true)
            {
                updateData();
            }
        }

        private void listBoxM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_isInitialized == true)
            {
                updateData();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void chipEVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switchToChipEVMode();
        }

        private void switchToChipEVMode()
        {
            //TODO:  think of somethin better then 7
            this.listBoxPL.SelectedIndex = 7;
            this.listBoxPL.Visible = false;
            this.labelPL.Visible = false;
            this.chipEVToolStripMenuItem.Checked = true;
            this.iCMToolStripMenuItem.Checked = false;

            openXML(m_ChipEV_Filename);

            m_save_file = m_ChipEV_Filename;

            updateData();
        }

        private void iCMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switchToICMMode();
        }

        private void switchToICMMode()
        {
            this.listBoxPL.SelectedIndex = 0;
            this.listBoxPL.Visible = true;
            this.labelPL.Visible = true;
            this.chipEVToolStripMenuItem.Checked = false;
            this.iCMToolStripMenuItem.Checked = true;

            openXML(m_ICM_Filename);

            m_save_file = m_ICM_Filename;

            updateData();
        }

        private void textBoxDescription_Validated(object sender, EventArgs e)
        {
            save_description();
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetRange();
            resetDescription();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ReadConfiguration()
        {
            XmlDocument xml = new XmlDocument();

            try
            {
                xml.Load("Configuration.xml");
            }
            catch
            {
                MessageBox.Show("Failed to read configuration", "Error");
                return;
            }
            XmlNodeList xnList = xml.SelectNodes("/CONF");

            foreach (XmlNode xn in xnList)
            {
                m_ICM_Filename = xn["ICM"].InnerText;
                m_ChipEV_Filename = xn["CHIPEV"].InnerText;
                m_descriptionFilename = xn["DESC"].InnerText;
            }
        }

        ///<summary>
        /// Steve Lydford - 12/05/2008.
        ///
        /// Encrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        private void EncryptFile(string inputFile, string outputFile)
        {
            try
            {
                string password = encryptionKey; // Your Key Here
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write);

                FileStream fsIn = new FileStream(inputFile, FileMode.Open);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);


                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
            }
            catch
            {
                MessageBox.Show("Encryption failed!", "Error");
            }
        }


        ///<summary>
        /// Steve Lydford - 12/05/2008.
        ///
        /// Decrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        private void DecryptFile(string inputFile, string outputFile)
        {

            {
                string password = encryptionKey; // Your Key Here

                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);

                FileStream fsOut = new FileStream(outputFile, FileMode.Create);

                int data;
                while ((data = cs.ReadByte()) != -1)
                    fsOut.WriteByte((byte)data);

                fsOut.Close();
                cs.Close();
                fsCrypt.Close();
            }
        }

        private void disableDecsriptionUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(disableDecsriptionUpdateToolStripMenuItem.Checked == true)
            {
                disableDecsriptionUpdateToolStripMenuItem.Checked = false;
            }
            else
            {
                disableDecsriptionUpdateToolStripMenuItem.Checked = true;
            }
        }
    }
}
