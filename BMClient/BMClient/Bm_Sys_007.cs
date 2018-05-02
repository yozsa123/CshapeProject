using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMClient
{
    public partial class Bm_Sys_007 : Form
    {

        Request req;
        QueryMaker qm = new QueryMaker();
        public Bm_Sys_007()
        {
            InitializeComponent();
            req = new Request();
        }
        public void updatePassword()
        {
            string rCheck = req.update("BMS009", qm.getQuery("Sys_007_U", Bm_Login.login_Id,Bm_Main.setPw(txtNewPassword.Text)), "BMI");


            if (rCheck.Equals("0"))
            {

                MessageBox.Show("패스워드가 정상적으로 변경되었습니다");
                rCheck = req.update("BMS009", qm.getQuery("SYS007_LOG_M", "1"), "BMI");
                if (!rCheck.Equals("0")) MessageBox.Show("로그 저장 실패..");
            }
            else
            {
                MessageBox.Show("패스워드 변경 실패");

            }
        }

      

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChangePassword_Click_1(object sender, EventArgs e)
        {
            if (!Bm_Login.login_Pw.Equals(txtCurrentPassword.Text))
            {
                MessageBox.Show("현재 패스워드가 일치하지않습니다. 다시 입력해주세요 ");
                return;
            }

           

            if (!txtNewPassword.Text.Equals(txtNewPasswordCheck.Text))
            {
                MessageBox.Show("입력하신 새로운 패드워드가 일치하지않습니다 다시 입력해주세요 ");
                return;
            }
            if (MessageBox.Show("패스워드를 변경하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                updatePassword();
                txtCurrentPassword.Text = "";
                txtNewPassword.Text = "";
                txtNewPasswordCheck.Text = "";
            }
            else
            {
                return;

            }
        }

        

      
    }
}
