namespace BMClient
{
    partial class AuthRetryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthRetryForm));
            this.topLabel = new System.Windows.Forms.Label();
            this.plant1Checkbox = new System.Windows.Forms.CheckBox();
            this.plant2Checkbox = new System.Windows.Forms.CheckBox();
            this.plant3Checkbox = new System.Windows.Forms.CheckBox();
            this.plant4Checkbox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // topLabel
            // 
            this.topLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(255)))));
            this.topLabel.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.topLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(0)))), ((int)(((byte)(76)))));
            this.topLabel.Image = ((System.Drawing.Image)(resources.GetObject("topLabel.Image")));
            this.topLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.topLabel.Location = new System.Drawing.Point(1, 2);
            this.topLabel.Name = "topLabel";
            this.topLabel.Size = new System.Drawing.Size(388, 55);
            this.topLabel.TabIndex = 64;
            this.topLabel.Text = "        권한 재전송";
            this.topLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // plant1Checkbox
            // 
            this.plant1Checkbox.AutoSize = true;
            this.plant1Checkbox.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold);
            this.plant1Checkbox.Location = new System.Drawing.Point(24, 125);
            this.plant1Checkbox.Name = "plant1Checkbox";
            this.plant1Checkbox.Size = new System.Drawing.Size(79, 21);
            this.plant1Checkbox.TabIndex = 65;
            this.plant1Checkbox.Text = "1 발전소";
            this.plant1Checkbox.UseVisualStyleBackColor = true;
            // 
            // plant2Checkbox
            // 
            this.plant2Checkbox.AutoSize = true;
            this.plant2Checkbox.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold);
            this.plant2Checkbox.Location = new System.Drawing.Point(24, 163);
            this.plant2Checkbox.Name = "plant2Checkbox";
            this.plant2Checkbox.Size = new System.Drawing.Size(79, 21);
            this.plant2Checkbox.TabIndex = 66;
            this.plant2Checkbox.Text = "2 발전소";
            this.plant2Checkbox.UseVisualStyleBackColor = true;
            // 
            // plant3Checkbox
            // 
            this.plant3Checkbox.AutoSize = true;
            this.plant3Checkbox.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold);
            this.plant3Checkbox.Location = new System.Drawing.Point(24, 203);
            this.plant3Checkbox.Name = "plant3Checkbox";
            this.plant3Checkbox.Size = new System.Drawing.Size(79, 21);
            this.plant3Checkbox.TabIndex = 67;
            this.plant3Checkbox.Text = "3 발전소";
            this.plant3Checkbox.UseVisualStyleBackColor = true;
            // 
            // plant4Checkbox
            // 
            this.plant4Checkbox.AutoSize = true;
            this.plant4Checkbox.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold);
            this.plant4Checkbox.Location = new System.Drawing.Point(24, 227);
            this.plant4Checkbox.Name = "plant4Checkbox";
            this.plant4Checkbox.Size = new System.Drawing.Size(79, 21);
            this.plant4Checkbox.TabIndex = 68;
            this.plant4Checkbox.Text = "4 발전소";
            this.plant4Checkbox.UseVisualStyleBackColor = true;
            this.plant4Checkbox.Visible = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(165, 150);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 34);
            this.button1.TabIndex = 69;
            this.button1.Text = "권한 재전송";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(113, 75);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(115, 21);
            this.textBox1.TabIndex = 70;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(21, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 71;
            this.label1.Text = "카드번호";
            // 
            // AuthRetryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.plant4Checkbox);
            this.Controls.Add(this.plant3Checkbox);
            this.Controls.Add(this.plant2Checkbox);
            this.Controls.Add(this.plant1Checkbox);
            this.Controls.Add(this.topLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AuthRetryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "권한재전송";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label topLabel;
        private System.Windows.Forms.CheckBox plant1Checkbox;
        private System.Windows.Forms.CheckBox plant2Checkbox;
        private System.Windows.Forms.CheckBox plant3Checkbox;
        private System.Windows.Forms.CheckBox plant4Checkbox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}