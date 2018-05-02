using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMClient
{
    class ListUnit
    {
        Boolean check = false;
        string name = "";
        string bid = "";
        string empId = "";
        string department = "";

        

        public ListUnit()
        {

        }

        public string getName() { return name; }
        public string getBID() { return bid; }
        public string getEmpID() { return empId; }
        public string getDepartment () { return department; }
        public Boolean getCheck() { return check; }

        public void setName (string _name) { name = _name; }
        public void setBid (string _bid) { bid = _bid; }
        public void setEmpId(string _empId) { empId = _empId; }
        public void setDepartment (string _department) { department = _department; }
        public void setCheck(Boolean _check) { check = _check; }

    }

}

