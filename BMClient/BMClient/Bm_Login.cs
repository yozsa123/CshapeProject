using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;


namespace BMClient
{
    public partial class Bm_Login : Form
    {


        public static string login_Name;
        public static string login_Id;
        public static string login_Pw;
        public static string login_Auth;

        Boolean login_flag = false;
        string delimeter = "^";
        string sDelimeter = "|";
        QueryMaker qm;

        string set_Path = Application.StartupPath + @"\Hub_Setting.ini";

        NetworkStream serverStream;
        IPEndPoint ip;

        Request req;

        public static string encType = "N";
        public Bm_Login()
        {
            InitializeComponent();



            SystemInfo.server_Ip = HubIniFile.GetIniValue("SERVER", "IP", set_Path);
            encType = HubIniFile.GetIniValue("SERVER", "ENC", set_Path);
            string id = HubIniFile.GetIniValue("LOGIN", "ID", set_Path);
            txt_Id.Text = id;
            login_Id = "";
            CheckSaveId.Checked = true;

            ip = new IPEndPoint(IPAddress.Parse(SystemInfo.server_Ip), 13300);
            qm = new QueryMaker();
            req = new Request();


        }

        private void LoginForm_Shown(object sender, EventArgs e)
        {
            if (txt_Id.Text != "") /// @brief 파일 아이디가 존재 하는지 검사.
            {
                txt_Pw.Focus(); /// @brief 파일 아이디가 존재 하면 포커스를 비밀번호로 이동한다.
            }
        }

        private void tbox_Pw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
                txt_Id.Focus();  // 중복 입력을 방지하기 위해 포커스를 아무 기능 없는 곳으로 이동
            }
        }



        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Id.Text) || string.IsNullOrEmpty(txt_Pw.Text))
            {
                MessageBox.Show("아이디와 패스워드를 입력하셔야 합니다", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            login();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void login()
        {
            string query = qm.getQuery("SYSTEM", txt_Id.Text);

            string request = connectS(query, "BMS");

            if (request == "0")
            {
                DataTable dt = ReturnDT.dt;

                //DataTable dt = DBHandling.selectDB("M^0", query);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("등록되지 않은 사용자입니다. 다시한번 확인해주세요", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    login_Name = dt.Rows[0]["User_Name"].ToString();
                    login_Id = dt.Rows[0]["ID"].ToString();
                    login_Auth = dt.Rows[0]["Program_List"].ToString();

                    string endPw = dt.Rows[0]["User_Password"].ToString();
                    byte[] pw = Convert.FromBase64String(endPw);
                    byte[] decpw = AESClass.AESClass.AESDEC(pw);
                    string strPassword = Encoding.UTF8.GetString(decpw);
                    string[] password = strPassword.Split('\0');

                    if (txt_Pw.Text.Trim() != password[0])
                    {
                        MessageBox.Show("입력하신 아이디 혹은 비밀번호가 일치하지 않습니다. 다시한번 확인해주세요", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    login_Pw = password[0];

                    // 체크박스 체크여부 확인, ID 저장
                    if (CheckSaveId.CheckState == CheckState.Checked)
                    {
                        HubIniFile.SetIniValue("LOGIN", "ID", txt_Id.Text, set_Path);
                    }
                    else if (CheckSaveId.CheckState == CheckState.Unchecked)
                    {
                        HubIniFile.SetIniValue("LOGIN", "ID", "", set_Path);
                    }

                    updateUserConn();
                    login_flag = true;
                    try
                    {
                        Application.OpenForms["bm_Login"].Close();
                        query = "select IP_ADDRESS from CLIENT_CONFIG where IP_ADDRESS ='" + getIp() + "'";
                        request = connectS(query, "BMS");


                        if (request.Equals("0"))
                        {
                            dt = ReturnDT.dt;
                            if (!login_Id.Equals("admin"))
                            {
                                if (dt.Rows.Count == 0)
                                {
                                    MessageBox.Show("등록되지않은 IP입니다. 프로그램을 종료합니다.");
                                    request = req.update("BMS001", qm.getQuery("LOGIN_LOG", "0"), "BMI");
                                    Application.Exit();
                                }
                            }


                            request = req.update("BMS001", qm.getQuery("LOGIN_LOG", "1"), "BMI");


                        }
                        else
                        {
                            MessageBox.Show("Client 정보를 입력하는데 실패하였습니다. 프로그램을 종료합니다.");
                            request = req.update("BMS001", qm.getQuery("LOGIN_LOG", "0"), "BMI");
                            Application.Exit();
                        }

                    }
                    catch (Exception e)
                    {
                        Application.Exit();
                    }


                }
            }
            else
            {
                MessageBox.Show("서버와 통신이 정상적이지 않습니다. 관리자에게 문의해주세요.");
            }
        }

        private void updateUserConn()
        {



            string iQuery = "UPDATE CLIENT_CONFIG SET ";
            iQuery += "Connect_Id = '" + txt_Id.Text.Trim() + "', ";
            iQuery += "IN_OUT_DATE = GETDATE() ";
            iQuery += "WHERE Ip_Address = '" + getIp() + "' ";

            Log.WriteLog("Query = " + iQuery);
            //string rCheck = DBHandling.cudDB("M^0", iQuery);            
            string rCheck = req.update("BMS002", iQuery, "BMI");



        }


        private string connectS(string query, string subCode)
        {
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            server.ReceiveBufferSize = 1000000;

            try
            {
                if (!server.Connected)
                {
                    server.Connect(ip);
                }
            }
            catch (SocketException se)
            {
                if (se.NativeErrorCode == 10060)
                {
                    MessageBox.Show("Server와의 연결에 실패했습니다.");
                }
            }

            DataTable dt = new DataTable();

            try
            {
                string mSendMessage = string.Empty;

                mSendMessage = "Start" + txt_Id.Text.Trim() + sDelimeter;
                mSendMessage += "BMS002" + sDelimeter;
                mSendMessage += subCode + sDelimeter;
                mSendMessage += query + sDelimeter;
                mSendMessage += "END" + sDelimeter;

                serverStream = new NetworkStream(server);

                Byte[] byteDateLine = Encoding.UTF8.GetBytes(mSendMessage); // string -> byte

                byte[] sendbyte = AESClass.AESClass.AESENC(byteDateLine); // 암호화

                if (serverStream.CanWrite)
                {
                    serverStream.Write(sendbyte, 0, sendbyte.Length);
                    serverStream.Flush();
                }

                //// ----- 받는부분 -----                

                byte[] inStream = new byte[4000000];

                int ReadBytes = 0;

                do
                {
                    ReadBytes = serverStream.Read(inStream, 0, (int)server.ReceiveBufferSize);
                } while (serverStream.DataAvailable);

                if (inStream.Length > 0)
                {
                    byte[] tData = new byte[ReadBytes];
                    Buffer.BlockCopy(inStream, 0, tData, 0, ReadBytes);
                    byte[] decryptedData = AESClass.AESClass.AESDEC(tData);

                    string sRecieved = Encoding.UTF8.GetString(decryptedData, 0, decryptedData.Length);

                    string[] arrayRData = sRecieved.Split('|');

                    if (arrayRData[1] == "Error")
                    {
                        MessageBox.Show("네트워크에 장애가 발생했습니다. 다시 시도해주세요");
                        //BackLog.Log.LogWrite("VMS002", "비정상", "loginProcess", "Error 리턴받음", "");
                        return "1";
                    }

                    byte[] tDataTable = getByte(arrayRData, decryptedData);

                    dt = Util.DeserializeData(tDataTable);

                    if (dt == null)
                    {
                        //BackLog.Log.LogWrite("VMS002", "비정상", "loginProcess", "Deserialize 결과 DataTable Null 리턴", "");
                        MessageBox.Show("정보를 불러오는데 실패했습니다. 다시 시도해주세요", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return "1";
                    }
                    else
                    {
                        ReturnDT.dt = dt;

                        return "0";
                    }
                }
            }
            catch (Exception ex)
            {
                //LogWrite("loginProcess", ex.Message);
            }
            finally
            {
                //serverStream.Close();
                //server.Close();
            }

            return "1";
        }

        private byte[] getByte(string[] data, byte[] decrypted)
        {
            byte[] tDataTable = null;

            int TotalLength = decrypted.Length;

            int StrLength = data[0].Length + data[1].Length + data[2].Length;

            Byte[] byteData = Encoding.UTF8.GetBytes(data[0] + "|" + data[1] + "|" + data[2] + "|");

            Byte[] byteReceiveData = new Byte[TotalLength - byteData.Length - 5];

            Buffer.BlockCopy(decrypted, byteData.Length, byteReceiveData, 0, TotalLength - byteData.Length - 5);

            tDataTable = byteReceiveData;

            return tDataTable;
        }

        private string getIp()
        {
            IPHostEntry host = Dns.GetHostByName(Dns.GetHostName());
            string myIp = host.AddressList[0].ToString();
            return myIp;
        }

        class ReturnDT
        {
            public static DataTable dt { get; set; }
        }

        private void Bm_Login_FormClosed(object sender, FormClosedEventArgs e)
        {

            if (!login_flag)
            {
                Application.Exit();
            }
        }
    }
}
