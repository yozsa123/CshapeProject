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
    public partial class Bm_Sys_005 : Form
    {

        private DataGridViewTextBoxColumn[][] topCell = null;
        int rowNum = 300;
        int cNum = 2;

        int rowNum2 = 10;
        int cNum2 = 1;
        DataTable dt;
     
        string delimeter = "^";
        string queryState = "S";

        string userTypeId = "";
        QueryMaker qm = new QueryMaker();
        Request  req = new Request();

        DataTable updateDataTable = new DataTable();


        public Bm_Sys_005()
        {
            InitializeComponent();

            for (int i = 0; i < Bm_Main.UserType.Rows.Count; i++)
            {
                comboUserType.Items.Add(Bm_Main.UserType.Rows[i][1]);
            }

            updateDataTable.Columns.Add("PROGRAM_ID");
            updateDataTable.Columns.Add("USER_TYPE");
           
            comboUserType.SelectedIndex = 0 ;
            getProgramList();
        }



        public void getProgramList()
        {
            string rCheck = req.select("BMS007", qm.getQuery("Sys_005_S"), "BMS");


            if (rCheck.Equals("0"))
            {
                dt = ReturnDT.dt;

                DataTable User_Info = new DataTable();



            }

            rowNum = dt.Rows.Count;
            cNum = dt.Columns.Count;
            makeCell(this.ProgramGridView, rowNum, cNum);

            if (rowNum > 0)
            {
                for (int i = 0; i < rowNum; i++)
                {
                    // AlarmActionRowUnit ua = (AlarmActionRowUnit)AdminDataClass.al[i];

                    this.ProgramGridView.Rows[i].Cells[0].Value = "" + dt.Rows[i][0];
                    this.ProgramGridView.Rows[i].Cells[1].Value = "" + dt.Rows[i][1];

                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            AuthGridView.Rows.Clear();
            
            showData();
            Cursor.Current = Cursors.Default;
        }


        public void showData()
        {
            AuthGridView.Rows.Clear();
            Cursor.Current = Cursors.WaitCursor;
           userTypeId = getUserType(comboUserType.Text);

           string rCheck = req.select("BMS007", qm.getQuery("Sys_005_Auth_S", userTypeId), "BMS");


           if (rCheck.Equals("0"))
            {


                dt = ReturnDT.dt;

                DataTable User_Info = new DataTable();



            }


           


           

            rowNum = dt.Rows.Count;
            cNum = dt.Columns.Count;
            makeCell(this.AuthGridView, rowNum, cNum);

            if (rowNum > 0)
            {
                for (int i = 0; i < rowNum; i++)
                {
                    // AlarmActionRowUnit ua = (AlarmActionRowUnit)AdminDataClass.al[i];

                    this.AuthGridView.Rows[i].Cells[0].Value = "" + dt.Rows[i][1];
                    this.AuthGridView.Rows[i].Cells[1].Value = "" + dt.Rows[i][2];
                 


                }
            }


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


        public void updateData()
        {

            updateDataTable.Rows.Clear();
            userTypeId =  getUserType(comboUserType.Text);

            DataRow row;
            for (int i = 0; i < ProgramGridView.Rows.Count; i++)
            {
                if ((bool)ProgramGridView.Rows[i].Cells[2].EditedFormattedValue  == true)
                {
                    row = updateDataTable.NewRow();
                    row[0] = "" + ProgramGridView.Rows[i].Cells[0].Value;
                    row[1] = "" + userTypeId;
                  
                    updateDataTable.Rows.Add(row);
                    
                }
            }

            for (int i = 0; i < updateDataTable.Rows.Count; i++)
            {
                Log.WriteLog("updateDataTable i = " + i + "  updateDataTable(PROGRAM_ID) =  " + updateDataTable.Rows[i][0]);
                Log.WriteLog("updateDataTable i = " + i + "  updateDataTable(USER_TYPE) =  " + userTypeId);
            }




            string rCheck = req.update("BMS007", qm.getQuery("Sys_005_Auth_U", updateDataTable), "BMI");

            if (rCheck.Equals("0"))
            {
                MessageBox.Show("사용자가 정상적으로 변경되었습니다");


                rCheck = req.update("BMS007", qm.getQuery("SYS005_LOG_U", "1", comboUserType.Text), "BMI");
                if (!rCheck.Equals("0")) MessageBox.Show("로그 저장 실패..");


                AuthGridView.Rows.Clear();
                showData();
                updateDataTable.Clear();
               

            }
            else
            {
                MessageBox.Show("사용자 유형 권한 변경 실패");
            }


        }

        public string getUserType(string userTypeName)
        {
            string userTypeId = "";
            for (int i = 0; i < Bm_Main.UserType.Rows.Count; i++)
            {
                if (("" + Bm_Main.UserType.Rows[i][1]).Equals(userTypeName))
                {
                    userTypeId = "" + Bm_Main.UserType.Rows[i][0];

                    break;
                }
            }
            return userTypeId;
        }
        

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("선택하신 사용자 유형의 권한을 수정하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                Boolean nonFlag = false;
                for (int i = 0; i < ProgramGridView.Rows.Count; i++)
                {
                    if ((bool)ProgramGridView.Rows[i].Cells[2].EditedFormattedValue == true)
                    {
                        nonFlag = true;
                    }
                }
                if (!nonFlag)
                {
                    MessageBox.Show("하나 이상의 사용자 권한을 선택해주세요");
                    return;
                }



                updateData();
                showData();

                Cursor.Current = Cursors.Default;
            }
            else
            {
                return;

            }
          
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
