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
    public partial class CardRestoreForm : Form
    {

        Request req;


        public CardRestoreForm(string bid)
        {
            InitializeComponent();
            cardNum.Focus();

            cardNum.Text = bid;
            req = new Request();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (cardNum.Text.Equals(""))
            {
                MessageBox.Show("카드번호를 입력해주세요..");
                return;
            }

            if (MessageBox.Show("해당 카드를 복구하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string query = "update BADGE set DELETE_FLAG = 'N' where bid = '" + addZero(cardNum.Text) + "'";

                string rCheck = req.update("BMS011", query, "BMI");

                if (rCheck.Equals("0"))
                {
                    MessageBox.Show("카드복구가 정상적으로 되었습니다.");
                }
                else
                {
                    MessageBox.Show("카드복구가 실패하였습니다..");
                }
            }
        }


        public string addZero(string org)
        {
            while (org.Length < 12)
            {
                org = "0" + org;
            }

            return org;
        }
    }
}
