using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;


using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading;

namespace BMClient
{
    public partial class BM_ReReg : Form
    {
        BM_Reg_001 reg;

        string oldBid = "";
        string newBid = "";
        Request req;
        QueryMaker qm = new QueryMaker();
        Boolean flag = true;
        public BM_ReReg(BM_Reg_001 reg001)
        {
            InitializeComponent();

            reg = reg001;
            req = new Request();
            
            cardNum.KeyDown += new KeyEventHandler(cardNum_KeyDown);
        
            if (reg001.endDate.Value.CompareTo(DateTime.Now) < 0)
            {
                flag = false;

            }

            endDateTime.MaskInputRejected += new MaskInputRejectedEventHandler(endDateTime_MaskInputRejected);
        }

        void endDateTime_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            toolTip1.Show("숫자만 입력할수있습니다.", endDateTime, 1000);
        }

        void cardNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) rereg();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now.AddDays(-1);


            
            try
            {
               dt = Convert.ToDateTime(endDateTime.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show("만료일을 제대로 입력하지 않았습니다.기존의 만료일을 사용합니다.");
                dt = new DateTime(1800, 1, 1);
            }


            if (flag || dt.CompareTo(DateTime.Now) > 0)
                rereg();
            else
                MessageBox.Show("만료일이 작습니다. 새로운 만료일자를 입력해주세요.");

        }

        public void rereg()
        {
            Cursor = Cursors.WaitCursor;

            oldBid = reg.cardNum.Text;
            newBid = addZero(cardNum.Text);
            Boolean flag = reg.checkDuplicate(newBid);
            string[] right = reg.getCurRight();


            for (int i = 0; i < right.Length; i++)
            {
                if (right[i].Equals("")) right[i] = "-100";
            }

            int plantReg1 = 0;
            int plantReg2 = 0;
            int plantReg3 = 0;
            int plantReg4 = 0;
            int FPReg = 0;
            int VMReg = 0;

            if (reg.plant1RegCheckBox.Checked) plantReg1 = 1;
            if (reg.plant2RegCheckBox.Checked) plantReg2 = 1;
            if (reg.plant3RegCheckBox.Checked) plantReg3 = 1;
            if (reg.plant4RegCheckBox.Checked) plantReg4 = 1;
            if (reg.fpCheck.Checked) FPReg = 1;
            if (reg.vmCheck.Checked) VMReg = 1;

            if (!flag)
            {
                string sendDate = oldBid + "&" + newBid + "&" + right[0] + "&" + right[1] + "&" + right[2] + "&" + right[3] + "&" + plantReg1 + "&" + plantReg2 + "&" + plantReg3 + "&" + plantReg4
                                + "&" + FPReg + "&" + VMReg+"&"+endDateTime.Text;
                string rNumString = req.update("BMS011", sendDate, "BMRE");

                if (!rNumString.Equals("0"))
                {
                    MessageBox.Show("[Reg.cs] BmReReg ()  재등록중에 오류가 발생하였습니다.");
                    Cursor = Cursors.Default;
                    return;
                }
                else
                {
                    rNumString = req.update("BMS011", qm.getQuery("RE_REG_LOG", oldBid + " => " + newBid), "BMI");

                    MessageBox.Show("[Reg.cs] BmReReg ()  재등록 완료...!");
                    reg.showCardList(25);
                    Cursor = Cursors.Default;
                    this.Close();
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

        private void BM_ReReg_Load(object sender, EventArgs e)
        {

        }

    }
}
