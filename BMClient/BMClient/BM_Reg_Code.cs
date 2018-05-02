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
    public partial class BM_Reg_Code : Form
    {

        string type;
        string bmQuery = "";
        string ppQuery = "";
        string OGQuery = "";
        string FPQuery = "";
        Request req = new Request();
        string nowDate = "";

        string set_Path = Application.StartupPath + @"\Hub_Setting.ini";


        public BM_Reg_Code(string _type)
        {
            InitializeComponent();
            type = _type;
            topLabel.Text = "        " + type + " 추가";
            label1.Text = "신규등록하실 " + type+"을(를) 입력하세요";
            button1.Text = "신규등록";
            nowDate = getDate(DateTime.Now);
            
        }

        private void BM_Reg_Code_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                insertCode();
            }
        }

        public void insertCode()
        {
            Cursor = Cursors.WaitCursor;
            if(type.Equals("소속")){
                string min = HubIniFile.GetIniValue("DEPT", "MINID", set_Path);
                string max = HubIniFile.GetIniValue("DEPT", "MAXID", set_Path);
                bmQuery = "insert into DEPARTMENT(ID,DESCRIPTION) values((select MAX(ID)+1 from DEPARTMENT where ID < 20000 ),'" + textBox1.Text + "')";
                OGQuery = "insert into DEPT(ID,NAME,SEGMENTID) values((select MAX(ID)+1 from DEPT where ID >= " + min + " and ID <= " + max + " ),'" + textBox1.Text + "',-1)";
                FPQuery = "insert into TB_USER_DEPT(sName,sDepartment,nDepth,nParentIdn) values('" + textBox1.Text + "','" + textBox1.Text + "',0,0)";

                string rCheck = req.update("BMS012", bmQuery, "BMI");


               

                if (rCheck.Equals("0"))
                {

                    string serverType1 = "" + Bm_Main.serverInfo.Rows[0]["SERVER_TYPE"];
                    if (serverType1.Equals("3"))
                    {
                        rCheck = req.update("BMS012", OGQuery, "ACS1I");
                        if (!rCheck.Equals("0"))
                        {
                            MessageBox.Show(type + " 추가중에 오류가 발생하였습니다.(3발전소 등록실패)");
                        }
                    }

                    string serverType2 = "" + Bm_Main.serverInfo.Rows[1]["SERVER_TYPE"];

                    if (serverType2.Equals("3"))
                    {
                        rCheck = req.update("BMS012", OGQuery, "ACS2I");
                        if (!rCheck.Equals("0"))
                        {
                            MessageBox.Show(type + " 추가중에 오류가 발생하였습니다.(3발전소 등록실패)");
                        }
                    }

                    string serverType3 = "" + Bm_Main.serverInfo.Rows[2]["SERVER_TYPE"];

                    if (serverType3.Equals("3"))
                    {
                        rCheck = req.update("BMS012", OGQuery, "ACS3I");
                        if (!rCheck.Equals("0"))
                        {
                            MessageBox.Show(type + " 추가중에 오류가 발생하였습니다.(3발전소 등록실패)");
                        }
                    }

                    string serverType4 = "" + Bm_Main.serverInfo.Rows[2]["SERVER_TYPE"];

                    if (serverType4.Equals("3"))
                    {
                        rCheck = req.update("BMS012", OGQuery, "ACS4I");
                        if (!rCheck.Equals("0"))
                        {
                            MessageBox.Show(type + " 추가중에 오류가 발생하였습니다.(3발전소 등록실패)");
                        }
                    }


                    
                    rCheck = req.update("BMS012", FPQuery, "FP1I");
                    if (!rCheck.Equals("0"))
                    {
                        MessageBox.Show(type + " 추가중에 오류가 발생하였습니다.(지문서버 등록실패)");
                    }

                    int ACS1Code=99999;
                    string selectQuery = "select top 1 ID from DEPT where NAME ='" + textBox1.Text + "'";


                    if (serverType1.Equals("3"))
                    {
                        
                        rCheck = req.select("BMS012", selectQuery, "ACS1S");

                        ACS1Code = Convert.ToInt32(ReturnDT.dt.Rows[0]["ID"]);

                    }

                    int ACS2Code=99999;

                    if (serverType2.Equals("3"))
                    {
                        rCheck = req.select("BMS012", selectQuery, "ACS2S");
                        ACS2Code = Convert.ToInt32(ReturnDT.dt.Rows[0]["ID"]);
                    }

                    int ACS3Code = 99999;

                    if (serverType3.Equals("3"))
                    {
                        rCheck = req.select("BMS012", selectQuery, "ACS3S");
                        ACS3Code = Convert.ToInt32(ReturnDT.dt.Rows[0]["ID"]);
                    }


                    int ACS4Code = 99999;

                    if (serverType4.Equals("3"))
                    {
                        rCheck = req.select("BMS012", selectQuery, "ACS3S");
                        ACS4Code = Convert.ToInt32(ReturnDT.dt.Rows[0]["ID"]);
                    }

                     

                    selectQuery = "select top 1 nDepartmentIdn from TB_USER_DEPT where sName = '" + textBox1.Text + "'";

                    rCheck = req.select("BMS012", selectQuery, "FP1S");

                    int FP1Code = Convert.ToInt32(ReturnDT.dt.Rows[0]["nDepartmentIdn"]);

                    

                    string BMUpdateQuery = "update DEPARTMENT set PLANT1_DEPT_CODE = "+ACS1Code
                        +" ,PLANT2_DEPT_CODE = "+ACS2Code
                        +" , FP1_DEPT_CODE = "+FP1Code
                        + ", PLANT3_DEPT_CODE =" + ACS3Code + " , PLANT4_DEPT_CODE =" + ACS4Code + " where DESCRIPTION ='" + textBox1.Text + "'";

                    rCheck = req.update("BMS012", BMUpdateQuery, "BMI");

                    if (rCheck.Equals("0"))
                    {
                        MessageBox.Show(type + " 추가 완료!");
                    }
                    else
                    {
                        MessageBox.Show(type + " 추가 실패..");
                    }

                }
                else
                {
                    MessageBox.Show(type+" 추가중에 오류가 발생하였습니다.");
                }


            }else if(type.Equals("부서")){
                string min = HubIniFile.GetIniValue("DIVISION", "MINID", set_Path);
                string max = HubIniFile.GetIniValue("DIVISION", "MAXID", set_Path);

                bmQuery = "insert into DIVISION(ID,DESCRIPTION) values((select MAX(ID)+1 from DIVISION where ID < 20000),'" + textBox1.Text + "')";
                OGQuery = "insert into DIVISION(ID,NAME,SEGMENTID) values((select MAX(ID)+1 from DIVISION where ID >= " + min + " and ID <= " + max + " ),'" + textBox1.Text + "',-1)";
                ppQuery = "insert into department(description,facility,modify_date,modify_time) values('" + textBox1.Text + "',-1,"+nowDate+",0)";

                string serverType1 = "" + Bm_Main.serverInfo.Rows[0]["SERVER_TYPE"];
                string serverType2 = "" + Bm_Main.serverInfo.Rows[1]["SERVER_TYPE"];
                string serverType3 = "" + Bm_Main.serverInfo.Rows[2]["SERVER_TYPE"];
                string serverType4 = "" + Bm_Main.serverInfo.Rows[2]["SERVER_TYPE"];


                string rCheck = req.update("BMS012", bmQuery, "BMI");

                if (rCheck.Equals("0"))
                {
                    if (serverType1.Equals("3"))
                    {

                        rCheck = req.update("BMS012", OGQuery, "ACS1I");
                        if (!rCheck.Equals("0"))
                        {
                            MessageBox.Show(type + " 추가중에 오류가 발생하였습니다.(1발전소 등록실패)");
                        }

                    }
                    else
                    {
                        rCheck = req.update("BMS012", ppQuery, "ACS1I");
                        if (!rCheck.Equals("0"))
                        {
                            MessageBox.Show(type + " 추가중에 오류가 발생하였습니다.(1발전소 등록실패)");
                        }
                    }

                    if (serverType2.Equals("3"))
                    {

                        rCheck = req.update("BMS012", OGQuery, "ACS2I");
                        if (!rCheck.Equals("0"))
                        {
                            MessageBox.Show(type + " 추가중에 오류가 발생하였습니다.(2발전소 등록실패)");
                        }
                    }
                    else
                    {
                        rCheck = req.update("BMS012", ppQuery, "ACS2I");
                        if (!rCheck.Equals("0"))
                        {
                            MessageBox.Show(type + " 추가중에 오류가 발생하였습니다.(1발전소 등록실패)");
                        }
                    }



                    if (serverType3.Equals("3"))
                    {
                        rCheck = req.update("BMS012", OGQuery, "ACS3I");
                        if (!rCheck.Equals("0"))
                        {
                            MessageBox.Show(type + " 추가중에 오류가 발생하였습니다.(3발전소 등록실패)");
                        }

                    }
                    else
                    {
                        rCheck = req.update("BMS012", ppQuery, "ACS3I");
                        if (!rCheck.Equals("0"))
                        {
                            MessageBox.Show(type + " 추가중에 오류가 발생하였습니다.(3발전소 등록실패)");
                        }
                    }


                    if (serverType4.Equals("3"))
                    {
                        rCheck = req.update("BMS012", OGQuery, "ACS4I");
                        if (!rCheck.Equals("0"))
                        {
                            MessageBox.Show(type + " 추가중에 오류가 발생하였습니다.(4발전소 등록실패)");
                        }

                    }
                    else
                    {
                        rCheck = req.update("BMS012", ppQuery, "ACS4I");
                        if (!rCheck.Equals("0"))
                        {
                            MessageBox.Show(type + " 추가중에 오류가 발생하였습니다.(4발전소 등록실패)");
                        }
                    }
                    
               


                    string selectQuery = "select top 1 ID from DIVISION where NAME ='" + textBox1.Text + "'";
                    selectQuery = "select first 1 id from department where description = '" + textBox1.Text + "'";


                    if(serverType1.Equals("3")){
                        selectQuery = "select top 1 ID from DIVISION where NAME ='" + textBox1.Text + "'";
                    }
                    else{
                        selectQuery = "select first 1 id from department where description = '" + textBox1.Text + "'";
                    }

                    


                    rCheck = req.select("BMS012", selectQuery, "ACS1S");

                    int ACS1Code = Convert.ToInt32(ReturnDT.dt.Rows[0]["ID"]);

                    if (serverType2.Equals("3"))
                    {
                        selectQuery = "select top 1 ID from DIVISION where NAME ='" + textBox1.Text + "'";
                    }
                    else
                    {
                        selectQuery = "select first 1 id from department where description = '" + textBox1.Text + "'";
                    }


                    rCheck = req.select("BMS012", selectQuery, "ACS2S");

                    int ACS2Code = Convert.ToInt32(ReturnDT.dt.Rows[0]["ID"]);

                    selectQuery = "select first 1 id from department where description = '" + textBox1.Text + "'";


                    if (serverType3.Equals("3"))
                    {
                        selectQuery = "select top 1 ID from DIVISION where NAME ='" + textBox1.Text + "'";
                    }
                    else
                    {
                        selectQuery = "select first 1 id from department where description = '" + textBox1.Text + "'";
                    }

                    rCheck = req.select("BMS012", selectQuery, "ACS3S");

                    int ACS3Code = Convert.ToInt32(ReturnDT.dt.Rows[0]["id"]);

                    if (serverType3.Equals("4"))
                    {
                        selectQuery = "select top 1 ID from DIVISION where NAME ='" + textBox1.Text + "'";
                    }
                    else
                    {
                        selectQuery = "select first 1 id from department where description = '" + textBox1.Text + "'";
                    }

                    rCheck = req.select("BMS012", selectQuery, "ACS4S");

                    int ACS4Code = Convert.ToInt32(ReturnDT.dt.Rows[0]["id"]);

                    

                    string BMUpdateQuery = "update DIVISION set PLANT1_DIVISION_CODE = " + ACS1Code
                        + " ,PLANT2_DIVISION_CODE = " + ACS2Code
                        + " , PLANT3_DIVISION_CODE = " + ACS3Code + " , PLANT4_DIVISION_CODE = " + ACS4Code
                        + " where DESCRIPTION ='" + textBox1.Text + "'";

                    rCheck = req.update("BMS012", BMUpdateQuery, "BMI");

                    if (rCheck.Equals("0"))
                    {
                        MessageBox.Show(type + " 추가 완료!");
                    }
                    else
                    {
                        MessageBox.Show(type + " 추가 실패..");
                    }

                }
                else
                {
                    MessageBox.Show(type + " 추가중에 오류가 발생하였습니다.");
                }

            }
            else if (type.Equals("직위"))
            {


                string min = HubIniFile.GetIniValue("TITLE", "MINID", set_Path);
                string max = HubIniFile.GetIniValue("TITLE", "MAXID", set_Path);

                bmQuery = "insert into TITLE(ID,DESCRIPTION) values((select MAX(ID)+1 from TITLE where ID < 20000),'" + textBox1.Text + "')";
                OGQuery = "insert into TITLE(ID,NAME,SEGMENTID) values((select MAX(ID)+1 from TITLE where ID >= " + min + " and ID <= " + max + " ),'" + textBox1.Text + "',-1)";
                string rCheck = req.update("BMS012", bmQuery, "BMI");
                if (rCheck.Equals("0"))
                {
                    string serverType1 = "" + Bm_Main.serverInfo.Rows[0]["SERVER_TYPE"];
                    string serverType2 = "" + Bm_Main.serverInfo.Rows[1]["SERVER_TYPE"];
                    string serverType3 = "" + Bm_Main.serverInfo.Rows[2]["SERVER_TYPE"];
                    string serverType4 = "" + Bm_Main.serverInfo.Rows[3]["SERVER_TYPE"];

                    if (serverType1.Equals("3"))
                    {

                        rCheck = req.update("BMS012", OGQuery, "ACS1I");
                        if (!rCheck.Equals("0"))
                        {
                            MessageBox.Show(type + " 추가중에 오류가 발생하였습니다.(1발전소 등록실패)");
                        }

                    }

                    if (serverType2.Equals("3"))
                    {
                        rCheck = req.update("BMS012", OGQuery, "ACS2I");
                        if (!rCheck.Equals("0"))
                        {
                            MessageBox.Show(type + " 추가중에 오류가 발생하였습니다.(2발전소 등록실패)");
                        }

                    }

                    if (serverType3.Equals("3"))
                    {
                        rCheck = req.update("BMS012", OGQuery, "ACS3I");
                        if (!rCheck.Equals("0"))
                        {
                            MessageBox.Show(type + " 추가중에 오류가 발생하였습니다.(2발전소 등록실패)");
                        }

                    }

                    if (serverType4.Equals("3"))
                    {
                        rCheck = req.update("BMS012", OGQuery, "ACS3I");
                        if (!rCheck.Equals("0"))
                        {
                            MessageBox.Show(type + " 추가중에 오류가 발생하였습니다.(2발전소 등록실패)");
                        }

                    }

                    string selectQuery = "select top 1 ID from TITLE where NAME ='" + textBox1.Text + "'";
                    int ACS1Code = 99999;
                    int ACS2Code = 99999;
                    int ACS3Code = 99999;
                    int ACS4Code = 99999;

                    if (serverType1.Equals("3"))
                    {
                        rCheck = req.select("BMS012", selectQuery, "ACS1S");
                        ACS1Code = Convert.ToInt32(ReturnDT.dt.Rows[0]["ID"]);
                    }


                    if (serverType2.Equals("3"))
                    {
                        rCheck = req.select("BMS012", selectQuery, "ACS2S");
                        ACS2Code = Convert.ToInt32(ReturnDT.dt.Rows[0]["ID"]);
                    }

                    if (serverType3.Equals("3"))
                    {
                        rCheck = req.select("BMS012", selectQuery, "ACS3S");
                        ACS3Code = Convert.ToInt32(ReturnDT.dt.Rows[0]["ID"]);
                    }

                    if (serverType4.Equals("3"))
                    {
                        rCheck = req.select("BMS012", selectQuery, "ACS3S");
                        ACS4Code = Convert.ToInt32(ReturnDT.dt.Rows[0]["ID"]);
                    }



                    string BMUpdateQuery = "update TITLE set PLANT1_TITLE_CODE = " + ACS1Code
                        + " ,PLANT2_TITLE_CODE = " + ACS2Code
                        + " , PLANT3_TITLE_CODE = " + ACS3Code + " , PLANT4_TITLE_CODE = " + ACS4Code
                        + " where DESCRIPTION ='" + textBox1.Text + "'";

                    rCheck = req.update("BMS012", BMUpdateQuery, "BMI");

                    if (rCheck.Equals("0"))
                    {
                        MessageBox.Show(type + " 추가 완료!");
                    }
                    else
                    {
                        MessageBox.Show(type + " 추가 실패..");
                    }

                }
                
            }
            Cursor = Cursors.Default;
        }

        public string getDate(DateTime dateTime)
        {
            string date = "";

            string month = "" + dateTime.Month;
            while (month.Length < 2) month = "0" + month;
            string day = "" + dateTime.Day;
            while (day.Length < 2) day = "0" + day;

            date = "" + dateTime.Year + month + day;
            return date;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            insertCode();
        }
    }
}
