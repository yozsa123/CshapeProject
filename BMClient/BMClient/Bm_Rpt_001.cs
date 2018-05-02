using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;

namespace BMClient
{
    public partial class Bm_Rpt_001 : Form
    {



        private DataGridViewTextBoxColumn[][] topCell = null;
        string delimeter = "^";

        string queryType = "S";
        QueryMaker qm = new QueryMaker();
        Request req = new Request();
        
        int cNum = 2;
        DataTable dt;

        int rowNum = 300;

        int perPage = 800;
        int lastSSNO = 0;
        int totalPage = 0;
        int currentPage = 0;
        int lastSEQ = 0;

        string[] serverNum;
        DataTable[] resultDt;
        string rCheck = "";
        public Bm_Rpt_001()
        {
            InitializeComponent();
           

            getDepartment();
            getDivison();
          
            getCardType();
            getIssueReason();
            getIssueType();

            tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);
            comboAuth1.Items.Add("전체");
            comboAuth2.Items.Add("전체");
            comboAuth3.Items.Add("전체");
            comboAuth4.Items.Add("전체");
            getAuth1();
            getAuth2();
            getAuth3();
            getAuth4();

            serverNum = new string[Bm_Main.serverInfo.Rows.Count];

            comboDepartment.SelectedIndexChanged += new EventHandler(comboDepartment_SelectedIndexChanged);
            comboDivision.SelectedIndexChanged +=new EventHandler(comboDivision_SelectedIndexChanged);

            textName.KeyDown += new KeyEventHandler(textName_KeyDown);
            txtCardNum.KeyDown += new KeyEventHandler(txtCardNum_KeyDown);
            txtSSNO.KeyDown += new KeyEventHandler(txtSSNO_KeyDown);

            comboAuth1.SelectedIndexChanged += new EventHandler(comboAuth1_SelectedIndexChanged);
            comboAuth1.SelectedIndex =0;
            comboAuth2.SelectedIndex =0;
            comboAuth3.SelectedIndex =0;
            comboAuth4.SelectedIndex = 0;


            radioCardNum.CheckedChanged += new EventHandler(radioCardNum_CheckedChanged);
            radioCardType.CheckedChanged += new EventHandler(radioCardType_CheckedChanged);
            radioDate.CheckedChanged += new EventHandler(radioDate_CheckedChanged);
            radioIssueReason.CheckedChanged += new EventHandler(radioIssueReason_CheckedChanged);
            radioIssueType.CheckedChanged += new EventHandler(radioIssueType_CheckedChanged);
            radioDepartment.CheckedChanged += new EventHandler(radioDepartment_CheckedChanged);
            radioDivision.CheckedChanged += new EventHandler(radioDivision_CheckedChanged);
            radioSSNO.CheckedChanged += new EventHandler(radioSSNO_CheckedChanged);
            radioName.CheckedChanged += new EventHandler(radioName_CheckedChanged);
            allEnableFalse();
            radioName.Checked = true;
            textName.Enabled = true;
            txtCardNum.Enabled = true;

            comboCardStat.Items.Add("전체");
            comboCardStat.Items.Add("활성");
            comboCardStat.Items.Add("비활성");

        }

        void txtSSNO_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Enter)
            {
                showData();
            }
          
        }

        void txtCardNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                showData();
            }
          
        }

        void radioName_CheckedChanged(object sender, EventArgs e)
        {

            txtCardNum.Text = "";
            textName.Text = "";
            txtSSNO.Text = "";
            if (radioName.Checked)
            {
                allEnableFalse();
                textName.Enabled = true;
            }
        }

        void radioSSNO_CheckedChanged(object sender, EventArgs e)
        {
            txtCardNum.Text = "";
            textName.Text = "";
            txtSSNO.Text = "";
            if (radioSSNO.Checked)
            {
                allEnableFalse();
                txtSSNO.Enabled = true;
            }
        }

        void radioDivision_CheckedChanged(object sender, EventArgs e)
        {
            txtCardNum.Text = "";
            textName.Text = "";
            txtSSNO.Text = "";
            if (radioDivision.Checked)
            {
                allEnableFalse();
                comboDivision.Enabled = true;
            }
        }

        void radioIssueType_CheckedChanged(object sender, EventArgs e)
        {
            txtCardNum.Text = "";
            textName.Text = "";
            txtSSNO.Text = "";
            if (radioIssueType.Checked)
            {
                allEnableFalse();
                comboIssueType.Enabled = true;
            }
        }

        void radioIssueReason_CheckedChanged(object sender, EventArgs e)
        {
            txtCardNum.Text = "";
            textName.Text = "";
            txtSSNO.Text = "";
            if (radioIssueReason.Checked)
            {
                allEnableFalse();
                comboIssueReson.Enabled = true;
            }
        }

        void radioDepartment_CheckedChanged(object sender, EventArgs e)
        {
            txtCardNum.Text = "";
            textName.Text = "";
            txtSSNO.Text = "";
            if (radioDepartment.Checked)
            {
                allEnableFalse();
                comboDepartment.Enabled = true;
            }
        }

        void radioDate_CheckedChanged(object sender, EventArgs e)
        {
            txtCardNum.Text = "";
            textName.Text = "";
            txtSSNO.Text = "";
            if (radioDate.Checked)
            {
                allEnableFalse();
                endDateTime.Enabled = true;
            }
        }

        void radioCardType_CheckedChanged(object sender, EventArgs e)
        {
            txtCardNum.Text = "";
            textName.Text = "";
            txtSSNO.Text = "";
            if (radioCardType.Checked)
            {
                allEnableFalse();
                comboRegType.Enabled = true;
            }
        }

        void radioCardNum_CheckedChanged(object sender, EventArgs e)
        {
            txtCardNum.Text = "";
            textName.Text = "";
            txtSSNO.Text = "";
            if (radioCardNum.Checked)
            {
                allEnableFalse();
                txtCardNum.Enabled = true;
            }
         
        }
        public void allEnableFalse(){
            textName.Enabled = false;
            comboDepartment.Enabled = false;
            comboDivision.Enabled = false;
            txtSSNO.Enabled = false;
            comboIssueReson.Enabled = false;
            comboIssueType.Enabled = false;
            endDateTime.Enabled = false;
            txtCardNum.Enabled = false;
            comboRegType.Enabled = false;
        }

        void textName_KeyDown(object sender, KeyEventArgs e)
        {
            
            radioName.Checked = true;
            if (e.KeyCode == Keys.Enter)
            {
                showData();
            }
          
        }

       
        void comboAuth1_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        

        void comboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        void comboPlant_SelectedValueChanged(object sender, EventArgs e)
        {
          
           
        }

        public void getAuth1()
        {
            for (int i = 0; i < Bm_Main.ACS1AccessLvl.Rows.Count;i++ )
                comboAuth1.Items.Add(Bm_Main.ACS1AccessLvl.Rows[i]["DESCRIPTION"]); 
        }
        public void getAuth2()
        {
            for (int i = 0; i < Bm_Main.ACS2AccessLvl.Rows.Count; i++)
                comboAuth2.Items.Add(Bm_Main.ACS2AccessLvl.Rows[i]["DESCRIPTION"]);

        }
        public void getAuth3()
        {
            for (int i = 0; i < Bm_Main.ACS3AccessLvl.Rows.Count; i++)
                comboAuth3.Items.Add(Bm_Main.ACS3AccessLvl.Rows[i]["DESCRIPTION"]);
        }
        public void getAuth4()
        {
            for (int i = 0; i < Bm_Main.ACS4AccessLvl.Rows.Count; i++)
                comboAuth4.Items.Add(Bm_Main.ACS4AccessLvl.Rows[i]["DESCRIPTION"]);
        }
        public void getDepartment()
        {
             

             for (int i = 0; i < Bm_Main.department.Rows.Count; i++)
             {
                 comboDepartment.Items.Add("" + Bm_Main.department.Rows[i][1]);
             }
        }

        public void getDivison()
        {
           

            for (int i = 0; i < Bm_Main.division.Rows.Count; i++)
            {
                comboDivision.Items.Add("" + Bm_Main.division.Rows[i][1]);
            }

        }

        public void getIssueType()
        {
            for (int i = 0; i < Bm_Main.issueType.Rows.Count; i++)
            {
                comboIssueType.Items.Add("" + Bm_Main.issueType.Rows[i][1]);
            }
        }

        public void getIssueReason()
        {
            for (int i = 0; i < Bm_Main.issueReason.Rows.Count; i++)
            {
                comboIssueReson.Items.Add("" + Bm_Main.issueReason.Rows[i][1]);
            }
        }

        public void getCardType()
        {
        

            for (int i = 0; i < Bm_Main.cardType.Rows.Count; i++)
            {
                comboRegType.Items.Add("" + Bm_Main.cardType.Rows[i][1]);
            }
        }

      

        void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            radioSetFalseCheck();
             if (tabControl1.SelectedIndex == 0)
            {
                radioName.Checked = true;

                radioIssueType.Checked = false;
                radioIssueReason.Checked = false;
                radioDate.Checked = false;
                radioCardType.Checked = false;
                radioCardNum.Checked = false;
                authRadioButton1.Checked = false;

            }
             else if (tabControl1.SelectedIndex == 1)
             {
                 radioCardNum.Checked = true;
            
                 
                 radioName.Checked = false;
                 radioDepartment.Checked = false;
                 radioDivision.Checked = false;
                 radioSSNO.Checked = false;
                 authRadioButton1.Checked = false;

             }
             else if (tabControl1.SelectedIndex == 2)
             {
                 authRadioButton1.Checked = true;

                 radioName.Checked = false;
                 radioDepartment.Checked = false;
                 radioDivision.Checked = false;
                 radioSSNO.Checked = false;
         
                 radioIssueType.Checked = false;
                 radioIssueReason.Checked = false;
                 radioDate.Checked = false;
                 radioCardType.Checked = false;
                 radioCardNum.Checked = false;
              


             }
    

        }


        public void radioSetFalseCheck()
        {
            radioSSNO.Checked = false;
       
            radioCardType.Checked = false;
          
            radioDivision.Checked = false;
            radioDepartment.Checked = false;
            radioDate.Checked = false;
            radioIssueReason.Checked = false;
            radioIssueType.Checked = false;
            radioCardNum.Checked = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            radioDivision.Select();
        }

        public int checkResult()
        {
            if (Convert.ToInt32(dt.Rows[0][0]).Equals(0))
            {
                MessageBox.Show("검색결과가 없습니다");
                ReportGridView.DataSource = null;
                return 1;
            }
            else
            {
                return 2;
            }

        }

        public void showData()
        {

            labelBefore.Visible = false;
            labelNext.Visible = false;
            resultDt = null;
            dt = null;
            lastSSNO = 0;
            lastSEQ = 0;
            string startDate = startDateTime.Value.ToShortDateString();
            string endDate = endDateTime.Value.ToShortDateString();
            
            string rCheck = "";

            Cursor.Current = Cursors.WaitCursor;
            if (radioSSNO.Checked == true)
            {
                rCheck = req.select("BMS019", qm.getQuery("Rpt_001_SSNO_Count", txtSSNO.Text), "BMS");

                if (rCheck.Equals("0"))
                {
                    dt = ReturnDT.dt;

                    if (checkResult() == 1) return;


                    MessageBox.Show("총 " + Convert.ToInt32(dt.Rows[0][0]) + "건이 검색되었습니다");

                    totalPage = (int)(Convert.ToInt32(dt.Rows[0][0]) / perPage);
                    totalPage = (Convert.ToInt32(dt.Rows[0][0]) % perPage == 0) ? totalPage : totalPage += 1;
                    resultDt = new DataTable[totalPage];


                }
                else
                {
                    MessageBox.Show("데이터를 불러오는데 실패했습니다. 다시 시도해주세요");
                }


                for (int i = 0; i < totalPage; i++)
                {
                    rCheck = req.select("BMS019", qm.getQuery("Rpt_001_SSNO", txtSSNO.Text, "" + lastSEQ), "BMS");
                    resultDt[i] = ReturnDT.dt;
                    if (resultDt[i].Rows.Count == perPage) lastSEQ = Convert.ToInt32(resultDt[i].Rows[perPage - 1][0]);

                }

            }
            else if (radioName.Checked == true)
            {
                rCheck = req.select("BMS019", qm.getQuery("Rpt_001_NAME_Count", textName.Text), "BMS");


                if (rCheck.Equals("0"))
                {
                    dt = ReturnDT.dt;

                    if (checkResult() == 1) return;


                    MessageBox.Show("총 " + Convert.ToInt32(dt.Rows[0][0]) + "건이 검색되었습니다");

                    totalPage = (int)(Convert.ToInt32(dt.Rows[0][0]) / perPage);
                    totalPage = (Convert.ToInt32(dt.Rows[0][0]) % perPage == 0) ? totalPage : totalPage += 1;

                    resultDt = new DataTable[totalPage];
                }
                else
                {
                    MessageBox.Show("데이터를 불러오는데 실패했습니다. 다시 시도해주세요");
                }

                for (int i = 0; i < totalPage; i++)
                {
                    rCheck = req.select("BMS019", qm.getQuery("Rpt_001_NAME", textName.Text, "" + lastSEQ), "BMS");

                    resultDt[i] = ReturnDT.dt;
                    if (resultDt[i].Rows.Count == perPage) lastSEQ = Convert.ToInt32(resultDt[i].Rows[perPage - 1][0]);

                }
            }

            else if (radioDepartment.Checked == true)
            {
                rCheck = req.select("BMS019", qm.getQuery("Rpt_001_Department_Count", comboDepartment.Text), "BMS");


                if (rCheck.Equals("0"))
                {
                    dt = ReturnDT.dt;

                    if (checkResult() == 1) return;


                    MessageBox.Show("총 " + Convert.ToInt32(dt.Rows[0][0]) + "건이 검색되었습니다");

                    totalPage = (int)(Convert.ToInt32(dt.Rows[0][0]) / perPage);
                    totalPage = (Convert.ToInt32(dt.Rows[0][0]) % perPage == 0) ? totalPage : totalPage += 1;
                
                    resultDt = new DataTable[totalPage];
                }
                else
                {
                    MessageBox.Show("데이터를 불러오는데 실패했습니다. 다시 시도해주세요");
                }
                
                for (int i = 0; i < totalPage; i++)
                {
                    rCheck = req.select("BMS019", qm.getQuery("Rpt_001_Department", comboDepartment.Text, "" + lastSEQ), "BMS");

                    resultDt[i]  = ReturnDT.dt;
                    if (resultDt[i].Rows.Count == perPage) lastSEQ = Convert.ToInt32(resultDt[i].Rows[perPage - 1][0]);

                }
            }
            else if (radioDivision.Checked == true)
            {
                rCheck = req.select("BMS019", qm.getQuery("Rpt_001_Division_Count", comboDivision.Text), "BMS");


                if (rCheck.Equals("0"))
                {
                    dt = ReturnDT.dt;

                    if (checkResult() == 1) return;


                    MessageBox.Show("총 " + Convert.ToInt32(dt.Rows[0][0]) + "건이 검색되었습니다");

                    totalPage = (int)(Convert.ToInt32(dt.Rows[0][0]) / perPage);
                    totalPage = (Convert.ToInt32(dt.Rows[0][0]) % perPage == 0) ? totalPage : totalPage += 1;

                    resultDt = new DataTable[totalPage];
                }
                else
                {
                    MessageBox.Show("데이터를 불러오는데 실패했습니다. 다시 시도해주세요");
                }



                for (int i = 0; i < totalPage; i++)
                {
                    rCheck = req.select("BMS019", qm.getQuery("Rpt_001_Division", comboDivision.Text, "" + lastSEQ), "BMS");
                    resultDt[i] = ReturnDT.dt;
                    if (resultDt[i].Rows.Count == perPage) lastSEQ = Convert.ToInt32(resultDt[i].Rows[perPage - 1][0]);

                }


            }
            else if (radioCardNum.Checked == true)
            {
                rCheck = req.select("BMS019", qm.getQuery("Rpt_001_CardNum_Count", txtCardNum.Text), "BMS");


                if (rCheck.Equals("0"))
                {
                    dt = ReturnDT.dt;

                    if (checkResult() == 1) return;

                    MessageBox.Show("총 " + Convert.ToInt32(dt.Rows[0][0]) + "건이 검색되었습니다");

                    totalPage = (int)(Convert.ToInt32(dt.Rows[0][0]) / perPage);
                    totalPage = (Convert.ToInt32(dt.Rows[0][0]) % perPage == 0) ? totalPage : totalPage += 1;

                    resultDt = new DataTable[totalPage];
                }
                else
                {
                    MessageBox.Show("데이터를 불러오는데 실패했습니다. 다시 시도해주세요");
                }

                for (int i = 0; i < totalPage; i++)
                {
                    rCheck = req.select("BMS019", qm.getQuery("Rpt_001_CardNum", txtCardNum.Text, "" + lastSEQ), "BMS");
                    resultDt[i] = ReturnDT.dt;
                    if (resultDt[i].Rows.Count == perPage) lastSEQ = Convert.ToInt32(resultDt[i].Rows[perPage - 1][0]);

                }

            }
            else if (radioCardType.Checked == true)
            {
                rCheck = req.select("BMS019", qm.getQuery("Rpt_001_CardType_Count", comboRegType.Text), "BMS");


                if (rCheck.Equals("0"))
                {
                    dt = ReturnDT.dt;

                    if (checkResult() == 1) return;


                    MessageBox.Show("총 " + Convert.ToInt32(dt.Rows[0][0]) + "건이 검색되었습니다");

                    totalPage = (int)(Convert.ToInt32(dt.Rows[0][0]) / perPage);
                    totalPage = (Convert.ToInt32(dt.Rows[0][0]) % perPage == 0) ? totalPage : totalPage += 1;

                    resultDt = new DataTable[totalPage];
                }
                else
                {
                    MessageBox.Show("데이터를 불러오는데 실패했습니다. 다시 시도해주세요");
                }

                for (int i = 0; i < totalPage; i++)
                {
                    rCheck = req.select("BMS019", qm.getQuery("Rpt_001_CardType", comboRegType.Text, "" + lastSEQ), "BMS");
                    resultDt[i] = ReturnDT.dt;
                    if (resultDt[i].Rows.Count == perPage) lastSEQ = Convert.ToInt32(resultDt[i].Rows[perPage - 1][0]);

                }
            }

            else if (radioDate.Checked == true)
            {
                rCheck = req.select("BMS019", qm.getQuery("Rpt_001_Date_Count", startDate, endDate), "BMS");




               
                if (rCheck.Equals("0"))
                {
                    dt = ReturnDT.dt;

                    if (checkResult() == 1) return;


                    MessageBox.Show("총 " + Convert.ToInt32(dt.Rows[0][0]) + "건이 검색되었습니다");

                    totalPage = (int)(Convert.ToInt32(dt.Rows[0][0]) / perPage);
                    totalPage = (Convert.ToInt32(dt.Rows[0][0]) % perPage == 0) ? totalPage : totalPage += 1;

                    resultDt = new DataTable[totalPage];
                }
                else
                {
                    MessageBox.Show("데이터를 불러오는데 실패했습니다. 다시 시도해주세요");
                }

                for (int i = 0; i < totalPage; i++)
                {
                    rCheck = req.select("BMS019", qm.getQuery("Rpt_001_Date", startDate, endDate, "" + lastSEQ, ""), "BMS");
                    resultDt[i] = ReturnDT.dt;
                    if (resultDt[i].Rows.Count == perPage) lastSEQ = Convert.ToInt32(resultDt[i].Rows[perPage - 1][0]);

                }
            }
            else if (radioIssueType.Checked == true)
            {
                rCheck = req.select("BMS019", qm.getQuery("Rpt_001_IssueType_Count", comboIssueType.Text), "BMS");


                if (rCheck.Equals("0"))
                {
                    dt = ReturnDT.dt;

                    if (checkResult() == 1) return;


                    MessageBox.Show("총 " + Convert.ToInt32(dt.Rows[0][0]) + "건이 검색되었습니다");

                    totalPage = (int)(Convert.ToInt32(dt.Rows[0][0]) / perPage);
                    totalPage = (Convert.ToInt32(dt.Rows[0][0]) % perPage == 0) ? totalPage : totalPage += 1;

                    resultDt = new DataTable[totalPage];
                }
                else
                {
                    MessageBox.Show("데이터를 불러오는데 실패했습니다. 다시 시도해주세요");
                }

                for (int i = 0; i < totalPage; i++)
                {
                    rCheck = req.select("BMS019", qm.getQuery("Rpt_001_IssueType", comboIssueType.Text, "" + lastSEQ), "BMS");
                    resultDt[i] = ReturnDT.dt;
                    if (resultDt[i].Rows.Count == perPage) lastSEQ = Convert.ToInt32(resultDt[i].Rows[perPage - 1][0]);

                }

            }
            else if (radioIssueReason.Checked == true)
            {
                rCheck = req.select("BMS019", qm.getQuery("Rpt_001_IssueReason_Count", comboIssueReson.Text), "BMS");


                if (rCheck.Equals("0"))
                {
                    dt = ReturnDT.dt;


                    if (checkResult() == 1) return;


                    MessageBox.Show("총 " + Convert.ToInt32(dt.Rows[0][0]) + "건이 검색되었습니다");

                    totalPage = (int)(Convert.ToInt32(dt.Rows[0][0]) / perPage);
                    totalPage = (Convert.ToInt32(dt.Rows[0][0]) % perPage == 0) ? totalPage : totalPage += 1;

                    resultDt = new DataTable[totalPage];
                    

                }
                else
                {
                    MessageBox.Show("데이터를 불러오는데 실패했습니다. 다시 시도해주세요");
                }



                for (int i = 0; i < totalPage; i++)
                {
                    rCheck = req.select("BMS019", qm.getQuery("Rpt_001_IssueReason", comboIssueReson.Text, "" + lastSEQ), "BMS");
                     resultDt[i] = ReturnDT.dt;
                     if (resultDt[i].Rows.Count == perPage) lastSEQ = Convert.ToInt32(resultDt[i].Rows[perPage - 1][0]);

                }
                
             }
            else if (authRadioButton1.Checked)
            {
                DataRow[] rows = null;
                rows = Bm_Main.ACS1AccessLvl.Select("DESCRIPTION = '"+comboAuth1.Text+"'");
                string authcode ="0";
                Dictionary<string, string> right = new Dictionary<string, string>();

                if (rows.Length > 0)
                {
                    right.Add("right1", "" + rows[0]["ID"]);
                }
                else
                {
                    if (comboAuth1.Text.Equals("전체")) right["right1"] = "";
                    else
                    {
                        MessageBox.Show("권한 조회 오류.");
                        return;
                    }
                }

                rows = Bm_Main.ACS2AccessLvl.Select("DESCRIPTION = '" + comboAuth2.Text + "'");
                if (rows.Length > 0)
                {
                    right.Add("right2", "" + rows[0]["ID"]);
                }
                else
                {
                    if (comboAuth2.Text.Equals("전체")) right["right2"] = "";
                    else
                    {
                        MessageBox.Show("권한 조회 오류.");
                        return;
                    }
                }

                rows = Bm_Main.ACS3AccessLvl.Select("DESCRIPTION = '" + comboAuth3.Text + "'");
                if (rows.Length > 0)
                {
                    right.Add("right3", "" + rows[0]["ID"]);
                }
                else
                {
                    if (comboAuth3.Text.Equals("전체")) right["right3"] = "";
                    else
                    {
                        MessageBox.Show("권한 조회 오류.");
                        return;
                    }
                }

                rows = Bm_Main.ACS4AccessLvl.Select("DESCRIPTION = '" + comboAuth4.Text + "'");
                if (rows.Length > 0)
                {
                    right.Add("right4", "" + rows[0]["ID"]);
                }
                else
                {
                    if (comboAuth4.Text.Equals("전체")) right["right4"] = "";
                    else
                    {
                        MessageBox.Show("권한 조회 오류.");
                        return;
                    }
                }

                right["STAT"] = comboCardStat.Text;
                
                rCheck = req.select("BMS019", qm.getQuery("Rpt_001_AUTH_Count", right), "BMS");


                if (rCheck.Equals("0"))
                {
                    dt = ReturnDT.dt;


                    if (checkResult() == 1) return;


                    MessageBox.Show("총 " + Convert.ToInt32(dt.Rows[0][0]) + "건이 검색되었습니다");

                    totalPage = (int)(Convert.ToInt32(dt.Rows[0][0]) / perPage);
                    totalPage = (Convert.ToInt32(dt.Rows[0][0]) % perPage == 0) ? totalPage : totalPage += 1;

                    resultDt = new DataTable[totalPage];


                }
                else
                {
                    MessageBox.Show("데이터를 불러오는데 실패했습니다. 다시 시도해주세요");
                }
                right.Add("lastSEQ", "0");
                for (int i = 0; i < totalPage; i++)
                {
                    rCheck = req.select("BMS019", qm.getQuery("Rpt_001_AUTH",right), "BMS");
                    resultDt[i] = ReturnDT.dt;
                    if (resultDt[i].Rows.Count == perPage) right["lastSEQ"] = "" + resultDt[i].Rows[perPage - 1][0];

                }

            }
           
           
            


          


            if (rCheck.Equals("0"))
            {
                dt = ReturnDT.dt;

            }
            else
            {
                MessageBox.Show("요청중에 네트워크 장애가 발생하였습니다. 다시 시도해주세요.");
            }

            string[] str = new string[1];
            DataRow[] row;
            for (int k = 0; k < resultDt.Length; k++)
            {
                for (int i = 0; i < resultDt[k].Rows.Count; i++)
                {
                    str = ("" + resultDt[k].Rows[i]["1발권한"]).Split('~');
                    if (str.Length > 1)
                    {
                        string right = "";
                        for (int j = 0; j < str.Length; j++)
                        {
                            
                            try
                            {
                                row = Bm_Main.ACS1AccessLvl.Select("ID = " + str[j]);
                                str[j] = "" + row[0]["DESCRIPTION"];
                                 right +=","+ str[j] ;
                            }
                            catch (Exception e)
                            {
                                resultDt[k].Rows[i]["1발권한"] = "권한미확인";
                            }
                            
                        }
                        resultDt[k].Rows[i]["1발권한"] = right.Remove(0,1);
                    }
                    else
                    {
                        str[0] = ("" + resultDt[k].Rows[i]["1발권한"]);
                        if (str[0].Equals("-100")) resultDt[k].Rows[i]["1발권한"] = "권한없음";
                        else
                        {
                            try
                            {
                                row = Bm_Main.ACS1AccessLvl.Select("ID = " + str[0]);
                                str[0] = "" + row[0]["DESCRIPTION"];
                                resultDt[k].Rows[i]["1발권한"] = str[0];
                            }
                            catch (Exception e)
                            {
                                resultDt[k].Rows[i]["1발권한"] = "권한미확인";
                            }
                        }
                    }


                    str = ("" + resultDt[k].Rows[i]["2발권한"]).Split('~');
                    if (str.Length > 1)
                    {
                        string right = "";
                        for (int j = 0; j < str.Length; j++)
                        {

                            try
                            {
                                row = Bm_Main.ACS2AccessLvl.Select("ID = " + str[j]);
                                str[j] = "" + row[0]["DESCRIPTION"];
                                right += "," + str[j];
                            }
                            catch (Exception e)
                            {
                                resultDt[k].Rows[i]["2발권한"] = "권한미확인";
                            }

                        }
                        resultDt[k].Rows[i]["2발권한"] = right.Remove(0, 1);
                    }
                    else
                    {
                        str[0] = ("" + resultDt[k].Rows[i]["2발권한"]);
                        if (str[0].Equals("-100")) resultDt[k].Rows[i]["2발권한"] = "권한없음";
                        else
                        {
                            try
                            {
                                row = Bm_Main.ACS2AccessLvl.Select("ID = " + str[0]);
                                str[0] = "" + row[0]["DESCRIPTION"];
                                resultDt[k].Rows[i]["2발권한"] = str[0];
                            }
                            catch (Exception e)
                            {
                                resultDt[k].Rows[i]["2발권한"] = "권한미확인";
                            }
                        }
                    }

                    str = ("" + resultDt[k].Rows[i]["3발권한"]).Split('~');
                    if (str.Length > 1)
                    {
                        string right = "";
                        for (int j = 0; j < str.Length; j++)
                        {

                            try
                            {
                                row = Bm_Main.ACS3AccessLvl.Select("ID = " + str[j]);
                                str[j] = "" + row[0]["DESCRIPTION"];
                                right += "," + str[j];
                            }
                            catch (Exception e)
                            {
                                resultDt[k].Rows[i]["3발권한"] = "권한미확인";
                            }

                        }
                        resultDt[k].Rows[i]["3발권한"] = right.Remove(0, 1);
                    }
                    else
                    {
                        str[0] = ("" + resultDt[k].Rows[i]["3발권한"]);
                        if (str[0].Equals("-100")) resultDt[k].Rows[i]["3발권한"] = "권한없음";
                        else
                        {
                            try
                            {
                                row = Bm_Main.ACS3AccessLvl.Select("ID = " + str[0]);
                                str[0] = "" + row[0]["DESCRIPTION"];
                                resultDt[k].Rows[i]["3발권한"] = str[0];
                            }
                            catch (Exception e)
                            {
                                resultDt[k].Rows[i]["3발권한"] = "권한미확인";
                            }
                        }
                    }

                   


                }
            }




                rowNum = dt.Rows.Count;
            cNum = dt.Columns.Count;
            currentPage = 0;
            labelBefore.Visible = false;
            labelNext.Visible = true;
            ReportGridView.DataSource = resultDt[0];
            labelPage.Visible = true;
            labelPage.Text = (currentPage +1) + " / " + totalPage;

            if (currentPage == totalPage - 1)
            {

                labelNext.Visible = false;

            }
            

       

        }





        public void makeCell(DataGridView _dgv, int rNum, int cNum)
        {
            topCell = new DataGridViewTextBoxColumn[rNum][];


            for (int k2 = 0; k2 < rNum; k2++)
            {

                topCell[k2] = new DataGridViewTextBoxColumn[cNum];
                for (int j2 = 0; j2 < cNum; j2++) this.topCell[k2][j2] = new System.Windows.Forms.DataGridViewTextBoxColumn();
            }


            for (int jj2 = 0; jj2 < rNum; jj2++)
            {
                if (cNum == 1)
                {
                    _dgv.Rows.Add(new System.Windows.Forms.DataGridViewColumn[] {
                    this.topCell[jj2][0] });
                }
                else if (cNum == 2)
                {
                    _dgv.Rows.Add(new System.Windows.Forms.DataGridViewColumn[] {
                    this.topCell[jj2][0],
                    this.topCell[jj2][1] });
                }
                else if (cNum == 3)
                {
                    _dgv.Rows.Add(new System.Windows.Forms.DataGridViewColumn[] {
                    this.topCell[jj2][0],
                    this.topCell[jj2][1],
                    this.topCell[jj2][2] });
                }
                else if (cNum == 4)
                {
                    _dgv.Rows.Add(new System.Windows.Forms.DataGridViewColumn[] {
                    this.topCell[jj2][0],
                    this.topCell[jj2][1],
                    this.topCell[jj2][2],
                    this.topCell[jj2][3] });
                }
                else if (cNum == 5)
                {
                    _dgv.Rows.Add(new System.Windows.Forms.DataGridViewColumn[] {
                    this.topCell[jj2][0],
                    this.topCell[jj2][1],
                    this.topCell[jj2][2],
                    this.topCell[jj2][3],
                    this.topCell[jj2][4] });
                }
                else if (cNum == 6)
                {
                    _dgv.Rows.Add(new System.Windows.Forms.DataGridViewColumn[] {
                    this.topCell[jj2][0],
                    this.topCell[jj2][1],
                    this.topCell[jj2][2],
                    this.topCell[jj2][3],
                    this.topCell[jj2][4],
                    this.topCell[jj2][5] });
                }
                else if (cNum == 7)
                {
                    _dgv.Rows.Add(new System.Windows.Forms.DataGridViewColumn[] {
                    this.topCell[jj2][0],
                    this.topCell[jj2][1],
                    this.topCell[jj2][2],
                    this.topCell[jj2][3],
                    this.topCell[jj2][4],
                    this.topCell[jj2][5],
                    this.topCell[jj2][6] });
                }
                else if (cNum == 8)
                {
                    _dgv.Rows.Add(new System.Windows.Forms.DataGridViewColumn[] {
                    this.topCell[jj2][0],
                    this.topCell[jj2][1],
                    this.topCell[jj2][2],
                    this.topCell[jj2][3],
                    this.topCell[jj2][4],
                    this.topCell[jj2][5],
                    this.topCell[jj2][6],
                    this.topCell[jj2][7] });
                }
                else if (cNum == 9)
                {
                    _dgv.Rows.Add(new System.Windows.Forms.DataGridViewColumn[] {
                    this.topCell[jj2][0],
                    this.topCell[jj2][1],
                    this.topCell[jj2][2],
                    this.topCell[jj2][3],
                    this.topCell[jj2][4],
                    this.topCell[jj2][5],
                    this.topCell[jj2][6],
                    this.topCell[jj2][7],
                    this.topCell[jj2][8] });
                }
                else if (cNum == 10)
                {
                    _dgv.Rows.Add(new System.Windows.Forms.DataGridViewColumn[] {
                    this.topCell[jj2][0],
                    this.topCell[jj2][1],
                    this.topCell[jj2][2],
                    this.topCell[jj2][3],
                    this.topCell[jj2][4],
                    this.topCell[jj2][5],
                    this.topCell[jj2][6],
                    this.topCell[jj2][7],
                    this.topCell[jj2][8],
                    this.topCell[jj2][9] });
                }
                else if (cNum == 11)
                {
                    _dgv.Rows.Add(new System.Windows.Forms.DataGridViewColumn[] {
                    this.topCell[jj2][0],
                    this.topCell[jj2][1],
                    this.topCell[jj2][2],
                    this.topCell[jj2][3],
                    this.topCell[jj2][4],
                    this.topCell[jj2][5],
                    this.topCell[jj2][6],
                    this.topCell[jj2][7],
                    this.topCell[jj2][8],
                    this.topCell[jj2][9],
                    this.topCell[jj2][10] });
                }
                else if (cNum == 12)
                {
                    _dgv.Rows.Add(new System.Windows.Forms.DataGridViewColumn[] {
                    this.topCell[jj2][0],
                    this.topCell[jj2][1],
                    this.topCell[jj2][2],
                    this.topCell[jj2][3],
                    this.topCell[jj2][4],
                    this.topCell[jj2][5],
                    this.topCell[jj2][6],
                    this.topCell[jj2][7],
                    this.topCell[jj2][8],
                    this.topCell[jj2][9],
                    this.topCell[jj2][10],
                    this.topCell[jj2][11] });
                }
                else if (cNum == 13)
                {
                    _dgv.Rows.Add(new System.Windows.Forms.DataGridViewColumn[] {
                    this.topCell[jj2][0],
                    this.topCell[jj2][1],
                    this.topCell[jj2][2],
                    this.topCell[jj2][3],
                    this.topCell[jj2][4],
                    this.topCell[jj2][5],
                    this.topCell[jj2][6],
                    this.topCell[jj2][7],
                    this.topCell[jj2][8],
                    this.topCell[jj2][9],
                    this.topCell[jj2][10],
                    this.topCell[jj2][11],
                    this.topCell[jj2][12] });
                }
                else if (cNum == 14)
                {
                    _dgv.Rows.Add(new System.Windows.Forms.DataGridViewColumn[] {
                    this.topCell[jj2][0],
                    this.topCell[jj2][1],
                    this.topCell[jj2][2],
                    this.topCell[jj2][3],
                    this.topCell[jj2][4],
                    this.topCell[jj2][5],
                    this.topCell[jj2][6],
                    this.topCell[jj2][7],
                    this.topCell[jj2][8],
                    this.topCell[jj2][9],
                    this.topCell[jj2][10],
                    this.topCell[jj2][11],
                    this.topCell[jj2][12],
                    this.topCell[jj2][13] });
                }
                else if (cNum == 15)
                {
                    _dgv.Rows.Add(new System.Windows.Forms.DataGridViewColumn[] {
                    this.topCell[jj2][0],
                    this.topCell[jj2][1],
                    this.topCell[jj2][2],
                    this.topCell[jj2][3],
                    this.topCell[jj2][4],
                    this.topCell[jj2][5],
                    this.topCell[jj2][6],
                    this.topCell[jj2][7],
                    this.topCell[jj2][8],
                    this.topCell[jj2][9],
                    this.topCell[jj2][10],
                    this.topCell[jj2][11],
                    this.topCell[jj2][12],
                    this.topCell[jj2][13],
                    this.topCell[jj2][14] });
                }
                else if (cNum == 16)
                {
                    _dgv.Rows.Add(new System.Windows.Forms.DataGridViewColumn[] {
                    this.topCell[jj2][0],
                    this.topCell[jj2][1],
                    this.topCell[jj2][2],
                    this.topCell[jj2][3],
                    this.topCell[jj2][4],
                    this.topCell[jj2][5],
                    this.topCell[jj2][6],
                    this.topCell[jj2][7],
                    this.topCell[jj2][8],
                    this.topCell[jj2][9],
                    this.topCell[jj2][10],
                    this.topCell[jj2][11],
                    this.topCell[jj2][12],
                    this.topCell[jj2][13],
                    this.topCell[jj2][14],
                    this.topCell[jj2][15]
                    });
                }
            }

        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (comboDepartment.Text.Equals("") && comboDivision.Text.Equals("") && comboIssueReson.Text.Equals("") && comboIssueType.Text.Equals("") && comboRegType.Text.Equals("") && txtCardNum.Text.Equals("") && txtSSNO.Text.Equals("") && !authRadioButton1.Checked &&!radioDate.Checked &&!radioName.Checked )
            {
                MessageBox.Show("검색조건을 정확히 입력하세요 ");
                return;
            }

  
            showData();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

            currentPage++;
            ReportGridView.DataSource = resultDt[currentPage];
            labelPage.Text = (currentPage + 1) + " / " + totalPage; 
            labelBefore.Visible = true;
            if (currentPage == totalPage-1) { 
                
                labelNext.Visible= false;
                
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

            currentPage--;
            ReportGridView.DataSource = resultDt[currentPage];
            labelPage.Text = (currentPage + 1) + " / " + totalPage; 
            
            labelNext.Visible = true;
            if (currentPage < 1) { 
                labelBefore.Visible =false;
                
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            ArrayList list = new ArrayList();

            for (int i = 0; i < resultDt.Length; i++)
            {
                list.Add(resultDt[i]);
            }
            SendDataToExcel(list, "출입정보이력", resultDt[0].Columns.Count, resultDt[0].Rows.Count);
        }

        public void SendDataToExcel(ArrayList list, string sheetName, int columnsCnt, int rowCnt)
        {
            Cursor.Current = Cursors.WaitCursor;
            Excel._Application app = new Excel.Application();
            Excel.Workbook workbook;
            Excel.Worksheet worksheet;

            workbook = app.Workbooks.Add(Type.Missing);

             app.Visible = false;

             worksheet = (Excel.Worksheet)workbook.Sheets["Sheet1"];
             worksheet = (Excel.Worksheet)workbook.ActiveSheet;

            worksheet.Name = sheetName;
            int colIndex = 0;

            //// storing header part in Excel   
            //for (int i = 1; i < dgv.Columns.Count + 1; i++)  
            //{  
            //    if (dgv.Columns[i - 1].Visible == true)  
            //    {  
            //        colIndex += 1;  
            //        worksheet.Cells[1, colIndex] = dgv.Columns[i - 1].HeaderText;  
            //    }               
            //}  
            //// storing Each row and column value to excel sheet  
            //for (int i = 0; i < dgv.Rows.Count - 1; i++)  
            //{  
            //    colIndex = 0;  
            //    for (int j = 0; j < dgv.Columns.Count; j++)  
            //    {  
            //        if (dgv.Columns[j].Visible == true)  
            //        {  
            //            colIndex += 1;  
            //            worksheet.Cells[i + 2, colIndex] = dgv.Rows[i].Cells[j].Value == null ? "" : dgv.Rows[i].Cells[j].Value.ToString() ?? "";  
            //        }  
            //    }  
            //}  
            worksheet.Cells.NumberFormat = "@";
            // storing header part in Excel  
            DataTable table = (DataTable)list[0];
            for (int i = 1; i <= columnsCnt; i++)
            {
                    colIndex += 1;
                    worksheet.Cells[1, colIndex] = table.Columns[i - 1].ColumnName;
            }
            // storing Each row and column value to excel sheet  
            int currentCell = 0;
            for(int k = 0; k < list.Count;k++){

                table = (DataTable)list[k];
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    colIndex = 0;
                    for (int j = 0; j < columnsCnt; j++)
                    {
                        
                            colIndex += 1;
                            
                            if (i == 0 && j == 2)
                            {
                                worksheet.Cells[currentCell + 2, colIndex] = table.Rows[0][4] == null ? "" : table.Rows[0][4].ToString() ?? "";
                            }
                            else
                            {
                                worksheet.Cells[currentCell + 2, colIndex] = table.Rows[i][j] == null ? "" : table.Rows[i][j].ToString() ?? "";
                            }
                            
                        
                    }
                    currentCell++;
                }
            }

            Excel.Range usedRange;
            usedRange = worksheet.UsedRange;

            Excel.Range rangeStart = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[usedRange.Row, usedRange.Column];
            Excel.Range rangeEnd = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[usedRange.Row + usedRange.Rows.Count, usedRange.Column + usedRange.Columns.Count];


          
           

            //// BackColor 설정  
            //((Excel.Range)excelWorksheet.get_Range("A1", "J1")).Interior.Color = ColorTranslator.ToOle(Color.Navy);  
            //((Excel.Range)excelWorksheet.get_Range("A2", "J2")).Interior.Color = ColorTranslator.ToOle(Color.RoyalBlue);  

            //// Font Color 설정  
            //((Excel.Range)excelWorksheet.get_Range("A1", "J1")).Font.Color = ColorTranslator.ToOle(Color.White);  
            //((Excel.Range)excelWorksheet.get_Range("A2", "J2")).Font.Color = ColorTranslator.ToOle(Color.White);  
            

            // 상단 첫번째 Row 타이틀 볼드  
            SetHeaderBold(worksheet, 1);

            // 자동 넓이 지정  
            for (int i = 0; i < usedRange.Columns.Count; i++)
            {
                AutoFitColumn(worksheet, i + 1);
            }

            // 타이틀 색상  
            for (int i = 0; i < usedRange.Columns.Count; i++)
            {
                //worksheet.Cells[1, i+1].Interior.Color = Excel.XlRgbColor.rgbGrey;  
                ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1]).Interior.ColorIndex = 15;
            }


            // 선그리기  
            usedRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            usedRange.Borders.ColorIndex = 1;

            // 정렬  
            usedRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            //차트화할 셀의영역  
            //     Excel.Range chartArea = worksheet.get_Range("A1", "B30");

            //차트생성  
            //     Excel.ChartObjects chart = (Excel.ChartObjects)worksheet.ChartObjects(Type.Missing);

            //차트위치  
            //     Excel.ChartObject mychart = (Excel.ChartObject)chart.Add(100, 40, 800, 400);

            //차트 할당  
            //     Excel.Chart chartPage = mychart.Chart;

            //차트의 데이터 셋팅  
            //     chartPage.SetSourceData(chartArea, Excel.XlRowCol.xlColumns);

            //차트의 형태  
            //chartPage.ChartType = Excel.XlChartType.xlCylinderColClustered;  
            //      chartPage.ChartType = Excel.XlChartType.xlColumnClustered;
            //      app.DisplayAlerts = false;
            app.Visible = true;
            // 작업관리자 프로세스 해제  
            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(app);

            Cursor.Current = Cursors.Default;
        }

        public void SetHeaderBold(Excel.Worksheet worksheet, int row)
        {
            ((Excel.Range)worksheet.Cells[row, 1]).EntireRow.Font.Bold = true;
        }

        //Here is how to set the column Width  
        public void SetColumnWidth(Excel.Worksheet worksheet, int col, int width)
        {
            ((Excel.Range)worksheet.Cells[1, col]).EntireColumn.ColumnWidth = width;
        }

        // Apply the setting so that it would autofit to contents  
        public void AutoFitColumn(Excel.Worksheet worksheet, int col)
        {
            ((Excel.Range)worksheet.Cells[1, col]).EntireColumn.AutoFit();
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {

        }
    }
}
