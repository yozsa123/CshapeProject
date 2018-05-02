using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.IO;

namespace BMClient
{
    class Request
    {
        string sDelimeter = "|";

        NetworkStream serverStream;
        IPEndPoint ip;
        string set_Path = Application.StartupPath + @"\Hub_Setting.ini";
        public string select (string mainCode, string query, string subCode)
        {
            string serverIP = HubIniFile.GetIniValue("SERVER", "IP", set_Path);

            Log.WriteLog("[Reg.cs] select (" + mainCode + ", " + query + ", " + subCode + ")");


            ip = new IPEndPoint(IPAddress.Parse(serverIP), 13300);

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

                mSendMessage = "Start" + Bm_Login.login_Id + sDelimeter;
                mSendMessage += mainCode + sDelimeter;
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

                ///////////////////////// 받는부분 ////////////////////////////////                

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

                    // JSJ
                    // MessageBox.Show("|" + arrayRData[0] + "|" + arrayRData[1] + "|" + arrayRData[2] + "|" + arrayRData[3] + "|" + arrayRData[4]);

                    if (arrayRData[1] == "Error")
                    {
                        if (arrayRData[2] == "DBConnection")
                        {
                            MessageBox.Show("DB 연결 에러 (" + arrayRData [3] + ")");
                            return "1";
                        }

                        MessageBox.Show("네트워크에 장애가 발생했습니다. 다시 시도해주세요");
                        //BackLog.Log.LogWrite("VMS002", "비정상", "loginProcess", "Error 리턴받음", "");
                        return "1";
                    }

                    byte[] tDataTable = null;

                    int TotalLength = decryptedData.Length;

                    int StrLength = arrayRData[0].Length + arrayRData[1].Length + arrayRData[2].Length;

                    Byte[] byteData = Encoding.UTF8.GetBytes(arrayRData[0] + "|" + arrayRData[1] + "|" + arrayRData[2] + "|");

                    Byte[] byteReceiveData = new Byte[TotalLength - byteData.Length - 5];

                    Buffer.BlockCopy(decryptedData, byteData.Length, byteReceiveData, 0, TotalLength - byteData.Length - 5);

                    tDataTable = byteReceiveData;

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

        public string update (string mainCode, string query, string subCode)
        {
            //ip = new IPEndPoint(IPAddress.Parse(SystemInfo.server_Ip), 13300);
            ip = new IPEndPoint(IPAddress.Parse(HubIniFile.GetIniValue("SERVER", "IP", set_Path)), 13300);


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

                mSendMessage = "Start" + Bm_Login.login_Id + sDelimeter;
                mSendMessage += mainCode + sDelimeter;
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
                        return "1";
                    }
                    else if (arrayRData[3] == "Update OK" || arrayRData[3] == "Insert OK" || arrayRData[3] == "Delete OK" || arrayRData[3] == "MultiProcess OK")
                    {
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

        public string updateImage (string mainCode, string query, string subCode, byte[] ImageByte)
        {

            Log.WriteLogTmp("[Request.cs] updateImage (" + mainCode + ", " + query + ", " + subCode + ")");

            
            // ip = new IPEndPoint(IPAddress.Parse(SystemInfo.server_Ip), 13300);
            ip = new IPEndPoint(IPAddress.Parse(HubIniFile.GetIniValue("SERVER", "IP", set_Path)), 13300);


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

                mSendMessage = "Start" + HubIniFile.GetIniValue("SERVER", "IP", set_Path) + sDelimeter;
                mSendMessage += mainCode + sDelimeter;
                mSendMessage += subCode + sDelimeter;
                
                mSendMessage += query + sDelimeter;

                /*
                // mSendMessage += "update EMP set NAME_1 = '장순중' where ID = 25601^" + query + sDelimeter;
                Byte[] byteData1 = Encoding.UTF8.GetBytes(mSendMessage); // string -> byte
                Byte[] byteData2 = ImageByte; //Image
                // Byte [] byteData3 = Encoding.UTF8.GetBytes("^update EMP set NAME_1 = '골이-10072' where ID = 25601|END|");

                // mSendMessage += "END" + sDelimeter;


                byte[] rv = new byte [byteData1.Length + byteData2.Length];

                System.Buffer.BlockCopy (byteData1, 0, rv, 0, byteData1.Length);
                System.Buffer.BlockCopy (byteData2, 0, rv, byteData1.Length, byteData2.Length);
                */


                if (ImageByte == null)
                    Log.WriteLogTmp("[Request.cs] updateImage () : ImageByte is NULL");
                else Log.WriteLogTmp("[Request.cs] updateImage () : ImageByte.Length : " + ImageByte.Length);

                Byte[] byteData1 = Encoding.UTF8.GetBytes(mSendMessage); // string -> byte
                Byte[] byteData2 = ImageByte; //Image

                byte [] rv = null;
                if (ImageByte == null) rv = new byte[byteData1.Length];
                else rv = new byte[byteData1.Length + byteData2.Length];

                System.Buffer.BlockCopy(byteData1, 0, rv, 0, byteData1.Length);
                Log.WriteLogTmp("[Request.cs] BEFORE");
                if (ImageByte != null) System.Buffer.BlockCopy(byteData2, 0, rv, byteData1.Length, byteData2.Length);
                Log.WriteLogTmp("[Request.cs] AFTER");
               

                serverStream = new NetworkStream(server);

                string s = Encoding.UTF8.GetString(rv, 0, rv.Length);


                // Log.WriteLogTmp("[Request.cs] updateImage () send msg : " + s);



                byte[] sendbyte = AESClass.AESClass.AESENC(rv); // 암호화

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
                    else if (arrayRData[3] == "Update OK" || arrayRData[3] == "Insert OK" || arrayRData[3] == "Delete OK" || arrayRData[3] == "MultiProcess OK")
                    {
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

        public string updateFinger(string queryType, string query, string connectType, DataTable dt, string serverIP)
        {

            //query면 dt == null
            // 프로시저에서 데이터 담아둠
            Log.WriteLog("[Request.cs] updateImage (" + queryType + ", " + query + ")");


            // string literal = "0x" + BitConverter.ToString(template1).Replace("-", "");

            // ip = new IPEndPoint(IPAddress.Parse(SystemInfo.server_Ip), 13300);
            ip = new IPEndPoint(IPAddress.Parse(serverIP), 13300);
            Log.WriteLog("1");

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.SendTimeout = 3000;
            server.ReceiveTimeout = 3000;


            Log.WriteLog("2");
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
                    Log.WriteLog("Server와의 연결에 실패했습니다.");
                }
            }


            if (queryType.Equals("Q"))
            {
                try
                {
                    string mSendMessage = string.Empty;

                    mSendMessage = sDelimeter + connectType + sDelimeter + queryType + sDelimeter; // |F|Q|
                    mSendMessage += query + sDelimeter; //  F|Q|Query|



                    /*
                    // mSendMessage += "update EMP set NAME_1 = '장순중' where ID = 25601^" + query + sDelimeter;
                    Byte[] byteData1 = Encoding.UTF8.GetBytes(mSendMessage); // string -> byte
                    Byte[] byteData2 = ImageByte; //Image
                    // Byte [] byteData3 = Encoding.UTF8.GetBytes("^update EMP set NAME_1 = '골이-10072' where ID = 25601|END|");

                    // mSendMessage += "END" + sDelimeter;


                    byte[] rv = new byte [byteData1.Length + byteData2.Length];

                    System.Buffer.BlockCopy (byteData1, 0, rv, 0, byteData1.Length);
                    System.Buffer.BlockCopy (byteData2, 0, rv, byteData1.Length, byteData2.Length);
                    */



                    Byte[] byteData1 = Encoding.UTF8.GetBytes(mSendMessage); // string -> byte



                    byte[] rv = new byte[byteData1.Length];

                    System.Buffer.BlockCopy(byteData1, 0, rv, 0, byteData1.Length);
                    Log.WriteLog("[Request.cs] BEFORE");


                    serverStream = new NetworkStream(server);

                    serverStream.ReadTimeout = 3000;
                    serverStream.WriteTimeout = 3000;

                    string s = Encoding.UTF8.GetString(rv, 0, rv.Length);


                    byte[] sendbyte = AESClass.AESClass.AESENC(rv); // 암호화

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



                        if (sRecieved == "E")
                        {
                            Log.WriteLog("네트워크에 장애가 발생했습니다. 다시 시도해주세요");
                            //BackLog.Log.LogWrite("VMS002", "비정상", "loginProcess", "Error 리턴받음", "");
                            return "1";
                        }
                        else if (sRecieved == "O")
                        {

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

                }
            }
            else if (queryType.Equals("P"))
            {
                try
                {
                    string mSendMessage = string.Empty;

                    mSendMessage = sDelimeter + connectType + sDelimeter + queryType + sDelimeter; // |F|Q|
                    mSendMessage += query + sDelimeter; // |F|Q|프로시저명|

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == 0)
                            mSendMessage += dt.Rows[i]["param"] + "~" + dt.Rows[i]["value"] + "~" + dt.Rows[i]["type"];// |F|Q|프로시저명|Param~value~type
                        else
                            mSendMessage += "$" + dt.Rows[i]["param"] + "~" + dt.Rows[i]["value"] + "~" + dt.Rows[i]["type"]; // |F|Q|프로시저명|Param~value~type&Param~value~type
                    }

                    mSendMessage += sDelimeter; // |F|Q|프로시저명|Param~value~type&Param~value~type|

                    Byte[] byteData1 = Encoding.UTF8.GetBytes(mSendMessage); // string -> byte
                    Log.WriteLog("[Request.cs] mSendMessage :" + mSendMessage);

                    byte[] rv = null;


                    rv = new byte[byteData1.Length];



                    System.Buffer.BlockCopy(byteData1, 0, rv, 0, byteData1.Length);

                    serverStream = new NetworkStream(server);



                    string s = Encoding.UTF8.GetString(rv, 0, rv.Length);



                    byte[] sendbyte = AESClass.AESClass.AESENC(rv); // 암호화



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
                        string[] tmp = sRecieved.Split('|');



                        if (tmp[1] == "E")
                        {
                            Log.WriteLog("네트워크에 장애가 발생했습니다. 다시 시도해주세요");
                            //BackLog.Log.LogWrite("VMS002", "비정상", "loginProcess", "Error 리턴받음", "");
                            return "1";
                        }
                        else if (tmp[1] == "S")
                        {
                            return "0";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteLog(ex.TargetSite + " :" + ex.Message);
                }
                finally
                {
                    serverStream.Dispose();
                    server.Close();
                }

            }

            return "1";
        }



    }

    public static class ReturnDT
    {
        public static DataTable dt { get; set; }
    }
}
