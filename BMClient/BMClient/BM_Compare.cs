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
    public partial class BM_Compare : Form
    {

        Util util = null;

        public BM_Compare()
        {
            InitializeComponent();

            util = new Util();

            fillDummyData();
        }

        public void fillDummyData()
        {
            int rNum = 21;
            int cNum = 9;

            dataGridView1.ClearSelection();
            util.makeCell(this.dataGridView1, rNum, cNum);


            dataGridView1.Rows[0].Cells[0].Value = "성  명";
            dataGridView1.Rows[1].Cells[0].Value = "BID";
            dataGridView1.Rows[2].Cells[0].Value = "카드타입";
            dataGridView1.Rows[3].Cells[0].Value = "카드상태";
            dataGridView1.Rows[4].Cells[0].Value = "카드포맷";
            dataGridView1.Rows[5].Cells[0].Value = "발급유형";
            dataGridView1.Rows[6].Cells[0].Value = "발급사유";
            dataGridView1.Rows[7].Cells[0].Value = "소  속";
            dataGridView1.Rows[8].Cells[0].Value = "부  서";
            dataGridView1.Rows[9].Cells[0].Value = "직  위";
            dataGridView1.Rows[10].Cells[0].Value = "사원번호";
            dataGridView1.Rows[11].Cells[0].Value = "연락처";
            dataGridView1.Rows[12].Cells[0].Value = "시작일";
            dataGridView1.Rows[13].Cells[0].Value = "종료일";
            dataGridView1.Rows[14].Cells[0].Value = "비밀번호";
            dataGridView1.Rows[15].Cells[0].Value = "지  문";
            dataGridView1.Rows[16].Cells[0].Value = "권  한";
            dataGridView1.Rows[17].Cells[0].Value = "권  한2";
            dataGridView1.Rows[18].Cells[0].Value = "권  한3";
            dataGridView1.Rows[19].Cells[0].Value = "권  한4";
            dataGridView1.Rows[20].Cells[0].Value = "권  한5";

            for (int i = 0; i < rNum; i++)
            {
                for (int j = 0; j < cNum; j++)
                {
                    if (j == 0) continue;
                    dataGridView1.Rows[i].Cells[j].Value = "";
                }

            }


            /*
            string name = "";
            string cardNum = "";


            int [] rBlue_0 = {3, 6, 8, 10, 12};
            int [] rBlue_1 = {3, 5, 7, 9, 11 };
            int [] rBlue_2 = {3, 10, 12, 14};
            int[] rBlue_3 = { 3, 4, 5, 6, 7, 8, 9, 10, 12 };
            int[] rBlue_4 = { 3, 9, 11 };
            int[] rBlue_5 = { 3, 6, 8, 9, 10, 12 };
            int[] rBlue_6 = { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            int[] rBlue_7 = { 3, 5, 7, 9, 11, 12 };
            int[] rBlue_8 = { 3, 4, 5, 12, 13, 14 };
            int[] rBlue_9 = { 3, 6, 7, 8, 9, 10, 11, 12 };

            int[][] rBlue = { rBlue_0, rBlue_1, rBlue_2, rBlue_3, rBlue_4, rBlue_5, rBlue_6, rBlue_7, rBlue_8, rBlue_9 };
            


            for (int i = 0; i < rNum; i++)
            {
                for (int j = 0; j < cNum; j++)
                {
                    if (i % 10 == 0) { name = "이영복"; cardNum = "003338126610"; }
                    else if (i % 10 == 1) { name = "신준호"; cardNum = "000971708290"; }
                    else if (i % 10 == 2) { name = "정성순"; cardNum = "000971391154"; }
                    else if (i % 10 == 3) { name = "홍명수"; cardNum = "000971412226"; }
                    else if (i % 10 == 4) { name = "길상섭"; cardNum = "000971508402"; }
                    else if (i % 10 == 5) { name = "고리-1234"; cardNum = "000971664594"; }
                    else if (i % 10 == 6) { name = "신고리-2245"; cardNum = "000971677346"; }
                    else if (i % 10 == 7) { name = "견학-6433"; cardNum = "000971407506"; }
                    else if (i % 10 == 8) { name = "일시-2890"; cardNum = "000971459250"; }
                    else if (i % 10 == 9) { name = "최태성"; cardNum = "000971655138"; }

                    if (j >= 0 && j < 3)
                    {
                        dataGridView1.Rows[i].Cells[0].Value = name;
                        dataGridView1.Rows[i].Cells[1].Value = cardNum;
                        dataGridView1.Rows[i].Cells[2].Value = DateTime.Now;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[j].Value = "";
                    }

                    if (i % 10 == 0)
                    {
                        for (int jj = 0; jj < rBlue[0].Length; jj++) dataGridView1.Rows[i].Cells[rBlue[0][jj]].Style.BackColor = activeColor;
                        dataGridView1.Rows[i].Cells[12].Value = 2;
                    }
                    else if (i % 10 == 1)
                    {
                        for (int jj = 0; jj < rBlue[1].Length; jj++) dataGridView1.Rows[i].Cells[rBlue[1][jj]].Style.BackColor = activeColor;
                        dataGridView1.Rows[i].Cells[12].Value = 0;
                    }
                    else if (i % 10 == 2)
                    {
                        for (int jj = 0; jj < rBlue[2].Length; jj++) dataGridView1.Rows[i].Cells[rBlue[2][jj]].Style.BackColor = activeColor;
                        dataGridView1.Rows[i].Cells[12].Value = 2;
                    }
                    else if (i % 10 == 3)
                    {
                        for (int jj = 0; jj < rBlue[3].Length; jj++) dataGridView1.Rows[i].Cells[rBlue[3][jj]].Style.BackColor = activeColor;
                        dataGridView1.Rows[i].Cells[12].Value = 1;
                    }
                    else if (i % 10 == 4)
                    {
                        for (int jj = 0; jj < rBlue[4].Length; jj++) dataGridView1.Rows[i].Cells[rBlue[4][jj]].Style.BackColor = activeColor;
                        dataGridView1.Rows[i].Cells[12].Value = 0;
                    }
                    else if (i % 10 == 5)
                    {
                        for (int jj = 0; jj < rBlue[5].Length; jj++) dataGridView1.Rows[i].Cells[rBlue[5][jj]].Style.BackColor = activeColor;
                        dataGridView1.Rows[i].Cells[12].Value = 2;
                    }
                    else if (i % 10 == 6)
                    {
                        for (int jj = 0; jj < rBlue[6].Length; jj++) dataGridView1.Rows[i].Cells[rBlue[6][jj]].Style.BackColor = activeColor;
                        dataGridView1.Rows[i].Cells[12].Value = 2;
                    }
                    else if (i % 10 == 7)
                    {
                        for (int jj = 0; jj < rBlue[7].Length; jj++) dataGridView1.Rows[i].Cells[rBlue[7][jj]].Style.BackColor = activeColor;
                        dataGridView1.Rows[i].Cells[12].Value = 2;
                    }
                    else if (i % 10 == 8)
                    {
                        for (int jj = 0; jj < rBlue[8].Length; jj++) dataGridView1.Rows[i].Cells[rBlue[8][jj]].Style.BackColor = activeColor;
                        dataGridView1.Rows[i].Cells[12].Value = 2;
                    }
                    else if (i % 10 == 9)
                    {
                        for (int jj = 0; jj < rBlue[9].Length; jj++) dataGridView1.Rows[i].Cells[rBlue[9][jj]].Style.BackColor = activeColor;
                        dataGridView1.Rows[i].Cells[12].Value = 2;
                    }
                }
            }

            */

        }
    

    }
}
