using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AP.CHRP.XRDB.AC.DataBase;
using AP.CHRP.XRDB.AC.DataContract;


namespace AP.CHRP.XRDB.AC.BusinessLogic
{
    public class AC_BC_BusinessLogic
    {
        public int m_rc = 0;
        public String m_ErrorMessage = "";
        public String m_WhereCondition = "";
        public AC_MSSQL_Access m_dbc = null;

        public AC_BC_BusinessLogic()
        {

        }


        public int getSQLQuery<T>(ref AC_DC_getList ipdc, ref T tbl)
        {
            m_rc = 0;
            m_ErrorMessage = "";
            String strSelectLimit = "";
            String strOrderBy = "";

            if ((ipdc.m_Limit != null) && (ipdc.m_Limit.Length >= 1))
            {
                strSelectLimit = " LIMIT " + ipdc.m_Limit;
            }

            if ((ipdc.m_OrderBy != null) && (ipdc.m_OrderBy.Length >= 1))
            {
                strOrderBy = " ORDER  BY " + ipdc.m_OrderBy;
            }
            else
            {
                strOrderBy = " ORDER BY ID";
            }

            switch (ipdc.m_Type)
            {
                // 0 = get All Columns Based on ID
                case (int)AC_ENUM.getListType.ID:
                    m_WhereCondition = " WHERE ID = " + ipdc.m_ID.ToString() + strOrderBy + strSelectLimit;
                    break;
                // 1 = get All Columns Bases on CreatedDateTime Options (Day, Week, Month, Year, All ie m_Duration)
                case (int)AC_ENUM.getListType.Duration:
                    DateTime dtStart = DateTime.Now;
                    DateTime dtEnd = DateTime.Now;
                    SetDurationDateTime(ipdc.m_Duration, ref dtStart, ref dtEnd);
                    m_WhereCondition = GetWhereContionOnDateTime("CreatedDateTime", dtStart, dtEnd);
                    m_WhereCondition = m_WhereCondition + strOrderBy + strSelectLimit;
                    break;
                // 2 = get All Columns Based on CreatedDateTime DateRange (m_StartDate, m_EndDate)
                case (int)AC_ENUM.getListType.DateTimeRange:
                    m_WhereCondition = GetWhereContionOnDateTime("CreatedDateTime", ipdc.m_CrDtStart, ipdc.m_CrDtEnd);
                    m_WhereCondition = m_WhereCondition + strOrderBy + strSelectLimit;
                    break;
                // 3 = get All Columns Based on Search Text
                case (int)AC_ENUM.getListType.SearchTextAllColumns:
                    m_WhereCondition = m_dbc.SearchAllColumns("or", ref tbl, ipdc.m_Search);
                    m_WhereCondition = m_WhereCondition + strOrderBy + strSelectLimit; ;
                    break;
                // 4 = get All Columns Based on Search Text InColumns
                case (int)AC_ENUM.getListType.SearchTextSpecifiedColumns:
                    List<String> ml_SearchColumns = ipdc.ml_SearchColumns;
                    m_WhereCondition = m_dbc.SearchInColumns("or", ref ml_SearchColumns, ipdc.m_Search);
                    m_WhereCondition = m_WhereCondition + strOrderBy + strSelectLimit;
                    break;
                // 5 = get All Columns Lastest Based on SearchLimit
                case (int)AC_ENUM.getListType.AllRows:
                    m_WhereCondition = " WHERE ID > 0 " + strOrderBy + strSelectLimit;
                    break;
                // 6 = get All Columns Lastest Based on Where Condition
                case (int)AC_ENUM.getListType.SpecifiedWhereCondition:
                    m_WhereCondition = ipdc.m_WhereCondition;
                    break;
                default:
                    break;
            }

            if (m_WhereCondition.Length <= 0)
            {
                m_ErrorMessage = "SQL Query Generation Error, There is no getList Type";
                m_rc = -1;
            }

            return m_rc;
        }

        public void getCreateDateTime<T>(String ID, ref T tbl, ref DateTime Crdt, ref long CrID)
        {
            if (ID == null)
            {
                return;
            }

            m_WhereCondition = " WHERE ID = " + ID;
            List<T> tmpList = new List<T>();
            m_rc = m_dbc.Select(ref m_ErrorMessage, m_WhereCondition, ref tmpList);
            if (tmpList.Count > 0)
            {
                var pdt = tmpList[0].GetType().GetProperty("CreatedDateTime");
                Crdt = (DateTime)pdt.GetValue(tmpList[0]);

                var pID = tmpList[0].GetType().GetProperty("CreatedStaffID");
                CrID = (long)pID.GetValue(tmpList[0]);
            }

            m_WhereCondition = "";
        }

        public void AssignUserData(ref AC_BC_DataContract ipdc, DateTime CtDt, long CrStaffID)
        {

        }

        public void SetDurationDateTime(String Duration, ref DateTime dtStart, ref DateTime dtEnd)
        {
            switch (Duration)
            {
                case "Current Day":
                    dtStart = DateTime.Now;
                    dtEnd = DateTime.Now;
                    break;
                case "Current Week":
                    dtEnd = DateTime.Now;
                    int dow = (int)dtEnd.DayOfWeek;
                    dtStart = dtEnd.Subtract(new TimeSpan(dow, 0, 0, 0));
                    break;
                case "Current Month":
                    dtEnd = DateTime.Now;
                    dtStart = new DateTime(dtEnd.Year, dtEnd.Month, 1);
                    break;
                case "Current Quarter":
                    dtEnd = DateTime.Now;
                    dtStart = dtEnd.Subtract(new TimeSpan(93, 0, 0, 0));
                    break;
                case "Current Year":
                    dtEnd = DateTime.Now;
                    dtStart = new DateTime(dtEnd.Year, 1, 1);
                    break;
                case "All":
                    dtEnd = DateTime.Now;
                    dtStart = new DateTime(1900, 1, 1);
                    break;
                default:
                    break;
            }
        }

        public String GetWhereContionOnDateTime(String dtColumnName, DateTime dtStart, DateTime dtEnd)
        {
            String strStartDate = "'" + dtStart.ToString("yyyy-MM-dd") + " 00:00:00'";
            String strEndDate = "'" + dtEnd.ToString("yyyy-MM-dd") + " 23:59:59'";
            String strWhereCondition = " WHERE " + dtColumnName + " >= " + strStartDate + " and " + dtColumnName + " <= " + strEndDate;
            return strWhereCondition;
        }



        public void GetStartEndSQLDate(String strOption, ref String strStart, ref String strEnd)
        {
            DateTime dtStart = DateTime.Now;
            DateTime dtEnd = DateTime.Now;

            switch (strOption)
            {
                case "DAY":
                    dtStart = DateTime.Now;
                    dtEnd = DateTime.Now;
                    break;
                case "WEEK":
                    dtEnd = DateTime.Now;
                    int dow = (int)dtEnd.DayOfWeek;
                    dtStart = dtEnd.Subtract(new TimeSpan(dow, 0, 0, 0));
                    break;
                case "MONTH":
                    dtEnd = DateTime.Now;
                    dtStart = new DateTime(dtEnd.Year, dtEnd.Month, 1);
                    break;
                case "QUARTER":
                    dtEnd = DateTime.Now;
                    dtStart = dtEnd.Subtract(new TimeSpan(93, 0, 0, 0));
                    break;
                case "YEAR":
                    dtEnd = DateTime.Now;
                    dtStart = new DateTime(dtEnd.Year, 1, 1);
                    break;
                case "ALL":
                    dtEnd = DateTime.Now;
                    dtStart = new DateTime(2000, 1, 1);
                    break;
                default:
                    break;
            }

            strStart = "'" + dtStart.ToString("yyyy-MM-dd") + " 00:00:00'";
            strEnd = "'" + dtEnd.ToString("yyyy-MM-dd") + " 23:59:59'";
        }
    }
}

