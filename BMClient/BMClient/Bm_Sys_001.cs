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
    public partial class Bm_Sys_001 : Form
    {
        private DataGridViewTextBoxColumn[][] topCell = null;
        int rowNum = 300;
        int cNum = 2;

        DataTable dt;
        DataSet dsAcsInfo = new DataSet();

       
        string delimeter = "^";
        string queryState = "S";
        int rowCnt = 0;
        string currentServerNum = "";
        int currentRowIndex = 0;

        DataTable updateDataTable = new DataTable();
        QueryMaker qm = new QueryMaker();

        Request  req = new Request ();

        Boolean updateFlag = true;

        string multiQuery = "";

        Dictionary<string, string> getData = new Dictionary<string, string>();
        string queryType = "S";
        public Bm_Sys_001()
        {
            InitializeComponent();
            Cursor.Current = Cursors.WaitCursor;
            showData();
            rowCnt = dataGridView1.Rows.Count - 1;
            updateDataTable.Columns.Add("ACSID");
            updateDataTable.Columns.Add("SERVER_NAME");
            updateDataTable.Columns.Add("SERVER_TYPE");
            updateDataTable.Columns.Add("P_IP");
            updateDataTable.Columns.Add("S_IP");
            updateDataTable.Columns.Add("SERVER_ID");
            updateDataTable.Columns.Add("SERVER_PW");
            updateDataTable.Columns.Add("DB_NAME");
            updateDataTable.Columns.Add("DB_ID");
            updateDataTable.Columns.Add("DB_PW");
            updateDataTable.Columns.Add("DB_HOST");
            updateDataTable.Columns.Add("DB_HOST2");
            updateDataTable.Columns.Add("DB_SERVICE");
            updateDataTable.Columns.Add("CURRENT_SERVER_NUM");
            
            dataGridView1.CellClick +=new DataGridViewCellEventHandler(dataGridViewCellClick);
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(cellValueChange);
            Cursor.Current = Cursors.Default;



        }

        public void dataGridViewCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(!(e.ColumnIndex == -1 || e.RowIndex == -1)){
                currentServerNum = "" + dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                currentRowIndex = e.RowIndex;
            }
        }

        public void cellValueChange(object sender, DataGridViewCellEventArgs e)
        {


            if (!updateFlag) return;

            if (queryType.Equals("I")  )
            {
                return;
            }
            queryType = "U";
            string checkServerNum = "" + dataGridView1.Rows[e.RowIndex].Cells[0].Value;

            DataRow row = null;

            row = updateDataTable.Select("ACSID = " + dataGridView1.Rows[e.RowIndex].Cells[0].Value).FirstOrDefault();

            if (row == null) //수정한 데이터값이 DataTable에 없을때 추가
            {

                row = updateDataTable.NewRow();
                row["ACSID"] = "" + dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                row["SERVER_NAME"] = "" + dataGridView1.Rows[e.RowIndex].Cells[1].Value;
                int serverType = getServerType(e.RowIndex);
                row["SERVER_TYPE"] = "" + serverType;
                row["P_IP"] = "" + dataGridView1.Rows[e.RowIndex].Cells[3].Value;
                row["S_IP"] = "" + dataGridView1.Rows[e.RowIndex].Cells[4].Value;
                row["SERVER_ID"] = "" + dataGridView1.Rows[e.RowIndex].Cells[5].Value;
                if (dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString().Length != 24)
                {// 패스워드를 수정했는지 체크 암호화 암호화된 문자의 길이는 24
                    row["SERVER_PW"] = Bm_Main.setPw("" + dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                }
                else
                {
                    row["SERVER_PW"] = "" + dataGridView1.Rows[e.RowIndex].Cells[6].Value;
                }

                row["DB_NAME"] = "" + dataGridView1.Rows[e.RowIndex].Cells[7].Value;
                row["DB_ID"] = "" + dataGridView1.Rows[e.RowIndex].Cells[8].Value;
                if (dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString().Length != 24)
                {// 패스워드를 수정했는지 체크 암호화 암호화된 문자의 길이는 24
                    row["DB_PW"] = Bm_Main.setPw("" + dataGridView1.Rows[e.RowIndex].Cells[9].Value);
                }
                else
                {
                    row["DB_PW"] = "" + dataGridView1.Rows[e.RowIndex].Cells[9].Value;
                }
                row["DB_HOST"] = "" + dataGridView1.Rows[e.RowIndex].Cells[10].Value;
                row["DB_HOST2"] = "" + dataGridView1.Rows[e.RowIndex].Cells[11].Value;
                row["DB_SERVICE"] = "" + dataGridView1.Rows[e.RowIndex].Cells[12].Value;
                row["CURRENT_SERVER_NUM"] = checkServerNum;
                updateDataTable.Rows.Add(row);

                for (int i = 0; i < updateDataTable.Rows.Count; i++)
                {
                    Log.WriteLog("updateDataTable : " + i + " : " + updateDataTable.Rows[i][0]);
                    Log.WriteLog("updateDataTable : " + i + " : " + updateDataTable.Rows[i][1]);
                    Log.WriteLog("updateDataTable : " + i + " : " + updateDataTable.Rows[i][2]);
                    Log.WriteLog("updateDataTable : " + i + " : " + updateDataTable.Rows[i][3]);
                    Log.WriteLog("updateDataTable : " + i + " : " + updateDataTable.Rows[i][4]);
                    Log.WriteLog("updateDataTable : " + i + " : " + updateDataTable.Rows[i][5]);
                    Log.WriteLog("updateDataTable : " + i + " : " + updateDataTable.Rows[i][6]);
                    Log.WriteLog("updateDataTable : " + i + " : " + updateDataTable.Rows[i][7]);
                }

                Log.WriteLog("updateDataTable.Rows.Count : " + updateDataTable.Rows.Count);
            }
            else  //수정한 데이터값이 DataTable에 있으면 값수정 
            {
                row["ACSID"] = "" + dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                row["SERVER_NAME"] = "" + dataGridView1.Rows[e.RowIndex].Cells[1].Value;
                int serverType = getServerType(e.RowIndex);
                row["SERVER_TYPE"] = "" + serverType;
                row["P_IP"] = "" + dataGridView1.Rows[e.RowIndex].Cells[3].Value;
                row["S_IP"] = "" + dataGridView1.Rows[e.RowIndex].Cells[4].Value;
                row["SERVER_ID"] = "" + dataGridView1.Rows[e.RowIndex].Cells[5].Value;

                if (dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString().Length != 24)
                {
                    row["SERVER_PW"] = Bm_Main.setPw("" + dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                }
                else
                {
                    row["SERVER_PW"] = "" + dataGridView1.Rows[e.RowIndex].Cells[6].Value;
                }
                row["DB_NAME"] = "" + dataGridView1.Rows[e.RowIndex].Cells[7].Value;
                row["DB_ID"] = "" + dataGridView1.Rows[e.RowIndex].Cells[8].Value;
                if (dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString().Length != 24)
                {
                    row["DB_PW"] = Bm_Main.setPw("" + dataGridView1.Rows[e.RowIndex].Cells[9].Value);
                }
                else
                {
                    row["DB_PW"] = "" + dataGridView1.Rows[e.RowIndex].Cells[9].Value;
                }
                row["DB_HOST"] = "" + dataGridView1.Rows[e.RowIndex].Cells[10].Value;
                row["DB_HOST2"] = "" + dataGridView1.Rows[e.RowIndex].Cells[11].Value;
                row["DB_SERVICE"] = "" + dataGridView1.Rows[e.RowIndex].Cells[12].Value;

                for (int i = 0; i < updateDataTable.Rows.Count; i++)
                {
                    Log.WriteLog("updateDataTable : " + i + " : " + updateDataTable.Rows[i][0]);
                    Log.WriteLog("updateDataTable : " + i + " : " + updateDataTable.Rows[i][1]);
                    Log.WriteLog("updateDataTable : " + i + " : " + updateDataTable.Rows[i][2]);
                    Log.WriteLog("updateDataTable : " + i + " : " + updateDataTable.Rows[i][3]);
                    Log.WriteLog("updateDataTable : " + i + " : " + updateDataTable.Rows[i][4]);
                    Log.WriteLog("updateDataTable : " + i + " : " + updateDataTable.Rows[i][5]);
                    Log.WriteLog("updateDataTable : " + i + " : " + updateDataTable.Rows[i][6]);
                    Log.WriteLog("updateDataTable : " + i + " : " + updateDataTable.Rows[i][7]);
                }

                Log.WriteLog("updateDataTable.Rows.Count : " + updateDataTable.Rows.Count);
            }

      
          
        }

        

        public int getServerType(int getRow)
        {
            int serverType = 1;
            if (("" + this.dataGridView1.Rows[getRow].Cells[2].Value).Equals("PP4.0"))
            {
                serverType = 1;
            }
            else if (("" + this.dataGridView1.Rows[getRow].Cells[2].Value).Equals("PP4.6"))
            {
                serverType = 2;
            }
            else if (("" + this.dataGridView1.Rows[getRow].Cells[2].Value).Equals("온가드"))
            {
                serverType = 3;
            }
            else if (("" + this.dataGridView1.Rows[getRow].Cells[2].Value).Equals("지문"))
            {
                serverType = 4;
            }

            else if (("" + this.dataGridView1.Rows[getRow].Cells[2].Value).Equals("VM"))
            {
                serverType = 5;
            }

            return serverType;
        }

        public void showData()
        {
            updateFlag = false;
            
            string rCheck = req.select("BMS003", qm.getQuery("Sys_001_S"), "BMS");


            if (rCheck.Equals("0"))
            {
                dt = ReturnDT.dt;

              
            }

            rowNum = dt.Rows.Count;
            cNum = dt.Columns.Count;
            makeCell(this.dataGridView1, rowNum, cNum);

           
            if (rowNum > 0)
            {
                
                for (int i = 0; i < rowNum; i++)
                {
                    // AlarmActionRowUnit ua = (AlarmActionRowUnit)AdminDataClass.al[i];
                    //dgvCb = (DataGridViewComboBoxCell)dataGridView1.Rows[i].Cells[2];
                    //dgvCb.ErrorText = "";

                    this.dataGridView1.Rows[i].Cells[0].Value = "" + dt.Rows[i][0];
                    this.dataGridView1.Rows[i].Cells[1].Value = "" + dt.Rows[i][1];
                    this.dataGridView1.Rows[i].Cells[2].Value = "" + dt.Rows[i][2];
                    this.dataGridView1.Rows[i].Cells[3].Value = "" + dt.Rows[i][3];
                    this.dataGridView1.Rows[i].Cells[4].Value = "" + dt.Rows[i][4];
                    this.dataGridView1.Rows[i].Cells[5].Value = "" + dt.Rows[i][5];
                    this.dataGridView1.Rows[i].Cells[6].Value = "" + dt.Rows[i][6];
                    this.dataGridView1.Rows[i].Cells[7].Value = "" + dt.Rows[i][7];
                    this.dataGridView1.Rows[i].Cells[8].Value = "" + dt.Rows[i][8];
                    this.dataGridView1.Rows[i].Cells[9].Value = "" + dt.Rows[i][9];
                    this.dataGridView1.Rows[i].Cells[10].Value = "" + dt.Rows[i][10];
                    this.dataGridView1.Rows[i].Cells[11].Value = "" + dt.Rows[i][11];
                    this.dataGridView1.Rows[i].Cells[12].Value = "" + dt.Rows[i][12];


                }
            }


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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public string resultcheckDuplicationServerNum()
        {
            string result = "";
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (i == rowCnt)
                {
                    break;
                }
                if (this.dataGridView1.Rows[rowNum].Cells[0].Value.Equals("" + this.dataGridView1.Rows[i].Cells[0].Value)) 
                     result = "0";         
                else result = "1";
            }

            return result;
        }

        public void insertData()
        {
            int serverType = 1;
            if (dataGridView1.Rows[rowNum].Cells[0].Value.Equals(""))
            {
                MessageBox.Show("ACSID값은 반드시 입력해야합니다.");
                dataGridView1.Rows.RemoveAt(rowNum);
                queryType = "S";
                return;
            }

            if (dataGridView1.Rows[rowNum].Cells[1].Value.Equals(""))
            {
                MessageBox.Show("서버명은 반드시 입력해야합니다.");
                dataGridView1.Rows.RemoveAt(rowNum);
                queryType = "S";
                return;
            }

            if (dataGridView1.Rows[rowNum].Cells[2].Value.Equals(""))
            {
                MessageBox.Show("메인 IP는 반드시 입력해야합니다.");
                dataGridView1.Rows.RemoveAt(rowNum);
                queryType = "S";
                return;
            }

            if (dataGridView1.Rows[rowNum].Cells[6].Value.Equals(""))
            {
                MessageBox.Show("DB명은 반드시 입력해야합니다.");
                dataGridView1.Rows.RemoveAt(rowNum);
                queryType = "S";
                return;
            }

            if (dataGridView1.Rows[rowNum].Cells[7].Value.Equals(""))
            {
                MessageBox.Show("DB ID는 반드시 입력해야합니다.");
                dataGridView1.Rows.RemoveAt(rowNum);
                queryType = "S";
                return;
            }

            if (dataGridView1.Rows[rowNum].Cells[8].Value.Equals(""))
            {
                MessageBox.Show("DB 패스워드는 반드시 입력해야 합니다.");
                dataGridView1.Rows.RemoveAt(rowNum);
                queryType = "S";
                return;
            }

            string result = resultcheckDuplicationServerNum();
            if (result.Equals("O"))
            {
                MessageBox.Show("ACS ID값은 중복으로 입력할수없습니다");
                return;
            }

            

            getData.Add("ACSID", "" + this.dataGridView1.Rows[rowNum].Cells[0].Value);
            getData.Add("SERVER_NAME", "" + this.dataGridView1.Rows[rowNum].Cells[1].Value);
            serverType = getServerType(rowNum);
            getData.Add("SERVER_TYPE", "" + serverType);
            getData.Add("MAIN_IP", "" + this.dataGridView1.Rows[rowNum].Cells[3].Value);
            getData.Add("SUB_IP", "" + this.dataGridView1.Rows[rowNum].Cells[4].Value);
            getData.Add("SERVER_ID", "" + this.dataGridView1.Rows[rowNum].Cells[5].Value);
            getData.Add("SERVER_PASSWORD", Bm_Main.setPw("" + this.dataGridView1.Rows[rowNum].Cells[6].Value));
            getData.Add("DB_NAME", "" + this.dataGridView1.Rows[rowNum].Cells[7].Value);
            getData.Add("DB_ID", "" + this.dataGridView1.Rows[rowNum].Cells[8].Value);
            getData.Add("DB_PASSWORD", Bm_Main.setPw("" + this.dataGridView1.Rows[rowNum].Cells[9].Value));
            getData.Add("DB_HOST", "" + this.dataGridView1.Rows[rowNum].Cells[10].Value);
            getData.Add("DB_HOST2", "" + this.dataGridView1.Rows[rowNum].Cells[11].Value);
            getData.Add("DB_SERVICE", "" + this.dataGridView1.Rows[rowNum].Cells[12].Value);


            Log.WriteLog("ACS ID = " + getData["ACSID"]);
            Log.WriteLog("SRVER_NAME = " + getData["SERVER_NAME"]);
            Log.WriteLog("SERVER_TYPE= " + getData["SERVER_TYPE"]);
            Log.WriteLog("MAIN_IP = " + getData["MAIN_IP"]);
            Log.WriteLog("SUB_IP = " + getData["SUB_IP"]);
            Log.WriteLog("SERVER_ID = " + getData["SERVER_ID"]);
            Log.WriteLog("SERVER_PASSWORD= " + getData["SERVER_PASSWORD"]);
            Log.WriteLog("DB_NAME = " + getData["DB_NAME"]);
            Log.WriteLog("DB_ID = " + getData["DB_ID"]);
            Log.WriteLog("DB_PASSWORD = " + getData["DB_PASSWORD"]);
            Log.WriteLog("DB_HOST = " + getData["ACSID"]);
            Log.WriteLog("DB_SERVICE = " + getData["DB_SERVICE"]);

            string rCheck = req.update("BMS003", qm.getQuery("Sys_001_I", getData)+"^"+qm.getQuery("Sys_001_N_LOG",getData), "BMI");


            if (rCheck.Equals("0"))
            {

                MessageBox.Show("ACS정보가 정상적으로 반영되었습니다");

                

                dataGridView1.Rows.Clear();
                showData();
                
                rowNum = dt.Rows.Count;
                Log.WriteLog("rowNum : " + rowNum);
                
            
            }
            else
            {
                MessageBox.Show("ACS정보 변경 실패");

            }
            getData.Clear();
            queryType = "S";
        }

        public void updateData()
        {
            if (MessageBox.Show("지금까지 변경하신 사항을 저장하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;


                string rCheck = req.update("BMS003", qm.getQuery("Sys_001_U", updateDataTable) + "^" + qm.getQuery("Sys_001_M_LOG", updateDataTable), "BMI");

               
                if (rCheck.Equals("0"))
                {
                    MessageBox.Show("ACS정보가 정상적으로 반영되었습니다");

                    dataGridView1.Rows.Clear();
                    showData();




                }
                else
                {
                    MessageBox.Show("ACS정보 변경 실패");

                }

                queryType = "S";
                Cursor.Current = Cursors.Default;
            }
            else
            {
                return;
            }


        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (queryType.Equals("U")) updateData();
            queryType = "I";
            dataGridView1.Rows.Add(1);

            this.dataGridView1.Rows[rowNum].Cells[0].Value = "";
            this.dataGridView1.Rows[rowNum].Cells[1].Value = "";
            this.dataGridView1.Rows[rowNum].Cells[2].Value = "";
            this.dataGridView1.Rows[rowNum].Cells[3].Value = "";
            this.dataGridView1.Rows[rowNum].Cells[4].Value = "";
            this.dataGridView1.Rows[rowNum].Cells[5].Value = "";
            this.dataGridView1.Rows[rowNum].Cells[6].Value = "";
            this.dataGridView1.Rows[rowNum].Cells[7].Value = "";
            this.dataGridView1.Rows[rowNum].Cells[8].Value = "";
            this.dataGridView1.Rows[rowNum].Cells[9].Value = "";
            this.dataGridView1.Rows[rowNum].Cells[10].Value = "";
            this.dataGridView1.Rows[rowNum].Cells[11].Value = "";
            this.dataGridView1.Rows[rowNum].Cells[12].Value = "";
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {

            if (queryType.Equals("U"))
            {
                updateData();
                updateDataTable.Rows.Clear();
            }
            if (queryType.Equals("I"))
            {
                insertData();
            }
            queryType = "S";

        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {

           // if (queryType.Equals("U")) updateData();
            queryType = "D";
            if (MessageBox.Show("선택하신 시스템 정보을 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
                string serverNum = "" + this.dataGridView1.Rows[currentRowIndex].Cells[0].Value;
                string rCheck = req.update("BMS003", qm.getQuery("Sys_001_D", "" + this.dataGridView1.Rows[currentRowIndex].Cells[0].Value) + "^" + qm.getQuery("Sys_001_D_LOG",""+ this.dataGridView1.Rows[currentRowIndex].Cells[0].Value), "BMI");
               

                if (rCheck.Equals("0"))
                {
                    MessageBox.Show("ACS정보가 정상적으로 반영되었습니다");

                    dataGridView1.Rows.Clear();
                    showData();


                    
                    getData.Clear();
                    rowNum = dt.Rows.Count;
                    Log.WriteLog("rowNum : " + rowNum);

                    

                }
                else
                {
                    MessageBox.Show("ACS정보 변경 실패");
                }

                queryType = "S";
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
