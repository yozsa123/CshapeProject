using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net;

namespace BMClient
{
    class QueryMaker
    {
        string query = "";

        string delimeter = "^";
        public string getQuery(string type)
        {

            query = "";

            if (type.Equals("Main_ServerInfo_R"))
            {
                query = " select SERVER_NAME , SERVER_TYPE, SERVER_NUM,P_IP";
                query += " from SERVER_CONFIG where SERVER_TYPE < 4";


            }


            if (type.Equals("Main_UserType_S"))
            {
                query = " select USER_TYPE , REMARK";
                query += " from USER_TYPE ";


            }







            if (type.Equals("Sys_001_S"))
            {
                query = " select s.SERVER_NUM , s.SERVER_NAME , t.DESCRIPTION , s.P_IP , s.S_IP , s.SERVER_ID , s.SERVER_PASSWORD , s.DB_NAME , s.DB_ID , s.DB_PASSWORD ,s.DB_HOST , s.DB_HOST2 ,s.DB_SERVICE";
                query += " from SERVER_CONFIG s inner join SERVER_TYPE_CODE t on s.SERVER_TYPE = t.ID";


            }





            if (type.Equals("Sys_005_S"))
            {
                query = " select distinct PROGRAM_ID,REMARK from PROGRAM_LIST";
                query += " where PROGRAM_ID != 'BMS001' and PROGRAM_ID != 'BMS002'";


            }


            if (type.Equals("Sys_008_S"))
            {
                query = " SELECT IP_ADDRESS, DESCRIPTION , CONNECT_ID , IN_OUT_DATE ";
                query += "FROM CLIENT_CONFIG ";



            }

            if (type.Equals("PP_Department_R"))
            {
                query = " select id,description";
                query += " from department order by description";


            }


            if (type.Equals("PP_Card_Type_R"))
            {
                query = " select SERVER_NAME , SERVER_TYPE";
                query += " from SERVER_CONFIG where SERVER_TYPE < 4";


            }

            if (type.Equals("PP_Card_Stat_R"))
            {
                query = " select * from badgests";



            }

            if (type.Equals("OG_Department_R"))
            {
                query = " select ID , NAME";
                query += " from DEPT";


            }

            if (type.Equals("OG_Division_R"))
            {
                query = " select ID , NAME";
                query += " from DIVISION order by NAME";


            }

            if (type.Equals("OG_Title_R"))
            {
                query = " select ID , NAME";
                query += " from TITLE";

            }

            if (type.Equals("OG_Card_Type_R"))
            {
                query = " select ID , NAME";
                query += " from BADGETYP";


            }

            if (type.Equals("OG_Card_Stat_R"))
            {
                query = " select ID , NAME";
                query += " from BADGESTAT";

            }



            if (type.Equals("Bm_CardCount_R"))
            {
                query = " select 'BADGE' ,count(*) from BADGE";

            }

            if (type.Equals("PP_CardCount_R"))
            {
                query = "select 'BADGE' ,count(*) from badge";



            }

            if (type.Equals("OG_CardCount_R"))
            {
                query = " select 'BADGE' ,count(*) from BADGE";



            }


            if (type.Equals("Bm_Auth_R"))
            {
                query = " select ID , DESCRIPTION from ACCESSLVL ";



            }

            if (type.Equals("PP_Auth_R"))
            {
                query = " select id , description from category";



            }

            if (type.Equals("OG_Auth_R"))
            {
                query = " select ACCESSLVID , DESCRIPT from ACCESSLVL";



            }
            Log.WriteLog("query = " + query);
            return query;
        }

        public string getQuery(string type, string param)
        {
            query = "";
            if (type.Equals("SYSTEM"))
            {
                query = "SELECT * FROM ( "
                         + "SELECT ID, User_Name, User_Password, User_Type "
                         + "FROM USER_MASTER "
                         + "WHERE ID = '" + param + "' ) A "
                         + "LEFT JOIN ( "
                         + "SELECT DISTINCT USER_TYPE, (SELECT PROGRAM_ID + ',' FROM USER_AUTH  "
                         + "WHERE USER_TYPE = F.USER_TYPE FOR XML PATH('')) AS Program_List "
                         + "FROM USER_AUTH F ) B "
                         + "ON A.User_Type = B.User_Type ";

            }
            else if (type.Equals("LOGIN_LOG"))
            {
                string result = "성공";

                if (result.Equals("1")) result = "실패";

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS001','1',GETDATE(),'" + result + "',' 로그인')";

            }
            else if (type.Equals("Sys_001_D_LOG"))
            {
                string result = "성공";
                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS003','1',GETDATE(),'" + result + "','" + "ACSID :" + param + " 삭제')";
            }
            else if (type.Equals("FP_REG_LOG"))
            {
                string result = "성공";

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS011','1',GETDATE(),'" + result + "','카드번호 : " + param + " 지문등록')";

            }
            else if (type.Equals("REG001_LOG_D"))
            {
                string result = "성공";

                if (result.Equals("1")) result = "실패";

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS011','1',GETDATE(),'" + result + "','카드번호 : " +param +" 삭제')";

            }

            else if (type.Equals("PP_DELETE_LOG"))
            {
                string result = "성공";

                if (result.Equals("1")) result = "실패";

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS011','1',GETDATE(),'" + result + "','" +param +"')";

            }


                


            else if (type.Equals("Sys_001_D"))
            {
                query = "delete from SERVER_CONFIG where SERVER_NUM = " + param;

            }

            else if (type.Equals("Sys_004_S"))
            {
                query = " SELECT Ip_Address, Description ";
                query += "FROM CLIENT_CONFIG where Ip_Address like '%" + param + "%'";
            }

            else if (type.Equals("Sys_004_D"))
            {
                query = "delete from CLIENT_CONFIG where Ip_Address = '" + param + "'";

            }

            else if (type.Equals("Sys_005_Auth_S"))
            {
                query = "select distinct a.USER_TYPE , a.Program_Id,p.REMARK ";
                query += "FROM USER_AUTH a "
                            + "inner join PROGRAM_LIST p on p.PROGRAM_ID = a.PROGRAM_ID "
                            + "WHERE a.User_Type = '" + param + "' ";


            }

            else if (type.Equals("Sys_006_Type_D"))
            {
                query = "delete from USER_TYPE where USER_TYPE = '" + param + "'";

            }


            else if (type.Equals("Rpt_001_Auth"))
            {
                query = "select ID,DESCRIPTION from ACCESSLVL where PLANT_NUM = " + param + "";

            }

            else if (type.Equals("Rpt_001_NAME_Count"))
            {
                query = "select count(*)"
                      + " from emp e "
                      + " where "
                      + " e.NAME_1 like '%" + param + "%'";
            }

            else if (type.Equals("Rpt_001_SSNO_Count"))
            {
                query = "select count(*)"
                      + "  from emp e "
                      + "  where "
                      + "  e.SSNO like '%" + param + "%'";
            }



            else if (type.Equals("Rpt_001_Department_Count"))
            {
                query = "select count(*)"
                      + "  from emp e  left join department d on e.department =  d.id"
                      + "  where "
                      + "  d.DESCRIPTION like '" + param + "'";
            }



            else if (type.Equals("Rpt_001_Division_Count"))
            {
                query = "select  count(*)"
                      + "  from emp e  left join division di on e.division =  di.id "
                      + "  where "
                      + "  di.DESCRIPTION like '" + param + "'";
            }





            else if (type.Equals("Rpt_001_CardNum_Count"))
            {
                query = "select count(*)"
                      + "  from BADGE b "
                      + "  where "
                      + "  b.BID like '%" + param + "%'";
            }





            else if (type.Equals("Rpt_001_CardType_Count"))
            {
                query = "select count(*)"
                      + "  from BADGE b left join BADGE_TYPE_CODE bt on b.type =  bt.id "
                      + "  where "
                      + "  bt.DESCRIPTION = '" + param + "'";
            }




            else if (type.Equals("Rpt_001_IssueType_Count"))
            {
                query = "select count(*)"
                      + "  from BADGE b left join ISSUE_TYPE_CODE it on b.type =  it.id "
                      + "  where "
                      + "  it.DESCRIPTION = '" + param + "' ";
            }



            else if (type.Equals("Rpt_001_IssueReason_Count"))
            {
                query = "select count(*)"
                      + "  from BADGE b left join ISSUE_REASON_CODE ir on b.type =  ir.id "
                      + "  where "
                      + "  ir.DESCRIPTION = '" + param + "' ";
            }


            else if (type.Equals("Rpt_001_AUTH2_Count"))
            {
                query = " select  count(*)"
                      + "  from BADGE b "
                      + "  where"
                      + "  b.RIGHT_2 like '%" + param + "%' ";
            }
            else if (type.Equals("Rpt_001_AUTH3_Count"))
            {
                query = " select  count(*)"
                      + "  from BADGE b "
                      + "  where "
                      + "  b.RIGHT_3 like '%" + param + "%' ";
            }
            else if (type.Equals("Rpt_001_AUTH4_Count"))
            {
                query = " select  count(*)"
                      + "  from BADGE b "
                      + "  where"
                      + "  b.RIGHT_4 like '%" + param + "%' ";
            }

            else if (type.Equals("SYS007_LOG_M"))
            {

                string result = "성공";

                if (param.Equals("0")) result = "실패";


                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS009','1',GETDATE(),'" + result + "','" + Bm_Login.login_Id + " 사용자 비밀번호수정')";

            }

            else if (type.Equals("REG001_LOG_MM"))
            {


                string result = "성공";

                if (param.Equals("0")) result = "실패";

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS011','1',GETDATE(),'" + result + "',' 일괄변경 작업요청')";


            }

            else if (type.Equals("EndDateSetting_LOG"))
            {

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS011','1',GETDATE(),'" + "성공" + "','" + param + "')";

            }

            else if (type.Equals("RE_REG_LOG"))
            {

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS011','1',GETDATE(),'" + "성공" + "','카드번호 : " + param + " 재등록')";

            }








            Log.WriteLog("query = " + query);
            return query;
        }

        public string getQuery(string type, string param1, string param2)
        {
            query = "";

            if (type.Equals("REG_LOG_N"))
            {


                string result = "성공";

                if (param1.Equals("0")) result = "실패";

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS011','1',GETDATE(),'" + result + "','" + param2 + " 신규카드등록')";

            }

            else if (type.Equals("REG_LOG_M"))
            {


                string result = "성공";

                if (param1.Equals("0")) result = "실패";

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS011','1',GETDATE(),'" + result + "','" + param2 + " 카드수정')";

            }

            else if (type.Equals("REG_LOG_R"))
            {


                string result = "성공";

                if (param1.Equals("0")) result = "실패";

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS011','1',GETDATE(),'" + result + "','" + param2 + " 재등록')";

            }


            else if (type.Equals("SYS002_LOG_N"))
            {


                string result = "성공";

                if (param1.Equals("0")) result = "실패";

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS004','1',GETDATE(),'" + result + "','" + param2 + " 사용자 추가')";

            }

            else if (type.Equals("SYS002_LOG_M"))
            {


                string result = "성공";

                if (param1.Equals("0")) result = "실패";

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS004','1',GETDATE(),'" + result + "','" + param2 + " 사용자 수정')";

            }

            else if (type.Equals("SYS002_LOG_D"))
            {


                string result = "성공";

                if (param1.Equals("0")) result = "실패";

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS004','1',GETDATE(),'" + result + "','" + param2 + " 사용자 삭제')";

            }



            else if (type.Equals("Sys_006_S"))
            {


                query = "select USER_TYPE , REMARK from USER_TYPE where   USER_TYPE like '%"
                                + param1 + "%' and "
                                + "REMARK like '%" + param2 + "%'";


            }

            else if (type.Equals("SYS004_LOG_N"))
            {


                string result = "성공";

                if (param1.Equals("0")) result = "실패";

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS006','1',GETDATE(),'" + result + "','" + param2 + " IP 추가')";

            }

            else if (type.Equals("SYS004_LOG_M"))
            {


                string result = "성공";

                if (param1.Equals("0")) result = "실패";

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS006','1',GETDATE(),'" + result + "','" + param2 + "  수정')";

            }

            else if (type.Equals("SYS004_LOG_D"))
            {


                string result = "성공";

                if (param1.Equals("0")) result = "실패";

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS006','1',GETDATE(),'" + result + "','" + param2 + " IP 삭제')";

            }

            else if (type.Equals("SYS005_LOG_U"))
            {


                string result = "성공";

                if (param1.Equals("0")) result = "실패";

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS007','1',GETDATE(),'" + result + "','" + param2 + " 권한 변경')";

            }

            else if (type.Equals("SYS006_LOG_N"))
            {


                string result = "성공";

                if (param1.Equals("0")) result = "실패";

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS008','1',GETDATE(),'" + result + "','" + param2 + " 사용자 유형추가')";

            }

            else if (type.Equals("SYS006_LOG_M"))
            {


                string result = "성공";

                if (param1.Equals("0")) result = "실패";

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS008','1',GETDATE(),'" + result + "','" + param2 + " 사용자 유형수정')";

            }

            else if (type.Equals("SYS006_LOG_D"))
            {


                string result = "성공";

                if (param1.Equals("0")) result = "실패";

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS008','1',GETDATE(),'" + result + "','" + param2 + " 사용자 유형삭제')";

            }


            else if (type.Equals("REG001_LOG_N"))
            {


                string result = "성공";

                if (param1.Equals("0")) result = "실패";

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS011','1',GETDATE(),'" + result + "','카드번호:" + param2 + " 신규등록')";



            }

            else if (type.Equals("REG001_LOG_M"))
            {


                string result = "성공";

                if (param1.Equals("0")) result = "실패";

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS011','1',GETDATE(),'" + result + "','카드번호:" + param2 + " 정보수정')";


            }

            else if (type.Equals("REG001_LOG_PIN"))
            {


                string result = "성공";

                if (param1.Equals("0")) result = "실패";

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS011','1',GETDATE(),'" + result + "','" + param2 + " 등록')";

            }


            else if (type.Equals("REG001_LOG_REG"))
            {


                string result = "성공";

                if (param1.Equals("0")) result = "실패";

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS011','1',GETDATE(),'" + result + "','" + param2 + " 재등록')";

            }
            else if (type.Equals("PLANT_REG_LOG"))
            {


                string result = "성공";

                if (param1.Equals("0")) result = "실패";

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS011','1',GETDATE(),'" + result + "','" + param2 + " 등록')";

            }


            else if (type.Equals("REG002_LOG_N"))
            {


                string result = "성공";

                if (param1.Equals("0")) result = "실패";

                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS012','1',GETDATE(),'" + result + "','" + param2 + " 신규등록')";



            }






            else if (type.Equals("Sys_002_S"))
            {
                query = " SELECT A.Id,  A.User_Name ,  B.Remark ";
                query += "FROM ( ";
                query += "SELECT SEQ, Id, User_Name, User_Password, User_Type ";
                query += "FROM User_Master ";
                query += "  ) A ";
                query += "LEFT JOIN User_Type B ";
                query += "ON A.User_Type = B.User_Type where ID like '%" + param1 + "%' and USER_NAME like '%" + param2 + "%'";
            }

            else if (type.Equals("Bm_ACS1AccessLvl_R"))
            {
                query = " SELECT ID, DESCRIPTION ";
                query += "FROM ACCESSLVL "
                            + "where PLANT_NUM = 1 order by DESCRIPTION ";

            }

            else if (type.Equals("Bm_ACS2AccessLvl_R"))
            {
                query = " SELECT ID, DESCRIPTION ";
                query += "FROM ACCESSLVL "
                            + "where PLANT_NUM = 2 order by DESCRIPTION";

            }

            else if (type.Equals("Bm_ACS3AccessLvl_R"))
            {
                query = " SELECT ID, DESCRIPTION ";
                query += "FROM ACCESSLVL "
                            + "where PLANT_NUM = 3 order by  DESCRIPTION";

            }

            else if (type.Equals("Bm_ACS4AccessLvl_R"))
            {
                query = " SELECT ID, DESCRIPTION ";
                query += "FROM ACCESSLVL "
                            + "where PLANT_NUM = 4 order by  DESCRIPTION";

            }


            else if (type.Equals("Bm_Department_R"))
            {
                query = " SELECT * ";
                query += "FROM DEPARTMENT "
                            + "where ID like '%" + param1 + "%' and DESCRIPTION like '%"
                            + param2 + "%' order by DESCRIPTION ASC";



            }

            else if (type.Equals("Bm_Division_R"))
            {
                query = " SELECT * ";
                query += "FROM DIVISION "
                            + "where ID like '%" + param1 + "%' and DESCRIPTION like '%"
                            + param2 + "%' order by DESCRIPTION ASC";



            }

            else if (type.Equals("Bm_Title_R"))
            {
                query = " SELECT * ";
                query += "FROM TITLE "
                            + "where ID like '%" + param1 + "%' and DESCRIPTION like '%"
                            + param2 + "%' order by DESCRIPTION ASC";



            }

            else if (type.Equals("Bm_Card_Type_R"))
            {
                query = " SELECT * ";
                query += "FROM BADGE_TYPE_CODE "
                            + "where ID like '%" + param1 + "%' and DESCRIPTION like '%"
                            + param2 + "%' order by ID ASC";



            }

            else if (type.Equals("Bm_Card_Stat_R"))
            {
                query = " SELECT * ";
                query += "FROM BADGE_STAT_CODE "
                            + "where ID like '%" + param1 + "%' and DESCRIPTION like '%"
                            + param2 + "%' order by ID ASC";


            }

            else if (type.Equals("Bm_Card_Format_R"))
            {
                query = " SELECT * ";
                query += "FROM BADGE_FORMAT_CODE "
                            + "where ID like '%" + param1 + "%' and DESCRIPTION like '%"
                            + param2 + "%' order by ID ASC";


            }

            else if (type.Equals("Bm_Issue_Type_R"))
            {
                query = " SELECT * ";
                query += "FROM ISSUE_TYPE_CODE "
                            + "where ID like '%" + param1 + "%' and DESCRIPTION like '%"
                            + param2 + "%' order by ID ASC";



            }

            else if (type.Equals("Bm_Issue_Reason_R"))
            {
                query = " SELECT * ";
                query += "FROM ISSUE_REASON_CODE "
                            + "where ID like '%" + param1 + "%' and DESCRIPTION like '%"
                            + param2 + "%' order by ID ASC";
            }

            else if (type.Equals("Sys_007_U"))
            {
                query = "update USER_MASTER set "
                      + " USER_PASSWORD = '" + param2 + "' "
                      + " where ID = '" + param1 + "'";

            }



            else if (type.Equals("Rpt_005_Count"))
            {
                query = " SELECT count(*)";
                query += " FROM USER_ACTION_HISTORY "
                            + "where ACTION_TIME between '" + param1 + "' and '" + param2 + "'";
            }





            else if (type.Equals("Rpt_001_Date_Count"))
            {
                query = "select count(*)"
                      + " from BADGE b "
                       + " where"
                       + " b.DEACTIVATE_DATE < '" + param2 + "' ";
            }

            else if (type.Equals("Rpt_001_NAME"))
            {
                query = "select top 800 b.SEQ as 순번 ,e.SSNO as 사번 ,e.NAME_1 as 이름 "
                     + " ,'성별' = case e.GENDER when 1 then '남자' when 2 then '여자' end , "
                     + " substring(convert(char(10),e.BIRTH_DATE,23),1,10) as 생년월일, "
                     + " d.DESCRIPTION as 소속 , di.DESCRIPTION  as 부서, "
                     + "t.DESCRIPTION as 직급 ,e.EMAIL as Email , e.TEL as 연락처"
                     + " ,e.ADDRESS as 주소 ,b.BID as 카드번호 "
                     + " ,b.RIGHT_1 as '1발권한',b.RIGHT_2 as '2발권한' ,b.RIGHT_3 as '3발권한' "
                     + " ,b.ACTIVATE_DATE as 유효시작일 ,b.DEACTIVATE_DATE as 유효만료일 "
                     + " ,bt.DESCRIPTION as 카드종류,bs.DESCRIPTION as 카드상태 ,ir.DESCRIPTION as 발급사유,it.DESCRIPTION as 발급유형 from BADGE b  "
                     + " left join EMP e on e.ID = b.EMPID"
                     + " left join DEPARTMENT d on d.ID = e.DEPARTMENT"
                     + " left join DIVISION di on di.ID = e.DIVISION"
                     + " left join title t on t.ID = e.TITLE"
                     + " left join BADGE_TYPE_CODE bt  on b.TYPE =  bt.ID"
                     + " left join ISSUE_REASON_CODE ir  on b.ISSUE_REASON =  ir.ID"
                     + " left join ISSUE_TYPE_CODE it  on b.ISSUE_TYPE =  it.ID"
                     + " left join BADGE_STAT_CODE bs on b.STATUS_1 = bs.ID"
                     + " where e.NAME_1 like '%" + param1 + "%' and b.SEQ > " + param2
                     + " order by SEQ";

                 
            }

            else if (type.Equals("Rpt_001_SSNO"))
            {

                query = "select top 800 b.SEQ as 순번 ,e.SSNO as 사번 ,e.NAME_1 as 이름 "
                     + " ,'성별' = case e.GENDER when 1 then '남자' when 2 then '여자' end , "
                     + " substring(convert(char(10),e.BIRTH_DATE,23),1,10) as 생년월일, "
                     + " d.DESCRIPTION as 소속 , di.DESCRIPTION  as 부서, "
                     + "t.DESCRIPTION as 직급 ,e.EMAIL as Email , e.TEL as 연락처"
                     + " ,e.ADDRESS as 주소 ,b.BID as 카드번호 "
                     + " ,b.RIGHT_1 as '1발권한',b.RIGHT_2 as '2발권한' ,b.RIGHT_3 as '3발권한' "
                     + " ,b.ACTIVATE_DATE as 유효시작일 ,b.DEACTIVATE_DATE as 유효만료일 "
                     + " ,bt.DESCRIPTION as 카드종류,bs.DESCRIPTION as 카드상태 ,ir.DESCRIPTION as 발급사유,it.DESCRIPTION as 발급유형 from BADGE b  "
                     + " left join EMP e on e.ID = b.EMPID"
                     + " left join DEPARTMENT d on d.ID = e.DEPARTMENT"
                     + " left join DIVISION di on di.ID = e.DIVISION"
                     + " left join title t on t.ID = e.TITLE"
                     + " left join BADGE_TYPE_CODE bt  on b.TYPE =  bt.ID"
                     + " left join ISSUE_REASON_CODE ir  on b.ISSUE_REASON =  ir.ID"
                     + " left join ISSUE_TYPE_CODE it  on b.ISSUE_TYPE =  it.ID"
                     + " left join BADGE_STAT_CODE bs on b.STATUS_1 = bs.ID"
                     + " where e.SSNO like '%" + param1 + "%' and  b.SEQ > " + param2
                     + " order by SEQ";


            }

            else if (type.Equals("Rpt_001_Department"))
            {

                query = "select top 800 b.SEQ as 순번 ,e.SSNO as 사번 ,e.NAME_1 as 이름 "
                     + " ,'성별' = case e.GENDER when 1 then '남자' when 2 then '여자' end , "
                     + " substring(convert(char(10),e.BIRTH_DATE,23),1,10) as 생년월일, "
                     + " d.DESCRIPTION as 소속 , di.DESCRIPTION  as 부서, "
                     + "t.DESCRIPTION as 직급 ,e.EMAIL as Email , e.TEL as 연락처"
                     + " ,e.ADDRESS as 주소 ,b.BID as 카드번호 "
                     + " ,b.RIGHT_1 as '1발권한',b.RIGHT_2 as '2발권한' ,b.RIGHT_3 as '3발권한' "
                     + " ,b.ACTIVATE_DATE as 유효시작일 ,b.DEACTIVATE_DATE as 유효만료일 "
                     + " ,bt.DESCRIPTION as 카드종류,bs.DESCRIPTION as 카드상태 ,ir.DESCRIPTION as 발급사유,it.DESCRIPTION as 발급유형 from BADGE b  "
                     + " left join EMP e on e.ID = b.EMPID"
                     + " left join DEPARTMENT d on d.ID = e.DEPARTMENT"
                     + " left join DIVISION di on di.ID = e.DIVISION"
                     + " left join title t on t.ID = e.TITLE"
                     + " left join BADGE_TYPE_CODE bt  on b.TYPE =  bt.ID"
                     + " left join ISSUE_REASON_CODE ir  on b.ISSUE_REASON =  ir.ID"
                     + " left join ISSUE_TYPE_CODE it  on b.ISSUE_TYPE =  it.ID"
                     + " left join BADGE_STAT_CODE bs on b.STATUS_1 = bs.ID"
                     + " where d.DESCRIPTION = '" + param1 + "' and b.SEQ > " + param2
                     + " order by SEQ";


            }

            else if (type.Equals("Rpt_001_Division"))
            {
                query = "select top 800 b.SEQ as 순번 ,e.SSNO as 사번 ,e.NAME_1 as 이름 "
                     + " ,'성별' = case e.GENDER when 1 then '남자' when 2 then '여자' end , "
                     + " substring(convert(char(10),e.BIRTH_DATE,23),1,10) as 생년월일, "
                     + " d.DESCRIPTION as 소속 , di.DESCRIPTION  as 부서, "
                     + "t.DESCRIPTION as 직급 ,e.EMAIL as Email , e.TEL as 연락처"
                     + " ,e.ADDRESS as 주소 ,b.BID as 카드번호 "
                     + " ,b.RIGHT_1 as '1발권한',b.RIGHT_2 as '2발권한' ,b.RIGHT_3 as '3발권한' "
                     + " ,b.ACTIVATE_DATE as 유효시작일 ,b.DEACTIVATE_DATE as 유효만료일 "
                     + " ,bt.DESCRIPTION as 카드종류,bs.DESCRIPTION as 카드상태 ,ir.DESCRIPTION as 발급사유,it.DESCRIPTION as 발급유형 from BADGE b  "
                     + " left join EMP e on e.ID = b.EMPID"
                     + " left join DEPARTMENT d on d.ID = e.DEPARTMENT"
                     + " left join DIVISION di on di.ID = e.DIVISION"
                     + " left join title t on t.ID = e.TITLE"
                     + " left join BADGE_TYPE_CODE bt  on b.TYPE =  bt.ID"
                     + " left join ISSUE_REASON_CODE ir  on b.ISSUE_REASON =  ir.ID"
                     + " left join ISSUE_TYPE_CODE it  on b.ISSUE_TYPE =  it.ID"
                     + " left join BADGE_STAT_CODE bs on b.STATUS_1 = bs.ID"
                     + " where di.DESCRIPTION = '" + param1 + "' and b.SEQ > " + param2
                     + " order by SEQ";


            }
            else if (type.Equals("Rpt_001_CardNum"))
            {
                query = "select top 800 b.SEQ as 순번 ,e.SSNO as 사번 ,e.NAME_1 as 이름 "
                      + " ,'성별' = case e.GENDER when 1 then '남자' when 2 then '여자' end , "
                      + " substring(convert(char(10),e.BIRTH_DATE,23),1,10) as 생년월일, "
                      + " d.DESCRIPTION as 소속 , di.DESCRIPTION  as 부서, "
                      + "t.DESCRIPTION as 직급 ,e.EMAIL as Email , e.TEL as 연락처"
                      + " ,e.ADDRESS as 주소 ,b.BID as 카드번호 "
                      + " ,b.RIGHT_1 as '1발권한',b.RIGHT_2 as '2발권한' ,b.RIGHT_3 as '3발권한' "
                      + " ,b.ACTIVATE_DATE as 유효시작일 ,b.DEACTIVATE_DATE as 유효만료일 "
                      + " ,bt.DESCRIPTION as 카드종류,bs.DESCRIPTION as 카드상태 ,ir.DESCRIPTION as 발급사유,it.DESCRIPTION as 발급유형 from BADGE b  "
                      + " left join EMP e on e.ID = b.EMPID"
                      + " left join DEPARTMENT d on d.ID = e.DEPARTMENT"
                      + " left join DIVISION di on di.ID = e.DIVISION"
                      + " left join title t on t.ID = e.TITLE"
                      + " left join BADGE_TYPE_CODE bt  on b.TYPE =  bt.ID"
                      + " left join ISSUE_REASON_CODE ir  on b.ISSUE_REASON =  ir.ID"
                      + " left join ISSUE_TYPE_CODE it  on b.ISSUE_TYPE =  it.ID"
                      + " left join BADGE_STAT_CODE bs on b.STATUS_1 = bs.ID"
                      + " where b.BID like '%" + param1 + "%' and b.SEQ > " + param2
                      + " order by SEQ";


            }

            else if (type.Equals("Rpt_001_CardType"))
            {

                query = "select top 800 b.SEQ as 순번 ,e.SSNO as 사번 ,e.NAME_1 as 이름 "
                      + " ,'성별' = case e.GENDER when 1 then '남자' when 2 then '여자' end , "
                      + " substring(convert(char(10),e.BIRTH_DATE,23),1,10) as 생년월일, "
                      + " d.DESCRIPTION as 소속 , di.DESCRIPTION  as 부서, "
                      + "t.DESCRIPTION as 직급 ,e.EMAIL as Email , e.TEL as 연락처"
                      + " ,e.ADDRESS as 주소 ,b.BID as 카드번호 "
                      + " ,b.RIGHT_1 as '1발권한',b.RIGHT_2 as '2발권한' ,b.RIGHT_3 as '3발권한' "
                      + " ,b.ACTIVATE_DATE as 유효시작일 ,b.DEACTIVATE_DATE as 유효만료일 "
                      + " ,bt.DESCRIPTION as 카드종류,bs.DESCRIPTION as 카드상태 ,ir.DESCRIPTION as 발급사유,it.DESCRIPTION as 발급유형 from BADGE b  "
                      + " left join EMP e on e.ID = b.EMPID"
                      + " left join DEPARTMENT d on d.ID = e.DEPARTMENT"
                      + " left join DIVISION di on di.ID = e.DIVISION"
                      + " left join title t on t.ID = e.TITLE"
                      + " left join BADGE_TYPE_CODE bt  on b.TYPE =  bt.ID"
                      + " left join ISSUE_REASON_CODE ir  on b.ISSUE_REASON =  ir.ID"
                      + " left join ISSUE_TYPE_CODE it  on b.ISSUE_TYPE =  it.ID"
                      + " left join BADGE_STAT_CODE bs on b.STATUS_1 = bs.ID"
                      + " where bt.DESCRIPTION = '" + param1 + "' and b.SEQ > " + param2
                      + " order by SEQ";


            }

            else if (type.Equals("Rpt_001_IssueType"))
            {
                query = "select top 800 b.SEQ as 순번 ,e.SSNO as 사번 ,e.NAME_1 as 이름 "
                      + " ,'성별' = case e.GENDER when 1 then '남자' when 2 then '여자' end , "
                      + " substring(convert(char(10),e.BIRTH_DATE,23),1,10) as 생년월일, "
                      + " d.DESCRIPTION as 소속 , di.DESCRIPTION  as 부서, "
                      + "t.DESCRIPTION as 직급 ,e.EMAIL as Email , e.TEL as 연락처"
                      + " ,e.ADDRESS as 주소 ,b.BID as 카드번호 "
                      + " ,b.RIGHT_1 as '1발권한',b.RIGHT_2 as '2발권한' ,b.RIGHT_3 as '3발권한' "
                      + " ,b.ACTIVATE_DATE as 유효시작일 ,b.DEACTIVATE_DATE as 유효만료일 "
                      + " ,bt.DESCRIPTION as 카드종류,bs.DESCRIPTION as 카드상태 ,ir.DESCRIPTION as 발급사유,it.DESCRIPTION as 발급유형 from BADGE b  "
                      + " left join EMP e on e.ID = b.EMPID"
                      + " left join DEPARTMENT d on d.ID = e.DEPARTMENT"
                      + " left join DIVISION di on di.ID = e.DIVISION"
                      + " left join title t on t.ID = e.TITLE"
                      + " left join BADGE_TYPE_CODE bt  on b.TYPE =  bt.ID"
                      + " left join ISSUE_REASON_CODE ir  on b.ISSUE_REASON =  ir.ID"
                      + " left join ISSUE_TYPE_CODE it  on b.ISSUE_TYPE =  it.ID"
                      + " left join BADGE_STAT_CODE bs on b.STATUS_1 = bs.ID"
                      + " where it.DESCRIPTION = '" + param1 + "' and b.SEQ > " + param2
                      + " order by SEQ";


            }

            else if (type.Equals("Rpt_001_IssueReason"))
            {
                query = "select top 800 b.SEQ as 순번 ,e.SSNO as 사번 ,e.NAME_1 as 이름 "
                      + " ,'성별' = case e.GENDER when 1 then '남자' when 2 then '여자' end , "
                      + " substring(convert(char(10),e.BIRTH_DATE,23),1,10) as 생년월일, "
                      + " d.DESCRIPTION as 소속 , di.DESCRIPTION  as 부서, "
                      + "t.DESCRIPTION as 직급 ,e.EMAIL as Email , e.TEL as 연락처"
                      + " ,e.ADDRESS as 주소 ,b.BID as 카드번호 "
                      + " ,b.RIGHT_1 as '1발권한',b.RIGHT_2 as '2발권한' ,b.RIGHT_3 as '3발권한' "
                      + " ,b.ACTIVATE_DATE as 유효시작일 ,b.DEACTIVATE_DATE as 유효만료일 "
                      + " ,bt.DESCRIPTION as 카드종류,bs.DESCRIPTION as 카드상태 ,ir.DESCRIPTION as 발급사유,it.DESCRIPTION as 발급유형 from BADGE b  "
                      + " left join EMP e on e.ID = b.EMPID"
                      + " left join DEPARTMENT d on d.ID = e.DEPARTMENT"
                      + " left join DIVISION di on di.ID = e.DIVISION"
                      + " left join title t on t.ID = e.TITLE"
                      + " left join BADGE_TYPE_CODE bt  on b.TYPE =  bt.ID"
                      + " left join ISSUE_REASON_CODE ir  on b.ISSUE_REASON =  ir.ID"
                      + " left join ISSUE_TYPE_CODE it  on b.ISSUE_TYPE =  it.ID"
                      + " left join BADGE_STAT_CODE bs on b.STATUS_1 = bs.ID"
                      + " where ir.DESCRIPTION = '" + param1 + "'" + " and b.SEQ > " + param2
                      + " order by SEQ";
            }






            Log.WriteLog("query = " + query);
            return query;

        }

        public string getQuery(string type, string param1, string param2, string param3, string param4)
        {
            string query = "";


            if (type.Equals("EMP_R"))
            {
                query = " select e.ID , e.NAME_1 , e.DEPARTMENT ";
                query += "FROM EMP "
                            + "where ID like '%" + param1 + "%' or NAME_1 like '%"
                            + param2 + "%' order by ID ASC";



            }


            else if (type.Equals("Rpt_001_Date"))
            {
                query = "select top 800 b.SEQ as 순번 ,e.SSNO as 사번 ,e.NAME_1 as 이름 "
                      + " ,'성별' = case e.GENDER when 1 then '남자' when 2 then '여자' end , "
                      + " substring(convert(char(10),e.BIRTH_DATE,23),1,10) as 생년월일, "
                      + " d.DESCRIPTION as 소속 , di.DESCRIPTION  as 부서, "
                      + "t.DESCRIPTION as 직급 ,e.EMAIL as Email , e.TEL as 연락처"
                      + " ,e.ADDRESS as 주소 ,b.BID as 카드번호 "
                      + " ,b.RIGHT_1 as '1발권한',b.RIGHT_2 as '2발권한' ,b.RIGHT_3 as '3발권한' "
                      + " ,b.ACTIVATE_DATE as 유효시작일 ,b.DEACTIVATE_DATE as 유효만료일 "
                      + " ,bt.DESCRIPTION as 카드종류,bs.DESCRIPTION as 카드상태 ,ir.DESCRIPTION as 발급사유,it.DESCRIPTION as 발급유형 from BADGE b  "
                      + " left join EMP e on e.ID = b.EMPID"
                      + " left join DEPARTMENT d on d.ID = e.DEPARTMENT"
                      + " left join DIVISION di on di.ID = e.DIVISION"
                      + " left join title t on t.ID = e.TITLE"
                      + " left join BADGE_TYPE_CODE bt  on b.TYPE =  bt.ID"
                      + " left join ISSUE_REASON_CODE ir  on b.ISSUE_REASON =  ir.ID"
                      + " left join ISSUE_TYPE_CODE it  on b.ISSUE_TYPE =  it.ID"
                      + " left join BADGE_STAT_CODE bs on b.STATUS_1 = bs.ID"
                      + " where b.DEACTIVATE_DATE < '" + param2 + "' and b.SEQ > " + param3
                      + " order by SEQ";


            }

           




            Log.WriteLog("query = " + query);
            return query;


        }

        public string getQuery(string type, Dictionary<string, string> getData)
        {
            query = "";

            if (type.Equals("Sys_001_I"))
            {
                query = " insert into SERVER_CONFIG(SERVER_NUM,SERVER_NAME,SERVER_TYPE,SERVER_ID,SERVER_PASSWORD,P_IP,S_IP,"
                                    + "DB_NAME , DB_ID , DB_PASSWORD , DB_HOST ,DB_HOST2 ,DB_SERVICE )"
                                    + " values ( " + getData["ACSID"] + ","
                                    + "'" + getData["SERVER_NAME"] + "' , "
                                    + getData["SERVER_TYPE"] + " , "
                                    + "'" + getData["SERVER_ID"] + "' , "
                                    + "'" + getData["SERVER_PASSWORD"] + "' , "
                                    + "'" + getData["MAIN_IP"] + "' , "
                                    + "'" + getData["SUB_IP"] + "' , "
                                    + "'" + getData["DB_NAME"] + "' , "
                                    + "'" + getData["DB_ID"] + "' , "
                                    + "'" + getData["DB_PASSWORD"] + "' , "
                                    + "'" + getData["DB_HOST"] + "' , "
                                    + "'" + getData["DB_HOST2"] + "' , "
                                    + "'" + getData["DB_SERVICE"] + "' ) ";




            }


            else if (type.Equals("Sys_002_I"))
            {
                query = "insert into USER_MASTER(ID , USER_NAME ,USER_PASSWORD,USER_TYPE,CREATE_ID,CREATE_DATE) values("
                            + "'" + getData["ID"] + "', "
                            + "'" + getData["USER_NAME"] + "', "
                            + "'" + getData["USER_PASSWORD"] + "', "
                            + "'" + getData["USER_TYPE"] + "', "
                            + "'" + getData["CREATE_ID"] + "', "
                            + "getdate())";
            }


            else if (type.Equals("Sys_002_U"))
            {
                query = "update USER_MASTER set "
                      + " USER_NAME = '" + getData["USER_NAME"] + "' ,"
                      + " USER_PASSWORD = '" + getData["USER_PASSWORD"] + "' ,"
                      + " USER_TYPE = '" + getData["USER_TYPE"] + "' "
                      + " where ID = '" + getData["ID"] + "'";
            }

            else if (type.Equals("Sys_002_D"))
            {
                query = "delete from USER_MASTER where ID='" + getData["ID"] + "'";
            }


            else if (type.Equals("Sys_004_I"))
            {
                query = "insert into CLIENT_CONFIG(IP_ADDRESS , DESCRIPTION ,CREATE_ID,CREATE_DATE) values("
                            + "'" + getData["IP_ADDRESS"] + "', "
                            + "'" + getData["DESCRIPTION"] + "', "
                            + "'" + getData["CREATE_ID"] + "', "
                            + "getdate())";
            }






            else if (type.Equals("Sys_006_I"))
            {
                query = "insert into USER_TYPE(USER_TYPE , REMARK ,CREATE_ID,CREATE_DATE) values("
                            + "'" + getData["USER_TYPE"] + "', "
                            + "'" + getData["REMARK"] + "', "
                            + "'" + getData["CREATE_ID"] + "', "
                            + "getdate())";
            }
            else if (type.Equals("Sys_006_Type_U"))
            {
                query = "update USER_TYPE set "
                      + " USER_TYPE = '" + getData["USER_TYPE"] + "' ,"
                      + " REMARK = '" + getData["REMARK"] + "' ,"
                      + " UPDATE_ID = '" + getData["UPDATE_ID"] + "' ,"
                      + " UPDATE_DATE = getdate() "
                      + " where USER_TYPE = '" + getData["CURRENT_TYPE_ID"] + "'";


            }

            else if (type.Equals("Rpt_001_AUTH_Count"))
            {
                query = " select  count(*)"
                      + "  from BADGE b"
                      + "  where   "
                      + "  b.RIGHT_1 like '%" + getData["right1"] + "%' and b.RIGHT_2 like '%" + getData["right2"] + "%' and b.RIGHT_3 like '%" + getData["right3"] + "%' and b.RIGHT_4 like '%" + getData["right4"] + "%' ";

                if (getData["STAT"].Equals("활성")) query += " and b.STATUS_1 =0";
                if (getData["STAT"].Equals("비활성")) query += " and b.STATUS_1 !=0";
            }

            else if (type.Equals("Rpt_001_AUTH"))
            {
                query = "select top 800 b.SEQ as 순번 ,e.SSNO as 사번 ,e.NAME_1 as 이름 "
                      + " ,'성별' = case e.GENDER when 1 then '남자' when 2 then '여자' end , "
                      + " substring(convert(char(10),e.BIRTH_DATE,23),1,10) as 생년월일, "
                      + " d.DESCRIPTION as 소속 , di.DESCRIPTION  as 부서, "
                      + "t.DESCRIPTION as 직급 ,e.EMAIL as Email , e.TEL as 연락처"
                      + " ,e.ADDRESS as 주소 ,b.BID as 카드번호 "
                      + " ,b.RIGHT_1 as '1발권한',b.RIGHT_2 as '2발권한' ,b.RIGHT_3 as '3발권한',b.RIGHT_4 as '4발권한' "
                      + " ,b.ACTIVATE_DATE as 유효시작일 ,b.DEACTIVATE_DATE as 유효만료일 "
                      + " ,bt.DESCRIPTION as 카드종류,bs.DESCRIPTION as 카드상태 ,ir.DESCRIPTION as 발급사유,it.DESCRIPTION as 발급유형 from BADGE b  "
                      + " left join EMP e on e.ID = b.EMPID"
                      + " left join DEPARTMENT d on d.ID = e.DEPARTMENT"
                      + " left join DIVISION di on di.ID = e.DIVISION"
                      + " left join title t on t.ID = e.TITLE"
                      + " left join BADGE_TYPE_CODE bt  on b.TYPE =  bt.ID"
                      + " left join ISSUE_REASON_CODE ir  on b.ISSUE_REASON =  ir.ID"
                      + " left join ISSUE_TYPE_CODE it  on b.ISSUE_TYPE =  it.ID"
                      + " left join BADGE_STAT_CODE bs on b.STATUS_1 = bs.ID"
                      + " where b.RIGHT_1 like '%" + getData["right1"] + "%' and b.RIGHT_2 like '%" + getData["right2"] + "%' and b.RIGHT_3 like '%" + getData["right3"] + "%' and b.RIGHT_4 like '%" + getData["right4"] + "%' and SEQ > " + getData["lastSEQ"];
                if (getData["STAT"].Equals("활성")) query += " and b.STATUS_1 =0";
                if (getData["STAT"].Equals("비활성")) query += " and b.STATUS_1 !=0";
                query += " order by SEQ";
            }

            else if (type.Equals("Bm_OH_Maker"))
            {
                query = "select EMP.SSNO as 사번  , EMP.NAME_1 as 이름 ,EMP.DEPARTMENT as 소속 ,EMP.DIVISION as 부서,EMP.TITLE as 직급,EMP.TEL as 전화번호,EMP.EMAIL as 이메일,EMP.ADDRESS as 주소,EMP.BIRTH_DATE as 생일  ,BADGE.BID as 카드번호 from EMP inner join BADGE on EMP.ID = BADGE.EMPID where ";
                foreach (KeyValuePair<string, string> pair in getData)
                {
                    query += "EMP.NAME_1 = '" + pair.Value + "' or ";
                }

                query = query.Remove(query.LastIndexOf("or"));
                query += "order by EMP.NAME_1";
            }

            else if (type.Equals("Rpt_005_Count"))
            {
                
                
                query = " SELECT count(*)";
                query += " FROM USER_ACTION_HISTORY "
                            + "where ACTION_TIME between '" + getData["startDate"] + "' and '" + getData["endDate"] + "'"
                            + "and USER_ID like '%" + getData["txtID"] + "%' and REMARK like '%" + getData["txtRemark"] + "%'";
                if(getData["type"].Equals("2"))  query += " and (ACTION_PROGRAM = 'BMS011' or ACTION_PROGRAM = 'BMS012') ";
                else if (getData["type"].Equals("1")) query += " and (ACTION_PROGRAM != 'BMS011' and ACTION_PROGRAM != 'BMS012') ";
 
                            
            }


            else if (type.Equals("Rpt_005_S"))
            {
                query = " SELECT top 1000 ROWID as SEQ, USER_ID  as 사용자ID, USER_NM as 사용자명 ,IP_ADRESS as IP  , ACTION_PROGRAM as 처리화면 , ACTION_TIME as 처리시간, REMARK as 작업내역";
                query += " FROM USER_ACTION_HISTORY "
                      + "where ACTION_TIME between '" + getData["startDate"] + "' and '" + getData["endDate"] + "'"
                       +"and USER_ID like '%" + getData["txtID"] + "%' and REMARK like '%" + getData["txtRemark"] + "%'";
                if (getData["type"].Equals("2")) query += " and (ACTION_PROGRAM = 'BMS011' or ACTION_PROGRAM = 'BMS012') ";
                else if (getData["type"].Equals("1")) query += " and (ACTION_PROGRAM != 'BMS011' and ACTION_PROGRAM != 'BMS012') ";

            }



            else if (type.Equals("Sys_001_N_LOG"))
            {
                string result = "성공";
                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS003','1',GETDATE(),'" + result + "','" +"ACSID :"+ getData["ACSID"] + " 등록')";
            }

            
            


            Log.WriteLog("query = " + query);
            return query;
        }

        public string getQuery(string type, DataTable getData)
        {
            query = "";

            if (type.Equals("Sys_001_U"))
            {
                for (int i = 0; i < getData.Rows.Count; i++)
                {
                    query += "update SERVER_CONFIG set SERVER_NUM = " + getData.Rows[i][0]
                          + " ,SERVER_NAME = '" + getData.Rows[i][1] + "'"
                          + " ,SERVER_TYPE = " + getData.Rows[i][2]
                          + " ,P_IP = '" + getData.Rows[i][3] + "'"
                          + " ,S_IP = '" + getData.Rows[i][4] + "'"
                          + " ,SERVER_ID = '" + getData.Rows[i][5] + "'"
                          + " ,SERVER_PASSWORD = '" + getData.Rows[i][6] + "'"
                          + " ,DB_NAME = '" + getData.Rows[i][7] + "'"
                          + " ,DB_ID = '" + getData.Rows[i][8] + "'"
                          + " ,DB_PASSWORD = '" + getData.Rows[i][9] + "'"
                          + " ,DB_HOST = '" + getData.Rows[i][10] + "'"
                          + " ,DB_HOST2 = '" + getData.Rows[i][11] + "'"
                          + " ,DB_SERVICE = '" + getData.Rows[i][12] + "' where SERVER_NUM= " + getData.Rows[i][13] + delimeter;
                }
            }


            else if (type.Equals("Sys_001_M_LOG"))
            {
                string result = "성공";
                query = "insert into USER_ACTION_HISTORY values('" + Bm_Login.login_Id + "','" + Bm_Login.login_Name + "','" + getIp() + "',1,'BMS003','1',GETDATE(),'" + result + "','" + "ACSID :"; 
                for (int i = 0; i < getData.Rows.Count; i++)
                {
                    query += getData.Rows[i][0] + " , "; 
                }
                query += "  수정')";
            }

            else if (type.Equals("Sys_004_U"))
            {
                for (int i = 0; i < getData.Rows.Count; i++)
                {
                    query += "update CLIENT_CONFIG set IP_ADDRESS = '" + getData.Rows[i][0] + "'"
                          + " ,DESCRIPTION = '" + getData.Rows[i][1] + "'"
                          + " ,UPDATE_ID = '" + getData.Rows[i][3] + "'"
                          + " ,UPDATE_DATE = getdate()"
                          + " where IP_ADDRESS= '" + getData.Rows[i][2] + "'" + delimeter;
                }
            }

            else if (type.Equals("Sys_005_Auth_U"))
            {

                query = "delete from USER_AUTH  where USER_TYPE = '" + getData.Rows[0][1] + "'" + delimeter;

                for (int i = 0; i < getData.Rows.Count; i++)
                {
                    query += "insert into USER_AUTH(PROGRAM_ID,USER_TYPE,CREATE_ID,CREATE_DATE) "
                          + "values('" + getData.Rows[i][0] + "' , "
                          + "'" + getData.Rows[i][1] + "' , "
                          + "'" + Bm_Login.login_Id + "' , "
                          + "getdate())" + delimeter;
                }
            }
            else if (type.Equals("Bm_Department_I"))
            {

                for (int i = 0; i < getData.Rows.Count; i++)
                {
                    query += "insert into DEPARTMENT(ID,DESCRIPTION) "
                          + "values(" + getData.Rows[i][0] + " , "
                          + "'" + getData.Rows[i][1] + "' ) "
                          + delimeter;
                }
            }

            else if (type.Equals("Bm_Division_I"))
            {

                for (int i = 0; i < getData.Rows.Count; i++)
                {
                    query += "insert into DIVISION(ID,DESCRIPTION) "
                          + "values(" + getData.Rows[i][0] + " , "
                          + "'" + getData.Rows[i][1] + "' ) "
                          + delimeter;
                }
            }

            else if (type.Equals("Bm_Title_I"))
            {

                for (int i = 0; i < getData.Rows.Count; i++)
                {
                    query += "insert into TITLE(ID,DESCRIPTION) "
                          + "values(" + getData.Rows[i][0] + " , "
                          + "'" + getData.Rows[i][1] + "' ) "
                          + delimeter;
                }
            }

            else if (type.Equals("Bm_Card_Stat_I"))
            {

                for (int i = 0; i < getData.Rows.Count; i++)
                {
                    query += "insert into BADGE_STAT_CODE(ID,DESCRIPTION) "
                          + "values(" + getData.Rows[i][0] + " , "
                          + "'" + getData.Rows[i][1] + "' ) "
                          + delimeter;
                }
            }

            else if (type.Equals("Bm_Card_Type_I"))
            {

                for (int i = 0; i < getData.Rows.Count; i++)
                {
                    query += "insert into BADGE_TYPE_CODE(ID,DESCRIPTION) "
                          + "values(" + getData.Rows[i][0] + " , "
                          + "'" + getData.Rows[i][1] + "' ) "
                          + delimeter;
                }
            }

            else if (type.Equals("Bm_Card_Format_I"))
            {

                for (int i = 0; i < getData.Rows.Count; i++)
                {
                    query += "insert into BADGE_FORMAT_CODE(ID,DESCRIPTION) "
                          + "values(" + getData.Rows[i][0] + " , "
                          + "'" + getData.Rows[i][1] + "' ) "
                          + delimeter;
                }
            }

            else if (type.Equals("Bm_Issue_Reason_I"))
            {

                for (int i = 0; i < getData.Rows.Count; i++)
                {
                    query += "insert into ISSUE_REASON_CODE(ID,DESCRIPTION) "
                          + "values(" + getData.Rows[i][0] + " , "
                          + "'" + getData.Rows[i][1] + "' ) "
                          + delimeter;
                }
            }

            else if (type.Equals("Bm_Issue_Type_I"))
            {

                for (int i = 0; i < getData.Rows.Count; i++)
                {
                    query += "insert into ISSUE_TYPE_CODE(ID,DESCRIPTION) "
                          + "values(" + getData.Rows[i][0] + " , "
                          + "'" + getData.Rows[i][1] + "' ) "
                          + delimeter;
                }
            }


            else if (type.Equals("Bm_Department_D"))
            {

                query += "delete from DEPARTMENT where ID = "
                      + getData.Rows[0][0];

            }
            else if (type.Equals("Bm_Division_D"))
            {
                query += "delete from DIVISION where ID = "
                      + getData.Rows[0][0];
            }
            else if (type.Equals("Bm_Title_D"))
            {
                query += "delete from TITLE where ID = "
                      + getData.Rows[0][0];
            }
            else if (type.Equals("Bm_Card_Stat_D"))
            {
                query += "delete from BADGE_STAT_CODE where ID = "
                      + getData.Rows[0][0];
            }
            else if (type.Equals("Bm_Card_Type_D"))
            {
                query += "delete from BADGE_TYPE_CODE where ID = "
                      + getData.Rows[0][0];
            }
            else if (type.Equals("Bm_Card_Format_D"))
            {
                query += "delete from BADGE_FORMAT_CODE where ID = "
                      + getData.Rows[0][0];
            }

            else if (type.Equals("Bm_Issue_Reason_D"))
            {
                query += "delete from ISSUE_REASON_CODE where ID = "
                      + getData.Rows[0][0];
            }
            else if (type.Equals("Bm_Issue_Type_D"))
            {
                query += "delete from ISSUE_TYPE_CODE where ID = "
                      + getData.Rows[0][0];
            }



            else if (type.Equals("Bm_Department_U"))
            {
                for (int i = 0; i < getData.Rows.Count; i++)
                {
                    query += "update  DEPARTMENT set DESCRIPTION = " + "'" + getData.Rows[i][1] + "' "
                          + " where ID = " + getData.Rows[i][0] + " "
                          + delimeter;
                }
            }
            else if (type.Equals("Bm_Division_U"))
            {
                for (int i = 0; i < getData.Rows.Count; i++)
                {
                    query += "update  DIVISION set DESCRIPTION = " + "'" + getData.Rows[i][1] + "' "
                          + " where ID = " + getData.Rows[i][0] + " "
                          + delimeter;
                }
            }
            else if (type.Equals("Bm_Title_U"))
            {
                for (int i = 0; i < getData.Rows.Count; i++)
                {
                    query += "update  TITLE set DESCRIPTION = " + "'" + getData.Rows[i][1] + "' "
                          + " where ID = " + getData.Rows[i][0] + " "
                          + delimeter;
                }
            }
            else if (type.Equals("Bm_Card_Stat_U"))
            {
                for (int i = 0; i < getData.Rows.Count; i++)
                {
                    query += "update  BADGE_STAT_CODE set DESCRIPTION = " + "'" + getData.Rows[i][1] + "' "
                          + " where ID = " + getData.Rows[i][0] + " "
                          + delimeter;
                }
            }
            else if (type.Equals("Bm_Card_Type_U"))
            {
                for (int i = 0; i < getData.Rows.Count; i++)
                {
                    query += "update  BADGE_TYPE_CODE set DESCRIPTION = " + "'" + getData.Rows[i][1] + "' "
                          + " where ID = " + getData.Rows[i][0] + " "
                          + delimeter;
                }
            }
            else if (type.Equals("Bm_Card_Format_U"))
            {
                for (int i = 0; i < getData.Rows.Count; i++)
                {
                    query += "update  BADGE_FORMAT_CODE set DESCRIPTION = " + "'" + getData.Rows[i][1] + "' "
                          + " where ID = " + getData.Rows[i][0] + " "
                          + delimeter;
                }
            }

            else if (type.Equals("Bm_Issue_Reason_U"))
            {
                for (int i = 0; i < getData.Rows.Count; i++)
                {
                    query += "update  ISSUE_REASON_CODE set DESCRIPTION = " + "'" + getData.Rows[i][1] + "' "
                          + " where ID = " + getData.Rows[i][0] + " "
                          + delimeter;
                }
            }
            else if (type.Equals("Bm_Issue_Type_U"))
            {
                for (int i = 0; i < getData.Rows.Count; i++)
                {
                    query += "update  ISSUE_TYPE_CODE set DESCRIPTION = " + "'" + getData.Rows[i][1] + "' "
                          + " where ID = " + getData.Rows[i][0] + " "
                          + delimeter;
                }
            }







            Log.WriteLog("query = " + query);



            return query;

        }

        private string getIp()
        {
            IPHostEntry host = Dns.GetHostByName(Dns.GetHostName());
            string myIp = host.AddressList[0].ToString();
            return myIp;
        }
    }
}
