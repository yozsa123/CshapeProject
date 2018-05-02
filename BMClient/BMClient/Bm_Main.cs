using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace BMClient
{
    public partial class Bm_Main : Form
    {

        DataTable dt;
        string delimeter = "^";



        public static DataTable serverInfo;


        public static DataTable department;
        public static DataTable division;
        public static DataTable title;
        public static DataTable cardType;
        public static DataTable cardStat;
        public static DataTable cardFormat;
        public static DataTable issueType;
        public static DataTable issueReason;

        public static DataTable ACS1AccessLvl;
        public static DataTable ACS2AccessLvl;
        public static DataTable ACS3AccessLvl;
        public static DataTable ACS4AccessLvl;


        public static DataTable UserType;
        string queryState = "S";


        QueryMaker qm = new QueryMaker();
        Request req = new Request();

        public Bm_Main()
        {
            InitializeComponent();
            new Bm_Login().ShowDialog();
            req = new Request();
            getPlantName();
            getUserType();

            getDepartment();
            getDivison();
            getCardStat();
            getCardType();
            getCardFormat();
            getIssueReason();
            getIssueType();
            getTitle();
            getAccessLvl();



            //setSystemInfo();

        }

        public void getAccessLvl()
        {
            string rCheck = req.select("BMS003", qm.getQuery("Bm_ACS1AccessLvl_R", "1", ""), "BMS");


            if (rCheck.Equals("0"))
            {

                ACS1AccessLvl = ReturnDT.dt;

            }
            else
            {
                MessageBox.Show("서버와의 통신중에 정보를 읽어오는데 실패하였습니다.");
            }

            rCheck = req.select("BMS003", qm.getQuery("Bm_ACS2AccessLvl_R", "2", ""), "BMS");


            if (rCheck.Equals("0"))
            {

                ACS2AccessLvl = ReturnDT.dt;

            }
            else
            {
                MessageBox.Show("서버와의 통신중에 정보를 읽어오는데 실패하였습니다.");
            }

            rCheck = req.select("BMS003", qm.getQuery("Bm_ACS3AccessLvl_R", "3", ""), "BMS");


            if (rCheck.Equals("0"))
            {

                ACS3AccessLvl = ReturnDT.dt;

            }
            else
            {
                MessageBox.Show("서버와의 통신중에 정보를 읽어오는데 실패하였습니다.");
            }

            rCheck = req.select("BMS003", qm.getQuery("Bm_ACS4AccessLvl_R", "4", ""), "BMS");


            if (rCheck.Equals("0"))
            {

                ACS4AccessLvl = ReturnDT.dt;

            }
            else
            {
                MessageBox.Show("서버와의 통신중에 정보를 읽어오는데 실패하였습니다.");
            }
        }

        public void getDepartment()
        {
            string rCheck = req.select("BMS003", qm.getQuery("Bm_Department_R", "", ""), "BMS");


            if (rCheck.Equals("0"))
            {

                department = ReturnDT.dt;

            }
            else
            {
                MessageBox.Show("서버와의 통신중에 정보를 읽어오는데 실패하였습니다.");
            }


        }

        public void getDivison()
        {
            string rCheck = req.select("BMS003", qm.getQuery("Bm_Division_R", "", ""), "BMS");
            if (rCheck.Equals("0"))
            {

                division = ReturnDT.dt;

            }
            else
            {
                MessageBox.Show("서버와의 통신중에 정보를 읽어오는데 실패하였습니다.");
            }



        }

        public void getTitle()
        {
            string rCheck = req.select("BMS003", qm.getQuery("Bm_Title_R", "", ""), "BMS");
            if (rCheck.Equals("0"))
            {

                title = ReturnDT.dt;

            }
            else
            {
                MessageBox.Show("서버와의 통신중에 정보를 읽어오는데 실패하였습니다.");
            }
        }



        public void getCardStat()
        {
            string rCheck = req.select("BMS003", qm.getQuery("Bm_Card_Stat_R", "", ""), "BMS");
            if (rCheck.Equals("0"))
            {

                cardStat = ReturnDT.dt;

            }
            else
            {
                MessageBox.Show("서버와의 통신중에 정보를 읽어오는데 실패하였습니다.");
            }

        }

        public void getCardType()
        {
            string rCheck = req.select("BMS003", qm.getQuery("Bm_Card_Type_R", "", ""), "BMS");
            if (rCheck.Equals("0"))
            {

                cardType = ReturnDT.dt;

            }
            else
            {
                MessageBox.Show("서버와의 통신중에 정보를 읽어오는데 실패하였습니다.");
            }


        }

        public void getCardFormat()
        {
            string rCheck = req.select("BMS003", qm.getQuery("Bm_Card_Format_R", "", ""), "BMS");
            if (rCheck.Equals("0"))
            {

                cardFormat = ReturnDT.dt;

            }
            else
            {
                MessageBox.Show("서버와의 통신중에 정보를 읽어오는데 실패하였습니다.");
            }


        }


        public void getIssueType()
        {
            string rCheck = req.select("BMS003", qm.getQuery("Bm_Issue_Type_R", "", ""), "BMS");
            if (rCheck.Equals("0"))
            {

                issueType = ReturnDT.dt;

            }
            else
            {
                MessageBox.Show("서버와의 통신중에 정보를 읽어오는데 실패하였습니다.");
            }


        }


        public void getIssueReason()
        {
            string rCheck = req.select("BMS003", qm.getQuery("Bm_Issue_Reason_R", "", ""), "BMS");
            if (rCheck.Equals("0"))
            {

                issueReason = ReturnDT.dt;

            }
            else
            {
                MessageBox.Show("서버와의 통신중에 정보를 읽어오는데 실패하였습니다.");
            }


        }




        public void getUserType()
        {

            string rCheck = req.select("BMS001", qm.getQuery("Main_UserType_S"), "BMS");
            if (rCheck.Equals("0"))
            {
                UserType = ReturnDT.dt;
            }
            else
            {
                Log.WriteLog("getUserType Fail...");
            }

        }

        public static string setPw(string pw)
        {
            byte[] bytePw = Encoding.UTF8.GetBytes(pw);
            byte[] encPw = AESClass.AESClass.AESENC(bytePw);

            string encPwStr = Convert.ToBase64String(encPw);

            return encPwStr;
        }

        public void getPlantName()
        {
            string rCheck = "";

            rCheck = req.select("BMS001", qm.getQuery("Main_ServerInfo_R"), "BMS");

            if (rCheck.Equals("0"))
            {
                dt = ReturnDT.dt;

                DataTable User_Info = new DataTable();
            }


            serverInfo = ReturnDT.dt;


        }

        private void setSystemInfo()
        {
            SystemInfo si = new SystemInfo();
            string request = req.select("BMS001", getQuery("SYSTEM"), "BMS");

        }

        private void doSomething(object sender)
        {
            Log.WriteLog("doSomething ().........................................................");
            Gloval.loading = new LoadingForm();
            Gloval.loading.ShowDialog();
        }


        private string getQuery(string type)
        {
            if (type.Equals("R"))
            {
                string selectQuery = "SELECT Server_Num, Server_Type, CASE WHEN Server_Type = '1' THEN 'PP40' WHEN Server_Type = '2' THEN 'PP45' WHEN Server_Type = '3' THEN 'OG' WHEN Server_Type = '4' THEN 'LS' END AS ServerName, "
                                   + "P_Ip, S_Ip, Server_Id, Server_Password, Db_Name, Db_Id, Db_Password "
                                   + "FROM ( "
                                   + "SELECT Server_Num, Server_Type, P_Ip, S_Ip, Server_Id, Server_Password, Db_Name, Db_Id, Db_Password "
                                   + "FROM [dbo].[SystemInfo_Master] "
                                   + "WHERE Server_Type = 1 OR Server_Type = 2 OR Server_Type = 3 OR Server_Type = 4 "
                                   + ") A "
                                   + "LEFT JOIN ( "
                                   + "SELECT MainCode, SubCode, Name FROM [dbo].[Code_Master] "
                                   + "WHERE MainCode = '0' AND SubCode != '0' "
                                   + ") B "
                                   + "ON A.Server_Type = B.SubCode ";

                return selectQuery;
            }
            else if (type.Equals("R2"))
            {
                string selectQuery = "SELECT SUBSTRING(Server_Name, 4, 1) AS Plant_Number, B.Name AS ServerName "
                                   + "FROM ( "
                                   + "SELECT Server_Name, Server_Type "
                                   + "FROM [dbo].[SystemInfo_Master] "
                                   + "WHERE Server_Type != 0 AND SUBSTRING(Server_Name, 1, 3) != 'BIO' "
                                   + ") A "
                                   + "LEFT JOIN ( "
                                   + "SELECT MainCode, SubCode, Name FROM [dbo].[Code_Master] "
                                   + "WHERE MainCode = '0' AND SubCode != '0' "
                                   + ") B "
                                   + "ON A.Server_Type = B.SubCode";

                return selectQuery;
            }
            else if (type.Equals("SYSTEM"))
            {
                string query = "SELECT * FROM [dbo].[SystemInfo_Master] WHERE Server_Type = 1 OR Server_Type = 2 OR Server_Type = 3 OR Server_Type = 4 ";

                return query;
            }
            else if (type.Equals("SYSTEM_ETC"))
            {
                string query = "SELECT * FROM [dbo].[SystemInfo_Master] WHERE Server_Type = 5 OR Server_Type = 6 ";

                return query;
            }

            return "-1";
        }

        Form currentForm = null;

        public void setForm(Form _form)
        {
            currentForm = _form;
            currentForm.TopLevel = false;
            currentForm.Location = new Point(0, 30);
            currentForm.Show();



        }
        private void mnu_Sys_001_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(currentForm);
            setForm(new Bm_Sys_001());
            this.Controls.Add(currentForm);



            /*
            Bm_Sys_001 sys001 = new Bm_Sys_001();

            if (formIsExist(sys001.GetType()))
            {
                sys001.Dispose();     // 창 리소스 제거                
            }
            else
            {
                sys001.MdiParent = this;
                sys001.Show();
            }
            */



        }

        private void mnu_Sys_002_Click(object sender, EventArgs e)
        {

            this.Controls.Remove(currentForm);
            setForm(new Bm_Sys_002());
            this.Controls.Add(currentForm);
            /*
            Bm_Sys_002 sys002 = new Bm_Sys_002();

            if (formIsExist(sys002.GetType()))
            {
                sys002.Dispose();     // 창 리소스 제거                
            }
            else
            {
                sys002.MdiParent = this;
                sys002.Show();
            }
             * */
        }

        private void mnu_Sys_003_Click(object sender, EventArgs e)
        {

            this.Controls.Remove(currentForm);
            setForm(new Bm_Sys_003());
            this.Controls.Add(currentForm);
            /*
            Bm_Sys_003 sys003 = new Bm_Sys_003();

            if (formIsExist(sys003.GetType()))
            {
                sys003.Dispose();     // 창 리소스 제거                
            }
            else
            {
                sys003.MdiParent = this;
                sys003.Show();
            }
             * */
        }

        private void mnu_Sys_004_Click(object sender, EventArgs e)
        {

            this.Controls.Remove(currentForm);
            setForm(new Bm_Sys_004());
            this.Controls.Add(currentForm);
            /*
            Bm_Sys_004 sys004 = new Bm_Sys_004();

            if (formIsExist(sys004.GetType()))
            {
                sys004.Dispose();     // 창 리소스 제거                
            }
            else
            {
                sys004.MdiParent = this;
                sys004.Show();
            }
             * 
             * */
        }

        private void mnu_Sys_005_Click(object sender, EventArgs e)
        {

            this.Controls.Remove(currentForm);
            setForm(new Bm_Sys_005());
            this.Controls.Add(currentForm);
            /*
            Bm_Sys_005 sys005 = new Bm_Sys_005();

            if (formIsExist(sys005.GetType()))
            {
                sys005.Dispose();     // 창 리소스 제거                
            }
            else
            {
                sys005.MdiParent = this;
                sys005.Show();
            }
             */
        }

        private void mnu_Sys_006_Click(object sender, EventArgs e)
        {

            this.Controls.Remove(currentForm);
            setForm(new Bm_Sys_006());
            this.Controls.Add(currentForm);
            /*
            Bm_Sys_006 sys006 = new Bm_Sys_006();

            if (formIsExist(sys006.GetType()))
            {
                sys006.Dispose();     // 창 리소스 제거                
            }
            else
            {
                sys006.MdiParent = this;
                sys006.Show();
            }
             * */
        }

        private void mnu_Sys_007_Click(object sender, EventArgs e)
        {

            this.Controls.Remove(currentForm);
            setForm(new Bm_Sys_007());
            this.Controls.Add(currentForm);
            /*
            Bm_Sys_007 sys007 = new Bm_Sys_007();

            if (formIsExist(sys007.GetType()))
            {
                sys007.Dispose();     // 창 리소스 제거                
            }
            else
            {
                sys007.MdiParent = this;
                sys007.Show();
            }
             * */
        }

        private void mnu_Sys_008_Click(object sender, EventArgs e)
        {

            this.Controls.Remove(currentForm);
            setForm(new Bm_Sys_008());
            this.Controls.Add(currentForm);
            /*
            Bm_Sys_008 sys008 = new Bm_Sys_008();

            if (formIsExist(sys008.GetType()))
            {
                sys008.Dispose();     // 창 리소스 제거                
            }
            else
            {
                sys008.MdiParent = this;
                sys008.Show();
            }
             * */
        }

        private void mnu_Logout_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void mnu_Reg_001_Click(object sender, EventArgs e)
        {
            System.Threading.TimerCallback tmp = new System.Threading.TimerCallback(doSomething);
            Gloval.timer = new System.Threading.Timer(tmp, null, 0, 0);

            this.Controls.Remove(currentForm);
            setForm(new BM_Reg_001());
            this.Controls.Add(currentForm);
            /*
            Bm_Reg_001 reg001 = new Bm_Reg_001();

            if (formIsExist(reg001.GetType()))
            {
                reg001.Dispose();     // 창 리소스 제거                
            }
            else
            {
                reg001.MdiParent = this;
                reg001.Show();
            }
             * 
             * */
            this.Cursor = Cursors.Default;
        }

        private void mnu_Reg_002_Click(object sender, EventArgs e)
        {

            this.Controls.Remove(currentForm);
            setForm(new BM_Reg_002());
            this.Controls.Add(currentForm);
            /*
            Bm_Reg_002 reg002 = new Bm_Reg_002();

            if (formIsExist(reg002.GetType()))
            {
                reg002.Dispose();     // 창 리소스 제거                
            }
            else
            {
                reg002.MdiParent = this;
                reg002.Show();
            }
             * */
        }

        private void mnu_Opr_001_Click(object sender, EventArgs e)
        {

            this.Controls.Remove(currentForm);
            setForm(new BM_Result());
            this.Controls.Add(currentForm);
            /*
            Bm_Opr_001 opr001 = new Bm_Opr_001();

            if (formIsExist(opr001.GetType()))
            {
                opr001.Dispose();     // 창 리소스 제거                
            }
            else
            {
                opr001.MdiParent = this;
                opr001.Show();
            }
             * */
        }

        private void mnu_Opr_002_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(currentForm);
            setForm(new Bm_Opr_002());
            this.Controls.Add(currentForm);
            /*
            Bm_Opr_002 opr002 = new Bm_Opr_002();

            if (formIsExist(opr002.GetType()))
            {
                opr002.Dispose();     // 창 리소스 제거                
            }
            else
            {
                opr002.MdiParent = this;
                opr002.Show();
            }
             * */
        }

        private void mnu_Opr_003_Click(object sender, EventArgs e)
        {

            this.Controls.Remove(currentForm);
            setForm(new Bm_Opr_003());
            this.Controls.Add(currentForm);
            /*
            Bm_Opr_003 opr003 = new Bm_Opr_003();

            if (formIsExist(opr003.GetType()))
            {
                opr003.Dispose();     // 창 리소스 제거                
            }
            else
            {
                opr003.MdiParent = this;
                opr003.Show();
            }
             * */
        }

        private void mnu_Rpt_001_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(currentForm);
            setForm(new Bm_Rpt_001());
            this.Controls.Add(currentForm);
            /*
            Bm_Rpt_001 rpt001 = new Bm_Rpt_001();

            if (formIsExist(rpt001.GetType()))
            {
                rpt001.Dispose();     // 창 리소스 제거                
            }
            else
            {
                rpt001.MdiParent = this;
                rpt001.Show();
            }
             * */
        }

        private void mnu_Rpt_002_Click(object sender, EventArgs e)
        {

        }



        private void mnu_Rpt_004_Click(object sender, EventArgs e)
        {
            MessageBox.Show("준비중입니다!");
            return;
            this.Controls.Remove(currentForm);
            setForm(new Bm_Rpt_004());
            this.Controls.Add(currentForm);
            /*
            Bm_Rpt_004 rpt004 = new Bm_Rpt_004();

            if (formIsExist(rpt004.GetType()))
            {
                rpt004.Dispose();     // 창 리소스 제거                
            }
            else
            {
                rpt004.MdiParent = this;
                rpt004.Show();
            }
             * */
        }

        private void mnu_Rpt_005_Click(object sender, EventArgs e)
        {

            this.Controls.Remove(currentForm);
            setForm(new Bm_Rpt_005());
            this.Controls.Add(currentForm);
            /*
            Bm_Rpt_005 rpt005 = new Bm_Rpt_005();

            if (formIsExist(rpt005.GetType()))
            {
                rpt005.Dispose();     // 창 리소스 제거                
            }
            else
            {
                rpt005.MdiParent = this;
                rpt005.Show();
            }
             * */
        }


        private void Bm_Main_Load(object sender, EventArgs e)
        {
            mnu_Sys_001.Visible = false; // 연동시스템
            mnu_Sys_002.Visible = false; // 사용자 설정
            mnu_Sys_003.Visible = false; // 포트 설정
            mnu_Sys_004.Visible = false; // 클라이언트 IP 설정
            mnu_Sys_005.Visible = false; // 사용자 권한 설정
            mnu_Sys_006.Visible = false; // 사용자 유형 설정
            mnu_Sys_007.Visible = false; // 사용자 비밀번호 변경
            mnu_Sys_008.Visible = false; // 접속자 조회

            //     mnu_Reg_Main.Visible = false; // 등록
            //  mnu_Reg_001.Visible = false; // 출입정보 관리
            mnu_Reg_002.Visible = false; // 관련정보 관리

            mnu_Opr_Main.Visible = false; // 운영 관리
            mnu_Opr_001.Visible = false; // Download Monitor
            mnu_Opr_002.Visible = false; // Upload Sync Monitor
            mnu_Opr_003.Visible = false; // Download Sync Monitor

            mnu_Rpt_Main.Visible = false; // 보고서
            mnu_Rpt_001.Visible = false; // 출입등록 Report

            mnu_Rpt_004.Visible = false; // 등록이력 Report
            mnu_Rpt_005.Visible = false; // 운영이력 Report

            string[] tempArray = null;

            try
            {
                if (string.IsNullOrEmpty(Bm_Login.login_Auth))
                {
                    MessageBox.Show("권한을 불러오지 못했습니다. 프로그램을 종료합니다");
                    Application.Exit();
                }
                else
                {
                    tempArray = Bm_Login.login_Auth.Split(',');

                    for (int i = 0; i < tempArray.Length - 1; i++)
                    {
                        PrgListYN(tempArray[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                Application.Exit();
                return;
            }
        }

        private void PrgListYN(string Listid)
        {
            switch (Listid.Trim())
            {
                case "BMS003":
                    mnu_Sys_001.Visible = true; // 연동시스템 설정                    
                    break;
                case "BMS004":
                    mnu_Sys_002.Visible = true; // 사용자 설정
                    break;
                case "BMS005":
                    mnu_Sys_003.Visible = false; // 포트 설정
                    break;
                case "BMS006":
                    mnu_Sys_004.Visible = true; // 클라이언트 IP설정
                    break;
                case "BMS007":
                    mnu_Sys_005.Visible = true; // 사용자 권한 설정
                    break;
                case "BMS008":
                    mnu_Sys_006.Visible = true; // 사용자 유형 설정
                    break;
                case "BMS009":
                    mnu_Sys_007.Visible = true; // 사용자 비밀번호 변경
                    break;
                case "BMS010":
                    mnu_Sys_008.Visible = true; // 접속자 조회
                    break;
                case "BMS011":
                    mnu_Reg_Main.Visible = true; // 등록
                    mnu_Reg_001.Visible = true; // 출입정보 관리
                    break;
                case "BMS012":
                    mnu_Reg_Main.Visible = true; // 등록
                    mnu_Reg_002.Visible = true; // 관련정보 관리
                    break;
                case "BMS013":
                    mnu_Opr_Main.Visible = true; // 운영 관리
                    mnu_Opr_001.Visible = true; // Download Monitor
                    break;
                case "BMS014":
                    mnu_Opr_Main.Visible = true; // 운영 관리
                    mnu_Opr_002.Visible = true; // Upload Sync Monitor
                    break;
                case "BMS015":
                    mnu_Opr_Main.Visible = true; // 운영 관리
                    mnu_Opr_003.Visible = true; // Download Sync Monitor
                    break;
                case "BMS016":
                    mnu_Rpt_Main.Visible = true;  // 보고서
                    mnu_Rpt_001.Visible = true; // 출입등록 Report
                    break;
                case "BMS017":
                    mnu_Rpt_Main.Visible = true;  // 보고서

                    break;
                case "BMS018":
                    // mnu_Rpt_Main.Visible = true;  // 보고서
                    // mnu_Rpt_004.Visible = true; // 등록이력 Report
                    break;
                case "BMS019":
                    mnu_Rpt_Main.Visible = true;  // 보고서
                    mnu_Rpt_005.Visible = true; // 운영이력 Report
                    break;
                default:
                    break;
            }
        }

        public static string getDeparmentDesc(int code)
        {
            Log.WriteLogTmp("[CodeTable.cs] getDepartmentDesc (" + code + ")" + department.Rows.Count);

            int count = department.Rows.Count;
            if (count < 1) return "Unknow";

            for (int i = 0; i < count; i++)
            {
                // Log.WriteLogTmp("[CodeTable.cs] " + department.Rows[i][0]);   //  + " | " + (string)department.Rows[i][1]);
                if (code == Convert.ToInt32(department.Rows[i][0])) return "" + department.Rows[i][1];
            }

            return "Unknown";
        }

        public static string getDivisionDesc(int code)
        {
            int count = division.Rows.Count;
            if (count < 1) return "Unknow";

            for (int i = 0; i < count; i++)
            {
                if (code == Convert.ToInt32(division.Rows[i][0])) return "" + division.Rows[i][1];
            }

            return "Unknown";
        }

        public static string getTitleDesc(int code)
        {
            int count = title.Rows.Count;
            if (count < 1) return "Unknow";

            for (int i = 0; i < count; i++)
            {
                if (code == Convert.ToInt32(title.Rows[i][0])) return "" + title.Rows[i][1];
            }

            return "Unknown";
        }

        public static string getBadgeStatDesc(int code)
        {
            int count = cardStat.Rows.Count;
            if (count < 1) return "Unknow";


            string result = "";
            for (int i = 0; i < count; i++)
            {

                if (code == Convert.ToInt32(cardStat.Rows[i][0]))
                {
                    result = cardStat.Rows[i][1].ToString();
                    Log.WriteLogTmp("[CodeTable.cs] getBadgeStatDesc (" + code + ") : " + result);
                    return result;
                }
            }

            return "Unknown";
        }

        public static string getBadgeTypeDesc(int code)
        {
            int count = cardType.Rows.Count;
            if (count < 1) return "Unknow";


            string result = "";
            for (int i = 0; i < count; i++)
            {
                if (code == Convert.ToInt32(cardType.Rows[i][0]))
                {
                    // return "" + cardType.Rows[i][1];
                    result = cardType.Rows[i][1].ToString();
                    Log.WriteLogTmp("[CodeTable.cs] getBadgeTypeDesc (" + code + ") : " + result);
                    return result;
                }

            }

            return "Unknown";
        }

        public static string getBadgeFormatDesc(int code)
        {
            int count = cardFormat.Rows.Count;
            if (count < 1) return "Unknow";

            string result = "";

            for (int i = 0; i < count; i++)
            {
                if (code == Convert.ToInt32(cardFormat.Rows[i][0]))
                {
                    result = cardFormat.Rows[i][1].ToString();
                    Log.WriteLogTmp("[CodeTable.cs] getBadgeFormatDesc (" + code + ") : " + result);
                    return result;
                    // return "" + cardFormat.Rows[i][1];
                }
            }
            return "Unknown";
        }

        public static string getIssueTypeDesc(int code)
        {
            int count = issueType.Rows.Count;
            if (count < 1) return "Unknow";

            for (int i = 0; i < count; i++)
            {
                if (code == Convert.ToInt32(issueType.Rows[i][0])) return "" + issueType.Rows[i][1];
            }
            return "Unknown";
        }

        public static string getIssueReasonDesc(int code)
        {
            int count = issueReason.Rows.Count;
            if (count < 1) return "Unknow";

            for (int i = 0; i < count; i++)
            {
                if (code == Convert.ToInt32(issueReason.Rows[i][0])) return "" + issueReason.Rows[i][1];
            }
            return "Unknown";
        }


    }
}
