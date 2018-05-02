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
    public partial class BM_Reg_002 : Form
    {
        Request req;
        QueryMaker qm = new QueryMaker();
        string descrition = "";
        int addCount = 0;
        public BM_Reg_002()
        {
            InitializeComponent();
            req = new Request();
            radioDepartment.Checked = true;

            nameTextBox.KeyDown +=new KeyEventHandler(nameTextBox_KeyDown);
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);
        
        }

        void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > -1)
              descrition = "" + dataGridView1.Rows[e.RowIndex].Cells[1].Value;
            
        }

      

        

        public void search()
        {
            string rNumString = "";
            if (radioDepartment.Checked)
            {
                rNumString = req.select("BMS012", qm.getQuery("Bm_Department_R", "", nameTextBox.Text), "BMS");
            }
            else if (radioDivision.Checked)
            {
                rNumString = req.select("BMS012", qm.getQuery("Bm_Division_R", "", nameTextBox.Text), "BMS");
            }
            else if (radioTitle.Checked)
            {
                rNumString = req.select("BMS012", qm.getQuery("Bm_Title_R", "", nameTextBox.Text), "BMS");
            }


            if (rNumString != "0")
            {
                MessageBox.Show("서버와 통신중에 장애가 발생하였습니다.");

                return;
            }
            else
            {
                int cnt = ReturnDT.dt.Columns.Count;
                for (int i = 2; i < cnt; i++)
                {
                    ReturnDT.dt.Columns.RemoveAt(2);
                }
                dataGridView1.DataSource = ReturnDT.dt;
            }
        }

        private void codeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) search();
        }

        private void nameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) search();
        }

        private void BM_Reg_002_Load(object sender, EventArgs e)
        {
            nameTextBox.Focus();
        }

     

        private void searchButton_Click_1(object sender, EventArgs e)
        {
            search();
        }

        private void addButton_Click_1(object sender, EventArgs e)
        {
            string type = "";
            if (radioDepartment.Checked)
            {
                type = "소속";
            }
            else if (radioDivision.Checked)
            {
                type = "부서";
            }
            else if (radioTitle.Checked)
            {
                type = "직위";
            }
            BM_Reg_Code reg = new BM_Reg_Code(type);
            reg.ShowDialog();
        }

        private void closeButton_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void modifyButton_Click(object sender, EventArgs e)
        {
            string type = "";
            if (radioDepartment.Checked)
            {
                type = "소속";
            }
            else if (radioDivision.Checked)
            {
                type = "부서";
            }
            else if (radioTitle.Checked)
            {
                type = "직위";
            }
            if (descrition.Equals(""))
            {
                MessageBox.Show("변경하실 소속/부서/직위 명칭을 클릭하세요");
                return;
            }
            Bm_Code_Modify form = new Bm_Code_Modify(type,descrition);
            form.ShowDialog();

        }
    }
}