namespace BMClient
{
    partial class PlantRegForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlantRegForm));
            this.cardGroupBox = new System.Windows.Forms.GroupBox();
            this.fp2RegButton = new System.Windows.Forms.Button();
            this.fp3RegButton = new System.Windows.Forms.Button();
            this.fp4RegButton = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.fp1RegButton = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.cardNum = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.topLabel = new System.Windows.Forms.Label();
            this.cardGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // cardGroupBox
            // 
            this.cardGroupBox.Controls.Add(this.fp2RegButton);
            this.cardGroupBox.Controls.Add(this.fp3RegButton);
            this.cardGroupBox.Controls.Add(this.fp4RegButton);
            this.cardGroupBox.Controls.Add(this.button6);
            this.cardGroupBox.Controls.Add(this.fp1RegButton);
            this.cardGroupBox.Controls.Add(this.button4);
            this.cardGroupBox.Controls.Add(this.button3);
            this.cardGroupBox.Controls.Add(this.button2);
            this.cardGroupBox.Controls.Add(this.button1);
            this.cardGroupBox.Controls.Add(this.label10);
            this.cardGroupBox.Controls.Add(this.cardNum);
            this.cardGroupBox.Controls.Add(this.label21);
            this.cardGroupBox.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold);
            this.cardGroupBox.Location = new System.Drawing.Point(12, 84);
            this.cardGroupBox.Name = "cardGroupBox";
            this.cardGroupBox.Size = new System.Drawing.Size(620, 264);
            this.cardGroupBox.TabIndex = 64;
            this.cardGroupBox.TabStop = false;
            this.cardGroupBox.Text = "카드 정보";
            // 
            // fp2RegButton
            // 
            this.fp2RegButton.Location = new System.Drawing.Point(370, 127);
            this.fp2RegButton.Name = "fp2RegButton";
            this.fp2RegButton.Size = new System.Drawing.Size(97, 37);
            this.fp2RegButton.TabIndex = 96;
            this.fp2RegButton.Text = " 지문서버 2등록";
            this.fp2RegButton.UseVisualStyleBackColor = true;
            this.fp2RegButton.Click += new System.EventHandler(this.fp2RegButton_Click);
            // 
            // fp3RegButton
            // 
            this.fp3RegButton.Location = new System.Drawing.Point(370, 185);
            this.fp3RegButton.Name = "fp3RegButton";
            this.fp3RegButton.Size = new System.Drawing.Size(97, 37);
            this.fp3RegButton.TabIndex = 95;
            this.fp3RegButton.Text = " 지문서버 3등록";
            this.fp3RegButton.UseVisualStyleBackColor = true;
            this.fp3RegButton.Click += new System.EventHandler(this.fp3RegButton_Click);
            // 
            // fp4RegButton
            // 
            this.fp4RegButton.Location = new System.Drawing.Point(144, 207);
            this.fp4RegButton.Name = "fp4RegButton";
            this.fp4RegButton.Size = new System.Drawing.Size(97, 37);
            this.fp4RegButton.TabIndex = 94;
            this.fp4RegButton.Text = " 지문서버 4등록";
            this.fp4RegButton.UseVisualStyleBackColor = true;
            this.fp4RegButton.Visible = false;
            this.fp4RegButton.Click += new System.EventHandler(this.fp4RegButton_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(496, 127);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(85, 37);
            this.button6.TabIndex = 93;
            this.button6.Text = "VM 등록";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // fp1RegButton
            // 
            this.fp1RegButton.Location = new System.Drawing.Point(370, 73);
            this.fp1RegButton.Name = "fp1RegButton";
            this.fp1RegButton.Size = new System.Drawing.Size(97, 37);
            this.fp1RegButton.TabIndex = 92;
            this.fp1RegButton.Text = " 지문서버 1등록";
            this.fp1RegButton.UseVisualStyleBackColor = true;
            this.fp1RegButton.Click += new System.EventHandler(this.fp1RegButton_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(41, 207);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(85, 37);
            this.button4.TabIndex = 91;
            this.button4.Text = " 4발전소   등록";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(259, 127);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(85, 37);
            this.button3.TabIndex = 90;
            this.button3.Text = " 3발전소   등록";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(135, 127);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 37);
            this.button2.TabIndex = 89;
            this.button2.Text = " 2발전소   등록";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 127);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 37);
            this.button1.TabIndex = 88;
            this.button1.Text = " 1발전소   등록";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(10, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 15);
            this.label10.TabIndex = 79;
            this.label10.Text = "*";
            // 
            // cardNum
            // 
            this.cardNum.Enabled = false;
            this.cardNum.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.cardNum.Location = new System.Drawing.Point(94, 27);
            this.cardNum.Name = "cardNum";
            this.cardNum.Size = new System.Drawing.Size(127, 23);
            this.cardNum.TabIndex = 36;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label21.Location = new System.Drawing.Point(21, 29);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(68, 17);
            this.label21.TabIndex = 35;
            this.label21.Text = "카드번호 :";
            // 
            // topLabel
            // 
            this.topLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(255)))));
            this.topLabel.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.topLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(0)))), ((int)(((byte)(76)))));
            this.topLabel.Image = ((System.Drawing.Image)(resources.GetObject("topLabel.Image")));
            this.topLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.topLabel.Location = new System.Drawing.Point(2, 2);
            this.topLabel.Name = "topLabel";
            this.topLabel.Size = new System.Drawing.Size(630, 55);
            this.topLabel.TabIndex = 63;
            this.topLabel.Text = "        발전소 등록";
            this.topLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlantRegForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 360);
            this.Controls.Add(this.cardGroupBox);
            this.Controls.Add(this.topLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PlantRegForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "발전소 등록";
            this.cardGroupBox.ResumeLayout(false);
            this.cardGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox cardGroupBox;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox cardNum;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label topLabel;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button fp1RegButton;
        private System.Windows.Forms.Button fp4RegButton;
        private System.Windows.Forms.Button fp2RegButton;
        private System.Windows.Forms.Button fp3RegButton;
    }
}