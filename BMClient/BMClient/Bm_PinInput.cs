using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BMClient
{
    public partial class Bm_PinInput : Form
    {
        Request req;
        string bid;
        string cardName;
        QueryMaker qm = new QueryMaker();

        public Bm_PinInput(BM_Reg_001 reg)
        {
            InitializeComponent();

            cardName = reg.getCardName();
            bid = cardNum.Text = reg.getCardNum();

            cardNum.Enabled = false;
            if (cardNum.Text.Equals("0"))
            {
                MessageBox.Show("카드정보를 불러오는중에 장애가 발생했습니다 다시 시도해주세요");
                Log.WriteLog("카드번호 0 입력발생 PlantRegForm.cs 종료");
            }



             req = new Request();
             textBox1.TextChanged += new EventHandler(textBox1_TextChanged);




             if (!reg.plant1RegCheckBox.Checked) button1.Enabled = false;
             if (!reg.plant2RegCheckBox.Checked) button2.Enabled = false;
             if (!reg.plant3RegCheckBox.Checked) button3.Enabled = false;
             if (!reg.plant4RegCheckBox.Checked) button4.Enabled = false;
        }

        void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = textBox3.Text = textBox4.Text = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkNum(textBox1.Text))
            {
                if (!("" + Bm_Main.serverInfo.Rows[0]["SERVER_TYPE"]).Equals("3"))
                {
                    ppInsertPin(textBox1.Text, cardName, "ACS1");
                    string Check = req.update("BMS0011", qm.getQuery("REG001_LOG_PIN", "1", "카드번호 : " + addZero(bid) + " 1발전소 비밀번호 -> " + textBox1.Text), "BMI");
                    if (!Check.Equals("0")) MessageBox.Show("로그 저장 실패..");
                }
                else if (("" + Bm_Main.serverInfo.Rows[0]["SERVER_TYPE"]).Equals("3"))
                {

                    string query = "insert into PROCESS_REQUEST values("
                                  + "'-10' , '-10' , '1800-01-01' , -10 , '-10' , -10 , -10 , -10 , '-10' , '-10' , '-10' ,'-10' ,"
                                  + textBox1.Text + ", -10 , -10 ,-10 ,-10 ,-10 , -10 , -10 , -10 ,'1800-01-01','1800-01-01' , -10 , -10,-10 ,-10 ,-10 ,-10 , -10 , -10 ,-10 , -10 ,-10 ,-10 ,-10 ,0,100,100,100,100,100,100,100,100,'U','"
                                  + addZero(bid)+"',-10)";
                    Log.WriteLogTmp("Bm_PinInput Query : " + query);
                    string rCheck = req.update("BMS011", query, "BMI");

                    if (rCheck.Equals("0"))
                    {
                        MessageBox.Show("비밀번호 입력 성공..");

                        string Check = req.update("BMS0011", qm.getQuery("REG001_LOG_PIN", "1", "카드번호 : " + addZero(bid) + " 1발전소 비밀번호 -> " + textBox1.Text), "BMI");
                        if (!Check.Equals("0")) MessageBox.Show("로그 저장 실패..");
                    }
                    else
                    {
                        MessageBox.Show("비밀번호 입력이 실패하였습니다. 등록되어있는 비밀번호인지 확인해주세요.");
                    }
                    
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkNum(textBox2.Text))
            {
                if (!("" + Bm_Main.serverInfo.Rows[1]["SERVER_TYPE"]).Equals("3"))
                {
                    ppInsertPin(textBox2.Text, cardName, "ACS2");
                    string Check = req.update("BMS0011", qm.getQuery("REG001_LOG_PIN", "1", "카드번호 : " + addZero(bid) + " 2발전소 비밀번호 -> " + textBox2.Text), "BMI");
                    if (!Check.Equals("0")) MessageBox.Show("로그 저장 실패..");
                }
                else if (("" + Bm_Main.serverInfo.Rows[1]["SERVER_TYPE"]).Equals("3"))
                {
                    string query = "insert into PROCESS_REQUEST values("
                                  + "'-10' , '-10' , '1800-01-01' , -10 , '-10' , -10 , -10 , -10 , '-10' , '-10' , '-10' ,'-10' ,"
                                  + textBox2.Text + ", -10 , -10 ,-10 ,-10 ,-10 , -10 , -10 , -10 ,'1800-01-01','1800-01-01' , -10 , -10,-10 ,-10 ,-10 ,-10 , -10 , -10 ,-10 , -10 ,-10 ,-10 ,-10 ,100,0,100,100,100,100,100,100,100,'U','"
                                  + addZero(bid) + "',-10)";
                    Log.WriteLogTmp("Bm_PinInput Query : " + query);
                    string rCheck = req.update("BMS011", query, "BMI");
                    if (rCheck.Equals("0"))
                    {
                        MessageBox.Show("비밀번호 입력 성공..");
                        string Check = req.update("BMS0011", qm.getQuery("REG001_LOG_PIN", "1", "카드번호 : " + addZero(bid) + " 2발전소 비밀번호 -> " + textBox2.Text), "BMI");
                        if (!Check.Equals("0")) MessageBox.Show("로그 저장 실패..");
                    }
                    else
                    {
                        MessageBox.Show("비밀번호 입력이 실패하였습니다. 등록되어있는 비밀번호인지 확인해주세요.");
                    }

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (checkNum(textBox3.Text))
            {
                if (!("" + Bm_Main.serverInfo.Rows[2]["SERVER_TYPE"]).Equals("3"))
                {
                    ppInsertPin(textBox3.Text, cardName, "ACS3");
                    string Check = req.update("BMS0011", qm.getQuery("REG001_LOG_PIN", "1", "카드번호 : " + addZero(bid) + " 3발전소 비밀번호 -> " + textBox3.Text), "BMI");
                    if (!Check.Equals("0")) MessageBox.Show("로그 저장 실패..");

                }
                else if (("" + Bm_Main.serverInfo.Rows[2]["SERVER_TYPE"]).Equals("3"))
                {
                    string query = "insert into PROCESS_REQUEST values("
                                  + "'-10' , '-10' , '1800-01-01' , -10 , '-10' , -10 ,-10 , -10 , '-10' , '-10' , '-10' ,'-10' ,"
                                  + textBox3.Text + ", -10 , -10 ,-10 ,-10 ,-10 , -10 , -10 , -10 ,'1800-01-01','1800-01-01' , -10 , -10,-10 ,-10 ,-10 ,-10 , -10 , -10 ,-10 , -10 ,-10 ,-10 ,-10 ,100,100,0,100,100,100,100,100,100,'U','"
                                  + addZero(bid) + "',-10)";
                    Log.WriteLogTmp("Bm_PinInput Query : " + query);
                    string rCheck = req.update("BMS011", query, "BMI");
                    if (rCheck.Equals("0"))
                    {
                        MessageBox.Show("비밀번호 입력 성공..");

                        string Check = req.update("BMS0011", qm.getQuery("REG001_LOG_PIN", "1", "카드번호 : " + addZero(bid) + " 3발전소 비밀번호 -> " + textBox3.Text), "BMI");
                        if (!Check.Equals("0")) MessageBox.Show("로그 저장 실패..");

                    }
                    else
                    {
                        MessageBox.Show("비밀번호 입력이 실패하였습니다. 등록되어있는 비밀번호인지 확인해주세요.");
                    }

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (checkNum(textBox4.Text))
            {
                if (!("" + Bm_Main.serverInfo.Rows[3]["SERVER_TYPE"]).Equals("3"))
                {
                    ppInsertPin(textBox4.Text, cardName, "ACS4");
                    string Check = req.update("BMS0011", qm.getQuery("REG001_LOG_PIN", "1", "카드번호 : " + addZero(bid) + " 4발전소 비밀번호 -> " + textBox4.Text), "BMI");
                    if (!Check.Equals("0")) MessageBox.Show("로그 저장 실패..");
                }
                else if (("" + Bm_Main.serverInfo.Rows[3]["SERVER_TYPE"]).Equals("3"))
                {
                    string query = "insert into PROCESS_REQUEST values("
                                  + "'-10' , '-10' , '1800-01-01' , -10 , '-10' , -10 , -10 , -10 , '-10' , '-10' , '-10' ,'-10' ,"
                                  + textBox4.Text + ", -10 , -10 ,-10 ,-10 ,-10 , -10 , -10 , -10 ,'1800-01-01','1800-01-01' , -10 , -10,-10 ,-10 ,-10 ,-10 , -10 , -10 ,-10 , -10 ,-10 ,-10 ,-10 ,100,100,100,0,100,100,100,100,100,'U','"
                                  + addZero(bid) + "',-10)";
                    Log.WriteLogTmp("Bm_PinInput Query : " + query);
                    string rCheck = req.update("BMS011", query, "BMI");
                    if (rCheck.Equals("0"))
                    {
                        MessageBox.Show("비밀번호 입력 성공..");

                        string Check = req.update("BMS0011", qm.getQuery("REG001_LOG_PIN", "1", "카드번호 : " + addZero(bid) + " 4발전소 비밀번호 -> " + textBox4.Text), "BMI");
                        if (!Check.Equals("0")) MessageBox.Show("로그 저장 실패..");

                    }
                    else
                    {
                        MessageBox.Show("비밀번호 입력이 실패하였습니다. 등록되어있는 비밀번호인지 확인해주세요.");
                    }

                }
            }
        }

        public Boolean checkNum(string num)
        {
            
            if (num.Length < 4)
            {
                MessageBox.Show("비밀번호는 무조건 4자리 이상으로 입력해야합니다.");
                return false;
            }
            
            try
            {
                Convert.ToInt32(num);

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("비밀번호는 숫자만 입력가능합니다");
                return false;
            }

        }

        public int ppInsertPin(string pin,string cardName,string acs)
        {

            Log.WriteLog("[Bm_PinInput.cs] ppInsertPin (" + pin + ", " + cardName + ", " + acs + ")");


            /////////////////////////////// (1) 카드번호를 가지고 PP badge 테이블에서 해당 row를 찾는다 /////////////////////////
            string selectQuery = "select * from badge where bid='" + cardNum.Text + "'";

            string modifyDate = "" + System.DateTime.Now.Year; //+System.DateTime.Now.Month +System.DateTime.Now.Day  ;


            if (System.DateTime.Now.Month < 10) modifyDate += "0" + System.DateTime.Now.Month;
            else modifyDate += System.DateTime.Now.Month;


            if (System.DateTime.Now.Day < 10) modifyDate += "0" + System.DateTime.Now.Day;
            else modifyDate += System.DateTime.Now.Day;

            string rCheck = req.select("BMS011", selectQuery, acs + "S");

            if (!rCheck.Equals("0"))
            {
                MessageBox.Show("출입통제 시스템간의 통신이 정상적이지않습니다.");
                Log.WriteLog("[Bm_PinInput.cs] ppInsertPin () select badge query fail");
                return 0;
            }

            // selectQuery 의 결과 row 갯수 확인 : 해당 badge row 가 1인 경우만 작업 //////////
            if (ReturnDT.dt.Rows.Count < 1)
            {
                MessageBox.Show("PP badge 테이블에 등록되어있지 않은 카드입니다.");
                Log.WriteLog("[Bm_PinInput.cs] ppInsertPin () PP badge 테이블에 등록되어있지 않은 카드 ㅠㅠ");
                return 0;
            }
            else if (ReturnDT.dt.Rows.Count > 1)
            {
                MessageBox.Show("PP badge 테이블에 2장 이상 등록된 카드입니다.");
                Log.WriteLog("[Bm_PinInput.cs] ppInsertPin () PP badge 테이블에 2장 이상 등록된 카드ㅠㅠ");
                return 0;

            }


            /////////////////////////////// (2) person 테이블의 해당 row의 Pin번호 값을 변경하고 badge 테이블의 pin값을 변경하거나 신규 insert ////////////////////////

            string digitTwelve = getIni("DIGIT_TWELVE");

            string pinQuery = "";

            int person_id = 0;
            person_id = Convert.ToInt32(ReturnDT.dt.Rows[0]["person_id"]);

            string deleteBadgeQuery = "delete from badge where person_id = " + person_id + " and bid_format_id != " + digitTwelve;
            string updatePersonQuery = "update person set pin ='" + pin + "' where id = (select person_id from badge where bid='" + bid + "')";
            string insertBadgeQuery = "";

            // badge row의 수가 2보다 작은 경우 : Pin 번호를 가지고 row를 신규 insert
            // bid_format (1) : 10 digit,   (2) : 12 digit,   (3) : 16 digit,    (6) : ?

            // "delete from badge where person_id = "~~" and bid_format_id != 2"


            string bidFormatNum = "";
            bidFormatNum = getIni("PIN_FORMAT_" + acs);



            insertBadgeQuery = "insert into badge(description , bid , status , person_id , issue_date , issue_time  ,issue_context , bid_format_id , facility , modify_date , modify_time ) ";
            insertBadgeQuery += "values( '" + cardName + "' , '" + pin + "', " + 0 + "," + person_id + " , " + modifyDate + " ,0,0," + bidFormatNum + ", " + "-1 , " + modifyDate + ", 0)";



            pinQuery = deleteBadgeQuery + " ^ " + updatePersonQuery + " ^ " + insertBadgeQuery;

            Log.WriteLog("[Bm_PinInput.cs] ppInsertPin () pinQuery : " + pinQuery);

            rCheck = req.update("BMS011", pinQuery, acs + "I");

            if (rCheck.Equals("0"))
            {
                MessageBox.Show("비밀번호 입력 성공..");
            }
            else
            {
                MessageBox.Show("비밀번호 입력이 실패하였습니다. 등록되어있는 비밀번호인지 확인해주세요.");
            }


            return 1;
        }

        public string addZero(string org)
        {
            while (org.Length < 12)
            {
                org = "0" + org;
            }

            return org;
        }

        public string getIni(string key)
        {
            using (StreamReader sr = new StreamReader(".\\client.ini"))
            {
                String line;

                String delimStr = "=";
                char[] delimiter = delimStr.ToCharArray();
                String[] strData = null;
                while ((line = sr.ReadLine()) != null)
                {

                    if (line.StartsWith(key))
                    {
                        int index = line.IndexOf("=");
                        if (index < 0) return null;

                        string result = line.Substring(index + 1);
                        return result;

                    }
                }
            }

            return null;

        }

    }
}
