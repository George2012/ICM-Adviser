namespace ICM_Adviser
{
    partial class Form1
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
            this.labelPL = new System.Windows.Forms.Label();
            this.labelP = new System.Windows.Forms.Label();
            this.labelM = new System.Windows.Forms.Label();
            this.radioButtonR = new System.Windows.Forms.RadioButton();
            this.radioButtonL = new System.Windows.Forms.RadioButton();
            this.radioButtonF = new System.Windows.Forms.RadioButton();
            this.labelRange = new System.Windows.Forms.Label();
            this.textBoxRange = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.descriptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDescriptionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveDescriptionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.buttonDescription = new System.Windows.Forms.Button();
            this.openDescriptionDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveDescriptionDialog = new System.Windows.Forms.SaveFileDialog();
            this.listBoxPL = new System.Windows.Forms.ListBox();
            this.listBoxP = new System.Windows.Forms.ListBox();
            this.listBoxM = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelPL
            // 
            this.labelPL.AutoSize = true;
            this.labelPL.Location = new System.Drawing.Point(35, 72);
            this.labelPL.Name = "labelPL";
            this.labelPL.Size = new System.Drawing.Size(20, 13);
            this.labelPL.TabIndex = 0;
            this.labelPL.Text = "PL";
            // 
            // labelP
            // 
            this.labelP.AutoSize = true;
            this.labelP.Location = new System.Drawing.Point(118, 72);
            this.labelP.Name = "labelP";
            this.labelP.Size = new System.Drawing.Size(14, 13);
            this.labelP.TabIndex = 1;
            this.labelP.Text = "P";
            // 
            // labelM
            // 
            this.labelM.AutoSize = true;
            this.labelM.Location = new System.Drawing.Point(190, 72);
            this.labelM.Name = "labelM";
            this.labelM.Size = new System.Drawing.Size(16, 13);
            this.labelM.TabIndex = 2;
            this.labelM.Text = "M";
            // 
            // radioButtonR
            // 
            this.radioButtonR.Location = new System.Drawing.Point(182, 32);
            this.radioButtonR.Name = "radioButtonR";
            this.radioButtonR.Size = new System.Drawing.Size(56, 24);
            this.radioButtonR.TabIndex = 18;
            this.radioButtonR.Text = "R";
            this.radioButtonR.CheckedChanged += new System.EventHandler(this.radioButtonR_CheckedChanged);
            // 
            // radioButtonL
            // 
            this.radioButtonL.Location = new System.Drawing.Point(109, 32);
            this.radioButtonL.Name = "radioButtonL";
            this.radioButtonL.Size = new System.Drawing.Size(55, 24);
            this.radioButtonL.TabIndex = 17;
            this.radioButtonL.Text = "L";
            this.radioButtonL.CheckedChanged += new System.EventHandler(this.radioButtonL_CheckedChanged);
            // 
            // radioButtonF
            // 
            this.radioButtonF.Checked = true;
            this.radioButtonF.Location = new System.Drawing.Point(40, 32);
            this.radioButtonF.Name = "radioButtonF";
            this.radioButtonF.Size = new System.Drawing.Size(45, 24);
            this.radioButtonF.TabIndex = 16;
            this.radioButtonF.TabStop = true;
            this.radioButtonF.Text = "F";
            this.radioButtonF.CheckedChanged += new System.EventHandler(this.radioButtonF_CheckedChanged);
            // 
            // labelRange
            // 
            this.labelRange.AutoSize = true;
            this.labelRange.Location = new System.Drawing.Point(246, 100);
            this.labelRange.Name = "labelRange";
            this.labelRange.Size = new System.Drawing.Size(42, 13);
            this.labelRange.TabIndex = 9;
            this.labelRange.Text = "Range:";
            // 
            // textBoxRange
            // 
            this.textBoxRange.Location = new System.Drawing.Point(354, 100);
            this.textBoxRange.Name = "textBoxRange";
            this.textBoxRange.Size = new System.Drawing.Size(83, 20);
            this.textBoxRange.TabIndex = 10;
            this.textBoxRange.Text = "0";
            this.textBoxRange.TextChanged += new System.EventHandler(this.textBoxRange_TextChanged);
            this.textBoxRange.Validated += new System.EventHandler(this.textBoxRange_TextValidated);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(149, 250);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(58, 27);
            this.buttonSave.TabIndex = 12;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(239, 151);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(198, 83);
            this.textBoxDescription.TabIndex = 13;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.modeToolStripMenuItem,
            this.descriptionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(481, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.saveAsToolStripMenuItem.Text = "Save As..";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // modeToolStripMenuItem
            // 
            this.modeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editModeToolStripMenuItem});
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.modeToolStripMenuItem.Text = "Mode";
            // 
            // editModeToolStripMenuItem
            // 
            this.editModeToolStripMenuItem.Checked = true;
            this.editModeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.editModeToolStripMenuItem.Name = "editModeToolStripMenuItem";
            this.editModeToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.editModeToolStripMenuItem.Text = "Edit Mode";
            this.editModeToolStripMenuItem.Click += new System.EventHandler(this.editModeToolStripMenuItem_Click);
            // 
            // descriptionToolStripMenuItem
            // 
            this.descriptionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDescriptionMenuItem,
            this.saveAsToolStripMenuItem1,
            this.saveDescriptionMenuItem});
            this.descriptionToolStripMenuItem.Name = "descriptionToolStripMenuItem";
            this.descriptionToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.descriptionToolStripMenuItem.Text = "Description";
            // 
            // openDescriptionMenuItem
            // 
            this.openDescriptionMenuItem.Name = "openDescriptionMenuItem";
            this.openDescriptionMenuItem.Size = new System.Drawing.Size(125, 22);
            this.openDescriptionMenuItem.Text = "Open";
            this.openDescriptionMenuItem.Click += new System.EventHandler(this.openDescriptionMenuItem_Click);
            // 
            // saveAsToolStripMenuItem1
            // 
            this.saveAsToolStripMenuItem1.Name = "saveAsToolStripMenuItem1";
            this.saveAsToolStripMenuItem1.Size = new System.Drawing.Size(125, 22);
            this.saveAsToolStripMenuItem1.Text = "Save As...";
            this.saveAsToolStripMenuItem1.Click += new System.EventHandler(this.saveAsDescriptionMenuItem_Click);
            // 
            // saveDescriptionMenuItem
            // 
            this.saveDescriptionMenuItem.Name = "saveDescriptionMenuItem";
            this.saveDescriptionMenuItem.Size = new System.Drawing.Size(125, 22);
            this.saveDescriptionMenuItem.Text = "Save";
            this.saveDescriptionMenuItem.Click += new System.EventHandler(this.saveDescriptionMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonDescription
            // 
            this.buttonDescription.Location = new System.Drawing.Point(239, 250);
            this.buttonDescription.Name = "buttonDescription";
            this.buttonDescription.Size = new System.Drawing.Size(198, 27);
            this.buttonDescription.TabIndex = 15;
            this.buttonDescription.Text = "Save Description";
            this.buttonDescription.UseVisualStyleBackColor = true;
            this.buttonDescription.Click += new System.EventHandler(this.buttonDescription_Click);
            // 
            // openDescriptionDialog
            // 
            this.openDescriptionDialog.FileName = "Description.xml";
            // 
            // listBoxPL
            // 
            this.listBoxPL.DisplayMember = "2";
            this.listBoxPL.FormattingEnabled = true;
            this.listBoxPL.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.listBoxPL.Location = new System.Drawing.Point(35, 100);
            this.listBoxPL.Name = "listBoxPL";
            this.listBoxPL.Size = new System.Drawing.Size(20, 134);
            this.listBoxPL.TabIndex = 19;
            this.listBoxPL.ValueMember = "2";
            this.listBoxPL.SelectedIndexChanged += new System.EventHandler(this.listBoxPL_SelectedIndexChanged);
            // 
            // listBoxP
            // 
            this.listBoxP.FormattingEnabled = true;
            this.listBoxP.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.listBoxP.Location = new System.Drawing.Point(105, 100);
            this.listBoxP.Name = "listBoxP";
            this.listBoxP.Size = new System.Drawing.Size(27, 134);
            this.listBoxP.TabIndex = 20;
            this.listBoxP.SelectedIndexChanged += new System.EventHandler(this.listBoxP_SelectedIndexChanged);
            // 
            // listBoxM
            // 
            this.listBoxM.FormattingEnabled = true;
            this.listBoxM.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.listBoxM.Location = new System.Drawing.Point(182, 100);
            this.listBoxM.Name = "listBoxM";
            this.listBoxM.Size = new System.Drawing.Size(25, 134);
            this.listBoxM.TabIndex = 21;
            this.listBoxM.SelectedIndexChanged += new System.EventHandler(this.listBoxM_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 287);
            this.Controls.Add(this.listBoxM);
            this.Controls.Add(this.listBoxP);
            this.Controls.Add(this.listBoxPL);
            this.Controls.Add(this.buttonDescription);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxRange);
            this.Controls.Add(this.labelRange);
            this.Controls.Add(this.radioButtonF);
            this.Controls.Add(this.radioButtonL);
            this.Controls.Add(this.radioButtonR);
            this.Controls.Add(this.labelM);
            this.Controls.Add(this.labelP);
            this.Controls.Add(this.labelPL);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPL;
        private System.Windows.Forms.Label labelP;
        private System.Windows.Forms.Label labelM;
        private System.Windows.Forms.RadioButton radioButtonR;
        private System.Windows.Forms.RadioButton radioButtonL;
        private System.Windows.Forms.RadioButton radioButtonF;
        private System.Windows.Forms.Label labelRange;
        private System.Windows.Forms.TextBox textBoxRange;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editModeToolStripMenuItem;
        private System.Windows.Forms.Button buttonDescription;
        private System.Windows.Forms.ToolStripMenuItem descriptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDescriptionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveDescriptionMenuItem;
        private System.Windows.Forms.OpenFileDialog openDescriptionDialog;
        private System.Windows.Forms.SaveFileDialog saveDescriptionDialog;
        private System.Windows.Forms.ListBox listBoxPL;
        private System.Windows.Forms.ListBox listBoxP;
        private System.Windows.Forms.ListBox listBoxM;
    }
}

