using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;

namespace BMClient
{
    public partial class Bm_BatchProcessing : Form
    {

        string xlsFileName;

        OleDbConnection ExcelConn = null;
        string xlsfilename;
    
        DataSet ds = null;
        DataTable excelDataTable = null;

        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        Excel.Worksheet xlWorkSheet2;
        Excel.Worksheet xlWorkSheet3;
        Excel.Worksheet xlWorkSheet4;
        Excel.Worksheet xlWorkSheet5;
        Excel.Worksheet xlWorkSheet6;
        Excel.Worksheet xlWorkSheet7;
        Excel.Worksheet xlWorkSheet8;
        Excel.Worksheet xlWorkSheet9;

        Excel.Worksheet xlWorkSheet10;

        Request req = null;


        public Bm_BatchProcessing()
        {

            InitializeComponent();
            
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                FileDialog fileDlg = new OpenFileDialog();
                fileDlg.InitialDirectory = "C:\\"; //기본 디렉토리

                fileDlg.Filter = "모든 파일 (*.*)|*.*";

                fileDlg.RestoreDirectory = true;

                if (fileDlg.ShowDialog() == DialogResult.OK)
                { //if문을 않 닫아줄것이다 이유는 파일 선택과 동시에 모든 작업이 완료되게 만들기 위해서....

                    xlsfilename = fileDlg.FileName;
                    excelDataTable = exceldata(xlsfilename);

                    dataGridView1.DataSource = excelDataTable;

                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static DataTable exceldata(string filePath)
        {
            DataTable dtexcel = new DataTable();
            bool hasHeaders = false;
            string HDR = hasHeaders ? "Yes" : "No";
            string strConn;
            if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xlsx")
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
            else
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();



            OleDbCommand cmd = new OleDbCommand("select * from [카드정보$]", conn);

            OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            return dt;
        }

       

        

       

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            

            object misValue;

            xlApp = new Excel.Application();

            misValue = System.Reflection.Missing.Value;
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet2 = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(2);
            xlWorkSheet3 = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(3);
            xlWorkSheet4 = (Excel.Worksheet)xlWorkBook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            xlWorkSheet5 = (Excel.Worksheet)xlWorkBook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            xlWorkSheet6 = (Excel.Worksheet)xlWorkBook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            xlWorkSheet7= (Excel.Worksheet)xlWorkBook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            xlWorkSheet8 = (Excel.Worksheet)xlWorkBook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            xlWorkSheet9 = (Excel.Worksheet)xlWorkBook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            xlWorkSheet10 = (Excel.Worksheet)xlWorkBook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            xlWorkSheet.Name =  "카드정보";
            xlWorkSheet2.Name = "소속";
            xlWorkSheet3.Name = "부서";
            xlWorkSheet4.Name = "직위";
            xlWorkSheet5.Name = "카드상태";
            xlWorkSheet6.Name = "카드종류";
            xlWorkSheet7.Name = "카드포멧";
            xlWorkSheet8.Name = "발급사유";
            xlWorkSheet9.Name = "발급유형";
            xlWorkSheet10.Name = "권한정보";

            xlWorkSheet.Move(misValue, xlApp.Worksheets[1]);
            xlWorkSheet2.Move(misValue, xlApp.Worksheets[2]);
            xlWorkSheet3.Move(misValue, xlApp.Worksheets[3]);
            xlWorkSheet4.Move(misValue, xlApp.Worksheets[4]);
            xlWorkSheet5.Move(misValue, xlApp.Worksheets[5]);
            xlWorkSheet6.Move(misValue, xlApp.Worksheets[6]);
            xlWorkSheet7.Move(misValue, xlApp.Worksheets[7]);
            xlWorkSheet8.Move(misValue, xlApp.Worksheets[8]);
            xlWorkSheet9.Move(misValue, xlApp.Worksheets[9]);
            xlWorkSheet10.Move(misValue, xlApp.Worksheets[10]);

            xlWorkSheet.Select(misValue);
           
       
          
            makeHeader();


            // 상단 첫번째 Row 타이틀 볼드  
            SetHeaderBold(xlWorkSheet, 1);

            // 자동 넓이 지정  
           // (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1,1].Interior.ColorIndex = 15;

            xlApp.Visible = true;
            
         

        }

        public void makeHeader()
        {



            xlWorkSheet.Cells[1, 1] = "카 드 번 호";
            xlWorkSheet.Cells[1, 2] = "성    명";
            xlWorkSheet.Cells[1, 3] = "생    일";
            xlWorkSheet.Cells[1, 4] = "성  별";
            xlWorkSheet.Cells[1, 5] = "사    번";
            xlWorkSheet.Cells[1, 6] = "소    속";
            xlWorkSheet.Cells[1, 7] = "부    서";
            xlWorkSheet.Cells[1, 8] = "직    위";
            xlWorkSheet.Cells[1, 9] = "이  메  일";
            xlWorkSheet.Cells[1, 10] = "전  화  번  호";
            xlWorkSheet.Cells[1, 11] = "주  소";
            xlWorkSheet.Cells[1, 12] = "카 드 명";
        
            xlWorkSheet.Cells[1, 13] = "카 드 타 입";
            xlWorkSheet.Cells[1, 14] = "카 드 상 태";
    
            xlWorkSheet.Cells[1, 15] = "발 급 유 형";
            xlWorkSheet.Cells[1, 16] = "발 급 사 유";
            xlWorkSheet.Cells[1, 17] = "유효시작일";
            xlWorkSheet.Cells[1, 18] = "만  료  일";
            xlWorkSheet.Cells[1, 19] = "항시통과여부";
            xlWorkSheet.Cells[1, 20] = "지문등록여부";
            xlWorkSheet.Cells[1, 21] = "VM등록여부";
            xlWorkSheet.Cells[1, 22] = "1발등록여부";
            xlWorkSheet.Cells[1, 23] = "2발등록여부";
            xlWorkSheet.Cells[1, 24] = "3발등록여부";
            xlWorkSheet.Cells[1, 25] = "4발등록여부";
            xlWorkSheet.Cells[1, 26] = "MDM등록여부";
            xlWorkSheet.Cells[1, 27] = "일시출입자";
            xlWorkSheet.Cells[1, 28] = "1발출입권한";
            xlWorkSheet.Cells[1, 29] = "2발출입권한";
            xlWorkSheet.Cells[1, 30] = "3발출입권한";
            xlWorkSheet.Cells[1, 31] = "4발출입권한";

       
            

            /*
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1]).EntireColumn.ColumnWidth = 10;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 2]).EntireColumn.ColumnWidth = 10;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 3]).EntireColumn.ColumnWidth = 5;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 4]).EntireColumn.ColumnWidth = 10;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 5]).EntireColumn.ColumnWidth = 30;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 9]).EntireColumn.ColumnWidth = 20;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 10]).EntireColumn.ColumnWidth = 30;
            */

            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 2]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 3]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 4]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 5]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 6]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 7]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 8]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 9]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 10]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 11]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 12]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 13]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 14]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 15]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 16]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 17]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 18]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 19]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 20]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 21]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 22]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 23]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 24]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 25]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 26]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 27]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 28]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 29]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 30]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 31]).Interior.ColorIndex = 15;



            for (int i = 1; i < 34; i++) AutoFitColumn(xlWorkSheet, i);

            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 11]).EntireColumn.ColumnWidth = 30;


            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet2.Cells[1, 1]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet2.Cells[1, 2]).Interior.ColorIndex = 15;

            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet3.Cells[1, 1]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet3.Cells[1, 2]).Interior.ColorIndex = 15;

            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet4.Cells[1, 1]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet4.Cells[1, 2]).Interior.ColorIndex = 15;

            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet5.Cells[1, 1]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet5.Cells[1, 2]).Interior.ColorIndex = 15;

            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet6.Cells[1, 1]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet6.Cells[1, 2]).Interior.ColorIndex = 15;

            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet7.Cells[1, 1]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet7.Cells[1, 2]).Interior.ColorIndex = 15;

            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet8.Cells[1, 1]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet8.Cells[1, 2]).Interior.ColorIndex = 15;

            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet9.Cells[1, 1]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet9.Cells[1, 2]).Interior.ColorIndex = 15;

            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet10.Cells[1, 1]).Interior.ColorIndex = 15;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet10.Cells[1, 2]).Interior.ColorIndex = 15;

            xlWorkSheet2.Cells[1, 1] = "코드값";
            xlWorkSheet2.Cells[1, 2] = "소속명";

            xlWorkSheet3.Cells[1, 1] = "코드값";
            xlWorkSheet3.Cells[1, 2] = "부서명";

            xlWorkSheet4.Cells[1, 1] = "코드값";
            xlWorkSheet4.Cells[1, 2] = "직위명";

            xlWorkSheet5.Cells[1, 1] = "코드값";
            xlWorkSheet5.Cells[1, 2] = "카드상태";

            xlWorkSheet6.Cells[1, 1] = "코드값";
            xlWorkSheet6.Cells[1, 2] = "카드종류";

            xlWorkSheet7.Cells[1, 1] = "코드값";
            xlWorkSheet7.Cells[1, 2] = "카드포멧";

            xlWorkSheet8.Cells[1, 1] = "코드값";
            xlWorkSheet8.Cells[1, 2] = "발급사유";

            xlWorkSheet9.Cells[1, 1] = "코드값";
            xlWorkSheet9.Cells[1, 2] = "발급유형";


            xlWorkSheet10.Cells[1, 1] = "코드값";
            xlWorkSheet10.Cells[1, 2] = "권한정보";
            makeBody();

        }

        public void makeBody()
        {
            for (int i = 0; i < Bm_Main.department.Rows.Count; i++)
            {
                xlWorkSheet2.Cells[i + 2, 1] = Bm_Main.department.Rows[i][0];
                xlWorkSheet2.Cells[i + 2, 2] = Bm_Main.department.Rows[i][1];
            }

            for (int i = 0; i < Bm_Main.division.Rows.Count; i++)
            {
                xlWorkSheet3.Cells[i + 2, 1] = Bm_Main.division.Rows[i][0];
                xlWorkSheet3.Cells[i + 2, 2] = Bm_Main.division.Rows[i][1];
            }

            for (int i = 0; i < Bm_Main.title.Rows.Count; i++)
            {
                xlWorkSheet4.Cells[i + 2, 1] = Bm_Main.title.Rows[i][0];
                xlWorkSheet4.Cells[i + 2, 2] = Bm_Main.title.Rows[i][1];
            }

            for (int i = 0; i < Bm_Main.cardStat.Rows.Count; i++)
            {
                xlWorkSheet5.Cells[i + 2, 1] = Bm_Main.cardStat.Rows[i][0];
                xlWorkSheet5.Cells[i + 2, 2] = Bm_Main.cardStat.Rows[i][1];
            }

            for (int i = 0; i < Bm_Main.cardType.Rows.Count; i++)
            {
                xlWorkSheet6.Cells[i + 2, 1] = Bm_Main.cardType.Rows[i][0];
                xlWorkSheet6.Cells[i + 2, 2] = Bm_Main.cardType.Rows[i][1];
            }

            for (int i = 0; i < Bm_Main.cardFormat.Rows.Count; i++)
            {
                xlWorkSheet7.Cells[i + 2, 1] = Bm_Main.cardFormat.Rows[i][0];
                xlWorkSheet7.Cells[i + 2, 2] = Bm_Main.cardFormat.Rows[i][1];
            }

            for (int i = 0; i < Bm_Main.issueType.Rows.Count; i++)
            {
                xlWorkSheet8.Cells[i + 2, 1] = Bm_Main.issueType.Rows[i][0];
                xlWorkSheet8.Cells[i + 2, 2] = Bm_Main.issueType.Rows[i][1];
            }

            for (int i = 0; i < Bm_Main.issueReason.Rows.Count; i++)
            {
                xlWorkSheet9.Cells[i + 2, 1] = Bm_Main.issueReason.Rows[i][0];
                xlWorkSheet9.Cells[i + 2, 2] = Bm_Main.issueReason.Rows[i][1];
            }

            int rowCnt = 2;
            xlWorkSheet10.Cells[rowCnt , 1] = "1발전소 권한정보";
            for (int i = 0; i < Bm_Main.ACS1AccessLvl.Rows.Count; i++)
            {
                rowCnt = rowCnt + 1;
                xlWorkSheet10.Cells[rowCnt, 1] = Bm_Main.ACS1AccessLvl.Rows[i][0];
                xlWorkSheet10.Cells[rowCnt, 2] = Bm_Main.ACS1AccessLvl.Rows[i][1];
               
            }

            rowCnt += 1;
            xlWorkSheet10.Cells[rowCnt, 1] = "2발전소 권한정보";

            for (int i = 0; i < Bm_Main.ACS2AccessLvl.Rows.Count; i++)
            {
                rowCnt += 1;
                xlWorkSheet10.Cells[rowCnt, 1] = Bm_Main.ACS2AccessLvl.Rows[i][0];
                xlWorkSheet10.Cells[rowCnt, 2] = Bm_Main.ACS2AccessLvl.Rows[i][1];
               
            }

            rowCnt += 1;
            xlWorkSheet10.Cells[rowCnt, 1] = "3발전소 권한정보";

            for (int i = 0; i < Bm_Main.ACS3AccessLvl.Rows.Count; i++)
            {
                rowCnt += 1;
                xlWorkSheet10.Cells[rowCnt, 1] = Bm_Main.ACS3AccessLvl.Rows[i][0];
                xlWorkSheet10.Cells[rowCnt, 2] = Bm_Main.ACS3AccessLvl.Rows[i][1];
                
            }

            rowCnt += 1;
            xlWorkSheet10.Cells[rowCnt, 1] = "4발전소 권한정보";
            for (int i = 0; i < Bm_Main.ACS4AccessLvl.Rows.Count; i++)
            {
                rowCnt += 1;
                xlWorkSheet10.Cells[rowCnt, 1] = Bm_Main.ACS4AccessLvl.Rows[i][0];
                xlWorkSheet10.Cells[rowCnt, 2] = Bm_Main.ACS4AccessLvl.Rows[i][1];
                
            }

            xlWorkSheet.Cells.NumberFormat = "@";

        }

        public void SetHeaderBold(Excel.Worksheet worksheet, int row)
        {
            ((Excel.Range)worksheet.Cells[row, 1]).EntireRow.Font.Bold = true;
        }

        public void AutoFitColumn(Excel.Worksheet worksheet, int col)
        {
            ((Excel.Range)worksheet.Cells[1, col]).EntireColumn.AutoFit();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            try
            {

                FileDialog fileDlg = new OpenFileDialog();
                fileDlg.InitialDirectory = "C:\\"; //기본 디렉토리

                fileDlg.Filter = "모든 파일 (*.*)|*.*";

                fileDlg.RestoreDirectory = true;

                if (fileDlg.ShowDialog() == DialogResult.OK)
                { //if문을 않 닫아줄것이다 이유는 파일 선택과 동시에 모든 작업이 완료되게 만들기 위해서....

                    xlsfilename = fileDlg.FileName;
                    excelDataTable = exceldata(xlsfilename);

                    dataGridView1.DataSource = excelDataTable;
                }
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Boolean insertReg(int rowNum)
        {
          

            int orgCode = -1;
            int newCode = -1;
            string newDesc = "";
            string bid = addZero(""+this.dataGridView1.Rows[rowNum].Cells[0].Value);


            string protocolString = "100=" + bid + ",1=" + 0;

            
            // Log.WriteLogTmp("protocolString : " + protocolString);
            Boolean flag = false;

            string msg = "";

            string go = "  ▶  ";

            int num = 1;



            msg = msg + num + ". 성    명 : " + this.dataGridView1.Rows[rowNum].Cells[1].Value + go + this.dataGridView1.Rows[rowNum].Cells[1].Value + "\n"; num++;
            protocolString = protocolString + ",2=" + this.dataGridView1.Rows[rowNum].Cells[1].Value;




            string date =""+ this.dataGridView1.Rows[rowNum].Cells[2].Value;
            date = date.Replace("-", "");
            date = date.Substring(0, 8);
            msg = msg + num + ". 생년월일 : " + date + "\n"; num++;
            protocolString = protocolString + ",3=" + date;






            int gender = 0;

            if (("" + this.dataGridView1.Rows[rowNum].Cells[2].Value).Equals("여자") || ("" + this.dataGridView1.Rows[rowNum].Cells[2].Value).Equals("여")) gender = 2;
            else gender = 1;





            msg = msg + num + ". 성    별 : " + gender + "(" + gender + ")" + "\n"; num++;
            protocolString = protocolString + ",4=" + gender;


            msg = msg + num + ". 사원번호 : " + this.dataGridView1.Rows[rowNum].Cells[4].Value + "\n"; num++;
            protocolString = protocolString + ",5=" + this.dataGridView1.Rows[rowNum].Cells[4].Value;



            // if (!du.getDepartment().Equals(this.department.Text.Trim())) { msg = msg + num + ". 소    속 : " + du.getDepartment() + go + this.department.Text + "\n"; num++; }
            // Log.WriteLogTmp(".................... dept.SelectedIndex : " + CodeTable.department.Rows [department.SelectedIndex][1]);

            

            newCode = Convert.ToInt32(""+this.dataGridView1.Rows[rowNum].Cells[5].Value);

            DataRow[] result = Bm_Main.department.Select("ID = " + newCode);

            if (result.Length > 0) newDesc = "" + result[0][1];
            else
            {
                newDesc = "" + Bm_Main.department.Rows[0][1];
                newCode = Convert.ToInt32("" + Bm_Main.department.Rows[0][0]);
            }
            
            
            msg = msg + num + ". 소    속 : " + newCode + "  (" + newDesc + ")\n"; num++;
            protocolString = protocolString + ",6=" + newCode;




            newCode = Convert.ToInt32("" + this.dataGridView1.Rows[rowNum].Cells[6].Value);
            result = Bm_Main.division.Select("ID = " + newCode);

            if (result.Length > 0) newDesc = "" + result[0][1];
            else
            {
                newDesc = "" + Bm_Main.division.Rows[0][1];
                newCode = Convert.ToInt32("" + Bm_Main.division.Rows[0][0]);
            }

            // if (!du.getDivision().Equals(this.division.Text.Trim())) { msg = msg + num + ". 부    서 : " + du.getDivision() + go + this.division.Text + "\n"; num++; }
            msg = msg + num + ". 부    서 : " + newCode + "  (" + newDesc + ")\n"; num++;
            protocolString = protocolString + ",7=" + newCode;


            // if (!du.getTitle().Equals(this.title.Text.Trim())) { msg = msg + num + ". 직    위 : " + du.getTitle() + go + this.title.Text + "\n"; num++; }

            newCode = Convert.ToInt32("" + this.dataGridView1.Rows[rowNum].Cells[7].Value);

            result = Bm_Main.title.Select("ID = " + newCode);

            if (result.Length > 0) newDesc = "" + result[0][1];
            else
            {
                newDesc = "" + Bm_Main.title.Rows[0][1];
                newCode = Convert.ToInt32("" + Bm_Main.title.Rows[0][0]);
            }


            msg = msg + num + ". 직    위 : " + newCode + "  (" + newDesc + ")\n"; num++;
            protocolString = protocolString + ",8=" + newCode;



            msg = msg + num + ". 이 메 일 : " + this.dataGridView1.Rows[rowNum].Cells[8].Value + "\n"; num++;
            protocolString = protocolString + ",9=" + this.dataGridView1.Rows[rowNum].Cells[8].Value;



            msg = msg + num + ". 연 락 처 : " + this.dataGridView1.Rows[rowNum].Cells[9].Value + "\n"; num++;
            protocolString = protocolString + ",10=" + this.dataGridView1.Rows[rowNum].Cells[9].Value;




            msg = msg + num + ". 주    소 : " + this.dataGridView1.Rows[rowNum].Cells[10].Value + "\n"; num++;
            protocolString = protocolString + ",11=" + this.dataGridView1.Rows[rowNum].Cells[10].Value;



            byte[] tmpSazin = null;

            tmpSazin = getCurSazin();



            if (tmpSazin != null )
            {

                msg = msg + num + ". 사진 변경 / 추가 : " + true + "(" + tmpSazin.Length + ")\n";
              
                Gloval.newSazin = tmpSazin;

            

            }


            // sazinChange = false;






            msg = msg + num + ". 카드번호 : " + bid + "\n"; num++;
            protocolString = protocolString + ",0=" + bid;







            msg = msg + num + ". 카 드 명 : " + this.dataGridView1.Rows[rowNum].Cells[11].Value + "\n"; num++;
            protocolString = protocolString + ",13=" + this.dataGridView1.Rows[rowNum].Cells[11].Value;



            msg = msg + num + ". PIN      : " + 0 + "\n"; num++;
            protocolString = protocolString + ",14=" +0;


            // if (!du.getBadgeType().Equals(this.cardType.Text.Trim())) { msg = msg + num + ". 카드타입 : " + du.getBadgeType() + go + this.cardType.Text + "\n"; num++; }

            if (this.dataGridView1.Rows[rowNum].Cells[13].Value.Equals(DBNull.Value)) newCode = 1;
            else
            newCode = Convert.ToInt32("" + this.dataGridView1.Rows[rowNum].Cells[12].Value);
            
            
            result = Bm_Main.cardType.Select("ID = " + newCode);

            if (result.Length > 0) newDesc = "" + result[0][1];
            else
            {
                newDesc = "" + Bm_Main.cardType.Rows[0][1];
                newCode = Convert.ToInt32("" + Bm_Main.cardType.Rows[0][0]);
            }

            msg = msg + num + ". 카드타입 : " + newCode + "  (" + newDesc + ")\n"; num++;
            protocolString = protocolString + ",16=" + newCode;




            // if (!du.getBadgeStatus().Equals(this.cardStatus.Text.Trim())) { msg = msg + num + ". 카드상태 : " + du.getBadgeStatus() + go + this.cardStatus.Text + "\n"; num++; }

            if (this.dataGridView1.Rows[rowNum].Cells[14].Value.Equals(DBNull.Value)) newCode = 0;
            else

            newCode = Convert.ToInt32("" + this.dataGridView1.Rows[rowNum].Cells[13].Value);
            result = Bm_Main.cardStat.Select("ID = " + newCode);

            if (result.Length > 0) newDesc = "" + result[0][1];
            else
            {
                newDesc = "" + Bm_Main.cardStat.Rows[0][1];
                newCode = Convert.ToInt32("" + Bm_Main.cardStat.Rows[0][0]);
            }

            msg = msg + num + ". 카드상태 : " + newCode + "  (" + newDesc + ")\n"; num++;
            protocolString = protocolString + ",17=" + newCode;




            // if (!du.getBadgeFormat().Equals(this.cardFormat.Text.Trim())) { msg = msg + num + ". 카드포맷 : " + du.getBadgeFormat() + go + this.cardFormat.Text + "\n"; num++; }




            newCode = 2;
            result = Bm_Main.cardFormat.Select("ID = " + newCode);

            if (result.Length > 0) newDesc = "" + result[0][1];
            else
            {
                newDesc = "" + Bm_Main.cardFormat.Rows[0][1];
                newCode = Convert.ToInt32("" + Bm_Main.cardFormat.Rows[0][0]);
            }

            msg = msg + num + ". 카드포맷 : " + newCode + "  (" + newDesc + ")\n"; num++;
            protocolString = protocolString + ",18=" + newCode;




            // if (!du.getIssueType().Equals(this.issueType.Text.Trim())) { msg = msg + num + ". 발급유형 : " + du.getIssueType() + go + this.issueType.Text + "\n"; num++; }


            if (this.dataGridView1.Rows[rowNum].Cells[14].Value.Equals(DBNull.Value)) newCode = 1;
            else
            newCode = Convert.ToInt32("" + this.dataGridView1.Rows[rowNum].Cells[14].Value);

            result = Bm_Main.issueType.Select("ID = " + newCode);

            if (result.Length > 0) newDesc = "" + result[0][1];
            else
            {
                newDesc = "" + Bm_Main.issueType.Rows[0][1];
                newCode = Convert.ToInt32("" + Bm_Main.issueType.Rows[0][0]);
            }
            
            msg = msg + num + ". 발급유형 : " + newCode + "  (" + newDesc + ")\n"; num++;
            protocolString = protocolString + ",19=" + newCode;





            // if (!du.getIssueReason().Equals(this.issueReason.Text.Trim())) { msg = msg + num + ". 발급사유 : " + du.getIssueReason() + go + this.issueReason.Text + "\n"; num++; }

            if (this.dataGridView1.Rows[rowNum].Cells[15].Value.Equals(DBNull.Value)) newCode = 1;
            else
            newCode = Convert.ToInt32("" + this.dataGridView1.Rows[rowNum].Cells[15].Value);

            result = Bm_Main.issueReason.Select("ID = " + newCode);
            if (result.Length > 0) newDesc = "" + result[0][1];
            else
            {
                newDesc = "" + Bm_Main.issueReason.Rows[0][1];
                newCode = Convert.ToInt32("" + Bm_Main.issueReason.Rows[0][0]);
            }

            msg = msg + num + ". 발급사유 : " + newCode + "  (" + newDesc + ")\n"; num++;
            protocolString = protocolString + ",20=" + newCode;


            // if (!du.getActive().ToString().StartsWith(this.startDate.Text)) { msg = msg + num + ". 시 작 일 : " + du.getActive().ToString() + go + this.startDate.Text + "\n"; num++; }
            // if (!du.getDeactive().ToString().StartsWith(this.endDate.Text)) { msg = msg + num + ". 만 료 일 : " + du.getDeactive().ToString() + go + this.endDate.Text + "\n"; num++; }

            date = "" + this.dataGridView1.Rows[rowNum].Cells[16].Value;
            date = date.Replace("-", "");
            date = date.Substring(0, 8);


            msg = msg + num + ". 시 작 일 : " + date + "\n"; num++;
            protocolString = protocolString + ",21=" + date;


            date = "" + this.dataGridView1.Rows[rowNum].Cells[17].Value;
            date = date.Replace("-", "");
            date = date.Substring(0, 8);

            msg = msg + num + ". 만 료 일 : " + date + "\n"; num++;
            protocolString = protocolString + ",22=" + date;



            string tmpCheck = "0";


            tmpCheck = "" + this.dataGridView1.Rows[rowNum].Cells[18].Value;

         
            protocolString = protocolString + ",23=" + tmpCheck;




            tmpCheck = "-1";
            if (this.dataGridView1.Rows[rowNum].Cells[19].Value.Equals("1")) tmpCheck = "0";

             Log.WriteLogTmp("========================  " + ",  check : " + tmpCheck);
             msg = msg + num + ". FP등록 : " + tmpCheck + "\n"; num++;
             protocolString = protocolString + ",24=" + tmpCheck;
            

            tmpCheck = "0";

            if (this.dataGridView1.Rows[rowNum].Cells[20].Value.Equals("1")) tmpCheck = "1";
              

            msg = msg + num + ". VM등록 : " + tmpCheck + "\n"; num++;

            protocolString = protocolString + ",25=" + tmpCheck;


            tmpCheck = "0";


            if (this.dataGridView1.Rows[rowNum].Cells[21].Value.Equals("1")) tmpCheck = "1";

            msg = msg + num + ". 1발등록 : " + tmpCheck + "\n"; num++;
            protocolString = protocolString + ",26=" + tmpCheck;


            tmpCheck = "0";
            if (this.dataGridView1.Rows[rowNum].Cells[22].Value.Equals("1")) tmpCheck = "1";
            msg = msg + num + ". 2발등록 : " + tmpCheck + "\n"; num++;
            protocolString = protocolString + ",27=" + tmpCheck;


            tmpCheck = "0";
            if (this.dataGridView1.Rows[rowNum].Cells[23].Value.Equals("1")) tmpCheck = "1";

            msg = msg + num + ". 3발등록 : " + tmpCheck + "\n"; num++;
            protocolString = protocolString + ",28=" + tmpCheck;


            tmpCheck = "0";
            if (this.dataGridView1.Rows[rowNum].Cells[24].Value.Equals("1")) tmpCheck = "1";

            msg = msg + num + ". 4발등록 : " + tmpCheck + "\n"; num++;
            protocolString = protocolString + ",29=" + tmpCheck;

            tmpCheck = "0";
            if (this.dataGridView1.Rows[rowNum].Cells[25].Value.Equals("1")) tmpCheck = "1";

            msg = msg + num + ". MDM : " + tmpCheck + "\n"; num++;
            protocolString = protocolString + ",30=" + tmpCheck;



            tmpCheck = "1";
            if (!this.dataGridView1.Rows[rowNum].Cells[26].Value.Equals("1")) tmpCheck = "2";
            protocolString = protocolString + ",31=" + tmpCheck;










            /////////////////////////////////////////////// 권    한 ///////////////////////////////////////////////////////

            

            for (int i = 0; i < 4; i++)
            {
                if (!("" + this.dataGridView1.Rows[rowNum].Cells[27 + i].Value).Equals(""))
                {
                    if (this.dataGridView1.Rows[rowNum].Cells[21 + i].Value.ToString().Equals("1"))
                        protocolString = protocolString + "," + ((i + 1) * 1000) + "=" + "" + this.dataGridView1.Rows[rowNum].Cells[27 + i].Value;
                    else
                    {
                        protocolString = protocolString + "," + ((i + 1) * 1000) + "=" + "-100"; 
                    }
                }
                else
                    protocolString = protocolString + "," + ((i + 1) * 1000) + "=" + "-100"; 


            }

            /*
            if (("" + this.dataGridView1.Rows[rowNum].Cells[14].Value).Equals("0"))
            {
                msg = msg + num + ". 권    한 : \n" + this.dataGridView1.Rows[rowNum].Cells[27].Value + "\n"; num++;
                msg = msg + num + ". 권    한 : \n" + this.dataGridView1.Rows[rowNum].Cells[28].Value + "\n"; num++;
                msg = msg + num + ". 권    한 : \n" + this.dataGridView1.Rows[rowNum].Cells[29].Value + "\n"; num++;
                msg = msg + num + ". 권    한 : \n" + this.dataGridView1.Rows[rowNum].Cells[30].Value + "\n"; num++;
            }
            else
            {
                msg = msg + num + ". 권    한 : \n-100\n"; num++;
                msg = msg + num + ". 권    한 : \n-100\n"; num++;
                msg = msg + num + ". 권    한 : \n-100\n"; num++;
                msg = msg + num + ". 권    한 : \n-100\n"; num++;
            }  

            */


            // MessageBox.Show(titleString + msg);
            if (!msg.Equals(""))
            {
                // MessageBox.Show(titleString + msg);
              //  MessageBox.Show(protocolString);

                Gloval.protocolString = protocolString;

                flag = true;

                new Parser().insertParseCard(protocolString);
            }
            else
            {
                flag = false;
            }


            return flag;


        }

        public byte[] getCurSazin()
        {

            byte[] sazinImage = null;

            try
            {


                Bitmap nobody = new Bitmap("C:\\Nobody.png");
             

                MemoryStream ms = new MemoryStream();
                // sazin.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                nobody.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] tms = new byte[ms.Length];

                ms.Position = 0;
                ms.Read(tms, 0, Convert.ToInt32(ms.Length));

                sazinImage = tms;
                ms.Dispose();

                // MessageBox.Show("SUCCESS !");

                return sazinImage;

            }
            catch (Exception e2)
            {
                // MessageBox.Show(e2.ToString());
                return null;
            }

        }

      

        private void toolStripButton3_Click(object sender, EventArgs e)
        {



            req = new Request();
            if (checkDuplicate())
            {
                return;
            }

            if (dataGridView1.Rows.Count < 1)
            {
                MessageBox.Show("현재 일괄입력 대상이 없습니다.");
                return;
            }


            string rNumString = "";
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                insertReg(i);
                string query = "";
                if (Gloval.empQuery != null && !Gloval.empQuery.Equals(""))
                {
                    if (query.Equals("")) query = Gloval.empQuery;
                    else query = query + "^" + Gloval.empQuery;
                }
                if (Gloval.badgeQuery != null && !Gloval.badgeQuery.Equals(""))
                {
                    if (query.Equals("")) query = Gloval.badgeQuery;
                    else query = query + "^" + Gloval.badgeQuery;
                }
                if (Gloval.sazinQuery != null && !Gloval.sazinQuery.Equals(""))
                {
                    if (query.Equals("")) query = Gloval.sazinQuery;
                    else query = query + "^" + Gloval.sazinQuery;
                }

                query = Gloval.protocolString + "&" + query;
                // Log.WriteLogTmp("query 1 : " + query);
                int index = -1;
                index = query.IndexOf("&");
                
                rNumString = req.updateImage("BMS011", query, "BMSI", Gloval.newSazin);

                if (rNumString != "0")
                {
                    MessageBox.Show("장애 발생");
                    Cursor = Cursors.Default;
                    return;
                }


                

            }
            if (rNumString != "0")
            {
                MessageBox.Show(" 수정 / 입력 실패");

            }
            else
            {
                MessageBox.Show(" 수정 / 입력 성공");
            }
        }

        public string addZero(string org)
        {
            while (org.Length < 12)
            {
                org = "0" + org;
            }

            return org;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public Boolean checkDuplicate()
        {
            Boolean flag = true;


            string duplicateCheckQuery = "select BID from BADGE where BID = '" + addZero("" + this.dataGridView1.Rows[0].Cells[0].Value) + "'";

            string whereBid = "";

            for (int i = 1; i < excelDataTable.Rows.Count; i++)
            {

                whereBid += " or BID ='" + addZero("" + excelDataTable.Rows[i]["카 드 번 호"]) + "'";
            }

            duplicateCheckQuery = duplicateCheckQuery + whereBid;

            string rcNumString = req.select("BMS011", duplicateCheckQuery, "BMS");

            if (rcNumString !=
                "0")
            {
                MessageBox.Show("[Reg.cs] clickPostPage () 1. 장애 발생");
                return true;
            }
            else
            {
                if (ReturnDT.dt.Rows.Count > 0)
                {
                    MessageBox.Show("현재 BM 시스템에 등록되어있는 카드가 존재합니다. 관리자에게 문의하세요.");
                    string bid = ""+ ReturnDT.dt.Rows[0][0];
                    for (int i = 1; i < ReturnDT.dt.Rows.Count; i++)
                    {
                        bid += ", "+ReturnDT.dt.Rows[i][0];
                    }
                        MessageBox.Show("현재 BM시스템에 등록되어있는카드.\n "+bid);
                    return true;
                }
                else
                {
                   
                    return false;
                }
            }


           

        }
       
    }
}
