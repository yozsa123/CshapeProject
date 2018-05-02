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
    public partial class Bm_Sys_002 : Form
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
        Dictionary<string, string> getData = new Dictionary<string, string>();
        string queryType = "S";
        QueryMaker qm = new QueryMaker();
        Request req = new Request();


        
      

        public Bm_Sys_002()
        {
            InitializeComponent();
    
            showData();

            for (int i = 0; i < Bm_Main.UserType.Rows.Count; i++)
            {
                userTypeComboBox.Items.Add(Bm_Main.UserType.Rows[i][1]);
            }


            UserGridView.CellClick += new DataGridViewCellEventHandler(clickGridView);
            txt_SearchUserId.KeyDown += new KeyEventHandler(txt_SearchUserId_KeyDown);
            txt_SearchUserName.KeyDown += new KeyEventHandler(txt_SearchUserName_KeyDown);

        }

        void txt_SearchUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                showData();
            }
        }

        void txt_SearchUserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                showData();
            }
        }

        private void clickGridView(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            queryType = "U";
            allEnable();
            txt_UserId.Text = "" + UserGridView.Rows[e.RowIndex].Cells[0].Value;
            txt_UserName.Text = "" + UserGridView.Rows[e.RowIndex].Cells[1].Value;
            txt_Password.Text = "";
            userTypeComboBox.Text = "" + UserGridView.Rows[e.RowIndex].Cells[2].Value;
        }




        public void showData()
        {


            Cursor.Current = Cursors.WaitCursor;
            UserGridView.Rows.Clear();
            allDisEnable();
            string rCheck = req.select("BMS004", qm.getQuery("Sys_002_S",txt_SearchUserId.Text,txt_SearchUserName.Text), "BMS");


            if (rCheck.Equals("0"))
            {
                dt = ReturnDT.dt;

                DataTable User_Info = new DataTable();


               
            }

            rowNum = dt.Rows.Count;
            cNum = dt.Columns.Count;
            makeCell(this.UserGridView, rowNum, cNum);

            if (rowNum > 0)
            {
                for (int i = 0; i < rowNum; i++)
                {
                    // AlarmActionRowUnit ua = (AlarmActionRowUnit)AdminDataClass.al[i];

                    this.UserGridView.Rows[i].Cells[0].Value = "" + dt.Rows[i][0];
                    this.UserGridView.Rows[i].Cells[1].Value = "" + dt.Rows[i][1];
                    this.UserGridView.Rows[i].Cells[2].Value = "" + dt.Rows[i][2];




                }
            }
            Cursor.Current = Cursors.Default;
            
         
        }

        private void tbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                showData(); 
            }
        }



        public void makeCell(DataGridView _dgv, int rNum, int cNum)
        {
            if (rNum == -1)
            {
               
                return;
            }
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

        private void btn_Add_Click(object sender, EventArgs e)
        {
            queryType = "I";
        
            txt_Password.Text = "";
            txt_UserId.Text = "";
            txt_UserName.Text = "";
            userTypeComboBox.SelectedIndex = 0;
            txt_Password.Enabled = true;
            txt_UserId.Enabled = true;
            txt_UserId.ReadOnly = false;
            txt_UserName.Enabled = true;
            userTypeComboBox.Enabled = true;

        }

        public void allDisEnable()
        {
            txt_Password.Enabled = false;
            txt_UserId.Enabled = false;
            txt_UserName.Enabled = false;
            userTypeComboBox.Enabled = false;
        }
        public void allEnable()
        {
            txt_Password.Enabled = true;
            txt_UserName.Enabled = true;
            userTypeComboBox.Enabled = true;
        }

        public int checkNull()
        {
            if (txt_UserId.Text.Equals(""))
            {
                MessageBox.Show("아이디를 입력해주세요");
                return 1;
            }
            else if (txt_UserName.Text.Equals(""))
            {
                MessageBox.Show("사용자 성명을 입력해주세요");
                return 1;
            }
            else if (txt_Password.Text.Equals(""))
            {
                MessageBox.Show("사용자 패스워드를 입력해주세요");
                return 1;
            }
            else if (userTypeComboBox.Text.Equals(""))
            {
                MessageBox.Show("사용자 유형을 선택해주세요");
                return 1;
            }
            else return 0;
        }


        public void userInsert()
        {
            if (checkNull() != 1)
            {
                getData.Add("ID", "" + txt_UserId.Text);
                getData.Add("USER_NAME", "" + txt_UserName.Text);
                getData.Add("USER_PASSWORD", Bm_Main.setPw(txt_Password.Text));
                getData.Add("CREATE_ID", Bm_Login.login_Id);
                getData.Add("USER_TYPE", getUserType(userTypeComboBox.Text));
                Cursor.Current = Cursors.WaitCursor;
                string rCheck = req.update("BMS004", qm.getQuery("Sys_002_I", getData), "BMI");

                if (rCheck.Equals("0"))
                {
                    MessageBox.Show("사용자가 정상적으로 추가되었습니다");

                    rCheck = req.update("BMS004", qm.getQuery("SYS002_LOG_N", "1", getData["ID"]), "BMI");

                    UserGridView.Rows.Clear();
                    showData();
                    getData.Clear();
                    Log.WriteLog("rowNum : " + rowNum);
                    Cursor.Current = Cursors.Default;

                }
                else
                {

                    MessageBox.Show("사용자 추가 실패");
                    rCheck = req.update("BMS004", qm.getQuery("SYS002_LOG_N", "0", getData["ID"]), "BMI");

                }
                queryType = "S";
            }
        }

        

        public void userUpdate()
        {
            if (checkNull() != 1)
            {
                getData.Add("ID", "" + txt_UserId.Text);
                getData.Add("USER_NAME", "" + txt_UserName.Text);
                getData.Add("USER_PASSWORD", Bm_Main.setPw(txt_Password.Text));
                getData.Add("USER_TYPE", getUserType(userTypeComboBox.Text));

                string rCheck = req.update("BMS004", qm.getQuery("Sys_002_U", getData), "BMI");

                if (rCheck.Equals("0"))
                {
                    MessageBox.Show("사용자가 정상적으로 변경되었습니다");

                    rCheck = req.update("BMS004", qm.getQuery("SYS002_LOG_M", "1", getData["ID"]), "BMI");


                    UserGridView.Rows.Clear();
                    showData();



                    getData.Clear();
                    Log.WriteLog("rowNum : " + rowNum);

                }
                else
                {
                    MessageBox.Show("사용자 정보 변경 실패");
                    rCheck = req.update("BMS004", qm.getQuery("SYS002_LOG_M", "0", getData["ID"]), "BMI");


                }
                queryType = "S";
            }


        }

        public void userDelete()
        {

            getData.Add("ID", "" + txt_UserId.Text);

            if (MessageBox.Show("선택하신 아이디를 정말로 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;

                string rCheck = req.update("BMS004", qm.getQuery("Sys_002_D", getData), "BMI");

                if (rCheck.Equals("0"))
                {
                    MessageBox.Show("사용자가 정상적으로 삭제되었습니다");
                    rCheck = req.update("BMS004", qm.getQuery("SYS002_LOG_D", "1", getData["ID"]), "BMI");
               

                    UserGridView.Rows.Clear();
                    showData();
                    getData.Clear();
                    Log.WriteLog("rowNum : " + rowNum);
                    Cursor.Current = Cursors.Default;

                }

                else
                {
                    MessageBox.Show("사용자정보 삭제 실패");
                    rCheck = req.update("BMS004", qm.getQuery("SYS002_LOG_D", "0", getData["ID"]), "BMI");
                }

            }
            else
            {
                return;
            }
        }
        public string getUserType(string userTypeName)
        {
            string userTypeId = "";
            for (int i = 0; i < Bm_Main.UserType.Rows.Count; i++)
            {
                if (("" + Bm_Main.UserType.Rows[i][1]).Equals(userTypeComboBox.Text))
                {
                    userTypeId = "" + Bm_Main.UserType.Rows[i][0];
                    
                    break;
                }
            }
            return userTypeId;

        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            Log.WriteLog(queryType);
            if (queryType.Equals("I")) userInsert();
            else if (queryType.Equals("U")) userUpdate();

            
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
           
            showData();
          
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            userDelete();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
