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
    class get_LocationState_BL : AC_BC_BusinessLogic
    {

        public get_LocationState_BL(ref AC_MSSQL_Access dbc)
        {
            m_dbc = dbc;
        }

        public int get_LocationState(ref get_LocationState_IP ip, ref get_LocationState_OP op)
        {
            m_WhereCondition = "WHERE CountryID = '" + ip.m_CountryID + "'";

            List<TBL_MAS_LOCATION_STATE> ml_LocationState = new List<TBL_MAS_LOCATION_STATE>();

            m_rc = m_dbc.Select<TBL_MAS_LOCATION_STATE>(ref m_ErrorMessage, m_WhereCondition, ref ml_LocationState);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            op.ml_LocationState = ml_LocationState;

            return 0;
        }
    }
}