using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AP.CHRP.XRDB.AC.BusinessLogic;
using AP.CHRP.XRDB.AC.DataBase;
using AP.CHRP.XRDB.BL.User;
using AP.CHRP.XRDB.DB.DataTable;
using AP.CHRP.XRDB.DT.Event;
using AP.CHRP.XRDB.DT.User;

namespace AP.CHRP.XRDB.BL.Event
{
    class get_AllEvent_BL : AC_BC_BusinessLogic
    {

        public get_AllEvent_BL(ref AC_MSSQL_Access dbc)
        {
            m_dbc = dbc;
        }

        public int get_AllEvent(ref get_AllEvent_IP ip, ref get_AllEvent_OP op)
        {
            m_WhereCondition = "WHERE ID > 0";
            List<TBL_TRN_EVENT> ml_event = new List<TBL_TRN_EVENT>();
            m_rc = m_dbc.Select(ref m_ErrorMessage, m_WhereCondition, ref ml_event);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            op.ml_event = ml_event;         

            return 0;
        }
    }
}