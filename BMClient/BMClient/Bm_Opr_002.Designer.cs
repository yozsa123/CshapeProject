namespace BMClient
{
    partial class Bm_Opr_002
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bm_Opr_002));
            this.acsGridView = new System.Windows.Forms.DataGridView();
            this.ACSID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACSDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bmGridView = new System.Windows.Forms.DataGridView();
            this.BMID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BMDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnl_Control = new System.Windows.Forms.Panel();
            this.btn_Close = new System.Windows.Forms.Button();
            this.searchInfoCombobox = new System.Windows.Forms.ComboBox();
            this.btn_Search = new System.Windows.Forms.Button();
            this.plantSearchComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.acsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bmGridView)).BeginInit();
            this.pnl_Control.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // acsGridView
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.acsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.acsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.acsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ACSID,
            this.ACSDescription});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.acsGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.acsGridView.Location = new System.Drawing.Point(690, 59);
            this.acsGridView.Name = "acsGridView";
            this.acsGridView.RowHeadersVisible = false;
            this.acsGridView.RowTemplate.Height = 23;
            this.acsGridView.Size = new System.Drawing.Size(396, 704);
            this.acsGridView.TabIndex = 112;
            // 
            // ACSID
            // 
            this.ACSID.HeaderText = "ID";
            this.ACSID.Name = "ACSID";
            // 
            // ACSDescription
            // 
            this.ACSDescription.HeaderText = "Description";
            this.ACSDescription.Name = "ACSDescription";
            this.ACSDescription.Width = 150;
            // 
            // bmGridView
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.bmGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.bmGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bmGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BMID,
            this.BMDescription});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.bmGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.bmGridView.Location = new System.Drawing.Point(45, 59);
            this.bmGridView.Name = "bmGridView";
            this.bmGridView.RowHeadersVisible = false;
            this.bmGridView.RowTemplate.Height = 23;
            this.bmGridView.Size = new System.Drawing.Size(396, 704);
            this.bmGridView.TabIndex = 111;
            // 
            // BMID
            // 
            this.BMID.HeaderText = "ID";
            this.BMID.Name = "BMID";
            // 
            // BMDescription
            // 
            this.BMDescription.HeaderText = "Description";
            this.BMDescription.Name = "BMDescription";
            this.BMDescription.Width = 150;
            // 
            // pnl_Control
            // 
            this.pnl_Control.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnl_Control.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Control.Controls.Add(this.btn_Close);
            this.pnl_Control.Controls.Add(this.searchInfoCombobox);
            this.pnl_Control.Controls.Add(this.btn_Search);
            this.pnl_Control.Controls.Add(this.plantSearchComboBox);
            this.pnl_Control.Controls.Add(this.label1);
            this.pnl_Control.Controls.Add(this.label3);
            this.pnl_Control.Location = new System.Drawing.Point(7, 58);
            this.pnl_Control.Name = "pnl_Control";
            this.pnl_Control.Size = new System.Drawing.Size(1250, 55);
            this.pnl_Control.TabIndex = 110;
            // 
            // btn_Close
            // 
            this.btn_Close.Image = ((System.Drawing.Image)(resources.GetObject("btn_Close.Image")));
            this.btn_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Close.Location = new System.Drawing.Point(1156, 8);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(65, 36);
            this.btn_Close.TabIndex = 114;
            this.btn_Close.Text = "    닫  기";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // searchInfoCombobox
            // 
            this.searchInfoCombobox.FormattingEnabled = true;
            this.searchInfoCombobox.Items.AddRange(new object[] {
            "소속",
            "부서",
            "직급",
            "카드유형",
            "카드상태"});
            this.searchInfoCombobox.Location = new System.Drawing.Point(340, 21);
            this.searchInfoCombobox.Name = "searchInfoCombobox";
            this.searchInfoCombobox.Size = new System.Drawing.Size(115, 20);
            this.searchInfoCombobox.TabIndex = 99;
            // 
            // btn_Search
            // 
            this.btn_Search.AutoSize = true;
            this.btn_Search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Search.Image = ((System.Drawing.Image)(resources.GetObject("btn_Search.Image")));
            this.btn_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Search.Location = new System.Drawing.Point(1083, 8);
            this.btn_Search.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(67, 36);
            this.btn_Search.TabIndex = 113;
            this.btn_Search.Text = "  검 색";
            this.btn_Search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // plantSearchComboBox
            // 
            this.plantSearchComboBox.FormattingEnabled = true;
            this.plantSearchComboBox.Location = new System.Drawing.Point(75, 22);
            this.plantSearchComboBox.Name = "plantSearchComboBox";
            this.plantSearchComboBox.Size = new System.Drawing.Size(115, 20);
            this.plantSearchComboBox.TabIndex = 98;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 97;
            this.label1.Text = "발전소 :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(215, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 16);
            this.label3.TabIndex = 66;
            this.label3.Text = "관련 정보 구분 :";
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.acsGridView);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.bmGridView);
            this.panel1.Location = new System.Drawing.Point(7, 128);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1206, 806);
            this.panel1.TabIndex = 113;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(687, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 17);
            this.label4.TabIndex = 102;
            this.label4.Text = "ACS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(42, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 17);
            this.label2.TabIndex = 101;
            this.label2.Text = "BM";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.label9.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.Image = ((System.Drawing.Image)(resources.GetObject("label9.Image")));
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Location = new System.Drawing.Point(2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1256, 55);
            this.label9.TabIndex = 114;
            this.label9.Text = "        관련정보 비교 점검 (Opr_002)";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Bm_Opr_002
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 1024);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.pnl_Control);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Bm_Opr_002";
            this.Text = "Bm_Opr_002";
            ((System.ComponentModel.ISupportInitialize)(this.acsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bmGridView)).EndInit();
            this.pnl_Control.ResumeLayout(false);
            this.pnl_Control.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView acsGridView;
        private System.Windows.Forms.DataGridView bmGridView;
        private System.Windows.Forms.Panel pnl_Control;
        private System.Windows.Forms.ComboBox searchInfoCombobox;
        private System.Windows.Forms.ComboBox plantSearchComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACSID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACSDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn BMID;
        private System.Windows.Forms.DataGridViewTextBoxColumn BMDescription;

    }
}