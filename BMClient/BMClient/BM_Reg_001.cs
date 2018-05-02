using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;

using System.Windows.Forms;



using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading;

using BS_SDK;

namespace BMClient
{
    public partial class BM_Reg_001 : Form
    {

        int maxChangeNum = 50;

        string[] orgPlantRight = { "", "", "", "" };
        string[] newPlantRight = { "", "", "", "" };

        string changeRightString = "";


        Boolean sazinChange = false;

        Boolean firstView = true;

        string whereClause = "";

        ArrayList listArr = null;
        ArrayList detailArr = null;

        ArrayList rightArr1 = null;
        ArrayList rightArr2 = null;
        ArrayList rightArr3 = null;
        ArrayList rightArr4 = null;

        int curDetailNum = 0;

        DetailUnit du = null;
        RightUnit ru = null;

        int rowNum = 1;
        int cNum = 2;

        int rowNum2 = 5;
        int cNum2 = 1;
        string msg = "";

        string curMode = "VIEW";
        string curTab = "CARD";

        Color viewColor = Color.FromArgb(0xA9, 0xE2, 0xC5);     // Color.FromArgb(0xA9, 0xE2, 0xC5);  
        Color regColor = Color.FromArgb(0xe3, 0xd2, 0xff);  // Color.FromArgb(0xfd, 0xf0, 0xff);   // Color.FromArgb(0xF8, 0xE5, 0xD0);
        Color addColor = Color.FromArgb(227, 255, 193);     // Color.FromArgb(0xA9, 0xE2, 0xC5);
        Color massColor = Color.FromArgb(227, 255, 193);

        Color viewLabelColor = Color.FromArgb(0x009999);
        Color regLabelColor = Color.FromArgb(0x99, 0x00, 0xff);
        Color massLabelColor = Color.FromArgb(0x009900);

        Util util = null;

        Request req = null;
        QueryMaker qm = new QueryMaker();

        int totalListNum = 0;

        ArrayList pageListNum = null;


        int listPerPage = 1000;
        int curPage = 1;
        int firstIndex = 0;
        int preEA = 0;
        int lastNum = 0;
        Boolean useFlag = false;
        LoadingForm loading = null;

        System.Threading.Timer timer = null;

        Boolean scanSuccess = false;

        int fpServerNum = 0;

        ArrayList[] nUserIdnArr = { new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList() };

        DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        DateTime startTime = new DateTime();
        DateTime endTime = new DateTime();

        public BM_Reg_001()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;

            // System.Threading.TimerCallback tmp = new System.Threading.TimerCallback(doSomething);
            // timer = new System.Threading.Timer(tmp, null, 0, 0);

            req = new Request();

            util = new Util();

            dataGridView1.CellClick += new DataGridViewCellEventHandler(clickGridView1);
            plant1Grid.CellClick += new DataGridViewCellEventHandler(clickPlant1Grid);
            plant2Grid.CellClick += new DataGridViewCellEventHandler(clickPlant2Grid);
            plant3Grid.CellClick += new DataGridViewCellEventHandler(clickPlant3Grid);
            plant4Grid.CellClick += new DataGridViewCellEventHandler(clickPlant4Grid);

            cardAllCheck.Click += new EventHandler(cardAllCheckChange);

            rightSave.Click += new EventHandler(clickSave);


            TAB.Selected += new TabControlEventHandler(selectedTab);




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
            initButton.Click += new EventHandler(clear);
            initRight.Click += new EventHandler(clear);

            save.Enabled = false;
            startDate.Value = DateTime.Now;
            endDate.Value = DateTime.Now.AddYears(30);



            name.KeyDown += new KeyEventHandler(name_KeyDown);
            birthday.KeyDown += new KeyEventHandler(birthday_KeyDown);
            gender.KeyDown += new KeyEventHandler(gender_KeyDown);
            ssno.KeyDown += new KeyEventHandler(ssno_KeyDown);
            department.KeyDown += new KeyEventHandler(department_KeyDown);
            division.KeyDown += new KeyEventHandler(division_KeyDown);
            title.KeyDown += new KeyEventHandler(title_KeyDown);
            email.KeyDown += new KeyEventHandler(email_KeyDown);
            tel.KeyDown += new KeyEventHandler(tel_KeyDown);
            address.KeyDown += new KeyEventHandler(address_KeyDown);
            cardNum.KeyDown += new KeyEventHandler(cardNum_KeyDown);
            cardType.KeyDown += new KeyEventHandler(cardType_KeyDown);
            cardStatus.KeyDown += new KeyEventHandler(cardStatus_KeyDown);
            startDate.KeyDown += new KeyEventHandler(startDate_KeyDown);
            endDate.KeyDown += new KeyEventHandler(endDate_KeyDown);


            nameRadio.CheckedChanged += new EventHandler(clickNameRadio);
            cardNumRadio.CheckedChanged += new EventHandler(clickCardNumRadio);
            departRadio.CheckedChanged += new EventHandler(clickDepartRadio);
            divisionRadio.CheckedChanged += new EventHandler(clickDivisionRadio);
            typeRadio.CheckedChanged += new EventHandler(clickTypeRadio);
            rightSave.Enabled = false;

            cardNum.KeyUp += new KeyEventHandler(keyUpCardNum); // += new KeyPressEventHandler(keyDownCardNum); // += new KeyEventHandler(keyDownCardNum);
            cardType.SelectedValueChanged += new EventHandler(clickCardType);


            // cardType.Click += new EventHandler(clickCardType2);

            // cardType.MouseClick += new MouseEventHandler(clickCardType2);


            cardNum.TextChanged += new EventHandler(cardNum_TextChanged);
            cardStatus.SelectedValueChanged += new EventHandler(clickCardStatus);
            cardFormat.SelectedValueChanged += new EventHandler(clickCardFormat);
            issueType.SelectedValueChanged += new EventHandler(clickIssueType);
            issueReason.SelectedValueChanged += new EventHandler(clickIssueReason);
            department.SelectedValueChanged += new EventHandler(clickDepartment);
            division.SelectedValueChanged += new EventHandler(clickDivision);
            title.SelectedValueChanged += new EventHandler(clickTitle);

            name.TextChanged += new EventHandler(name_TextChanged);

            searchEndDate.KeyDown += new KeyEventHandler(searchEndDate_KeyDown);


            dataGridView1.SelectionChanged += new EventHandler(selectionChanged);




            post.Click += new EventHandler(clickPostPage);







            showCardList(50);
            // getDataRightFromDB();


            testButton.Click += new EventHandler(clickTest);

            searchButton.Click += new EventHandler(clickSearch);

            nameRadio.Checked = true;

            badge_0.Click += new EventHandler(clickBadge0);
            badge_1.Click += new EventHandler(clickBadge1);
            badge_2.Click += new EventHandler(clickBadge2);
            badge_3.Click += new EventHandler(clickBadge3);
            badge_4.Click += new EventHandler(clickBadge4);

            CheckForIllegalCrossThreadCalls = false;

            this.Shown += new EventHandler(clickShown);

            sazinButton.Click += new EventHandler(clickSazin);


            nameTextBox.KeyDown += new KeyEventHandler(clickNameEnter);

            cardNumTextBox.KeyDown += new KeyEventHandler(clickCardNumEnter);


            openButton.Click += new EventHandler(clickOpenButton);


            // finger1.Click += new EventHandler(clickFinger1);
            // finger2.Click += new EventHandler(clickFinger2);

            addFP.Click += new EventHandler(clickAddFP);
            deleteFP.Click += new EventHandler(clickDeleteFP);


            scanFP.Click += new EventHandler(clickScan);

            fpCheck.CheckedChanged += new EventHandler(fpCheckChange);
            fpCardCheck.CheckedChanged += new EventHandler(fpCardCheckChange);

            startDate.ValueChanged += new EventHandler(changeStartDate);
            endDate.ValueChanged += new EventHandler(changeEndDate);

            getCode();




            alwaysCheckBox.CheckStateChanged += new EventHandler(clickAlwaysCheck);
            ptCheckBox.CheckStateChanged += new EventHandler(clickPtCheck);


            /*
            if (loading != null)
            {
                loading.Close();
                loading = null;
                timer.Dispose();
                timer = null;
            }
            */


            checkuseFlag(useFlag);



            string[] tempArray = null;
            try
            {
                if (string.IsNullOrEmpty(Bm_Login.login_Auth))
                {
                    MessageBox.Show("권한을 불러오지 못했습니다. 프로그램을 종료합니다");
                    Application.Exit();
                }
                else
                {
                    tempArray = Bm_Login.login_Auth.Split(',');

                    for (int i = 0; i < tempArray.Length - 1; i++)
                    {
                        PrgListYN(tempArray[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                Application.Exit();
                return;
            }

            // JSJ 0611
            fpIP.Text = getIni("FP_TERMINAL");
            fpIP.Items.Clear();
            fpIP.Items.Add(getIni("FP_TERMINAL"));
            fpIP.Items.Add(getIni("FP2_TERMINAL"));


            fpServerIP.Text = getIni("FP_SERVER_IP");
            fpServerIP.Enabled = false;

            // fpIP.SelectedIndexChanged += new EventHandler(changeFP);




        }



        void endDate_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Tab) name.Focus();
        }

        void startDate_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Tab) endDate.Focus();
        }

        void cardStatus_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Tab) startDate.Focus();
        }

        void cardType_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Tab) cardStatus.Focus();
        }

        void cardNum_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Tab) cardType.Focus();
        }

        void address_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Tab) cardNum.Focus();
        }

        void tel_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Tab) address.Focus();
        }

        void email_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Tab) tel.Focus();
        }

        void title_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Tab) email.Focus();
        }

        void division_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Tab) title.Focus();
        }

        void department_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Tab) division.Focus();
        }

        void ssno_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Tab) department.Focus();
        }

        void gender_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Tab) ssno.Focus();
        }

        void birthday_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Tab) gender.Focus();
        }

        void name_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Tab) birthday.Focus();
        }

        void searchEndDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                find();
            }
        }

        void cardNum_TextChanged(object sender, EventArgs e)
        {
            if (curMode.Equals("VIEW"))
            {
                reRegButton.Enabled = true;
            }
        }

        // JSJ GR 0804
        private void changeFP(object sender, EventArgs e)
        {

            fpServerIP.Text = getIni("FP_SERVER_IP");
            Gloval.fpServerNum = 1;
            fpServerLabel.Text = "지문서버 (1) :";
            // MessageBox.Show("FP1 지문등록여부 : " + du.getFpNum());
            tmpNum.Text = du.getFpNum();
            showFPTab(Convert.ToInt32(du.getFpNum()));

            /*
            if (fpIP.SelectedIndex == 0)
            {
                fpServerIP.Text = getIni("FP_SERVER_IP");
                Gloval.fpServerNum = 1;
                fpServerLabel.Text = "지문서버 (1) :";
                // MessageBox.Show("FP1 지문등록여부 : " + du.getFpNum());
                tmpNum.Text = du.getFpNum();
                showFPTab(Convert.ToInt32(du.getFpNum()));
            }
            else if (fpIP.SelectedIndex == 1)
            {
                fpServerIP.Text = getIni("FP_SERVER_IP");
                Gloval.fpServerNum = 1;
                fpServerLabel.Text = "지문서버 (1) : ";
                // MessageBox.Show("FP1 지문등록여부 : " + du.getFpNum());
                tmpNum.Text = du.getFpNum();
                showFPTab(Convert.ToInt32(du.getFpNum()));
            }
            else if (fpIP.SelectedIndex == 2)
            {
                fpServerIP.Text = getIni("FP2_SERVER_IP");
                Gloval.fpServerNum = 2;
                fpServerLabel.Text = "지문서버 (2) : ";
                // MessageBox.Show("FP2 지문등록여부 : " + du.getFpNum2());
                tmpNum.Text = du.getFpNum2();
                showFPTab(Convert.ToInt32(du.getFpNum2()));
            }
            */


        }

        // JSJ 0611
        public void showFPTab(int _curFPNum)
        {

            if (_curFPNum < 0)
            {
                name3.Text = commonCardNum3.Text = birthday3.Text = gender3.Text = "";
                alwaysCheckBox.Checked = false;
                finger1.Visible = finger2.Visible = false;
                // fpImg.Image = null;

            }
            else
            {
                // if (_curFPNum == 0) fpImg.Image = null;

                name3.Text = name.Text;
                commonCardNum3.Text = commonCardNum.Text;
                birthday3.Text = birthday.Value.Year + "년" + birthday.Value.Month + "월" + birthday.Value.Day + "일";
                gender3.Text = gender.Text;

                finger1.Visible = finger2.Visible = false;
                if (_curFPNum > 0) finger1.Visible = true;
                if (_curFPNum > 1) finger2.Visible = true;
            }
        }


        private void PrgListYN(string Listid)
        {
            switch (Listid.Trim())
            {
                case "BMS011":

                    useFlag = true;

                    break;
                default:
                    break;
            }
        }

        void checkuseFlag(Boolean flag)
        {
            if (flag)
            {
                excel.Enabled = true;
                regStartButton.Enabled = true;
                save.Enabled = true;
                ohButton.Enabled = true;
                initButton.Enabled = true;
                rightSave.Enabled = true;
                initRight.Enabled = true;
                pinButton.Enabled = true;
                massChangeStart.Enabled = true;
              
                plantRegButton.Enabled = true;
            }
            else
            {
                excel.Enabled = false;
                regStartButton.Enabled = false;
                save.Enabled = false;
                ohButton.Enabled = false;
                initButton.Enabled = false;
                rightSave.Enabled = false;
                initRight.Enabled = false;
                pinButton.Enabled = false;
                massChangeStart.Enabled = false;
                plantRegButton.Enabled = false;
              

            }
        }


        void name_TextChanged(object sender, EventArgs e)
        {
            cardName.Text = name.Text;
        }


        private void clickAlwaysCheck(object sender, EventArgs e)
        {

            if (curMode.Equals("MASS_CHANGE"))
            {
                alwaysCheckBox.ForeColor = Color.Red;

            }
        }

        private void clickPtCheck(object sender, EventArgs e)
        {

            if (curMode.Equals("MASS_CHANGE"))
            {
                ptCheckBox.ForeColor = Color.Red;

            }
        }

        public void getCode()
        {
            // CodeTable ct = new CodeTable();

            int cnt = -1;

            cnt = Bm_Main.department.Rows.Count;
            Log.WriteLogTmp("\n\n................................................... [BM_Reg_001.cs] getCode () deparment code count : " + cnt);
            department.Items.Clear();
            for (int i = 0; i < cnt; i++)
            {
                department.Items.Add(Bm_Main.department.Rows[i][1].ToString());
                departCombo.Items.Add(Bm_Main.department.Rows[i][1].ToString());
            }

            // Log.WriteLogTmp(".................. : " + CodeTable.department.Rows[0][0] + ", " + CodeTable.department.Rows[0][1]);

            cnt = Bm_Main.division.Rows.Count;
            division.Items.Clear();
            for (int i = 0; i < cnt; i++)
            {
                division.Items.Add(Bm_Main.division.Rows[i][1].ToString());
                divisionCombo.Items.Add(Bm_Main.division.Rows[i][1].ToString());
            }

            cnt = Bm_Main.title.Rows.Count;
            title.Items.Clear();
            for (int i = 0; i < cnt; i++) title.Items.Add(Bm_Main.title.Rows[i][1].ToString());

            cnt = Bm_Main.cardStat.Rows.Count;
            cardStatus.Items.Clear();
            for (int i = 0; i < cnt; i++) cardStatus.Items.Add(Bm_Main.cardStat.Rows[i][1].ToString());

            cnt = Bm_Main.cardType.Rows.Count;
            cardType.Items.Clear();
            for (int i = 0; i < cnt; i++)
            {
                cardType.Items.Add(Bm_Main.cardType.Rows[i][1].ToString());
                typeCombo.Items.Add(Bm_Main.cardType.Rows[i][1].ToString());

            }

            cnt = Bm_Main.cardFormat.Rows.Count;
            cardFormat.Items.Clear();
            for (int i = 0; i < cnt; i++) cardFormat.Items.Add(Bm_Main.cardFormat.Rows[i][1].ToString());

            cnt = Bm_Main.issueType.Rows.Count;
            issueType.Items.Clear();
            for (int i = 0; i < cnt; i++) issueType.Items.Add(Bm_Main.issueType.Rows[i][1].ToString());

            cnt = Bm_Main.issueReason.Rows.Count;
            issueReason.Items.Clear();
            for (int i = 0; i < cnt; i++) issueReason.Items.Add(Bm_Main.issueReason.Rows[i][1].ToString());

        }


        private void fpCheckChange(object sender, EventArgs e)
        {
            if (fpCheck.CheckState == CheckState.Unchecked)
            {
                name3.Text = commonCardNum3.Text = birthday3.Text = gender3.Text = "";
                alwaysCheckBox.Checked = false;
            }
            else if (fpCheck.CheckState == CheckState.Checked)
            {
                name3.Text = name.Text;
                commonCardNum3.Text = commonCardNum.Text;
                birthday3.Text = birthday.Value.Year + "년" + birthday.Value.Month + "월" + birthday.Value.Day + "일";

                gender3.Text = gender.Text;
            }
        }

        private void fpCardCheckChange(object sender, EventArgs e)
        {
            if (fpCardCheck.CheckState == CheckState.Unchecked)
            {
                name3.Text = commonCardNum3.Text = birthday3.Text = gender3.Text = "";
            }
            else if (fpCardCheck.CheckState == CheckState.Checked)
            {
                name3.Text = name.Text;
                commonCardNum3.Text = commonCardNum.Text;
                birthday3.Text = birthday.Value.Year + "년" + birthday.Value.Month + "월" + birthday.Value.Day + "일";
                gender3.Text = gender.Text;
            }
        }

        private void clickScan(object sender, EventArgs e)
        {



            if (finger4.Visible)
            {
                MessageBox.Show("지문은 4개까지만 등록 가능합니다.");
                return;
            }

            if (fpIP.Text.Equals("") || fpIP.Text == null || commonCardNum3.Text.Equals("") || commonCardNum3.Text == null)
            {
                MessageBox.Show("지문단말 IP 혹은 카드번호를 확인하세요");
                return;
            }

            int fingerNum = 6;

            if (finger1.BackColor == Color.Blue) fingerNum = 6;
            else if (finger2.BackColor == Color.Blue) fingerNum = 7;

            string query = "select TOP 1 nUserIdn from TB_USER_CARD where sCardNo = '" + Convert.ToInt64(commonCardNum3.Text) + "'";


            string rNumString = "";



            if (!finger1.Visible && !finger2.Visible && !finger3.Visible && !finger4.Visible)
            {

                fingerNum = 6;

                // MessageBox.Show("첫번째 손가락 추가");

            }
            else if (finger1.Visible && !finger2.Visible && !finger3.Visible && !finger4.Visible)
            {
                fingerNum = 7;

                //  MessageBox.Show("두번째 손가락 추가");

            }
            else if (finger1.Visible && finger2.Visible && !finger3.Visible && !finger4.Visible)
            {

                fingerNum = 8;

                //   MessageBox.Show("세번째 손가락 추가");

            }
            else if (finger1.Visible && finger2.Visible && finger3.Visible && !finger4.Visible)
            {
                fingerNum = 9;

                //   MessageBox.Show("네번째 손가락 추가");

            }


            Gloval.scanSuccess = false;


            Form fpForm = new FPRegisterForm(fpIP.Text, commonCardNum3.Text, name3.Text, fingerNum);

            fpForm.ShowDialog();
            fpForm.TopMost = true;


            if (Gloval.scanSuccess)
            {

                int fpCnt = Convert.ToInt32(tmpNum.Text.Trim()) + 1;

                query = "update BADGE set FP_1 = " + fpCnt + " where bid ='" + commonCardNum3.Text.Trim() + "'";

                Log.WriteLog("[BM_Reg_001.cs] clickScan () fpCnt query : " + query);

                string rCheck = req.update("BMS011", query, "BMI");

                if (rCheck.Equals("0"))
                {
                    Log.WriteLog("[BM_Reg_001.cs] clickScan () " + commonCardNum3.Text.Trim() + "'s fpCnt : " + fpCnt);
                    tmpNum.Text = "" + fpCnt;
                }
                else
                {

                }


                if (!finger1.Visible && !finger2.Visible && !finger3.Visible && !finger4.Visible)
                {
                    finger1.Visible = true;
                    finger1.BackColor = Color.Blue;
                    finger1.ForeColor = Color.White;
                    drawFinger(1);
                }
                else if (finger1.Visible && !finger2.Visible && !finger3.Visible && !finger4.Visible)
                {
                    finger2.Visible = true;
                    finger2.BackColor = Color.Blue;
                    finger2.ForeColor = Color.White;
                    drawFinger(2);
                }
                else if (finger1.Visible && finger2.Visible && !finger3.Visible && !finger4.Visible)
                {
                    finger3.Visible = true;
                    finger3.BackColor = Color.Blue;
                    finger3.ForeColor = Color.White;
                    drawFinger(3);
                }
                else if (finger1.Visible && finger2.Visible && finger3.Visible && !finger4.Visible)
                {
                    finger4.Visible = true;
                    finger4.BackColor = Color.Blue;
                    finger4.ForeColor = Color.White;
                    drawFinger(4);
                }

                Gloval.scanSuccess = false;
            }


        }

        private void clickOpenButton(object sender, EventArgs e)
        {
            nameTextBox.Text = "";
            departCombo.Text = divisionCombo.Text = "전체";


            name3.Text = commonCardNum3.Text = birthday3.Text = "";
            fpImg.Image = null;
            fpImg.BackColor = Color.Silver;

            finger1.Visible = finger2.Visible = finger3.Visible = finger4.Visible = false;

            Form cardForm = new CardReadForm(this, fpIP.Text);

            cardForm.ShowDialog();
            cardForm.TopMost = true;
        }

        private void clickFinger1(object sender, EventArgs e)
        {
            setFingerImage(1);
        }

        private void clickFinger2(object sender, EventArgs e)
        {
            setFingerImage(2);
        }

        private void finger3_Click(object sender, EventArgs e)
        {

        }

        private void finger4_Click(object sender, EventArgs e)
        {

        }


        public void setFingerImage(int index)
        {

            if (index == 1)
            {
                fpImg.Image = util.ResizeBitmap(new Bitmap(".\\img\\fp.png"), fpImg.Width, fpImg.Height);
                finger1.BackColor = Color.Blue;
                finger1.ForeColor = Color.White;


                finger4.BackColor = Color.Silver;
                finger4.ForeColor = Color.Black;
                finger3.BackColor = Color.Silver;
                finger3.ForeColor = Color.Black;
                finger2.BackColor = Color.Silver;
                finger2.ForeColor = Color.Black;
            }
            if (index == 2)
            {
                fpImg.Image = util.ResizeBitmap(new Bitmap(".\\img\\f0.png"), fpImg.Width, fpImg.Height);
                finger2.BackColor = Color.Blue;
                finger2.ForeColor = Color.White;

                finger4.BackColor = Color.Silver;
                finger4.ForeColor = Color.Black;
                finger3.BackColor = Color.Silver;
                finger3.ForeColor = Color.Black;
                finger1.BackColor = Color.Silver;
                finger1.ForeColor = Color.Black;
            }
            if (index == 3)
            {
                fpImg.Image = util.ResizeBitmap(new Bitmap(".\\img\\f2.png"), fpImg.Width, fpImg.Height);
                finger3.BackColor = Color.Blue;
                finger3.ForeColor = Color.White;

                finger4.BackColor = Color.Silver;
                finger4.ForeColor = Color.Black;
                finger2.BackColor = Color.Silver;
                finger2.ForeColor = Color.Black;
                finger1.BackColor = Color.Silver;
                finger1.ForeColor = Color.Black;
            }

            if (index == 4)
            {
                fpImg.Image = util.ResizeBitmap(new Bitmap(".\\img\\f3.png"), fpImg.Width, fpImg.Height);
                finger4.BackColor = Color.Blue;
                finger4.ForeColor = Color.White;

                finger3.BackColor = Color.Silver;
                finger3.ForeColor = Color.Black;
                finger2.BackColor = Color.Silver;
                finger2.ForeColor = Color.Black;
                finger1.BackColor = Color.Silver;
                finger1.ForeColor = Color.Black;
            }
        }


        private void clickAddFP(object sender, EventArgs e)
        {

            if (name3.Text.Equals("") || name3.Text == null)
            {
                MessageBox.Show("지문서버(1) 에 카드가 등록되어있지 않습니다.");
                return;
            }

            if (finger4.Visible)
            {
                MessageBox.Show("지문은 4개까지만 등록 가능합니다.");
                return;
            }
            if (!finger1.Visible && !finger2.Visible && !finger3.Visible && !finger4.Visible)
            {

                // 지문1개추가
                finger1.Visible = true;
                finger1.BackColor = Color.Blue;
                finger1.ForeColor = Color.White;

            }
            else if (finger3.Visible && finger2.Visible && finger1.Visible && !finger4.Visible)
            {
                // 지문4개추가
                finger3.BackColor = Color.Silver;
                finger3.ForeColor = Color.Black;

                finger4.Visible = true;
                finger4.BackColor = Color.Blue;
                finger4.ForeColor = Color.White;

                scanSuccess = false;


            }
            else if (finger2.Visible && finger1.Visible && !finger3.Visible && !finger4.Visible)
            {


                // 지문3개추가
                finger2.BackColor = Color.Silver;
                finger2.ForeColor = Color.Black;

                finger3.Visible = true;
                finger3.BackColor = Color.Blue;
                finger3.ForeColor = Color.White;

                scanSuccess = false;

            }
            else if (finger1.Visible && !finger2.Visible && !finger3.Visible && !finger4.Visible)
            {


                // 지문2개추가
                finger1.BackColor = Color.Silver;
                finger1.ForeColor = Color.Black;

                finger2.Visible = true;
                finger2.BackColor = Color.Blue;
                finger2.ForeColor = Color.White;

                scanSuccess = false;
            }
        }

        private void clickDeleteFP(object sender, EventArgs e)
        {


            ///////////////////////////////////////// 1 차 //////////////////////////////////////////////////////
         

            if (commonCardNum3.Text.Equals("") || commonCardNum3.Text == null)
            {
                MessageBox.Show("카드번호를 확인하세요");
                return;
            }

            ///////////////////////////////////////// 2 차 //////////////////////////////////////////////////////
            long tmp = Convert.ToInt64(commonCardNum3.Text);
            string query = "select TOP 1 nUserIdn from TB_USER_CARD where sCardNo = '" + tmp + "'";



            string rNumString = "";            




            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            /*
            rNumString = req.select("BMS011", query, "FP2S");

            
            string nUserIdn2 = "-1";
            for (int i = 0; i < ReturnDT.dt.Rows.Count; i++)
            {
                nUserIdn2 = ReturnDT.dt.Rows[0]["nUserIdn"].ToString();
            }
            */
            Form form = new YesNoForm(commonCardNum3.Text);

            form.ShowDialog();
            form.TopMost = true;



            if (Gloval.deleteOK)
            {
                Gloval.deleteOK = false;

                long notZeroCardNum = Convert.ToInt64(commonCardNum3.Text.Trim());
                query = "delete from TB_USER_TEMPLATE where nUserIdn = (select TOP 1 nUserIdn from TB_USER_CARD where sCardNo = '" + notZeroCardNum + "')";
                // query = "delete from TB_USER_TEMPLATE where nUserIdn = " + nUserIdn1 + "";
                query = query + " ^ update TB_EXTNL_REQ set sValue = '1'";


                Log.WriteLog("[BM_Reg_001.cs] clickDeleteFP () query : " + query);

                // return;

                for (int i = 0; i < Convert.ToInt32(getIni("BS_NUM")); i++)
                {
                    rNumString = req.update("BMS011", query, "FP"+i+"I");
                }


                // MessageBox.Show("rNumString : " + rNumString);


                if (!rNumString.Equals("0"))
                {
                    MessageBox.Show("[Reg.cs] clickScan ()  지문 삭제가 실패되었습니다");
                    return;
                }


                ///////////////////////////////////////////////////////////////////////////////////////////////////
                /*
               query = "delete from TB_USER_TEMPLATE where nUserIdn = " + nUserIdn2 + "";
               query = query + " ^ update TB_EXTNL_REQ set sValue = '1'";

               // query = "delete from TB_EXTNL_REQ";

               rNumString = req.update("BMS011", query, "FP2I");
               */
                ////////////////////////////////////////////////////////////////////////////////////////////////////


                // MessageBox.Show("[Reg.cs] clickScan ()  " + tmp + " 카드의 지문이 삭제되었습니다");
                MessageBox.Show("" + tmp + " 카드의 지문이 삭제되었습니다");
                finger1.Visible = finger2.Visible = finger3.Visible = finger4.Visible = false;
                // fpImg.Image = null;
                // fpImg.BackColor = Color.Silver;
                drawFinger(0);


                ////////////////////////// ADD FP CNT ///////////////////////////////////////////////
                query = "update BADGE set FP_1 = 0 where bid ='" + commonCardNum3.Text.Trim() + "'";

                Log.WriteLog("[BM_Reg_001.cs] clickScan () fpCnt query : " + query);

                string rCheck = req.update("BMS011", query, "BMI");

                if (rCheck.Equals("0"))
                {
                    Log.WriteLog("[BM_Reg_001.cs] clickScan () " + commonCardNum3.Text.Trim() + "'s fpCnt : 0");
                    tmpNum.Text = "0";
                }
                else
                {

                }
                ////////////////////////////////////////////////////////////////////////////////////////



            }

        }

        private void clickNameEnter(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.Enter)
            {
                cardNumTextBox.Text = departCombo.Text = divisionCombo.Text = "전체";
                find();
            }
        }

        private void clickCardNumEnter(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.Enter)
            {
                nameTextBox.Text = departCombo.Text = divisionCombo.Text = "전체";
                find();
            }
        }

        public void find()
        {
            curPage = 1;
            firstIndex = 0;
            preEA = 0;
            lastNum = 0;
            listPerPage = 1000;
            curPageLabel.Text = "1";
            initializeControl("VIEW");
            clearAllCardDetail();
            showDefaultImage();
            showCardList(0);

            // MessageBox.Show("" + totalListNum + " 건이 검색되었습니다");

            nameTextBox.Text = cardNumTextBox.Text = "";

        }

        public void getDataAfterSave()
        {
            curPage = 1;
            firstIndex = 0;
            preEA = 0;
            lastNum = 0;
            listPerPage = 3000;
            curPageLabel.Text = "1";

            clearAllCardDetail();
            showDefaultImage();
            showListAfterSave();

            //  MessageBox.Show("" + totalListNum + " 건이 검색되었습니다");
        }

        private void clickSazin(object sender, EventArgs e)
        {

            byte[] array = null; ;
            string filename = "";

            try
            {

                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

                dlg.InitialDirectory = ".\\";
                dlg.Title = "사진 등록";

                if (dlg.ShowDialog() != DialogResult.OK) return;

                filename = dlg.FileName;
                array = File.ReadAllBytes(filename);

                sazin.Image = util.ResizeBitmap(ByteToImage(array), sazin.Width, sazin.Height);



                sazinChange = true;

                Log.WriteLogTmp("사진 : " + sazinChange);


            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.ToString());
            }


        }

        private void clickShown(object sender, EventArgs e)
        {
            Gloval.loading.Close();
            Gloval.loading = null;
            Gloval.timer.Dispose();
            Gloval.timer = null;
        }


        private void doSomething(object sender)
        {
            Log.WriteLog("doSomething ().........................................................");
            loading = new LoadingForm();
            loading.ShowDialog();
        }

        private void keyUpCardNum(object sener, EventArgs e)
        {
            Log.WriteLog("[Reg.cs] keyUpCardNum () ................................................");
            commonCardNum.Text = commonCardNum2.Text = commonCardNum3.Text = cardNum.Text;


        }

        private void clickGridView1(object sener, EventArgs e)
        {
            if (!curMode.Equals("MASS_CHANGE")) return;

            int rNum = dataGridView1.CurrentCell.RowIndex;
            int cNum = dataGridView1.CurrentCell.ColumnIndex;

            if (cNum != 2) return;
            //if (rNum == dataGridView1.RowCount - 1) return;

            if (dataGridView1.Rows[rNum].Cells[2].Value == null) return;

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

        private void clickPlant1Grid(object sener, EventArgs e)
        {
            util.checkRight(plant1Grid);
            Boolean flag = false;


            flag = allClearOrNot(plant1Grid);
            if (flag)
            {
                doPlantCheck(plant1Grid, false, plantTitle1);
                plant1CheckBox.ForeColor = Color.Black;
                plant1CheckBox.Checked = false;
            }

            changeNewRight();


        }
        private void clickPlant2Grid(object sener, EventArgs e)
        {
            util.checkRight(plant2Grid);
            Boolean flag = false;
            flag = allClearOrNot(plant2Grid);
            if (flag)
            {
                doPlantCheck(plant2Grid, false, plantTitle2);
                plant2CheckBox.ForeColor = Color.Black;
                plant2CheckBox.Checked = false;
            }
            changeNewRight();
        }
        private void clickPlant3Grid(object sener, EventArgs e)
        {
            util.checkRight(plant3Grid);
            Boolean flag = false;
            flag = allClearOrNot(plant3Grid);
            if (flag)
            {
                doPlantCheck(plant3Grid, false, plantTitle3);
                plant3CheckBox.ForeColor = Color.Black;
                plant3CheckBox.Checked = false;
            }



            changeNewRight();


        }
        private void clickPlant4Grid(object sener, EventArgs e)
        {
            util.checkRight(plant4Grid);
            Boolean flag = false;
            flag = allClearOrNot(plant4Grid);
            if (flag)
            {
                doPlantCheck(plant4Grid, false, plantTitle1);
                plant4CheckBox.ForeColor = Color.Black;
                plant4CheckBox.Checked = false;
            }
            changeNewRight();
        }


        public void changeNewRight()
        {
            DataGridView[] dgv = { plant1Grid, plant2Grid, plant3Grid, plant4Grid };




            for (int j = 0; j < dgv.Length; j++)
            {

                newPlantRight[j] = "";


                for (int i = 0; i < dgv[j].RowCount; i++)
                {
                    if (dgv[j].Rows[i].Cells[0].Style.BackColor == Color.Yellow)
                    {
                        // if (!newPlantRight[j].Equals("")) newPlantRight[j] = newPlantRight[j] + "," + (i + 1);

                        int accID = -100;
                        if (j == 0) accID = getAccessID(rightArr1, i);
                        else if (j == 1) accID = getAccessID(rightArr2, i);
                        else if (j == 2) accID = getAccessID(rightArr3, i);
                        else if (j == 3) accID = getAccessID(rightArr4, i);

                        //Log.WriteLogTmp("============== accID => " + accID);

                        if (!newPlantRight[j].Equals(""))
                        {
                            newPlantRight[j] = newPlantRight[j] + "," + accID;
                        }
                        else
                        {
                            newPlantRight[j] = "" + accID;
                        }
                    }
                }
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
            if (curTab.Equals("RIGHT") || curTab.Equals("FP"))
            {
                name.Text = name2.Text = name3.Text = cardName.Text = "";
                commonCardNum.Text = commonCardNum2.Text = commonCardNum3.Text = "";
                birthday.Value = new DateTime(1980, 1, 1);
                birthday2.Text = birthday3.Text = "1980년1월1일";
                gender.Text = gender2.Text = gender3.Text = "남자";

            }




            initializeControl("REG");

            department.SelectedIndex = 0;
            division.SelectedIndex = 0;
            title.SelectedIndex = 0;
            cardStatus.SelectedIndex = 0;

            cardFormat.SelectedIndex = 1;
            issueType.SelectedIndex = 0;
            issueReason.SelectedIndex = 0;
            fpCheck.Checked = true;


        }


        private void clickMassChangeStart(object sender, EventArgs e)
        {
            initializeControl("MASS_CHANGE");
            clearAllPlantRight();
        }


        private void clickSave(object sender, EventArgs e)
        {
            Log.WriteLog("clickSave () ............ curMode : " + curMode);

            msg = "";

            Boolean flag = false;
            cardNum.Text = addZero(cardNum.Text);

            if (curMode.Equals("REG") || curMode.Equals("MASS_CHANGE"))
            {
                // if (curMode.Equals("MASS_CHANGE")) flag = validatePlantGrid ();
                // else flag = validatePlantCheckBox();

                flag = validatePlantCheckBox();

                if (!flag)
                {
                    //  MessageBox.Show(msg);
                    return;
                }
            }

            if (curMode.Equals("VIEW") || curMode.Equals("REG"))
            {
                msg = "";

                if (!checkVaild())
                {
                    save.Enabled = true;

                    return;
                }

            }

            string tmp = "";
            if (curMode.Equals("REG")) tmp = "신규등록";
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

            if (curMode.Equals("MASS_CHANGE"))
            {
                //  if (TAB.SelectedTab == rightTab) MessageBox.Show("권한 일괄변경입니다. 주의하십시요");
                //  else if (TAB.SelectedTab == cardTab) MessageBox.Show(msg + "  " + tmp + " 하시겠습니까?");
            }
            else if (curMode.Equals("REG"))
            {






            }


            //////////////////////////////// 데이터 유효성 검사 /////////////////////////////////////

            if (!curMode.Equals("MASS_CHANGE"))
            {
                if (!checkName())
                {
                    save.Enabled = true;
                    return;
                }

                if (!checkCardNum())
                {
                    save.Enabled = true;
                    return;
                }

            }

            if (!checkStartEndDate())
            {
                save.Enabled = true;
                return;
            }




            Log.WriteLog("FP : " + fpCheck.Checked);
            Log.WriteLog("BY : " + alwaysCheckBox.Checked);


            if (fpCheck.Checked == false && alwaysCheckBox.Checked == true)
            {
                MessageBox.Show("지문서버에 등록되지 않은 카드입니다.  항시통과 설정할 수 없습니다.");
                save.Enabled = true;
                return;
            }

            Log.WriteLogTmp("..................JJJJJJJJJJJJJJJJJJJ.................");

            Gloval.initQuery();

            if (curMode.Equals("VIEW"))
            {
                Boolean tmpFlag = false;

                if (curTab.Equals("CARD") || curTab.Equals("RIGHT"))
                {

                    tmpFlag = showCardCompare();

                    if (!tmpFlag)
                    {
                        MessageBox.Show(" 변경 사항이 없습니다.");
                        save.Enabled = true;
                        return;
                    }
                }
            }
            else if (curMode.Equals("REG"))
            {

                if (checkDuplicate(cardNum.Text))
                {
                    return;
                }



                if (MessageBox.Show("신규 등록을 하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {



                    Boolean tmpFlag = false;
                    tmpFlag = insertReg();


                }
                else
                {
                    return;
                }
            }
            else if (curMode.Equals("MASS_CHANGE"))
            {

                ArrayList bidArr = getMultiBid();
                ArrayList empArr = getMultiEmp();
                if (bidArr == null || bidArr.Count < 1)
                {
                    save.Enabled = true;
                    MessageBox.Show("한개 이상의 카드를 선택해야 합니다.");
                    return;

                }

                if (bidArr.Count > maxChangeNum)
                {

                    save.Enabled = true;
                    MessageBox.Show("" + maxChangeNum + "장 까지 일괄 수정 가능합니다");
                    return;

                }

                makeMassChange(bidArr, empArr);


            }


            ///////////////////////////////////////////////////////////////////////////////////////
            // 통신 부분

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


            Log.WriteLogTmp("[BM_Reg.cs] clickSave () query : " + query);

            if (query == null || query.Equals(""))
            {
                MessageBox.Show(" 변경 사항이 없습니다.");
                save.Enabled = true;
                return;
            }

            else // MessageBox.Show(query);

                // string rNumString = "0";   // req.updateImage("BMS011", query, "BMIM", Gloval.newSazin);
                // string rNumString = req.update ("BMS011", query, "BMI");



                // Log.WriteLogTmp("[BM_Reg_001.cs] req.updateImage () BEFORE");
                // string rNumString = req.updateImage("BMS011", query, "BMIM", Gloval.newSazin);
                // string rNumString = req.update ("BMS011", query, "BMI");


                ////////////////////////////////////////////////////////////////////////////////
                query = Gloval.protocolString + "&" + query;
            // Log.WriteLogTmp("query 1 : " + query);
            int index = -1;
            index = query.IndexOf("&");

            // Log.WriteLogTmp("............index : " + index);

            ////////////////////////////////////////////////////////////////////////////////
            string rNumString = "";
            if (curMode.Equals("REG"))
            {
                //신규등록
                rNumString = req.updateImage("BMS011", query, "BMSI", Gloval.newSazin);

            }
            else if (curMode.Equals("VIEW"))
            {
                if (!checkVaild())
                {
                    return;
                }
                rNumString = req.updateImage("BMS011", query, "BMSU", Gloval.newSazin);
            }
            else if (curMode.Equals("MASS_CHANGE"))
            {
                rNumString = req.updateImage("BMS011", query, "BMMU", Gloval.newSazin);
            }





            // string rNumString = "0";


            if (rNumString != "0")
            {
                MessageBox.Show("장애 발생");
                Cursor = Cursors.Default;
                return;
            }


            if (rNumString != "0")
            {
                MessageBox.Show("수정 / 입력 실패");

            }
            else
            {
                MessageBox.Show("수정 / 입력 성공");



            }


            if (curMode.Equals("REG"))
            {
                string Check = req.update("BMS0011", qm.getQuery("REG001_LOG_N", "1", cardNum.Text), "BMI");
                if (!Check.Equals("0")) MessageBox.Show("로그 저장 실패..");

            }
            else if (curMode.Equals("VIEW"))
            {
                string Check = req.update("BMS0011", qm.getQuery("REG001_LOG_M", "1", cardNum.Text), "BMI");
                if (!Check.Equals("0")) MessageBox.Show("로그 저장 실패..");
            }

            else if (curMode.Equals("MASS_CHANGE"))
            {
                string Check = req.update("BMS0011", qm.getQuery("REG001_LOG_MM", "1"), "BMI");
                if (!Check.Equals("0")) MessageBox.Show("로그 저장 실패..");
            }

            ///////////////////////////////////////////////////////////////////////////////////////

            initializeControl("VIEW");   // 수정

            cardGroupBox.BackColor = viewColor;

            // MessageBox.Show(msg + " " + tmp + " 성공 " + curMode);
            sazinChange = false;


            rightSave.Enabled = false;

            TAB.SelectedTab = cardTab;
            clearAllCardCheck();

            if (dataGridView1.Rows.Count < 1) getDataAfterSave();
            else
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
                getDataAfterSave();
                showRightDetail();
                changeNewRight();

            }
            this.Cursor = Cursors.Default;


        }

        public Boolean checkSSNO()
        {
            if (ssno.Text == null || ssno.Text.Trim().Equals(""))
            {
                MessageBox.Show("사번을 확인하세요");
                return false;
            }
            return true;
        }

        public Boolean checkDuplicate(string bid)
        {
            Boolean flag = true;


            string duplicateCheckQuery = "select BID from BADGE where BID = '" + addZero(bid) + "'";


            string rcNumString = req.select("BMS011", duplicateCheckQuery, "BMS");

            if (rcNumString != "0")
            {
                MessageBox.Show("[Reg.cs] clickPostPage () 1. 장애 발생");
                Cursor = Cursors.Default;
                return true;
            }
            else
            {
                if (ReturnDT.dt.Rows.Count > 0)
                {
                    MessageBox.Show("현재 BM 시스템에 등록되어있는 카드입니다. 관리자에게 문의하세요.");
                    return true;
                }
                else
                {
                    flag = false;
                    return flag;
                }
            }



        }


        public Boolean checkVaild()
        {
            if (name.Text.Equals("") || cardNum.Text.Equals(""))
            {
                MessageBox.Show("성명과 카드번호는 반드시 입력해야합니다");
                return false;
            }

            if (!isNumOrNot(cardNum.Text))
            {
                MessageBox.Show(" 카드번호는 반드시 숫자이어야합니다");
                return false;
            }

            if (checkVaildString(name.Text) || checkVaildString(email.Text) || checkVaildString(address.Text) || checkVaildString(cardName.Text))
            {
                MessageBox.Show(" | , ^ , ~ ,&  특수문자는 사용할수없습니다");
                return false;
            }


            return true;
        }



        public Boolean checkVaildString(string _str)
        {
            int i = -1;
            i = _str.IndexOf("|");
            i = _str.IndexOf("&");
            i = _str.IndexOf("~");
            i = _str.IndexOf("^");
            if (i > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void setColor(Control control, Color color)
        {
            if (!control.Text.Equals("현재데이터유지")) control.BackColor = color;
        }


        private void clear(object sender, EventArgs e)
        {
            TAB.SelectedTab = cardTab;
            clearAllCardCheck();
            clearAllCardData();
            // clearAllPlantRight();

            cardGroupBox.BackColor = viewColor;

            initializeControl("VIEW");

            // getDataRightFromDB ();

        }

        private void selectionChanged(object sender, EventArgs e)
        {
            Log.WriteLogTmp("========================================================== [Reg.cs] selectionChanged ().........");
            refreshThis();
        }

        public void refreshThis()
        {

            if (dataGridView1.CurrentCell == null) return;

            // Log.WriteLogTmp("[Reg.cs] refreshThis 0 (" + name.Text + ", " + name3.Text + ").........");

            Log.WriteLog("[Reg.cs] refreshThis () selected Row Num : " + dataGridView1.CurrentCell.RowIndex);
            if (curMode.Equals("MASS_CHANGE")) return;

            int cNum = dataGridView1.CurrentCell.ColumnIndex;
            if (cNum == 2) return;

            this.Cursor = Cursors.WaitCursor;

            initializeControl("VIEW");
            // getDataRightFromDB ();

            int _curRow = (dataGridView1.CurrentCell.RowIndex) + (listPerPage * (curPage - 1));

            Log.WriteLog("[Reg.cs] refreshThis () listArr.Couht : " + listArr.Count + ",  curRow : " + _curRow);

            // MessageBox.Show("" + _curRow);

            ListUnit lu = null;
            if (listArr.Count > _curRow)
            {
                lu = (ListUnit)listArr[_curRow];
                // MessageBox.Show("LIST [" + _curRow + "] " + lu.getName() + " | " + lu.getID() + " | " + lu.getDepartment());
                // if (curMode.Equals ("VIEW") && curTab.Equals ("CARD"))  showDetail(lu.getID());
                // if (curMode.Equals("VIEW") && curTab.Equals("RIGHT")) showRightDetail();


                showDetail(lu.getBID(), lu.getEmpID());

                /*
                name3.Text = name.Text;
                commonCardNum3.Text = commonCardNum.Text;
                birthday3.Text = birthday.Text;
                gender3.Text = gender.Text;
                */


                // Log.WriteLogTmp("[Reg.cs] refreshThis 1 (" + name.Text + ", " + name2.Text + ", " + name3.Text + ").........");
                showRightDetail();
                // Log.WriteLogTmp("[Reg.cs] refreshThis 2 (" + name.Text + ", " + name2.Text + ", " + name3.Text + ").........");
                showSazin(lu.getEmpID());
                // Log.WriteLogTmp("[Reg.cs] refreshThis 3 (" + name.Text + ", " + name2.Text + ", " + name3.Text + ").........");
            }

            // JSJ 2016-02-01
            // fpImg.Image = null;
            // fpImg.BackColor = Color.Silver;

            if (!tmpNum.Equals("") || tmpNum != null) drawFinger(Convert.ToInt32(tmpNum.Text));
            else drawFinger(-1);


            sazinChange = false;

            this.Cursor = Cursors.Default;


        }

        public Boolean validatePlantCheckBox()
        {
            Log.WriteLog("validatePlantCheckBox ()");

            if (curTab.Equals("RIGHT"))
            {
                msg = "";

                if (plant1CheckBox.Checked && plant1CheckBox.Enabled) msg = msg + "1발, , ";
                if (plant2CheckBox.Checked && plant2CheckBox.Enabled) msg = msg + "2발, ";
                if (plant3CheckBox.Checked && plant3CheckBox.Enabled) msg = msg + "3발, ";
                if (plant4CheckBox.Checked && plant4CheckBox.Enabled) msg = msg + "4발, ";

                if (msg.Equals("")) { msg = "1개 이상의 시스템을 선택하세요"; return false; }
            }

            return true;
        }

        public Boolean validatePlantGrid()
        {
            msg = "";

            if (plant1Grid.Enabled == false && plant2Grid.Enabled == false && plant3Grid.Enabled == false && plant4Grid.Enabled && false)
            {
                msg = "1개 이상의 발전소를 선택하세요";
                return false;
            }

            return true;
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


        public void readDataTable(DataTable dt)
        {
            foreach (DataColumn c in dt.Columns)
            {
                string name = c.ColumnName;

                Log.WriteLog("");
                Log.WriteLog("          [SERVER ==> CLIENT] (" + c.ColumnName + ")");
                Log.WriteLog("          [SERVER ==> CLIENT] rowCount : " + dt.Rows.Count);



                string valueTmp = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string value = dt.Rows[i][c.ColumnName].ToString();
                    valueTmp = valueTmp + value + ",  ";
                }
                Log.WriteLog("                              : " + valueTmp);





            }
            Log.WriteLog("\n\n");
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void displayList(int pageNum)
        {

            Log.WriteLog("[Reg.cs] dsplayList (" + pageNum + ") rowNum : " + rowNum);
            string bidTmp = "";
            string empIdTmp = "";
            string seq = "";

            if (rowNum > 0)
            {
                for (int i = 0; i < rowNum; i++)
                {
                    seq = ReturnDT.dt.Rows[i]["SEQ"].ToString();
                    string name = ReturnDT.dt.Rows[i]["NAME"].ToString();
                    bidTmp = ReturnDT.dt.Rows[i]["BID"].ToString();
                    empIdTmp = ReturnDT.dt.Rows[i]["EMPID"].ToString();
                    string department = ReturnDT.dt.Rows[i]["DEPARTMENT"].ToString();
                    this.dataGridView1.Rows[i].Cells[0].Value = name;
                    this.dataGridView1.Rows[i].Cells[1].Value = department;
                    this.dataGridView1.Rows[i].Cells[2].Value = "";
                    if (ReturnDT.dt.Rows[i]["DELETE_FLAG"].ToString().Equals("Y"))
                    {
                        this.dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.Gray;
                        this.dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.Gray;

                    }
                    ListUnit lu = new ListUnit();
                    lu.setName(name);
                    lu.setBid(bidTmp);
                    lu.setEmpId(empIdTmp);
                    lu.setDepartment(department);
                    listArr.Add(lu);
                }

                lastNum = Convert.ToInt32(seq);
            }

            Log.WriteLog("[Reg.cs] dsplayList (" + pageNum + ") listArr.Count : " + listArr.Count);

        }

        private void clickPostPage(object sender, EventArgs e)
        {

            curPage = Convert.ToInt32(curPageLabel.Text) + 1;
            curPageLabel.Text = "" + curPage;


            int ea = (int)pageListNum[curPage - 1];

            firstIndex = firstIndex + preEA;


            if (curPage >= pageListNum.Count)
            {
                post.Visible = false;
                // return;
            }

            // MessageBox.Show("firstIndex : " + firstIndex + ",   " + ea + " EA / " + totalListNum);

            preEA = ea;

            ////////////////////////////////////////////////////////////////////////////////////////////////

            rowNum = ea;

            this.dataGridView1.Rows.Clear();
            util.makeCell(this.dataGridView1, ea, 3);
            dataGridView1.ClearSelection();

            this.Cursor = Cursors.WaitCursor;

            string query = "select  TOP " + ea + " b.description as NAME, "
                    + " b.SEQ as SEQ, "
                    + " e.NAME_1 as personName, "
                    + " b.bid as BID, "
                    + " b.EMPID as EMPID, "
                    + " d.DESCRIPTION  as DEPARTMENT, "
                    + " d2.DESCRIPTION as DIVISION "
                    + " from BADGE b, EMP e, DEPARTMENT d, DIVISION d2 ,BADGE_TYPE_CODE bt "
                    + " where b.EMPID = e.ID and e.DEPARTMENT = d.ID and e.DIVISION = d2.ID and bt.ID = b.TYPE "
                    + " and b.SEQ < " + lastNum
                // + " order by b.MODIFY_DATE desc";
                    + " order by SEQ desc";




            string rNumString = req.select("BMS011", query, "BMS");

            if (rNumString != "0")
            {
                MessageBox.Show("[Reg.cs] clickPostPage()  장애 발생");
                Cursor = Cursors.Default;
                return;
            }

            displayList(curPage);

            this.Cursor = Cursors.Default;

        }



        public void showCardList(int num)
        {

            Log.WriteLog("[Reg.cs] showCardList ()");

            string query = "select COUNT(*) as count from BADGE b, EMP e, DEPARTMENT d, DIVISION d2 ,BADGE_TYPE_CODE bt"
                            + " where b.EMPID = e.ID "
                            + "and e.DEPARTMENT = d.ID and e.DIVISION = d2.ID and bt.ID = b.TYPE and b.DELETE_FLAG != 'Y' "
                            + makeClause();

            if (num == 0)
                query = "select COUNT(*) as count from BADGE b, EMP e, DEPARTMENT d, DIVISION d2 ,BADGE_TYPE_CODE bt"
                           + " where b.EMPID = e.ID "
                           + "and e.DEPARTMENT = d.ID and e.DIVISION = d2.ID and bt.ID = b.TYPE  "
                           + makeClause();

            string resultString = req.select("BMS011", query, "BMS");

            Log.WriteLogTmp("[Reg.cs] showCardList () ....................... query 1 : " + query);

            if (resultString != "0")
            {
                MessageBox.Show("[Reg.cs] showCardList () 장애 발생");
                return;
            }

            string value = ReturnDT.dt.Rows[0]["count"].ToString();

            if (num == 0) totalListNum = Convert.ToInt32(value);
            else totalListNum = 100;
            totalCount.Text = "" + totalListNum + " 건";

            // MessageBox.Show("(1) totalListNum : " + totalListNum);

            makePage();



            if (totalListNum <= listPerPage) rowNum = totalListNum;
            else rowNum = listPerPage;


            listArr = new ArrayList();

            // MessageBox.Show("listPerPage : " + listPerPage);

            this.dataGridView1.Rows.Clear();
            util.makeCell(this.dataGridView1, rowNum, 3);
            // dataGridView1.ClearSelection();

            if (totalListNum < 1) return;

            this.Cursor = Cursors.WaitCursor;

            if (num == 0)
                query = "select  TOP " + rowNum + " b.description as NAME, "
                     + " b.SEQ as SEQ, "
                     + " e.NAME_1 as personName, "
                     + " b.bid as BID, "
                     + " b.EMPID as EMPID, "
                     + " d.DESCRIPTION  as DEPARTMENT, "
                     + " d2.DESCRIPTION as DIVISION ,"
                     + " b.DELETE_FLAG as DELETE_FLAG"
                     + " from BADGE b, EMP e, DEPARTMENT d, DIVISION d2 ,BADGE_TYPE_CODE bt "
                     + " where b.EMPID = e.ID and e.DEPARTMENT = d.ID and e.DIVISION = d2.ID and bt.ID = b.TYPE  "
                     + makeClause()
                    // + " order by b.MODIFY_DATE desc";
                     + " order by SEQ desc";
            else
            {
                query = "select  TOP " + rowNum + " b.description as NAME, "
                        + " b.SEQ as SEQ, "
                        + " e.NAME_1 as personName, "
                        + " b.bid as BID, "
                        + " b.EMPID as EMPID, "
                        + " d.DESCRIPTION  as DEPARTMENT, "
                        + " d2.DESCRIPTION as DIVISION ,"
                        + " b.DELETE_FLAG as DELETE_FLAG"
                        + " from BADGE b, EMP e, DEPARTMENT d, DIVISION d2 ,BADGE_TYPE_CODE bt "
                        + " where b.EMPID = e.ID and e.DEPARTMENT = d.ID and e.DIVISION = d2.ID and bt.ID = b.TYPE and b.DELETE_FLAG != 'Y' "
                        + makeClause()
                    // + " order by b.MODIFY_DATE desc";
                        + " order by SEQ desc";
            }



            resultString = req.select("BMS011", query, "BMS");

            Log.WriteLogTmp("[Reg.cs] showCardList () ....................... query 2 : " + query);

            if (resultString != "0")
            {
                MessageBox.Show("[Reg.cs] showCardList ()  장애 발생");
                return;
            }

            displayList(1);


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            DataGridView[] pgv = { plant1Grid, plant2Grid, plant3Grid, plant4Grid };

            rightArr1 = new ArrayList();
            rightArr2 = new ArrayList();
            rightArr3 = new ArrayList();
            rightArr4 = new ArrayList();

            ArrayList[] pal = { rightArr1, rightArr2, rightArr3, rightArr4 };

            for (int jj = 0; jj < pgv.Length; jj++)
            {
                pgv[jj].Rows.Clear();
                pgv[jj].ClearSelection();

                query = "select ID as id, DESCRIPTION as description from ACCESSLVL where PLANT_NUM = " + (jj + 1) + " order by DESCRIPTION asc";

                Log.WriteLog("[Reg.cs] showCardList () query ======================== : " + query);


                resultString = req.select("BMS011", query, "BMS");

                readDataTable(ReturnDT.dt);

                if (resultString != "0")
                {
                    MessageBox.Show("[Reg.cs] showCardList () plantGridView [" + jj + "]  장애 발생");
                    return;
                }

                int plantRowNum = ReturnDT.dt.Rows.Count;

                util.makeCell(pgv[jj], plantRowNum, 2);

                for (int i = 0; i < plantRowNum; i++)
                {

                    string id = ReturnDT.dt.Rows[i]["ID"].ToString();
                    string desc = ReturnDT.dt.Rows[i]["description"].ToString();

                    Log.WriteLog("[Reg.cs] showCardList () plant [" + jj + "] [" + i + "] (" + id + ")" + desc);

                    ru = new RightUnit();

                    ru.setID(id);
                    ru.setDesc(desc);

                    pal[jj].Add(ru);

                    pgv[jj].Rows[i].Cells[0].Value = desc;
                    pgv[jj].Rows[i].Cells[1].Value = "";
                }

            }




            clearAllPlantRight();

            refreshThis();

            this.Cursor = Cursors.Default;

        }



        public void showListAfterSave()
        {

            Log.WriteLog("[Reg.cs] showListAfterSave ()");


            totalListNum = 25;
            totalCount.Text = "" + totalListNum + " 건";
            makePage();

            if (totalListNum <= listPerPage) rowNum = totalListNum;
            else rowNum = listPerPage;

            listArr = new ArrayList();

            this.dataGridView1.Rows.Clear();
            util.makeCell(this.dataGridView1, rowNum, 3);

            if (totalListNum < 1) return;

            this.Cursor = Cursors.WaitCursor;

            string query = "select  TOP " + rowNum + " b.description as NAME, "
                    + " b.SEQ as SEQ, "
                    + " e.NAME_1 as personName, "
                    + " b.bid as BID, "
                    + " b.EMPID as EMPID, "
                    + " d.DESCRIPTION  as DEPARTMENT, "
                    + " d2.DESCRIPTION as DIVISION ,"
                    + " b.DELETE_FLAG as DELETE_FLAG "
                    + " from BADGE b, EMP e, DEPARTMENT d, DIVISION d2 "
                    + " where b.EMPID = e.ID and e.DEPARTMENT = d.ID and e.DIVISION = d2.ID and b.DELETE_FLAG !='Y' "
                    + " order by b.MODIFY_DATE desc";
            // + " order by SEQ asc";



            string resultString = req.select("BMS011", query, "BMS");

            Log.WriteLog("[Reg.cs] showListAfterSave () ....................... query : " + query);

            if (resultString != "0")
            {
                MessageBox.Show("[Reg.cs] showCardList () 2.  장애 발생");
                return;
            }

            displayList(1);


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /*
            DataGridView[] pgv = { plant1Grid, plant2Grid, plant3Grid, plant4Grid };

            rightArr1 = new ArrayList();
            rightArr2 = new ArrayList();
            rightArr3 = new ArrayList();
            rightArr4 = new ArrayList();

            ArrayList[] pal = { rightArr1, rightArr2, rightArr3, rightArr4 };

            for (int jj = 0; jj < pgv.Length; jj++)
            {
                pgv[jj].Rows.Clear();
                pgv[jj].ClearSelection();

                query = "select ID as id, DESCRIPTION as description from ACCESSLVL where PLANT_NUM = " + (jj + 1);

                Log.WriteLog("[Reg.cs] showListAfterSave () query ======================== : " + query);


                resultString = req.select("BMS011", query, "BMS");

                readDataTable(ReturnDT.dt);

                if (resultString != "0")
                {
                    MessageBox.Show("[Reg.cs] showListAfterSave () plantGridView [" + jj + "]  장애 발생");
                    return;
                }

                int plantRowNum = ReturnDT.dt.Rows.Count;

                util.makeCell(pgv[jj], plantRowNum, 2);

                for (int i = 0; i < plantRowNum; i++)
                {

                    string id = ReturnDT.dt.Rows[i]["ID"].ToString();
                    string desc = ReturnDT.dt.Rows[i]["description"].ToString();

                    Log.WriteLog("[Reg.cs] showListAfterSave () plant [" + jj + "] [" + i + "] (" + id + ")" + desc);

                    ru = new RightUnit();

                    ru.setID(id);
                    ru.setDesc(desc);

                    pal[jj].Add(ru);

                    pgv[jj].Rows[i].Cells[0].Value = desc;
                    pgv[jj].Rows[i].Cells[1].Value = "";
                }

            }

            */




            clearAllPlantRight();

            refreshThis();

            // this.Cursor = Cursors.Default;

        }

        public void initDetailLabelColor()
        {
            Label[] tmpLabel = { badge_0, badge_1, badge_2, badge_3, badge_4 };

            for (int jj = 0; jj < 5; jj++) tmpLabel[jj].ForeColor = Color.DarkGray;
        }

        public void showDetail(string _bid, string _empIDParam)
        {

            Log.WriteLog("[Reg.cs] showDetail (" + _bid + ", " + _empIDParam + ")");

            curDetailNum = 0;


            clearAllCardDetail();

            // this.Cursor = Cursors.WaitCursor;

            detailArr = new ArrayList();



            initDetailLabelColor();

            Label[] tmpLabel = { badge_0, badge_1, badge_2, badge_3, badge_4 };

            for (int jj = 0; jj < 5; jj++) tmpLabel[jj].Visible = false;



            du = new DetailUnit();
            curDetailNum = 0;

            ///////////////////////////////// (1) EMP /////////////////////////////////////////////////
            string query = "select NAME_1 as name, BIRTH_DATE as birth, "
                    + "GENDER as gender, SSNO as ssno, TEL as tel, EMAIL as email, ADDRESS as address, "
                    + "ID as empID, "
                // + "(select DESCRIPTION from DEPARTMENT de where de.ID = e.DEPARTMENT) as department, "
                    + "DEPARTMENT as department, "
                // + "(select DESCRIPTION from DIVISION di where di.ID = e.DIVISION) as division, "
                    + "DIVISION as division, "
                // + "(select DESCRIPTION from TITLE t where t.ID = e.TITLE) as title "
                    + "TITLE as title "
                    + "from EMP e where ID = " + _empIDParam;


            string resultString = req.select("BMS011", query, "BMS");

            if (resultString != "0")
            {
                curDetailNum = -1;
                clearAllCardDetail();
                MessageBox.Show("[Reg.cs] showDetail () 1. 장애 발생");
                return;
            }

            readDataTable(ReturnDT.dt);



            if (ReturnDT.dt.Rows.Count == 0)
            {
                curDetailNum = -1;
                clearAllCardDetail();
                return;
            }


            for (int i = 0; i < ReturnDT.dt.Rows.Count; i++)
            {
                string _empID = ReturnDT.dt.Rows[0]["empID"].ToString();
                string _name = ReturnDT.dt.Rows[0]["name"].ToString();
                DateTime _birth = (DateTime)ReturnDT.dt.Rows[0]["birth"];
                string _gender = ReturnDT.dt.Rows[0]["gender"].ToString();
                if (_gender.Equals("1")) _gender = "남자";
                else if (_gender.Equals("2")) _gender = "여자";
                else _gender = "기타";
                string _ssno = ReturnDT.dt.Rows[0]["ssno"].ToString();
                int _department = (int)ReturnDT.dt.Rows[0]["department"];
                int _division = (int)ReturnDT.dt.Rows[0]["division"];
                int _title = (int)ReturnDT.dt.Rows[0]["title"];
                string _email = ReturnDT.dt.Rows[0]["email"].ToString();
                string _tel = ReturnDT.dt.Rows[0]["tel"].ToString();
                string _address = ReturnDT.dt.Rows[0]["address"].ToString();

                du.setEmpID(_empID);
                du.setName(_name);
                du.setBirth(_birth);
                du.setGender(_gender);
                du.setSsno(_ssno);
                du.setDepartment(_department);
                du.setDivision(_division);
                du.setTitle(_title);
                du.setEmail(_email);
                du.setTel(_tel);
                du.setAddress(_address);

                this.name.Text = this.name2.Text = this.name3.Text = _name;

                // if (this.name3.Text.Equals("고리-10025")) MessageBox.Show("1................ 고리-10025");


                this.birthday.Value = new DateTime(_birth.Year, _birth.Month, _birth.Day, 10, 10, 0);
                this.birthday2.Text = this.birthday3.Text = "" + _birth.Year + "년" + _birth.Month + "월" + _birth.Day + "일";




                this.gender.Text = this.gender2.Text = this.gender3.Text = _gender;
                this.ssno.Text = _ssno;
                this.tel.Text = _tel;
                this.email.Text = _email;
                this.address.Text = _address;
                this.department.Text = Bm_Main.getDeparmentDesc(_department);

                this.division.Text = Bm_Main.getDivisionDesc(_division);
                this.title.Text = Bm_Main.getTitleDesc(_title);

            }


            // if (this.name.Text.Equals("고리-10025")) MessageBox.Show("2.................... 고리-10025");
            Log.WriteLog("JJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJ (2) name3.Text : " + this.name3.Text);

            ///////////////////////////////// (2) BADGE /////////////////////////////////////////////////
            query = "select BID as bid, DESCRIPTION as cardName, SEQ as issueNum, PIN as pin, "

                    + "TYPE as badgeType, "
                    + "STATUS_1 as badgeStatus, "
                    + "FORMAT as badgeFormat, "
                    + "ISSUE_TYPE as issueType, "
                    + "ISSUE_REASON as issueReason, "
                // + "(select DESCRIPTION from BADGE_TYPE_CODE bt where b.TYPE = bt.ID) as badgeType, "
                // + "(select DESCRIPTION from BADGE_STAT_CODE bs where b.STATUS_1 = bs.ID) as badgeStatus, "
                // + "(select DESCRIPTION from BADGE_FORMAT_CODE bf where b.FORMAT = bf.ID) as badgeFormat, "
                // + "(select DESCRIPTION from ISSUE_TYPE_CODE it where b.ISSUE_TYPE = it.ID) as issueType, "
                // + "(select DESCRIPTION from ISSUE_REASON_CODE ir where b.ISSUE_REASON = ir.ID) as issueReason, " 
                    + "ACTIVATE_DATE as active, DEACTIVATE_DATE as deactive,DEACTIVATE_DATE1 as deactive1,DEACTIVATE_DATE2 as deactive2,DEACTIVATE_DATE3 as deactive3,DEACTIVATE_DATE4 as deactive4, "
                    + "FP_1 as fpNum, FP_2 as fpNum2, BYPASS_FLAG as bypass, PT as pt, "
                    + "ACS_1 as acs1, ACS_2 as acs2, ACS_3 as acs3, ACS_4 as acs4, ACS_5 as mdm, VM as vm "
                    + "from BADGE b where BID = '" + _bid + "'";


            resultString = req.select("BMS011", query, "BMS");

            if (resultString != "0")
            {
                curDetailNum = -1;
                MessageBox.Show("[Reg.cs] showDetail () 2. 장애 발생");
                return;
            }


            Log.WriteLog("[Reg.cs] showDetail () ReturnDT.dt.Rows.Count : " + ReturnDT.dt.Rows.Count);

            if (ReturnDT.dt.Rows.Count == 0)
            {
                curDetailNum = -1;
                clearAllCardDetail();
                return;
            }


            if (ReturnDT.dt.Rows.Count > 1 && ReturnDT.dt.Rows.Count < 6)
            {
                tmpLabel[0].ForeColor = Color.Black;
                curDetailNum = 0;

                for (int j = 0; j < ReturnDT.dt.Rows.Count; j++)
                {
                    tmpLabel[j].Visible = true;
                }
            }

            Log.WriteLog("JJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJ (3) name3.Text : " + this.name3.Text);


            // readDataTable(ReturnDT.dt);

            // du.setCardEA(ReturnDT.dt.Rows.Count);


            for (int j = 0; j < ReturnDT.dt.Rows.Count; j++)
            {
                string bid = ReturnDT.dt.Rows[j]["bid"].ToString();
                string cardName = ReturnDT.dt.Rows[j]["cardName"].ToString();
                string issueNum = ReturnDT.dt.Rows[j]["issueNum"].ToString();
                string pin = ReturnDT.dt.Rows[j]["pin"].ToString();
                int badgeType = (int)ReturnDT.dt.Rows[j]["badgeType"];
                int badgeStatus = (int)ReturnDT.dt.Rows[j]["badgeStatus"];
                int badgeFormat = (int)ReturnDT.dt.Rows[j]["badgeFormat"];
                int issueType = (int)ReturnDT.dt.Rows[j]["issueType"];
                int issueReason = (int)ReturnDT.dt.Rows[j]["issueReason"];
                DateTime active = (DateTime)ReturnDT.dt.Rows[j]["active"];
                DateTime deactive = (DateTime)ReturnDT.dt.Rows[j]["deactive"];
                DateTime deactive1;
                DateTime deactive2;
                DateTime deactive3;
                DateTime deactive4;
                try
                {
                    deactive1 = (DateTime)ReturnDT.dt.Rows[j]["deactive1"];
                    deactive2 = (DateTime)ReturnDT.dt.Rows[j]["deactive2"];
                    deactive3 = (DateTime)ReturnDT.dt.Rows[j]["deactive3"];
                    deactive4 = (DateTime)ReturnDT.dt.Rows[j]["deactive4"];

                }
                catch (Exception e)
                {

                    deactive1 = DateTime.Now;
                    deactive2 = DateTime.Now;
                    deactive3 = DateTime.Now;
                    deactive4 = DateTime.Now;

                }

                string acs1 = "-1";
                if (ReturnDT.dt.Rows[j]["acs1"] != null) acs1 = ReturnDT.dt.Rows[j]["acs1"].ToString();

                string acs2 = "-1";
                if (ReturnDT.dt.Rows[j]["acs2"] != null) acs2 = ReturnDT.dt.Rows[j]["acs2"].ToString();

                string acs3 = "-1";
                if (ReturnDT.dt.Rows[j]["acs3"] != null) acs3 = ReturnDT.dt.Rows[j]["acs3"].ToString();
                string acs4 = "-1";
                if (ReturnDT.dt.Rows[j]["acs4"] != null) acs4 = ReturnDT.dt.Rows[j]["acs4"].ToString();
                string vm = "-1";
                if (ReturnDT.dt.Rows[j]["vm"] != null) vm = ReturnDT.dt.Rows[j]["vm"].ToString();

                string pt = "-1";
                if (ReturnDT.dt.Rows[j]["pt"] != null) pt = ReturnDT.dt.Rows[j]["pt"].ToString();

                string mdm = "-1";
                if (ReturnDT.dt.Rows[j]["mdm"] != null) mdm = ReturnDT.dt.Rows[j]["mdm"].ToString();

                string fpNum = ReturnDT.dt.Rows[j]["fpNum"].ToString();


                // JSJ 0804
                string fpNum2 = "-1";
                if (ReturnDT.dt.Rows[j]["fpNum2"] != null)
                {
                    if (!ReturnDT.dt.Rows[j]["fpNum2"].ToString().Equals(""))
                    {
                        fpNum2 = ReturnDT.dt.Rows[j]["fpNum2"].ToString();
                    }
                }

                string bypass = ReturnDT.dt.Rows[j]["bypass"].ToString();

                du.setFpNum(fpNum);
                du.setFpNum2(fpNum2);

                du.setBypass(bypass);




                // Log.WriteLog("........................ active : " + active);
                // Log.WriteLog("........................ active : " + (DateTime) ReturnDT.dt.Rows[i]["active"]);



                if (j == 0)
                {

                    this.cardNum.Text = this.commonCardNum.Text = this.commonCardNum2.Text = this.commonCardNum3.Text = bid;


                    this.cardName.Text = cardName;
                    this.issueNum.Text = issueNum;
                    this.pinNum.Text = pin;
                    this.cardType.Text = Bm_Main.getBadgeTypeDesc(badgeType);
                    this.cardStatus.Text = Bm_Main.getBadgeStatDesc(badgeStatus);
                    this.cardFormat.Text = Bm_Main.getBadgeFormatDesc(badgeFormat);
                    this.issueType.Text = Bm_Main.getIssueTypeDesc(issueType);
                    this.issueReason.Text = Bm_Main.getIssueReasonDesc(issueReason);
                    this.startDate.Value = active;


                    this.endDate.Value = deactive;

                    int _fpNum = -1;
                    _fpNum = Convert.ToInt32(fpNum);
                    drawFP(_fpNum);

                    int _fpNum2 = -1;
                    _fpNum2 = Convert.ToInt32(fpNum2);
                    // drawFP(_fpNum2);

                    if (bypass.Equals("1")) alwaysCheckBox.Checked = true;
                    else alwaysCheckBox.Checked = false;

                    if (vm.Equals("1")) vmCheck.CheckState = CheckState.Checked;
                    else vmCheck.CheckState = CheckState.Unchecked;

                    if (pt.Equals("2")) ptCheckBox.CheckState = CheckState.Checked;
                    else ptCheckBox.CheckState = CheckState.Unchecked;

                    if (mdm.Equals("1")) mdmCheck.CheckState = CheckState.Checked;
                    else mdmCheck.CheckState = CheckState.Unchecked;

                }



                du.setBid(bid);
                du.setCardName(cardName);

                du.setPin(pin);
                du.setIssueNum(issueNum);
                du.setBadgeType(badgeType);
                du.setBadgeStatus(badgeStatus);
                du.setBadgeFormat(badgeFormat);
                du.setIssueType(issueType);
                du.setIssueReason(issueReason);
                du.setActive(active);
                du.setDeactive(deactive);


                du.setAcs1Status(acs1);
                du.setAcs2Status(acs2);
                du.setAcs3Status(acs3);
                du.setAcs4Status(acs4);



                du.setVmStatus(vm);


                Log.WriteLogTmp("PT : " + pt);
                if (pt != null && !pt.Equals("")) du.setPT(Convert.ToInt32(pt));
                else du.setPT(1);

                du.setMdmStatus(mdm);


                if (acs1.Equals("1")) plant1RegCheckBox.CheckState = CheckState.Checked;
                else plant1RegCheckBox.CheckState = CheckState.Unchecked;
                if (acs2.Equals("1")) plant2RegCheckBox.CheckState = CheckState.Checked;
                else plant2RegCheckBox.CheckState = CheckState.Unchecked;
                if (acs3.Equals("1")) plant3RegCheckBox.CheckState = CheckState.Checked;
                else plant3RegCheckBox.CheckState = CheckState.Unchecked;
                if (acs4.Equals("1")) plant4RegCheckBox.CheckState = CheckState.Checked;
                else plant4RegCheckBox.CheckState = CheckState.Unchecked;




                DateTime[] tmpDt = { deactive1, deactive2, deactive3, deactive4 };

                getEndDate(cardNum.Text, plant1RegCheckBox.Checked, plant2RegCheckBox.Checked, plant3RegCheckBox.Checked, plant4RegCheckBox.Checked, tmpDt);

            }





        }

        public int findRightRow(ArrayList _rightArr, string acc)
        {
            int result = -1;

            for (int i = 0; i < _rightArr.Count; i++)
            {
                RightUnit ru = (RightUnit)_rightArr[i];

                // Log.WriteLog("\n\n[Reg.cs] findRightRow " + ru.getID () + ", " + ru.getDesc());

                if (ru.getID().Equals(acc))
                {
                    Log.WriteLog("[Reg.cs] findRightRow (" + acc + ") " + ru.getDesc());
                    result = i;
                    return result;
                }
            }

            return result;
        }

        public void showRightDetail()
        {
            Log.WriteLog("[Reg.cs] showRightDetail ().....................................................");

            clearAllPlantRight();




            Color activeColor = Color.Red;


            if (curDetailNum < 0) return;


            // JSJ 권한은 카드소유자 한명에 대해 여러장 카드 모두 동일
            // du = (DetailUnit) detailArr[0];

            string tmpBid = du.getBid();

            string query = "select RIGHT_1 as right_1, RIGHT_2 as right_2, RIGHT_3 as right_3, RIGHT_4 as right_4 from BADGE where BID = '" + tmpBid + "'";

            Log.WriteLog("[Reg.cs] showRightDetail () query : " + query);

            string resultString = req.select("BMS011", query, "BMS");

            Log.WriteLog("[Reg.cs] showRightDetail () resultString : " + resultString);

            if (resultString != "0")
            {
                MessageBox.Show("[Reg.cs] showRightDetail (BID : " + tmpBid + ") 1. 장애 발생");
                return;
            }


            if (ReturnDT.dt.Rows.Count == 0) return;


            // readDataTable(ReturnDT.dt);

            // ArrayList right1 = new ArrayList();
            // ArrayList right2 = new ArrayList();
            // ArrayList right3 = new ArrayList();
            // ArrayList right4 = new ArrayList();


            string totalRight_1 = ReturnDT.dt.Rows[0]["right_1"].ToString();
            string totalRight_2 = ReturnDT.dt.Rows[0]["right_2"].ToString();
            string totalRight_3 = ReturnDT.dt.Rows[0]["right_3"].ToString();
            string totalRight_4 = ReturnDT.dt.Rows[0]["right_4"].ToString();

            Log.WriteLogTmp("[BM_Reg_001.cs] showRightDetail () RIGHT_1 : " + totalRight_1);
            Log.WriteLogTmp("[BM_Reg_001.cs] showRightDetail () RIGHT_2 : " + totalRight_2);
            Log.WriteLogTmp("[BM_Reg_001.cs] showRightDetail () RIGHT_3 : " + totalRight_3);
            Log.WriteLogTmp("[BM_Reg_001.cs] showRightDetail () RIGHT_4 : " + totalRight_4);

            ArrayList arrTmp1 = separate(totalRight_1, "~");
            ArrayList arrTmp2 = separate(totalRight_2, "~");
            ArrayList arrTmp3 = separate(totalRight_3, "~");
            ArrayList arrTmp4 = separate(totalRight_4, "~");

            if (arrTmp1 != null && arrTmp1.Count > 0)
            {
                for (int i = 0; i < arrTmp1.Count; i++) Log.WriteLogTmp("[BM_Reg_001.cs] arrTmp1 [" + i + "] : " + (string)arrTmp1[i]);
            }
            if (arrTmp2 != null && arrTmp2.Count > 0)
            {
                for (int i2 = 0; i2 < arrTmp2.Count; i2++) Log.WriteLogTmp("[BM_Reg_001.cs] arrTmp2 [" + i2 + "] : " + (string)arrTmp2[i2]);
            }
            if (arrTmp3 != null && arrTmp3.Count > 0)
            {
                for (int i3 = 0; i3 < arrTmp3.Count; i3++) Log.WriteLogTmp("[BM_Reg_001.cs] arrTmp3 [" + i3 + "] : " + (string)arrTmp3[i3]);
            }
            if (arrTmp4 != null && arrTmp4.Count > 0)
            {
                for (int i4 = 0; i4 < arrTmp4.Count; i4++) Log.WriteLogTmp("[BM_Reg_001.cs] arrTmp4 [" + i4 + "] : " + (string)arrTmp4[i4]);
            }


            ////////////////////////////////////////////////////////////////// 0827 /////////////////////////////////////////////////////////////////////

            if (!totalRight_1.Equals("-100")) newPlantRight[0] = orgPlantRight[0] = totalRight_1;
            if (!totalRight_2.Equals("-100")) newPlantRight[1] = orgPlantRight[1] = totalRight_2;
            if (!totalRight_3.Equals("-100")) newPlantRight[2] = orgPlantRight[2] = totalRight_3;
            if (!totalRight_4.Equals("-100")) newPlantRight[3] = orgPlantRight[3] = totalRight_4;


            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            if (arrTmp1 != null && arrTmp1.Count > 0)
            {
                for (int i = 0; i < arrTmp1.Count; i++)
                {
                    int tmp = -1;
                    tmp = findRightRow(rightArr1, (string)arrTmp1[i]);

                    if (tmp > -1)
                    {
                        plant1CheckBox.CheckState = CheckState.Checked;
                        plant1CheckBox.ForeColor = activeColor;
                        checkPlantRight(plant1Grid, tmp, Color.Yellow);

                        // 0827 if (!orgPlantRight[0].Equals("")) newPlantRight[0] = orgPlantRight[0] = orgPlantRight[0] + "," + (tmp + 1);
                        // 0827 else newPlantRight[0] = orgPlantRight[0] = "" + (tmp + 1);

                        // right1.Add(tmp);
                    }

                    // Log.WriteLogTmp("[BM_Reg_001.cs] arrTmp1 [" + i + "] : " + (string)arrTmp1[i]);
                }

            }

            if (arrTmp2 != null && arrTmp2.Count > 0)
            {
                for (int i = 0; i < arrTmp2.Count; i++)
                {
                    int tmp = -1;
                    tmp = findRightRow(rightArr2, (string)arrTmp2[i]);

                    if (tmp > -1)
                    {
                        plant2CheckBox.CheckState = CheckState.Checked;
                        plant2CheckBox.ForeColor = activeColor;
                        checkPlantRight(plant2Grid, tmp, Color.Yellow);

                        // 0827 if (!orgPlantRight[1].Equals("")) newPlantRight[1] = orgPlantRight[1] = orgPlantRight[1] + "," + (tmp + 1);
                        // 0827 else newPlantRight[1] = orgPlantRight[1] = "" + (tmp + 1);
                    }
                }

            }

            if (arrTmp3 != null && arrTmp3.Count > 0)
            {
                for (int i = 0; i < arrTmp3.Count; i++)
                {
                    int tmp = -1;
                    tmp = findRightRow(rightArr3, (string)arrTmp3[i]);

                    if (tmp > -1)
                    {
                        plant3CheckBox.CheckState = CheckState.Checked;
                        plant3CheckBox.ForeColor = activeColor;
                        checkPlantRight(plant3Grid, tmp, Color.Yellow);

                        // 0827 if (!orgPlantRight[2].Equals("")) newPlantRight[2] = orgPlantRight[2] = orgPlantRight[2] + "," + (tmp + 1);
                        // 0827 else newPlantRight[2] = orgPlantRight[2] = "" + (tmp + 1);

                    }
                }

            }

            if (arrTmp4 != null && arrTmp4.Count > 0)
            {
                for (int i = 0; i < arrTmp4.Count; i++)
                {
                    int tmp = -1;
                    tmp = findRightRow(rightArr4, (string)arrTmp4[i]);

                    if (tmp > -1)
                    {
                        plant4CheckBox.CheckState = CheckState.Checked;
                        plant4CheckBox.ForeColor = activeColor;
                        checkPlantRight(plant4Grid, tmp, Color.Yellow);

                        // 0827 if (!orgPlantRight[3].Equals("")) newPlantRight[3] = orgPlantRight[3] = orgPlantRight[3] + "," + (tmp + 1);
                        // 0827 else newPlantRight[3] = orgPlantRight[3] = "" + (tmp + 1);

                    }
                }

            }





        }

        public void getDataRightFromDB()
        {


            Random r = new Random();
            int randomNum = r.Next(0, 7);

            Log.WriteLog("[Reg.cs] getDataRightFromDB () random Num : " + randomNum);

            int curRow = dataGridView1.CurrentCell.RowIndex;



            name.Text = name2.Text = name3.Text = cardName.Text = "" + this.dataGridView1.Rows[curRow].Cells[0].Value;
            department.Text = "" + this.dataGridView1.Rows[curRow].Cells[1].Value;
            this.dataGridView1.Rows[curRow].Cells[2].Value = "";

            if (randomNum % 3 == 0 || randomNum % 3 == 4)
            {
                birthday.Value = new DateTime(1980, 1, 1);
                birthday2.Text = birthday3.Text = "1980년1월1일";


                cardType.Text = "상시출입증";
                issueType.Text = "발급";
                cardStatus.Text = "ACTIVE";
                issueReason.Text = "분실";
                division.Text = "부서1";
                title.Text = "사장";
                cardFormat.Text = "12 DIGIT";
                issueType.Text = "신규";
                issueReason.Text = "신규";
            }
            else if (randomNum % 3 == 1 || randomNum % 3 == 5)
            {
                birthday.Value = new DateTime(1980, 1, 1);
                birthday2.Text = birthday3.Text = "1980년1월1일";
                cardType.Text = "일시출입증";
                cardStatus.Text = "LOST";
                issueType.Text = "회수";
                issueReason.Text = "신규";
                division.Text = "부서2";
                title.Text = "전무";
                cardFormat.Text = "10 DIGIT";
                issueType.Text = "분실";
                issueReason.Text = "분실";

            }
            else if (randomNum % 3 == 2 || randomNum % 3 == 6)
            {
                birthday.Value = new DateTime(1980, 1, 1);
                birthday2.Text = birthday3.Text = "1980년1월1일";
                cardType.Text = "방문객카드";
                cardStatus.Text = "SUSPENDED";
                issueType.Text = "몰라";
                division.Text = "부서3";
                title.Text = "부장";
                cardFormat.Text = "6 DIGIT";
                issueType.Text = "회수";
                issueReason.Text = "회수";
            }
            else
            {
                birthday.Value = new DateTime(1980, 1, 1);
                birthday2.Text = birthday3.Text = "1980년1월1일";
                cardType.Text = "견학카드";
                cardStatus.Text = "ACTIVE";
                issueType.Text = "몰라";
                division.Text = "부서5";
                title.Text = "과장";
                cardFormat.Text = "12 DIGIT";
                issueType.Text = "신규";
                issueReason.Text = "신규";

            }

            if (randomNum % 10 == 0) { cardNum.Text = commonCardNum.Text = "000971708290"; }
            else if (randomNum == 2) { cardNum.Text = commonCardNum.Text = "000971391154"; }
            else if (randomNum == 3) { cardNum.Text = commonCardNum.Text = "000971412226"; }
            else if (randomNum == 4) { cardNum.Text = commonCardNum.Text = "000971508402"; }
            else if (randomNum == 5) { cardNum.Text = commonCardNum.Text = "000971664594"; }
            else if (randomNum == 6) { cardNum.Text = commonCardNum.Text = "000971677346"; }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////

            clearAllPlantRight();

            Color activeColor = Color.Red;

            if (randomNum == 0 || randomNum == 4)
            {
                plant2CheckBox.CheckState = plant4CheckBox.CheckState = CheckState.Checked;
                plant2CheckBox.ForeColor = plant4CheckBox.ForeColor = activeColor;
                checkPlantRight(plant2Grid, 2, Color.Yellow);
                checkPlantRight(plant2Grid, 3, Color.Yellow);
                checkPlantRight(plant4Grid, 0, Color.Yellow);
                checkPlantRight(plant4Grid, 4, Color.Yellow);
            }
            else if (randomNum == 1 || randomNum == 5)
            {
                plant1CheckBox.CheckState = plant3CheckBox.CheckState = plant4CheckBox.CheckState = CheckState.Checked;
                plant1CheckBox.ForeColor = plant3CheckBox.ForeColor = plant4CheckBox.ForeColor = activeColor;
                checkPlantRight(plant1Grid, 0, Color.Yellow);
                checkPlantRight(plant1Grid, 1, Color.Yellow);
                checkPlantRight(plant3Grid, 2, Color.Yellow);
                checkPlantRight(plant4Grid, 3, Color.Yellow);
                checkPlantRight(plant4Grid, 4, Color.Yellow);

            }
            else if (randomNum == 2 || randomNum == 6)
            {
                plant2CheckBox.CheckState = plant3CheckBox.CheckState = plant4CheckBox.CheckState = CheckState.Checked;
                plant2CheckBox.ForeColor = plant3CheckBox.ForeColor = plant4CheckBox.ForeColor = activeColor;
                checkPlantRight(plant2Grid, 3, Color.Yellow);
                checkPlantRight(plant2Grid, 4, Color.Yellow);
                checkPlantRight(plant3Grid, 0, Color.Yellow);
                checkPlantRight(plant4Grid, 2, Color.Yellow);
            }
            else
            {
                plant2CheckBox.CheckState = plant1CheckBox.CheckState = CheckState.Checked;
                plant2CheckBox.ForeColor = plant1CheckBox.ForeColor = activeColor;
                checkPlantRight(plant1Grid, 3, Color.Yellow);
                checkPlantRight(plant2Grid, 3, Color.Yellow);
                checkPlantRight(plant2Grid, 2, Color.Yellow);
            }

        }

        private void selectedTab(object sender, TabControlEventArgs e)
        {
            Log.WriteLog("[Reg.cs] selectedTab () ..........................");

            /*
            if (curMode.Equals("ADD"))
            {
                TAB.SelectedTab = cardTab;
                return;
            }
            */


            Boolean flag = false;

            // JSJ
            // commonCardNum.Text = commonCardNum2.Text = commonCardNum3.Text = cardNum.Text;
            // MessageBox.Show("3 : " +  commonCardNum.Text + ", " + commonCardNum2.Text + ", " +  commonCardNum3.Text);

            if (e.TabPage == cardTab)
            {
                curTab = "CARD";
                // JSJ 0611
                fpIP.Enabled = false;

            }
            else if (e.TabPage == rightTab)
            {
                flag = validData();
                if (!flag) return;

                curTab = "RIGHT";

                name2.Text = name.Text.Trim();
                commonCardNum2.Text = commonCardNum.Text.Trim();
                birthday2.Text = "" + birthday.Value.Year + "년" + birthday.Value.Month + "월" + birthday.Value.Day + "일";

                gender2.Text = gender.Text.Trim();

                commonGroupBox2.Enabled = false;

                if (curMode.Equals("VIEW") || curMode.Equals("REG") || curMode.Equals("MASS_CHANGE"))
                {
                    //rightSave.Enabled = true;
                }
                else
                {
                    rightSave.Enabled = false;
                }

                doPlantCheck(plant1Grid, plant1CheckBox.Checked, plantTitle1);
                doPlantCheck(plant2Grid, plant2CheckBox.Checked, plantTitle2);
                doPlantCheck(plant3Grid, plant3CheckBox.Checked, plantTitle3);
                doPlantCheck(plant4Grid, plant4CheckBox.Checked, plantTitle4);

                // if (curMode.Equals("MASS_CHANGE")) clearAllPlantRight();
                // JSJ 0611
                fpIP.Enabled = false;
            }
            else if (e.TabPage == fpTab)
            {
                // JSJ 0611
                if (getIni("USER_TYPE").Equals("S"))
                {
                    fpIP.Enabled = true;
                }

                flag = validData();
                if (!flag) return;


                curTab = "FP";


                if (fpCheck.CheckState == CheckState.Checked)
                {
                    name3.Text = name.Text.Trim();
                    commonCardNum3.Text = commonCardNum.Text.Trim();
                    birthday3.Text = birthday2.Text = "" + birthday.Value.Year + "년" + birthday.Value.Month + "월" + birthday.Value.Day + "일";

                    gender3.Text = gender.Text.Trim();
                }
                else
                {
                    name3.Text = commonCardNum3.Text = birthday3.Text = gender3.Text = "";
                }

                commonGroupBox3.Enabled = false;

                tmpNum.Text = du.getFpNum();


            }

            // refreshThis ();

        }

        private void allChange(object sender, EventArgs e)
        {
            Boolean flag = allCheckBox.Checked;

            plant1CheckBox.Checked = plant2CheckBox.Checked = plant3CheckBox.Checked = plant4CheckBox.Checked = flag;
            plant1CheckBox.Enabled = plant2CheckBox.Enabled = plant3CheckBox.Enabled = plant4CheckBox.Enabled = true;

            Color c = Color.Black;
            if (flag) c = Color.Red;
            else c = Color.Black;



            plant1CheckBox.ForeColor = plant2CheckBox.ForeColor = plant3CheckBox.ForeColor = plant4CheckBox.ForeColor = c;

            if (!plant4CheckBox.Visible) plant4CheckBox.Checked = false;

        }

        public void doPlantCheck(DataGridView dgv, Boolean checkFlag, Label title)
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
            if (curMode.Equals("VIEW") || curMode.Equals("REG") || curMode.Equals("MASS_CHANGE"))
            {
                if (curTab.Equals("RIGHT"))
                {
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
                    else
                    {
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

            changeRightString = "";
            for (int i = 0; i < orgPlantRight.Length; i++)
            {
                orgPlantRight[i] = "";
                newPlantRight[i] = "";
            }



            DataGridView[] dg = { plant1Grid, plant2Grid, plant3Grid, plant4Grid };
            CheckBox[] cb = { plant1CheckBox, plant2CheckBox, plant3CheckBox, plant4CheckBox };

            allCheckBox.CheckState = CheckState.Unchecked;

            doPlantCheck(dg[0], false, plantTitle1);
            doPlantCheck(dg[1], false, plantTitle2);
            doPlantCheck(dg[2], false, plantTitle3);
            doPlantCheck(dg[3], false, plantTitle4);

            for (int j = 0; j < 4; j++)
            {
                cb[j].ForeColor = Color.Black;
                cb[j].CheckState = CheckState.Unchecked;
                dg[j].ClearSelection();
            }

        }



        private void clickNameRadio(object sender, EventArgs e)
        {
            disableRadio();
            nameTextBox.Enabled = true;
            nameTextBox.Text = "";
            departCombo.Text = divisionCombo.Text = typeCombo.Text = "전체";
            nameTextBox.Focus();
        }
        private void clickCardNumRadio(object sender, EventArgs e)
        {
            disableRadio();
            cardNumTextBox.Enabled = true;
            departCombo.Text = divisionCombo.Text = typeCombo.Text = "전체";
            cardNumTextBox.Text = "";
            cardNumTextBox.Focus();
        }


        private void clickDepartRadio(object sender, EventArgs e)
        {
            disableRadio();
            departCombo.Enabled = true;
            divisionCombo.Enabled = true;
            departCombo.Focus();
            nameTextBox.Text = cardNumTextBox.Text = departCombo.Text = typeCombo.Text = "전체";

        }


        private void clickDivisionRadio(object sender, EventArgs e)
        {
            disableRadio();
            departCombo.Enabled = true;
            divisionCombo.Enabled = true;
            divisionCombo.Focus();
            nameTextBox.Text = cardNumTextBox.Text = divisionCombo.Text = typeCombo.Text = "전체";

        }

        private void clickTypeRadio(object sender, EventArgs e)
        {
            disableRadio();
            typeCombo.Enabled = true;
            typeCombo.Focus();
            nameTextBox.Text = cardNumTextBox.Text = divisionCombo.Text = typeCombo.Text = "전체";

        }

        public void disableRadio()
        {
            nameTextBox.Enabled = false;
            cardNumTextBox.Enabled = false;
            departCombo.Enabled = false;
            divisionCombo.Enabled = false;
            typeCombo.Enabled = false;
            searchEndDate.Enabled = false;
        }

        public void enableAllPlantCheckBox(Boolean flag)
        {
            allCheckBox.Enabled = plant1CheckBox.Enabled = plant2CheckBox.Enabled = plant3CheckBox.Enabled = plant4CheckBox.Enabled = flag;
        }


        public void initializeControl(string _mode)
        {
            if (_mode.Equals("VIEW"))
            {
                initDetailDataColor();

                cardTab.BackColor = rightTab.BackColor = fpTab.BackColor = viewColor;

                curMode = "VIEW";
                modeLabel.Text = "조회모드입니다";
                modeLabel.ForeColor = viewLabelColor;   // Color.Blue;

                vmCheck.Enabled = fpCheck.Enabled = mdmCheck.Enabled = true;

                commonGroupBox.Enabled = true;
                cardNum.Enabled = true;
                cardName.Enabled = true;
                issueNum.Enabled = true;
                email.Enabled = true;
                deleteCardButton.Enabled = true;
                //endDateButton.Enabled = true;
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
                reRegButton.Enabled = true;
                personGroupBox.Enabled = true;
                name.Enabled = true;
                ssno.Enabled = true;
                department.Enabled = true;
                division.Enabled = true;
                title.Enabled = true;
                tel.Enabled = true;
                address.Enabled = true;
                sazin.Enabled = true;
                sazinButton.Enabled = true;
                cardNum.Enabled = false;
                commonGroupBox2.Enabled = false;
                fpCheck.Enabled = false;
                commonGroupBox3.Enabled = false;
                scanFP.Enabled = true;
                alwaysCheckBox.Enabled = true;
                finger1.Enabled = true;
                finger2.Enabled = true;
                fpIP.Enabled = false;
                reAddButton.Enabled = true;
                save.Enabled = true;
                regStartButton.Enabled = true;
                ppDeleteButton.Enabled = true;
                massChangeStart.Enabled = true;
                fpCardCheck.Enabled = false;
                vmCheck.Enabled = false;
                cardRestoreButton.Enabled = true;
                plantRegButton.Enabled = true;
                reRegButton.Enabled = true;

                setColor(cardType, Color.White);
                setColor(cardStatus, Color.White);
                setColor(cardFormat, Color.White);
                setColor(issueType, Color.White);
                setColor(issueReason, Color.White);
                setColor(department, Color.White);
                setColor(division, Color.White);
                setColor(title, Color.White);

                cardNum.Text = commonCardNum2.Text = commonCardNum3.Text = commonCardNum.Text;

                alwaysCheckBox.ForeColor = Color.Black;
                ptCheckBox.ForeColor = Color.Black;

                fpCardCheck.Checked = false;
                fpCheck.Checked = false;
                checkuseFlag(useFlag);


                endDateCheckBox.Checked = false;
                acs1EndDateTime.Enabled = false;
                acs2EndDateTime.Enabled = false;
                acs3EndDateTime.Enabled = false;
                acs4EndDateTime.Enabled = false;
                acs1EndDateButton.Enabled = false;
                acs2EndDateButton.Enabled = false;
                acs3EndDateButton.Enabled = false;
                acs4EndDateButton.Enabled = false;

                // JSJ 0611
                if (getIni("USER_TYPE").Equals("S"))
                {
                    fpIP.Enabled = true;
                }

            }
            else if (_mode.Equals("REG"))
            {

                cardTab.BackColor = rightTab.BackColor = fpTab.BackColor = regColor;


                curMode = "REG";
                modeLabel.Text = "신규등록모드입니다";
                modeLabel.ForeColor = regLabelColor;

                allCheckBox.Enabled = plant1CheckBox.Enabled = plant2CheckBox.Enabled = plant3CheckBox.Enabled = plant4CheckBox.Enabled = true;
                allCheckBox.CheckState = plant1CheckBox.CheckState = plant2CheckBox.CheckState = plant3CheckBox.CheckState = plant4CheckBox.CheckState = CheckState.Unchecked;
                allCheckBox.ForeColor = plant1CheckBox.ForeColor = plant2CheckBox.ForeColor = plant3CheckBox.ForeColor = plant4CheckBox.ForeColor = Color.Black;
                reRegButton.Enabled = false;

                plant1RegCheckBox.Checked = false;
                plant2RegCheckBox.Checked = false;
                plant3RegCheckBox.Checked = false;
                plant4RegCheckBox.Checked = false;
                reAddButton.Enabled = false;
                commonGroupBox.Enabled = true;
                cardNum.Enabled = true;
                cardGroupBox.Enabled = true;
                cardNum.Enabled = true;
                issueNum.Enabled = false;
                //  pinNum.Enabled = true;
                cardType.Enabled = true;
                cardStatus.Enabled = true;
                cardFormat.Enabled = true;
                issueType.Enabled = true;
                issueReason.Enabled = true;
                startDate.Enabled = true;
                endDate.Enabled = true;

                personGroupBox.Enabled = true;
                name.Enabled = true;
                ssno.Enabled = true;
                department.Enabled = true;
                division.Enabled = true;
                title.Enabled = true;
                tel.Enabled = true;
                address.Enabled = true;
                sazin.Enabled = true;
                sazinButton.Enabled = true;

                commonGroupBox2.Enabled = false;
                deleteCardButton.Enabled = false;
                commonGroupBox3.Enabled = false;
                scanFP.Enabled = true;
                alwaysCheckBox.Enabled = true;
                finger1.Enabled = true;
                finger2.Enabled = true;
                fpIP.Enabled = false;
              
                save.Enabled = true;
                regStartButton.Enabled = false;
                cardRestoreButton.Enabled = false;
                massChangeStart.Enabled = false;
              
                plantRegButton.Enabled = false;
                fpCardCheck.Enabled = true;
                vmCheck.Enabled = true;
                fpCheck.Enabled = true;
                ppDeleteButton.Enabled = false;
                DataGridView[] dgv = { plant1Grid, plant2Grid, plant3Grid, plant4Grid };
                for (int i = 0; i < dgv.Length; i++)
                {

                    for (int j = 0; j < dgv[i].Rows.Count; j++)
                    {
                        dgv[i].Rows[j].Cells[0].Style.BackColor = Color.FromArgb(221, 221, 255);
                    }

                }

                /////////////////////////////////////////////// 초기 데이터 /////////////////////////////////////////
                name.Text = commonCardNum.Text = email.Text = cardName.Text = "";
                birthday.Value = new DateTime(1980, 1, 1);
                gender.Text = "남자";
                cardNum.Text = issueNum.Text = pinNum.Text = "";
                startDate.Value = DateTime.Now;
                endDate.Value = DateTime.Now.AddYears(1);
                name.Text = ssno.Text = tel.Text = address.Text = "";
                showDefaultImage();

                alwaysCheckBox.CheckState = CheckState.Unchecked;

                acs1EndDateTime.Enabled = false;
                acs2EndDateTime.Enabled = false;
                acs3EndDateTime.Enabled = false;
                acs4EndDateTime.Enabled = false;
                acs1EndDateButton.Enabled = false;
                acs2EndDateButton.Enabled = false;
                acs3EndDateButton.Enabled = false;
                acs4EndDateButton.Enabled = false;

                setCodeDefault();
                fpCheck.Checked = true;

            }
            else if (_mode.Equals("MASS_CHANGE"))
            {

                cardTab.BackColor = rightTab.BackColor = fpTab.BackColor = massColor;

                curMode = "MASS_CHANGE";
                modeLabel.Text = "일괄변경모드입니다";
                modeLabel.ForeColor = massLabelColor;   // Color.Purple;

                allCheckBox.Enabled = plant1CheckBox.Enabled = plant2CheckBox.Enabled = plant3CheckBox.Enabled = plant4CheckBox.Enabled = true;
                allCheckBox.CheckState = plant1CheckBox.CheckState = plant2CheckBox.CheckState = plant3CheckBox.CheckState = plant4CheckBox.CheckState = CheckState.Unchecked;

                vmCheck.Enabled = fpCheck.Enabled = mdmCheck.Enabled = false;

                commonGroupBox.Enabled = false;
                cardNum.Enabled = false;
                cardName.Enabled = false;
                issueNum.Enabled = false;
                email.Enabled = false;
                
                reAddButton.Enabled = false;
                cardGroupBox.Enabled = true;
                commonCardNum.Enabled = false;
                issueNum.Enabled = false;
                pinNum.Enabled = false;
                cardType.Enabled = true;
                cardStatus.Enabled = true;
                cardFormat.Enabled = true;
                issueType.Enabled = false;
                issueReason.Enabled = false;
                startDate.Enabled = false;
                // endDate.Enabled = false;
                deleteCardButton.Enabled = false;
                personGroupBox.Enabled = true;
                name.Enabled = false;
                ssno.Enabled = false;
                department.Enabled = true;
                division.Enabled = true;
                title.Enabled = true;
                tel.Enabled = false;
                address.Enabled = false;
                sazin.Enabled = false;
                sazinButton.Enabled = false;
              
                commonGroupBox2.Enabled = false;
                commonGroupBox3.Enabled = false;
                scanFP.Enabled = false;
                alwaysCheckBox.Enabled = true;
                finger1.Enabled = false;
                finger2.Enabled = false;
                fpIP.Enabled = false;
                ppDeleteButton.Enabled = false;
                save.Enabled = true;
                regStartButton.Enabled = false;
                cardRestoreButton.Enabled = false;
                plantRegButton.Enabled = false;
                reRegButton.Enabled = false;

                massChangeStart.Enabled = false;

                acs1EndDateTime.Enabled = true;
                acs2EndDateTime.Enabled = true;
                acs3EndDateTime.Enabled = true;
                acs4EndDateTime.Enabled = true;
                acs1EndDateButton.Enabled = true;
                acs2EndDateButton.Enabled = true;
                acs3EndDateButton.Enabled = true;
                acs4EndDateButton.Enabled = true;

                ///////////////////////////////////////////////////////////////////////////////////////////////////////
                name.Text = name2.Text = name3.Text = "";
                commonCardNum.Text = commonCardNum2.Text = commonCardNum3.Text = "";


                birthday.Value = new DateTime(1980, 1, 1);
                birthday2.Text = birthday3.Text = "1980년1월1일";

                gender.Text = gender2.Text = gender3.Text = "";
                gender.Text = "";
                cardNum.Text = cardName.Text = issueNum.Text = "";
                pinNum.Text = email.Text = ssno.Text = "";


                cardType.Text = cardStatus.Text = cardFormat.Text = "현재데이터유지";
                issueType.Text = issueReason.Text = "현재데이터유지";
                startDate.Value = DateTime.Now;   // new DateTime(1970, 1, 1);  // DateTime.Now;
                //endDate.Value = DateTime.Now.AddYears(-10);
                name.Text = ssno.Text = tel.Text = address.Text = "";
                department.Text = division.Text = title.Text = "현재데이터유지";


                showDefaultImage();

                alwaysCheckBox.CheckState = CheckState.Unchecked;
            }

            if (!_mode.Equals("VIEW")) pinButton.Visible = false;
            else pinButton.Visible = true;
        }

        public void setCodeDefault()
        {
            department.SelectedIndex = 0;
            division.SelectedIndex = 0;
            title.SelectedIndex = 0;
            cardStatus.SelectedIndex = 0;
            cardType.SelectedIndex = 3;
            cardFormat.SelectedIndex = 1;
            issueType.SelectedIndex = 0;
            issueReason.SelectedIndex = 0;

            department.Text = "" + department.Items[0];
            division.Text = "" + division.Items[0];
            title.Text = "" + title.Items[0];
            cardStatus.Text = "" + cardStatus.Items[0];
            //cardType.Text = "" + cardType.Items[1];
            cardFormat.Text = "" + cardFormat.Items[1];
            issueType.Text = "" + issueType.Items[0];
            issueReason.Text = "" + issueReason.Items[0];
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
                    if (dataGridView1.Rows[i].Cells[0].Value != null && !dataGridView1.Rows[i].Cells[0].Value.Equals(""))
                    {
                        dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.Yellow;
                        dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.Yellow;
                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Yellow;
                        dataGridView1.Rows[i].Cells[2].Value = "V";
                    }
                }
            }
            else if (cardAllCheck.Text.Equals("V"))
            {
                cardAllCheck.Text = "";
                cardAllCheck.BackColor = Color.FromArgb(221, 221, 255);

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value != null && !dataGridView1.Rows[i].Cells[0].Value.Equals(""))
                    {
                        dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(221, 221, 255);
                        dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(221, 221, 255);
                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(221, 221, 255);
                        dataGridView1.Rows[i].Cells[2].Value = "";
                    }
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




        private void clickCardType(object sender, EventArgs e)
        {

            if (cardType.Text.Equals("방문")) name.Text = cardName.Text;

            if (curMode.Equals("MASS_CHANGE") && !cardType.Text.Equals("현재데이터유지")) cardType.BackColor = Color.Yellow;

        }

        private void clickCardType2(object sender, MouseEventArgs e)
        {
            cardType.BackColor = Color.Yellow;
            // MessageBox.Show("clickCardType2 ()");
        }


        private void clickCardStatus(object sender, EventArgs e)
        {
            if (curMode.Equals("MASS_CHANGE") && !cardStatus.Text.Equals("현재데이터유지")) cardStatus.BackColor = Color.Yellow;
            // if (curMode.Equals("VIEW") && firstView == false) cardStatus.BackColor = Color.Yellow; 
        }
        private void clickCardFormat(object sender, EventArgs e)
        {
            if (curMode.Equals("MASS_CHANGE") && !cardFormat.Text.Equals("현재데이터유지")) cardFormat.BackColor = Color.Yellow;
            // if (curMode.Equals("VIEW") && firstView == false) cardFormat.BackColor = Color.Yellow; 
        }
        private void clickIssueType(object sender, EventArgs e)
        {
            if (curMode.Equals("MASS_CHANGE") && !issueType.Text.Equals("현재데이터유지")) issueType.BackColor = Color.Yellow;
            // if (curMode.Equals("VIEW") && firstView == false) issueType.BackColor = Color.Yellow; 
        }
        private void clickIssueReason(object sender, EventArgs e)
        {
            if (curMode.Equals("MASS_CHANGE") && !issueReason.Text.Equals("현재데이터유지")) issueReason.BackColor = Color.Yellow;
            // if (curMode.Equals("VIEW") && firstView == false) issueReason.BackColor = Color.Yellow;
        }
        private void clickDepartment(object sender, EventArgs e)
        {
            if (curMode.Equals("MASS_CHANGE") && !department.Text.Equals("현재데이터유지")) department.BackColor = Color.Yellow;
            // if (curMode.Equals("VIEW") && firstView == false) department.BackColor = Color.Yellow;
        }
        private void clickDivision(object sender, EventArgs e)
        {
            if (curMode.Equals("MASS_CHANGE") && !division.Text.Equals("현재데이터유지")) division.BackColor = Color.Yellow;
            // if (curMode.Equals("VIEW") && firstView == false) division.BackColor = Color.Yellow;
        }
        private void clickTitle(object sender, EventArgs e)
        {
            if (curMode.Equals("MASS_CHANGE") && !title.Text.Equals("현재데이터유지")) title.BackColor = Color.Yellow;
            // if (curMode.Equals("VIEW") && firstView == false) title.BackColor = Color.Yellow; 
        }




        public void initDetailDataColor()
        {
            name.BackColor = commonCardNum.BackColor = birthday.BackColor = gender.BackColor = Color.White;
            ssno.BackColor = department.BackColor = division.BackColor = title.BackColor = email.BackColor = address.BackColor = tel.BackColor = Color.White;
            cardNum.BackColor = cardName.BackColor = issueNum.BackColor = pinNum.BackColor = cardType.BackColor = Color.White;
            cardStatus.BackColor = cardFormat.BackColor = issueType.BackColor = issueReason.BackColor = Color.White;


        }

        public void makePage()
        {

            firstIndex = listPerPage;
            preEA = 0;

            pageListNum = new ArrayList();

            int last = 0;
            last = totalListNum % listPerPage;

            int ea = totalListNum / listPerPage;
            for (int i = 0; i < ea; i++)
            {
                pageListNum.Add(listPerPage);
            }

            if (last > 0) pageListNum.Add(last);

            totalPageLabel.Text = "/   " + pageListNum.Count;


            if (pageListNum.Count > 1)
            {
                curPageLabel.Visible = true;
                totalPageLabel.Visible = true;
                post.Visible = true;
            }
            else
            {
                curPageLabel.Visible = false;
                totalPageLabel.Visible = false;
                post.Visible = false;
            }


        }

        public void clearAllCardData()
        {

            Log.WriteLog("[Reg.cs] clearAllCardData ()");

            name.Text = email.Text = cardNum.Text = commonCardNum.Text = issueNum.Text = pinNum.Text = "";
            birthday.Value = new DateTime(1980, 1, 1);

            name.Text = ssno.Text = tel.Text = address.Text = "";

            cardType.Text = cardStatus.Text = cardFormat.Text = issueType.Text = issueReason.Text = "선택";

            startDate.Value = DateTime.Now;
            endDate.Value = DateTime.Now.AddYears(30);
        }
        public void clearAllCardDetail()
        {
            Log.WriteLog("[Reg.cs] clearAllCardDataDetail ()");

            name.Text = "";
            commonCardNum.Text = commonCardNum2.Text = commonCardNum3.Text = "";
            gender.Text = "";
            birthday.Value = new DateTime(1980, 1, 1);

            ssno.Text = email.Text = tel.Text = address.Text = "";
            department.Text = division.Text = title.Text = "선택";

            cardNum.Text = cardName.Text = issueNum.Text = pinNum.Text = "";
            cardType.Text = cardStatus.Text = cardFormat.Text = issueType.Text = issueReason.Text = "선택";
            startDate.Value = DateTime.Now;
            endDate.Value = DateTime.Now.AddYears(30);

        }

        public void clearOnlyCardDetail()
        {
            Log.WriteLog("[Reg.cs] clearOnlyCardDataDetail ()");

            cardNum.Text = cardName.Text = issueNum.Text = pinNum.Text = cardType.Text = cardStatus.Text = cardFormat.Text = "";
            issueType.Text = issueReason.Text = "";
            startDate.Value = DateTime.Now;
            endDate.Value = DateTime.Now;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void showSazin(string _empID)
        {
            string query = "select IMAGE from EMP_IMAGE where EMPID = " + _empID;

            Log.WriteLogTmp("[BM_Reg.cs] showSazin () query : " + query);


            try
            {
                string resultString = req.select("BMS011", query, "BMS");

                if (resultString != "0")
                {
                    // MessageBox.Show("장애 발생");
                    Log.WriteLog("[Reg.cs] showSazin () 장해");
                    showDefaultImage();

                    return;
                }

                // MessageBox.Show("SUCCESS !");

                // readDataTable(ReturnDT.dt);

                Log.WriteLogTmp(".............. JJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJ");

                byte[] imageColumn = (byte[])ReturnDT.dt.Rows[0]["IMAGE"];
                sazin.Image = util.ResizeBitmap(ByteToImage(imageColumn), sazin.Width, sazin.Height);


                du.setSazin(imageColumn);

                Log.WriteLog("[Reg.cs] showSazin () 성공");

            }
            catch (Exception e2)
            {

                // MessageBox.Show(e2.ToString());

                Log.WriteLog("[Reg.cs] showSazin () 장해");
                showDefaultImage();

            }

        }

        public Bitmap ByteToImage(byte[] blob)
        {
            Bitmap bm = null;

            try
            {
                MemoryStream mStream = new MemoryStream();
                byte[] pData = blob;
                mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
                bm = new Bitmap(mStream, false);
                //mStream.Dispose();
            }
            catch (NullReferenceException e2)
            {
                // bm = new Bitmap(".\\img\\nobody2.png");

            }
            catch (Exception e)
            {
                // bm = new Bitmap(".\\img\\nobody2.png");

            }

            return bm;
        }

        private void clickTest(object sender, EventArgs e)
        {

            Bitmap img = null;

            try
            {
                // img = new Bitmap(".\\img\\Marlon Brando.jpg");
                img = new Bitmap(".\\img\\kbs.jpg");

            }
            catch (ArgumentException ae2)
            {
                img = new Bitmap(".\\img\\nobody2.png");

            }
            this.sazin.BackColor = Color.Transparent;
            this.sazin.Image = util.ResizeBitmap(img, sazin.Width, sazin.Height);


            // string query = "INSERT INTO EMP_IMAGE (EMPID, IMAGE) values (25532, @Image)";
            string query = "UPDATE EMP_IMAGE SET IMAGE = @Image WHERE EMPID = 25601";

            // string query = "INSERT INTO EMP_IMAGE (EMPID) values (25532)";

            try
            {

                MemoryStream ms = new MemoryStream();
                sazin.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] tms = new byte[ms.Length];

                ms.Position = 0;
                ms.Read(tms, 0, Convert.ToInt32(ms.Length));

                byte[] ParamImage = tms;
                ms.Dispose();

                string rNumString = req.updateImage("BMS011", query, "BMIM", ParamImage);
                // string rNumString = req.update ("BMS011", query, "BMI");


                if (rNumString != "0")
                {
                    MessageBox.Show("장애 발생");
                    return;
                }

                MessageBox.Show("SUCCESS !");

            }
            catch (Exception e2)
            {

                MessageBox.Show(e2.ToString());

            }

        }

        private void clickSearch(object sender, EventArgs e)
        {

            find();
        }

        public string makeClause()
        {
            string result = "";

            Log.WriteLogTmp("============================ depart : " + departCombo.Text);

            if (!nameTextBox.Text.Equals("") && !nameTextBox.Text.Equals("전체"))
            {

                result = result + " and e.NAME_1 like '%" + nameTextBox.Text.Trim() + "%'";
                return result;
            }

            if (!cardNumTextBox.Text.Equals("") && !cardNumTextBox.Text.Equals("전체"))
            {
                result = result + " and b.BID = '" + cardNumTextBox.Text.Trim() + "'";
                return result;
            }


            if (!departCombo.Text.Equals("") && !departCombo.Text.Equals("전체"))
            {
                result = result + " and d.DESCRIPTION = '" + departCombo.Text.Trim() + "' ";

            }

            if (!divisionCombo.Text.Equals("") && !divisionCombo.Text.Equals("전체"))
            {
                result = result + " and d2.DESCRIPTION = '" + divisionCombo.Text.Trim() + "' ";

            }


            if (!typeCombo.Text.Equals("") && !typeCombo.Text.Equals("전체"))
            {
                result = result + " and bt.DESCRIPTION = '" + typeCombo.Text.Trim() + "' ";

            }

            if (endDateRadio.Checked)
                result = result + " and b.DEACTIVATE_DATE = '" + searchEndDate.Value.ToShortDateString() + "' ";


            return result;

        }

        public void showDefaultImage()
        {
            Bitmap img = new Bitmap(".\\img\\nobody2.png");
            sazin.Image = util.ResizeBitmap(img, sazin.Width, sazin.Height);

        }

        private void cardNumRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }



        public void drawBack(Graphics g)
        {

            Bitmap back = new Bitmap(".\\img\\logo\\back.png");
            // Bitmap img2 = ResizeBitmap(ChangeOpacity(back, .7F), 684, 475);
            Bitmap img2 = util.ResizeBitmap(util.ChangeOpacity(back, 1.0F), this.Width, this.Height - 50);

            TextureBrush brush = new TextureBrush(img2, WrapMode.Tile);
            g.FillRectangle(brush, 0, 0, this.Width, this.Height);

        }

        private void badge_3_Click(object sender, EventArgs e)
        {

        }

        private void clickBadge0(object sender, EventArgs e) { showOnlyCard(0); }
        private void clickBadge1(object sender, EventArgs e) { showOnlyCard(1); }
        private void clickBadge2(object sender, EventArgs e) { showOnlyCard(2); }
        private void clickBadge3(object sender, EventArgs e) { showOnlyCard(3); }
        private void clickBadge4(object sender, EventArgs e) { showOnlyCard(4); }

        public void setCardNum(string _cardNum)
        {
            commonCardNum3.Text = cardNumTextBox.Text = _cardNum;
            



        }



        public void showOnlyCard(int num)
        {
            Log.WriteLog("[Reg.cs] showOnlyCard (" + num + ")");
        }



        public Boolean showCardCompare()
        {
            string titleString = "[조회/수정모드] ==== (" + du.getBid() + ") ====\n\n";

            int orgCode = -1;
            int newCode = -1;
            string newDesc = "";

            string protocolString = "100=" + du.getBid() + ",1=" + du.getEmpID();

            // Log.WriteLogTmp("protocolString : " + protocolString);
            Boolean flag = false;

            string msg = "";

            string go = "  ▶  ";

            int num = 1;

            if (!du.getName().Equals(this.name.Text.Trim()))
            {

                msg = msg + num + ". 성    명 : " + du.getName() + go + this.name.Text + "\n"; num++;
                protocolString = protocolString + ",2=" + this.name.Text.Trim();
            }


            if (!getDate(du.getBirth()).Equals(getDate(this.birthday)))
            {
                if (du.getBirth() != new DateTime(1000, 1, 1))
                {

                    msg = msg + num + ". 생년월일 : " + getDate(du.getBirth()) + go + getDate(this.birthday) + "\n"; num++;
                    protocolString = protocolString + ",3=" + getDate(this.birthday);

                    /*
                    DateTimePicker tmp1 = this.birthday;
                    string dayString = "" + tmp1.Value.Year + "-" + tmp1.Value.Month + "-" + tmp1.Value.Day;

                    // string query = "UPDATE EMP SET BIRTH_DATE = CAST('2009-05-25' AS DATETIME) where ID = " + du.getEmpID ();
                    string query = "UPDATE EMP SET BIRTH_DATE = CAST('" + dayString + "' AS DATETIME) where ID = " + du.getEmpID();
                    // string query = "UPDATE EMP SET BIRTH_DATE = " + du.getBirth ();


                    Log.WriteLogTmp("[BM_Reg_001.cs] query : >>>>>>>>>>>>>>>>>>>>>>>>>>> " + query);


                    string rNumString = req.update("BMS011", query, "BMI");

                    if (rNumString != "0") MessageBox.Show("실패");
                    else MessageBox.Show("성공");

                    msg = msg + num + ". 생년월일 : " + getDate(du.getBirth()) + go + getDate(this.birthday) + "\n"; num++;
                    MessageBox.Show(titleString + msg);
                    
                    return true;
                    */

                }



            }



            if (!du.getGender().Equals(this.gender.Text.Trim()))
            {
                msg = msg + num + ". 성    별 : " + du.getGender() + go + this.gender.Text + "(" + (gender.SelectedIndex + 1) + ")" + "\n"; num++;
                protocolString = protocolString + ",4=" + (gender.SelectedIndex + 1);
            }
            if (!du.getSsno().Equals(this.ssno.Text.Trim()))
            {
                msg = msg + num + ". 사원번호 : " + du.getSsno() + go + this.ssno.Text + "\n"; num++;
                protocolString = protocolString + ",5=" + this.ssno.Text.Trim();
            }


            // if (!du.getDepartment().Equals(this.department.Text.Trim())) { msg = msg + num + ". 소    속 : " + du.getDepartment() + go + this.department.Text + "\n"; num++; }
            // Log.WriteLogTmp(".................... dept.SelectedIndex : " + CodeTable.department.Rows [department.SelectedIndex][1]);
            orgCode = du.getDepartment();
            newCode = Convert.ToInt32(Bm_Main.department.Rows[department.SelectedIndex][0]);
            newDesc = Bm_Main.department.Rows[department.SelectedIndex][1].ToString();
            if (orgCode != newCode)
            {
                msg = msg + num + ". 소    속 : " + orgCode + go + newCode + "  (" + newDesc + ")\n"; num++;
                protocolString = protocolString + ",6=" + newCode;
            }

            // if (!du.getDivision().Equals(this.division.Text.Trim())) { msg = msg + num + ". 부    서 : " + du.getDivision() + go + this.division.Text + "\n"; num++; }
            orgCode = du.getDivision();
            newCode = Convert.ToInt32(Bm_Main.division.Rows[division.SelectedIndex][0]);
            newDesc = Bm_Main.division.Rows[division.SelectedIndex][1].ToString();
            if (orgCode != newCode)
            {
                msg = msg + num + ". 부    서 : " + orgCode + go + newCode + "  (" + newDesc + ")\n"; num++;
                protocolString = protocolString + ",7=" + newCode;
            }

            // if (!du.getTitle().Equals(this.title.Text.Trim())) { msg = msg + num + ". 직    위 : " + du.getTitle() + go + this.title.Text + "\n"; num++; }
            orgCode = du.getTitle();
            newCode = Convert.ToInt32(Bm_Main.title.Rows[title.SelectedIndex][0]);
            newDesc = Bm_Main.title.Rows[title.SelectedIndex][1].ToString();
            if (orgCode != newCode)
            {
                msg = msg + num + ". 직    위 : " + orgCode + go + newCode + "  (" + newDesc + ")\n"; num++;
                protocolString = protocolString + ",8=" + newCode;
            }

            if (!du.getEmail().Equals(this.email.Text.Trim()))
            {
                msg = msg + num + ". 이 메 일 : " + du.getEmail() + go + this.email.Text + "\n"; num++;
                protocolString = protocolString + ",9=" + this.email.Text.Trim();
            }

            if (!du.getTel().Equals(this.tel.Text.Trim()))
            {
                msg = msg + num + ". 연 락 처 : " + du.getTel() + go + this.tel.Text + "\n"; num++;
                protocolString = protocolString + ",10=" + this.tel.Text.Trim();
            }


            if (!du.getAddress().Equals(this.address.Text.Trim()))
            {
                msg = msg + num + ". 주    소 : " + du.getAddress() + go + this.address.Text + "\n"; num++;
                protocolString = protocolString + ",11=" + this.address.Text.Trim();
            }

            if (sazinChange)
            {
                byte[] tmpSazin = null;

                tmpSazin = getCurSazin();



                if (tmpSazin != null && !du.getEmpID().Equals(""))
                {

                    msg = msg + num + ". 사진 변경 / 추가 : " + sazinChange + "(" + tmpSazin.Length + ")\n";
                    // protocolString = protocolString + ",12=" + tmpSazin;

                    Gloval.newSazin = tmpSazin;

                    /*
                    string query = "UPDATE EMP_IMAGE SET IMAGE = @Image WHERE EMPID = " + du.getEmpID();

                    Log.WriteLogTmp("[BM_Reg_001.cs] query : " + query);


                    string rNumString = req.updateImage("BMS011", query, "BMIM", tmpSazin);

                    if (!rNumString.Equals ("0"))
                    {
                        MessageBox.Show("장애 발생");
                        return false;
                    }
                    return true;
                    */


                }


                // sazinChange = false;

            }



            if (!du.getBid().Equals(this.cardNum.Text.Trim()))
            {
                msg = msg + num + ". 카드번호 : " + du.getBid() + go + this.cardNum.Text + "\n"; num++;
                protocolString = protocolString + ",0=" + this.cardNum.Text.Trim();
            }

            string tmpCheck = "0";
            if (alwaysCheckBox.CheckState == CheckState.Checked) tmpCheck = "1";
            if (du.getByPass().Equals("") || du.getByPass() == null) du.setBypass("0");
            if (!du.getByPass().Equals(tmpCheck))
            {
                msg = msg + num + ". 항시통과 : " + du.getByPass() + go + tmpCheck + "\n"; num++;
                protocolString = protocolString + ",23=" + tmpCheck;
            }

            tmpCheck = "0";
            if (mdmCheck.CheckState == CheckState.Checked) tmpCheck = "1";
            if (du.getMdmStatus().Equals("") || du.getMdmStatus() == null) du.setMdmStatus("0");
            if (!du.getMdmStatus().Equals(tmpCheck))
            {
                msg = msg + num + ". MDM     : " + du.getMdmStatus() + go + tmpCheck + "\n"; num++;
                protocolString = protocolString + ",30=" + tmpCheck;
            }

            tmpCheck = "1";
            if (ptCheckBox.CheckState == CheckState.Checked) tmpCheck = "2";

            if (du.getPT() == -1) du.setPT(1);

            Log.WriteLogTmp("......................... PT tmpCheck : " + tmpCheck + ptCheckBox.CheckState);
            Log.WriteLogTmp("......................... PT du.getPT () : " + du.getPT());


            if (!("" + du.getPT()).Equals(tmpCheck))
            {
                msg = msg + num + ". PT : " + tmpCheck + "\n"; num++;
                protocolString = protocolString + ",31=" + tmpCheck;
            }





            if (!du.getCardName().Equals(this.cardName.Text.Trim()))
            {
                msg = msg + num + ". 카 드 명 : " + du.getCardName() + go + this.cardName.Text + "\n"; num++;
                protocolString = protocolString + ",13=" + this.cardName.Text.Trim();
            }


            if (!du.getPin().Equals(this.pinNum.Text.Trim()))
            {
                msg = msg + num + ". PIN      : " + du.getPin() + go + this.pinNum.Text + "\n"; num++;
                protocolString = protocolString + ",14=" + this.pinNum.Text.Trim();
            }

            // if (!du.getBadgeType().Equals(this.cardType.Text.Trim())) { msg = msg + num + ". 카드타입 : " + du.getBadgeType() + go + this.cardType.Text + "\n"; num++; }
            orgCode = du.getBadgeType();
            newCode = Convert.ToInt32(Bm_Main.cardType.Rows[cardType.SelectedIndex][0]);
            newDesc = Bm_Main.cardType.Rows[cardType.SelectedIndex][1].ToString();
            if (orgCode != newCode)
            {
                msg = msg + num + ". 카드타입 : " + orgCode + go + newCode + "  (" + newDesc + ")\n"; num++;
                protocolString = protocolString + ",16=" + newCode;
            }



            // if (!du.getBadgeStatus().Equals(this.cardStatus.Text.Trim())) { msg = msg + num + ". 카드상태 : " + du.getBadgeStatus() + go + this.cardStatus.Text + "\n"; num++; }
            orgCode = du.getBadgeStatus();
            newCode = Convert.ToInt32(Bm_Main.cardStat.Rows[cardStatus.SelectedIndex][0]);
            newDesc = Bm_Main.cardStat.Rows[cardStatus.SelectedIndex][1].ToString();
            if (orgCode != newCode)
            {
                msg = msg + num + ". 카드상태 : " + orgCode + go + newCode + "  (" + newDesc + ")\n"; num++;
                protocolString = protocolString + ",17=" + newCode;
            }



            // if (!du.getBadgeFormat().Equals(this.cardFormat.Text.Trim())) { msg = msg + num + ". 카드포맷 : " + du.getBadgeFormat() + go + this.cardFormat.Text + "\n"; num++; }
            orgCode = du.getBadgeFormat();
            newCode = Convert.ToInt32(Bm_Main.cardFormat.Rows[cardFormat.SelectedIndex][0]);
            newDesc = Bm_Main.cardFormat.Rows[cardFormat.SelectedIndex][1].ToString();
            if (orgCode != newCode)
            {
                msg = msg + num + ". 카드포맷 : " + orgCode + go + newCode + "  (" + newDesc + ")\n"; num++;
                protocolString = protocolString + ",18=" + newCode;

            }




            // if (!du.getIssueType().Equals(this.issueType.Text.Trim())) { msg = msg + num + ". 발급유형 : " + du.getIssueType() + go + this.issueType.Text + "\n"; num++; }
            orgCode = du.getIssueType();
            newCode = Convert.ToInt32(Bm_Main.issueType.Rows[issueType.SelectedIndex][0]);
            newDesc = Bm_Main.issueType.Rows[issueType.SelectedIndex][1].ToString();
            if (orgCode != newCode)
            {
                msg = msg + num + ". 발급유형 : " + orgCode + go + newCode + "  (" + newDesc + ")\n"; num++;
                protocolString = protocolString + ",19=" + newCode;
            }




            // if (!du.getIssueReason().Equals(this.issueReason.Text.Trim())) { msg = msg + num + ". 발급사유 : " + du.getIssueReason() + go + this.issueReason.Text + "\n"; num++; }
            orgCode = du.getIssueReason();
            newCode = Convert.ToInt32(Bm_Main.issueReason.Rows[issueReason.SelectedIndex][0]);
            newDesc = Bm_Main.issueReason.Rows[issueReason.SelectedIndex][1].ToString();
            if (orgCode != newCode)
            {
                msg = msg + num + ". 발급사유 : " + orgCode + go + newCode + "  (" + newDesc + ")\n"; num++;
                protocolString = protocolString + ",20=" + newCode;
            }

            // if (!du.getActive().ToString().StartsWith(this.startDate.Text)) { msg = msg + num + ". 시 작 일 : " + du.getActive().ToString() + go + this.startDate.Text + "\n"; num++; }
            // if (!du.getDeactive().ToString().StartsWith(this.endDate.Text)) { msg = msg + num + ". 만 료 일 : " + du.getDeactive().ToString() + go + this.endDate.Text + "\n"; num++; }

            if (!getDate(du.getActive()).Equals(getDate(this.startDate)))
            {
                msg = msg + num + ". 시 작 일 : " + getDate(du.getActive()) + go + getDate(this.startDate) + "\n"; num++;
                protocolString = protocolString + ",21=" + getDate(this.startDate);
            }


            // 만료일 체크박스 할경우에만 일괄적용
            if (endDateCheckBox.Checked)
            {
                msg = msg + num + ". 만 료 일 : " + getDate(du.getDeactive()) + go + getDate(this.endDate) + "\n"; num++;
                protocolString = protocolString + ",22=" + getDate(this.endDate);
            }




            tmpCheck = "-1";
            if (vmCheck.CheckState == CheckState.Checked) tmpCheck = "1";
            if (du.getVmStatus().Equals("") || du.getVmStatus() == null) du.setVmStatus("-1");
            if (!du.getVmStatus().Equals(tmpCheck))
            {
                msg = msg + num + ". VM등록 : " + du.getVmStatus() + go + tmpCheck + "\n"; num++;
                protocolString = protocolString + ",25=" + tmpCheck;
            }




            tmpCheck = "-1";
            if (fpCheck.CheckState == CheckState.Checked) tmpCheck = "0";
            else tmpCheck = "-1";

            // if (du.getFpNum().Equals("") || du.getFpNum() == null) du.setFpNum("-1");
            int orgNum = -1;
            orgNum = Convert.ToInt32(du.getFpNum());
            /*
            if (tmpCheck.Equals("-1"))
            {
                if (orgNum > -1) { msg = msg + num + ". FP등록 : " + orgNum + go + tmpCheck + "\n"; num++; }
            }
            else
            {
                if (orgNum < 0) { msg = msg + num + ". FP등록 : " + orgNum + go + tmpCheck + "\n"; num++; }
            }
            */
            if (Convert.ToInt32(tmpCheck) == -1)
            {
                if (orgNum != -1)
                {
                    Log.WriteLogTmp("======================== orgNum : " + orgNum + ",  check : " + tmpCheck);
                    msg = msg + num + ". FP등록 : " + orgNum + go + tmpCheck + "\n"; num++;
                    protocolString = protocolString + ",24=" + tmpCheck;
                }
            }
            else
            {
                if (orgNum < 0)
                {
                    Log.WriteLogTmp("======================== orgNum : " + orgNum + ",  check : " + tmpCheck);
                    msg = msg + num + ". FP등록 : " + orgNum + go + tmpCheck + "\n"; num++;
                    protocolString = protocolString + ",24=" + tmpCheck;
                }
            }
            /*
            tmpCheck = "-1";
            if (vmCheck.Checked)
            {
                tmpCheck = "1";
            }
            protocolString = protocolString + ",25=" + tmpCheck;
            */

            tmpCheck = "0";
            if (plant1RegCheckBox.Checked)
            {
                tmpCheck = "1";
            }
            protocolString = protocolString + ",26=" + tmpCheck;


            tmpCheck = "0";
            if (plant2RegCheckBox.Checked)
            {
                tmpCheck = "1";
            }
            protocolString = protocolString + ",27=" + tmpCheck;


            tmpCheck = "0";
            if (plant3RegCheckBox.Checked)
            {
                tmpCheck = "1";
            }
            protocolString = protocolString + ",28=" + tmpCheck;


            tmpCheck = "0";
            if (plant4RegCheckBox.Checked)
            {
                tmpCheck = "1";
            }
            protocolString = protocolString + ",29=" + tmpCheck;





            /////////////////////////////////////////////// 권    한 ///////////////////////////////////////////////////////

            changeRightString = "";

            for (int i = 0; i < orgPlantRight.Length; i++)
            {
                string plantNum = "";

                if (!orgPlantRight[i].Equals(newPlantRight[i]))
                {
                    if (newPlantRight[i].Equals("")) newPlantRight[i] = "-100";   // 있던 권한이 모두 삭제되는 경우 -100 입력

                    plantNum = "(" + (i + 1) + " 발전소)  ";
                    changeRightString = changeRightString + plantNum + orgPlantRight[i] + go + newPlantRight[i] + "\n";

                    string tmp = newPlantRight[i].Replace(",", "~");
                    protocolString = protocolString + "," + ((i + 1) * 1000) + "=" + tmp;
                }
            }
            if (!changeRightString.Equals("")) msg = msg + num + ". 권    한 : \n" + changeRightString + "\n"; num++;

            Log.WriteLogTmp("orgPlantRight[0] = > " + orgPlantRight[0]);
            Log.WriteLogTmp("orgPlantRight[1] = > " + orgPlantRight[1]);
            Log.WriteLogTmp("orgPlantRight[2] = > " + orgPlantRight[2]);
            Log.WriteLogTmp("orgPlantRight[3] = > " + orgPlantRight[3]);



            Log.WriteLogTmp("JJJJJJJJJJJJJJJJJJJJJJJJJ msg : " + msg);

            if (!msg.Equals(""))
            {
                // MessageBox.Show(titleString + msg);
                // MessageBox.Show(protocolString);

                Gloval.protocolString = protocolString;

                flag = true;

                new Parser().parseCard(protocolString, "VIEW");
            }
            else
            {
                flag = false;
            }
            return flag;


        }


        public Boolean insertReg()
        {
            string titleString = "[조회/수정모드] ==== (" + du.getBid() + ") ====\n\n";

            int orgCode = -1;
            int newCode = -1;
            string newDesc = "";

            string protocolString = "100=" + du.getBid() + ",1=" + du.getEmpID();

            //changeNewRight();
            // Log.WriteLogTmp("protocolString : " + protocolString);
            Boolean flag = false;

            string msg = "";

            string go = "  ▶  ";

            int num = 1;



            msg = msg + num + ". 성    명 : " + this.name.Text.Trim() + go + this.name.Text + "\n"; num++;
            protocolString = protocolString + ",2=" + this.name.Text.Trim();





            msg = msg + num + ". 생년월일 : " + getDate(this.birthday) + "\n"; num++;
            protocolString = protocolString + ",3=" + getDate(this.birthday);











            msg = msg + num + ". 성    별 : " + this.gender.Text + "(" + (gender.SelectedIndex + 1) + ")" + "\n"; num++;
            protocolString = protocolString + ",4=" + (gender.SelectedIndex + 1);


            msg = msg + num + ". 사원번호 : " + this.ssno.Text + "\n"; num++;
            protocolString = protocolString + ",5=" + this.ssno.Text.Trim();



            // if (!du.getDepartment().Equals(this.department.Text.Trim())) { msg = msg + num + ". 소    속 : " + du.getDepartment() + go + this.department.Text + "\n"; num++; }
            // Log.WriteLogTmp(".................... dept.SelectedIndex : " + CodeTable.department.Rows [department.SelectedIndex][1]);

            newCode = Convert.ToInt32(Bm_Main.department.Rows[department.SelectedIndex][0]);
            newDesc = Bm_Main.department.Rows[department.SelectedIndex][1].ToString();

            msg = msg + num + ". 소    속 : " + newCode + "  (" + newDesc + ")\n"; num++;
            protocolString = protocolString + ",6=" + newCode;


            // if (!du.getDivision().Equals(this.division.Text.Trim())) { msg = msg + num + ". 부    서 : " + du.getDivision() + go + this.division.Text + "\n"; num++; }

            newCode = Convert.ToInt32(Bm_Main.division.Rows[division.SelectedIndex][0]);
            newDesc = Bm_Main.division.Rows[division.SelectedIndex][1].ToString();

            msg = msg + num + ". 부    서 : " + newCode + "  (" + newDesc + ")\n"; num++;
            protocolString = protocolString + ",7=" + newCode;


            // if (!du.getTitle().Equals(this.title.Text.Trim())) { msg = msg + num + ". 직    위 : " + du.getTitle() + go + this.title.Text + "\n"; num++; }

            newCode = Convert.ToInt32(Bm_Main.title.Rows[title.SelectedIndex][0]);
            newDesc = Bm_Main.title.Rows[title.SelectedIndex][1].ToString();

            msg = msg + num + ". 직    위 : " + newCode + "  (" + newDesc + ")\n"; num++;
            protocolString = protocolString + ",8=" + newCode;



            msg = msg + num + ". 이 메 일 : " + this.email.Text + "\n"; num++;
            protocolString = protocolString + ",9=" + this.email.Text.Trim();



            msg = msg + num + ". 연 락 처 : " + this.tel.Text + "\n"; num++;
            protocolString = protocolString + ",10=" + this.tel.Text.Trim();




            msg = msg + num + ". 주    소 : " + this.address.Text + "\n"; num++;
            protocolString = protocolString + ",11=" + this.address.Text.Trim();



            byte[] tmpSazin = null;

            tmpSazin = getCurSazin();



            if (tmpSazin != null && !du.getEmpID().Equals(""))
            {

                msg = msg + num + ". 사진 변경 / 추가 : " + sazinChange + "(" + tmpSazin.Length + ")\n";
                // protocolString = protocolString + ",12=" + tmpSazin;

                Gloval.newSazin = tmpSazin;

                /*
                string query = "UPDATE EMP_IMAGE SET IMAGE = @Image WHERE EMPID = " + du.getEmpID();

                Log.WriteLogTmp("[BM_Reg_001.cs] query : " + query);


                string rNumString = req.updateImage("BMS011", query, "BMIM", tmpSazin);

                if (!rNumString.Equals ("0"))
                {
                    MessageBox.Show("장애 발생");
                    return false;
                }
                return true;
                */


            }


            // sazinChange = false;






            msg = msg + num + ". 카드번호 : " + this.cardNum.Text + "\n"; num++;
            protocolString = protocolString + ",0=" + this.cardNum.Text.Trim();







            msg = msg + num + ". 카 드 명 : " + this.cardName.Text + "\n"; num++;
            protocolString = protocolString + ",13=" + this.cardName.Text.Trim();



            msg = msg + num + ". PIN      : " + 0 + "\n"; num++;
            protocolString = protocolString + ",14=" + 0;


            // if (!du.getBadgeType().Equals(this.cardType.Text.Trim())) { msg = msg + num + ". 카드타입 : " + du.getBadgeType() + go + this.cardType.Text + "\n"; num++; }

            newCode = Convert.ToInt32(Bm_Main.cardType.Rows[cardType.SelectedIndex][0]);
            newDesc = Bm_Main.cardType.Rows[cardType.SelectedIndex][1].ToString();

            msg = msg + num + ". 카드타입 : " + newCode + "  (" + newDesc + ")\n"; num++;
            protocolString = protocolString + ",16=" + newCode;




            // if (!du.getBadgeStatus().Equals(this.cardStatus.Text.Trim())) { msg = msg + num + ". 카드상태 : " + du.getBadgeStatus() + go + this.cardStatus.Text + "\n"; num++; }

            newCode = Convert.ToInt32(Bm_Main.cardStat.Rows[cardStatus.SelectedIndex][0]);
            newDesc = Bm_Main.cardStat.Rows[cardStatus.SelectedIndex][1].ToString();

            msg = msg + num + ". 카드상태 : " + newCode + "  (" + newDesc + ")\n"; num++;
            protocolString = protocolString + ",17=" + newCode;




            // if (!du.getBadgeFormat().Equals(this.cardFormat.Text.Trim())) { msg = msg + num + ". 카드포맷 : " + du.getBadgeFormat() + go + this.cardFormat.Text + "\n"; num++; }

            newCode = Convert.ToInt32(Bm_Main.cardFormat.Rows[cardFormat.SelectedIndex][0]);
            newDesc = Bm_Main.cardFormat.Rows[cardFormat.SelectedIndex][1].ToString();

            msg = msg + num + ". 카드포맷 : " + newCode + "  (" + newDesc + ")\n"; num++;
            protocolString = protocolString + ",18=" + newCode;






            // if (!du.getIssueType().Equals(this.issueType.Text.Trim())) { msg = msg + num + ". 발급유형 : " + du.getIssueType() + go + this.issueType.Text + "\n"; num++; }

            newCode = Convert.ToInt32(Bm_Main.issueType.Rows[issueType.SelectedIndex][0]);
            newDesc = Bm_Main.issueType.Rows[issueType.SelectedIndex][1].ToString();

            msg = msg + num + ". 발급유형 : " + newCode + "  (" + newDesc + ")\n"; num++;
            protocolString = protocolString + ",19=" + newCode;





            // if (!du.getIssueReason().Equals(this.issueReason.Text.Trim())) { msg = msg + num + ". 발급사유 : " + du.getIssueReason() + go + this.issueReason.Text + "\n"; num++; }

            newCode = Convert.ToInt32(Bm_Main.issueReason.Rows[issueReason.SelectedIndex][0]);
            newDesc = Bm_Main.issueReason.Rows[issueReason.SelectedIndex][1].ToString();

            msg = msg + num + ". 발급사유 : " + newCode + "  (" + newDesc + ")\n"; num++;
            protocolString = protocolString + ",20=" + newCode;


            // if (!du.getActive().ToString().StartsWith(this.startDate.Text)) { msg = msg + num + ". 시 작 일 : " + du.getActive().ToString() + go + this.startDate.Text + "\n"; num++; }
            // if (!du.getDeactive().ToString().StartsWith(this.endDate.Text)) { msg = msg + num + ". 만 료 일 : " + du.getDeactive().ToString() + go + this.endDate.Text + "\n"; num++; }


            msg = msg + num + ". 시 작 일 : " + getDate(this.startDate) + "\n"; num++;
            protocolString = protocolString + ",21=" + getDate(this.startDate);



            msg = msg + num + ". 만 료 일 : " + getDate(this.endDate) + "\n"; num++;
            protocolString = protocolString + ",22=" + getDate(this.endDate);



            string tmpCheck = "0";


            tmpCheck = "0";
            if (alwaysCheckBox.CheckState == CheckState.Checked) tmpCheck = "1";
            protocolString = protocolString + ",23=" + tmpCheck;




            tmpCheck = "0";
            if (fpCheck.CheckState == CheckState.Checked) tmpCheck = "0";



            if (Convert.ToInt32(tmpCheck) == 1)
            {
                Log.WriteLogTmp("========================  : " + ",  check : " + tmpCheck);
                msg = msg + num + ". FP등록 : " + tmpCheck + "\n"; num++;
                protocolString = protocolString + ",24=" + tmpCheck;

            }
            else
            {
                Log.WriteLogTmp("========================  " + ",  check : " + tmpCheck);
                msg = msg + num + ". FP등록 : " + tmpCheck + "\n"; num++;
                protocolString = protocolString + ",24=" + tmpCheck;
            }

            tmpCheck = "0";
            if (vmCheck.CheckState == CheckState.Checked) tmpCheck = "1";


            msg = msg + num + ". VM등록 : " + tmpCheck + "\n"; num++;
            protocolString = protocolString + ",25=" + tmpCheck;

            tmpCheck = "0";
            if (plant1CheckBox.Checked) tmpCheck = "1";

            msg = msg + num + ". 1발등록 : " + tmpCheck + "\n"; num++;
            protocolString = protocolString + ",26=" + tmpCheck;


            tmpCheck = "0";
            if (plant2CheckBox.Checked) tmpCheck = "1";

            msg = msg + num + ". 2발등록 : " + tmpCheck + "\n"; num++;
            protocolString = protocolString + ",27=" + tmpCheck;


            tmpCheck = "0";
            if (plant3CheckBox.Checked) tmpCheck = "1";

            msg = msg + num + ". 3발등록 : " + tmpCheck + "\n"; num++;
            protocolString = protocolString + ",28=" + tmpCheck;


            tmpCheck = "0";
            if (plant4CheckBox.Checked) tmpCheck = "1";

            msg = msg + num + ". 4발등록 : " + tmpCheck + "\n"; num++;
            protocolString = protocolString + ",29=" + tmpCheck;

            tmpCheck = "0";
            if (mdmCheck.Checked) tmpCheck = "1";

            msg = msg + num + ". MDM : " + tmpCheck + "\n"; num++;
            protocolString = protocolString + ",30=" + tmpCheck;



            tmpCheck = "1";
            if (ptCheckBox.Checked) tmpCheck = "2";
            protocolString = protocolString + ",31=" + tmpCheck;










            /////////////////////////////////////////////// 권    한 ///////////////////////////////////////////////////////

            changeRightString = "";



            for (int i = 0; i < orgPlantRight.Length; i++)
            {

                changeRightString = "\n";

                string[] tmpRight = getCurRight();
                if (tmpRight[i].Equals("")) tmpRight[i] = "-100";
                protocolString = protocolString + "," + ((i + 1) * 1000) + "=" + tmpRight[i];

            }
            if (!changeRightString.Equals("")) msg = msg + num + ". 권    한 : \n" + changeRightString + "\n"; num++;





            // MessageBox.Show(titleString + msg);
            if (!msg.Equals(""))
            {
                // MessageBox.Show(titleString + msg);
                // MessageBox.Show(protocolString);

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


        public Boolean makeMassChange(ArrayList bidArr, ArrayList empArr)
        {
            string titleString = "[일괄변경 모드] =====\n\n";

            int orgCode = -1;
            int newCode = -1;
            string newDesc = "";


            string multiBid = "";

            for (int i = 0; i < bidArr.Count; i++)
            {
                if (multiBid.Equals("")) multiBid = (string)bidArr[i];
                else multiBid = multiBid + "$" + (string)bidArr[i];
            } string protocolString = "99=" + multiBid;




            string multiEmp = "";
            for (int i = 0; i < empArr.Count; i++)
            {
                if (multiEmp.Equals("")) multiEmp = (string)empArr[i];
                else multiEmp = multiEmp + "$" + (string)empArr[i];
            }
            protocolString = protocolString + ",98=" + multiEmp;



            // Log.WriteLogTmp("protocolString : " + protocolString);
            Boolean flag = false;

            string msg = "";

            int num = 1;



            if (department.SelectedIndex != -1)
            {

                newCode = Convert.ToInt32(Bm_Main.department.Rows[department.SelectedIndex][0]);
                newDesc = Bm_Main.department.Rows[department.SelectedIndex][1].ToString();
                if (!department.Text.Equals("현재데이터유지"))
                {
                    msg = msg + num + ". 소    속 : " + newCode + "  (" + newDesc + ")\n"; num++;
                    protocolString = protocolString + ",6=" + newCode;
                }

            }
            if (division.SelectedIndex != -1)
            {
                newCode = Convert.ToInt32(Bm_Main.division.Rows[division.SelectedIndex][0]);
                newDesc = Bm_Main.division.Rows[division.SelectedIndex][1].ToString();
                if (!division.Text.Equals("현재데이터유지"))
                {
                    msg = msg + num + ". 부    서 : " + newCode + "  (" + newDesc + ")\n"; num++;
                    protocolString = protocolString + ",7=" + newCode;
                }

            }

            if (title.SelectedIndex != -1)
            {

                newCode = Convert.ToInt32(Bm_Main.title.Rows[title.SelectedIndex][0]);
                newDesc = Bm_Main.title.Rows[title.SelectedIndex][1].ToString();
                if (!title.Text.Equals("현재데이터유지"))
                {
                    msg = msg + num + ". 직    위 : " + newCode + "  (" + newDesc + ")\n"; num++;
                    protocolString = protocolString + ",8=" + newCode;
                }
            }



            if (alwaysCheckBox.ForeColor == Color.Red)
            {
                string tmpCheck = "0";
                if (alwaysCheckBox.CheckState == CheckState.Checked) tmpCheck = "1";
                else tmpCheck = "0";

                msg = msg + num + ". 항시통과 : " + tmpCheck + "\n"; num++;
                protocolString = protocolString + ",23=" + tmpCheck;
            }

            if (ptCheckBox.ForeColor == Color.Red)
            {
                string tmpCheck = "0";
                if (ptCheckBox.CheckState == CheckState.Checked) tmpCheck = "1";
                else tmpCheck = "0";

                msg = msg + num + ".  상시수시 : " + tmpCheck + "\n"; num++;
                protocolString = protocolString + ",31=" + tmpCheck;
            }

            if (cardType.SelectedIndex != -1)
            {

                newCode = Convert.ToInt32(Bm_Main.cardType.Rows[cardType.SelectedIndex][0]);
                newDesc = Bm_Main.cardType.Rows[cardType.SelectedIndex][1].ToString();
                if (!cardType.Text.Equals("현재데이터유지"))
                {
                    msg = msg + num + ". 카드타입 : " + newCode + "  (" + newDesc + ")\n"; num++;
                    protocolString = protocolString + ",16=" + newCode;
                }

            }

            if (cardStatus.SelectedIndex != -1)
            {

                newCode = Convert.ToInt32(Bm_Main.cardStat.Rows[cardStatus.SelectedIndex][0]);
                newDesc = Bm_Main.cardStat.Rows[cardStatus.SelectedIndex][1].ToString();
                if (!cardStatus.Text.Equals("현재데이터유지"))
                {
                    msg = msg + num + ". 카드상태 : " + newCode + "  (" + newDesc + ")\n"; num++;
                    protocolString = protocolString + ",17=" + newCode;
                }

            }

            if (cardFormat.SelectedIndex != -1)
            {
                newCode = Convert.ToInt32(Bm_Main.cardFormat.Rows[cardFormat.SelectedIndex][0]);
                newDesc = Bm_Main.cardFormat.Rows[cardFormat.SelectedIndex][1].ToString();
                if (!cardFormat.Text.Equals("현재데이터유지"))
                {
                    msg = msg + num + ". 카드포맷 : " + newCode + "  (" + newDesc + ")\n"; num++;
                    protocolString = protocolString + ",18=" + newCode;

                }
            }

            if (!endDate.Text.Equals("현재데이터유지"))
            {
                msg = msg + num + ". 만료일 : " + getDate(this.endDate) + "  (" + getDate(this.endDate) + ")\n"; num++;
                protocolString = protocolString + ",22=" + getDate(this.endDate);

            }


            /////////////////////////////////////////////// 권    한 ///////////////////////////////////////////////////////

            changeRightString = "";



            CheckBox[] checkArr = { plant1CheckBox, plant2CheckBox, plant3CheckBox, plant4CheckBox };

            for (int i = 0; i < orgPlantRight.Length; i++)
            {
                if (checkArr[i].CheckState == CheckState.Checked)
                {
                    changeRightString = "\n";

                    string[] tmpRight = getCurRight();
                    protocolString = protocolString + "," + ((i + 1) * 1000) + "=" + tmpRight[i];
                }

            }
            if (!changeRightString.Equals("")) msg = msg + num + ". 권    한 : \n" + changeRightString + "\n"; num++;




            if (!msg.Equals(""))
            {
                // MessageBox.Show(titleString + msg);
                // PJH0612
                // MessageBox.Show(protocolString);

                Gloval.protocolString = protocolString;

                flag = true;

                new Parser().parseCard(protocolString, "MASS_CHANGE");
            }
            else
            {
                flag = false;
            }
            return flag;

        }


        public void drawFP(int _fpNum)
        {

            Random r = new Random();
            int randomNum = r.Next(0, 10);

            Bitmap[] img = { new Bitmap (".\\img\\f0.png"), new Bitmap (".\\img\\f1.png"), new Bitmap (".\\img\\f2.png"), new Bitmap (".\\img\\f3.png"), new Bitmap (".\\img\\f4.png"), 
                                    new Bitmap (".\\img\\f5.png"), new Bitmap (".\\img\\f6.png"), new Bitmap (".\\img\\f7.png"), new Bitmap (".\\img\\f8.png"), new Bitmap (".\\img\\f9.png")};






            // finger1.BackColor = finger2.BackColor = Color.Silver;
            // finger1.ForeColor = finger2.ForeColor = Color.Black;

            name3.Text = name.Text;
            commonCardNum3.Text = commonCardNum.Text;
            birthday3.Text = birthday.Value.Year + "년" + birthday.Value.Month + "월" + birthday.Value.Day + "일";
            gender3.Text = gender.Text;


            // JSJ 
            tmpNum.Text = "" + _fpNum;

            if (_fpNum == -1)
            {
                name3.Text = commonCardNum3.Text = birthday3.Text = gender3.Text = "";
                finger1.Visible = finger2.Visible = finger3.Visible = finger4.Visible = false;
                fpImg.Image = null;
                fpImg.BackColor = Color.Silver;

                fpCardCheck.Checked = false;
                fpCheck.Checked = false;
            }
            else if (_fpNum == 0)
            {
                finger1.Visible = finger2.Visible = finger3.Visible = finger4.Visible = false;
                fpImg.Image = null;
                fpImg.BackColor = Color.Silver;
                fpCardCheck.Checked = true;
                fpCheck.Checked = true;

            }
            else if (_fpNum == 1)
            {
                finger1.Visible = true;
                finger1.BackColor = Color.Blue;
                finger1.ForeColor = Color.White;

                finger2.Visible = finger3.Visible = finger4.Visible = false;

                fpImg.Image = util.ResizeBitmap(img[randomNum], fpImg.Width, fpImg.Height);
                fpCardCheck.Checked = true;
                fpCheck.Checked = true;

            }
            else if (_fpNum == 2)
            {
                finger1.Visible = finger2.Visible = true;
                finger1.BackColor = finger2.BackColor = Color.Blue;
                finger1.ForeColor = finger2.ForeColor = Color.White;

                finger3.Visible = finger4.Visible = false;

                fpImg.Image = util.ResizeBitmap(img[randomNum], fpImg.Width, fpImg.Height);
                fpCardCheck.Checked = true;
                fpCheck.Checked = true;
            }
            else if (_fpNum == 3)
            {
                finger1.Visible = finger2.Visible = finger3.Visible = true;
                finger1.BackColor = finger2.BackColor = finger3.BackColor = Color.Blue;
                finger1.ForeColor = finger2.ForeColor = finger3.ForeColor = Color.White;

                finger4.Visible = false;

                fpImg.Image = util.ResizeBitmap(img[randomNum], fpImg.Width, fpImg.Height);
                fpCardCheck.Checked = true;
                fpCheck.Checked = true;
            }
            else if (_fpNum == 4)
            {
                finger1.Visible = finger2.Visible = finger3.Visible = finger4.Visible = true;
                finger1.BackColor = finger2.BackColor = finger3.BackColor = finger4.BackColor = Color.Blue;
                finger1.ForeColor = finger2.ForeColor = finger3.ForeColor = finger4.ForeColor = Color.White;

                fpImg.Image = util.ResizeBitmap(img[randomNum], fpImg.Width, fpImg.Height);
                fpCardCheck.Checked = true;
                fpCheck.Checked = true;
            }

        }

        public Boolean checkName()
        {
            if (name.Text == null || name.Text.Trim().Equals(""))
            {
                MessageBox.Show("이름을 확인하세요");
                return false;
            }

            return true;
        }

        public Boolean checkStartEndDate()
        {
            System.TimeSpan diffResult = endTime.Subtract(startTime);

            if (Convert.ToInt32("" + diffResult.Days) < 0)
            {
                MessageBox.Show("유효기간 만기일이 시작일보다 과거입니다.");
                return false;
            }

            return true;
        }

        public Boolean checkCardNum()
        {
            if (cardNum.Text == null || cardNum.Text.Trim().Length < 1 || cardNum.Text.Trim().Length > 12)
            {
                MessageBox.Show("카드번호를 확인하세요");
                return false;
            }

            string tmp = cardNum.Text.Trim();

            for (int i = 0; i < tmp.Length; i++)
            {
                string unitNum = tmp.Substring(i, 1);
                if (!isNumOrNot(unitNum))
                {
                    MessageBox.Show("카드번호를 확인하세요");
                    return false;
                }
            }

            return true;


        }

        public Boolean isNumOrNot(string _str)
        {

            try
            {

                long i = Convert.ToInt64(_str);

                return true;

            }
            catch (FormatException e)
            {
                return false;
            }
            catch (OverflowException oe)
            {
                return false;
            }

        }

        private void changeStartDate(object sender, EventArgs e)
        {
            Log.WriteLog("...................." + startDate.Value.ToString());
            Log.WriteLog("...................." + DateTime.Now.ToString());

            startTime = new DateTime(startDate.Value.Year, startDate.Value.Month, startDate.Value.Day);

            TimeSpan ts = today - startTime;
            if (ts.Days < 0) cardGroupBox.BackColor = Color.FromArgb(0xd8, 0xd8, 0xd8);
            else
            {
                ts = endTime - today;
                if (ts.Days >= 0)
                {
                    if (curMode.Equals("VIEW")) cardGroupBox.BackColor = viewColor;
                    else if (curMode.Equals("REG")) cardGroupBox.BackColor = regColor;
                    else if (curMode.Equals("MASS_CHANGE")) cardGroupBox.BackColor = massColor;
                }
                else
                {
                    cardGroupBox.BackColor = Color.FromArgb(0xd8, 0xd8, 0xd8);
                }
            }

        }

        private void changeEndDate(object sender, EventArgs e)
        {

            endTime = new DateTime(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day);

            TimeSpan ts = endTime - today;
            if (ts.Days < 0) cardGroupBox.BackColor = Color.FromArgb(0xd8, 0xd8, 0xd8);
            else
            {
                ts = today - startTime;

                if (ts.Days >= 0)
                {
                    if (curMode.Equals("VIEW")) cardGroupBox.BackColor = viewColor;
                    else if (curMode.Equals("REG")) cardGroupBox.BackColor = regColor;
                    else if (curMode.Equals("MASS_CHANGE")) cardGroupBox.BackColor = massColor;
                }
                else
                {
                    cardGroupBox.BackColor = Color.FromArgb(0xd8, 0xd8, 0xd8);
                }
            }

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

        public string getDate(DateTimePicker dtp)
        {
            string date = "";

            string month = "" + dtp.Value.Month;
            while (month.Length < 2) month = "0" + month;
            string day = "" + dtp.Value.Day;
            while (day.Length < 2) day = "0" + day;

            date = "" + dtp.Value.Year + month + day;
            return date;
        }


        public byte[] getCurSazin()
        {

            byte[] sazinImage = null;

            try
            {

                MemoryStream ms = new MemoryStream();
                // sazin.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                sazin.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
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
        public string addZero(string org)
        {
            while (org.Length < 12)
            {
                org = "0" + org;
            }

            return org;
        }

        public int getAccessID(ArrayList arr, int index)
        {
            RightUnit tmp = (RightUnit)arr[index];

            // Log.WriteLogTmp(".........................right : " + arr.Count);

            string str = "-100";

            str = tmp.getID();
            int result = Convert.ToInt32(str);

            //   Log.WriteLogTmp(".........................accessID : " + result);
            return result;

        }


        public ArrayList separate(string _totalString, string _separator)
        {
            if (_totalString.Equals("") || _totalString == null) return null;

            // totalRight_2 = "2~3~4~5~6~7~8~289~100~102~103~2~3~4~5~6~7~8";

            ArrayList arr = new ArrayList();

            while (true)
            {
                int index = -1;
                index = _totalString.IndexOf("~");
                string bb = "";

                if (index < 0)
                {
                    bb = _totalString.Substring(0);
                    // Log.WriteLogTmp("............. : bb last : " + bb);
                    arr.Add(bb);
                    return arr;
                }
                bb = _totalString.Substring(0, index);
                _totalString = _totalString.Substring(index + 1);
                // Log.WriteLogTmp("............. : bb : " + bb);
                arr.Add(bb);

            }


        }

        public string getRightDesc(int plantNum, int accID)
        {

            if (plantNum == 1)
            {
                for (int i = 0; i < rightArr1.Count; i++)
                {
                    RightUnit ru = (RightUnit)rightArr1[i];
                    if (ru.getID().Equals(accID)) return ru.getDesc();
                }
            }
            else if (plantNum == 2)
            {
                for (int i = 0; i < rightArr2.Count; i++)
                {
                    RightUnit ru = (RightUnit)rightArr2[i];
                    if (ru.getID().Equals(accID)) return ru.getDesc();
                }
            }
            else if (plantNum == 3)
            {
                for (int i = 0; i < rightArr3.Count; i++)
                {
                    RightUnit ru = (RightUnit)rightArr3[i];
                    if (ru.getID().Equals(accID)) return ru.getDesc();
                }
            }
            else if (plantNum == 4)
            {
                for (int i = 0; i < rightArr4.Count; i++)
                {
                    RightUnit ru = (RightUnit)rightArr4[i];
                    if (ru.getID().Equals(accID)) return ru.getDesc();
                }
            }

            return null;
        }




        public string[] getCurRight()
        {
            DataGridView[] dgv = { plant1Grid, plant2Grid, plant3Grid, plant4Grid };

            string[] plantRight = { "-100", "-100", "-100", "-100" };


            if (cardStatus.SelectedIndex == 0)
            {
                for (int j = 0; j < dgv.Length; j++)
                {
                    plantRight[j] = "";

                    for (int i = 0; i < dgv[j].RowCount; i++)
                    {
                        if (dgv[j].Rows[i].Cells[0].Style.BackColor == Color.Yellow)
                        {

                            // if (!newPlantRight[j].Equals("")) newPlantRight[j] = newPlantRight[j] + "," + (i + 1);

                            if (j == 0)
                            {
                                if (plantRight[j].Equals("")) plantRight[j] = "" + getAccessID(rightArr1, i);
                                else plantRight[j] = plantRight[j] + "~" + getAccessID(rightArr1, i);
                            }
                            else if (j == 1)
                            {
                                if (plantRight[j].Equals("")) plantRight[j] = "" + getAccessID(rightArr2, i);
                                else plantRight[j] = plantRight[j] + "~" + getAccessID(rightArr2, i);
                            }
                            else if (j == 2)
                            {
                                if (plantRight[j].Equals("")) plantRight[j] = "" + getAccessID(rightArr3, i);
                                else plantRight[j] = plantRight[j] + "~" + getAccessID(rightArr3, i);
                            }
                            else if (j == 3)
                            {
                                if (plantRight[j].Equals("")) plantRight[j] = "" + getAccessID(rightArr4, i);
                                else plantRight[j] = plantRight[j] + "~" + getAccessID(rightArr4, i);
                            }
                        }
                    }
                }
            }



            if (!curMode.Equals("REG"))
            {

                /*
                if (plantRight[0].Equals("")) plantRight[0] = "-100";
                if (plantRight[1].Equals("")) plantRight[1] = "-100";
                if (plantRight[2].Equals("")) plantRight[2] = "-100";
                if (plantRight[3].Equals("")) plantRight[3] = "-100";
                */
                if (cardStatus.SelectedIndex == 0)
                {
                    CheckBox[] checkArr = { plant1CheckBox, plant2CheckBox, plant3CheckBox, plant4CheckBox };

                    for (int i = 0; i < orgPlantRight.Length; i++)
                    {
                        if (checkArr[i].CheckState == CheckState.Checked)
                        {

                            if (plantRight[i].Equals("")) plantRight[i] = "-100";
                        }
                    }
                }
            }


            return plantRight;
        }

        public ArrayList getMultiBid()
        {
            ArrayList d1 = new ArrayList();


            if (dataGridView1.RowCount < 1) return null;

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[2].Value == "V")
                {
                    ListUnit lu = (ListUnit)listArr[i];
                    d1.Add(lu.getBID());

                }
            }
            return d1;

        }

        public ArrayList getMultiEmp()
        {
            ArrayList d1 = new ArrayList();


            if (dataGridView1.RowCount < 1) return null;

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[2].Value == "V")
                {
                    ListUnit lu = (ListUnit)listArr[i];
                    d1.Add(lu.getEmpID());

                }
            }
            return d1;

        }

        private void excel_Click(object sender, EventArgs e)
        {
            Bm_BatchProcessing batchFrom = new Bm_BatchProcessing();
            batchFrom.StartPosition = FormStartPosition.CenterScreen;
            batchFrom.Show();
        }
        public string getCardNum()
        {
            return cardNum.Text.Trim();
        }

        public string getCardName()
        {
            return cardName.Text.Trim();
        }


        private void pinButton_Click(object sender, EventArgs e)
        {
            Bm_PinInput pinInput = new Bm_PinInput(this);
            pinInput.ShowDialog();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ohButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("준비중입니다!");
            return;
        }


        private void plantRegButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> sendData = new Dictionary<string, string>();
            sendData.Add("name", name.Text);
            sendData.Add("gender", "" + (gender.SelectedIndex + 1));
            sendData.Add("birth", birthday.Text);
            sendData.Add("ssno", ssno.Text);

            string newCode = "" + Convert.ToInt32(Bm_Main.department.Rows[department.SelectedIndex][0]);
            sendData.Add("department", newCode);

            newCode = "" + Convert.ToInt32(Bm_Main.division.Rows[division.SelectedIndex][0]);
            sendData.Add("division", newCode);

            newCode = "" + Convert.ToInt32(Bm_Main.title.Rows[title.SelectedIndex][0]);
            sendData.Add("title", newCode);

            newCode = "" + Convert.ToInt32(Bm_Main.cardType.Rows[cardType.SelectedIndex][0]);
            sendData.Add("cardType", newCode);

            newCode = "" + Convert.ToInt32(Bm_Main.cardStat.Rows[cardStatus.SelectedIndex][0]);
            sendData.Add("cardStat", newCode);

            newCode = "" + Convert.ToInt32(Bm_Main.cardFormat.Rows[cardFormat.SelectedIndex][0]);
            sendData.Add("cardFormat", newCode);

            newCode = "" + Convert.ToInt32(Bm_Main.issueReason.Rows[issueReason.SelectedIndex][0]);
            sendData.Add("issueReason", newCode);

            newCode = "" + Convert.ToInt32(Bm_Main.issueType.Rows[issueType.SelectedIndex][0]);
            sendData.Add("issueType", newCode);

            int tmp = 1;
            if (ptCheckBox.Checked) tmp = 2;



            sendData.Add("pt", "" + tmp);
            sendData.Add("email", email.Text);
            sendData.Add("cardName", cardName.Text);
            sendData.Add("address", address.Text);
            sendData.Add("tel", tel.Text);
            sendData.Add("activate", startDate.Text);
            sendData.Add("deactivate", endDate.Text);
            sendData.Add("bid", addZero(cardNum.Text));
            if (alwaysCheckBox.Checked)
            {
                sendData.Add("byPass", "" + 1);
            }
            else
            {
                sendData.Add("byPass", "" + 0);
            }


            string[] right = getCurRight();

            if (right[0].Equals("")) right[0] = "-100";
            if (right[1].Equals("")) right[1] = "-100";
            if (right[2].Equals("")) right[2] = "-100";
            if (right[3].Equals("")) right[3] = "-100";
            sendData.Add("right1", right[0]);
            sendData.Add("right2", right[1]);
            sendData.Add("right3", right[2]);
            sendData.Add("right4", right[3]);

            PlantRegForm reg = new PlantRegForm(sendData, this);
            reg.Owner = this;
            reg.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] right = getCurRight();
            AuthRetryForm authRetry = new AuthRetryForm(right, cardNum.Text);
            authRetry.ShowDialog();
        }

        // JSJ 0611
        public string getIni(string key)
        {
            using (StreamReader sr = new StreamReader(".\\client.ini"))
            {
                String line;

                String delimStr = "=";
                char[] delimiter = delimStr.ToCharArray();
                String[] strData = null;
                while ((line = sr.ReadLine()) != null)
                {

                    if (line.StartsWith(key))
                    {
                        int index = line.IndexOf("=");
                        if (index < 0) return null;

                        string result = line.Substring(index + 1);
                        return result;

                    }
                }
            }

            return null;

        }

        private void deleteCardButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("해당카드를 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;
                int plantReg1 = 0;
                int plantReg2 = 0;
                int plantReg3 = 0;
                int plantReg4 = 0;
                int FPReg = 0;
                int VMReg = 0;

                if (plant1RegCheckBox.Checked) plantReg1 = 1;
                if (plant2RegCheckBox.Checked) plantReg2 = 1;
                if (plant3RegCheckBox.Checked) plantReg3 = 1;
                if (plant4RegCheckBox.Checked) plantReg4 = 1;
                if (fpCheck.Checked) FPReg = 1;
                if (vmCheck.Checked) VMReg = 1;

                // 
                //




                string resultString = req.update("BMS011", cardNum.Text + "&" + plantReg1 + "&" + plantReg2 + "&" + plantReg3 + "&" + plantReg4 + "&" + FPReg + "&" + VMReg, "BMD");

                if (resultString.Equals("1"))
                {
                    MessageBox.Show("장애 발생");
                    Log.WriteLog("[Reg.cs] deleteCardButton_Click () 장애");
                    showDefaultImage();
                    Cursor = Cursors.Default;
                    return;
                }
                else
                {
                    string Check = req.update("BMS0011", qm.getQuery("REG001_LOG_D", cardNum.Text), "BMI");
                    if (!Check.Equals("0")) MessageBox.Show("로그 저장 실패..");

                    MessageBox.Show("정상적으로 삭제되었습니다.");
                    showCardList(50);
                }

            }
            else
            {
                return;
            }
            Cursor = Cursors.Default;
        }

        private void reRegButton_Click(object sender, EventArgs e)
        {

          
            BM_ReReg reReg = new BM_ReReg(this);
            reReg.ShowDialog();

        }

        private void endDateButton_Click(object sender, EventArgs e)
        {
            EndDateSetting form = new EndDateSetting(cardNum.Text, plant1RegCheckBox.Checked, plant2RegCheckBox.Checked, plant3RegCheckBox.Checked, plant4RegCheckBox.Checked);
            form.ShowDialog();
        }

        private void cardRestoreButton_Click(object sender, EventArgs e)
        {
            CardRestoreForm restore = new CardRestoreForm(cardNum.Text);
            restore.ShowDialog();
        }


        

        private void endDate1Button_Click(object sender, EventArgs e)
        {

            if (curMode.Equals("MASS_CHANGE"))
            {
                ArrayList bidArr = getMultiBid();
                string multiBid = "";
                string query = "";
                string updateQuery = "update BADGE set DEACTIVATE_DATE1='" + acs1EndDateTime.Value.ToShortDateString() + "' ,DEACTIVATE_DATE ='" + acs1EndDateTime.Value.ToShortDateString() + "' ,MODIFY_DATE =getdate() where bid =";
                for (int i = 0; i < bidArr.Count; i++)
                {
                    query += "insert into PROCESS_REQUEST "
                             + " (BID,NAME,BRITH,GENDER,SSNO,DEPARTMENT,DIVISION,TITLE,EMAIL,TEL,ADDRESS,DESCRIPTION,PIN,TYPE,STATUS_1,STATUS_2,STATUS_3,STATUS_4,FORMAT,ISSUE_TYPE,ISSUE_REASON,ACTIVATE_DATE,DEACTIVATE_DATE,BYPASS_FLAG,ACS_1,ACS_2,ACS_3,ACS_4,ACS_5,FP_1,FP_2,VM,RIGHT_1,RIGHT_2,RIGHT_3,RIGHT_4,ACS1_RESULT,ACS2_RESULT,ACS3_RESULT,ACS4_RESULT,FP1_RESULT,FP2_RESULT,FP3_RESULT,FP4_RESULT,VM_RESULT,Q_TYPE,OLD_BID,PT)"
                             + " values('-10' , '-10' , '1800-01-01' , -10 , '-10' , -10 , -10 , -10 , '-10' , '-10' , '-10' ,'-10' ,"
                             + "-10, -10 , -10 ,-10 ,-10 ,-10 , -10 , -10 , -10 ,'1800-01-01','" + acs1EndDateTime.Value.ToShortDateString() + "' , -10 , -10,-10 ,-10 ,-10 ,-10 , -10 , -10 ,-10 , -10 ,-10 ,-10 ,-10 ,0,100,100,100,0,100,100,100,100,'U','"
                             + (string)bidArr[i] + "',-10)";

                    if (i == 0)
                    {
                        updateQuery += "'" + (string)bidArr[i] + "' ";

                    }
                    else
                    {
                        updateQuery += " or bid ='" + (string)bidArr[i] + "' ";
                    }

                    query += "^";


                }

                string rCheck = req.update("BMS011", query + updateQuery, "BMI");

                if (!rCheck.Equals("0"))
                {
                    MessageBox.Show("만료일 일괄수정 요청 실패 서버와의 통신을 점검해주세요..");
                    return;
                }




            }
            else if (curMode.Equals("VIEW"))
            {


                string query = "insert into PROCESS_REQUEST "
                             + " (BID,NAME,BRITH,GENDER,SSNO,DEPARTMENT,DIVISION,TITLE,EMAIL,TEL,ADDRESS,DESCRIPTION,PIN,TYPE,STATUS_1,STATUS_2,STATUS_3,STATUS_4,FORMAT,ISSUE_TYPE,ISSUE_REASON,ACTIVATE_DATE,DEACTIVATE_DATE,BYPASS_FLAG,ACS_1,ACS_2,ACS_3,ACS_4,ACS_5,FP_1,FP_2,VM,RIGHT_1,RIGHT_2,RIGHT_3,RIGHT_4,ACS1_RESULT,ACS2_RESULT,ACS3_RESULT,ACS4_RESULT,FP1_RESULT,FP2_RESULT,FP3_RESULT,FP4_RESULT,VM_RESULT,Q_TYPE,OLD_BID,PT)"
                             + " values('-10' , '-10' , '1800-01-01' , -10 , '-10' , -10 , -10 , -10 , '-10' , '-10' , '-10' ,'-10' ,"
                             + "-10, -10 , -10 ,-10 ,-10 ,-10 , -10 , -10 , -10 ,'1800-01-01','" + acs1EndDateTime.Value.ToShortDateString() + "' , -10 , -10,-10 ,-10 ,-10 ,-10 , -10 , -10 ,-10 , -10 ,-10 ,-10 ,-10 ,0,100,100,100,0,100,100,100,100,'U','"
                              + cardNum.Text + "',-10)";
            
                if (endDate.Value.CompareTo(acs1EndDateTime.Value) < 0)
                {
                    query += "^update BADGE set DEACTIVATE_DATE = '" + acs1EndDateTime.Value.ToShortDateString() + "',DEACTIVATE_DATE1 = '" + acs1EndDateTime.Value.ToShortDateString() + "' where bid = '" + cardNum.Text + "'";

                }
                else
                {
                    query += "^update BADGE set DEACTIVATE_DATE1 = '" + acs1EndDateTime.Value.ToShortDateString() + "' where bid = '" + cardNum.Text + "'";
                }


                string rCheck = req.update("BMS011", query, "BMI");

                if (!rCheck.Equals("0"))
                {
                    MessageBox.Show("만료일 수정요청 실패 서버와의 통신을 점검해주세요..");
                    return;
                }
            }

            MessageBox.Show("만료일수정 요청 성공");



            clearAllCardCheck();


            this.Cursor = Cursors.Default;


        }

        private void endDate2Button_Click(object sender, EventArgs e)
        {
            if (curMode.Equals("MASS_CHANGE"))
            {
                ArrayList bidArr = getMultiBid();
                string multiBid = "";
                string query = "";
                string updateQuery = "update BADGE set DEACTIVATE_DATE2 = '" + acs2EndDateTime.Value.ToShortDateString() + "' , DEACTIVATE_DATE ='" + acs2EndDateTime.Value.ToShortDateString() + "' ,MODIFY_DATE =getdate() where bid =";

                for (int i = 0; i < bidArr.Count; i++)
                {
                    query += "insert into PROCESS_REQUEST "
                             + " (BID,NAME,BRITH,GENDER,SSNO,DEPARTMENT,DIVISION,TITLE,EMAIL,TEL,ADDRESS,DESCRIPTION,PIN,TYPE,STATUS_1,STATUS_2,STATUS_3,STATUS_4,FORMAT,ISSUE_TYPE,ISSUE_REASON,ACTIVATE_DATE,DEACTIVATE_DATE,BYPASS_FLAG,ACS_1,ACS_2,ACS_3,ACS_4,ACS_5,FP_1,FP_2,VM,RIGHT_1,RIGHT_2,RIGHT_3,RIGHT_4,ACS1_RESULT,ACS2_RESULT,ACS3_RESULT,ACS4_RESULT,FP1_RESULT,FP2_RESULT,FP3_RESULT,FP4_RESULT,VM_RESULT,Q_TYPE,OLD_BID,PT)"
                             + " values('-10' , '-10' , '1800-01-01' , -10 , '-10' , -10 , -10 , -10 , '-10' , '-10' , '-10' ,'-10' ,"
                             + "-10, -10 , -10 ,-10 ,-10 ,-10 , -10 , -10 , -10 ,'1800-01-01','" + acs2EndDateTime.Value.ToShortDateString() + "' , -10 , -10,-10 ,-10 ,-10 ,-10 , -10 , -10 ,-10 , -10 ,-10 ,-10 ,-10 ,100,0,100,100,100,0,100,100,100,'U','"
                             + (string)bidArr[i] + "',-10)";

                    if (i == 0)
                    {
                        updateQuery += "'" + (string)bidArr[i] + "' ";

                    }
                    else
                    {
                        updateQuery += " or bid ='" + (string)bidArr[i] + "' ";
                    }

                    query += "^";


                }

                string rCheck = req.update("BMS011", query, "BMI");

                if (!rCheck.Equals("0"))
                {
                    MessageBox.Show("만료일 일괄수정 요청 실패 서버와의 통신을 점검해주세요..");
                    return;
                }


            }
            else if (curMode.Equals("VIEW"))
            {

                string query = "insert into PROCESS_REQUEST "
                             + " (BID,NAME,BRITH,GENDER,SSNO,DEPARTMENT,DIVISION,TITLE,EMAIL,TEL,ADDRESS,DESCRIPTION,PIN,TYPE,STATUS_1,STATUS_2,STATUS_3,STATUS_4,FORMAT,ISSUE_TYPE,ISSUE_REASON,ACTIVATE_DATE,DEACTIVATE_DATE,BYPASS_FLAG,ACS_1,ACS_2,ACS_3,ACS_4,ACS_5,FP_1,FP_2,VM,RIGHT_1,RIGHT_2,RIGHT_3,RIGHT_4,ACS1_RESULT,ACS2_RESULT,ACS3_RESULT,ACS4_RESULT,FP1_RESULT,FP2_RESULT,FP3_RESULT,FP4_RESULT,VM_RESULT,Q_TYPE,OLD_BID,PT)"
                             + " values('-10' , '-10' , '1800-01-01' , -10 , '-10' , -10 , -10 , -10 , '-10' , '-10' , '-10' ,'-10' ,"
                             + "-10, -10 , -10 ,-10 ,-10 ,-10 , -10 , -10 , -10 ,'1800-01-01','" + acs2EndDateTime.Value.ToShortDateString() + "' , -10 , -10,-10 ,-10 ,-10 ,-10 , -10 , -10 ,-10 , -10 ,-10 ,-10 ,-10 ,100,0,100,100,100,0,100,100,100,'U','"
                              + cardNum.Text + "',-10)";
            
                if (endDate.Value.CompareTo(acs2EndDateTime.Value) < 0)
                {
                    query += "^update BADGE set DEACTIVATE_DATE = '" + acs2EndDateTime.Value.ToShortDateString() + "',DEACTIVATE_DATE2 = '" + acs2EndDateTime.Value.ToShortDateString() + "' where bid = '" + cardNum.Text + "'";

                }
                else
                {
                    query += "^update BADGE set DEACTIVATE_DATE2 = '" + acs2EndDateTime.Value.ToShortDateString() + "' where bid = '" + cardNum.Text + "'";
                }
                string rCheck = req.update("BMS011", query, "BMI");

                if (!rCheck.Equals("0"))
                {
                    MessageBox.Show("만료일 수정요청 실패 서버와의 통신을 점검해주세요..");
                    return;
                }


            }

            MessageBox.Show("만료일수정 요청 성공");



            clearAllCardCheck();


            this.Cursor = Cursors.Default;
        }

        private void endDate3Button_Click(object sender, EventArgs e)
        {
            if (curMode.Equals("MASS_CHANGE"))
            {
                ArrayList bidArr = getMultiBid();
                string multiBid = "";
                string query = "";
                string updateQuery = "update BADGE set DEACTIVATE_DATE3 = '" + acs3EndDateTime.Value.ToShortDateString() + "' , DEACTIVATE_DATE ='" + acs3EndDateTime.Value.ToShortDateString() + "' ,MODIFY_DATE =getdate() where bid =";

                for (int i = 0; i < bidArr.Count; i++)
                {
                    query += "insert into PROCESS_REQUEST "
                             + " (BID,NAME,BRITH,GENDER,SSNO,DEPARTMENT,DIVISION,TITLE,EMAIL,TEL,ADDRESS,DESCRIPTION,PIN,TYPE,STATUS_1,STATUS_2,STATUS_3,STATUS_4,FORMAT,ISSUE_TYPE,ISSUE_REASON,ACTIVATE_DATE,DEACTIVATE_DATE,BYPASS_FLAG,ACS_1,ACS_2,ACS_3,ACS_4,ACS_5,FP_1,FP_2,VM,RIGHT_1,RIGHT_2,RIGHT_3,RIGHT_4,ACS1_RESULT,ACS2_RESULT,ACS3_RESULT,ACS4_RESULT,FP1_RESULT,FP2_RESULT,FP3_RESULT,FP4_RESULT,VM_RESULT,Q_TYPE,OLD_BID,PT)"
                             + " values('-10' , '-10' , '1800-01-01' , -10 , '-10' , -10 , -10 , -10 , '-10' , '-10' , '-10' ,'-10' ,"
                             + "-10, -10 , -10 ,-10 ,-10 ,-10 , -10 , -10 , -10 ,'1800-01-01','" + acs3EndDateTime.Value.ToShortDateString() + "' , -10 , -10,-10 ,-10 ,-10 ,-10 , -10 , -10 ,-10 , -10 ,-10 ,-10 ,-10 ,100,100,0,100,100,100,0,100,100,'U','"
                             + (string)bidArr[i] + "',-10)";

                    if (i == 0)
                    {
                        updateQuery += "'" + (string)bidArr[i] + "' ";

                    }
                    else
                    {
                        updateQuery += " or bid ='" + (string)bidArr[i] + "' ";
                    }

                    query += "^";


                }

                string rCheck = req.update("BMS011", query, "BMI");

                if (!rCheck.Equals("0"))
                {
                    MessageBox.Show("만료일 일괄수정 요청 실패 서버와의 통신을 점검해주세요..");
                    return;
                }


            }
            else if (curMode.Equals("VIEW"))
            {

                string query = "insert into PROCESS_REQUEST "
                             + " (BID,NAME,BRITH,GENDER,SSNO,DEPARTMENT,DIVISION,TITLE,EMAIL,TEL,ADDRESS,DESCRIPTION,PIN,TYPE,STATUS_1,STATUS_2,STATUS_3,STATUS_4,FORMAT,ISSUE_TYPE,ISSUE_REASON,ACTIVATE_DATE,DEACTIVATE_DATE,BYPASS_FLAG,ACS_1,ACS_2,ACS_3,ACS_4,ACS_5,FP_1,FP_2,VM,RIGHT_1,RIGHT_2,RIGHT_3,RIGHT_4,ACS1_RESULT,ACS2_RESULT,ACS3_RESULT,ACS4_RESULT,FP1_RESULT,FP2_RESULT,FP3_RESULT,FP4_RESULT,VM_RESULT,Q_TYPE,OLD_BID,PT)"
                             + " values('-10' , '-10' , '1800-01-01' , -10 , '-10' , -10 , -10 , -10 , '-10' , '-10' , '-10' ,'-10' ,"
                             + "-10, -10 , -10 ,-10 ,-10 ,-10 , -10 , -10 , -10 ,'1800-01-01','" + acs3EndDateTime.Value.ToShortDateString() + "' , -10 , -10,-10 ,-10 ,-10 ,-10 , -10 , -10 ,-10 , -10 ,-10 ,-10 ,-10 ,100,100,0,100,100,100,0,100,100,'U','"
                              + cardNum.Text + "',-10)";

         
                if (endDate.Value.CompareTo(acs3EndDateTime.Value) < 0)
                {
                    query += "^update BADGE set DEACTIVATE_DATE = '" + acs3EndDateTime.Value.ToShortDateString() + "',DEACTIVATE_DATE3 = '" + acs3EndDateTime.Value.ToShortDateString() + "' where bid = '" + cardNum.Text + "'";

                }
                else
                {
                    query += "^update BADGE set DEACTIVATE_DATE3 = '" + acs3EndDateTime.Value.ToShortDateString() + "' where bid = '" + cardNum.Text + "'";
                }
                string rCheck = req.update("BMS011", query, "BMI");

                if (!rCheck.Equals("0"))
                {
                    MessageBox.Show("만료일 수정요청 실패 서버와의 통신을 점검해주세요..");
                    return;
                }


            }


            MessageBox.Show("만료일수정 요청 성공");



            clearAllCardCheck();


            this.Cursor = Cursors.Default;
        }

        private void endDate4Button_Click(object sender, EventArgs e)
        {
            if (curMode.Equals("MASS_CHANGE"))
            {
                ArrayList bidArr = getMultiBid();
                string multiBid = "";
                string query = "";
                string updateQuery = "update BADGE set DEACTIVATE_DATE4 = '" + acs4EndDateTime.Value.ToShortDateString() + "', DEACTIVATE_DATE ='" + acs4EndDateTime.Value.ToShortDateString() + "' ,MODIFY_DATE =getdate() where bid =";


                for (int i = 0; i < bidArr.Count; i++)
                {
                    query += "insert into PROCESS_REQUEST "
                             +" (BID,NAME,BRITH,GENDER,SSNO,DEPARTMENT,DIVISION,TITLE,EMAIL,TEL,ADDRESS,DESCRIPTION,PIN,TYPE,STATUS_1,STATUS_2,STATUS_3,STATUS_4,FORMAT,ISSUE_TYPE,ISSUE_REASON,ACTIVATE_DATE,DEACTIVATE_DATE,BYPASS_FLAG,ACS_1,ACS_2,ACS_3,ACS_4,ACS_5,FP_1,FP_2,VM,RIGHT_1,RIGHT_2,RIGHT_3,RIGHT_4,ACS1_RESULT,ACS2_RESULT,ACS3_RESULT,ACS4_RESULT,FP1_RESULT,FP2_RESULT,FP3_RESULT,FP4_RESULT,VM_RESULT,Q_TYPE,OLD_BID,PT)"
                             + " values('-10' , '-10' , '1800-01-01' , -10 , '-10' , -10 , -10 , -10 , '-10' , '-10' , '-10' ,'-10' ,"
                             + "-10, -10 , -10 ,-10 ,-10 ,-10 , -10 , -10 , -10 ,'1800-01-01','" + acs4EndDateTime.Value.ToShortDateString() + "' , -10 , -10,-10 ,-10 ,-10 ,-10 , -10 , -10 ,-10 , -10 ,-10 ,-10 ,-10 ,100,100,100,0,100,100,100,0,100,'U','"
                             + (string)bidArr[i] + "',-10)";

                    if (i == 0)
                    {
                        updateQuery += "'" + (string)bidArr[i] + "' ";

                    }
                    else
                    {
                        updateQuery += " or bid ='" + (string)bidArr[i] + "' ";
                    }

                    query += "^";


                }

                string rCheck = req.update("BMS011", query, "BMI");

                if (!rCheck.Equals("0"))
                {
                    MessageBox.Show("만료일 일괄수정 요청 실패 서버와의 통신을 점검해주세요..");
                    return;
                }


            }
            else if (curMode.Equals("VIEW"))
            {

                string query = "insert into PROCESS_REQUEST "
                             + " (BID,NAME,BRITH,GENDER,SSNO,DEPARTMENT,DIVISION,TITLE,EMAIL,TEL,ADDRESS,DESCRIPTION,PIN,TYPE,STATUS_1,STATUS_2,STATUS_3,STATUS_4,FORMAT,ISSUE_TYPE,ISSUE_REASON,ACTIVATE_DATE,DEACTIVATE_DATE,BYPASS_FLAG,ACS_1,ACS_2,ACS_3,ACS_4,ACS_5,FP_1,FP_2,VM,RIGHT_1,RIGHT_2,RIGHT_3,RIGHT_4,ACS1_RESULT,ACS2_RESULT,ACS3_RESULT,ACS4_RESULT,FP1_RESULT,FP2_RESULT,FP3_RESULT,FP4_RESULT,VM_RESULT,Q_TYPE,OLD_BID,PT)"
                             + " values('-10' , '-10' , '1800-01-01' , -10 , '-10' , -10 , -10 , -10 , '-10' , '-10' , '-10' ,'-10' ,"
                             + "-10, -10 , -10 ,-10 ,-10 ,-10 , -10 , -10 , -10 ,'1800-01-01','" + acs4EndDateTime.Value.ToShortDateString() + "' , -10 , -10,-10 ,-10 ,-10 ,-10 , -10 , -10 ,-10 , -10 ,-10 ,-10 ,-10 ,100,100,100,0,100,100,100,0,100,'U','"
                              + cardNum.Text + "',-10)";
          
                if (endDate.Value.CompareTo(acs4EndDateTime.Value) < 0)
                {
                    query += "^update BADGE set DEACTIVATE_DATE = '" + acs4EndDateTime.Value.ToShortDateString() + "',DEACTIVATE_DATE4 = '" + acs4EndDateTime.Value.ToShortDateString() + "' where bid = '" + cardNum.Text + "'";

                }
                else
                {
                    query += "^update BADGE set DEACTIVATE_DATE4 = '" + acs4EndDateTime.Value.ToShortDateString() + "' where bid = '" + cardNum.Text + "'";
                }
                string rCheck = req.update("BMS011", query, "BMI");

                if (!rCheck.Equals("0"))
                {
                    MessageBox.Show("만료일 수정요청 실패 서버와의 통신을 점검해주세요..");
                    return;
                }


            }

            MessageBox.Show("만료일수정 요청 성공");



            clearAllCardCheck();


            this.Cursor = Cursors.Default;
        }

        public void getEndDate(string bid, Boolean plant1Reg, Boolean plant2Reg, Boolean plant3Reg, Boolean plant4Reg, DateTime[] time)
        {
            cardNum.Text = bid;
         

            acs1EndDateTime.Value = DateTime.Now;
            acs2EndDateTime.Value = DateTime.Now;
            acs3EndDateTime.Value = DateTime.Now;
            acs4EndDateTime.Value = DateTime.Now;

            if (plant1Reg)
            {
                acs1EndDateTime.Value = time[0];
                acs1EndDateButton.Enabled = true;
                acs1EndDateTime.Enabled = true;


                try
                {

                    if (endDate.Value.CompareTo(acs1EndDateTime.Value) < 0) endDate.Value = acs1EndDateTime.Value;
                }
                catch (Exception e)
                {
                    //MessageBox.Show("만료일 조회중에 장애 발생..");
                    acs1EndDateTime.Value = DateTime.Now;
                }

            }
            else
            {
                acs1EndDateTime.Enabled = false;
            }
            if (plant2Reg)
            {
                acs2EndDateTime.Value = time[1];
                acs2EndDateButton.Enabled = true;
                acs2EndDateTime.Enabled = true;


                try
                {


                    if (endDate.Value.CompareTo(acs2EndDateTime.Value) < 0) endDate.Value = acs2EndDateTime.Value;
                }
                catch (Exception e)
                {
                    // MessageBox.Show("만료일 조회중에 장애 발생..");
                    acs2EndDateTime.Value = DateTime.Now;
                }

            }
            else
            {
                acs2EndDateTime.Enabled = false;
            }

            if (plant3Reg)
            {
                acs3EndDateTime.Value = time[2];
                acs3EndDateButton.Enabled = true;
                acs3EndDateTime.Enabled = true;
                try
                {

                    if (endDate.Value.CompareTo(acs3EndDateTime.Value) < 0) endDate.Value = acs3EndDateTime.Value;
                }
                catch (Exception e)
                {
                    //  MessageBox.Show("만료일 조회중에 장애 발생..");
                    acs3EndDateTime.Value = DateTime.Now;
                }

            }
            else
            {
                acs3EndDateTime.Enabled = false;
            }





        }

        public string makeQuery(string ACS)
        {
            string query = "";
            if (ACS.Equals("3"))
            {
                query = "select top 1 DEACTIVATE from BADGE where id = " + cardNum.Text;
            }
            else if (ACS.Equals("1") || ACS.Equals("2"))
            {
                query = "select first 1 case when expired_date is null then 0 else expired_date end from badge where bid ='" + cardNum.Text + "'";
            }

            return query;

        }


        public DateTime getdate(string ACSType)
        {
            DateTime dt;


            if (ACSType.Equals("3"))
            {
                string[] tmp = ReturnDT.dt.Rows[0][0].ToString().Split('-');
                dt = new DateTime(Convert.ToInt32(tmp[0]), Convert.ToInt32(tmp[1]), Convert.ToInt32(tmp[2].Substring(0, 2)));

            }
            else
            {
                if (ReturnDT.dt.Rows[0][0].ToString().Equals("0"))
                {
                    dt = DateTime.Now;
                    return dt;
                }
                dt = new DateTime(Convert.ToInt32(ReturnDT.dt.Rows[0][0].ToString().Substring(0, 4)), Convert.ToInt32(ReturnDT.dt.Rows[0][0].ToString().Substring(4, 2)), Convert.ToInt32(ReturnDT.dt.Rows[0][0].ToString().Substring(6, 2)));

            }


            return dt;

        }

        private void endDateRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (endDateRadio.Checked)
            {
                disableRadio();
                searchEndDate.Enabled = true;
                nameTextBox.Text = cardNumTextBox.Text = divisionCombo.Text = typeCombo.Text = "전체";
                searchEndDate.Focus();

            }
        }

        public DateTime getEndDate()
        {
            DateTime tmp = endDate.Value;

            if (endDate.Value.CompareTo(acs1EndDateTime.Value) < 0) endDate.Value = acs1EndDateTime.Value;
            if (endDate.Value.CompareTo(acs2EndDateTime.Value) < 0) endDate.Value = acs2EndDateTime.Value;
            if (endDate.Value.CompareTo(acs3EndDateTime.Value) < 0) endDate.Value = acs3EndDateTime.Value;
            // if (endDate.Value.CompareTo(acs1EndDateTime.Value) < 0) endDate.Value = acs1EndDateTime.Value;


            return tmp;

        }

        private void BM_Reg_001_Load(object sender, EventArgs e)
        {

        }

        private void BM_Reg_001_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F && e.Control)
            {
                nameRadio.Select();
                nameTextBox.Focus();
                nameTextBox.Text = "";

            }

            if (e.KeyCode == Keys.D && e.Control)
            {
                endDateRadio.Select();
                searchEndDate.Focus();

            }


            if (e.KeyCode == Keys.N && e.Control)
            {

                if (curTab.Equals("RIGHT") || curTab.Equals("FP"))
                {
                    name.Text = name2.Text = name3.Text = cardName.Text = "";
                    commonCardNum.Text = commonCardNum2.Text = commonCardNum3.Text = "";
                    birthday.Value = new DateTime(1980, 1, 1);
                    birthday2.Text = birthday3.Text = "1980년1월1일";
                    gender.Text = gender2.Text = gender3.Text = "남자";

                }




                initializeControl("REG");

                department.SelectedIndex = 0;
                division.SelectedIndex = 0;
                title.SelectedIndex = 0;
                cardStatus.SelectedIndex = 0;

                cardFormat.SelectedIndex = 1;
                issueType.SelectedIndex = 0;
                issueReason.SelectedIndex = 0;
                fpCheck.Checked = true;
            }

        }

        private void ppDeleteButton_Click(object sender, EventArgs e)
        {

            int plantReg1 = 0;
            int plantReg2 = 0;
            int plantReg3 = 0;
            int plantReg4 = 0;


            if (plant1RegCheckBox.Checked) plantReg1 = 1;
            if (plant2RegCheckBox.Checked) plantReg2 = 1;
            if (plant3RegCheckBox.Checked) plantReg3 = 1;
            if (plant4RegCheckBox.Checked) plantReg4 = 1;



            string rNumString = req.update("BMS011", cardNum.Text + "&" + plantReg1 + "&" + plantReg2 + "&" + plantReg3 + "&" + plantReg4, "PPD");

            if (!rNumString.Equals("0"))
            {
                MessageBox.Show("PP 데이터삭제중에 오류가 발생하였습니다.");
                Cursor = Cursors.Default;
                return;
            }
            else
            {
                rNumString = req.update("BMS011", qm.getQuery("PP_DELETE_LOG", "카드번호 : " + addZero(cardNum.Text) + " PP삭제"), "BMI");

                MessageBox.Show("카드회수 완료..");

                Cursor = Cursors.Default;

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string tmpName = name.Text;
            string tmpbirth = birthday.Text;
            int tmpGender = gender.SelectedIndex;
            string tmpSSNO = ssno.Text;
            int tmpDepartment = department.SelectedIndex;
            int tmpDivision = division.SelectedIndex;
            int tmpTitle = title.SelectedIndex;
            string tmpTel = tel.Text;
            string tmpAddress = address.Text;
            string tmpEmail = email.Text;
            string tmpCardName = cardName.Text;
            int tmpCardType = cardType.SelectedIndex;
            int tmpCardStat = cardStatus.SelectedIndex;
            int tmpCardFormat = cardFormat.SelectedIndex;
            int tmpissuseType = issueType.SelectedIndex;
            int tmpIssuseReason = issueReason.SelectedIndex;
            Boolean tmpFP = fpCardCheck.Checked;
            Boolean tmpVM = vmCheck.Checked;
            Boolean tmpMDM = mdmCheck.Checked;
            Boolean tmpAlways = alwaysCheckBox.Checked;
            Boolean tmpPt = ptCheckBox.Checked;

            Boolean plant1Reg = plant1CheckBox.Checked;
            Boolean plant2Reg = plant2CheckBox.Checked;
            Boolean plant3Reg = plant3CheckBox.Checked;
            Boolean plant4Reg = plant4CheckBox.Checked;

            Boolean[] tmpPlant1 = new Boolean[plant1Grid.Rows.Count];
            Boolean[] tmpPlant2 = new Boolean[plant2Grid.Rows.Count];
            Boolean[] tmpPlant3 = new Boolean[plant3Grid.Rows.Count];
            Boolean[] tmpPlant4 = new Boolean[plant4Grid.Rows.Count];

            for (int i = 0; i < plant1Grid.Rows.Count; i++)
            {
                if (plant1Grid.Rows[i].Cells[0].Style.BackColor == Color.Yellow)
                    tmpPlant1[i] = true;
                else
                    tmpPlant1[i] = false;
            }

            for (int i = 0; i < plant2Grid.Rows.Count; i++)
            {
                if (plant2Grid.Rows[i].Cells[0].Style.BackColor == Color.Yellow)
                    tmpPlant2[i] = true;
                else
                    tmpPlant2[i] = false;
            }

            for (int i = 0; i < plant3Grid.Rows.Count; i++)
            {
                if (plant3Grid.Rows[i].Cells[0].Style.BackColor == Color.Yellow)
                    tmpPlant3[i] = true;
                else
                    tmpPlant3[i] = false;
            }

            for (int i = 0; i < plant4Grid.Rows.Count; i++)
            {
                if (plant4Grid.Rows[i].Cells[0].Style.BackColor == Color.Yellow)
                    tmpPlant4[i] = true;
                else
                    tmpPlant4[i] = false;
            }

            initializeControl("REG");



            gender.SelectedIndex = tmpGender;

            department.SelectedIndex = tmpDepartment;
            division.SelectedIndex = tmpDivision;
            title.SelectedIndex = tmpTitle;
            tel.Text = tmpTel;
            address.Text = tmpAddress;
            email.Text = tmpEmail;
            cardName.Text = tmpCardName;
            cardType.SelectedIndex = tmpCardType;
            cardStatus.SelectedIndex = tmpCardStat;
            cardFormat.SelectedIndex = tmpCardFormat;
            issueType.SelectedIndex = tmpissuseType;
            issueReason.SelectedIndex = tmpIssuseReason;
            fpCardCheck.Checked = tmpFP;
            vmCheck.Checked = tmpVM;
            mdmCheck.Checked = tmpMDM;
            alwaysCheckBox.Checked = tmpAlways;
            ptCheckBox.Checked = tmpPt;


            plant1CheckBox.Checked = plant1Reg;
            plant2CheckBox.Checked = plant2Reg;
            plant3CheckBox.Checked = plant3Reg;
            plant4CheckBox.Checked = plant4Reg;

            for (int i = 0; i < plant1Grid.Rows.Count; i++)
            {
                if (tmpPlant1[i])
                    plant1Grid.Rows[i].Cells[0].Style.BackColor = Color.Yellow;
            }

            for (int i = 0; i < plant2Grid.Rows.Count; i++)
            {
                if (tmpPlant2[i])
                    plant2Grid.Rows[i].Cells[0].Style.BackColor = Color.Yellow;
            }

            for (int i = 0; i < plant3Grid.Rows.Count; i++)
            {
                if (tmpPlant3[i])
                    plant3Grid.Rows[i].Cells[0].Style.BackColor = Color.Yellow;
            }

            for (int i = 0; i < plant4Grid.Rows.Count; i++)
            {
                if (tmpPlant4[i])
                    plant4Grid.Rows[i].Cells[0].Style.BackColor = Color.Yellow;
            }
        }


        // JSJ 2016-02-01
        public void drawFinger(int _fpNum)
        {

            Log.WriteLog("[BM_Reg_001.cs] drawFinger (" + _fpNum + ")    tmpNum : " + tmpNum);

            fpImg.Visible = true;

            if (_fpNum < 1)
            {
                fpImg.Image = null;
                fpImg.BackColor = Color.Silver;
                return;
            }


            Random r = new Random();
            int randomNum = r.Next(0, 10);

            Bitmap[] img = { new Bitmap (".\\img\\f0.png"), new Bitmap (".\\img\\f1.png"), new Bitmap (".\\img\\f2.png"), new Bitmap (".\\img\\f3.png"), new Bitmap (".\\img\\f4.png"), 
                                    new Bitmap (".\\img\\f5.png"), new Bitmap (".\\img\\f6.png"), new Bitmap (".\\img\\f7.png"), new Bitmap (".\\img\\f8.png"), new Bitmap (".\\img\\f9.png")};


            fpImg.Visible = true;
            fpImg.Image = util.ResizeBitmap(img[randomNum], fpImg.Width, fpImg.Height);
            fpImg.Refresh();

        }

        private void reAddButton_Click(object sender, EventArgs e)
        {
            string tmpName = name.Text;
            string tmpbirth = birthday.Text;
            int tmpGender = gender.SelectedIndex;
            string tmpSSNO = ssno.Text;
            int tmpDepartment = department.SelectedIndex;
            int tmpDivision = division.SelectedIndex;
            int tmpTitle = title.SelectedIndex;
            string tmpTel = tel.Text;
            string tmpAddress = address.Text;
            string tmpEmail = email.Text;
            string tmpCardName = cardName.Text;
            int tmpCardType = cardType.SelectedIndex;
            int tmpCardStat = cardStatus.SelectedIndex;
            int tmpCardFormat = cardFormat.SelectedIndex;
            int tmpissuseType = issueType.SelectedIndex;
            int tmpIssuseReason = issueReason.SelectedIndex;
            Boolean tmpFP = fpCardCheck.Checked;
            Boolean tmpVM = vmCheck.Checked;
            Boolean tmpMDM = mdmCheck.Checked;
            Boolean tmpAlways = alwaysCheckBox.Checked;
            Boolean tmpPt = ptCheckBox.Checked;

            Boolean plant1Reg = plant1CheckBox.Checked;
            Boolean plant2Reg = plant2CheckBox.Checked;
            Boolean plant3Reg = plant3CheckBox.Checked;
            Boolean plant4Reg = plant4CheckBox.Checked;

            Boolean[] tmpPlant1 = new Boolean[plant1Grid.Rows.Count];
            Boolean[] tmpPlant2 = new Boolean[plant2Grid.Rows.Count];
            Boolean[] tmpPlant3 = new Boolean[plant3Grid.Rows.Count];
            Boolean[] tmpPlant4 = new Boolean[plant4Grid.Rows.Count];

            for (int i = 0; i < plant1Grid.Rows.Count; i++)
            {
                if (plant1Grid.Rows[i].Cells[0].Style.BackColor == Color.Yellow)
                    tmpPlant1[i] = true;
                else
                    tmpPlant1[i] = false;
            }

            for (int i = 0; i < plant2Grid.Rows.Count; i++)
            {
                if (plant2Grid.Rows[i].Cells[0].Style.BackColor == Color.Yellow)
                    tmpPlant2[i] = true;
                else
                    tmpPlant2[i] = false;
            }

            for (int i = 0; i < plant3Grid.Rows.Count; i++)
            {
                if (plant3Grid.Rows[i].Cells[0].Style.BackColor == Color.Yellow)
                    tmpPlant3[i] = true;
                else
                    tmpPlant3[i] = false;
            }

            for (int i = 0; i < plant4Grid.Rows.Count; i++)
            {
                if (plant4Grid.Rows[i].Cells[0].Style.BackColor == Color.Yellow)
                    tmpPlant4[i] = true;
                else
                    tmpPlant4[i] = false;
            }

            initializeControl("REG");



            gender.SelectedIndex = tmpGender;

            department.SelectedIndex = tmpDepartment;
            division.SelectedIndex = tmpDivision;
            title.SelectedIndex = tmpTitle;
            tel.Text = tmpTel;
            address.Text = tmpAddress;
            email.Text = tmpEmail;
            cardName.Text = tmpCardName;
            cardType.SelectedIndex = tmpCardType;
            cardStatus.SelectedIndex = tmpCardStat;
            cardFormat.SelectedIndex = tmpCardFormat;
            issueType.SelectedIndex = tmpissuseType;
            issueReason.SelectedIndex = tmpIssuseReason;
            fpCardCheck.Checked = tmpFP;
            vmCheck.Checked = tmpVM;
            mdmCheck.Checked = tmpMDM;
            alwaysCheckBox.Checked = tmpAlways;
            ptCheckBox.Checked = tmpPt;


            plant1CheckBox.Checked = plant1Reg;
            plant2CheckBox.Checked = plant2Reg;
            plant3CheckBox.Checked = plant3Reg;
            plant4CheckBox.Checked = plant4Reg;

            for (int i = 0; i < plant1Grid.Rows.Count; i++)
            {
                if (tmpPlant1[i])
                    plant1Grid.Rows[i].Cells[0].Style.BackColor = Color.Yellow;
            }

            for (int i = 0; i < plant2Grid.Rows.Count; i++)
            {
                if (tmpPlant2[i])
                    plant2Grid.Rows[i].Cells[0].Style.BackColor = Color.Yellow;
            }

            for (int i = 0; i < plant3Grid.Rows.Count; i++)
            {
                if (tmpPlant3[i])
                    plant3Grid.Rows[i].Cells[0].Style.BackColor = Color.Yellow;
            }

            for (int i = 0; i < plant4Grid.Rows.Count; i++)
            {
                if (tmpPlant4[i])
                    plant4Grid.Rows[i].Cells[0].Style.BackColor = Color.Yellow;
            }


        }

        public void findBS()
        {
            Log.WriteLog("(6)~~~~~~~[BM_Reg_001.cs] findBS () =========================> ^^^^^^^^^^^^^^^^^^^^ ");




            curPage = 1;
            firstIndex = 0;
            preEA = 0;
            lastNum = 0;
            listPerPage = 3000;
            curPageLabel.Text = "1";

            // initializeControl("VIEW");
            // clearAllCardDetail();
            showDefaultImage();
            // showCardList(0);
            // name3.Text = commonCardNum3.Text = 

            birthday3.Text = fpServerIP.Text = "";
            fpServerIP.Enabled = false;



            if (commonCardNum3.Text == null || commonCardNum3.Text.Equals(""))
            {
                MessageBox.Show("유효한 카드번호가 아닙니다");
                return;
            }

            getUserIdnFromBS("" + Convert.ToInt64(commonCardNum3.Text.Trim()));

        }


        public void getUserIdnFromBS(string _sCardNo)
        {

            Log.WriteLog("[Reg.cs] getUserIdnFromBS (" + _sCardNo + ")");



            fpServerNum = Convert.ToInt32(getIni("BS_NUM"));


            string query = "select nUserIdn from TB_USER_CARD where sCardNo = '" + _sCardNo + "'";

            string[] fpServerType = { "FP1S", "FP2S", "FP3S", "FP4S" };

            for (int j = 0; j < fpServerNum; j++)
            {
                nUserIdnArr[j].Clear();

                string resultString = req.select("BMS011", query, fpServerType[j]);

                Log.WriteLog("[Reg.cs] getUserIdnFromBS (" + fpServerType[j] + ") query  : " + query);

                if (resultString != "0")
                {
                    MessageBox.Show("[Reg.cs] getUserIdnFromBS () BMServer DB 연결 장애 발생");
                    Log.WriteLog("[Reg.cs] getUserIdnFromBS ()  BMServer DB 연결 장애 발생");

                    return;
                }

                int count = ReturnDT.dt.Rows.Count;

                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {

                        string value = "";

                        value = ReturnDT.dt.Rows[i]["nUserIdn"].ToString();

                        Log.WriteLog("[Reg.cs] getUserIdnFromBS ()  " + (j + 1) + "발전소 nUserIdn (" + i + ") : " + value);

                        nUserIdnArr[j].Add(value);

                    }
                }
                else
                {
                    Log.WriteLog("[Reg.cs] getUserIdnFromBS ()  " + (j + 1) + "발전소에 해당 nUserIdn 이 없습니다");
                }

            }
        }



        private void deleteAllFP_Click(object sender, EventArgs e)
        {
                string query = "";


            fpServerNum = Convert.ToInt32(getIni("BS_NUM"));

            string[] fpServerType = { "FP1I", "FP2I", "FP3I", "FP4I" };

            if (fpServerNum < 1) return;



            Form form = new YesNoForm(commonCardNum3.Text);
            form.ShowDialog();
            form.TopMost = true;
            if (!Gloval.deleteOK) return;

            ///////////////////////////////////////// Valid Check Complete //////////////////////////////////////////////////////

            findBS();


            Gloval.deleteOK = false;

            for (int i = 0; i < fpServerNum; i++)
            {

                if (nUserIdnArr[i].Count < 1) continue;

                for (int j = 0; j < nUserIdnArr[i].Count; j++)
                {
                    string rNumString = "";

                    string curUserIdn = "" + nUserIdnArr[i][j];

                    query = "exec sp_DeleteUserInfo  " + curUserIdn;
                    query = query + " ^ update TB_EXTNL_REQ set sValue = '1'";

                    // query = "delete from TB_EXTNL_REQ";

                    rNumString = req.update("BMS011", query, fpServerType[i]);

                    if (rNumString.Equals("1")) MessageBox.Show((i + 1) + "발전소 카드삭제 실패");

                    Log.WriteLog("[Reg.cs] clickDeleteFP () " + (i + 1) + "발전소의 " + commonCardNum3.Text + "(" + curUserIdn + ") 카드가 삭제되었습니다");


                }

            }

            finger1.Visible = finger2.Visible = finger3.Visible = finger4.Visible = false;
            fpImg.Image = null;
            fpImg.BackColor = Color.Silver;
            drawFinger(0);

            ////////////////////////// ADD FP CNT ///////////////////////////////////////////////
            query = "update BADGE set FP_1 = -1 where bid ='" + commonCardNum3.Text.Trim() + "'";

            Log.WriteLog("[BM_Reg_001.cs] clickScan () fpCnt query : " + query);

            string rCheck = req.update("BMS011", query, "BMI");

            if (rCheck.Equals("0"))
            {
                Log.WriteLog("[BM_Reg_001.cs] clickScan () " + commonCardNum3.Text.Trim() + "'s fpCnt : 0");
                tmpNum.Text = "0";
            }
            else
            {

            }
            ////////////////////////////////////////////////////////////////////////////////////////


            MessageBox.Show("" + commonCardNum3.Text.Trim() + " 카드가  모든 지문서버에서 삭제되었습니다.");
        

        }

        private void searchDepartmentButton_Click(object sender, EventArgs e)
        {
            Bm_SearchCodeForm searchForm = new Bm_SearchCodeForm("Department");
            searchForm.confirmButtonClick_Event_handler += new selectResultCodeIndex(searchForm_confirmButtonClick_Event_handler);
            searchForm.ShowDialog();
        }

        void searchForm_confirmButtonClick_Event_handler(string desc, string codeType)
        {
            if (codeType.Equals("Department"))
            {
                department.SelectedIndex = department.FindString(desc);
            }
            else if (codeType.Equals("Division"))
            {
                division.SelectedIndex = division.FindString(desc);
            }
        }

        private void searchDivisionButton_Click(object sender, EventArgs e)
        {
            Bm_SearchCodeForm searchForm = new Bm_SearchCodeForm("Division");
            searchForm.confirmButtonClick_Event_handler += new selectResultCodeIndex(searchForm_confirmButtonClick_Event_handler);
            searchForm.ShowDialog();
        }

      

        private void department_KeyDown_1(object sender, KeyEventArgs e)
        {
          
        }

        private void division_KeyDown_1(object sender, KeyEventArgs e)
        {
          
        }

        private void searchListBox_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private void searchListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
         
        }


      
    }

}
