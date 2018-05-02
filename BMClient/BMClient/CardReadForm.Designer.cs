namespace BS_SDK
{
    partial class CardReadForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardReadForm));
            this.ip = new System.Windows.Forms.TextBox();
            this.finger1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bid = new System.Windows.Forms.TextBox();
            this.readButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ip
            // 
            this.ip.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ip.Location = new System.Drawing.Point(126, 22);
            this.ip.Name = "ip";
            this.ip.Size = new System.Drawing.Size(133, 25);
            this.ip.TabIndex = 58;
            this.ip.Text = "192.168.1.188";
            this.ip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // finger1
            // 
            this.finger1.AutoSize = true;
            this.finger1.BackColor = System.Drawing.Color.Transparent;
            this.finger1.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.finger1.Location = new System.Drawing.Point(12, 25);
            this.finger1.Name = "finger1";
            this.finger1.Size = new System.Drawing.Size(93, 20);
            this.finger1.TabIndex = 82;
            this.finger1.Text = "지문 단말 IP";
            this.finger1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(23, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 20);
            this.label1.TabIndex = 84;
            this.label1.Text = "카드 BID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Visible = false;
            // 
            // bid
            // 
            this.bid.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bid.Location = new System.Drawing.Point(126, 57);
            this.bid.Name = "bid";
            this.bid.Size = new System.Drawing.Size(133, 25);
            this.bid.TabIndex = 83;
            this.bid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.bid.Visible = false;
            // 
            // readButton
            // 
            this.readButton.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.readButton.Location = new System.Drawing.Point(91, 98);
            this.readButton.Name = "readButton";
            this.readButton.Size = new System.Drawing.Size(89, 30);
            this.readButton.TabIndex = 85;
            this.readButton.Text = "카드 읽기";
            this.readButton.UseVisualStyleBackColor = true;
            // 
            // CardReadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 155);
            this.Controls.Add(this.readButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bid);
            this.Controls.Add(this.finger1);
            this.Controls.Add(this.ip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CardReadForm";
            this.Text = "카드관리";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ip;
        private System.Windows.Forms.Label finger1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox bid;
        private System.Windows.Forms.Button readButton;
    }
}