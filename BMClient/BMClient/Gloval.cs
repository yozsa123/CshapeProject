using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;


namespace BMClient
{
    class Gloval
    {

        public static LoadingForm loading = null;
        public static System.Threading.Timer timer = null;

        public static string readBid = "";

        public static Boolean aleadyLoad = false;
        public static Boolean scanSuccess = false;

        public static byte[] template = new byte[384];
        public static byte[] template2 = new byte[384];

        public static Boolean deleteOK = false;

        public static string empQuery = "";
        public static string badgeQuery = "";
        public static string sazinQuery = "";

        public static byte[] newSazin = null;

        public static string protocolString = "";

        // JSJ 0611
        public static int fpServerNum = 1;


        public Gloval()
        {

        }

        public static void initQuery ()
        {
            empQuery = badgeQuery = sazinQuery = protocolString = "";
            newSazin = null;
        }

    }
}

