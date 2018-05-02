namespace BMClient
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.reg = new System.Windows.Forms.Button();
            this.result = new System.Windows.Forms.Button();
            this.compare = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // reg
            // 
            this.reg.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.reg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(0)))), ((int)(((byte)(76)))));
            this.reg.Location = new System.Drawing.Point(0, -1);
            this.reg.Name = "reg";
            this.reg.Size = new System.Drawing.Size(80, 30);
            this.reg.TabIndex = 64;
            this.reg.Text = "Reg";
            this.reg.UseVisualStyleBackColor = true;
            // 
            // result
            // 
            this.result.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.result.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(0)))), ((int)(((byte)(76)))));
            this.result.Location = new System.Drawing.Point(84, -1);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(80, 30);
            this.result.TabIndex = 65;
            this.result.Text = "Result";
            this.result.UseVisualStyleBackColor = true;
            // 
            // compare
            // 
            this.compare.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.compare.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(0)))), ((int)(((byte)(76)))));
            this.compare.Location = new System.Drawing.Point(169, -1);
            this.compare.Name = "compare";
            this.compare.Size = new System.Drawing.Size(85, 30);
            this.compare.TabIndex = 66;
            this.compare.Text = "Compare";
            this.compare.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 986);
            this.Controls.Add(this.compare);
            this.Controls.Add(this.result);
            this.Controls.Add(this.reg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button reg;
        private System.Windows.Forms.Button result;
        private System.Windows.Forms.Button compare;
    }
}