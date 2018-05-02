using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Collections;
namespace BMClient
{
    public partial class Bm_Rpt_005 : Form
    {

        private DataGridViewTextBoxColumn[][] topCell = null;
        string delimeter = "^";
 
        string queryType = "S";
        QueryMaker qm = new QueryMaker();
        Request req = new Request();
        int cNum = 2;
        DataTable dt;

        int rowNum = 300;

        int perPage = 1000;
        int lastSEQ = 0;
        int totalPage = 0;
        int currentPage = 0;

        DataTable[] resultDt;

        int cellHeight = 0;
        int count = 0;
        


        public Bm_Rpt_005()
        {
            InitializeComponent();
            ReportGridView.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvUserDetails_RowPostPaint);
            comboBox1.Items.Add("전체");
            comboBox1.Items.Add("시스템");
            comboBox1.Items.Add("등록");

            comboBox1.SelectedIndex = 0;
            txtID.KeyDown += new KeyEventHandler(txtID_KeyDown);
            txtRemark.KeyDown += new KeyEventHandler(txtID_KeyDown);

        }

        void txtID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) showData();
        }

       

       
        private void btn_Search_Click(object sender, EventArgs e)
        {
            
            showData();
            

        }


        private void dgvUserDetails_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(ReportGridView.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
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

            Cursor.Current = Cursors.WaitCursor;
            
            labelBefore.Visible = false;
            labelNext.Visible = false;
            resultDt = null;
            dt = null;
            int type = 0;
            if (comboBox1.Text.Equals("시스템")) type = 1;
            else if (comboBox1.Text.Equals("등록")) type = 2;

           // ReportGridView.Rows.Clear();
            
            string startDate = startDateTime.Value.ToShortDateString();
            string endDate = endDateTime.Value.ToShortDateString();
            startDate += " " + startTimePicker.Value.ToString("HH:mm:ss");
            endDate += " " + endTimePicker.Value.ToString("HH:mm:ss");

            Dictionary<string,string> sendData = new Dictionary<string,string>();
            sendData.Add("startDate", startDate);
            sendData.Add("endDate", endDate);
            sendData.Add("lastSEQ", "" + lastSEQ);
            sendData.Add("txtID", txtID.Text);
            sendData.Add("txtRemark", txtRemark.Text);
            sendData.Add("type", ""+type);


            string rCheck = req.select("BMS019", qm.getQuery("Rpt_005_Count",sendData ), "BMS");


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
                MessageBox.Show("요청중에 네트워크 장애가 발생하였습니다. 다시 시도해주세요.");
            }



             

             for (int i = 0; i < totalPage; i++)
             {
                 rCheck = req.select("BMS019", qm.getQuery("Rpt_005_S", sendData), "BMS");
                 resultDt[i] = ReturnDT.dt;
                 if (i != (totalPage-1)) lastSEQ = Convert.ToInt32(resultDt[i].Rows[perPage - 1][0]);
                 sendData["lastSEQ"] = ""+lastSEQ;

             }




             rowNum = resultDt[0].Rows.Count;
             cNum = resultDt[0].Columns.Count;

             currentPage = 0;
             labelBefore.Visible = false;
             labelNext.Visible = true;

             // setGridview(ReportGridView, resultDt[currentPage].Rows.Count, resultDt[currentPage].Columns.Count, resultDt[currentPage]);


             ReportGridView.DataSource = resultDt[currentPage];
             labelPage.Visible = true;
             labelPage.Text = (currentPage + 1) + " / " + totalPage;

             if (currentPage == totalPage - 1)
             {

                 labelNext.Visible = false;

             }
             txtRemark.Text = "";
             txtID.Text = "";
             Cursor.Current = Cursors.Default;
            


        }

        public void setGridview(DataGridView dgv, int rowNum, int cNum,DataTable setDt)
        {

            makeCell(dgv, rowNum, cNum);
            if (rowNum > 0)
            {
                for (int i = 0; i < rowNum; i++)
                {
                    // AlarmActionRowUnit ua = (AlarmActionRowUnit)AdminDataClass.al[i];

                    dgv.Rows[i].Cells[0].Value = "" + setDt.Rows[i][1];
                    dgv.Rows[i].Cells[1].Value = "" + setDt.Rows[i][2];
                    dgv.Rows[i].Cells[2].Value = "" + setDt.Rows[i][3];
                    dgv.Rows[i].Cells[3].Value = "" + setDt.Rows[i][4];
                    dgv.Rows[i].Cells[4].Value = "" + setDt.Rows[i][5];
                    dgv.Rows[i].Cells[5].Value = "" + setDt.Rows[i][6];
                    dgv.Rows[i].Cells[6].Value = "" + setDt.Rows[i][7];
                    dgv.Rows[i].Cells[7].Value = "" + setDt.Rows[i][8];




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

        private void btn_Print_Click(object sender, EventArgs e)
        {

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument1;
            printDialog.UseEXDialog = true;

            if (DialogResult.OK == printDialog.ShowDialog())
            {
                printDocument1.DocumentName = "운영관리 리포트";
                printDocument1.Print();
            }
           
            
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int leftMargin = e.MarginBounds.Left;
            int topMargin = e.MarginBounds.Top;
            bool morePagesToPrint = false;
            int tmpWidth = 0;
            int width = 10;
            int height = 20;
            for(int i = 0 ; i <ReportGridView.ColumnCount;i++){
                e.Graphics.DrawRectangle(Pens.Black, width, height, ReportGridView.Columns[i].Width, ReportGridView.Rows[0].Height);
                e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(width, height, ReportGridView.Columns[i].Width, ReportGridView.Rows[0].Height));
                e.Graphics.DrawString("  "+ReportGridView.Columns[i].HeaderText, ReportGridView.Font, Brushes.Black, new RectangleF(width, height, ReportGridView.Columns[i].Width, ReportGridView.Rows[i].Height));
                width += ReportGridView.Columns[i].Width;

            }


            int rowHeight = height + ReportGridView.Rows[0].Height;
            int rowWidth = 0;
            height += ReportGridView.Rows[0].Height;
            for (int i = 0; i < ReportGridView.RowCount; i++)
            {
                width = 10;
                for (int j = 0; j < ReportGridView.ColumnCount; j++)
                {
                    e.Graphics.DrawRectangle(Pens.Black, width, height, ReportGridView.Columns[j].Width, ReportGridView.Rows[i].Height);
                    e.Graphics.DrawString("  " + ReportGridView.Rows[i].Cells[j].Value, ReportGridView.Font, Brushes.Black, new RectangleF(width , height, ReportGridView.Columns[j].Width, ReportGridView.Rows[i].Height));
                    width += ReportGridView.Columns[j].Width;
                }
                height += ReportGridView.Rows[i].Height;
            }
            




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
            for (int k = 0; k < list.Count; k++)
            {

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

        private void button1_Click(object sender, EventArgs e)
        {
            string rCheck = req.select("BMS011", "delete from TB_EXTNL_REQ", "BMI");
            MessageBox.Show("rCheck = " + rCheck);
        }

        private void labelNext_Click(object sender, EventArgs e)
        {
            currentPage++;
            ReportGridView.DataSource = resultDt[currentPage];
            //  setGridview(ReportGridView, resultDt[currentPage].Rows.Count, resultDt[currentPage].Columns.Count, resultDt[currentPage]) ;
            labelPage.Text = (currentPage + 1) + " / " + totalPage;
            labelBefore.Visible = true;
            if (currentPage == totalPage - 1)
            {

                labelNext.Visible = false;

            }
        }

        private void labelBefore_Click(object sender, EventArgs e)
        {
            currentPage--;
             // setGridview(ReportGridView, resultDt[currentPage].Rows.Count, resultDt[currentPage].Columns.Count, resultDt[currentPage]);
            ReportGridView.DataSource = resultDt[currentPage];
            labelPage.Text = (currentPage + 1) + " / " + totalPage;

            labelNext.Visible = true;
            if (currentPage < 1)
            {
                labelBefore.Visible = false;

            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }




       
    }
}
