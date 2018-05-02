using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace BMClient
{
    public partial class PlantRegForm : Form
    {
        Dictionary<string, string> getData = null;
        Request req = null;
        string query = "";
        BM_Reg_001 reg001;
        QueryMaker qm = new QueryMaker();


        public PlantRegForm(Dictionary<string,string> _getData,BM_Reg_001 _reg)
        {
            InitializeComponent();
            getData = _getData;
            reg001 = _reg;
            cardNum.Text = getData["bid"];
            if (cardNum.Text.Equals("0"))
            {
                MessageBox.Show("카드정보를 불러오는중에 장애가 발생했습니다 다시 시도해주세요");
                Log.WriteLog("카드번호 0 입력발생 PlantRegForm.cs 종료");
            }
            req = new Request();

            

        }

        private void button1_Click(object sender, EventArgs e)
        {
           query = "update BADGE set ACS_1 = 1 where bid ='" + getData["bid"] + "'";
               
            string rCheck = req.update("BMS011", query, "BMI");
            if (rCheck.Equals("0"))
            {
                
                MessageBox.Show("BM시스템 수정입력 완료..");
                query = makeQuery("1");
                rCheck = req.update("BMS011", query, "BMI");

                if (rCheck.Equals("0"))
                {

                    rCheck = req.update("BMS011", qm.getQuery("PLANT_REG_LOG", "1", "카드번호 : " + addZero(cardNum.Text) + " 1발전소 "), "BMI");
                    if (!rCheck.Equals("0")) MessageBox.Show("로그 저장 실패..");
                    MessageBox.Show("1발전소 등록요청 완료..!");
                    reg001.plant1RegCheckBox.Checked = true;
                    reg001.acs1EndDateButton.Enabled = true;
                    reg001.acs1EndDateTime.Enabled = true;
             
                }
                else MessageBox.Show("1발전소 등록요청 실패..");
           
            }
            else
            {
                MessageBox.Show("1발전소 등록이 실패하였습니다 서버와 통신상태를 확인하세요");
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            query = "update BADGE set ACS_2 = 1 where bid ='" + getData["bid"] + "'";

            string rCheck = req.update("BMS011", query, "BMI");
            if (rCheck.Equals("0"))
            {

                MessageBox.Show("BM시스템 수정입력 완료..");
                query = makeQuery("2");
                rCheck = req.update("BMS011", query, "BMI");

                if (rCheck.Equals("0"))
                {
                    rCheck = req.update("BMS011", qm.getQuery("PLANT_REG_LOG", "1", "카드번호 : " + addZero(cardNum.Text) + " 2발전소 "), "BMI");
                    if (!rCheck.Equals("0")) MessageBox.Show("로그 저장 실패..");

                    MessageBox.Show("2발전소 등록요청 완료..!");
                    reg001.plant2RegCheckBox.Checked = true;
                    reg001.acs2EndDateButton.Enabled = true;
                    reg001.acs2EndDateTime.Enabled = true;
                }

                else MessageBox.Show("2발전소 등록요청 실패..");

            }
            else
            {
                MessageBox.Show("2발전소 등록이 실패하였습니다 서버와 통신상태를 확인하세요");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            query = "update BADGE set ACS_3 = 1 where bid ='" + getData["bid"] + "'";

            string rCheck = req.update("BMS011", query, "BMI");
            if (rCheck.Equals("0"))
            {

                MessageBox.Show("BM시스템 수정입력 완료..");
                query = makeQuery("3");
                rCheck = req.update("BMS011", query, "BMI");

                if (rCheck.Equals("0"))
                {
                    rCheck = req.update("BMS011", qm.getQuery("PLANT_REG_LOG", "1", "카드번호 : " + addZero(cardNum.Text) + " 3발전소 "), "BMI");
                    if (!rCheck.Equals("0")) MessageBox.Show("로그 저장 실패..");


                    MessageBox.Show("3발전소 등록요청 완료..!");
                    reg001.plant3RegCheckBox.Checked = true;
                    reg001.acs3EndDateButton.Enabled = true;
                    reg001.acs3EndDateTime.Enabled = true;
                }
                else MessageBox.Show("3발전소 등록요청 실패..");

            }
            else
            {
                MessageBox.Show("3발전소 등록이 실패하였습니다 서버와 통신상태를 확인하세요");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            query = "update BADGE set ACS_4 = 1 where bid ='" + getData["bid"] + "'";

            string rCheck = req.update("BMS011", query, "BMI");
            if (rCheck.Equals("0"))
            {

                MessageBox.Show("BM시스템 수정입력 완료..");
                query = makeQuery("4");
                rCheck = req.update("BMS011", query, "BMI");

                if (rCheck.Equals("0"))
                {
                    rCheck = req.update("BMS011", qm.getQuery("PLANT_REG_LOG", "1", "카드번호 : " + addZero(cardNum.Text) + " 4발전소 "), "BMI");
                    if (!rCheck.Equals("0")) MessageBox.Show("로그 저장 실패..");


                    MessageBox.Show("4발전소 등록요청 완료..!");
                    reg001.plant4RegCheckBox.Checked = true;
                    reg001.acs4EndDateButton.Enabled = true;
                    reg001.acs4EndDateTime.Enabled = true;
                }
                else MessageBox.Show("4발전소 등록요청 실패..");

            }
            else
            {
                MessageBox.Show("4발전소 등록이 실패하였습니다 서버와 통신상태를 확인하세요");
            }
        }

        public string makeQuery(string ACS)
        {
            
            if(ACS.Equals("1"))
                query = "insert into PROCESS_REQUEST values('" + getData["bid"] + "','" + getData["name"] + "','" + getData["birth"] + "'," + getData["gender"] + ",'" + getData["ssno"] + "'," + getData["department"] + "," + getData["division"] + "," + getData["title"] + ",'" + getData["email"] + "','" + getData["tel"] + "','" + getData["address"] + "','" + getData["cardName"] + "'"
                  + ",0, " + getData["cardType"] + ", " + getData["cardStat"] + ",0 ,0 ,0 ,"+getData["cardFormat"]+ ", -10 , -10 ,'" + getData["activate"] + "', '" + getData["deactivate"] + "', -10 , 1"
                  + ",0 ,0 ,0 ,0 , -1, -1 ,0 ,'" + getData["right1"] + "' , ' " + getData["right2"] + "', '" + getData["right3"] + "' ,'" + getData["right4"] + "' ,0,100,100,100,100,100,100,100,100,'I','" + getData["bid"] + "',1)";
            
            
            
            else if (ACS.Equals("2"))
            {
                query = "insert into PROCESS_REQUEST values('" + getData["bid"] + "','" + getData["name"] + "','" + getData["birth"] + "'," + getData["gender"] + ",'" + getData["ssno"] + "'," + getData["department"] + "," + getData["division"] + "," + getData["title"] + ",'" + getData["email"] + "','" + getData["tel"] + "','" + getData["address"] + "','" + getData["cardName"] + "'"
                  + ",0, " + getData["cardType"] + ", " + getData["cardStat"] + ",0 ,0 ,0 ," + getData["cardFormat"] + ", -10 , -10 ,'" + getData["activate"] + "', '" + getData["deactivate"] + "', -10 , 0"
                  + ",1 ,0 ,0 ,0 , -1, -1 ,0 ,'" + getData["right1"] + "' , ' " + getData["right2"] + "', '" + getData["right3"] + "' ,'" + getData["right4"] + "',100,0,100,100,100,100,100,100,100,'I','" + getData["bid"] + "',1)";
            }

            else if (ACS.Equals("3"))
            {
                query = "insert into PROCESS_REQUEST values('" + getData["bid"] + "','" + getData["name"] + "','" + getData["birth"] + "'," + getData["gender"] + ",'" + getData["ssno"] + "'," + getData["department"] + "," + getData["division"] + "," + getData["title"] + ",'" + getData["email"] + "','" + getData["tel"] + "','" + getData["address"] + "','" + getData["cardName"] + "'"
                  + ",0, " + getData["cardType"] + ", " + getData["cardStat"] + ",0 ,0 ,0 ," + getData["cardFormat"] + ", -10 , -10 ,'" + getData["activate"] + "', '" + getData["deactivate"] + "', -10 , 0"
                  + ",0 ,1 ,0 ,0 , -1 , -1 ,0 ,'" + getData["right1"] + "' , ' " + getData["right2"] + "', '" + getData["right3"] + "' ,'" + getData["right4"] + "',100,100,0,100,100,100,100,100,100,'I','" + getData["bid"] + "',1)";
            }

            else if (ACS.Equals("4"))
            {
                query = "insert into PROCESS_REQUEST values('" + getData["bid"] + "','" + getData["name"] + "','" + getData["birth"] + "'," + getData["gender"] + ",'" + getData["ssno"] + "'," + getData["department"] + "," + getData["division"] + "," + getData["title"] + ",'" + getData["email"] + "','" + getData["tel"] + "','" + getData["address"] + "','" + getData["cardName"] + "'"
                  + ",0, " + getData["cardType"] + ", " + getData["cardStat"] + ",0 ,0 ,0 ," + getData["cardFormat"] + " , -10 , -10 ,'" + getData["activate"] + "', '" + getData["deactivate"] + "', -10 , 0"
                  + ",0 ,0 ,1 ,0 , -1 , -1 ,0 ,'" + getData["right1"] + "' , ' " + getData["right2"] + "', '" + getData["right3"] + "' ,'" + getData["right4"] + "',100,100,100,0,100,100,100,100,100,'I','" + getData["bid"] + "',1)";
            }

            else if (ACS.Equals("5"))
            {
                query = "insert into PROCESS_REQUEST values('" + getData["bid"] + "','" + getData["name"] + "','" + getData["birth"] + "'," + getData["gender"] + ",'" + getData["ssno"] + "'," + getData["department"] + "," + getData["division"] + "," + getData["title"] + ",'" + getData["email"] + "','" + getData["tel"] + "','" + getData["address"] + "','" + getData["cardName"] + "'"
                  + ",0, " + getData["cardType"] + ", " + getData["cardStat"] + ",0 ,0 ,0 ," + getData["cardFormat"] + " , -10 , -10 ,'" + getData["activate"] + "', '" + getData["deactivate"] + "', " + getData["byPass"] + ", 0"
                  + ",0 ,0 ,0 ,0 , 0 , -1 ,0 , -100 ,-100 ,-100 ,-100 ,100,100,100,100,0,100,100,100,100,'I','" + getData["bid"] + "',1)";
            }
            else if (ACS.Equals("6"))
            {
                query = "insert into PROCESS_REQUEST values('" + getData["bid"] + "','" + getData["name"] + "','" + getData["birth"] + "'," + getData["gender"] + ",'" + getData["ssno"] + "'," + getData["department"] + "," + getData["division"] + "," + getData["title"] + ",'" + getData["email"] + "','" + getData["tel"] + "','" + getData["address"] + "','" + getData["cardName"] + "'"
                  + ",0, " + getData["cardType"] + ", " + getData["cardStat"] + ",0 ,0 ,0 ," + getData["cardFormat"] + " , -10 , -10 ,'" + getData["activate"] + "', '" + getData["deactivate"] + "', " + getData["byPass"] + ", 0"
                  + ",0 ,0 ,0 ,0 , 0 ,-1 ,0 , -100 ,-100 ,-100 ,-100 ,100,100,100,100,100,0,100,100,100,'I','" + getData["bid"] + "',1)";
            }
            else if (ACS.Equals("7"))
            {
                query = "insert into PROCESS_REQUEST values('" + getData["bid"] + "','" + getData["name"] + "','" + getData["birth"] + "'," + getData["gender"] + ",'" + getData["ssno"] + "'," + getData["department"] + "," + getData["division"] + "," + getData["title"] + ",'" + getData["email"] + "','" + getData["tel"] + "','" + getData["address"] + "','" + getData["cardName"] + "'"
                  + ",0, " + getData["cardType"] + ", " + getData["cardStat"] + ",0 ,0 ,0 ," + getData["cardFormat"] + " , -10 , -10 ,'" + getData["activate"] + "', '" + getData["deactivate"] + "', " + getData["byPass"] + ", 0"
                  + ",0 ,0 ,0 ,0 , 0 , -1 ,0 , -100 ,-100 ,-100 ,-100 ,100,100,100,100,100,100,0,100,100,'I','" + getData["bid"] + "',1)";
            }

            else if (ACS.Equals("8"))
            {
                query = "insert into PROCESS_REQUEST values('" + getData["bid"] + "','" + getData["name"] + "','" + getData["birth"] + "'," + getData["gender"] + ",'" + getData["ssno"] + "'," + getData["department"] + "," + getData["division"] + "," + getData["title"] + ",'" + getData["email"] + "','" + getData["tel"] + "','" + getData["address"] + "','" + getData["cardName"] + "'"
                  + ",0, " + getData["cardType"] + ", " + getData["cardStat"] + ",0 ,0 ,0 ," + getData["cardFormat"] + " ," + getData["issueType"] + " , " + getData["issueReason"] + " ,'" + getData["activate"] + "', '" + getData["deactivate"] + "'," + getData["byPass"] + " , 0"
                  + ",0 ,0 ,0 ,0 , -1 , 0 ,-1, -100 ,-100 ,-100 ,-100 ,100,100,100,100,100,100,100,0,100,'I','" + getData["bid"] + "'," + getData["pt"] + ")";
            }
            else if (ACS.Equals("9"))
            {
                query = "insert into PROCESS_REQUEST values('" + getData["bid"] + "','" + getData["name"] + "','" + getData["birth"] + "'," + getData["gender"] + ",'" + getData["ssno"] + "'," + getData["department"] + "," + getData["division"] + "," + getData["title"] + ",'" + getData["email"] + "','" + getData["tel"] + "','" + getData["address"] + "','" + getData["cardName"] + "'"
                  + ",0, " + getData["cardType"] + ", " + getData["cardStat"] + ",0 ,0 ,0 ," + getData["cardFormat"] + " ," + getData["issueType"] + " , " + getData["issueReason"] + " ,'" + getData["activate"] + "', '" + getData["deactivate"] + "', -10 , 0"
                  + ",0 ,0 ,0 ,0 , -1 , -1 ,1, -100 ,-100 ,-100 ,-100 ,100,100,100,100,100,100,100,100,0,'I','" + getData["bid"] + "'," + getData["pt"] + ")";
            }

            Log.WriteLogTmp("query : " + query);

            return query;
        
        }

    

        private void button6_Click(object sender, EventArgs e)
        {
            query = "update BADGE set VM = 1 where bid ='" + getData["bid"] + "'";

            string rCheck = req.update("BMS011", query, "BMI");
            if (rCheck.Equals("0"))
            {

                MessageBox.Show("BM시스템 수정입력 완료..");
                query = makeQuery("9");
                rCheck = req.update("BMS011", query, "BMI");

                if (rCheck.Equals("0"))
                {
                    rCheck = req.update("BMS011", qm.getQuery("PLANT_REG_LOG", "1", "카드번호 : " + addZero(cardNum.Text) + " VM "), "BMI");
                    if (!rCheck.Equals("0")) MessageBox.Show("로그 저장 실패..");

                    reg001.vmCheck.Checked = true;
                    MessageBox.Show("VM 등록요청 완료..!");
                    
                }
                else MessageBox.Show("VM 등록요청 실패..");

            }
            else
            {
                MessageBox.Show("VM 등록이 실패하였습니다 서버와 통신상태를 확인하세요");
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
        private void fp1RegButton_Click_1(object sender, EventArgs e)
        {
            query = "update BADGE set FP_1 = 0 where bid ='" + getData["bid"] + "'";

            string rCheck = req.update("BMS011", query, "BMI");
            if (rCheck.Equals("0"))
            {

                MessageBox.Show("BM시스템 수정입력 완료..");
                query = makeQuery("5");
                rCheck = req.update("BMS011", query, "BMI");

                if (rCheck.Equals("0"))
                {
                    rCheck = req.update("BMS011", qm.getQuery("PLANT_REG_LOG", "1", "카드번호 : " + addZero(cardNum.Text) + " 지문서버1 "), "BMI");
                    if (!rCheck.Equals("0")) MessageBox.Show("로그 저장 실패..");

                    MessageBox.Show("지문서버1 등록요청 완료..!");
                    reg001.fpCheck.Checked = true;
                }
                else MessageBox.Show("지문서버1 등록요청 실패..");

            }
            else
            {
                MessageBox.Show("지문서버1 등록이 실패하였습니다 서버와 통신상태를 확인하세요");
            }
        }

        private void fp2RegButton_Click(object sender, EventArgs e)
        {
            query = "update BADGE set FP_1 = 0 where bid ='" + getData["bid"] + "'";

            string rCheck = req.update("BMS011", query, "BMI");
            if (rCheck.Equals("0"))
            {

                MessageBox.Show("BM시스템 수정입력 완료..");
                query = makeQuery("6");
                rCheck = req.update("BMS011", query, "BMI");

                if (rCheck.Equals("0"))
                {
                    rCheck = req.update("BMS011", qm.getQuery("PLANT_REG_LOG", "1", "카드번호 : " + addZero(cardNum.Text) + " 지문서버1 "), "BMI");

                    if (!rCheck.Equals("0")) MessageBox.Show("로그 저장 실패..");

                    MessageBox.Show("지문서버2 등록요청 완료..!");

                    reg001.fpCheck.Checked = true;
                }
                else MessageBox.Show("지문서버2 등록요청 실패..");

            }
            else
            {
                MessageBox.Show("지문서버2 등록이 실패하였습니다 서버와 통신상태를 확인하세요");
            }
        }

        private void fp3RegButton_Click(object sender, EventArgs e)
        {
            query = "update BADGE set FP_1 = 0 where bid ='" + getData["bid"] + "'";

            string rCheck = req.update("BMS011", query, "BMI");
            if (rCheck.Equals("0"))
            {

                MessageBox.Show("BM시스템 수정입력 완료..");
                query = makeQuery("7");
                rCheck = req.update("BMS011", query, "BMI");

                if (rCheck.Equals("0"))
                {
                    rCheck = req.update("BMS011", qm.getQuery("PLANT_REG_LOG", "1", "카드번호 : " + addZero(cardNum.Text) + " 지문서버1 "), "BMI");
                    if (!rCheck.Equals("0")) MessageBox.Show("로그 저장 실패..");

                    MessageBox.Show("지문서버3 등록요청 완료..!");
                    reg001.fpCheck.Checked = true;
                }
                else MessageBox.Show("지문서버3 등록요청 실패..");

            }
            else
            {
                MessageBox.Show("지문서버1 등록이 실패하였습니다 서버와 통신상태를 확인하세요");
            }
        }
        private void fp4RegButton_Click(object sender, EventArgs e)
        {
            query = "update BADGE set FP_2 = 0 where bid ='" + getData["bid"] + "'";

            string rCheck = req.update("BMS011", query, "BMI");
            if (rCheck.Equals("0"))
            {

                MessageBox.Show("BM시스템 수정입력 완료..");
                query = makeQuery("8");
                rCheck = req.update("BMS011", query, "BMI");

                if (rCheck.Equals("0"))
                {
                    rCheck = req.update("BMS011", qm.getQuery("PLANT_REG_LOG", "1", "카드번호 : " + addZero(cardNum.Text) + " 지문서버2 "), "BMI");
                    if (!rCheck.Equals("0")) MessageBox.Show("로그 저장 실패..");
                    MessageBox.Show("지문서버4 등록요청 완료..!");
                }
                else MessageBox.Show("지문서버4 등록요청 실패..");

            }
            else
            {
                MessageBox.Show("지문서버2 등록이 실패하였습니다 서버와 통신상태를 확인하세요");
            }
        }
        
    }
}
