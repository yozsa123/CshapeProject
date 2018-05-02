using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;

namespace BMClient
{
    public partial class BM_Result : Form
    {
        Util util = null;

        Color activeColor = Color.FromArgb(0xA9, 0xE2, 0xC5);
        int perPage = 1000;
        int seqNum = 0;
        int totalPage = 0;
        int currentPage = 0;
        Request req = null;
        DataTable[] sendExcel = new DataTable[1];
        public BM_Result()
        {
            InitializeComponent();

            util = new Util();

   
            req = new Request();
            
           
        }

       
        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void runSearch_Click(object sender, EventArgs e)
        {
            string countQuery = "select count(*) as SEQ from PROCESS_REQUEST where NAME like '%" + nameTextBox.Text + "%' and (BID like '%" + cardNumTextBox.Text + "%' or OLD_BID like '%" + cardNumTextBox.Text + "%') or  DEACTIVATE_DATE <= '"+endDate.Text+"'";
            string rcNumString = req.select("BMS011", countQuery, "BMS");

            if (rcNumString != "0")
            {
                MessageBox.Show("[Reg.cs] clickPostPage () 1. 장애 발생");
                return;
            }else{
                DataTable dt = ReturnDT.dt;

                MessageBox.Show("총 "+dt.Rows[0]["SEQ"] + "건이 검색되었습니다.");
                if (Convert.ToInt32("" + dt.Rows[0]["SEQ"]) > 500) MessageBox.Show("500건 이상의 처리내역을 보여주지 않습니다.");
                else if (Convert.ToInt32("" + dt.Rows[0]["SEQ"]) < 1) return;
            }


            string query = "select top 500 SEQ as 순번,"
                         + " case Q_TYPE when 'I' then '신규' when 'U' then '수정' end as 등록유형,"
                         + " OLD_BID as '기존카드번호', "
                         + " case BID when '-10' then Convert(varchar(16), '기존데이터유지') else BID end as 신규카드번호 ,"
                         + " case NAME when '-10' then '기존데이터유지' else NAME end as 이름, "
                         + " case BRITH when '1800-01-01' then convert(varchar(20),'기존데이터유지') else convert(varchar(10),BRITH,23) end as 생년월일,"
                         + " case gender when 1 then '남자' when 2 then '여자' else '기존데이터유지' end as 성별 , "
                         + " case SSNO when '-10' then '기존데이터유지' else  SSNO end as 사번 , "
                         + " case DEPARTMENT when -10 then '기존데이터유지' else CONVERT(varchar(12), DEPARTMENT) end as 소속,"
                         + " case DIVISION when -10 then '기존데이터유지' else CONVERT(varchar(12), DIVISION) end as 부서 ,"
                         + " case TITLE when -10 then '기존데이터유지' else CONVERT(varchar(12),TITLE) end as 직위 ,"
                         + " case EMAIL when '-10' then '기존데이터유지' else EMAIL end as 이메일, "
                         + " case TEL  when '-10' then '기존데이터유지' else TEL end as 연락처 ,"
                         + " case ADDRESS when '-10' then '기존데이터유지' else ADDRESS end as 주소 ,"
                         + " case DESCRIPTION when '-10' then '기존데이터유지' else DESCRIPTION end as 카드이름 ,"
                         + " PIN  as 비밀번호 ,"
                         + " case TYPE when -10 then '기존데이터유지' else CONVERT(varchar(12),TYPE) end as 카드타입,"
                         + " case STATUS_1 when -10 then '기존데이터유지' else CONVERT(varchar(12),STATUS_1) end as 카드상태,"
                         + " case FORMAT when -10 then '기존데이터유지' else CONVERT(varchar(12),FORMAT) end as 카드포멧,"
                         + " case ISSUE_REASON when -10 then '기존데이터유지' else CONVERT(varchar(12),ISSUE_REASON)end  as 발급사유 ,"
                         + " case ISSUE_TYPE when -10 then '기존데이터유지' else CONVERT(varchar(12),ISSUE_TYPE) end as 발급유형 ,"
                         + " case ACTIVATE_DATE when '1800-01-01' then convert(varchar(20),'기존데이터유지') else CONVERT(CHAR(10), ACTIVATE_DATE, 23) end as 유효시작일, "
                         + " case DEACTIVATE_DATE when '1800-01-01' then convert(varchar(20),'기존데이터유지') else CONVERT(CHAR(10), DEACTIVATE_DATE, 23) end as 만료일,"
                         + " case BYPASS_FLAG when -10 then '기존데이터유지' when 0 then '항시출입미사용' when 1 then '항시출입' end  as 항시출입여부 ,"
                         + " case ACS_1 when 1 then '등록' when 0 then '미등록' else '기존데이터유지' end  as '1발 등록여부' ,"
                         + " case ACS_2 when 1 then '등록' when 0 then '미등록' else '기존데이터유지' end  as '2발 등록여부' ,"
                         + " case ACS_3 when 1 then '등록' when 0 then '미등록' else '기존데이터유지' end  as '3발 등록여부' ,"
                         + " case ACS_5 when 1 then '등록' when 0 then '미등록' else '기존데이터유지' end  as 'MDM 등록여부' ,"
                         + " case FP_1 when 0 then '등록' when -1 then '미등록' else '기존데이터유지' end as '지문등록여부' , "
                         + " case VM when 1 then '등록' when 0 then '미등록' else '기존데이터유지' end as Vm등록여부 ,"
                         + " case RIGHT_1 when '-10' then '기존데이터유지' else RIGHT_1 end as '1발권한' , "
                         + " case RIGHT_2 when '-10' then '기존데이터유지' else RIGHT_2 end as '2발권한' , "
                         + " case RIGHT_3 when '-10' then '기존데이터유지' else RIGHT_3 end as '3발권한' , "
                         + " case ACS1_RESULT when 100 then '등록완료' when 0 then '미처리' else '처리오류' end as '1발처리결과',"
                         + " case ACS2_RESULT when 100 then '등록완료' when 0 then '미처리' else '처리오류' end as '2발처리결과',"
                         + " case ACS3_RESULT when 100 then '등록완료' when 0 then '미처리' else '처리오류' end as '3발처리결과',"
                         + " case FP1_RESULT when 100 then '등록완료' when 0 then '미처리' else '처리오류' end as '지문처리결과',"
                         + " case VM_RESULT when 100 then '등록완료' when 0 then '미처리' else '처리오류' end as 'VM처리결과',"
                         + " case PT when 1 then '상시출입증' when 2 then '일시출입증'else '기존데이터유지' end as 'VM 카드타입'"
                         + " from PROCESS_REQUEST"
                         + " where  NAME like '%" + nameTextBox.Text + "%' and (BID like '%" + cardNumTextBox.Text + "%' or OLD_BID like '%" + cardNumTextBox.Text + "%') or  DEACTIVATE_DATE <= '"+endDate.Text+"' order by SEQ";
            Log.WriteLogTmp(query);
                        
            rcNumString = req.select("BMS011", query, "BMS");

            if (rcNumString != "0")
            {
                MessageBox.Show("[Reg.cs] clickPostPage () 1. 장애 발생");
                return ;
            }
            else
            {

                
                ////////////////////////////////////////////////////////////////////////////
                DataRow[] rows = ReturnDT.dt.Select("소속 <> '기존데이터유지'");
                DataRow[] result = null;
                for (int i = 0; i < rows.Length; i++)
                {
                    result = Bm_Main.department.Select("ID = " + rows[i]["소속"]);
                    try
                    {
                        rows[i]["소속"] = "" + result[0][1];
                    }
                    catch (Exception ex)
                    {
                        rows[i]["소속"] = "미등록소속";
                    }
                }
                rows = ReturnDT.dt.Select("부서 <> '기존데이터유지'");
                for (int i = 0; i < rows.Length; i++)
                {
                    result = Bm_Main.division.Select("ID = " + rows[i]["부서"]);
                    try
                    {
                        rows[i]["부서"] = "" + result[0][1];
                    }
                    catch (Exception ex)
                    {
                        rows[i]["부서"] = "미등록부서";
                    }
                }
                rows = ReturnDT.dt.Select("직위 <> '기존데이터유지'");
                for (int i = 0; i < rows.Length; i++)
                {
                    try
                    {
                        result = Bm_Main.title.Select("ID = " + rows[i]["직위"]);
                        rows[i]["직위"] = "" + result[0][1];
                    }
                    catch (Exception ex)
                    {
                        rows[i]["직위"] = "미등록직위";
                    }
                }
                rows = ReturnDT.dt.Select("카드타입 <> '기존데이터유지'");
                for (int i = 0; i < rows.Length; i++)
                {
                    try
                    {
                        result = Bm_Main.cardType.Select("ID = " + rows[i]["카드타입"]);
                        rows[i]["카드타입"] = "" + result[0][1];
                    }
                    catch (Exception ex)
                    {
                        rows[i]["카드타입"] = "미등록타입" ;
                    }
                }
                rows = ReturnDT.dt.Select("카드상태 <> '기존데이터유지'");
                for (int i = 0; i < rows.Length; i++)
                {
                    try
                    {
                        result = Bm_Main.cardStat.Select("ID = " + rows[i]["카드상태"]);
                        rows[i]["카드상태"] = "" + result[0][1];
                    }
                    catch (Exception ex)
                    {
                        rows[i]["카드상태"] = "미등록카드상태"; 
                    }
                }
                rows = ReturnDT.dt.Select("카드포멧 <> '기존데이터유지'");
                for (int i = 0; i < rows.Length; i++)
                {
                    try
                    {
                        result = Bm_Main.cardFormat.Select("ID = " + rows[i]["카드포멧"]);
                        rows[i]["카드포멧"] = "" + result[0][1];
                    }
                    catch (Exception ex)
                    {
                        rows[i]["카드포멧"] = "미등록카드포멧";
                    }
                }
                rows = ReturnDT.dt.Select("발급사유 <> '기존데이터유지'");
                for (int i = 0; i < rows.Length; i++)
                {
                    try
                    {
                        result = Bm_Main.issueReason.Select("ID = " + rows[i]["발급사유"]);
                        rows[i]["발급사유"] = "" + result[0][1];
                    }
                    catch (Exception ex)
                    {
                        rows[i]["발급사유"] = "미등록발급사유";
                    }
                }
                rows = ReturnDT.dt.Select("발급유형 <> '기존데이터유지'");
                for (int i = 0; i < rows.Length; i++)
                {
                    try
                    {
                        result = Bm_Main.issueType.Select("ID = " + rows[i]["발급유형"]);
                        rows[i]["발급유형"] = "" + result[0][1];
                    }
                    catch (Exception)
                    {
                        rows[i]["발급유형"] = "미등록 발급유형";
                    }
                }


                sendExcel[0] = ReturnDT.dt;


                string[] str = new string[1];
                DataRow[] row;
                for (int k = 0; k < sendExcel.Length; k++)
                {
                    for (int i = 0; i < sendExcel[k].Rows.Count; i++)
                    {
                        str = ("" + sendExcel[k].Rows[i]["1발권한"]).Split('~');
                        if (str.Length > 1)
                        {
                            string right = "";
                            for (int j = 0; j < str.Length; j++)
                            {

                                try
                                {
                                    row = Bm_Main.ACS1AccessLvl.Select("ID = " + str[j]);
                                    str[j] = "" + row[0]["DESCRIPTION"];
                                    right += "," + str[j];
                                }
                                catch (Exception ex)
                                {
                                    if (!sendExcel[k].Rows[i]["1발권한"].Equals("기존데이터유지")) 
                                    sendExcel[k].Rows[i]["1발권한"] = "권한미확인";
                                }

                            }
                            sendExcel[k].Rows[i]["1발권한"] = right.Remove(0, 1);
                        }
                        else
                        {
                            str[0] = ("" + sendExcel[k].Rows[i]["1발권한"]);
                            if (str[0].Equals("-100")) sendExcel[k].Rows[i]["1발권한"] = "권한없음";
                            else
                            {
                                try
                                {
                                    row = Bm_Main.ACS1AccessLvl.Select("ID = " + str[0]);
                                    str[0] = "" + row[0]["DESCRIPTION"];
                                    sendExcel[k].Rows[i]["1발권한"] = str[0];
                                }
                                catch (Exception ex)
                                {
                                    if (!sendExcel[k].Rows[i]["1발권한"].Equals("기존데이터유지")) 
                                    sendExcel[k].Rows[i]["1발권한"] = "권한미확인";
                                }
                            }
                        }


                        str = ("" + sendExcel[k].Rows[i]["2발권한"]).Split('~');
                        if (str.Length > 1)
                        {
                            string right = "";
                            for (int j = 0; j < str.Length; j++)
                            {

                                try
                                {
                                    row = Bm_Main.ACS2AccessLvl.Select("ID = " + str[j]);
                                    str[j] = "" + row[0]["DESCRIPTION"];
                                    right += "," + str[j];
                                }
                                catch (Exception ex)
                                {
                                    if (!sendExcel[k].Rows[i]["2발권한"].Equals("기존데이터유지")) 
                                    sendExcel[k].Rows[i]["2발권한"] = "권한미확인";
                                }

                            }
                            sendExcel[k].Rows[i]["2발권한"] = right.Remove(0, 1);
                        }
                        else
                        {
                            str[0] = ("" + sendExcel[k].Rows[i]["2발권한"]);
                            if (str[0].Equals("-100")) sendExcel[k].Rows[i]["2발권한"] = "권한없음";
                            else
                            {
                                try
                                {
                                    row = Bm_Main.ACS2AccessLvl.Select("ID = " + str[0]);
                                    str[0] = "" + row[0]["DESCRIPTION"];
                                    sendExcel[k].Rows[i]["2발권한"] = str[0];
                                }
                                catch (Exception ex)
                                {
                                    if (!sendExcel[k].Rows[i]["2발권한"].Equals("기존데이터유지")) 
                                    sendExcel[k].Rows[i]["2발권한"] = "권한미확인";
                                }
                            }
                        }

                        str = ("" + sendExcel[k].Rows[i]["3발권한"]).Split('~');
                        if (str.Length > 1)
                        {
                            string right = "";
                            for (int j = 0; j < str.Length; j++)
                            {

                                try
                                {
                                    row = Bm_Main.ACS3AccessLvl.Select("ID = " + str[j]);
                                    str[j] = "" + row[0]["DESCRIPTION"];
                                    right += "," + str[j];
                                }
                                catch (Exception ex)
                                {
                                    if (!sendExcel[k].Rows[i]["3발권한"].Equals("기존데이터유지")) 
                                    sendExcel[k].Rows[i]["3발권한"] = "권한미확인";
                                }

                            }
                            sendExcel[k].Rows[i]["3발권한"] = right.Remove(0, 1);
                        }
                        else
                        {
                            str[0] = ("" + sendExcel[k].Rows[i]["3발권한"]);
                            if (str[0].Equals("-100")) sendExcel[k].Rows[i]["3발권한"] = "권한없음";
                            else
                            {
                                try
                                {
                                    row = Bm_Main.ACS3AccessLvl.Select("ID = " + str[0]);
                                    str[0] = "" + row[0]["DESCRIPTION"];
                                    sendExcel[k].Rows[i]["3발권한"] = str[0];
                                }
                                catch (Exception ex)
                                {
                                    if (!sendExcel[k].Rows[i]["3발권한"].Equals("기존데이터유지")) 
                                    sendExcel[k].Rows[i]["3발권한"] = "권한미확인";
                                }
                            }
                        }

                     


                    }
                }
                

                dataGridView1.DataSource = ReturnDT.dt;
                



             

               
            }
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

        private void nextLabel_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void beforeLabel_Click(object sender, EventArgs e)
        {

        }
        public string addZero(string org)
        {
            while (org.Length < 12)
            {
                org = "0" + org;
            }

            return org;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void excel_Click(object sender, EventArgs e)
        {

            if (sendExcel != null)
            {
                MessageBox.Show("검색후 데이터를 추출하세요.");

                return;
            }
            ArrayList list = new ArrayList();
            
            for (int i = 0; i < sendExcel.Length; i++)
            {
                list.Add(sendExcel[i]);
            }
            SendDataToExcel(list, "작업처리내역", sendExcel[0].Columns.Count, sendExcel[0].Rows.Count);
        }

        private void BM_Result_Load(object sender, EventArgs e)
        {

        }

    }
}
