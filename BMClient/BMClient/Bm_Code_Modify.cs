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
    public partial class Bm_Code_Modify : Form
    {

         string type;
        string bmQuery = "";
        string ppQuery = "";
        string OGQuery = "";
        string FPQuery = "";
        Request req = new Request();
        string nowDate = "";
        QueryMaker qm = new QueryMaker();
        DataTable  dt = null;
        string set_Path = Application.StartupPath + @"\Hub_Setting.ini";
        string descrition = "";
        int pNum = 0;
        public Bm_Code_Modify(string _type, string _descrition)
        {
            InitializeComponent();
            type = _type;
            comboSetPlant.SelectedIndexChanged += new EventHandler(comboSetPlant_SelectedIndexChanged);
            comboSetPlant.SelectedIndex = 0;
            pNum = comboSetPlant.SelectedIndex+1;
            descrition = _descrition;
           

            topLabel.Text = "        " + type + " 변경";
            label1.Text = "발전소";
            button1.Text = "변경";
            nowDate = getDate(DateTime.Now);

            
        }

        void comboSetPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            pNum = comboSetPlant.SelectedIndex + 1;
            label2.Text = pNum + "발전소 " + type ;
            string serverType = "";
            if (pNum == 1) serverType = "" + Bm_Main.serverInfo.Rows[0]["SERVER_TYPE"];
            else if (pNum == 2) serverType = "" + Bm_Main.serverInfo.Rows[1]["SERVER_TYPE"];
            else if (pNum == 3) serverType = "" + Bm_Main.serverInfo.Rows[2]["SERVER_TYPE"];
            else if (pNum == 4) serverType = "" + Bm_Main.serverInfo.Rows[2]["SERVER_TYPE"];

            if (type.Equals("부서"))
            {
               
                comboResult.Items.Clear();

                if (serverType.Equals("3"))
                {
                    string rCheck = req.select("BMS003", qm.getQuery("OG_Division_R"), "ACS" + pNum + "S");
                    dt = ReturnDT.dt;
                   
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        
                        comboResult.Items.Add(dt.Rows[i]["NAME"]);
                    }
                }
                else
                {
                    string rCheck = req.select("BMS003", qm.getQuery("PP_Department_R"), "ACS" + pNum + "S");
                    dt = ReturnDT.dt;
                   
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                       
                        comboResult.Items.Add(dt.Rows[i]["description"].ToString().Trim());
                    }
                }

               

            }
            comboResult.SelectedIndex = 0;

        }


         public string getDate(DateTime dateTime)
        {
            string date = "";

            string month = "" + dateTime.Month;
            while (month.Length < 2) month = "0" + month;
            string day = "" + dateTime.Day;
            while (day.Length < 2) day = "0" + day;

            date = "" + dateTime.Year + month + day;
            return date;
        }

         private void button1_Click(object sender, EventArgs e)
         {
             string query = "";

             if (MessageBox.Show(descrition +" 코드값을 변경하시겠습니까?", "코드변경", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
             {
                 if (type.Equals("부서"))
                 {
                     query = "update DIVISION set PLANT" + pNum + "_DIVISION_CODE =" + dt.Rows[comboResult.SelectedIndex]["ID"] + " where DESCRIPTION='" + descrition + "'";
                 }
                 string rCheck = req.update("BMS012", query, "BMI");

                 if (rCheck.Equals("0"))
                 {
                     MessageBox.Show(type + " 변경 완료!");
                 }
                 else
                 {
                     MessageBox.Show(type + " 변경 실패..");
                 }
             }
             else
             {
                 return;
             }
             


            
         }

    }
}
