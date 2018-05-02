using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace BMClient
{
    public partial class BM_Reg_001 : Form
    {


        int rowNum = 300;
        int cNum = 2;

        int rowNum2 = 5;
        int cNum2 = 1;
        string msg = "";

        string curMode = "VIEW";
        string curTab = "CARD";

        Color viewColor = Color.FromArgb(0xe3, 0xd2, 0xff);    // Color.FromArgb(221, 221, 255);
        Color regColor = Color.FromArgb(0xA9, 0xE2, 0xC5);  // Color.FromArgb(0xfd, 0xf0, 0xff);   // Color.FromArgb(0xF8, 0xE5, 0xD0);
        Color addColor = Color.FromArgb(227, 255, 193);     // Color.FromArgb(0xA9, 0xE2, 0xC5);
        Color massColor = Color.FromArgb(227, 255, 193); 

        Util util = null;


        public BM_Reg_001()
        {
            InitializeComponent();

            util = new Util();

            dataGridView1.CellClick += new DataGridViewCellEventHandler(clickGridView1);
            plant1Grid.CellClick += new DataGridViewCellEventHandler(clickPlant1Grid);
            plant2Grid.CellClick += new DataGridViewCellEventHandler(clickPlant2Grid);
            plant3Grid.CellClick += new DataGridViewCellEventHandler(clickPlant3Grid);
            plant4Grid.CellClick += new DataGridViewCellEventHandler(clickPlant4Grid);

            cardAllCheck.Click += new EventHandler(cardAllCheckChange);

            rightSave.Click += new EventHandler(clickSave);

            fpSave.Click += new EventHandler(clickSave);

            TAB.Selected += new TabControlEventHandler(selectedTab);

            this.dataGridView1.SelectionChanged += new EventHandler(selectionChanged);


            reverseCheckBox.Visible = false;
            allCheckBox.CheckedChanged += new EventHandler(allChange);
            // reverseCheckBox.CheckedChanged += new EventHandler(reverseCheckBoxChange);
            plant1CheckBox.CheckedChanged += new EventHandler(plant1CheckBoxChange);
            plant2CheckBox.CheckedChanged += new EventHandler(plant2CheckBoxChange);
            plant3CheckBox.CheckedChanged += new EventHandler(plant3CheckBoxChange);
            plant4CheckBox.CheckedChanged += new EventHandler(plant4CheckBoxChange);

            
            regStartButton.Click += new EventHandler(clickRegStart);
            // addStartButton.Click += new EventHandler(clickAddStart);
            massChangeStart.Click += new EventHandler(clickMassChangeStart);
            save.Click += new EventHandler(clickSave);
            initButton.Click += new EventHandler (clear);
            initRight.Click += new EventHandler(clear);
            initFp.Click += new EventHandler(clear);

            save.Enabled = false;
            getRandomCardData();
            startDate.Value = DateTime.Now;
            endDate.Value = DateTime.Now.AddYears(30);






            nameRadioButton.CheckedChanged += new EventHandler(clickNameRadio);
            cardNumRadioButton.CheckedChanged += new EventHandler(clickCardNumRadio);
            ssnRadioButton.CheckedChanged += new EventHandler(clickSsnRadio);
            cardTypeRadioButton.CheckedChanged += new EventHandler(clickCardTypeRadio);

            rightSave.Enabled = fpSave.Enabled = false;


            cardNum.KeyDown += new KeyEventHandler(keyDownCardNum);
            cardType.SelectedValueChanged += new EventHandler(clickCardType);
            cardStatus.SelectedValueChanged += new EventHandler(clickCardStatus);
            cardFormat.SelectedValueChanged += new EventHandler(clickCardFormat);
            issueType.SelectedValueChanged += new EventHandler(clickIssueType);
            issueReason.SelectedValueChanged += new EventHandler(clickIssueReason);
            department.SelectedValueChanged += new EventHandler(clickDepartment);
            division.SelectedValueChanged += new EventHandler(clickDivision);
            title.SelectedValueChanged += new EventHandler(clickTitle);
        }

        private void keyDownCardNum (object sener, EventArgs e)
        {
            commonCardNum.Text = commonCardNum2.Text = commonCardNum3.Text = cardNum.Text;
        }

        private void clickGridView1(object sener, EventArgs e)
        {
            if (!curMode.Equals("MASS_CHANGE")) return;

            int rNum = dataGridView1.CurrentCell.RowIndex;
            int cNum = dataGridView1.CurrentCell.ColumnIndex;

            if (cNum != 2) return;
            if (rNum == dataGridView1.RowCount - 1) return;
            

            if (dataGridView1.Rows[rNum].Cells[2].Value.Equals(""))
            {
                dataGridView1.Rows[rNum].Cells[2].Value = "V";
                dataGridView1.Rows[rNum].Cells[0].Style.BackColor = Color.Yellow;
                dataGridView1.Rows[rNum].Cells[1].Style.BackColor = Color.Yellow;
                dataGridView1.Rows[rNum].Cells[2].Style.BackColor = Color.Yellow;

            }
            else
            {
                dataGridView1.Rows[rNum].Cells[2].Value = "";
                dataGridView1.Rows[rNum].Cells[0].Style.BackColor = Color.FromArgb(221, 221, 255);
                dataGridView1.Rows[rNum].Cells[1].Style.BackColor = Color.FromArgb(221, 221, 255);
                dataGridView1.Rows[rNum].Cells[2].Style.BackColor = Color.FromArgb(221, 221, 255);
            }
        }

        private void clickPlant1Grid(object sener, EventArgs e) { 
            util.checkRight(plant1Grid);
            Boolean flag = false;
            flag = allClearOrNot(plant1Grid);
            if (flag)
            {
                doPlantCheck(plant1Grid, false, plantTitle1);
                plant1CheckBox.ForeColor = Color.Black;
                plant1CheckBox.Checked = false;
            }
        }
        private void clickPlant2Grid(object sener, EventArgs e) { 
            util.checkRight(plant2Grid);
            Boolean flag = false;
            flag = allClearOrNot(plant2Grid);
            if (flag)
            {
                doPlantCheck(plant2Grid, false, plantTitle2);
                plant2CheckBox.ForeColor = Color.Black;
                plant2CheckBox.Checked = false;
            }
        }
        private void clickPlant3Grid(object sener, EventArgs e) { 
            util.checkRight(plant3Grid);
            Boolean flag = false;
            flag = allClearOrNot(plant3Grid);
            if (flag)
            {
                doPlantCheck(plant3Grid, false, plantTitle3);
                plant3CheckBox.ForeColor = Color.Black;
                plant3CheckBox.Checked = false;
            }
        }
        private void clickPlant4Grid(object sener, EventArgs e) { 
            util.checkRight(plant4Grid);
            Boolean flag = false;
            flag = allClearOrNot(plant4Grid);
            if (flag)
            {
                doPlantCheck(plant4Grid, false, plantTitle1);
                plant4CheckBox.ForeColor = Color.Black;
                plant4CheckBox.Checked = false;
            }
        }


        public Boolean allClearOrNot(DataGridView dgv)
        {
            for (int i = 0; i < dgv.RowCount; i++)
            {
                if (dgv.Rows[i].Cells[1].Value == "V") return false;

            }
            return true;
        }

        /*
        private void clickRightModify(object sender, EventArgs e)
        {
            initializeControl("VIEW");
            rightModify.Enabled = false;
            rightSave.Enabled = true;
        }
        private void clickFpModify(object sender, EventArgs e)
        {
            initializeControl("VIEW");
            fpModify.Enabled = false;
            fpSave.Enabled = true;
        }
        */


        private void clickRegStart(object sender, EventArgs e)
        { 
            
            
            if (curTab.Equals ("RIGHT") || curTab.Equals ("FP")) {
                name.Text = cardName2.Text = cardName3.Text = "";
                commonCardNum.Text = commonCardNum2.Text = commonCardNum3.Text = "";
                birthday.Text = birthday2.Text = birthday3.Text = "";
                gender.Text = gender2.Text = gender3.Text = "";

            }
            

            initializeControl("REG"); 
        }
        
        // private void clickAddStart(object sender, EventArgs e) { initializeControl("ADD"); }

        private void clickMassChangeStart(object sender, EventArgs e) {

            if (curTab.Equals ("RIGHT") || curTab.Equals ("FP")) {
                name.Text = cardName2.Text = cardName3.Text = "";
                commonCardNum.Text = commonCardNum2.Text = commonCardNum3.Text = "";
                birthday.Text = birthday2.Text = birthday3.Text = "";
                gender.Text = gender2.Text = gender3.Text = "";

            }
            

            initializeControl("MASS_CHANGE");
       }


        private void clickSave(object sender, EventArgs e)
        {
            Log.WriteLog("clickSave () ............ curMode : " + curMode);

            Boolean flag = false;

            if (curMode.Equals("REG") || curMode.Equals("ADD") || curMode.Equals("MASS_CHANGE"))
            {
                if (curMode.Equals("MASS_CHANGE")) flag = validatePlantGrid ();
                else flag = validatePlantCheckBox();

                if (!flag)
                {
                    MessageBox.Show(msg);
                    return;
                }
            }

            if (curMode.Equals("VIEW")) msg = "";

            string tmp = "";
            if (curMode.Equals("REG") || curMode.Equals("ADD")) tmp = "신규등록";
            else if (curMode.Equals("ADD")) tmp = "추가 등록";
            else if (curMode.Equals("VIEW")) tmp = "저장";
            else if (curMode.Equals("MASS_CHANGE")) tmp = "일괄 변경";

            save.Enabled = false;

            if (curMode.Equals("MASS_CHANGE"))
            {
                setColor(cardType, Color.Yellow);
                setColor(cardStatus, Color.Yellow);
                setColor(cardFormat, Color.Yellow);
                setColor(issueType, Color.Yellow);
                setColor(issueReason, Color.Yellow);
                setColor(department, Color.Yellow);
                setColor(division, Color.Yellow);
                setColor(title, Color.Yellow);
            }

            if (curMode.Equals("MASS_CHANGE") && TAB.SelectedTab == rightTab)
            {
                MessageBox.Show("권한에 대하여 일괄변경 주의하십시요");
            }
            else
            {
                MessageBox.Show(msg + "  " + tmp + " 하시겠습니까?");
            }

            initializeControl("VIEW");   // 수정


            MessageBox.Show(msg + " " + tmp + " 성공 " + curMode);

            // startCheck();

            // 수정, 등록, 발전추가된 카드정보를 DB에서 가져와서 화면에 뿌려줌
            // getDataRightFromDB(false);


            rightSave.Enabled = fpSave.Enabled = false;
            
            if (curMode.Equals("REG") && curTab.Equals("RIGHT"))
            {
                choicePlantGridView();
            }

            TAB.SelectedTab = cardTab;
            clearAllCardCheck();

            dataGridView1.ClearSelection();
            dataGridView1.Rows[0].Selected = true;
        }

        public void setColor(Control control, Color color)
        {
            if (!control.Text.Equals("현재데이터유지")) control.BackColor = color;
        }


        private void clear (object sender, EventArgs e)
        {
            TAB.SelectedTab = cardTab;
            clearAllCardCheck();
            clearAllCardData();
            clearAllPlantRight();

            initializeControl("VIEW");

            // getDataRightFromDB(false);
           
        }

        private void selectionChanged(object sender, EventArgs e)
        {
            // JSJ
            if (curMode.Equals("MASS_CHANGE")) return;

            
            
            initializeControl("VIEW");
            getDataRightFromDB(false);

            // choicePlantGridView();
        }

        public Boolean validatePlantCheckBox ()
        {
            Log.WriteLog("validatePlantCheckBox ()");
            msg = "";

            // if (curMode.Equals ("ADD")) 

            if (plant1CheckBox.Checked && plant1CheckBox.Enabled) msg = msg + "1발, , ";
            if (plant2CheckBox.Checked && plant2CheckBox.Enabled) msg = msg + "2발, ";
            if (plant3CheckBox.Checked && plant3CheckBox.Enabled) msg = msg + "3발, ";
            if (plant4CheckBox.Checked && plant4CheckBox.Enabled) msg = msg + "4발, ";

            if (msg.Equals("")) { msg = "1개 이상의 시스템을 선택하세요"; return false; }

            return true;
        }

        public Boolean validatePlantGrid ()
        {
            msg = "";

            if (plant1Grid.Enabled == false && plant2Grid.Enabled == false && plant3Grid.Enabled == false && plant4Grid.Enabled && false)
            {
                msg = "1개 이상의 발전소를 선택하세요";
                return false;
            }

            return true;
        }



        public void getDataRightFromDB(Boolean dbFlag)
        {

            Random r = new Random();
            int i = r.Next(0, 5);

            if (dbFlag)
            {
                name.Text = "카드명 from DB";
                engName.Text = "영문명 from DB";
                birthday.Text = "생일 from DB";
                cardNum.Text = "123456789012 from DB";
                issueNum.Text = "123 given by BM";
                pinNum.Text = "12334 from DB";
            }
            else
            {
                int rowNum = this.dataGridView1.CurrentCell.RowIndex;
                name.Text = cardName2.Text = cardName3.Text = "" + this.dataGridView1.Rows[rowNum].Cells[0].Value;

                if (rowNum % 3 == 0)
                {
                    engName.Text = "Lee Young Bok";
                    birthday.Text = birthday2.Text = birthday3.Text = "1980년 10월 10일";
                    cardType.Text = "상시출입증";
                    issueType.Text = "발급";
                    cardStatus.Text = "ACTIVE";
                    issueReason.Text = "분실";
                }
                else if (rowNum % 3 == 1)
                {
                    engName.Text = "Shin jung ho";
                    birthday.Text = birthday2.Text = birthday3.Text = "1971년 2월 3일";
                    cardType.Text = "일시출입증";
                    cardStatus.Text = "LOST";
                    issueType.Text = "회수";
                    issueReason.Text = "신규";
                }
                else if (rowNum % 3 == 2)
                {
                    engName.Text = "Jung Sung Sun";
                    birthday.Text = birthday2.Text = birthday3.Text = "1973년 5월 8일";
                    cardType.Text = "방문객카드";
                    cardStatus.Text = "SUSPENDED";
                    issueType.Text = "몰라";
                }


                if (i % 10 == 0) { commonCardNum.Text = "003338126610"; }
                else if (i == 1) { commonCardNum.Text = "000971708290"; }
                else if (i == 2) { commonCardNum.Text = "000971391154"; }
                else if (i == 3) { commonCardNum.Text = "000971412226"; }
                else if (i == 4) {  commonCardNum.Text = "000971508402"; }
                

                



            }

            clearAllPlantRight();

            Log.WriteLog("i========================================= : " + i);

            Color activeColor = Color.Red;

            if (i == 0)
            {
                plant2CheckBox.CheckState = plant4CheckBox.CheckState = CheckState.Checked;
                plant2CheckBox.ForeColor = plant4CheckBox.ForeColor = activeColor;
                // plant2CheckBox.Enabled = plant4CheckBox.Enabled = false;

                checkPlantRight(plant2Grid, 2, Color.Yellow);
                checkPlantRight(plant2Grid, 3, Color.Yellow);
                checkPlantRight(plant4Grid, 0, Color.Yellow);
                checkPlantRight(plant4Grid, 4, Color.Yellow);
            }
            else if (i == 1)
            {
                plant1CheckBox.CheckState = plant3CheckBox.CheckState = plant4CheckBox.CheckState = CheckState.Checked;
                plant1CheckBox.ForeColor = plant3CheckBox.ForeColor = plant4CheckBox.ForeColor = activeColor;
                
                checkPlantRight(plant1Grid, 0, Color.Yellow);
                checkPlantRight(plant1Grid, 1, Color.Yellow);
                checkPlantRight(plant3Grid, 2, Color.Yellow);
                checkPlantRight(plant4Grid, 3, Color.Yellow);
                checkPlantRight(plant4Grid, 4, Color.Yellow);
                
            }
            else if (i == 3)
            {
                plant2CheckBox.CheckState = plant3CheckBox.CheckState = plant4CheckBox.CheckState = CheckState.Checked;
                plant2CheckBox.ForeColor = plant3CheckBox.ForeColor = plant4CheckBox.ForeColor = activeColor;
                // plant2CheckBox.Enabled = plant3CheckBox.Enabled = plant4CheckBox.Enabled = false;

                checkPlantRight(plant2Grid, 3, Color.Yellow);
                checkPlantRight(plant2Grid, 4, Color.Yellow);
                checkPlantRight(plant3Grid, 0, Color.Yellow);
                checkPlantRight(plant4Grid, 2, Color.Yellow);
            }
            else
            {
                plant2CheckBox.CheckState = plant1CheckBox.CheckState = CheckState.Checked;
                plant2CheckBox.ForeColor = plant1CheckBox.ForeColor = activeColor;
                // plant2CheckBox.Enabled = plant1CheckBox.Enabled = false;

                checkPlantRight(plant1Grid, 3, Color.Yellow);
                checkPlantRight(plant2Grid, 3, Color.Yellow);
                checkPlantRight(plant2Grid, 2, Color.Yellow);
            }
            
        }
        

        private void reverseCheckBoxChange(object sender, EventArgs e)
        {
            if (plant1CheckBox.Checked) { plant1CheckBox.Checked = false; plant1CheckBox.ForeColor = Color.Red; }
            else { plant1CheckBox.Checked = true; plant1CheckBox.ForeColor = Color.Black; }

            if (plant2CheckBox.Checked) { plant2CheckBox.Checked = false; plant2CheckBox.ForeColor = Color.Red; }
            else { plant2CheckBox.Checked = true; plant2CheckBox.ForeColor = Color.Black; }

            if (plant3CheckBox.Checked) { plant3CheckBox.Checked = false; plant3CheckBox.ForeColor = Color.Red; }
            else { plant3CheckBox.Checked = true; plant3CheckBox.ForeColor = Color.Black; }

            if (plant4CheckBox.Checked) { plant4CheckBox.Checked = false; plant4CheckBox.ForeColor = Color.Red; }
            else { plant4CheckBox.Checked = true; plant4CheckBox.ForeColor = Color.Black; }

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////





        public void getRandomCardData()
        {

            dataGridView1.ClearSelection();
            util.makeCell(this.dataGridView1, rowNum, cNum);



            if (rowNum > 0)
            {
                Random r = new Random();

                for (int j = 0; j < rowNum; j++)
                {

                    int i = r.Next(0, 10);


                    // AlarmActionRowUnit ua = (AlarmActionRowUnit)AdminDataClass.al[i];
                    string name = "";

                    if (i % 10 == 0) { name = "이영복"; commonCardNum.Text = "003338126610"; }
                    else if (i == 1) { name = "신준호"; commonCardNum.Text = "000971708290"; }
                    else if (i == 2) { name = "정성순"; commonCardNum.Text = "000971391154"; }
                    else if (i == 3) { name = "홍명수"; commonCardNum.Text = "000971412226"; }
                    else if (i == 4) { name = "길상섭"; commonCardNum.Text = "000971508402"; }
                    else if (i == 5) { name = "고리-1234"; commonCardNum.Text = "000971664594"; }
                    else if (i == 6) { name = "신고리-2245"; commonCardNum.Text = "000971677346"; }
                    else if (i == 7) { name = "견학-6433"; commonCardNum.Text = "000971407506"; }
                    else if (i == 8) { name = "일시-2890"; commonCardNum.Text = "000971459250"; }
                    else if (i == 9) { name = "최태성"; commonCardNum.Text = "000971655138"; }

                    Log.WriteLog("" + name + ", " + cardNum.Text);


                    this.dataGridView1.Rows[j].Cells[0].Value = "" + name;
                    this.dataGridView1.Rows[j].Cells[1].Value = "" + "정보시스템팀";

                    this.dataGridView1.Rows[j].Cells[2].Value = "";

                }
            }


            util.makeCell(this.plant1Grid, rowNum2, cNum2);

            if (rowNum2 > 0)
            {
                for (int i = 0; i < rowNum2; i++)
                {

                    this.plant1Grid.Rows[i].Cells[0].Value = "" + ((i + 1) * 10) + " 권한";
                    this.plant1Grid.Rows[i].Cells[1].Value = "";
                }
            }

            util.makeCell(this.plant2Grid, rowNum2, cNum2);
            if (rowNum2 > 0)
            {
                for (int i = 0; i < rowNum2; i++)
                {
                    this.plant2Grid.Rows[i].Cells[0].Value = "" + ((i + 1) * 10) + " 권한";
                    this.plant2Grid.Rows[i].Cells[1].Value = "";
                }
            }

            util.makeCell(this.plant3Grid, rowNum2, cNum2);
            if (rowNum2 > 0)
            {
                for (int i = 0; i < rowNum2; i++)
                {
                    this.plant3Grid.Rows[i].Cells[0].Value = "출입등급 " + ((i + 1) * 10);
                    this.plant3Grid.Rows[i].Cells[1].Value = "";
                }
            }

            util.makeCell(this.plant4Grid, rowNum2, cNum2);
            if (rowNum2 > 0)
            {
                for (int i = 0; i < rowNum2; i++)
                {
                    this.plant4Grid.Rows[i].Cells[0].Value = "출입등급 " + ((i + 1) * 10);
                    this.plant4Grid.Rows[i].Cells[1].Value = "";
                }
            }
            clearAllPlantRight();
        }







        private void selectedTab(object sender, TabControlEventArgs e)
        {

            if (curMode.Equals("ADD"))
            {
                TAB.SelectedTab = cardTab;
                return;
            }


            choicePlantGridView();
            Boolean flag = false;

            commonCardNum.Text = commonCardNum2.Text = commonCardNum3.Text = cardNum.Text;


            if (e.TabPage == cardTab)
            {
                // flag = validData();
                // if (!flag) return;

                curTab = "CARD";


            }
            else if (e.TabPage == rightTab)
            {

                // if (curMode.Equals("REG") || 
                    
                // if (curMode.Equals("MASS_CHANGE")) clearAllPlantRight();


                flag = validData();
                if (!flag) return;

                curTab = "RIGHT";

                cardName2.Text = name.Text.Trim();
                commonCardNum2.Text = commonCardNum.Text.Trim();
                birthday2.Text = birthday.Text.Trim();
                gender2.Text = gender.Text.Trim();

                commonGroupBox2.Enabled = false;

                if (curMode.Equals("VIEW") || curMode.Equals("ADD") || curMode.Equals("REG") || curMode.Equals("MASS_CHANGE"))
                {
                    rightSave.Enabled = true;
                }
                else
                {
                    rightSave.Enabled = false;
                }

                
                doPlantCheck(plant1Grid, plant1CheckBox.Checked, plantTitle1);
                doPlantCheck(plant2Grid, plant2CheckBox.Checked, plantTitle2);
                doPlantCheck(plant3Grid, plant3CheckBox.Checked, plantTitle3);
                doPlantCheck(plant4Grid, plant4CheckBox.Checked, plantTitle4);




            }
            else if (e.TabPage == fpTab)
            {
                flag = validData();
                if (!flag) return;


                curTab = "FP";

                cardName3.Text = name.Text.Trim();
                commonCardNum3.Text = commonCardNum.Text.Trim();
                birthday3.Text = birthday.Text.Trim();
                gender3.Text = gender.Text.Trim();

                commonGroupBox3.Enabled = false;

                if (curMode.Equals("VIEW") || curMode.Equals("ADD") || curMode.Equals("REG") || curMode.Equals("MASS_CHANGE"))
                {
                    fpSave.Enabled = true;
                }
                else
                {
                    fpSave.Enabled = false;
                }

            }

        }

        private void allChange(object sender, EventArgs e)
        {
            Boolean flag = allCheckBox.Checked;

            // plant1CheckBox.CheckState = plant2CheckBox.CheckState = plant3CheckBox.CheckState = plant4CheckBox.CheckState = CheckState.Checked;
            plant1CheckBox.Checked = plant2CheckBox.Checked = plant3CheckBox.Checked = plant4CheckBox.Checked = flag;
            plant1CheckBox.Enabled = plant2CheckBox.Enabled = plant3CheckBox.Enabled = plant4CheckBox.Enabled = true;

            Color c = Color.Black;
            if (flag) c = Color.Red;
            else c = Color.Black;

            plant1CheckBox.ForeColor = plant2CheckBox.ForeColor = plant3CheckBox.ForeColor = plant4CheckBox.ForeColor = c;

        }

        public void doPlantCheck (DataGridView dgv, Boolean checkFlag, Label title)
        {
            if (checkFlag)
            {
                title.ForeColor = Color.Red;
                dgv.Enabled = true;
                util.changeFont(title, 1);
            }
            else
            {
                title.ForeColor = Color.Black;
                dgv.Enabled = false;
                util.changeFont(title, 0);


                int rNum = dgv.RowCount;
                dgv.ClearSelection();
                for (int i = 0; i < rNum; i++)
                {
                    dgv.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(221, 221, 255);
                    dgv.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(221, 221, 255);
                    dgv.Rows[i].Cells[1].Value = "";
                }
            }
        }

        private void plant1CheckBoxChange(object sender, EventArgs e)
        {
            if (curMode.Equals("VIEW") || curMode.Equals("REG") || curMode.Equals ("MASS_CHANGE"))
            {
                if (curTab.Equals("RIGHT")) {
                    if (plant1CheckBox.Checked)
                    {
                        plant1CheckBox.ForeColor = Color.Red;
                        doPlantCheck(plant1Grid, true, plantTitle1);
                    }
                    else
                    {
                        plant1CheckBox.ForeColor = Color.Black;
                        doPlantCheck(plant1Grid, false, plantTitle1);
                    }
                }
            }

        }

        private void plant2CheckBoxChange(object sender, EventArgs e)
        {
            if (curMode.Equals("VIEW") || curMode.Equals("REG") || curMode.Equals("MASS_CHANGE"))
            {
                if (curTab.Equals("RIGHT"))
                {
                    if (plant2CheckBox.Checked)
                    {
                        plant2CheckBox.ForeColor = Color.Red;
                        doPlantCheck(plant2Grid, true, plantTitle2);
                    }
                    else {
                        plant2CheckBox.ForeColor = Color.Black;
                        doPlantCheck(plant2Grid, false, plantTitle2);
                    }
                }
            }
        }

        private void plant3CheckBoxChange(object sender, EventArgs e)
        {
            if (curMode.Equals("VIEW") || curMode.Equals("REG") || curMode.Equals("MASS_CHANGE"))
            {
                if (curTab.Equals("RIGHT"))
                {
                    if (plant3CheckBox.Checked)
                    {
                        plant3CheckBox.ForeColor = Color.Red;
                        doPlantCheck(plant3Grid, true, plantTitle3);
                    }
                    else
                    {
                        plant3CheckBox.ForeColor = Color.Black;
                        doPlantCheck(plant3Grid, false, plantTitle3);
                    }
                }
            }
        }

        private void plant4CheckBoxChange(object sender, EventArgs e)
        {
            if (curMode.Equals("VIEW") || curMode.Equals("REG") || curMode.Equals("MASS_CHANGE"))
            {
                if (curTab.Equals("RIGHT"))
                {
                    if (plant4CheckBox.Checked)
                    {
                        plant4CheckBox.ForeColor = Color.Red;
                        doPlantCheck(plant4Grid, true, plantTitle4);
                    }
                    else
                    {
                        plant4CheckBox.ForeColor = Color.Black;
                        doPlantCheck(plant4Grid, false, plantTitle4);
                    }
                }
            }
        }


        public void checkPlantRight(DataGridView dgv, int rowNum, Color c)
        {
            dgv.Rows[rowNum].Cells[0].Style.BackColor = c;
            dgv.Rows[rowNum].Cells[1].Style.BackColor = c;
            dgv.Rows[rowNum].Cells[1].Value = "V";
        }

        public void clearAllPlantRight()
        {
            DataGridView[] dg = { plant1Grid, plant2Grid, plant3Grid, plant4Grid };
            CheckBox[] cb = { plant1CheckBox, plant2CheckBox, plant3CheckBox, plant4CheckBox };

            allCheckBox.CheckState = CheckState.Unchecked;

            doPlantCheck(dg [0], false, plantTitle1);
            doPlantCheck(dg [1], false, plantTitle2);
            doPlantCheck(dg [2], false, plantTitle3);
            doPlantCheck(dg [3], false, plantTitle4);

            for (int j = 0; j < 4; j++)  {
                cb[j].ForeColor = Color.Black;
                cb[j].CheckState = CheckState.Unchecked;
		        dg[j].ClearSelection();
		    }

        }

        
        public void clearAllCardData()
        {

            Log.WriteLog("[Reg.cs] clearAllCardData ()");

            name.Text = engName.Text = birthday.Text = cardNum.Text = commonCardNum.Text = issueNum.Text = pinNum.Text = "";
            name.Text = ssn.Text = tel.Text = address.Text = "";

            cardType.Text = cardStatus.Text = cardFormat.Text = issueType.Text = issueReason.Text = "선택";

            startDate.Value = DateTime.Now;
            endDate.Value = DateTime.Now.AddYears(30);
        }



        private void clickNameRadio(object sender, EventArgs e)
        {
            disableRadio();
            nameTextBox.Enabled = true;
        }

        private void clickSsnRadio(object sender, EventArgs e)
        {
            disableRadio();
            ssnTextBox.Enabled = true;
        }

        private void clickCardNumRadio(object sender, EventArgs e)
        {
            disableRadio();
            cardNumTextBox.Enabled = true;
        }

        private void clickCardTypeRadio(object sender, EventArgs e)
        {
            disableRadio();
            cardTypeComboBox.Enabled = true;
        }

        public void disableRadio()
        {
            nameTextBox.Enabled = false;
            cardNumTextBox.Enabled = false;
            ssnTextBox.Enabled = false;
            cardTypeComboBox.Enabled = false;
        }

        public void enableAllPlantCheckBox(Boolean flag)
        {
            allCheckBox.Enabled = plant1CheckBox.Enabled = plant2CheckBox.Enabled = plant3CheckBox.Enabled = plant4CheckBox.Enabled = flag;
        }


        public void initializeControl(string _mode)
        {
            if (_mode.Equals("VIEW"))
            {
                cardTab.BackColor = rightTab.BackColor = fpTab.BackColor = viewColor;

                curMode = "VIEW";
                modeLabel.Text = "조회모드입니다";
                modeLabel.ForeColor = Color.FromArgb(0x99, 0x00, 0xff);   // Color.Blue;
                // this.BackColor = Color.FromArgb(0xCC, 0xD1, 0xFF);


                // 등록 ACS만 Indeterminate
                // allCheckBox.Enabled = plant1CheckBox.Enabled = plant2CheckBox.Enabled = plant3CheckBox.Enabled = plant4CheckBox.Enabled = false;
                // allCheckBox.CheckState = CheckState.Unchecked;

                commonGroupBox.Enabled = true;

                cardGroupBox.Enabled = true;
                commonCardNum.Enabled = false;
                issueNum.Enabled = false;
                pinNum.Enabled = false;
                cardType.Enabled = true;
                cardStatus.Enabled = true;
                cardFormat.Enabled = true;
                issueType.Enabled = true;
                issueReason.Enabled = true;
                startDate.Enabled = true;
                endDate.Enabled = true;

                personGroupBox.Enabled = true;
                name.Enabled = true;
                ssn.Enabled = true;
                department.Enabled = true;
                division.Enabled = true;
                title.Enabled = true;
                tel.Enabled = true;
                address.Enabled = true;
                sazin.Enabled = true;
                sazinButton.Enabled = true;

                commonGroupBox2.Enabled = false;

                // choicePlantGridView();

                commonGroupBox3.Enabled = false;
                scanButton.Enabled = true;
                alwaysCheckBox.Enabled = true;
                finger1.Enabled = true;
                finger2.Enabled = true;
                fpIP.Enabled = false;

                save.Enabled = true;
                regStartButton.Enabled = true;
                addStartButton.Enabled = true;
                massChangeStart.Enabled = true;

                setColor(cardType, Color.White);
                setColor(cardStatus, Color.White);
                setColor(cardFormat, Color.White);
                setColor(issueType, Color.White);
                setColor(issueReason, Color.White);
                setColor(department, Color.White);
                setColor(division, Color.White);
                setColor(title, Color.White);


                /////////////////////////////////// 초기 데이터 /////////////////////////////////////

                /*
                cardType.Text = "일시출입증";
                cardStatus.Text = "ACTIVE";
                cardFormat.Text = "12 DIGIT";
                issueType.Text = "발급";
                issueReason.Text = "신규";
                startDate.Value = DateTime.Now;
                endDate.Value = DateTime.Now.AddYears(30);
                name.Text = ssn.Text = tel.Text = address.Text = "";
                department.Text = "고리1발";
                division.Text = "정보시스템팀";
                title.Text = "차장";
                Bitmap img = new Bitmap(".\\img\\nobody3.png");
                sazin.Image = util.ResizeBitmap(img, sazin.Width, sazin.Height);
                alwaysCheckBox.CheckState = CheckState.Unchecked;
                */

                cardNum.Text = commonCardNum2.Text = commonCardNum3.Text = commonCardNum.Text;


            }
            else if (_mode.Equals("REG"))
            {

                cardTab.BackColor = rightTab.BackColor = fpTab.BackColor = regColor;


                curMode = "REG";
                modeLabel.Text = "신규등록모드입니다";
                modeLabel.ForeColor = Color.FromArgb (0x009999);   // Color.Red;
                // this.BackColor = Color.FromArgb(0xFF, 0xA9, 0xB0);


                allCheckBox.Enabled = plant1CheckBox.Enabled = plant2CheckBox.Enabled = plant3CheckBox.Enabled = plant4CheckBox.Enabled = true;
                allCheckBox.CheckState = plant1CheckBox.CheckState = plant2CheckBox.CheckState = plant3CheckBox.CheckState = plant4CheckBox.CheckState = CheckState.Unchecked;
                allCheckBox.ForeColor = plant1CheckBox.ForeColor = plant2CheckBox.ForeColor = plant3CheckBox.ForeColor = plant4CheckBox.ForeColor = Color.Black;



                commonGroupBox.Enabled = true;

                cardGroupBox.Enabled = true;
                cardNum.Enabled = true;
                issueNum.Enabled = false;
                pinNum.Enabled = true;
                cardType.Enabled = true;
                cardStatus.Enabled = true;
                cardFormat.Enabled = true;
                issueType.Enabled = true;
                issueReason.Enabled = true;
                startDate.Enabled = true;
                endDate.Enabled = true;

                personGroupBox.Enabled = true;
                name.Enabled = true;
                ssn.Enabled = true;
                department.Enabled = true;
                division.Enabled = true;
                title.Enabled = true;
                tel.Enabled = true;
                address.Enabled = true;
                sazin.Enabled = true;
                sazinButton.Enabled = true;

                commonGroupBox2.Enabled = false;

                // choicePlantGridView();

                commonGroupBox3.Enabled = false;
                scanButton.Enabled = true;
                alwaysCheckBox.Enabled = true;
                finger1.Enabled = true;
                finger2.Enabled = true;
                fpIP.Enabled = false;

                save.Enabled = true;
                regStartButton.Enabled = false;
                addStartButton.Enabled = false;
                massChangeStart.Enabled = false;



                /////////////////////////////////////////////// 초기 데이터 /////////////////////////////////////////
                name.Text = commonCardNum.Text = engName.Text = birthday.Text = "";
                gender.Text = "선택";
                cardNum.Text = issueNum.Text = pinNum.Text = "";
                cardType.Text = cardStatus.Text = cardFormat.Text = "선택";
                issueType.Text = issueReason.Text = "선택";
                startDate.Value = DateTime.Now;
                endDate.Value = DateTime.Now.AddYears(30);
                name.Text = ssn.Text = tel.Text = address.Text = "";
                department.Text = division.Text = title.Text = "선택";


                Bitmap img = new Bitmap(".\\img\\nobody3.png");
                sazin.Image = util.ResizeBitmap(img, sazin.Width, sazin.Height);
                alwaysCheckBox.CheckState = CheckState.Unchecked;

            }
            else if (_mode.Equals("MASS_CHANGE"))
            {

                cardTab.BackColor = rightTab.BackColor = fpTab.BackColor = massColor;

                curMode = "MASS_CHANGE";
                modeLabel.Text = "일괄변경모드입니다";
                modeLabel.ForeColor = Color.FromArgb(0x009900);   // Color.Purple;


                allCheckBox.Enabled = plant1CheckBox.Enabled = plant2CheckBox.Enabled = plant3CheckBox.Enabled = plant4CheckBox.Enabled = true;
                allCheckBox.CheckState = plant1CheckBox.CheckState = plant2CheckBox.CheckState = plant3CheckBox.CheckState = plant4CheckBox.CheckState = CheckState.Unchecked;

                commonGroupBox.Enabled = false;

                cardGroupBox.Enabled = true;
                commonCardNum.Enabled = false;
                issueNum.Enabled = false;
                pinNum.Enabled = false;
                cardType.Enabled = true;
                cardStatus.Enabled = true;
                cardFormat.Enabled = true;
                issueType.Enabled = true;
                issueReason.Enabled = true;
                startDate.Enabled = true;
                endDate.Enabled = true;

                personGroupBox.Enabled = true;
                name.Enabled = false;
                ssn.Enabled = false;
                department.Enabled = true;
                division.Enabled = true;
                title.Enabled = true;
                tel.Enabled = false;
                address.Enabled = false;
                sazin.Enabled = false;
                sazinButton.Enabled = false;

                commonGroupBox2.Enabled = false;

                // choicePlantGridView();

                commonGroupBox3.Enabled = false;
                scanButton.Enabled = false;
                alwaysCheckBox.Enabled = true;
                finger1.Enabled = false;
                finger2.Enabled = false;
                fpIP.Enabled = false;

                save.Enabled = true;
                regStartButton.Enabled = false;
                addStartButton.Enabled = false;
                massChangeStart.Enabled = false;



                /////////////////////////////////////////////// 초기 데이터 /////////////////////////////////////////
                name.Text = commonCardNum.Text = engName.Text = birthday.Text = "";
                gender.Text = "선택";
                cardNum.Text = issueNum.Text = pinNum.Text = "";
                cardType.Text = cardStatus.Text = cardFormat.Text = "현재데이터유지";
                issueType.Text = issueReason.Text = "현재데이터유지";
                startDate.Value = DateTime.Now;   // new DateTime(1970, 1, 1);  // DateTime.Now;
                endDate.Value = DateTime.Now;     // new DateTime(1970, 1, 1);  // DateTime.Now.AddYears(30);
                name.Text = ssn.Text = tel.Text = address.Text = "";
                department.Text = division.Text = title.Text = "현재데이터유지";

                Bitmap img = new Bitmap(".\\img\\nobody3.png");
                sazin.Image = util.ResizeBitmap(img, sazin.Width, sazin.Height);
                alwaysCheckBox.CheckState = CheckState.Unchecked;
            }
            
        }


        public void choicePlantGridView()
        {
            Log.WriteLog("\n");
            Log.WriteLog("[BM_Reg_001.cs] choicePlantGridView () plant1 : " + plant1CheckBox.CheckState);
            Log.WriteLog("[BM_Reg_001.cs] choicePlantGridView () plant2 : " + plant2CheckBox.CheckState);
            Log.WriteLog("[BM_Reg_001.cs] choicePlantGridView () plant3 : " + plant3CheckBox.CheckState);
            Log.WriteLog("[BM_Reg_001.cs] choicePlantGridView () plant4 : " + plant4CheckBox.CheckState);



            /*
            if (plant1CheckBox.CheckState == CheckState.Checked || plant1CheckBox.CheckState == CheckState.Indeterminate)
            {
                if (curMode.Equals("ADD"))
                {
                    plant1Grid.Enabled = false;
                    util.changeFont(plantTitle1, 0);
                }
                else
                {
                    plant1Grid.Enabled = true;
                    util.changeFont(plantTitle1, 1);
                }
            }
            else
            {
                plant1Grid.Enabled = false;
                util.changeFont(plantTitle1, 0);
            }

            if (plant2CheckBox.CheckState == CheckState.Checked || plant2CheckBox.CheckState == CheckState.Indeterminate)
            {
                if (curMode.Equals("ADD"))
                {
                    plant2Grid.Enabled = false;
                    util.changeFont(plantTitle2, 0);
                }
                else
                {
                    plant2Grid.Enabled = true;
                    util.changeFont(plantTitle2, 1);
                }
            }
            else
            {
                plant2Grid.Enabled = false;
                util.changeFont(plantTitle2, 0);
            }

            if (plant3CheckBox.CheckState == CheckState.Checked || plant3CheckBox.CheckState == CheckState.Indeterminate)
            {
                if (curMode.Equals("ADD"))
                {
                    plant3Grid.Enabled = false;
                    util.changeFont(plantTitle3, 0);
                }
                else
                {
                    plant3Grid.Enabled = true;
                    util.changeFont(plantTitle3, 1);
                }
            }
            else
            {
                plant3Grid.Enabled = false;
                util.changeFont(plantTitle3, 0);
            }

            if (plant4CheckBox.CheckState == CheckState.Checked || plant4CheckBox.CheckState == CheckState.Indeterminate)
            {
                if (curMode.Equals("ADD"))
                {
                    plant4Grid.Enabled = false;
                    util.changeFont(plantTitle4, 0);
                }
                else
                {
                    plant4Grid.Enabled = true;
                    util.changeFont(plantTitle4, 1);
                }
            }
            else
            {
                plant4Grid.Enabled = false;
                util.changeFont(plantTitle4, 0);
            }
            */


        }

        private void cardAllCheckChange(object sender, EventArgs e)
        {
            if (!curMode.Equals("MASS_CHANGE")) return;

            if (cardAllCheck.Text.Equals(""))
            {
                cardAllCheck.Text = "V";
                cardAllCheck.BackColor = Color.Yellow;

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.Yellow;
                    dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.Yellow;
                    dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Yellow;
                    dataGridView1.Rows[i].Cells[2].Value = "V";
                }
            }
            else if (cardAllCheck.Text.Equals("V"))
            {
                cardAllCheck.Text = "";
                cardAllCheck.BackColor = Color.FromArgb(221, 221, 255);

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(221, 221, 255);
                    dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(221, 221, 255);
                    dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(221, 221, 255);
                    dataGridView1.Rows[i].Cells[2].Value = "";
                }

            }
        }

        public void clearAllCardCheck()
        {

            cardAllCheck.Text = "";
            cardAllCheck.BackColor = Color.FromArgb(221, 221, 255);
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(221, 221, 255);
                dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(221, 221, 255);
                dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(221, 221, 255);
                dataGridView1.Rows[i].Cells[2].Value = "";

            }
        }

        public Boolean validData()
        {

            Log.WriteLog("validData () cardName: " + name);
            Log.WriteLog("validData () cardNum : " + cardNum);

            if (!curMode.Equals("MASS_CHANGE"))
            {
                if (name.Text.Equals("") || name.Text == null || cardNum.Text.Equals("") || cardNum.Text == null)
                {
                    MessageBox.Show("카드명, 카드번호 확인要");

                    TAB.SelectedTab = cardTab;
                    return false;
                }
            }


            return true;

        }

        private void clickCardType(object sender, EventArgs e) { if (curMode.Equals("MASS_CHANGE") && !cardType.Text.Equals ("현재데이터유지")) cardType.BackColor = Color.Yellow; }
        private void clickCardStatus(object sender, EventArgs e) { if (curMode.Equals("MASS_CHANGE") && !cardStatus.Text.Equals("현재데이터유지")) cardStatus.BackColor = Color.Yellow; }
        private void clickCardFormat(object sender, EventArgs e) { if (curMode.Equals("MASS_CHANGE") && !cardFormat.Text.Equals("현재데이터유지")) cardFormat.BackColor = Color.Yellow; }
        private void clickIssueType(object sender, EventArgs e) { if (curMode.Equals("MASS_CHANGE") && !issueType.Text.Equals("현재데이터유지")) issueType.BackColor = Color.Yellow; }
        private void clickIssueReason(object sender, EventArgs e) { if (curMode.Equals("MASS_CHANGE") && !issueReason.Text.Equals("현재데이터유지")) issueReason.BackColor = Color.Yellow; }
        private void clickDepartment(object sender, EventArgs e) { if (curMode.Equals("MASS_CHANGE") && !department.Text.Equals("현재데이터유지")) department.BackColor = Color.Yellow; }
        private void clickDivision(object sender, EventArgs e) { if (curMode.Equals("MASS_CHANGE") && !division.Text.Equals("현재데이터유지")) division.BackColor = Color.Yellow; }
        private void clickTitle(object sender, EventArgs e) { if (curMode.Equals("MASS_CHANGE") && !title.Text.Equals("현재데이터유지")) title.BackColor = Color.Yellow; }
        
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }       
        
}
