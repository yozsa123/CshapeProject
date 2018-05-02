using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;


namespace BMClient
{
    class CodeTable
    {
        public static DataTable department = new DataTable();
        public static DataTable division = new DataTable();
        public static DataTable title = new DataTable();
        public static DataTable cardType = new DataTable();
        public static DataTable cardStat = new DataTable();
        public static DataTable cardFormat = new DataTable();
        public static DataTable issueType = new DataTable();
        public static DataTable issueReason = new DataTable();

        public CodeTable()
        {
            if (department.Columns.Count == 0)
            {
                department.Columns.Add("ID");
                department.Columns.Add("DESCRIPTION");
            }

            if (division.Columns.Count == 0)
            {
                division.Columns.Add("ID");
                division.Columns.Add("DESCRIPTION");
            }

            if (title.Columns.Count == 0)
            {
                title.Columns.Add("ID");
                title.Columns.Add("DESCRIPTION");
            }

            if (cardType.Columns.Count == 0)
            {
                cardType.Columns.Add("ID");
                cardType.Columns.Add("DESCRIPTION");
            }

            if (cardStat.Columns.Count == 0)
            {
                cardStat.Columns.Add("ID");
                cardStat.Columns.Add("DESCRIPTION");
            }
            if (cardFormat.Columns.Count == 0)
            {
                cardFormat.Columns.Add("ID");
                cardFormat.Columns.Add("DESCRIPTION");
            }
            if (issueType.Columns.Count == 0)
            {
                issueType.Columns.Add("ID");
                issueType.Columns.Add("DESCRIPTION");
            }
            if (issueReason.Columns.Count == 0)
            {
                issueReason.Columns.Add("ID");
                issueReason.Columns.Add("DESCRIPTION");
            }
            makeRow(department, 106);
            makeRow(division, 72);
            makeRow(title, 13);
            makeRow(cardType, 11);
            makeRow(cardStat, 5);
            makeRow(cardFormat, 3);
            makeRow(issueType, 3);
            makeRow(issueReason, 8);



            getDepartment();
            getDivision();
            getTitle();
            getCardStat();
            getCardType();
            getCardFormat();
            getIssueReason();
            getIssueType();


        }

        void makeRow(DataTable getTable, int rowCnt)
        {
            for (int i = 0; i < rowCnt; i++)
            {
                getTable.Rows.Add(1);
            }


        }


        void getDepartment()
        {



            department.Rows[0][0] = 10000;
            department.Rows[1][0] = 10003;
            department.Rows[2][0] = 10006;
            department.Rows[3][0] = 10007;
            department.Rows[4][0] = 10009;
            department.Rows[5][0] = 10014;
            department.Rows[6][0] = 10016;
            department.Rows[7][0] = 10017;
            department.Rows[8][0] = 10018;
            department.Rows[9][0] = 10019;
            department.Rows[10][0] = 10020;
            department.Rows[11][0] = 10021;
            department.Rows[12][0] = 10022;
            department.Rows[13][0] = 10023;
            department.Rows[14][0] = 10024;
            department.Rows[15][0] = 10025;
            department.Rows[16][0] = 10026;
            department.Rows[17][0] = 10027;
            department.Rows[18][0] = 10028;
            department.Rows[19][0] = 10037;
            department.Rows[20][0] = 10038;
            department.Rows[21][0] = 10039;
            department.Rows[22][0] = 10044;
            department.Rows[23][0] = 10045;
            department.Rows[24][0] = 10047;
            department.Rows[25][0] = 10058;
            department.Rows[26][0] = 10061;
            department.Rows[27][0] = 10065;
            department.Rows[28][0] = 10068;
            department.Rows[29][0] = 10069;
            department.Rows[30][0] = 10070;
            department.Rows[31][0] = 10071;
            department.Rows[32][0] = 10072;
            department.Rows[33][0] = 10073;
            department.Rows[34][0] = 10075;
            department.Rows[35][0] = 10077;
            department.Rows[36][0] = 10080;
            department.Rows[37][0] = 10082;
            department.Rows[38][0] = 10086;
            department.Rows[39][0] = 10089;
            department.Rows[40][0] = 10090;
            department.Rows[41][0] = 10091;
            department.Rows[42][0] = 10092;
            department.Rows[43][0] = 10093;
            department.Rows[44][0] = 10094;
            department.Rows[45][0] = 10095;
            department.Rows[46][0] = 10096;
            department.Rows[47][0] = 10097;
            department.Rows[48][0] = 10098;
            department.Rows[49][0] = 10099;
            department.Rows[50][0] = 10100;
            department.Rows[51][0] = 10102;
            department.Rows[52][0] = 10103;
            department.Rows[53][0] = 10104;
            department.Rows[54][0] = 10110;
            department.Rows[55][0] = 10111;
            department.Rows[56][0] = 10112;
            department.Rows[57][0] = 10113;
            department.Rows[58][0] = 10116;
            department.Rows[59][0] = 10117;
            department.Rows[60][0] = 10118;
            department.Rows[61][0] = 10119;
            department.Rows[62][0] = 10125;
            department.Rows[63][0] = 10126;
            department.Rows[64][0] = 10127;
            department.Rows[65][0] = 10130;
            department.Rows[66][0] = 10131;
            department.Rows[67][0] = 10132;
            department.Rows[68][0] = 10133;
            department.Rows[69][0] = 10134;
            department.Rows[70][0] = 10135;
            department.Rows[71][0] = 10136;
            department.Rows[72][0] = 10137;
            department.Rows[73][0] = 10138;
            department.Rows[74][0] = 10139;
            department.Rows[75][0] = 10140;
            department.Rows[76][0] = 10141;
            department.Rows[77][0] = 10142;
            department.Rows[78][0] = 10143;
            department.Rows[79][0] = 10144;
            department.Rows[80][0] = 10150;
            department.Rows[81][0] = 10155;
            department.Rows[82][0] = 10156;
            department.Rows[83][0] = 10157;
            department.Rows[84][0] = 10158;
            department.Rows[85][0] = 10159;
            department.Rows[86][0] = 10160;
            department.Rows[87][0] = 10162;
            department.Rows[88][0] = 10163;
            department.Rows[89][0] = 10165;
            department.Rows[90][0] = 10167;
            department.Rows[91][0] = 10168;
            department.Rows[92][0] = 10170;
            department.Rows[93][0] = 10171;
            department.Rows[94][0] = 10174;
            department.Rows[95][0] = 10175;
            department.Rows[96][0] = 10176;
            department.Rows[97][0] = 10177;
            department.Rows[98][0] = 10178;
            department.Rows[99][0] = 10184;
            department.Rows[100][0] = 10185;
            department.Rows[101][0] = 10187;
            department.Rows[102][0] = 10188;
            department.Rows[103][0] = 10189;
            department.Rows[104][0] = 10190;
            department.Rows[105][0] = 10191;


            department.Rows[0][1] = "신고리2발전소";
            department.Rows[1][1] = "한전KPS(주)원정센터";
            department.Rows[2][1] = "한전KPS(주)1";
            department.Rows[3][1] = "신고리2건설소";
            department.Rows[4][1] = "고리2발전소";
            department.Rows[5][1] = "경영지원처";
            department.Rows[6][1] = "삼진보안";
            department.Rows[7][1] = "유화건설";
            department.Rows[8][1] = "삼아디오시스템";
            department.Rows[9][1] = "KINS";
            department.Rows[10][1] = "하나검사기술";
            department.Rows[11][1] = "이성CNI";
            department.Rows[12][1] = "금화PSC";
            department.Rows[13][1] = "두로산전";
            department.Rows[14][1] = "주재관실";
            department.Rows[15][1] = "한전KPS(주)신고리";
            department.Rows[16][1] = "한국전력기술";
            department.Rows[17][1] = "삼진공작";
            department.Rows[18][1] = "넥스텔";
            department.Rows[19][1] = "안전감시역";
            department.Rows[20][1] = "국일산업";
            department.Rows[21][1] = "신고리1발전소";
            department.Rows[22][1] = "한전KPS(주)2";
            department.Rows[23][1] = "고리원자력본부";
            department.Rows[24][1] = "고리1발전소";
            department.Rows[25][1] = "고리본부";
            department.Rows[26][1] = "교육훈련센터";
            department.Rows[27][1] = "신고리3건설소";
            department.Rows[28][1] = "석원산업";
            department.Rows[29][1] = "경남상사";
            department.Rows[30][1] = "삼공사";
            department.Rows[31][1] = "케이뉴텍";
            department.Rows[32][1] = "한국정수공업";
            department.Rows[33][1] = "브니엘네이처";
            department.Rows[34][1] = "품질보증3팀";
            department.Rows[35][1] = "기타";
            department.Rows[36][1] = "품질보증2팀";
            department.Rows[37][1] = "신고리1건설소";
            department.Rows[38][1] = "품질보증1팀";
            department.Rows[39][1] = "액트";
            department.Rows[40][1] = "코네스코퍼레이션";
            department.Rows[41][1] = "갑부통신";
            department.Rows[42][1] = "제이제이테크";
            department.Rows[43][1] = "영중개발";
            department.Rows[44][1] = "대경기술";
            department.Rows[45][1] = "현대건설";
            department.Rows[46][1] = "현대엔지니어링";
            department.Rows[47][1] = "(주)포뉴텍";
            department.Rows[48][1] = "대길건설";
            department.Rows[49][1] = "전방재엔지니어링 ";
            department.Rows[50][1] = "대명엘리베이터";
            department.Rows[51][1] = "ES다산 ";
            department.Rows[52][1] = "두산중공업";
            department.Rows[53][1] = "유진기업";
            department.Rows[54][1] = "한전KPS(주)";
            department.Rows[55][1] = "나일플랜트";
            department.Rows[56][1] = "동일엔지니어링";
            department.Rows[57][1] = "KIMS";
            department.Rows[58][1] = "폴";
            department.Rows[59][1] = "센츄리";
            department.Rows[60][1] = "제이스코리아";
            department.Rows[61][1] = "천일";
            department.Rows[62][1] = "해금테크(주)";
            department.Rows[63][1] = "서전사";
            department.Rows[64][1] = "넥스지오";
            department.Rows[65][1] = "원창건설";
            department.Rows[66][1] = "풀무원";
            department.Rows[67][1] = "BNF테크놀로지(주)";
            department.Rows[68][1] = "FSCOM";
            department.Rows[69][1] = "우리기술(주)";
            department.Rows[70][1] = "안승엘리베이터";
            department.Rows[71][1] = "(주)에프엔비씨";
            department.Rows[72][1] = "보성INC";
            department.Rows[73][1] = "거산코아트";
            department.Rows[74][1] = "다인전설";
            department.Rows[75][1] = "세영엔디씨";
            department.Rows[76][1] = "선광원자력안전";
            department.Rows[77][1] = "현대중공업";
            department.Rows[78][1] = "은민S&D";
            department.Rows[79][1] = "GS중공업";
            department.Rows[80][1] = "본부요원";
            department.Rows[81][1] = "원자력안전위원회";
            department.Rows[82][1] = "성웅기술";
            department.Rows[83][1] = "효성";
            department.Rows[84][1] = "고려검사";
            department.Rows[85][1] = "에이스기전";
            department.Rows[86][1] = "청림전설";
            department.Rows[87][1] = "비앤티엔지니어링";
            department.Rows[88][1] = "세안기술";
            department.Rows[89][1] = "OS산업개발";
            department.Rows[90][1] = "KINAC";
            department.Rows[91][1] = "진흥산업";
            department.Rows[92][1] = "삼신";
            department.Rows[93][1] = "한국건설안전기술";
            department.Rows[94][1] = "고리민간환경감시기구";
            department.Rows[95][1] = "DHI";
            department.Rows[96][1] = "대호전기";
            department.Rows[97][1] = "합동전기(주)";
            department.Rows[98][1] = "(주)유원기술";
            department.Rows[99][1] = "기장군청";
            department.Rows[100][1] = "울주군청";
            department.Rows[101][1] = "다켐엔지니어링";
            department.Rows[102][1] = "신고리민간환경감시기구";
            department.Rows[103][1] = "ENEC";
            department.Rows[104][1] = "제일기업";
            department.Rows[105][1] = "발전파트2";



        }

        void getDivision()
        {


            division.Rows[0][0] = 10001;
            division.Rows[1][0] = 10004;
            division.Rows[2][0] = 10008;
            division.Rows[3][0] = 10010;
            division.Rows[4][0] = 10011;
            division.Rows[5][0] = 10012;
            division.Rows[6][0] = 10013;
            division.Rows[7][0] = 10015;
            division.Rows[8][0] = 10030;
            division.Rows[9][0] = 10032;
            division.Rows[10][0] = 10033;
            division.Rows[11][0] = 10035;
            division.Rows[12][0] = 10036;
            division.Rows[13][0] = 10040;
            division.Rows[14][0] = 10042;
            division.Rows[15][0] = 10043;
            division.Rows[16][0] = 10046;
            division.Rows[17][0] = 10048;
            division.Rows[18][0] = 10049;
            division.Rows[19][0] = 10050;
            division.Rows[20][0] = 10052;
            division.Rows[21][0] = 10053;
            division.Rows[22][0] = 10054;
            division.Rows[23][0] = 10055;
            division.Rows[24][0] = 10056;
            division.Rows[25][0] = 10059;
            division.Rows[26][0] = 10060;
            division.Rows[27][0] = 10062;
            division.Rows[28][0] = 10063;
            division.Rows[29][0] = 10066;
            division.Rows[30][0] = 10067;
            division.Rows[31][0] = 10074;
            division.Rows[32][0] = 10076;
            division.Rows[33][0] = 10078;
            division.Rows[34][0] = 10079;
            division.Rows[35][0] = 10081;
            division.Rows[36][0] = 10083;
            division.Rows[37][0] = 10084;
            division.Rows[38][0] = 10085;
            division.Rows[39][0] = 10087;
            division.Rows[40][0] = 10088;
            division.Rows[41][0] = 10105;
            division.Rows[42][0] = 10107;
            division.Rows[43][0] = 10108;
            division.Rows[44][0] = 10109;
            division.Rows[45][0] = 10114;
            division.Rows[46][0] = 10115;
            division.Rows[47][0] = 10120;
            division.Rows[48][0] = 10121;
            division.Rows[49][0] = 10122;
            division.Rows[50][0] = 10123;
            division.Rows[51][0] = 10124;
            division.Rows[52][0] = 10128;
            division.Rows[53][0] = 10129;
            division.Rows[54][0] = 10146;
            division.Rows[55][0] = 10147;
            division.Rows[56][0] = 10148;
            division.Rows[57][0] = 10149;
            division.Rows[58][0] = 10151;
            division.Rows[59][0] = 10152;
            division.Rows[60][0] = 10153;
            division.Rows[61][0] = 10154;
            division.Rows[62][0] = 10164;
            division.Rows[63][0] = 10169;
            division.Rows[64][0] = 10172;
            division.Rows[65][0] = 10173;
            division.Rows[66][0] = 10179;
            division.Rows[67][0] = 10180;
            division.Rows[68][0] = 10181;
            division.Rows[69][0] = 10182;
            division.Rows[70][0] = 10183;
            division.Rows[71][0] = 10186;

            division.Rows[0][1] = "시운전터빈팀";
            division.Rows[1][1] = "기타";
            division.Rows[2][1] = "토목팀";
            division.Rows[3][1] = "발전2팀";
            division.Rows[4][1] = "계통기술팀";
            division.Rows[5][1] = "시운전기전팀";
            division.Rows[6][1] = "기계팀";
            division.Rows[7][1] = "총무팀";
            division.Rows[8][1] = "정비기술팀";
            division.Rows[9][1] = "건축팀";
            division.Rows[10][1] = "방재환경팀";
            division.Rows[11][1] = "공정관리팀";
            division.Rows[12][1] = "자재팀";
            division.Rows[13][1] = "방사선안전팀";
            division.Rows[14][1] = "배관팀";
            division.Rows[15][1] = "전기팀";
            division.Rows[16][1] = "재난안전팀";
            division.Rows[17][1] = "운영실";
            division.Rows[18][1] = "발전3팀";
            division.Rows[19][1] = "시운전원자로팀";
            division.Rows[20][1] = "화학기술팀";
            division.Rows[21][1] = "계측제어팀";
            division.Rows[22][1] = "UAE기전팀";
            division.Rows[23][1] = "발전1팀";
            division.Rows[24][1] = "시운전발전팀";
            division.Rows[25][1] = "정보시스템팀";
            division.Rows[26][1] = "발전6팀";
            division.Rows[27][1] = "발전4팀";
            division.Rows[28][1] = "기계공사팀";
            division.Rows[29][1] = "입지팀";
            division.Rows[30][1] = "공사관리팀";
            division.Rows[31][1] = "계약관리팀";
            division.Rows[32][1] = "엔지니어링센터";
            division.Rows[33][1] = "품질검사팀";
            division.Rows[34][1] = "홍보팀";
            division.Rows[35][1] = "안전팀";
            division.Rows[36][1] = "기전팀";
            division.Rows[37][1] = "시설팀";
            division.Rows[38][1] = "지역협력팀";
            division.Rows[39][1] = "계전공사팀";
            division.Rows[40][1] = "시운전운영팀";
            division.Rows[41][1] = "발전5팀";
            division.Rows[42][1] = "발전팀";
            division.Rows[43][1] = "UAE운영팀";
            division.Rows[44][1] = "토건팀";
            division.Rows[45][1] = "기획관리팀";
            division.Rows[46][1] = "품질보증2팀";
            division.Rows[47][1] = "운영실직할";
            division.Rows[48][1] = "UAE원자로팀";
            division.Rows[49][1] = "기술실";
            division.Rows[50][1] = "부지팀";
            division.Rows[51][1] = "선택해주세요";
            division.Rows[52][1] = "교육훈련센터";
            division.Rows[53][1] = "해상공사팀";
            division.Rows[54][1] = "시운전MMIS팀";
            division.Rows[55][1] = "건설기술팀";
            division.Rows[56][1] = "품질보증1팀";
            division.Rows[57][1] = "해외사업지원팀(T/F)";
            division.Rows[58][1] = "구매기술팀";
            division.Rows[59][1] = "설비개선실";
            division.Rows[60][1] = "품질보증3팀";
            division.Rows[61][1] = "건설정리실";
            division.Rows[62][1] = "청경대";
            division.Rows[63][1] = "안전감시역";
            division.Rows[64][1] = "건설품질보증팀";
            division.Rows[65][1] = "발전품질보증팀";
            division.Rows[66][1] = "UAE지원팀";
            division.Rows[67][1] = "UAE시)발전팀";
            division.Rows[68][1] = "건설품질검사팀";
            division.Rows[69][1] = "발전품질검사팀";
            division.Rows[70][1] = "성능기술팀";
            division.Rows[71][1] = "성능개선팀";


        }

        void getTitle()
        {

            title.Rows[0][0] = 10002;
            title.Rows[1][0] = 10005;
            title.Rows[2][0] = 10029;
            title.Rows[3][0] = 10031;
            title.Rows[4][0] = 10034;
            title.Rows[5][0] = 10041;
            title.Rows[6][0] = 10051;
            title.Rows[7][0] = 10057;
            title.Rows[8][0] = 10064;
            title.Rows[9][0] = 10101;
            title.Rows[10][0] = 10106;
            title.Rows[11][0] = 10145;
            title.Rows[12][0] = 10161;



            title.Rows[0][1] = "4직급";
            title.Rows[1][1] = "기타";
            title.Rows[2][1] = "3직급";
            title.Rows[3][1] = "6직급";
            title.Rows[4][1] = "5직급";
            title.Rows[5][1] = "촉탁";
            title.Rows[6][1] = "2직급";
            title.Rows[7][1] = "예비군대대장";
            title.Rows[8][1] = "청경대";
            title.Rows[9][1] = "계약직원";
            title.Rows[10][1] = "1직급";
            title.Rows[11][1] = "예비군지휘관";
            title.Rows[12][1] = "선택해주세요";


        }

        void getCardType()
        {


            cardType.Rows[0][0] = 0;
            cardType.Rows[1][0] = 1;
            cardType.Rows[2][0] = 2;
            cardType.Rows[3][0] = 3;
            cardType.Rows[4][0] = 4;
            cardType.Rows[5][0] = 5;
            cardType.Rows[6][0] = 6;
            cardType.Rows[7][0] = 7;
            cardType.Rows[8][0] = 8;
            cardType.Rows[9][0] = 9;
            cardType.Rows[10][0] = 10;

            cardType.Rows[0][1] = "";
            cardType.Rows[1][1] = "직원";
            cardType.Rows[2][1] = "방문";
            cardType.Rows[3][1] = "임시";
            cardType.Rows[4][1] = "한수원 사원증";
            cardType.Rows[5][1] = "협력업체 키카드";
            cardType.Rows[6][1] = "작업증";
            cardType.Rows[7][1] = "협력업체 장기출입증";
            cardType.Rows[8][1] = "VIP키카드";
            cardType.Rows[9][1] = "임시키카드";
            cardType.Rows[10][1] = "직원대체카드";



        }

        void getCardStat()
        {


            cardStat.Rows[0][0] = 0;
            cardStat.Rows[1][0] = 1;
            cardStat.Rows[2][0] = 2;
            cardStat.Rows[3][0] = 3;
            cardStat.Rows[4][0] = 4;

            cardStat.Rows[0][1] = "ACTIVE";
            cardStat.Rows[1][1] = "LOST";
            cardStat.Rows[2][1] = "RETURNED";
            cardStat.Rows[3][1] = "DELETED";
            cardStat.Rows[4][1] = "INACTIVE";
            

        }

        void getCardFormat()
        {



            cardFormat.Rows[0][0] = 1;
            cardFormat.Rows[1][0] = 2;
            cardFormat.Rows[2][0] = 3;


            cardFormat.Rows[0][1] = "Standard 10 Digit Badge";
            cardFormat.Rows[1][1] = "Standard 12 Digit Badge";
            cardFormat.Rows[2][1] = "Standard 6 Digit Badge";
        }

        void getIssueType()
        {



            issueType.Rows[0][0] = 1;
            issueType.Rows[1][0] = 2;
            issueType.Rows[2][0] = 3;


            issueType.Rows[0][1] = "발급";
            issueType.Rows[1][1] = "회수";
            issueType.Rows[2][1] = "분실";
        }

        void getIssueReason()
        {


            issueReason.Rows[0][0] = 1;
            issueReason.Rows[1][0] = 2;
            issueReason.Rows[2][0] = 3;
            issueReason.Rows[3][0] = 4;
            issueReason.Rows[4][0] = 5;
            issueReason.Rows[5][0] = 6;
            issueReason.Rows[6][0] = 7;
            issueReason.Rows[7][0] = 8;

            issueReason.Rows[0][1] = "신규";
            issueReason.Rows[1][1] = "분실";
            issueReason.Rows[2][1] = "훼손";
            issueReason.Rows[3][1] = "갱신";
            issueReason.Rows[4][1] = "기타";
            issueReason.Rows[5][1] = "전출";
            issueReason.Rows[6][1] = "퇴사";
            issueReason.Rows[7][1] = "사유없음";

        }

        public static string getDeparmentDesc(int code)
        {
            Log.WriteLogTmp("[CodeTable.cs] getDepartmentDesc (" + code + ")" + department.Rows.Count);

            int count = department.Rows.Count;
            if (count < 1) return "Unknow";

            for (int i = 0; i < count; i++)
            {
                // Log.WriteLogTmp("[CodeTable.cs] " + department.Rows[i][0]);   //  + " | " + (string)department.Rows[i][1]);
                if (code == Convert.ToInt32 (department.Rows[i][0])) return "" + department.Rows[i][1];
            }

            return "Unknown";
        }

        public static string getDivisionDesc(int code)
        {
            int count = division.Rows.Count;
            if (count < 1) return "Unknow";

            for (int i = 0; i < count; i++)
            {
                if (code == Convert.ToInt32(division.Rows[i][0])) return "" + division.Rows[i][1];
            }

            return "Unknown";
        }

        public static string getTitleDesc(int code)
        {
            int count = title.Rows.Count;
            if (count < 1) return "Unknow";

            for (int i = 0; i < count; i++)
            {
                if (code == Convert.ToInt32(title.Rows[i][0])) return "" + title.Rows[i][1];
            }

            return "Unknown";
        }

        public static string getBadgeStatDesc (int code)
        {
            int count = cardStat.Rows.Count;
            if (count < 1) return "Unknow";


            string result = "";
            for (int i = 0; i < count; i++)
            {

                if (code == Convert.ToInt32(cardStat.Rows[i][0]))
                {
                    result = cardStat.Rows[i][1].ToString();
                    Log.WriteLogTmp("[CodeTable.cs] getBadgeStatDesc (" + code + ") : " + result);
                    return result;
                }
            }

            return "Unknown";
        }

        public static string getBadgeTypeDesc (int code)
        {
            int count = cardType.Rows.Count;
            if (count < 1) return "Unknow";


            string result = "";
            for (int i = 0; i < count; i++)
            {
                if (code == Convert.ToInt32(cardType.Rows[i][0])) {
                    // return "" + cardType.Rows[i][1];
                    result = cardType.Rows[i][1].ToString();
                    Log.WriteLogTmp("[CodeTable.cs] getBadgeTypeDesc (" + code + ") : " + result);
                    return result;
                }
            
            }

            return "Unknown";
        }

        public static string getBadgeFormatDesc (int code)
        {
            int count = cardFormat.Rows.Count;
            if (count < 1) return "Unknow";

            string result = "";

            for (int i = 0; i < count; i++)
            {
                if (code == Convert.ToInt32(cardFormat.Rows[i][0]))
                {
                    result = cardFormat.Rows[i][1].ToString();
                    Log.WriteLogTmp("[CodeTable.cs] getBadgeFormatDesc (" + code + ") : " + result);
                    return result;
                    // return "" + cardFormat.Rows[i][1];
                }
            }
            return "Unknown";
        }

        public static string getIssueTypeDesc (int code)
        {
            int count = issueType.Rows.Count;
            if (count < 1) return "Unknow";

            for (int i = 0; i < count; i++)
            {
                if (code == Convert.ToInt32(issueType.Rows[i][0])) return "" + issueType.Rows[i][1];
            }
            return "Unknown";
        }

        public static string getIssueReasonDesc (int code)
        {
            int count = issueReason.Rows.Count;
            if (count < 1) return "Unknow";

            for (int i = 0; i < count; i++)
            {
                if (code == Convert.ToInt32(issueReason.Rows[i][0])) return "" + issueReason.Rows[i][1];
            }
            return "Unknown";
        }

        /*
        getDepartment();
            getDivision();
            getTitle();
            getCardStat();
            getCardType();
            getCardFormat();
            getIssueReason();
            getIssueType();
        */


    }
}

