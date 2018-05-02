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
    public partial class Bm_Sys_004 : Form
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
        DataTable updateDataTable = new DataTable();
        string currentIP ="";
        Boolean updateFlag = true;
        string delimeter = "^";
        string queryState = "S";

        QueryMaker qm = new QueryMaker();
        Request req = new Request();


        Dictionary<string, string> getData = new Dictionary<string, string>();


        string queryType = "S";
        public Bm_Sys_004()
        {
            InitializeComponent();
            showData();

            updateDataTable.Columns.Add("IP_ADDRESS");
            updateDataTable.Columns.Add("DESCRIPTION");
            updateDataTable.Columns.Add("CURRENT_IP");
            updateDataTable.Columns.Add("UPDATE_ID");

            txtIP.KeyDown += new KeyEventHandler(txtIP_KeyDown);

            ClientGridView.CellClick += new DataGridViewCellEventHandler(dataGridViewCellClick);
            ClientGridView.CellValueChanged += new DataGridViewCellEventHandler(cellValueChange);
        }

        void txtIP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                showData();
            }
        }
        public void dataGridViewCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(!(e.RowIndex < 0 || e.ColumnIndex <0) )
            currentIP = "" + ClientGridView.Rows[e.RowIndex].Cells[0].Value;
         
        }

        public void cellValueChange(object sender, DataGridViewCellEventArgs e)
        {
          
            
            if (queryType.Equals("I") || !updateFlag)
            {
                return;
            }
            queryType = "U";

            DataRow row = null;
            //updateDataTable.Columns["IP_ADDRESS"].DataType = typeof(string);
            row = updateDataTable.Select("IP_ADDRESS = '" + ClientGridView.Rows[e.RowIndex].Cells[0].Value+ "'").FirstOrDefault();

            if (row == null) //수정한 데이터값이 DataTable에 없을때 추가
            {

                row = updateDataTable.NewRow();
                row["IP_ADDRESS"] = "" + ClientGridView.Rows[e.RowIndex].Cells[0].Value;
                row["DESCRIPTION"] = "" + ClientGridView.Rows[e.RowIndex].Cells[1].Value;
                row["UPDATE_ID"] = Bm_Login.login_Id;
                row["CURRENT_IP"] = currentIP;
                
                updateDataTable.Rows.Add(row);

                for (int i = 0; i < updateDataTable.Rows.Count; i++)
                {
                    Log.WriteLog("updateDataTable : " + i + " : " + updateDataTable.Rows[i][0]);
                    Log.WriteLog("updateDataTable : " + i + " : " + updateDataTable.Rows[i][1]);
                    Log.WriteLog("updateDataTable : " + i + " : " + updateDataTable.Rows[i][2]);

                }

                Log.WriteLog("updateDataTable.Rows.Count : " + updateDataTable.Rows.Count);
            }
            else  //수정한 데이터값이 DataTable에 있으면 값수정 
            {
                row["IP_ADDRESS"] = "" + ClientGridView.Rows[e.RowIndex].Cells[0].Value;
                row["DESCRIPTION"] = "" + ClientGridView.Rows[e.RowIndex].Cells[1].Value;
                row["UPDATE_ID"] = Bm_Login.login_Id ;
                row["CURRENT_IP"] = currentIP;
                for (int i = 0; i < updateDataTable.Rows.Count; i++)
                {
                    Log.WriteLog("updateDataTable : " + i + " : " + updateDataTable.Rows[i][0]);
                    Log.WriteLog("updateDataTable : " + i + " : " + updateDataTable.Rows[i][1]);
                }

            }
        }


        public void showData()
        {

            Cursor.Current = Cursors.WaitCursor;
            ClientGridView.Rows.Clear();
            updateFlag = false;
            string rCheck = req.select("BMS006", qm.getQuery("Sys_004_S", txtIP.Text), "BMS");


            if (rCheck.Equals("0"))
            {
                dt = ReturnDT.dt;

                DataTable User_Info = new DataTable();



            }

            rowNum = dt.Rows.Count;
            cNum = dt.Columns.Count;
            makeCell(this.ClientGridView, rowNum, cNum);

            if (rowNum > 0)
            {
                for (int i = 0; i < rowNum; i++)
                {
                    // AlarmActionRowUnit ua = (AlarmActionRowUnit)AdminDataClass.al[i];

                    this.ClientGridView.Rows[i].Cells[0].Value = "" + dt.Rows[i][0];
                    this.ClientGridView.Rows[i].Cells[1].Value = "" + dt.Rows[i][1];
                 




                }
            }
            Cursor.Current = Cursors.Default;

            updateFlag = true;

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
        public void insertClient()
        {
        
            
                getData.Add("IP_ADDRESS", "" + this.ClientGridView.Rows[rowNum].Cells[0].Value);
                getData.Add("DESCRIPTION", "" + this.ClientGridView.Rows[rowNum].Cells[1].Value);
                getData.Add("CREATE_ID", Bm_Login.login_Id);


                string rCheck = req.update("BMS006", qm.getQuery("Sys_004_I", getData), "BMI");


            if (rCheck.Equals("0"))
            {
                MessageBox.Show("Client 정보가 정상적으로 추가되었습니다");

                rCheck = req.update("BMS006", qm.getQuery("SYS004_LOG_N", "1", getData["IP_ADDRESS"]), "BMI");
                if (!rCheck.Equals("0")) MessageBox.Show("로그 저장 실패..");

                ClientGridView.Rows.Clear();
                showData();
                getData.Clear();
                rowNum = dt.Rows.Count;
                Log.WriteLog("rowNum : " + rowNum);

            }
            else
            {
                MessageBox.Show("Client정보 추가 실패");

            }
        }

        public void updateData()
        {
            if (MessageBox.Show("지금까지 변경하신 사항을 저장하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string rCheck = req.update("BMS006", qm.getQuery("Sys_004_U", updateDataTable), "BMI");

                Cursor.Current = Cursors.WaitCursor;
                if (rCheck.Equals("0"))
                {
                    MessageBox.Show("해당 IP정보가 정상적으로 반영되었습니다");

                    string tmp = "";

                    for (int i = 0; i < updateDataTable.Rows.Count; i++)
                    {
                        if (i == updateDataTable.Rows.Count - 1) tmp += updateDataTable.Rows[i]["IP_ADDRESS"] + " : " + updateDataTable.Rows[i]["DESCRIPTION"];
                        else tmp += updateDataTable.Rows[i]["IP_ADDRESS"] + " : " + updateDataTable.Rows[i]["DESCRIPTION"] +" . ";
                    }



                    rCheck = req.update("BMS006", qm.getQuery("SYS004_LOG_M", "1", tmp), "BMI");
                    if (!rCheck.Equals("0")) MessageBox.Show("로그 저장 실패..");

                    ClientGridView.Rows.Clear();
                    showData();

                    Cursor.Current = Cursors.Default;
                }
                else
                {
                    MessageBox.Show("IP정보 변경 실패");
                }

                queryType = "S";
            }
            else
            {
                return;
            }
        }

        public void deleteData()
        {

            if (MessageBox.Show("해당 IP를 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
                string rCheck = req.update("BMS006", qm.getQuery("Sys_004_D", currentIP), "BMI");


                if (rCheck.Equals("0"))
                {
                    MessageBox.Show("해당 Ip가 정상적으로삭제되었습니다");

                    rCheck = req.update("BMS006", qm.getQuery("SYS004_LOG_D", "1", currentIP), "BMI");
                    if (!rCheck.Equals("0")) MessageBox.Show("로그 저장 실패..");
                    
                    ClientGridView.Rows.Clear();
                    showData();

                
                }
                else
                {
                    MessageBox.Show("삭제 실패");

                }

                queryType = "S";
            }
            else
            {
                return;
            }

            
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            queryType = "I";
            
            ClientGridView.Rows.Add(1); 
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {

            
            if (queryType.Equals("I"))
            {
                insertClient();

            }
            else if (queryType.Equals("U"))
            {
                updateData();
                updateDataTable.Rows.Clear();
            }

            

                queryType = "S";
          
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            ClientGridView.Rows.Clear();
     
            showData();
           
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            deleteData();
            showData();
            Cursor.Current = Cursors.Default;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
