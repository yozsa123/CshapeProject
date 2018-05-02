using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace BMClient
{
    class Log
    {
        public Log()
        {
        }

        public static void WriteLog(string LogMessage)
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
                //throw new Exception("|C|*|" + ex.Message + "| <- ibFramework.ibPerformance.ibPerfLog.WriteLog");        
            }

        }


        public static void WriteLogTmp (string LogMessage)
        {
            string strDate = DateTime.Now.ToString("[HH:mm:ss]", DateTimeFormatInfo.InvariantInfo);

            // string strPath = "D:\\BTMS_DEV_TST\\NEW\\Log";
            string strPath = ".\\LogTmp";
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
                //throw new Exception("|C|*|" + ex.Message + "| <- ibFramework.ibPerformance.ibPerfLog.WriteLog");        
            }

        }


    }
}

