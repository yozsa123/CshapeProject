namespace BMClient
{
    partial class Bm_Opr_003
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bm_Opr_003));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.bmGridView = new System.Windows.Forms.DataGridView();
            this.BmTableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BmCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.acsGridView = new System.Windows.Forms.DataGridView();
            this.ACSTableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACSCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnl_Control = new System.Windows.Forms.Panel();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_Search = new System.Windows.Forms.Button();
            this.acsTypeInfoCombobox = new System.Windows.Forms.ComboBox();
            this.plantSearchComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bmGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.acsGridView)).BeginInit();
            this.pnl_Control.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.bmGridView);
            this.panel1.Controls.Add(this.acsGridView);
            this.panel1.Location = new System.Drawing.Point(19, 138);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1202, 767);
            this.panel1.TabIndex = 107;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(45, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 17);
            this.label2.TabIndex = 110;
            this.label2.Text = "BM";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(665, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 17);
            this.label4.TabIndex = 111;
            this.label4.Text = "ACS";
            // 
            // bmGridView
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.bmGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.bmGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bmGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BmTableName,
            this.BmCount});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.bmGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.bmGridView.Location = new System.Drawing.Point(48, 52);
            this.bmGridView.Name = "bmGridView";
            this.bmGridView.RowHeadersVisible = false;
            this.bmGridView.RowTemplate.Height = 23;
            this.bmGridView.Size = new System.Drawing.Size(396, 656);
            this.bmGridView.TabIndex = 112;
            // 
            // BmTableName
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.BmTableName.DefaultCellStyle = dataGridViewCellStyle2;
            this.BmTableName.HeaderText = "테이블명";
            this.BmTableName.Name = "BmTableName";
            this.BmTableName.Width = 130;
            // 
            // BmCount
            // 
            this.BmCount.HeaderText = "건수";
            this.BmCount.Name = "BmCount";
            // 
            // acsGridView
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.acsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.acsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.acsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ACSTableName,
            this.ACSCount});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.acsGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.acsGridView.Location = new System.Drawing.Point(668, 52);
            this.acsGridView.Name = "acsGridView";
            this.acsGridView.RowHeadersVisible = false;
            this.acsGridView.RowTemplate.Height = 23;
            this.acsGridView.Size = new System.Drawing.Size(396, 656);
            this.acsGridView.TabIndex = 113;
            // 
            // ACSTableName
            // 
            this.ACSTableName.HeaderText = "테이블명";
            this.ACSTableName.Name = "ACSTableName";
            // 
            // ACSCount
            // 
            this.ACSCount.HeaderText = "건수";
            this.ACSCount.Name = "ACSCount";
            // 
            // pnl_Control
            // 
            this.pnl_Control.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_Control.BackgroundImage")));
            this.pnl_Control.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnl_Control.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Control.Controls.Add(this.btn_Close);
            this.pnl_Control.Controls.Add(this.btn_Search);
            this.pnl_Control.Controls.Add(this.acsTypeInfoCombobox);
            this.pnl_Control.Controls.Add(this.plantSearchComboBox);
            this.pnl_Control.Controls.Add(this.label1);
            this.pnl_Control.Controls.Add(this.label3);
            this.pnl_Control.Location = new System.Drawing.Point(4, 61);
            this.pnl_Control.Name = "pnl_Control";
            this.pnl_Control.Size = new System.Drawing.Size(1248, 55);
            this.pnl_Control.TabIndex = 106;
            // 
            // btn_Close
            // 
            this.btn_Close.Image = ((System.Drawing.Image)(resources.GetObject("btn_Close.Image")));
            this.btn_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Close.Location = new System.Drawing.Point(1148, 9);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(65, 36);
            this.btn_Close.TabIndex = 114;
            this.btn_Close.Text = "    닫  기";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_Search
            // 
            this.btn_Search.AutoSize = true;
            this.btn_Search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Search.Image = ((System.Drawing.Image)(resources.GetObject("btn_Search.Image")));
            this.btn_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Search.Location = new System.Drawing.Point(1047, 10);
            this.btn_Search.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(67, 36);
            this.btn_Search.TabIndex = 113;
            this.btn_Search.Text = "  검 색";
            this.btn_Search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // acsTypeInfoCombobox
            // 
            this.acsTypeInfoCombobox.FormattingEnabled = true;
            this.acsTypeInfoCombobox.Items.AddRange(new object[] {
            "카드정보",
            "권한정보"});
            this.acsTypeInfoCombobox.Location = new System.Drawing.Point(334, 23);
            this.acsTypeInfoCombobox.Name = "acsTypeInfoCombobox";
            this.acsTypeInfoCombobox.Size = new System.Drawing.Size(115, 20);
            this.acsTypeInfoCombobox.TabIndex = 99;
            // 
            // plantSearchComboBox
            // 
            this.plantSearchComboBox.FormattingEnabled = true;
            this.plantSearchComboBox.Location = new System.Drawing.Point(76, 23);
            this.plantSearchComboBox.Name = "plantSearchComboBox";
            this.plantSearchComboBox.Size = new System.Drawing.Size(115, 20);
            this.plantSearchComboBox.TabIndex = 98;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 97;
            this.label1.Text = "발전소 :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(209, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 16);
            this.label3.TabIndex = 66;
            this.label3.Text = "출입 정보 구분 :";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.label9.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.Image = ((System.Drawing.Image)(resources.GetObject("label9.Image")));
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Location = new System.Drawing.Point(3, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1256, 55);
            this.label9.TabIndex = 100;
            this.label9.Text = "        출입정보 비교 점검 (Opr_003)";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Bm_Opr_003
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 1024);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnl_Control);
            this.Controls.Add(this.label9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Bm_Opr_003";
            this.Text = "Bm_Opr_003";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bmGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.acsGridView)).EndInit();
            this.pnl_Control.ResumeLayout(false);
            this.pnl_Control.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel pnl_Control;
        private System.Windows.Forms.ComboBox acsTypeInfoCombobox;
        private System.Windows.Forms.ComboBox plantSearchComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView bmGridView;
        private System.Windows.Forms.DataGridView acsGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn BmTableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BmCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACSTableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACSCount;
    }
}