namespace BMClient
{
    partial class Bm_Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bm_Login));
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.txt_Pw = new System.Windows.Forms.TextBox();
            this.txt_Id = new System.Windows.Forms.TextBox();
            this.CheckSaveId = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLogin.BackgroundImage")));
            this.btnLogin.Location = new System.Drawing.Point(505, 65);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(77, 57);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.Location = new System.Drawing.Point(505, 151);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 35);
            this.btnExit.TabIndex = 1;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txt_Pw
            // 
            this.txt_Pw.Location = new System.Drawing.Point(265, 100);
            this.txt_Pw.Name = "txt_Pw";
            this.txt_Pw.Size = new System.Drawing.Size(228, 21);
            this.txt_Pw.TabIndex = 3;
            this.txt_Pw.UseSystemPasswordChar = true;
            this.txt_Pw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbox_Pw_KeyDown);
            // 
            // txt_Id
            // 
            this.txt_Id.Location = new System.Drawing.Point(265, 63);
            this.txt_Id.Name = "txt_Id";
            this.txt_Id.Size = new System.Drawing.Size(228, 21);
            this.txt_Id.TabIndex = 2;
            // 
            // CheckSaveId
            // 
            this.CheckSaveId.AutoSize = true;
            this.CheckSaveId.Location = new System.Drawing.Point(187, 161);
            this.CheckSaveId.Name = "CheckSaveId";
            this.CheckSaveId.Size = new System.Drawing.Size(88, 16);
            this.CheckSaveId.TabIndex = 4;
            this.CheckSaveId.Text = "아이디 저장";
            this.CheckSaveId.UseVisualStyleBackColor = true;
            // 
            // Bm_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(614, 206);
            this.Controls.Add(this.CheckSaveId);
            this.Controls.Add(this.txt_Pw);
            this.Controls.Add(this.txt_Id);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnLogin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Bm_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bm_Login";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Bm_Login_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txt_Pw;
        private System.Windows.Forms.TextBox txt_Id;
        private System.Windows.Forms.CheckBox CheckSaveId;
    }
}