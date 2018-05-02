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
    public partial class AuthRetryForm : Form
    {

        Request req = null;
        string query = "";
        string[] right = new string[4];
        string bid = "";
        public AuthRetryForm(string[] _right,string _bid)
        {

            InitializeComponent();
            right = _right;
            textBox1.Text= bid = _bid;
            req = new Request();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string rCheck = "0";
           
                query = makeQuery();
                if (!query.Equals(""))
                {
                    rCheck = req.update("BMS011", query, "BMI");

                    if (rCheck.Equals("0")) MessageBox.Show(" 권한재전송 요청 완료..!");
                    else MessageBox.Show("권한재전송 요청 실패..");
                    this.Close();

                }
                else
                {
                    return;
                }
         
     
        }


        public string makeQuery()
        {

            int plant1Check = 0;
            int plant2Check = 0;
            int plant3Check = 0;
            int plant4Check = 0;
            if (plant1Checkbox.Checked) plant1Check = 1;
            if (plant2Checkbox.Checked) plant2Check = 1;
            if (plant3Checkbox.Checked) plant3Check = 1;
            if (plant4Checkbox.Checked) plant4Check = 1;

            int result1 = 100;
            int result2 = 100;
            int result3 = 100;
            int result4 = 100;

            if (plant1Check == 1) result1 = 0;
            if (plant2Check == 1) result2 = 0;
            if (plant3Check == 1) result3 = 0;
            if (plant4Check == 1) result4 = 0;
            if (plant1Check == 0 && plant2Check == 0 && plant3Check == 0 && plant4Check == 0)
            {
                MessageBox.Show("권한을 재전송할 발전소를 선택해주세요");
                return "";
            }

           
            query = "insert into PROCESS_REQUEST values('-10','-10','1800-01-01',-10,'-10',-10,-10,-10,'-10','-10','-10','-10'"
                  + ",-10, -10, -10,-10 ,-10 ,-10 ,-10 ,-10 , -10 ,'1800-01-01', '1800-01-01', -10 , "+plant1Check
                  + "," + plant2Check + "," + plant3Check + "," + plant4Check + " ,0 , -1 , -1 ,0, '" + right[0] + "' ,'" + right[1] + "' ,'" + right[2] + "' ,'" + right[3] + "' ," + result1 + "," + result2 + "," + result3 + "," + result4 + ",100,100,100,100,100,'U','" + bid + "',-10)";

         
            Log.WriteLogTmp("query : " + query);

            return query;

        }
    }
}
