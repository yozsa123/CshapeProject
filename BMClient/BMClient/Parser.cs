using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace BMClient
{
    class Parser
    {
        public Parser()
        {

        }

        public DetailUnit parseCard (string _orgData, string mode)
        {
            // _orgData = "100=003337926642,1=123,3=1980년04월05일,6=1,2=홍길동,14=1111,17=1,21=2015-06-01,22=2030-06-01,10=010-9089-2143";


            _orgData = _orgData.Replace("\n", "");

            Log.WriteLogTmp("[Parser.cs] parseCard JJJJJJJJ (" + _orgData + ")");


            int end = -1; ;
            string curData = "";
            string totalData = "";

            DetailUnit du = new DetailUnit();

            du.initData();

            while (true)
            {
                end = -1;
                end = _orgData.IndexOf(",");
                if (end < 0)
                {
                    curData = _orgData;
                    setUnit(curData, du);

                    totalData = totalData + " | " + curData;
                    break;
                }
                else
                {
                    curData = _orgData.Substring(0, end);
                    setUnit(curData, du);

                    _orgData = _orgData.Substring(end + 1);
                }

                totalData = totalData + " | " + curData;

            }

            string empQuery = "";
            if (mode.Equals ("MASS_CHANGE")) empQuery = makeEmpMultiUpdateQuery(du);
            else empQuery = makeEmpUpdateQuery(du);
            Log.WriteLogTmp("[Parser.cs] parseCard () EMP query : " + empQuery);
            // if (empQuery != null) MessageBox.Show("[Parser.cs] parseCard () EMP query : \n" + empQuery);
            Gloval.empQuery = empQuery;

            string badgeQuery = "";
            if (mode.Equals("MASS_CHANGE")) badgeQuery = makeBadgeMultiUpdateQuery(du);
            else badgeQuery = makeBadgeUpdateQuery(du);
            Log.WriteLogTmp("[[Parser.cs] parseCard () BADGE query : " + badgeQuery);
            // if (badgeQuery != null) MessageBox.Show("[[Parser.cs] parseCard () BADGE query : \n" + badgeQuery);
            Gloval.badgeQuery = badgeQuery;

            string sazinQuery = "";
            sazinQuery = makeSazinQuery(du);
            Log.WriteLogTmp("[[Parser.cs] parseCard () SAZIN query : " + sazinQuery);
            // if (badgeQuery != null) MessageBox.Show("[[Parser.cs] parseCard () SAZIN query : \n" + sazinQuery);
            Gloval.sazinQuery = sazinQuery;


            // MessageBox.Show ("(" + totalData + ")");



            return null;
        }


        public DetailUnit insertParseCard(string _orgData)
        {
            // _orgData = "100=003337926642,1=123,3=1980년04월05일,6=1,2=홍길동,14=1111,17=1,21=2015-06-01,22=2030-06-01,10=010-9089-2143";


            _orgData = _orgData.Replace("\n", "");

            Log.WriteLogTmp("[Parser.cs] parseCard JJJJJJJJ (" + _orgData + ")");


            int end = -1; ;
            string curData = "";
            string totalData = "";

            DetailUnit du = new DetailUnit();

            du.initData();

            while (true)
            {
                end = -1;
                end = _orgData.IndexOf(",");
                if (end < 0)
                {
                    curData = _orgData;
                    setUnit(curData, du);

                    totalData = totalData + " | " + curData;
                    break;
                }
                else
                {
                    curData = _orgData.Substring(0, end);
                    setUnit(curData, du);

                    _orgData = _orgData.Substring(end + 1);
                }

                totalData = totalData + " | " + curData;

            }

            string empQuery = "";
            empQuery = makeEmpinsertQuery(du);
            Log.WriteLogTmp("[Parser.cs] parseCard () EMP query : " + empQuery);
            // if (empQuery != null) MessageBox.Show("[Parser.cs] parseCard () EMP query : \n" + empQuery);
            Gloval.empQuery = empQuery;

            string badgeQuery = "";
            badgeQuery = makeBadgeinsertQuery(du);
            Log.WriteLogTmp("[[Parser.cs] parseCard () BADGE query : " + badgeQuery);
            // if (badgeQuery != null) MessageBox.Show("[[Parser.cs] parseCard () BADGE query : \n" + badgeQuery);
            Gloval.badgeQuery = badgeQuery;

            string sazinQuery = "";
            sazinQuery = makeSazinInsertQuery(du);
            Log.WriteLogTmp("[[Parser.cs] parseCard () SAZIN query : " + sazinQuery);
            // if (badgeQuery != null) MessageBox.Show("[[Parser.cs] parseCard () SAZIN query : \n" + sazinQuery);
            Gloval.sazinQuery = sazinQuery;


            // MessageBox.Show ("(" + totalData + ")");



            return null;
        }

        public void setUnit(string orgStr, DetailUnit unit)
        {

            Log.WriteLogTmp("............... orgStr : " + orgStr);
            int index = -1;
            index = orgStr.IndexOf("=");

            string key = "";
            string value = "";
            byte[] byteValue = null;

            if (index > -1)
            {
                key = orgStr.Substring(0, index);
                if (!key.Equals("12")) value = orgStr.Substring(index + 1);
                else byteValue = null;              // JSJ

                if (key.Equals("100")) unit.setBid(value);
                else if (key.Equals("0")) unit.setNewBid(value);
                else if (key.Equals("1")) unit.setEmpID(value);
                else if (key.Equals("2")) unit.setName(value);
                else if (key.Equals("3")) unit.setBirth(getDateTimeDesc(value));
                else if (key.Equals("4")) unit.setGender(value);
                else if (key.Equals("5")) unit.setSsno(value);
                else if (key.Equals("6")) unit.setDepartment(Convert.ToInt32(value));
                else if (key.Equals("7")) unit.setDivision(Convert.ToInt32(value));
                else if (key.Equals("8")) unit.setTitle(Convert.ToInt32(value));
                else if (key.Equals("9")) unit.setEmail(value);
                else if (key.Equals("10")) unit.setTel(value);
                else if (key.Equals("11")) unit.setAddress(value);
                else if (key.Equals("12")) unit.setSazin(byteValue);
                else if (key.Equals("13")) unit.setCardName(value);
                else if (key.Equals("14")) unit.setPin(value);
                else if (key.Equals("15")) unit.setIssueNum(value);
                else if (key.Equals("16")) unit.setBadgeType(Convert.ToInt32(value));
                else if (key.Equals("17")) unit.setBadgeStatus(Convert.ToInt32(value));
                else if (key.Equals("18")) unit.setBadgeFormat(Convert.ToInt32(value));
                else if (key.Equals("19"))
                {
                    unit.setIssueType(Convert.ToInt32(value));
                    // MessageBox.Show("issueType : " + Convert.ToInt32(value));
                }
                else if (key.Equals("20")) unit.setIssueReason(Convert.ToInt32(value));
                else if (key.Equals("21")) unit.setActive(getDateTimeDesc(value));
                else if (key.Equals("22")) unit.setDeactive(getDateTimeDesc(value));
                else if (key.Equals("23")) unit.setBypass(value);
                else if (key.Equals("24"))
                {
                    unit.setFpNum(value);
                    Log.WriteLogTmp("........................FpNum : " + unit.getFpNum());
                }
                else if (key.Equals("25")) unit.setVmStatus(value);
                else if (key.Equals("26")) unit.setAcs1Status(value);
                else if (key.Equals("27")) unit.setAcs2Status(value);
                else if (key.Equals("28")) unit.setAcs3Status(value);
                else if (key.Equals("29")) unit.setAcs4Status(value);
                else if (key.Equals("30")) unit.setMdmStatus(value);
                else if (key.Equals("31")) unit.setPT(Convert.ToInt32(value));

                else if (key.Equals("1000")) unit.setRight1(value);
                else if (key.Equals("2000")) unit.setRight2(value);
                else if (key.Equals("3000")) unit.setRight3(value);
                else if (key.Equals("4000")) unit.setRight4(value);

                else if (key.Equals("99")) unit.setMultiBid (value);
                else if (key.Equals("98")) unit.setMultiEmp (value);

                // MessageBox.Show("(" + key + ")" + "(" + value + ")");
            }

        }

        public DateTime getDateTimeDesc (string date)
        {
            Log.WriteLogTmp("[Parser.cs] getDateTimeDesc (" + date + ")");

            int year = Convert.ToInt32 (date.Substring(0, 4));
            int month = Convert.ToInt32(date.Substring(4, 2));
            int day = Convert.ToInt32(date.Substring(6, 2));

            return new DateTime(year, month, day);
        }

        public string makeEmpUpdateQuery(DetailUnit _du)
        {
            string preString = "update EMP set ";
            string whereString = "";
            string setString = "";


            if (_du.getEmpID().Equals("")) return null;

            whereString = " where ID = " + _du.getEmpID();

            if (!_du.getName().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "NAME_1 = '" + _du.getName() + "'";
            }
            if (_du.getBirth() != new DateTime(1000, 1, 1))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "BIRTH_DATE = " + getCastString(getDate(_du.getBirth()));
            }
            if (!_du.getGender().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "GENDER = " + _du.getGender();
            }
            if (!_du.getSsno().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "SSNO = '" + _du.getSsno()+ "'";
            }
            if (_du.getDepartment() != -1)
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "DEPARTMENT = " + _du.getDepartment();
            }
            if (_du.getDivision() != -1)
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "DIVISION = " + _du.getDivision();
            }
            if (_du.getTitle() != -1)
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "TITLE = " + _du.getTitle();
            }
            if (!_du.getEmail().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "EMAIL = '" + _du.getEmail() + "'";
            }
            if (!_du.getTel().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "TEL = '" + _du.getTel() + "'";
            }
            if (!_du.getAddress().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "ADDRESS = '" + _du.getAddress() + "'";
            }

            if (setString.Equals("")) return null;

            setString = setString + ", MODIFY_DATE = GETDATE()";

            return preString + setString + whereString;

        }


        public string makeBadgeUpdateQuery(DetailUnit _du)
        {
            string preString = "update BADGE set ";
            string whereString = "";
            string setString = "";

            if (_du.getBid().Equals("")) return null;

            whereString = " where BID = '" + _du.getBid() + "'";

            if (!_du.getNewBid().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "BID = '" + _du.getNewBid() + "'";

            }
            if (!_du.getCardName().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "DESCRIPTION = '" + _du.getCardName() + "'";

            }
            if (!_du.getPin().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "PIN = " + _du.getPin();

            }
            if (!_du.getIssueNum().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "SEQ = " + _du.getIssueNum() + "'";

            }
            if (_du.getBadgeType() != -1)
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "TYPE = " + _du.getBadgeType();

            }
            if (_du.getBadgeStatus() != -1)
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "STATUS_1 = " + _du.getBadgeStatus();

            }
            if (_du.getBadgeFormat() != -1)
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "FORMAT = " + _du.getBadgeFormat();

            }
            if (_du.getIssueType() != -1)
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "ISSUE_TYPE = " + _du.getIssueType();


            }
            if (_du.getIssueReason() != -1)
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "ISSUE_REASON = " + _du.getIssueReason();

            }
            if (_du.getActive() != new DateTime(1000, 1, 1))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "ACTIVATE_DATE = " + getCastString(getDate(_du.getActive()));

            }
            if (_du.getDeactive() != new DateTime(1000, 1, 1))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "DEACTIVATE_DATE = " + getCastString(getDate(_du.getDeactive())) + " ,DEACTIVATE_DATE1 =" + getCastString(getDate(_du.getDeactive())) + " ,DEACTIVATE_DATE2 =" + getCastString(getDate(_du.getDeactive())) + " ,DEACTIVATE_DATE3 =" + getCastString(getDate(_du.getDeactive())) + " ,DEACTIVATE_DATE4 =" + getCastString(getDate(_du.getDeactive()));

            }
            if (!_du.getByPass().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "BYPASS_FLAG = " + _du.getByPass();

            }
            if (!_du.getFpNum().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "FP_1 = " + _du.getFpNum();

            }
            if (!_du.getVmStatus().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "VM = " + _du.getVmStatus();

            }
            if (!_du.getAcs1Status().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "ACS_1 = " + _du.getAcs1Status();

            }
            if (!_du.getAcs2Status().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "ACS_2 = " + _du.getAcs2Status();

            }

            if (!_du.getAcs3Status().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "ACS_3 = " + _du.getAcs3Status();

            }
            if (!_du.getAcs4Status().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "ACS_4 = " + _du.getAcs4Status();

            }
            if (!_du.getMdmStatus().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "ACS_5 = " + _du.getMdmStatus();

            }
            if (_du.getPT () != -1)
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "PT = " + _du.getPT ();

            }
            if (!_du.getRight1().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "RIGHT_1 = '" + _du.getRight1() + "'";

            }
            if (!_du.getRight2().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "RIGHT_2 = '" + _du.getRight2() + "'";

            }
            if (!_du.getRight3().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "RIGHT_3 = '" + _du.getRight3() + "'"; ;

            }
            if (!_du.getRight4().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "RIGHT_4 = '" + _du.getRight4() + "'"; ;

            }


            if (setString.Equals("")) return null;

            setString = setString + ", MODIFY_DATE = GETDATE()";

            return preString + setString + whereString;

        }

        public string makeEmpinsertQuery(DetailUnit _du)
        {
            string preString = "insert into EMP  ";
            string whereString = "";
            string valuesString = "values (  ";


            if (_du.getEmpID().Equals("")) return null;

            whereString = "";

           
                valuesString = valuesString + "'" + _du.getName() + "'";
                valuesString = valuesString + ", '" + _du.getName() + "'";
                valuesString = valuesString + ", '" + _du.getName() + "'";
            
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + "'" + _du.getSsno()+"'";
            
           
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + "" + _du.getDepartment();
            
            
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " " + _du.getDivision();
            
            
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + "" + _du.getTitle();
            
            
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " '" + _du.getTel() + "'";
            
           
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " '" + _du.getEmail() + "'";
            

           
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " '" + _du.getAddress() + "'";
            


            valuesString = valuesString + ", getdate()";

            
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " " + getCastString(getDate(_du.getBirth()));
            
            
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " " + _du.getGender();
            
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " " + _du.getFpNum();
            
            valuesString = valuesString + ",'','','','','','','')";
            
            
           

            if (valuesString.Equals("")) return null;

           

            return preString + valuesString + whereString;

        }


        public string makeBadgeinsertQuery(DetailUnit _du)
        {
            string preString = "insert into BADGE  ";
            string whereString = "";
            string valuesString = "values(";

            if (_du.getBid().Equals("")) return null;

            whereString = "";

            
               
                valuesString = valuesString + " '" + _du.getNewBid() + "'";

            
            valuesString = valuesString + " ,(select MAX(ID) from EMP) ";

           
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " '" + _du.getCardName() + "'";

            
          
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " " + _du.getBadgeType();

            
           
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " " + _du.getBadgeStatus();

            

            valuesString = valuesString + ",1" + ",1" + ",1";
           
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " " + _du.getBadgeFormat();

            

            valuesString = valuesString + ",0";

                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " " + _du.getPin();

            
           
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " " + getCastString(getDate(_du.getActive()));

            
            
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " " + getCastString(getDate(_du.getDeactive()));

            

            valuesString = valuesString + " , getdate()";


            
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " " + _du.getIssueType();


            
           
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " " + _du.getIssueReason();

            
            
          
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " " + _du.getByPass();
                Log.WriteLogTmp("_du.getByPass()" + _du.getByPass());

            
           
            
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " " + _du.getAcs1Status();

           
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " " + _du.getAcs2Status();

   
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " " + _du.getAcs3Status();

            
           
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " " + _du.getAcs4Status();

            
            
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " " + _du.getMdmStatus();
                Log.WriteLogTmp("_du.getMdmStatus()" + _du.getMdmStatus());

           
            
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + "" + _du.getFpNum();


            
           
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + "" + _du.getFpNum();
                Log.WriteLogTmp("_du.getFpNum()" + _du.getFpNum());
            
            

           
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + "" + _du.getVmStatus();
                Log.WriteLogTmp("_du.getVmStatus() : " + _du.getVmStatus());
            
          
          
            
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + "'" + _du.getRight1() + "'";

            
            if (!_du.getRight2().Equals(""))
            {
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " '" + _du.getRight2() + "'";

            }
            if (!_du.getRight3().Equals(""))
            {
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " '" + _du.getRight3() + "'"; ;

            }
            if (!_du.getRight4().Equals(""))
            {
                if (!valuesString.Equals("")) valuesString = valuesString + ", ";
                valuesString = valuesString + " '" + _du.getRight4() + "'"; 

            }

            if (!_du.getPT().Equals(-1))
            {
                if (!valuesString.Equals("")) valuesString = valuesString + ",'', ";
                valuesString = valuesString + " " + _du.getPT() + " ,'N'," + getCastString(getDate(_du.getDeactive())) + "," + getCastString(getDate(_du.getDeactive())) + "," + getCastString(getDate(_du.getDeactive())) + "," + getCastString(getDate(_du.getDeactive())) + ")";

            }


            if (valuesString.Equals("")) return null;

          

            return preString + valuesString;

        }

        public string makeSazinQuery(DetailUnit _du)
        {
            Log.WriteLogTmp("[Parser.cs] makeSazinQuery () empID : " + _du.getEmpID());

            /*
            if (Gloval.newSazin == null) Log.WriteLogTmp("[Parser.cs] Gloval.newSazin == null");
            else
            {
                Log.WriteLogTmp("not NULL");
            }
            */


            if (Gloval.newSazin == null || _du.getEmpID().Equals("")) return null;
            // Log.WriteLogTmp("[Parser.cs] makeSazinQuery () AFTER");



            Gloval.sazinQuery = "UPDATE EMP_IMAGE SET IMAGE = @Image WHERE EMPID = " + _du.getEmpID();
            // Gloval.sazinQuery = "UPDATE EMP_IMAGE SET UPDATE_DATETIME = GETDATE(), IMAGE = @Image WHERE EMPID = " + _du.getEmpID();



            Log.WriteLogTmp("[Parser.cs] makeSazinQuery ()  Gloval.sazin.Query : " + Gloval.sazinQuery);



            return Gloval.sazinQuery;
        }
        public string makeSazinInsertQuery(DetailUnit _du)
        {
            Log.WriteLogTmp("[Parser.cs] makeSazinQuery () empID : " + _du.getEmpID());

            /*
            if (Gloval.newSazin == null) Log.WriteLogTmp("[Parser.cs] Gloval.newSazin == null");
            else
            {
                Log.WriteLogTmp("not NULL");
            }
            */


            if (Gloval.newSazin == null || _du.getEmpID().Equals("")) return null;
            // Log.WriteLogTmp("[Parser.cs] makeSazinQuery () AFTER");



            Gloval.sazinQuery = "insert into  EMP_IMAGE(EMPID,IMAGE) values ( (select max(ID) from EMP) , @Image )";
            // Gloval.sazinQuery = "UPDATE EMP_IMAGE SET UPDATE_DATETIME = GETDATE(), IMAGE = @Image WHERE EMPID = " + _du.getEmpID();



            Log.WriteLogTmp("[Parser.cs] makeSazinQuery ()  Gloval.sazin.Query : " + Gloval.sazinQuery);



            return Gloval.sazinQuery;
        }


        public string getDate(DateTime dateTime)
        {
            string date = "";

            string month = "" + dateTime.Month;
            while (month.Length < 2) month = "0" + month;
            string day = "" + dateTime.Day;
            while (day.Length < 2) day = "0" + day;

            date = "" + dateTime.Year + month + day;
            return date;
        }

        public string getCastString(string org)
        {

            string year = org.Substring(0, 4);
            string month = org.Substring(4, 2);
            string day = org.Substring(6, 2);

            string castString = "CAST('" + year + "-" + month + "-" + day + "' AS DATETIME)";

            return castString;

        }


        public string makeEmpMultiUpdateQuery (DetailUnit _du)
        {
            string preString = "update EMP set ";
            string whereString = "";
            string setString = "";


            if (_du.getMultiEmp ().Equals("")) return null;

            whereString = " where " + getMultiWhereString(_du.getMultiEmp(), "$", "ID");

            
            if (_du.getDepartment() != -1)
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "DEPARTMENT = " + _du.getDepartment();
            }
            if (_du.getDivision() != -1)
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "DIVISION = " + _du.getDivision();
            }
            if (_du.getTitle() != -1)
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "TITLE = " + _du.getTitle();
            }
            

            if (setString.Equals("")) return null;

            setString = setString + ", MODIFY_DATE = GETDATE()";

            return preString + setString + whereString;

        }


        public string makeBadgeMultiUpdateQuery (DetailUnit _du)
        {
            string preString = "update BADGE set ";
            string whereString = "";
            string setString = "";

            if (_du.getMultiEmp().Equals("")) return null;

            whereString = " where " + getMultiWhereString(_du.getMultiBid (), "$", "BID");

            if (_du.getBadgeType() != -1)
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "TYPE = " + _du.getBadgeType();

            }
            if (_du.getBadgeStatus() != -1)
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "STATUS_1 = " + _du.getBadgeStatus();

            }
            if (_du.getBadgeFormat() != -1)
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "FORMAT = " + _du.getBadgeFormat();

            }

            if (_du.getDeactive() !=DateTime.Now.AddYears(-10))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "DEACTIVATE_DATE = " + getCastString(getDate(_du.getDeactive()));

            }

            if (!_du.getByPass().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "BYPASS_FLAG = " + _du.getByPass();

            }

            if (_du.getPT() != -1)
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "PT = " + _du.getPT ();

            }

            if (!_du.getRight1().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "RIGHT_1 = '" + _du.getRight1() + "'";

            }
            if (!_du.getRight2().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "RIGHT_2 = '" + _du.getRight2() + "'";

            }
            if (!_du.getRight3().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "RIGHT_3 = '" + _du.getRight3() + "'"; ;

            }
            if (!_du.getRight4().Equals(""))
            {
                if (!setString.Equals("")) setString = setString + ", ";
                setString = setString + "RIGHT_4 = '" + _du.getRight4() + "'"; ;

            }


            if (setString.Equals("")) return null;

            setString = setString + ", MODIFY_DATE = GETDATE()";

            return preString + setString + whereString;

        }

        public string getMultiWhereString (string _orgData, string sep, string colName) {

            int end = -1; ;
            string curData = "";
            string totalData = "";

            while (true)
            {
                end = -1;
                end = _orgData.IndexOf(sep);
                if (end < 0)
                {
                    curData = _orgData;
                    if (colName.Equals ("ID")) totalData = totalData + "ID = " + curData;
                    else if (colName.Equals("BID")) totalData = totalData + "BID = '" + curData + "'";
                    break;
                }
                else
                {
                    curData = _orgData.Substring(0, end);
                    _orgData = _orgData.Substring(end + 1);
                }

                if (colName.Equals("ID")) totalData = totalData + " ID = " + curData + " or ";
                else if (colName.Equals("BID")) totalData = totalData + " BID = '" + curData + "' or ";
            }

            return totalData;

        }


    }
}
