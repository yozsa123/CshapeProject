using System;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.IO;

namespace BMClient
{
    class HubIniFile
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
            string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);

        // INI 값 읽기
        public static String GetIniValue(String Section, String Key, String iniPath)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, iniPath);
            return temp.ToString();
        }

        // INI 값 설정
        public static void SetIniValue(String Section, String Key, String Value, String iniPath)
        {
            FileInfo _finfo = new FileInfo(iniPath);
            if (_finfo.Exists)
            {
                /*
                 * @WritePrivateProfileString :ini 파일에 기록한다.
                 */
                WritePrivateProfileString(Section, Key, Value, iniPath);
            }
            else
            {
                /*
                 * @objSaveFile : ini파일이 없을경우 ini파일을 생성한다. 
                 */
                Stream FS = new FileStream(iniPath, FileMode.Create, FileAccess.Write);
                System.IO.StreamWriter objSaveFile = new System.IO.StreamWriter(FS, System.Text.Encoding.Default);
                objSaveFile.Write("");
                objSaveFile.Close();
                objSaveFile.Dispose();

                /*
                 * @WritePrivateProfileString :ini 파일에 기록한다.
                 */
                WritePrivateProfileString(Section, Key, Value, iniPath);
            }

            //FileInfo finfo = new FileInfo(iniPath);
            //// INI파일 존재
            //if (finfo.Exists)
            //{
            //    WritePrivateProfileString(Section, Key, Value, iniPath);
            //}
            //// INI파일 비존재
            //else
            //{
            //    Stream s = new FileStream(iniPath, FileMode.Create, FileAccess.Write);
            //    StreamWriter sw = new StreamWriter(s, Encoding.Default);
            //    sw.Write("");
            //    sw.Close();
            //    sw.Dispose();

            //    WritePrivateProfileString(Section, Key, Value, iniPath);
            //}            
        }

        public static string getIni(string key)
        {
            using (StreamReader sr = new StreamReader(".\\Hub_Setting.ini"))
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
                        return result.Trim();

                    }
                }
            }

            return null;

        }


    }
}
