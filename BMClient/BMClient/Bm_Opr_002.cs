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
    public partial class Bm_Opr_002 : Form
    {
        private DataGridViewTextBoxColumn[][] topCell = null;
        int rowNum = 300;
        int cNum = 2;

        int rowNum2 = 10;
        int cNum2 = 1;
        DataTable dt;
        DataSet dsAcsInfo = new DataSet();
        DataTable dtAcsInfo;
        DataTable dtAcsInfo_Base;

        string delimeter = "^";
        string queryState = "S";

        QueryMaker qm = new QueryMaker();
        Request  req = new Request ();


        public Bm_Opr_002()
        {
            InitializeComponent();
            for (int i = 0; i < Bm_Main.serverInfo.Rows.Count; i++)
            {
                plantSearchComboBox.Items.Add(Bm_Main.serverInfo.Rows[i]["SERVER_NAME"]);
            }
        }


        public void showData()
        {
            string rCheck = "";
            if (searchInfoCombobox.Text.Equals("소속"))
            {
                rCheck = req.select("BMS003", qm.getQuery("Bm_Department_R", "", ""), "BMS");
            }
            else if (searchInfoCombobox.Text.Equals("부서"))
            {
                rCheck = req.select("BMS003", qm.getQuery("Bm_Division_R", "", ""), "BMS");
            }
            else if (searchInfoCombobox.Text.Equals("직급"))
            {
                rCheck = req.select("BMS003", qm.getQuery("Bm_Title_R", "", ""), "BMS");
            }
            else if (searchInfoCombobox.Text.Equals("카드유형"))
            {
                rCheck = req.select("BMS003", qm.getQuery("Bm_Card_Type_R", "", ""), "BMS");
            }
            else if (searchInfoCombobox.Text.Equals("카드상태"))
            {
                rCheck = req.select("BMS003", qm.getQuery("Bm_Card_Stat_R", "", ""), "BMS");
            }




            if (rCheck.Equals("0"))
            {
                dt = ReturnDT.dt;

                DataTable User_Info = new DataTable();



            }

            rowNum = dt.Rows.Count;
            cNum = dt.Columns.Count;
            makeCell(this.bmGridView, rowNum, cNum);

            if (rowNum > 0)
            {
                for (int i = 0; i < rowNum; i++)
                {
                    // AlarmActionRowUnit ua = (AlarmActionRowUnit)AdminDataClass.al[i];

                    this.bmGridView.Rows[i].Cells[0].Value = "" + dt.Rows[i][0];
                    this.bmGridView.Rows[i].Cells[1].Value = "" + dt.Rows[i][1];


                }
            }

            string pNum = "";
            string pType = "";
            for(int i = 0 ; i < Bm_Main.serverInfo.Rows.Count;i++){
                if (plantSearchComboBox.Text.Equals(Bm_Main.serverInfo.Rows[i]["SERVER_NAME"]))
                {
                    pNum = "" + Bm_Main.serverInfo.Rows[i]["SERVER_NUM"];
                    pType = "" + Bm_Main.serverInfo.Rows[i]["SERVER_TYPE"];
                }
            }

            if (pType.Equals("1") || pType.Equals("2"))
            {

                if (searchInfoCombobox.Text.Equals("소속"))
                {
                    rCheck = req.select("BMS003", qm.getQuery("PP_Department_R"), "ACS" + pNum + "S");
                }
                else if (searchInfoCombobox.Text.Equals("부서"))
                {
                    MessageBox.Show("해당 ACS에서는 해당 컬럼이 존재하지 않습니다.");
                    return;
                }
                else if (searchInfoCombobox.Text.Equals("직급"))
                {
                    MessageBox.Show("해당 ACS에서는 해당 컬럼이 존재하지 않습니다.");
                    return;
                }
                else if (searchInfoCombobox.Text.Equals("카드유형"))
                {
                    rCheck = req.select("BMS003", qm.getQuery("PP_Card_Type_R"), "ACS" + pNum + "S");
                }
                else if (searchInfoCombobox.Text.Equals("카드상태"))
                {
                    rCheck = req.select("BMS003", qm.getQuery("PP_Card_Stat_R"), "ACS" + pNum + "S");
                }



            }
            else if (pType.Equals("3"))
            {

                if (searchInfoCombobox.Text.Equals("소속"))
                {
                    rCheck = req.select("BMS003", qm.getQuery("OG_Department_R"), "ACS" + pNum + "S");
                }
                else if (searchInfoCombobox.Text.Equals("부서"))
                {
                    rCheck = req.select("BMS003", qm.getQuery("OG_Division_R"), "ACS" + pNum + "S");
                }
                else if (searchInfoCombobox.Text.Equals("직급"))
                {
                    rCheck = req.select("BMS003", qm.getQuery("OG_Title_R"), "ACS" + pNum + "S");
                }
                else if (searchInfoCombobox.Text.Equals("카드유형"))
                {
                    rCheck = req.select("BMS003", qm.getQuery("OG_Card_Type_R"), "ACS" + pNum + "S");
                }
                else if (searchInfoCombobox.Text.Equals("카드상태"))
                {
                    rCheck = req.select("BMS003", qm.getQuery("OG_Card_Stat_R"), "ACS" + pNum + "S");
                }
            }

            if (rCheck.Equals("0"))
            {
                dt = ReturnDT.dt;

               // Log.WriteLog(" DT = > " + "" + dt.Rows[0][0]);



            }

            rowNum = dt.Rows.Count;
            cNum = dt.Columns.Count;
            makeCell(this.acsGridView, rowNum, cNum);

            if (rowNum > 0)
            {
                for (int i = 0; i < rowNum; i++)
                {
                    // AlarmActionRowUnit ua = (AlarmActionRowUnit)AdminDataClass.al[i];

                    this.acsGridView.Rows[i].Cells[0].Value = "" + dt.Rows[i][0];
                    this.acsGridView.Rows[i].Cells[1].Value = "" + dt.Rows[i][1];






                }
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
            }

        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (searchInfoCombobox.Text.Equals("") || plantSearchComboBox.Text.Equals(""))
            {
                MessageBox.Show("검색조건을 정확히 입력해주세요");
                return;
            }
            bmGridView.Rows.Clear();
            acsGridView.Rows.Clear();
            showData();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
