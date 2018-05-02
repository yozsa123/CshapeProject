using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BMClient
{
    class MyProperties
    {

        public static string getIni(string key)
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
                        return result.Trim();

                    }
                }
            }

            return null;

        }
    }
}

