using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AP.CHRP.XRDB.AC.BusinessLogic;
using AP.CHRP.XRDB.AC.DataBase;
using AP.CHRP.XRDB.DB.DataTable;
using AP.CHRP.XRDB.DT.MasterData;

namespace AP.CHRP.XRDB.BL.MasterData
{
    class get_LocationCity_BL : AC_BC_BusinessLogic
    {

        public get_LocationCity_BL(ref AC_MSSQL_Access dbc)
        {
            m_dbc = dbc;
        }

        public int get_LocationCity(ref get_LocationCity_IP ip, ref get_LocationCity_OP op)
        {
            m_WhereCondition = "WHERE ";

            if (ip.m_CountryID > 0)
            {
                m_WhereCondition = m_WhereCondition + " CountryID = " + ip.m_CountryID.ToString();

                if (ip.m_StateID > 0)
                {
                    m_WhereCondition = m_WhereCondition + " and  StateID = " + ip.m_StateID.ToString();
                }
            }

            m_WhereCondition = m_WhereCondition + " ORDER BY Name ASC";

            List<TBL_MAS_LOCATION_CITY> ml_LocationCity = new List<TBL_MAS_LOCATION_CITY>();

            m_rc = m_dbc.Select<TBL_MAS_LOCATION_CITY>(ref m_ErrorMessage, m_WhereCondition, ref ml_LocationCity);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            op.ml_LocationCity = ml_LocationCity;

            return 0;
        }


    }
}
