namespace BMClient
{
    partial class Bm_Rpt_001
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bm_Rpt_001));
            this.panel2 = new System.Windows.Forms.Panel();
            this.ReportGridView = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelPage = new System.Windows.Forms.Label();
            this.labelBefore = new System.Windows.Forms.Label();
            this.labelNext = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.radioName = new System.Windows.Forms.RadioButton();
            this.textName = new System.Windows.Forms.TextBox();
            this.radioDivision = new System.Windows.Forms.RadioButton();
            this.radioDepartment = new System.Windows.Forms.RadioButton();
            this.radioSSNO = new System.Windows.Forms.RadioButton();
            this.txtSSNO = new System.Windows.Forms.TextBox();
            this.comboDepartment = new System.Windows.Forms.ComboBox();
            this.comboDivision = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.comboIssueReson = new System.Windows.Forms.ComboBox();
            this.comboIssueType = new System.Windows.Forms.ComboBox();
            this.radioIssueReason = new System.Windows.Forms.RadioButton();
            this.radioIssueType = new System.Windows.Forms.RadioButton();
            this.radioDate = new System.Windows.Forms.RadioButton();
            this.radioCardType = new System.Windows.Forms.RadioButton();
            this.radioCardNum = new System.Windows.Forms.RadioButton();
            this.endDateTime = new System.Windows.Forms.DateTimePicker();
            this.startDateTime = new System.Windows.Forms.DateTimePicker();
            this.txtCardNum = new System.Windows.Forms.TextBox();
            this.comboRegType = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.authRadioButton1 = new System.Windows.Forms.RadioButton();
            this.comboAuth4 = new System.Windows.Forms.ComboBox();
            this.comboAuth3 = new System.Windows.Forms.ComboBox();
            this.comboAuth2 = new System.Windows.Forms.ComboBox();
            this.comboAuth1 = new System.Windows.Forms.ComboBox();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_Excel = new System.Windows.Forms.Button();
            this.btn_Search = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.comboCardStat = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReportGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.Controls.Add(this.ReportGridView);
            this.panel2.Location = new System.Drawing.Point(3, 215);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1256, 783);
            this.panel2.TabIndex = 33;
            // 
            // ReportGridView
            // 
            this.ReportGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ReportGridView.Location = new System.Drawing.Point(3, 3);
            this.ReportGridView.Name = "ReportGridView";
            this.ReportGridView.ReadOnly = true;
            this.ReportGridView.RowHeadersVisible = false;
            this.ReportGridView.RowTemplate.Height = 23;
            this.ReportGridView.Size = new System.Drawing.Size(1240, 759);
            this.ReportGridView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labelPage);
            this.panel1.Controls.Add(this.labelBefore);
            this.panel1.Controls.Add(this.labelNext);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.btn_Close);
            this.panel1.Controls.Add(this.btn_Excel);
            this.panel1.Controls.Add(this.btn_Search);
            this.panel1.Location = new System.Drawing.Point(3, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1249, 149);
            this.panel1.TabIndex = 32;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // labelPage
            // 
            this.labelPage.AutoSize = true;
            this.labelPage.Location = new System.Drawing.Point(1014, 123);
            this.labelPage.Name = "labelPage";
            this.labelPage.Size = new System.Drawing.Size(0, 12);
            this.labelPage.TabIndex = 119;
            this.labelPage.Visible = false;
            // 
            // labelBefore
            // 
            this.labelBefore.AutoSize = true;
            this.labelBefore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelBefore.Location = new System.Drawing.Point(953, 123);
            this.labelBefore.Name = "labelBefore";
            this.labelBefore.Size = new System.Drawing.Size(45, 12);
            this.labelBefore.TabIndex = 118;
            this.labelBefore.Text = "◀ 이전";
            this.labelBefore.Visible = false;
            this.labelBefore.Click += new System.EventHandler(this.label3_Click);
            // 
            // labelNext
            // 
            this.labelNext.AutoSize = true;
            this.labelNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelNext.Location = new System.Drawing.Point(1069, 123);
            this.labelNext.Name = "labelNext";
            this.labelNext.Size = new System.Drawing.Size(45, 12);
            this.labelNext.TabIndex = 117;
            this.labelNext.Text = "다음 ▶";
            this.labelNext.Visible = false;
            this.labelNext.Click += new System.EventHandler(this.label2_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(20, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(926, 107);
            this.tabControl1.TabIndex = 116;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.radioName);
            this.tabPage1.Controls.Add(this.textName);
            this.tabPage1.Controls.Add(this.radioDivision);
            this.tabPage1.Controls.Add(this.radioDepartment);
            this.tabPage1.Controls.Add(this.radioSSNO);
            this.tabPage1.Controls.Add(this.txtSSNO);
            this.tabPage1.Controls.Add(this.comboDepartment);
            this.tabPage1.Controls.Add(this.comboDivision);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(918, 81);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "사람";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // radioName
            // 
            this.radioName.AutoSize = true;
            this.radioName.Checked = true;
            this.radioName.Location = new System.Drawing.Point(14, 32);
            this.radioName.Name = "radioName";
            this.radioName.Size = new System.Drawing.Size(55, 16);
            this.radioName.TabIndex = 85;
            this.radioName.TabStop = true;
            this.radioName.Text = "성명 :";
            this.radioName.UseVisualStyleBackColor = true;
            // 
            // textName
            // 
            this.textName.Location = new System.Drawing.Point(75, 30);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(107, 21);
            this.textName.TabIndex = 86;
            // 
            // radioDivision
            // 
            this.radioDivision.AutoSize = true;
            this.radioDivision.Location = new System.Drawing.Point(663, 33);
            this.radioDivision.Name = "radioDivision";
            this.radioDivision.Size = new System.Drawing.Size(55, 16);
            this.radioDivision.TabIndex = 84;
            this.radioDivision.Text = "부서 :";
            this.radioDivision.UseVisualStyleBackColor = true;
            // 
            // radioDepartment
            // 
            this.radioDepartment.AutoSize = true;
            this.radioDepartment.Location = new System.Drawing.Point(425, 33);
            this.radioDepartment.Name = "radioDepartment";
            this.radioDepartment.Size = new System.Drawing.Size(55, 16);
            this.radioDepartment.TabIndex = 83;
            this.radioDepartment.Text = "소속 :";
            this.radioDepartment.UseVisualStyleBackColor = true;
            // 
            // radioSSNO
            // 
            this.radioSSNO.AutoSize = true;
            this.radioSSNO.Location = new System.Drawing.Point(202, 33);
            this.radioSSNO.Name = "radioSSNO";
            this.radioSSNO.Size = new System.Drawing.Size(55, 16);
            this.radioSSNO.TabIndex = 2;
            this.radioSSNO.Text = "사번 :";
            this.radioSSNO.UseVisualStyleBackColor = true;
            // 
            // txtSSNO
            // 
            this.txtSSNO.Location = new System.Drawing.Point(263, 31);
            this.txtSSNO.Name = "txtSSNO";
            this.txtSSNO.Size = new System.Drawing.Size(107, 21);
            this.txtSSNO.TabIndex = 32;
            // 
            // comboDepartment
            // 
            this.comboDepartment.FormattingEnabled = true;
            this.comboDepartment.Location = new System.Drawing.Point(486, 31);
            this.comboDepartment.Name = "comboDepartment";
            this.comboDepartment.Size = new System.Drawing.Size(107, 20);
            this.comboDepartment.TabIndex = 80;
            // 
            // comboDivision
            // 
            this.comboDivision.FormattingEnabled = true;
            this.comboDivision.Location = new System.Drawing.Point(728, 31);
            this.comboDivision.Name = "comboDivision";
            this.comboDivision.Size = new System.Drawing.Size(107, 20);
            this.comboDivision.TabIndex = 81;
            this.comboDivision.SelectedIndexChanged += new System.EventHandler(this.comboDivision_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.comboIssueReson);
            this.tabPage2.Controls.Add(this.comboIssueType);
            this.tabPage2.Controls.Add(this.radioIssueReason);
            this.tabPage2.Controls.Add(this.radioIssueType);
            this.tabPage2.Controls.Add(this.radioDate);
            this.tabPage2.Controls.Add(this.radioCardType);
            this.tabPage2.Controls.Add(this.radioCardNum);
            this.tabPage2.Controls.Add(this.endDateTime);
            this.tabPage2.Controls.Add(this.startDateTime);
            this.tabPage2.Controls.Add(this.txtCardNum);
            this.tabPage2.Controls.Add(this.comboRegType);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(918, 81);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "카드";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // comboIssueReson
            // 
            this.comboIssueReson.FormattingEnabled = true;
            this.comboIssueReson.Location = new System.Drawing.Point(618, 13);
            this.comboIssueReson.Name = "comboIssueReson";
            this.comboIssueReson.Size = new System.Drawing.Size(109, 20);
            this.comboIssueReson.TabIndex = 104;
            // 
            // comboIssueType
            // 
            this.comboIssueType.FormattingEnabled = true;
            this.comboIssueType.Location = new System.Drawing.Point(380, 13);
            this.comboIssueType.Name = "comboIssueType";
            this.comboIssueType.Size = new System.Drawing.Size(109, 20);
            this.comboIssueType.TabIndex = 103;
            // 
            // radioIssueReason
            // 
            this.radioIssueReason.AutoSize = true;
            this.radioIssueReason.Location = new System.Drawing.Point(533, 14);
            this.radioIssueReason.Name = "radioIssueReason";
            this.radioIssueReason.Size = new System.Drawing.Size(79, 16);
            this.radioIssueReason.TabIndex = 102;
            this.radioIssueReason.TabStop = true;
            this.radioIssueReason.Text = "발급사유 :";
            this.radioIssueReason.UseVisualStyleBackColor = true;
            // 
            // radioIssueType
            // 
            this.radioIssueType.AutoSize = true;
            this.radioIssueType.Location = new System.Drawing.Point(283, 14);
            this.radioIssueType.Name = "radioIssueType";
            this.radioIssueType.Size = new System.Drawing.Size(79, 16);
            this.radioIssueType.TabIndex = 100;
            this.radioIssueType.TabStop = true;
            this.radioIssueType.Text = "발급유형 :";
            this.radioIssueType.UseVisualStyleBackColor = true;
            // 
            // radioDate
            // 
            this.radioDate.AutoSize = true;
            this.radioDate.Location = new System.Drawing.Point(283, 45);
            this.radioDate.Name = "radioDate";
            this.radioDate.Size = new System.Drawing.Size(79, 16);
            this.radioDate.TabIndex = 98;
            this.radioDate.TabStop = true;
            this.radioDate.Text = "유효기간 :";
            this.radioDate.UseVisualStyleBackColor = true;
            // 
            // radioCardType
            // 
            this.radioCardType.AutoSize = true;
            this.radioCardType.Location = new System.Drawing.Point(24, 47);
            this.radioCardType.Name = "radioCardType";
            this.radioCardType.Size = new System.Drawing.Size(79, 16);
            this.radioCardType.TabIndex = 96;
            this.radioCardType.TabStop = true;
            this.radioCardType.Text = "카드타입 :";
            this.radioCardType.UseVisualStyleBackColor = true;
            // 
            // radioCardNum
            // 
            this.radioCardNum.AutoSize = true;
            this.radioCardNum.Location = new System.Drawing.Point(26, 17);
            this.radioCardNum.Name = "radioCardNum";
            this.radioCardNum.Size = new System.Drawing.Size(79, 16);
            this.radioCardNum.TabIndex = 95;
            this.radioCardNum.TabStop = true;
            this.radioCardNum.Text = "카드번호 :";
            this.radioCardNum.UseVisualStyleBackColor = true;
            // 
            // endDateTime
            // 
            this.endDateTime.Location = new System.Drawing.Point(380, 45);
            this.endDateTime.Name = "endDateTime";
            this.endDateTime.Size = new System.Drawing.Size(116, 21);
            this.endDateTime.TabIndex = 94;
            // 
            // startDateTime
            // 
            this.startDateTime.Location = new System.Drawing.Point(776, 40);
            this.startDateTime.Name = "startDateTime";
            this.startDateTime.Size = new System.Drawing.Size(116, 21);
            this.startDateTime.TabIndex = 93;
            this.startDateTime.Visible = false;
            // 
            // txtCardNum
            // 
            this.txtCardNum.Location = new System.Drawing.Point(109, 15);
            this.txtCardNum.Name = "txtCardNum";
            this.txtCardNum.Size = new System.Drawing.Size(111, 21);
            this.txtCardNum.TabIndex = 87;
            // 
            // comboRegType
            // 
            this.comboRegType.FormattingEnabled = true;
            this.comboRegType.Location = new System.Drawing.Point(109, 46);
            this.comboRegType.Name = "comboRegType";
            this.comboRegType.Size = new System.Drawing.Size(109, 20);
            this.comboRegType.TabIndex = 88;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.comboCardStat);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.authRadioButton1);
            this.tabPage3.Controls.Add(this.comboAuth4);
            this.tabPage3.Controls.Add(this.comboAuth3);
            this.tabPage3.Controls.Add(this.comboAuth2);
            this.tabPage3.Controls.Add(this.comboAuth1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(918, 81);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "권한";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(415, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "4발전소";
            this.label4.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(426, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "3발전소";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(222, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "2발전소";
            // 
            // authRadioButton1
            // 
            this.authRadioButton1.AutoSize = true;
            this.authRadioButton1.Location = new System.Drawing.Point(6, 32);
            this.authRadioButton1.Name = "authRadioButton1";
            this.authRadioButton1.Size = new System.Drawing.Size(65, 16);
            this.authRadioButton1.TabIndex = 8;
            this.authRadioButton1.TabStop = true;
            this.authRadioButton1.Text = "1발전소";
            this.authRadioButton1.UseVisualStyleBackColor = true;
            // 
            // comboAuth4
            // 
            this.comboAuth4.FormattingEnabled = true;
            this.comboAuth4.Location = new System.Drawing.Point(490, 54);
            this.comboAuth4.Name = "comboAuth4";
            this.comboAuth4.Size = new System.Drawing.Size(126, 20);
            this.comboAuth4.TabIndex = 3;
            this.comboAuth4.Visible = false;
            // 
            // comboAuth3
            // 
            this.comboAuth3.FormattingEnabled = true;
            this.comboAuth3.Location = new System.Drawing.Point(490, 30);
            this.comboAuth3.Name = "comboAuth3";
            this.comboAuth3.Size = new System.Drawing.Size(126, 20);
            this.comboAuth3.TabIndex = 2;
            // 
            // comboAuth2
            // 
            this.comboAuth2.FormattingEnabled = true;
            this.comboAuth2.Location = new System.Drawing.Point(275, 31);
            this.comboAuth2.Name = "comboAuth2";
            this.comboAuth2.Size = new System.Drawing.Size(126, 20);
            this.comboAuth2.TabIndex = 1;
            // 
            // comboAuth1
            // 
            this.comboAuth1.FormattingEnabled = true;
            this.comboAuth1.Location = new System.Drawing.Point(77, 31);
            this.comboAuth1.Name = "comboAuth1";
            this.comboAuth1.Size = new System.Drawing.Size(126, 20);
            this.comboAuth1.TabIndex = 0;
            // 
            // btn_Close
            // 
            this.btn_Close.Image = ((System.Drawing.Image)(resources.GetObject("btn_Close.Image")));
            this.btn_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Close.Location = new System.Drawing.Point(1170, 55);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(65, 36);
            this.btn_Close.TabIndex = 115;
            this.btn_Close.Text = "    닫  기";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_Excel
            // 
            this.btn_Excel.Image = ((System.Drawing.Image)(resources.GetObject("btn_Excel.Image")));
            this.btn_Excel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Excel.Location = new System.Drawing.Point(1099, 55);
            this.btn_Excel.Name = "btn_Excel";
            this.btn_Excel.Size = new System.Drawing.Size(65, 36);
            this.btn_Excel.TabIndex = 114;
            this.btn_Excel.Text = "   엑  셀";
            this.btn_Excel.UseVisualStyleBackColor = true;
            this.btn_Excel.Click += new System.EventHandler(this.btn_Excel_Click);
            // 
            // btn_Search
            // 
            this.btn_Search.AutoSize = true;
            this.btn_Search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Search.Image = ((System.Drawing.Image)(resources.GetObject("btn_Search.Image")));
            this.btn_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Search.Location = new System.Drawing.Point(1026, 53);
            this.btn_Search.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(67, 38);
            this.btn_Search.TabIndex = 97;
            this.btn_Search.Text = " 검 색";
            this.btn_Search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(219)))), ((int)(((byte)(196)))));
            this.label9.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.Image = ((System.Drawing.Image)(resources.GetObject("label9.Image")));
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Location = new System.Drawing.Point(3, 2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1256, 55);
            this.label9.TabIndex = 101;
            this.label9.Text = "        출입정보 Report (Rpt_001)";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboCardStat
            // 
            this.comboCardStat.FormattingEnabled = true;
            this.comboCardStat.Location = new System.Drawing.Point(710, 31);
            this.comboCardStat.Name = "comboCardStat";
            this.comboCardStat.Size = new System.Drawing.Size(100, 20);
            this.comboCardStat.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(635, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "카드상태 :";
            // 
            // Bm_Rpt_001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 1024);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Bm_Rpt_001";
            this.Text = "Bm_Rpt_001";
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ReportGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboRegType;
        private System.Windows.Forms.TextBox txtCardNum;
        private System.Windows.Forms.ComboBox comboDivision;
        private System.Windows.Forms.ComboBox comboDepartment;
        private System.Windows.Forms.TextBox txtSSNO;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Excel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DateTimePicker endDateTime;
        private System.Windows.Forms.DateTimePicker startDateTime;
        private System.Windows.Forms.RadioButton radioDivision;
        private System.Windows.Forms.RadioButton radioDepartment;
        private System.Windows.Forms.RadioButton radioSSNO;
        private System.Windows.Forms.RadioButton radioDate;
        private System.Windows.Forms.RadioButton radioCardType;
        private System.Windows.Forms.RadioButton radioCardNum;
        private System.Windows.Forms.RadioButton radioIssueReason;
        private System.Windows.Forms.RadioButton radioIssueType;
        private System.Windows.Forms.ComboBox comboIssueReson;
        private System.Windows.Forms.ComboBox comboIssueType;
        private System.Windows.Forms.DataGridView ReportGridView;
        private System.Windows.Forms.Label labelPage;
        private System.Windows.Forms.Label labelBefore;
        private System.Windows.Forms.Label labelNext;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RadioButton authRadioButton1;
        private System.Windows.Forms.ComboBox comboAuth4;
        private System.Windows.Forms.ComboBox comboAuth3;
        private System.Windows.Forms.ComboBox comboAuth2;
        private System.Windows.Forms.ComboBox comboAuth1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioName;
        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboCardStat;
    }
}