namespace BMClient
{
    partial class Bm_Opr_001
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bm_Opr_001));
            this.DownloadGridView = new System.Windows.Forms.DataGridView();
            this.RequestDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Plant1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Plant2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plant3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Plant4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_Search = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DownloadGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DownloadGridView
            // 
            this.DownloadGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DownloadGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RequestDate,
            this.CardNum,
            this.Name,
            this.Plant1,
            this.Plant2,
            this.plant3,
            this.Plant4});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DownloadGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.DownloadGridView.Location = new System.Drawing.Point(2, 133);
            this.DownloadGridView.Name = "DownloadGridView";
            this.DownloadGridView.RowHeadersVisible = false;
            this.DownloadGridView.RowTemplate.Height = 23;
            this.DownloadGridView.Size = new System.Drawing.Size(1256, 823);
            this.DownloadGridView.TabIndex = 79;
            // 
            // RequestDate
            // 
            this.RequestDate.HeaderText = "요청일시";
            this.RequestDate.Name = "RequestDate";
            this.RequestDate.Width = 150;
            // 
            // CardNum
            // 
            this.CardNum.HeaderText = "카드번호";
            this.CardNum.Name = "CardNum";
            this.CardNum.Width = 150;
            // 
            // Name
            // 
            this.Name.HeaderText = "성명";
            this.Name.Name = "Name";
            // 
            // Plant1
            // 
            this.Plant1.HeaderText = "1발전소";
            this.Plant1.Name = "Plant1";
            // 
            // Plant2
            // 
            this.Plant2.HeaderText = "2발전소";
            this.Plant2.Name = "Plant2";
            // 
            // plant3
            // 
            this.plant3.HeaderText = "3발전소";
            this.plant3.Name = "plant3";
            // 
            // Plant4
            // 
            this.Plant4.HeaderText = "4발전소";
            this.Plant4.Name = "Plant4";
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_Close);
            this.panel1.Controls.Add(this.btn_Search);
            this.panel1.Location = new System.Drawing.Point(2, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1256, 55);
            this.panel1.TabIndex = 81;
            // 
            // btn_Close
            // 
            this.btn_Close.Image = ((System.Drawing.Image)(resources.GetObject("btn_Close.Image")));
            this.btn_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Close.Location = new System.Drawing.Point(1123, 11);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(65, 36);
            this.btn_Close.TabIndex = 112;
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
            this.btn_Search.Location = new System.Drawing.Point(1041, 11);
            this.btn_Search.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(67, 36);
            this.btn_Search.TabIndex = 36;
            this.btn_Search.Text = " 검 색";
            this.btn_Search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_Search.UseVisualStyleBackColor = true;
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
            this.label9.TabIndex = 80;
            this.label9.Text = "        출입증 다운로드 점검 (Opr_001)";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Bm_Opr_001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 1024);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.DownloadGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
  
            this.Text = "Bm_Opr_001";
            ((System.ComponentModel.ISupportInitialize)(this.DownloadGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DownloadGridView;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequestDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Plant1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Plant2;
        private System.Windows.Forms.DataGridViewTextBoxColumn plant3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Plant4;
    }
}