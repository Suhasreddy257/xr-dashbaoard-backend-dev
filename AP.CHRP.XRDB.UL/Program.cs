using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using AP.CHRP.XRDB.UL.GenDTCls;

namespace AP.CHRP.XRDB.UL
{
    class Program
    {

        static String connectionString = "server=192.168.190.22;database=XRDashboard_Dev;Connection Timeout=120;uid=chrp;pwd=chrp@123;TrustServerCertificate=True";
        static String path = @"E:\CHRP_XR\XR_Dashboard_BackEnd_16_OCT_2025\AP.CHRP.XRDB.DB\DataTable\";
        //static String path = @"E:\Ravi_Work\CHRP_XR_CODE\XR_Dashboard_BackEnd\AP.CHRP.XRDB.WebApi\AP.CHRP.XRDB.DB\DataTable\";
        static String strDBName = "XRDashboard_Dev";
        static String strProjectName = "AP.CHRP.XRDB.DB.DataTable";



        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            GenerateAll_DataObjects();
            Console.ReadKey();
        }



        private static void GenerateAll_DataObjects()
        {
            DBConnect m_DBConnect;


            m_DBConnect = new DBConnect(connectionString);
            List<String> tbl_list = new List<String>();
            m_DBConnect.GetTableList(ref tbl_list, strDBName);


            List<DBTableDefination> tblList = new List<DBTableDefination>();


            for (int i = 0; i < tbl_list.Count; i++)
            {
                DBTableDefination tbl = new DBTableDefination(tbl_list[i]);
                m_DBConnect.GetColumnList(ref tbl.Columns, strDBName, tbl_list[i]);
                tblList.Add(tbl);
            }


            GenerateTableCS table1 = new GenerateTableCS();
            for (int i = 0; i < tblList.Count; i++)
            {
                DBTableDefination tbl = tblList[i];
                table1.GenerateCodeAndWriteToFile(ref tbl, path, strProjectName);
            }



            Console.WriteLine("Totoal Files Generated Should be = " + tblList.Count.ToString());
        }


    }
}

