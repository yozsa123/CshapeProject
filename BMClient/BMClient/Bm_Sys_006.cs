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
    public partial class Bm_Sys_006 : Form
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

        string currentTypeID = "";

        string delimeter = "^";
        string queryState = "S";

        QueryMaker qm = new QueryMaker();
        Request  req = new Request ();

        Dictionary<string, string> getData = new Dictionary<string, string>();
        string queryType = "S";


        public Bm_Sys_006()
        {
            InitializeComponent();
            showData();
            UserTypeGridView.CellClick += new DataGridViewCellEventHandler(clickGridView);


            txt_SearchCode.KeyDown += new KeyEventHandler(txt_SearchCode_KeyDown);
            txt_SearchCodeName.KeyDown += new KeyEventHandler(txt_SearchCodeName_KeyDown);
        }

        void txt_SearchCodeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                showData();
            }
        }

        void txt_SearchCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                showData();
            }
        }

        private void clickGridView(object sender, DataGridViewCellEventArgs e)
        {
            if (!(e.RowIndex < 0 || e.ColumnIndex < 0))
            {
                queryType = "U";
                currentTypeID = "" + UserTypeGridView.Rows[e.RowIndex].Cells[0].Value;
                txt_Code.Text = "" + UserTypeGridView.Rows[e.RowIndex].Cells[0].Value;
                txt_CodeName.Text = "" + UserTypeGridView.Rows[e.RowIndex].Cells[1].Value;

                txt_Code.Enabled = true;
                txt_CodeName.Enabled = true;
            }

        }


        public void showData()
        {
            Cursor.Current = Cursors.WaitCursor;
            UserTypeGridView.Rows.Clear();

            string rCheck = req.select("BMS008", qm.getQuery("Sys_006_S", txt_SearchCode.Text, txt_SearchCodeName.Text), "BMS");


            if (rCheck.Equals("0"))
            {
                dt = ReturnDT.dt;
                Bm_Main.UserType = ReturnDT.dt;
                DataTable User_Info = new DataTable();



            }

            rowNum = dt.Rows.Count;
            cNum = dt.Columns.Count;
            makeCell(this.UserTypeGridView, rowNum, cNum);

            if (rowNum > 0)
            {
                for (int i = 0; i < rowNum; i++)
                {
                    // AlarmActionRowUnit ua = (AlarmActionRowUnit)AdminDataClass.al[i];

                    this.UserTypeGridView.Rows[i].Cells[0].Value = "" + dt.Rows[i][0];
                    this.UserTypeGridView.Rows[i].Cells[1].Value = "" + dt.Rows[i][1];


                }
            }

            txt_Code.Text = "";
            txt_CodeName.Text = "";

            txt_Code.Enabled = false;
            txt_CodeName.Enabled = false;
            Cursor.Current = Cursors.Default;
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
            UserTypeGridView.Rows.Clear();
            showData();
          
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {

            if(queryType.Equals("U")){
                txt_Code.Text = "";
                txt_CodeName.Text = "";
            }

            MessageBox.Show("새로운 사용자 유형을 추가합니다");
            queryType = "I";
            UserTypeGridView.ReadOnly = true;
            txt_Code.Enabled = true;
            txt_CodeName.Enabled = true;
            txt_Code.Focus();

        }

        public void insertData()
        {
            getData.Add("USER_TYPE", "" + txt_Code.Text);
            getData.Add("REMARK", "" + txt_CodeName.Text);
            getData.Add("CREATE_ID", Bm_Login.login_Id);

            string rCheck = req.update("BMS008", qm.getQuery("Sys_006_I", getData), "BMI");


            if (rCheck.Equals("0"))
            {
                
                MessageBox.Show("사용자유형이 정상적으로 추가되었습니다");

                rCheck = req.update("BMS008", qm.getQuery("SYS006_LOG_N", "1", getData["REMARK"]), "BMI");
                if (!rCheck.Equals("0")) MessageBox.Show("로그 저장 실패..");

                UserTypeGridView.Rows.Clear();
                showData();
                Cursor.Current = Cursors.Default;
                getData.Clear();
                Log.WriteLog("rowNum : " + rowNum);

            }
            else
            {
                MessageBox.Show("사용자유형 추가 실패");
            }

            UserTypeGridView.ReadOnly = false;
        }

        public void updateData()
        {
            getData.Add("USER_TYPE", "" + txt_Code.Text);
            getData.Add("REMARK", "" + txt_CodeName.Text);
            getData.Add("UPDATE_ID", Bm_Login.login_Id);
            getData.Add("CURRENT_TYPE_ID", currentTypeID);


            string rCheck = req.update("BMS008", qm.getQuery("Sys_006_Type_U", getData), "BMI");


            if (rCheck.Equals("0"))
            {

                MessageBox.Show("사용자유형이 정상적으로 수정되었습니다");

                rCheck = req.update("BMS008", qm.getQuery("SYS006_LOG_M", "1", getData["REMARK"]), "BMI");
                if (!rCheck.Equals("0")) MessageBox.Show("로그 저장 실패..");

                UserTypeGridView.Rows.Clear();
             
                showData();

                Cursor.Current = Cursors.Default;

                getData.Clear();
                Log.WriteLog("rowNum : " + rowNum);

            }
            else
            {
                MessageBox.Show("사용자유형 추가 실패");
            }
        }

        public void deleteData()
        {
            string rCheck = req.update("BMS008", qm.getQuery("Sys_006_Type_D", currentTypeID), "BMI");


            if (rCheck.Equals("0"))
            {

                MessageBox.Show("사용자유형이 정상적으로 삭제되었습니다");

                rCheck = req.update("BMS008", qm.getQuery("SYS006_LOG_D", "1", txt_CodeName.Text), "BMI");
                if (!rCheck.Equals("0")) MessageBox.Show("로그 저장 실패..");
 
                showData();
                Cursor.Current = Cursors.Default;
                Log.WriteLog("rowNum : " + rowNum);

            }
            else
            {
                MessageBox.Show("사용자유형 삭제 실패");
            }

            
        }

                

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (queryType.Equals("I"))
            {
                insertData();
            }

            if (queryType.Equals("U"))
            {
                 updateData();
            }



            queryType = "S";
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            queryType = "D";

            if (MessageBox.Show("선택하신 사용자 유형의 권한을 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                deleteData();
            }
            else
            {
                return;

            }
            queryType = "S";
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
