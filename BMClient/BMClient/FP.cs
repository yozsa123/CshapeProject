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

using BS_SDK;

using System.Diagnostics;
using System.Threading;


namespace BMClient
{
    class FP
    {

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

        public FP(string ip)
        {
            WriteLog("[FP.cs] START..................................");
            terminalIP = ip;
        }

        public void readBid ()
        {


            WriteLog("scan () Start !!");
            closeSocket();

            initSDK();
            if (result != 0) { endService(); return; }


            openSocket();
            if (result != 0) { endService(); return; }

            disable();
            if (result != 0) { endService(); return; }


            readCard();

           
            endService();

        }

        public void closeSocket()
        {
            result = BSSDK.BS_CloseSocket(handle);
        }

        public void initSDK()
        {
            // WriteLog(">>>>>>>>>>>>>>>>>>>>> BSSDK.BS_InitSDK() START");
            BSSDK.BS_InitSDK();
            WriteLog(">>>>>>>>>>>>>>>>>>>>> BSSDK.BS_InitSDK()...result : " + result);
            WriteLog("\n\n");
        }

        public void endService()
        {

            result = BSSDK.BS_Enable(handle);
            result = BSSDK.BS_CloseSocket(handle);
            
        }

        public void openUDP()
        {
            result = BSSDK.BS_OpenInternalUDP(ref handle);
            WriteLog(">>>>>>>>>>>>>>>>>>>>> BSSDK.BS_OpenInternalUDP()...result : " + result);
            WriteLog("\n\n");
        }

        public void searchLan()
        {
            result = BSSDK.BS_SearchDeviceInLAN(handle, ref m_NumOfDevice, m_DeviceID, m_DeviceType, m_DeviceAddr);
            // if (result != 0) return;

            WriteLog(">>>>>>>>>>>>>>>>>>>>> (3) BS_SearchDeviceInLAN().... result : " + result);
            WriteLog("                        m_DeviceID   : " + m_DeviceID[0]);
            WriteLog("                        m_DeviceType : " + m_DeviceType[0]);
            WriteLog("                        m_DeviceAddr : " + m_DeviceAddr[0]);
            WriteLog("\n\n");

        }

        public void openSocket()
        {
            WriteLog(">>>>>>>>>>>>>>>>>>>>>  BS_OpenSocket() START!!");

            result = BSSDK.BS_OpenSocket(terminalIP, 1470, ref handle);
            if (result != 0) return;
            WriteLog(">>>>>>>>>>>>>>>>>>>>> (4) BS_OpenSocket().... result : " + result);
            WriteLog(".................... AFTER  handle : " + handle);
            WriteLog("\n\n");

        }

        public void setDeviceID()
        {
            uint deviceID = 55615;
            result = BSSDK.BS_SetDeviceID(handle, deviceID, 0);
            // result = BSSDK.BS_SetDeviceID(handle, deviceID, 0);
            WriteLog(">>>>>>>>>>>>>>>>>>>> (5) BS_SetDeviceID().... result : " + result);
            WriteLog("\n\n");

        }

        public void getDeviceID()
        {
            uint[] deviceIDTmp = new uint[MAX_DEVICE]; ;
            int deviceTypeTmp = 0;

            result = BSSDK.BS_GetDeviceID(handle, deviceIDTmp, ref deviceTypeTmp);

            WriteLog(">>>>>>>>>>>>>>>>>>>>> BS_GetDeviceI().... result : " + result);
            WriteLog(">>>>>>>>>>>>>>>>>>>>> deviceID : " + deviceIDTmp[0] + ",   deviceType : " + deviceTypeTmp);
            WriteLog("\n\n");
            if (result != 0) return;
        }

        public void deleteUser()
        {
            WriteLog("=========== BEFORE : " + handle);
            result = BSSDK.BS_DeleteUser(handle, 3);
            WriteLog(">>>>>>>>>>>>>>> [tmp] BS_DeleteUser(3).... result : " + result);
            WriteLog("=========== AFTER  : " + handle);
            WriteLog("\n\n");


        }

        public void disable()
        {
            BSSDK.BS_Disable(handle, 20);
            WriteLog(">>>>>>>>>>>>>>>>>>>>> (6) BS_Disable ().... result : " + result);
            WriteLog("\n\n");
        }

        public void enable()
        {
            result = BSSDK.BS_Enable(handle);
            WriteLog(">>>>>>>>>>>>>>>>>>>>> (6-2) BS_Enable ().... result : " + result);
            WriteLog("\n\n");

        }

        public void reset()
        {
            result = BSSDK.BS_Reset(handle);
            WriteLog(">>>>>>>>>>>>>>>>>>>>> (7) BS_Reset ().... result : " + result);
            WriteLog("\n\n");

        }

        public void readIPConfig()
        {
            /////////////////////////////////////// <0> READ IPConfig ////////////////////////////////////////////////////////////////////


            // ipData = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(BSSDK.BSIPConfig)));
            result = BSSDK.BS_ReadIPConfig(handle, ipData);

            WriteLog(">>>>>>>>>>>>>>>>>>>>> (0) BS_ReadIPConfig ().... result : " + result);

            WriteLog("\n\n=================== [BioStar.cs] READ Network ....  FP Terminal ==> PC =====================");
            fetchIPData = (BSSDK.BSIPConfig)Marshal.PtrToStructure(ipData, typeof(BSSDK.BSIPConfig));
            WriteLog("lanType           = " + fetchIPData.lanType);
            WriteLog("useDHCP           = " + fetchIPData.useDHCP);
            WriteLog("port              = " + fetchIPData.port);


            WriteLog("ipAddr            = " + getB2S(fetchIPData.ipAddr));
            WriteLog("gateWay           = " + getB2S(fetchIPData.gateway));
            WriteLog("subnetMask        = " + getB2S(fetchIPData.subnetMask));
            WriteLog("serverIP          = " + getB2S(fetchIPData.serverIP));


            WriteLog("maxConnection     = " + fetchIPData.maxConnection);
            WriteLog("useServer         = " + (fetchIPData.useServer).ToString());
            WriteLog("serverPort        = " + fetchIPData.serverPort);
            WriteLog("syncTimeWithServer= " + fetchIPData.syncTimeWithServer);
            WriteLog("reserved          = " + getB2S(fetchIPData.reserved));


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

            WriteLog(">>>>>>>>>>>>>>>>>>>>> (0-1) Write Network  BS_WriteIPConfig ().... result : " + result);


        }


        public void readFingerPrintConfig()
        {
            /////////////////////////////////////// <1> READ FingerPrintConfig ////////////////////////////////////////////////////////////////////


            data = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(BSSDK.BSFingerprintConfig)));
            // IntPtr data = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(BSSDK.BEConfigData)));
            result = BSSDK.BS_ReadFingerprintConfig(handle, data);
            // if (result != 0) return;
            // string hexValue = result.ToString("X");
            // WriteLog(">>>>>>>>>>>>>>>>>>>>> (6) BS_ReadFingerprintConfig ().... result : " + result + ", " + hexValue);
            WriteLog(">>>>>>>>>>>>>>>>>>>>> (8) BS_ReadFingerprintConfig ().... result : " + result);
            WriteLog("     BSSDK.BS_SUCCESS : " + BSSDK.BS_SUCCESS);
            WriteLog("\n\n=================== [BioStar.cs] READ FP ..... FP Terminal ==> PC =====================");
            fetchFingerData = (BSSDK.BSFingerprintConfig)Marshal.PtrToStructure(data, typeof(BSSDK.BSFingerprintConfig));
            WriteLog("security         = " + fetchFingerData.security);
            WriteLog("userSecurity     = " + fetchFingerData.userSecurity);
            WriteLog("fastMode         = " + fetchFingerData.fastMode);
            WriteLog("sensitivity      = " + fetchFingerData.sensitivity);
            WriteLog("timeout          = " + fetchFingerData.timeout);
            WriteLog("imageQuality     = " + fetchFingerData.imageQuality);
            WriteLog("viewImage         = " + fetchFingerData.viewImage);
            WriteLog("freeScanDelay     = " + fetchFingerData.freeScanDelay);
            WriteLog("useCheckDuplicate = " + fetchFingerData.useCheckDuplicate);
            WriteLog("matchTimeout      = " + fetchFingerData.matchTimeout);
            WriteLog("useSIF            = " + fetchFingerData.useSIF);
            WriteLog("useFakeDetect     = " + fetchFingerData.useFakeDetect);
            WriteLog("useServerMatching = " + fetchFingerData.useServerMatching);

            // WriteLog("............................... SIZE : " + Marshal.SizeOf(typeof(BSSDK.BSFingerprintConfig)));

        }

        public void writeFingerPrintConfig()
        {

            fetchFingerData.matchTimeout = 10;
            fetchFingerData.timeout = 5;
            fetchFingerData.useServerMatching = true;


            IntPtr sendData = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(BSSDK.BSFingerprintConfig)));
            Marshal.StructureToPtr(fetchFingerData, sendData, true);

            result = BSSDK.BS_WriteFingerprintConfig(handle, sendData);

            WriteLog(">>>>>>>>>>>>>>>>>>>>> (9) Writer FP.... result : " + result);


        }

        public void readCard()
        {
            IntPtr hdr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(BSSDK.BSUserHdrEx)));

            WriteLog("[FP.cs] READ Card START ");

            result = BSSDK.BS_ReadCardIDEx(handle, ref cardID, ref customID);

            WriteLog(">>>>>>>>>>>>>>>>>>>>> (0) BS_ReadICardIDEx ().... result : " + result);

            WriteLog("\n\n=================== [FP.cs] READ Card =====================");

            WriteLog("cardID           = " + cardID);
            WriteLog("customID         = " + customID);

            Gloval.readBid = "" + cardID;

        }

        public void readTemplate()
        {
            // int TEMPLATE_SIZE = 384;

            // button1.Enabled = false;
            WriteLog("......................... FP START ............");
            // for (int i = 0; i < 10; i++) WriteLog ("template [" + i + "] " + templateData [i]);


            result = BSSDK.BS_ScanTemplate(handle, template);
            Buffer.BlockCopy(template, 0, m_TemplateData, 0, TEMPLATE_SIZE);

            if (result != 0) return;

            // JSJ
            // this.label1.Text = "한번 더 대세요^^";
            // Refresh();

            result = BSSDK.BS_ScanTemplate(handle, template2);
            // Buffer.BlockCopy(template, 0, m_TemplateData, TEMPLATE_SIZE, TEMPLATE_SIZE);
            Buffer.BlockCopy(template2, 0, m_TemplateData, 0, TEMPLATE_SIZE);

            if (result == 0) WriteLog("............ FP SUCCESS ........... SIZE : " + template.Length);
            else WriteLog("................... FP FAILED ...........................");

        }

        public void readImage()
        {

            int imageType = 0;
            bitmapImage = new byte[100 * 1024];

            // for (int i = 0; i < 10; i++) WriteLog("template [" + i + "] " + bitmapImage[i]);
            result = BSSDK.BS_ReadImage(handle, imageType, bitmapImage, ref imageLen);
            // for (int i = 0; i < 20; i++) WriteLog("template [" + i + "] " + bitmapImage[i]);
            WriteLog("imageLen : " + imageLen);
            WriteLog(">>>>>>>>>>>>>>>>>>>>> BS_ReadImage ().... result : " + result);
            WriteLog("\n\n");


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

            WriteLog(">>>>>>>>>>>>>>>>>>>>> (91) GET All  User  result : " + result);
            WriteLog("     m_NumOfUser : " + m_NumOfUser);
            WriteLog("     m_NumOfTemplate : " + m_NumOfTemplate);


            for (int i = 0; i < 1; i++)
            {
                userHdr[i] = (BSSDK.BSUserHdrEx)Marshal.PtrToStructure(new IntPtr(userInfo.ToInt32() + i * Marshal.SizeOf(typeof(BSSDK.BSUserHdrEx))), typeof(BSSDK.BSUserHdrEx));
                WriteLog("name          : " + getB2S(userHdr[i].name));
                WriteLog("department    : " + getB2S(userHdr[i].department));
                WriteLog("password      : " + getB2S(userHdr[i].password));
                WriteLog("ID            : " + userHdr[i].ID);
                WriteLog("cardID        : " + userHdr[i].cardID);
                WriteLog("headerVersion : " + userHdr[i].headerVersion);
                WriteLog("adminLevel    : " + userHdr[i].adminLevel);
                WriteLog("securityLevel : " + userHdr[i].securityLevel);
                WriteLog("statusMask    : " + userHdr[i].statusMask);
                WriteLog("accessGM      : " + userHdr[i].accessGroupMask);
                WriteLog("numOfFinger   : " + userHdr[i].numOfFinger);
                WriteLog("duressMask    : " + userHdr[i].duressMask);
                WriteLog("authMod       : " + userHdr[i].authMode);
                WriteLog("authLimitCount: " + userHdr[i].authLimitCount);
                WriteLog("timeAntiPB    : " + userHdr[i].timedAntiPassback);
                WriteLog("startDateTime : " + userHdr[i].startDateTime);
                WriteLog("expireDateTime: " + userHdr[i].expireDateTime);
                WriteLog("version       : " + userHdr[i].version);
                WriteLog("headerVersion : " + userHdr[i].headerVersion);
                WriteLog("disabled      : " + userHdr[i].disabled);
                WriteLog("bypassCard    : " + userHdr[i].bypassCard);
                WriteLog("checksum      : " + userHdr[i].checksum[0]);
                WriteLog("customID      : " + userHdr[i].customID + "\n\n\n");

            }

        }


        public void enrollUser()
        {
            // BSSDK.BSUserHdrEx sendHdr = new BSSDK.BSUserHdrEx();

            // userHdr[0].ID = 2;

            /*
            sendHdr.ID              = 2;
            // sendHdr.startDateTime = 0; //  946684800;
            // sendHdr.expireDateTime = 0; //  1924988400;
            sendHdr.adminLevel      = BSSDK.BS_USER_NORMAL;
            sendHdr.securityLevel   = BSSDK.BS_USER_SECURITY_DEFAULT;
            sendHdr.authMode        = BSSDK.BS_AUTH_MODE_DISABLED;
            sendHdr.accessGroupMask = 4294967294;
            sendHdr.statusMask      = 0;
            sendHdr.timedAntiPassback = 0;
            sendHdr.duressMask       = 0;
            sendHdr.version = 21;
            
            string tmp = "한수원_영광";
            sendHdr.name = new byte[33];
            System.Buffer.BlockCopy(tmp.ToCharArray(), 0, sendHdr.name, 0, tmp.Length);
                       
            tmp = "본부";
            sendHdr.department = new byte[33];
            System.Buffer.BlockCopy(tmp.ToCharArray(), 0, sendHdr.department, 0, tmp.Length);

            // tmp = "패스워드";
            // sendHdr.password = new byte[17];
            // System.Buffer.BlockCopy(tmp.ToCharArray(), 0, sendHdr.password, 0, tmp.Length);
            
            sendHdr.bypassCard      = 0;
            sendHdr.numOfFinger     = 1;
            sendHdr.cardID          = cardID;
            sendHdr.customID        = customID;

            WriteLog("numOfFinger  = " + sendHdr.numOfFinger);
            WriteLog("cardID        = " + sendHdr.cardID);
            WriteLog("customID      = " + sendHdr.customID);

            for (int i = 0; i < 5; i++) WriteLog("templateData [" + i + "] " + templateData[i]);
            */


            userHdr[0].ID = 3;
            userHdr[0].cardID = cardID;

            IntPtr usrData = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(BSSDK.BSUserHdrEx)));
            // Marshal.StructureToPtr(sendHdr, usrData, true);
            Marshal.StructureToPtr(userHdr[0], usrData, true);


            WriteLog("adminLevel : " + userHdr[0].adminLevel);
            result = BSSDK.BS_EnrollUserEx(handle, usrData, template);

            WriteLog(">>>>>>>>>>>>>>>>>>>>> (91) Enroll User FP.... result : " + result);





            /*
            hdr.ID = 1;
           
            fetchFingerData.matchTimeout = 10;
            fetchFingerData.timeout = 5;
            fetchFingerData.useServerMatching = true;


            IntPtr sendData = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(BSSDK.BSFingerprintConfig)));
            Marshal.StructureToPtr(fetchFingerData, sendData, true);

            result = BSSDK.BS_WriteFingerprintConfig(handle, sendData);

            WriteLog(">>>>>>>>>>>>>>>>>>>>> (9) Writer FP.... result : " + result);
            */


        }

        public string getB2S(byte[] input)
        {
            UTF8Encoding enc = new UTF8Encoding();
            string str = enc.GetString(input).Trim();
            return str;
        }

        public void WriteLog(string LogMessage)
        {
            string strDate = DateTime.Now.ToString("[HH:mm:ss]", DateTimeFormatInfo.InvariantInfo);

            // string strPath = "D:\\BTMS_DEV_TST\\NEW\\Log";
            string strPath = ".\\Log";
            string filePath = strPath + @"\" + DateTime.Now.ToString("MMdd", DateTimeFormatInfo.InvariantInfo) + ".log";

            try
            {
                StreamWriter sw = new StreamWriter(filePath, true, System.Text.Encoding.Default);
                LogMessage = strDate + LogMessage;
                sw.WriteLine(LogMessage);
                sw.Flush();
                sw.Close();
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                string err = ex.Message;
                Directory.CreateDirectory(strPath);
                WriteLog(LogMessage);
            }
            catch (System.IO.IOException ex)
            {
                //파일쓰기 오류이면 다시 쓴다.
                string exStr = ex.ToString();
                WriteLog(LogMessage);
            }
            catch
            {
                //throw new Exception("|C|*|" + ex.Message + "| <- ibFramework.ibPerformance.ibPerfWriteLog");        
            }

        }

    }

    
}
