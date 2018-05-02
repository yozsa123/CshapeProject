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
    public delegate void selectResultCodeIndex(string desc, string codeType);
    
    public partial class Bm_SearchCodeForm : Form
    {
        string codeType = "";
        

        public event selectResultCodeIndex confirmButtonClick_Event_handler; 
 
        public Bm_SearchCodeForm(string _codeType)
        {
            InitializeComponent();

            codeType = _codeType;
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {

            confirmButtonClick_Event_handler(searchResultComboBox.SelectedItem.ToString(), codeType);

            this.Close();
        }


        public void search()
        {

            searchResultComboBox.Items.Clear();
            searchResultComboBox.Enabled = false;
            confirmButton.Enabled = false;

            DataRow[] dr = null;
            if (codeType.Equals("Department"))
            {
                dr = Bm_Main.department.Select("DESCRIPTION LIKE '%"+searchTextBox.Text + "%'");
            }
            else if (codeType.Equals("Division"))
            {
                dr = Bm_Main.division.Select("DESCRIPTION LIKE '%" + searchTextBox.Text + "%'");
            }


            if (dr.Length < 1)
            {
                MessageBox.Show("검색 결과가 없습니다..");
                return;
            }
            else
            {
                foreach (DataRow drs in dr)
                {
                    searchResultComboBox.Items.Add(drs["DESCRIPTION"]);
                }
                searchResultComboBox.SelectedIndex = 0;
                searchResultComboBox.Enabled = true;
                confirmButton.Enabled = true;
            }

        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            search();
        }

        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                search();
            }
           
        }



    }
}
