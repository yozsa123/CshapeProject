using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Globalization;

using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


using System.Data.OleDb;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using ADOX;



using System.Diagnostics;

using BMClient;





namespace BS_SDK
{
    public partial class FPRegisterForm : Form
    {

        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = null;
        SqlTransaction trx = null;
        Boolean resultFlag = false;
        string set_Path = Application.StartupPath + @"\Hub_Setting.ini";

        byte[] template = new byte[384];
        byte[] template2 = new byte[384];
        byte[] m_TemplateData = new byte[384];

        int result = -1;
        // Boolean aleadyLoad  = false;
        int imageLen = 0;
        int TEMPLATE_SIZE = 384;

        IntPtr ipData = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(BSSDK.BSIPConfig)));
        BSSDK.BSIPConfig fetchIPData = new BSSDK.BSIPConfig();


        IntPtr data = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(BSSDK.BSFingerprintConfig)));
        BSSDK.BSFingerprintConfig fetchFingerData = new BSSDK.BSFingerprintConfig();

        uint cardID = 0;
        int customID = 0;

        BSSDK.BSUserHdrEx[] userHdr = new BSSDK.BSUserHdrEx[10];

        byte[] bitmapImage = null;


        public const int MAX_DEVICE = 5;

        private int m_NumOfDevice = 0;
        private uint[] m_DeviceID;
        private int[] m_DeviceType;
        private uint[] m_DeviceAddr;

        int handle = 0;

        string terminalIP = "";


        string[] dbConString = { "", "", "", "" };


        string bid = "";

        int nFingerIndex = 6;

        Request req = new Request();
        QueryMaker qm = new QueryMaker();

        int fpServerNum = 0;



        public FPRegisterForm(string _ip, string _bid, string _name, int _fingerNum)
        {

            Log.WriteLog("[FPReg.cs]  (" + _ip + _bid + ", " + _name + ", " + _fingerNum);


            Gloval.scanSuccess = false;

            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            string[] fpIP = { "FP1_SERVER_IP", "FP2_SERVER_IP", "FP3_SERVER_IP", "FP4_SERVER_IP" };
            string[] fpDB = { "FP1_DB", "FP2_DB", "FP3_DB", "FP4_DB" };
            string[] fpUser = { "FP1_USER_ID", "FP2_USER_ID", "FP3_USER_ID", "FP4_USER_ID" };
            string[] fpPass = { "FP1_PASS", "FP2_PASS", "FP3_PASS", "FP4_PASS" };

            fpServerNum = Convert.ToInt32(getIni("BS_NUM"));

            for (int i = 0; i < fpServerNum; i++)
            {
                dbConString[i] = "Data Source=" + getIni(fpIP[i]).Trim() + ";Initial Catalog=" + getIni(fpDB[i]).Trim() + ";User ID=" + getIni(fpUser[i]).Trim() + ";Password=" + getIni(fpPass[i]).Trim();

                Log.WriteLog("[FPReg.cs]  (dbConString [" + i + "] : " + dbConString[i]);
            }



            if (_fingerNum == 6) label9.Text = "첫번째 손가락 등록";
            else if (_fingerNum == 7) label9.Text = "두번째 손가락 등록";
            if (_fingerNum == 8) label9.Text = "세번째 손가락 등록";
            else if (_fingerNum == 9) label9.Text = "네번째 손가락 등록";


            terminalIP = _ip;

            this.bidTextBox.Text = _bid;

            bid = _bid;

            this.nameTextBox.Text = _name;


            nFingerIndex = _fingerNum;

            Log.WriteLog("TERMINAL_IP : " + terminalIP);

        }





        public void drawBack(Graphics g)
        {
            Bitmap back = new Bitmap(".\\khnp.png");
            Bitmap img2 = ResizeBitmap(ChangeOpacity(back, .08F), 168, 20);

            TextureBrush brush = new TextureBrush(img2, WrapMode.Tile);
            g.FillRectangle(brush, 0, 0, this.Width, this.Height);


        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            drawBack(g);

        }


        private void scan(object sender, EventArgs e)
        {

            Log.WriteLog("scan () Start !!");


            button1.Enabled = false;

            pictureBox1.Image = null;
            this.label1.Text = "";




            closeSocket();



            Refresh();


            if (Gloval.aleadyLoad == false)
            {
                initSDK();
                if (result != 0)
                {
                    endService();
                    Gloval.template[0] = 0;
                    MessageBox.Show("창을 모두 닫은후 다시 로그인해 주세요");
                    return;
                }
            }



            Gloval.aleadyLoad = true;


            openSocket();

            if (result != 0)
            {
                endService();
                return;
            }

            disable();
            if (result != 0) return;


            /*
            this.label1.Text = "카드를 대세요^^";
            Refresh();


            readCard();
            // disable();
            */
            this.label1.Text = "손가락을 대세요^^";



            Refresh();



            readTemplate();

            if (result != 0)
            {
                endService();
                return;
            }



            if (result == 0)
            {
                readImage();

                if (result == 0)
                {

                    this.label1.Text = "감사합니다^^";
                    // this.label1.Text = "지문 스캔 성공^^";
                    byte[] fetchImage = new byte[imageLen];
                    for (int i = 0; i < imageLen; i++) fetchImage[i] = bitmapImage[i];
                    MemoryStream imagestream = new MemoryStream(fetchImage);
                    Bitmap bmp = new Bitmap(imagestream);

                    Bitmap newBitmap = ResizeBitmap(bmp, pictureBox1.Width, pictureBox1.Height);

                    pictureBox1.Image = ChangeOpacity(newBitmap, 0.6F);

                    Gloval.template = new byte[384];
                    Buffer.BlockCopy(template, 0, Gloval.template, 0, TEMPLATE_SIZE);

                    Gloval.template2 = new byte[384];
                    Buffer.BlockCopy(template2, 0, Gloval.template2, 0, TEMPLATE_SIZE);


                    Gloval.scanSuccess = true;

                }
                else
                {
                    this.label1.Text = "지문 스캔 실패ㅠㅠ";
                    Gloval.scanSuccess = false;

                }

                if (Gloval.scanSuccess == false || Gloval.template[0] == 0)
                {
                    Gloval.scanSuccess = false;
                    MessageBox.Show("[TemplateDB.cs] (지문미등록자) 지문이 정상적으로 스캔되지 않았습니다");
                    return;
                }
                if (Gloval.scanSuccess)
                {
                    for (int i = 0; i < fpServerNum; i++) insertDB(i);

                }

                this.Show();
                Refresh();



                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                Thread.Sleep(1000);
                stopwatch.Stop();


            }

            endService();



        }

        // JSJ 0804 GR
        public void insertDB(int _fpServerNum)
        {

            Log.WriteLog("[FPRegisterForm.cs] insertDB () START ==================================================");

            int nIndex = 1;

            string ip = "";

            if (nFingerIndex == 6) nIndex = 1;
            else if (nFingerIndex == 7) nIndex = 2;
            else if (nFingerIndex == 8) nIndex = 3;
            else if (nFingerIndex == 9) nIndex = 4;


            conn.ConnectionString = dbConString[_fpServerNum];


            resultFlag = false;
            closeConnection();

            resultFlag = openConnection();

            if (!resultFlag)
            {
                MessageBox.Show("[FPRegisterForm.cs] 지문서버 DB Conneciton 실패 : " + conn.ConnectionString);
                return;
            }


            try
            {
                cmd = new SqlCommand("GuardTemplate", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@sCardNo", Convert.ToInt64(bid) + "");
                Log.WriteLog("\t (1) sCardNo   : " + Convert.ToInt64(bid));


                SqlParameter nFingerIndexTmp = new SqlParameter("@nFingerIndexTmp", SqlDbType.Int);
                cmd.Parameters.Add(nFingerIndexTmp);
                nFingerIndexTmp.Direction = ParameterDirection.Input;
                nFingerIndexTmp.Value = nFingerIndex;
                Log.WriteLog("\t (2) nFingerIndex   : " + nFingerIndex);



                SqlParameter nIndexTmp = new SqlParameter("@nIndexTmp", SqlDbType.Int);
                cmd.Parameters.Add(nIndexTmp);
                nIndexTmp.Direction = ParameterDirection.Input;
                nIndexTmp.Value = nIndex;
                Log.WriteLog("\t (3) nIndex         : " + nIndex);


                SqlParameter templateTmp = new SqlParameter("@templateTmp", SqlDbType.Binary);
                cmd.Parameters.Add(templateTmp);
                templateTmp.Direction = ParameterDirection.Input;
                templateTmp.Value = Gloval.template;

                SqlParameter templateTmp2 = new SqlParameter("@templateTmp2", SqlDbType.Binary);
                cmd.Parameters.Add(templateTmp2);
                templateTmp2.Direction = ParameterDirection.Input;
                templateTmp2.Value = Gloval.template2;



                cmd.ExecuteNonQuery();
                Log.WriteLog("..............................(3) .........................");



                string Check = req.update("BMS0011", qm.getQuery("FP_REG_LOG", bidTextBox.Text), "BMI");
                if (!Check.Equals("0")) MessageBox.Show("로그 저장 실패..");

            }
            catch (SqlException se)
            {
                Log.WriteLog("[FPRegisterForm] insertDB (" + _fpServerNum + ") Exception : " + se.Message + ")");
                MessageBox.Show("" + (_fpServerNum + 1) + "발전소 지문등록이 실패했습니다");
            }
            finally
            {
                closeConnection();
            }


        }

        public Boolean openConnection()
        {

            try
            {
                conn.Open();
                Log.WriteLog("[FPRegisterForm.cs] (1) dbConnection SUCCESS ");
                return true;
            }
            catch (Exception e2)
            {
                Log.WriteLog("[FPRegisterForm.cs] (1) dbConnection FAILED ");
                conn.Close();
                return false;
            }

        }

        public void closeConnection()
        {
            try
            {
                conn.Close();
                Log.WriteLog("[FPRegisterForm.cs] (1) dbClose SUCCESS ^^");

            }
            catch (Exception e)
            {
                Log.WriteLog("[FPRegisterForm.cs] (1) dbClose FAILED ㅠㅠ ");
                MessageBox.Show(e.Message);
                conn.Close();

            }
            finally
            {
                conn.Close();
            }



        }


        private void register(object sender, EventArgs e)
        {

            this.Close();

        }



        public void closeSocket()
        {
            result = BSSDK.BS_CloseSocket(handle);
        }

        public void initSDK()
        {
            // Log.WriteLog(">>>>>>>>>>>>>>>>>>>>> BSSDK.BS_InitSDK() START");
            BSSDK.BS_InitSDK();
            Log.WriteLog(">>>>>>>>>>>>>>>>>>>>> BSSDK.BS_InitSDK()...result : " + result);
            Log.WriteLog("\n\n");
        }

        public void endService()
        {


            result = BSSDK.BS_Enable(handle);
            result = BSSDK.BS_CloseSocket(handle);
            // this.label1.Text = "";
            button1.Enabled = true;
            Refresh();
            this.Close();
            // return;

        }

        public void openUDP()
        {
            result = BSSDK.BS_OpenInternalUDP(ref handle);
            Log.WriteLog(">>>>>>>>>>>>>>>>>>>>> BSSDK.BS_OpenInternalUDP()...result : " + result);
            Log.WriteLog("\n\n");
        }

        public void searchLan()
        {
            result = BSSDK.BS_SearchDeviceInLAN(handle, ref m_NumOfDevice, m_DeviceID, m_DeviceType, m_DeviceAddr);
            // if (result != 0) return;

            Log.WriteLog(">>>>>>>>>>>>>>>>>>>>> (3) BS_SearchDeviceInLAN().... result : " + result);
            Log.WriteLog("                        m_DeviceID   : " + m_DeviceID[0]);
            Log.WriteLog("                        m_DeviceType : " + m_DeviceType[0]);
            Log.WriteLog("                        m_DeviceAddr : " + m_DeviceAddr[0]);
            Log.WriteLog("\n\n");

        }

        public void openSocket()
        {
            Log.WriteLog(">>>>>>>>>>>>>>>>>>>>>  BS_OpenSocket() START!!");

            result = BSSDK.BS_OpenSocket(terminalIP, 1470, ref handle);
            if (result != 0) return;
            Log.WriteLog(">>>>>>>>>>>>>>>>>>>>> (4) BS_OpenSocket().... result : " + result);
            Log.WriteLog(".................... AFTER  handle : " + handle);
            Log.WriteLog("\n\n");

        }

        public void setDeviceID()
        {
            uint deviceID = 55615;
            result = BSSDK.BS_SetDeviceID(handle, deviceID, 0);
            // result = BSSDK.BS_SetDeviceID(handle, deviceID, 0);
            Log.WriteLog(">>>>>>>>>>>>>>>>>>>> (5) BS_SetDeviceID().... result : " + result);
            Log.WriteLog("\n\n");

        }

        public void getDeviceID()
        {
            uint[] deviceIDTmp = new uint[MAX_DEVICE]; ;
            int deviceTypeTmp = 0;

            result = BSSDK.BS_GetDeviceID(handle, deviceIDTmp, ref deviceTypeTmp);

            Log.WriteLog(">>>>>>>>>>>>>>>>>>>>> BS_GetDeviceI().... result : " + result);
            Log.WriteLog(">>>>>>>>>>>>>>>>>>>>> deviceID : " + deviceIDTmp[0] + ",   deviceType : " + deviceTypeTmp);
            Log.WriteLog("\n\n");
            if (result != 0) return;
        }

        public void deleteUser()
        {
            Log.WriteLog("=========== BEFORE : " + handle);
            result = BSSDK.BS_DeleteUser(handle, 3);
            Log.WriteLog(">>>>>>>>>>>>>>> [tmp] BS_DeleteUser(3).... result : " + result);
            Log.WriteLog("=========== AFTER  : " + handle);
            Log.WriteLog("\n\n");


        }

        public void disable()
        {
            BSSDK.BS_Disable(handle, 20);
            Log.WriteLog(">>>>>>>>>>>>>>>>>>>>> (6) BS_Disable ().... result : " + result);
            Log.WriteLog("\n\n");
        }

        public void enable()
        {
            result = BSSDK.BS_Enable(handle);
            Log.WriteLog(">>>>>>>>>>>>>>>>>>>>> (6-2) BS_Enable ().... result : " + result);
            Log.WriteLog("\n\n");

        }

        public void reset()
        {
            result = BSSDK.BS_Reset(handle);
            Log.WriteLog(">>>>>>>>>>>>>>>>>>>>> (7) BS_Reset ().... result : " + result);
            Log.WriteLog("\n\n");

        }

        public void readIPConfig()
        {
            /////////////////////////////////////// <0> READ IPConfig ////////////////////////////////////////////////////////////////////


            // ipData = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(BSSDK.BSIPConfig)));
            result = BSSDK.BS_ReadIPConfig(handle, ipData);

            Log.WriteLog(">>>>>>>>>>>>>>>>>>>>> (0) BS_ReadIPConfig ().... result : " + result);

            Log.WriteLog("\n\n=================== [BioStar.cs] READ Network ....  FP Terminal ==> PC =====================");
            fetchIPData = (BSSDK.BSIPConfig)Marshal.PtrToStructure(ipData, typeof(BSSDK.BSIPConfig));
            Log.WriteLog("lanType           = " + fetchIPData.lanType);
            Log.WriteLog("useDHCP           = " + fetchIPData.useDHCP);
            Log.WriteLog("port              = " + fetchIPData.port);


            Log.WriteLog("ipAddr            = " + getB2S(fetchIPData.ipAddr));
            Log.WriteLog("gateWay           = " + getB2S(fetchIPData.gateway));
            Log.WriteLog("subnetMask        = " + getB2S(fetchIPData.subnetMask));
            Log.WriteLog("serverIP          = " + getB2S(fetchIPData.serverIP));


            Log.WriteLog("maxConnection     = " + fetchIPData.maxConnection);
            Log.WriteLog("useServer         = " + (fetchIPData.useServer).ToString());
            Log.WriteLog("serverPort        = " + fetchIPData.serverPort);
            Log.WriteLog("syncTimeWithServer= " + fetchIPData.syncTimeWithServer);
            Log.WriteLog("reserved          = " + getB2S(fetchIPData.reserved));


        }

        public void writeIPConfig()
        {
            /////////////////////////////////////// <0_0> Write IPConfig ////////////////////////////////////////////////////////////////////

            fetchIPData.useServer = true;

            // IMPORTANT  1 로 해놓으면  BioStar 서버 프로그램에서 장치추가 되면 SDK는 쓰질 못하게 됨..
            fetchIPData.maxConnection = 4;



            IntPtr sendIPData = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(BSSDK.BSIPConfig)));
            Marshal.StructureToPtr(fetchIPData, sendIPData, true);

            result = BSSDK.BS_WriteIPConfig(handle, sendIPData);

            Log.WriteLog(">>>>>>>>>>>>>>>>>>>>> (0-1) Write Network  BS_WriteIPConfig ().... result : " + result);


        }


        public void readFingerPrintConfig()
        {
            /////////////////////////////////////// <1> READ FingerPrintConfig ////////////////////////////////////////////////////////////////////


            data = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(BSSDK.BSFingerprintConfig)));
            // IntPtr data = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(BSSDK.BEConfigData)));
            result = BSSDK.BS_ReadFingerprintConfig(handle, data);
            // if (result != 0) return;
            // string hexValue = result.ToString("X");
            // Log.WriteLog(">>>>>>>>>>>>>>>>>>>>> (6) BS_ReadFingerprintConfig ().... result : " + result + ", " + hexValue);
            Log.WriteLog(">>>>>>>>>>>>>>>>>>>>> (8) BS_ReadFingerprintConfig ().... result : " + result);
            Log.WriteLog("     BSSDK.BS_SUCCESS : " + BSSDK.BS_SUCCESS);
            Log.WriteLog("\n\n=================== [BioStar.cs] READ FP ..... FP Terminal ==> PC =====================");
            fetchFingerData = (BSSDK.BSFingerprintConfig)Marshal.PtrToStructure(data, typeof(BSSDK.BSFingerprintConfig));
            Log.WriteLog("security         = " + fetchFingerData.security);
            Log.WriteLog("userSecurity     = " + fetchFingerData.userSecurity);
            Log.WriteLog("fastMode         = " + fetchFingerData.fastMode);
            Log.WriteLog("sensitivity      = " + fetchFingerData.sensitivity);
            Log.WriteLog("timeout          = " + fetchFingerData.timeout);
            Log.WriteLog("imageQuality     = " + fetchFingerData.imageQuality);
            Log.WriteLog("viewImage         = " + fetchFingerData.viewImage);
            Log.WriteLog("freeScanDelay     = " + fetchFingerData.freeScanDelay);
            Log.WriteLog("useCheckDuplicate = " + fetchFingerData.useCheckDuplicate);
            Log.WriteLog("matchTimeout      = " + fetchFingerData.matchTimeout);
            Log.WriteLog("useSIF            = " + fetchFingerData.useSIF);
            Log.WriteLog("useFakeDetect     = " + fetchFingerData.useFakeDetect);
            Log.WriteLog("useServerMatching = " + fetchFingerData.useServerMatching);

            // Log.WriteLog("............................... SIZE : " + Marshal.SizeOf(typeof(BSSDK.BSFingerprintConfig)));

        }

        public void writeFingerPrintConfig()
        {

            fetchFingerData.matchTimeout = 10;
            fetchFingerData.timeout = 5;
            fetchFingerData.useServerMatching = true;


            IntPtr sendData = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(BSSDK.BSFingerprintConfig)));
            Marshal.StructureToPtr(fetchFingerData, sendData, true);

            result = BSSDK.BS_WriteFingerprintConfig(handle, sendData);

            Log.WriteLog(">>>>>>>>>>>>>>>>>>>>> (9) Writer FP.... result : " + result);


        }

        public void readCard()
        {
            IntPtr hdr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(BSSDK.BSUserHdrEx)));

            Log.WriteLog("[BioStar.cs] READ Card START ");

            result = BSSDK.BS_ReadCardIDEx(handle, ref cardID, ref customID);

            Log.WriteLog(">>>>>>>>>>>>>>>>>>>>> (0) BS_ReadICardIDEx ().... result : " + result);

            Log.WriteLog("\n\n=================== [BioStar.cs] READ Card =====================");

            Log.WriteLog("cardID           = " + cardID);
            Log.WriteLog("customID         = " + customID);

            this.bidTextBox.Text = "" + cardID;
            this.nameTextBox.Text = "일시발전_" + cardID;


        }

        public void readTemplate()
        {
          
            Log.WriteLog("......................... FP START ............");
           
            result = BSSDK.BS_ScanTemplate(handle, template);
            Buffer.BlockCopy(template, 0, m_TemplateData, 0, TEMPLATE_SIZE);

            if (result != 0) return;

            this.label1.Text = "한번 더 대세요^^";

            Refresh();

            result = BSSDK.BS_ScanTemplate(handle, template2);
            // Buffer.BlockCopy(template, 0, m_TemplateData, TEMPLATE_SIZE, TEMPLATE_SIZE);
            Buffer.BlockCopy(template2, 0, m_TemplateData, 0, TEMPLATE_SIZE);

            if (result == 0) Log.WriteLog("............ FP SUCCESS ........... SIZE : " + template.Length);
            else Log.WriteLog("................... FP FAILED ...........................");

        }

        public void readImage()
        {

            int imageType = 0;
            bitmapImage = new byte[100 * 1024];

            // for (int i = 0; i < 10; i++) Log.WriteLog("template [" + i + "] " + bitmapImage[i]);
            result = BSSDK.BS_ReadImage(handle, imageType, bitmapImage, ref imageLen);
            // for (int i = 0; i < 20; i++) Log.WriteLog("template [" + i + "] " + bitmapImage[i]);
            Log.WriteLog("imageLen : " + imageLen);
            Log.WriteLog(">>>>>>>>>>>>>>>>>>>>> BS_ReadImage ().... result : " + result);
            Log.WriteLog("\n\n");


        }

        public void getAllUser()
        {


            IntPtr userInfo = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(BSSDK.BSUserHdrEx)));
            byte[] m_TemplateData = new byte[TEMPLATE_SIZE * 2 * 2];
            // result = BSSDK.BS_GetUserEx(handle, 4, userInfo, m_TemplateData);


            int m_NumOfUser = 0;
            int m_Handle = 0;
            int m_NumOfTemplate = 0;


            result = BSSDK.BS_GetAllUserInfoEx(handle, userInfo, ref m_NumOfUser);


            // result = BSSDK.BS_GetUserDBInfo(m_Handle, ref m_NumOfUser, ref m_NumOfTemplate);

            Log.WriteLog(">>>>>>>>>>>>>>>>>>>>> (91) GET All  User  result : " + result);
            Log.WriteLog("     m_NumOfUser : " + m_NumOfUser);
            Log.WriteLog("     m_NumOfTemplate : " + m_NumOfTemplate);


            for (int i = 0; i < 1; i++)
            {
                userHdr[i] = (BSSDK.BSUserHdrEx)Marshal.PtrToStructure(new IntPtr(userInfo.ToInt32() + i * Marshal.SizeOf(typeof(BSSDK.BSUserHdrEx))), typeof(BSSDK.BSUserHdrEx));
                Log.WriteLog("name          : " + getB2S(userHdr[i].name));
                Log.WriteLog("department    : " + getB2S(userHdr[i].department));
                Log.WriteLog("password      : " + getB2S(userHdr[i].password));
                Log.WriteLog("ID            : " + userHdr[i].ID);
                Log.WriteLog("cardID        : " + userHdr[i].cardID);
                Log.WriteLog("headerVersion : " + userHdr[i].headerVersion);
                Log.WriteLog("adminLevel    : " + userHdr[i].adminLevel);
                Log.WriteLog("securityLevel : " + userHdr[i].securityLevel);
                Log.WriteLog("statusMask    : " + userHdr[i].statusMask);
                Log.WriteLog("accessGM      : " + userHdr[i].accessGroupMask);
                Log.WriteLog("numOfFinger   : " + userHdr[i].numOfFinger);
                Log.WriteLog("duressMask    : " + userHdr[i].duressMask);
                Log.WriteLog("authMod       : " + userHdr[i].authMode);
                Log.WriteLog("authLimitCount: " + userHdr[i].authLimitCount);
                Log.WriteLog("timeAntiPB    : " + userHdr[i].timedAntiPassback);
                Log.WriteLog("startDateTime : " + userHdr[i].startDateTime);
                Log.WriteLog("expireDateTime: " + userHdr[i].expireDateTime);
                Log.WriteLog("version       : " + userHdr[i].version);
                Log.WriteLog("headerVersion : " + userHdr[i].headerVersion);
                Log.WriteLog("disabled      : " + userHdr[i].disabled);
                Log.WriteLog("bypassCard    : " + userHdr[i].bypassCard);
                Log.WriteLog("checksum      : " + userHdr[i].checksum[0]);
                Log.WriteLog("customID      : " + userHdr[i].customID + "\n\n\n");

            }

        }


        public void enrollUser()
        {


            userHdr[0].ID = 3;
            userHdr[0].cardID = cardID;

            IntPtr usrData = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(BSSDK.BSUserHdrEx)));
            // Marshal.StructureToPtr(sendHdr, usrData, true);
            Marshal.StructureToPtr(userHdr[0], usrData, true);


            Log.WriteLog("adminLevel : " + userHdr[0].adminLevel);
            result = BSSDK.BS_EnrollUserEx(handle, usrData, template);

            Log.WriteLog(">>>>>>>>>>>>>>>>>>>>> (91) Enroll User FP.... result : " + result);


        }



        public string getB2S(byte[] input)
        {
            UTF8Encoding enc = new UTF8Encoding();
            string str = enc.GetString(input).Trim();
            return str;
        }



        public Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {

            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result)) g.DrawImage(sourceBMP, 0, 0, width, height);
            return result;

        }





        public static Bitmap ChangeOpacity(Image img, float opacityvalue)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height); // Determining Width and Height of Source Image
            Graphics graphics = Graphics.FromImage(bmp);
            ColorMatrix colormatrix = new ColorMatrix();
            colormatrix.Matrix33 = opacityvalue;
            ImageAttributes imgAttribute = new ImageAttributes();
            imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);
            graphics.Dispose();   // Releasing all resource used by graphics 
            return bmp;
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



    }
}

