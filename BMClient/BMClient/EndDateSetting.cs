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
    public partial class EndDateSetting : Form
    {

        Request req; 
       
        public EndDateSetting(string bid,Boolean plant1Reg,Boolean plant2Reg,Boolean plant3Reg,Boolean plant4Reg)
        {
           InitializeComponent();
           req = new Request();
           cardNum.Text = bid;
           string query = "";
           string rNumString = "";
           if (plant1Reg)
           {
               query = makeQuery("" + Bm_Main.serverInfo.Rows[0]["SERVER_TYPE"]);
               rNumString = req.select("BMS011", query, "ACS1S");

               if (rNumString != "0")
               {
                   MessageBox.Show("[EndDateSetting.cs] EndDateSetting()  장애 발생 (1발전소)");
               }

               try
               {
                   dateTimePicker1.Value = getdate("" + Bm_Main.serverInfo.Rows[0]["SERVER_TYPE"]);
               }
               catch (Exception e)
               {
                   MessageBox.Show("만료일 조회중에 장애 발생..(1발전소)");
                   dateTimePicker1.Value = DateTime.Now;
               }

           }
           else
           {
               dateTimePicker1.Enabled = false;
               button1.Enabled = false;
           }
           if (plant2Reg)
           {
               query = makeQuery("" + Bm_Main.serverInfo.Rows[1]["SERVER_TYPE"]);
               rNumString = req.select("BMS011", query, "ACS2S");

               if (rNumString != "0")
               {
                   MessageBox.Show("[EndDateSetting.cs] EndDateSetting()  장애 발생 (1발전소) ");
               }


               try
               {
                   dateTimePicker2.Value = getdate("" + Bm_Main.serverInfo.Rows[1]["SERVER_TYPE"]);
               }
               catch (Exception e)
               {
                   MessageBox.Show("만료일 조회중에 장애 발생..(2발전소)");
                   dateTimePicker2.Value = DateTime.Now;
               }

           }
           else
           {
               dateTimePicker2.Enabled = false;
               button2.Enabled = false;
           }

           if (plant3Reg)
           {

               query = makeQuery("" + Bm_Main.serverInfo.Rows[2]["SERVER_TYPE"]);
               rNumString = req.select("BMS011", query, "ACS3S");

               if (rNumString != "0")
               {
                   MessageBox.Show("[EndDateSetting.cs] EndDateSetting()  장애 발생 (3발전소)");
               }
               try
               {
                   dateTimePicker3.Value = getdate("" + Bm_Main.serverInfo.Rows[2]["SERVER_TYPE"]);
               }
               catch (Exception e)
               {
                   MessageBox.Show("만료일 조회중에 장애 발생.. (3발전소)");
                   dateTimePicker3.Value = DateTime.Now;
               }

           }
           else
           {
               dateTimePicker3.Enabled = false;
               button3.Enabled = false;
           }
           if (plant4Reg)
           {
               try
               {
                   query = makeQuery("" + Bm_Main.serverInfo.Rows[3]["SERVER_TYPE"]);


                   rNumString = req.select("BMS011", query, "ACS4S");
                   if (rNumString != "0")
                   {
                       MessageBox.Show("[EndDateSetting.cs] EndDateSetting()  장애 발생");
                   }

                   dateTimePicker4.Value = getdate("" + Bm_Main.serverInfo.Rows[3]["SERVER_TYPE"]);
               }
               catch (Exception e)
               {
                   MessageBox.Show("만료일 조회중에 장애 발생..");
                   dateTimePicker4.Value = DateTime.Now;
               }
           }
           else
           {
               dateTimePicker4.Enabled = false;
           }

        }

        private void EndDateSetting_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

                string query = "insert into PROCESS_REQUEST values("
                              + "'-10' , '-10' , '1800-01-01' , -10 , '-10' , -10 , -10 , -10 , '-10' , '-10' , '-10' ,'-10' ,"
                              + "-10" + ", -10 , -10 ,-10 ,-10 ,-10 , -10 , -10 , -10 ,'1800-01-01','"+dateTimePicker1.Value.ToShortDateString()+"' , -10 , -10,-10 ,-10 ,-10 ,-10 , -10 , -10 ,-10 , -10 ,-10 ,-10 ,-10 ,0,100,100,100,100,100,100,'U','"
                              + cardNum.Text + "',-10)";
                string rCheck = req.update("BMS011", query, "BMI");

                if (!rCheck.Equals("0"))
                {
                    MessageBox.Show("만료일 수정요청 실패 서버와의 통신을 점검해주세요..");
                    return;
                }
                

              

                string bmQuery = "update BADGE set DEACTIVATE_DATE = '" + dateTimePicker1.Value.ToShortDateString() + "' where bid = '" + cardNum.Text + "'";
                rCheck = req.update("BMS011", bmQuery, "BMI");
                if (rCheck.Equals("0"))
                {
                    MessageBox.Show("만료일 수정요청 성공..");
                }
                else
                {
                    MessageBox.Show("만료일 수정요청 실패 서버와의 통신을 점검해주세요..");
                }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "insert into PROCESS_REQUEST values("
                              + "'-10' , '-10' , '1800-01-01' , -10 , '-10' , -10 , -10 , -10 , '-10' , '-10' , '-10' ,'-10' ,"
                              + "-10" + ", -10 , -10 ,-10 ,-10 ,-10 , -10 , -10 , -10 ,'1800-01-01','" + dateTimePicker2.Value.ToShortDateString() + "' , -10 , -10,-10 ,-10 ,-10 ,-10 , -10 , -10 ,-10 , -10 ,-10 ,-10 ,-10 ,100,0,100,100,100,100,100,'U','"
                              + cardNum.Text + "',-10)";
            string rCheck = req.update("BMS011", query, "BMI");

            if (rCheck.Equals("0"))
            {
                MessageBox.Show("만료일 수정요청 성공..");
            }
            else
            {
                MessageBox.Show("만료일 수정요청 실패 서버와의 통신을 점검해주세요..");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "insert into PROCESS_REQUEST values("
                              + "'-10' , '-10' , '1800-01-01' , -10 , '-10' , -10 , -10 , -10 , '-10' , '-10' , '-10' ,'-10' ,"
                              + "-10" + ", -10 , -10 ,-10 ,-10 ,-10 , -10 , -10 , -10 ,'1800-01-01','" + dateTimePicker3.Value.ToShortDateString() + "' , -10 , -10,-10 ,-10 ,-10 ,-10 , -10 , -10 ,-10 , -10 ,-10 ,-10 ,-10 ,100,100,0,100,100,100,100,'U','"
                              + cardNum.Text + "',-10)";
            string rCheck = req.update("BMS011", query, "BMI");

            if (rCheck.Equals("0"))
            {
                MessageBox.Show("만료일 수정요청 성공..");
            }
            else
            {
                MessageBox.Show("만료일 수정요청 실패 서버와의 통신을 점검해주세요..");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string query = "insert into PROCESS_REQUEST values("
                              + "'-10' , '-10' , '1800-01-01' , -10 , '-10' , -10 , -10 , -10 , '-10' , '-10' , '-10' ,'-10' ,"
                              + "-10" + ", -10 , -10 ,-10 ,-10 ,-10 , -10 , -10 , -10 ,'1800-01-01','" + dateTimePicker4.Value.ToShortDateString() + "' , -10 , -10,-10 ,-10 ,-10 ,-10 , -10 , -10 ,-10 , -10 ,-10 ,-10 ,-10 ,100,100,100,0,100,100,100,'U','"
                              + cardNum.Text + "',-10)";
            string rCheck = req.update("BMS011", query, "BMI");

            if (rCheck.Equals("0"))
            {
                MessageBox.Show("만료일 수정요청 성공..");
            }
            else
            {
                MessageBox.Show("만료일 수정요청 실패 서버와의 통신을 점검해주세요..");
            }
        }

        public string makeQuery(string ACS)
        {
            string query = "";
            if (ACS.Equals("3"))
            {
                query = "select top 1 DEACTIVATE from BADGE where id = " + cardNum.Text;
            }
            else if (ACS.Equals("1") || ACS.Equals("2"))
            {
                query = "select first 1 expired_date from badge where bid ='" + cardNum.Text + "'";
            }

            return query;

        }

        public DateTime getdate(string ACSType)
        {
            DateTime dt;


            if (ACSType.Equals("3"))
            {
                string[] tmp = ReturnDT.dt.Rows[0][0].ToString().Split('-');
                dt = new DateTime(Convert.ToInt32(tmp[0]),Convert.ToInt32(tmp[1]),Convert.ToInt32(tmp[2].Substring(0,2)));
               
            }
            else
            {
                dt = new DateTime(Convert.ToInt32(ReturnDT.dt.Rows[0][0].ToString().Substring(0,4)),Convert.ToInt32(ReturnDT.dt.Rows[0][0].ToString().Substring(4,2)),Convert.ToInt32(ReturnDT.dt.Rows[0][0].ToString().Substring(6,2)));
            }


            return dt;

        }
    }
}
