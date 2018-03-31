namespace codvar
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
            this.label3 = new System.Windows.Forms.Label();
            this.gameBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dvarDataGrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ADDRESS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALUE1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALUE2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALUE3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALUE4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusLabel = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dvarDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(174, 512);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Game:";
            // 
            // gameBox
            // 
            this.gameBox.FormattingEnabled = true;
            this.gameBox.Items.AddRange(new object[] {
            "iw3sp",
            "iw3mp",
            "CoDWaW",
            "CoDWaWmp",
            "iw4sp",
            "iw4mp",
            "BlackOps",
            "BlackOpsMP",
            "iw5sp",
            "iw5mp"});
            this.gameBox.Location = new System.Drawing.Point(220, 509);
            this.gameBox.Name = "gameBox";
            this.gameBox.Size = new System.Drawing.Size(121, 21);
            this.gameBox.TabIndex = 11;
            this.gameBox.Text = "iw3sp";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(662, 512);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Made by Corey Nelson";
            // 
            // dvarDataGrid
            // 
            this.dvarDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvarDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.ADDRESS,
            this.TYPE,
            this.VALUE1,
            this.VALUE2,
            this.VALUE3,
            this.VALUE4});
            this.dvarDataGrid.Location = new System.Drawing.Point(12, 1);
            this.dvarDataGrid.Name = "dvarDataGrid";
            this.dvarDataGrid.Size = new System.Drawing.Size(763, 493);
            this.dvarDataGrid.TabIndex = 9;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "DVAR";
            this.Column1.Name = "Column1";
            // 
            // ADDRESS
            // 
            this.ADDRESS.HeaderText = "ADDRESS";
            this.ADDRESS.Name = "ADDRESS";
            // 
            // TYPE
            // 
            this.TYPE.HeaderText = "TYPE";
            this.TYPE.Name = "TYPE";
            // 
            // VALUE1
            // 
            this.VALUE1.HeaderText = "VALUE";
            this.VALUE1.Name = "VALUE1";
            // 
            // VALUE2
            // 
            this.VALUE2.HeaderText = "VALUE2";
            this.VALUE2.Name = "VALUE2";
            // 
            // VALUE3
            // 
            this.VALUE3.HeaderText = "VALUE3";
            this.VALUE3.Name = "VALUE3";
            // 
            // VALUE4
            // 
            this.VALUE4.HeaderText = "VALUE4";
            this.VALUE4.Name = "VALUE4";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(32, 512);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(66, 13);
            this.statusLabel.TabIndex = 8;
            this.statusLabel.Text = "Status: 0 / 0";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(359, 500);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(132, 36);
            this.button2.TabIndex = 7;
            this.button2.Text = "Dump All Dvars";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 539);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gameBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dvarDataGrid);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.button2);
            this.Name = "Form1";
            this.Text = "Call of Duty DVAR Dumper";
            ((System.ComponentModel.ISupportInitialize)(this.dvarDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox gameBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dvarDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ADDRESS;
        private System.Windows.Forms.DataGridViewTextBoxColumn TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALUE1;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALUE2;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALUE3;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALUE4;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button button2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

