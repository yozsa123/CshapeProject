using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMClient
{
    class DetailUnit
    {

        //////////////////////// 1. PERSONNEL DATA /////////////////////////

        string empID = "";
        string name = "";
        DateTime birth = new DateTime(1000, 1, 1);
        string gender = "";
        string ssno = "";
        int department = -1;
        int division = -1;
        int title = -1;
        string email = "";
        string tel = "";
        string address = "";
        byte[] sazin = null;




        //////////////////////// 2. CARD DATA /////////////////////////

        string bid = "";
        string newBid = "";
        string cardName = "";
        string pin = "";
        string issueNum = "";
        int badgeType = -1;
        int badgeStatus = -1;
        int badgeFormat = -1;
        int issueType = -1;
        int issueReason = -1;
        int pt = -1;
        DateTime active = new DateTime(1000, 1, 1);
        DateTime deactive = new DateTime(1000, 1, 1);



        //////////////////////// 2. RIGHT DATA /////////////////////////
        string right1 = "-100";
        string right2 = "-100";
        string right3 = "-100";
        string right4 = "-100";

        string fpNum = "";
        string fpNum2 = "";
        string byPass = "";

        string vmStatus = "";
        string acs1Status = "";
        string acs2Status = "";
        string acs3Status = "";
        string acs4Status = "";
        string mdmStatus = "";


        string multiBid = "";
        string multiEmp = "";


        public DetailUnit()
        {

        }

        
        public string getEmpID() { return empID; }
        public string getName() { return name; }
        public DateTime getBirth() { return birth; }
        public string getGender() { return gender; }
        public string getSsno() { return ssno; }
        public int getDepartment() { return department; }
        public int getDivision() { return division; }
        public int getTitle() { return title; }
        public string getEmail() { return email; }
        public string getTel() { return tel; }
        public string getAddress() { return address; }
        public byte[] getSazin() { return sazin; }





        public string getBid () { return bid; }
        public string getNewBid() { return newBid; }
        public string getCardName() { return cardName; }
        public string getPin() { return pin; }
        public string getIssueNum() { return issueNum; }
        public int getBadgeType() { return badgeType; }
        public int getBadgeStatus() { return badgeStatus; }
        public int getBadgeFormat() { return badgeFormat; }
        public int getIssueType() { return issueType; }
        public int getIssueReason() { return issueReason; }
        public DateTime getActive() { return active; }
        public DateTime getDeactive() { return deactive; }
        public int getPT(){ return pt;}
        public string getRight1() { return right1; }
        public string getRight2() { return right2; }
        public string getRight3() { return right3; }
        public string getRight4() { return right4; }

        public string getFpNum() { return fpNum; }
        public string getFpNum2() { return fpNum2; }
        public string getByPass() { return byPass; }

        public string getVmStatus() { return vmStatus; }
        public string getAcs1Status() { return acs1Status; }
        public string getAcs2Status() { return acs2Status; }
        public string getAcs3Status() { return acs3Status; }
        public string getAcs4Status() { return acs4Status; }
        public string getMdmStatus() { return mdmStatus; }

        public string getMultiBid () { return multiBid; }
        public string getMultiEmp () { return multiEmp; }

        public void setEmpID(string _empID) { empID = _empID; }
        public void setName(string _name) { name = _name; }
        public void setBirth(DateTime _birth) { birth = _birth; }
        public void setGender(string _gender) { gender = _gender; }
        public void setSsno(string _ssno) { ssno = _ssno; }
        public void setDepartment(int _department) { department = _department; }
        public void setDivision(int _division) { division = _division; }
        public void setTitle(int _title) { title = _title; }
        public void setEmail(string _email) { email = _email; }
        public void setTel(string _tel) { tel = _tel; }
        public void setAddress(string _address) { address = _address; }
        public void setSazin(byte[] _sazin) { sazin = _sazin; }




        public void setBid (string _bid) { bid = _bid; }
        public void setNewBid(string _newBid) { newBid = _newBid; }
        public void setCardName(string _cardName) { cardName = _cardName; }
        public void setPin(string _pin) { pin = _pin; }
        public void setIssueNum(string _issueNum) { issueNum = _issueNum; }
        public void setBadgeType(int _badgeType) { badgeType = _badgeType; }
        public void setBadgeStatus(int _badgeStaus) { badgeStatus = _badgeStaus; }
        public void setBadgeFormat(int _badgeFormat) { badgeFormat = _badgeFormat; }
        public void setIssueType(int _issueType) { issueType = _issueType; }
        public void setIssueReason(int _issueReason) { issueReason = _issueReason; }
        public void setActive(DateTime _active) { active = _active; }
        public void setDeactive(DateTime _deactive) { deactive = _deactive; }
        public void setPT(int _pt){pt =_pt;}
        public void setRight1 (string _right1) { right1 = _right1; }
        public void setRight2 (string _right2) { right2 = _right2; }
        public void setRight3 (string _right3) { right3 = _right3; }
        public void setRight4 (string _right4) { right4 = _right4; }



        public void setFpNum(string _fpNum) { fpNum = _fpNum; }
        public void setFpNum2(string _fpNum2) { fpNum2 = _fpNum2; }
        public void setBypass(string _bypass) { byPass = _bypass; }

        public void setVmStatus(string _vmStatus) { vmStatus = _vmStatus; }
        public void setAcs1Status(string _acs1Status) { acs1Status = _acs1Status; }
        public void setAcs2Status(string _acs2Status) { acs2Status = _acs2Status; }
        public void setAcs3Status(string _acs3Status) { acs3Status = _acs3Status; }
        public void setAcs4Status(string _acs4Status) { acs4Status = _acs4Status; }
        public void setMdmStatus(string _mdmStatus) { mdmStatus = _mdmStatus; }

        public void setMultiBid (string _multiBid) { multiBid = _multiBid; }
        public void setMultiEmp(string _multiEmp) { multiEmp = _multiEmp; }


        public void initData()
        {

            empID = "";
            name = "";
            birth = new DateTime(1000, 1, 1);
            gender = "";
            ssno = "";
            department = -1;
            division = -1;
            title = -1;
            email = "";
            tel = "";
            address = "";
            sazin = null;



            
            bid = "";
            newBid = "";
            cardName = "";
            pin = "";
            issueNum = "";
            badgeType = -1;
            badgeStatus = -1;
            badgeFormat = -1;
            issueType = -1;
            issueReason = -1;
            active = new DateTime(1000, 1, 1);
            deactive = new DateTime(1000, 1, 1);

            right1 = "";
            right2 = "";
            right3 = "";
            right4 = "";
           

            fpNum = "";
            byPass = "";

            vmStatus = "";
            acs1Status = "";
            acs2Status = "";
            acs3Status = "";
            acs4Status = "";
            mdmStatus = "";

            multiBid = "";
            multiEmp = "";

            pt = -1;


        }


    }


}
