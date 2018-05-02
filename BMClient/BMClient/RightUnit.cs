using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMClient
{
    class RightUnit
    {
        string id   = "";
        string desc = "";

        public RightUnit()  {  }

        public string getID () { return id; }
        public string getDesc () { return desc; }
        
        public void setID(string _id) { id = _id; }
        public void setDesc (string _desc) { desc = _desc; }

    }

}

