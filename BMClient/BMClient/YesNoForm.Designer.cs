namespace BMClient
{
    partial class YesNoForm
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
            this.yes = new System.Windows.Forms.Button();
            this.no = new System.Windows.Forms.Button();
            this.question = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // yes
            // 
            this.yes.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.yes.ForeColor = System.Drawing.Color.Black;
            this.yes.Location = new System.Drawing.Point(71, 80);
            this.yes.Name = "yes";
            this.yes.Size = new System.Drawing.Size(63, 25);
            this.yes.TabIndex = 46;
            this.yes.Text = "예";
            this.yes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.yes.UseVisualStyleBackColor = true;
            // 
            // no
            // 
            this.no.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.no.ForeColor = System.Drawing.Color.Black;
            this.no.Location = new System.Drawing.Point(161, 80);
            this.no.Name = "no";
            this.no.Size = new System.Drawing.Size(63, 25);
            this.no.TabIndex = 47;
            this.no.Text = "아니오";
            this.no.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.no.UseVisualStyleBackColor = true;
            // 
            // question
            // 
            this.question.AutoSize = true;
            this.question.BackColor = System.Drawing.SystemColors.Control;
            this.question.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.question.ForeColor = System.Drawing.Color.Blue;
            this.question.Location = new System.Drawing.Point(12, 23);
            this.question.Name = "question";
            this.question.Size = new System.Drawing.Size(54, 20);
            this.question.TabIndex = 54;
            this.question.Text = "질   문";
            // 
            // YesNoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 132);
            this.Controls.Add(this.question);
            this.Controls.Add(this.no);
            this.Controls.Add(this.yes);
            this.Name = "YesNoForm";
            this.Text = "YesNoForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button yes;
        private System.Windows.Forms.Button no;
        private System.Windows.Forms.Label question;

    }
}