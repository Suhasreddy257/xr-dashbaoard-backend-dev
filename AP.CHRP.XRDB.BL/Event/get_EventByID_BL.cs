using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AP.CHRP.XRDB.AC.BusinessLogic;
using AP.CHRP.XRDB.AC.DataBase;
using AP.CHRP.XRDB.DB.DataTable;
using AP.CHRP.XRDB.DT.Event;

namespace AP.CHRP.XRDB.BL.Event
{
    class get_EventByID_BL : AC_BC_BusinessLogic
    {

        public get_EventByID_BL(ref AC_MSSQL_Access dbc)
        {
            m_dbc = dbc;
        }

        public int get_EventByID(ref get_EventByID_IP ip, ref get_EventByID_OP op)
        {
            m_WhereCondition = "WHERE ID = '" + ip.m_ID + "' ";
            TBL_TRN_EVENT m_event = new TBL_TRN_EVENT();
            m_rc = m_dbc.Select(ref m_ErrorMessage, m_WhereCondition, ref m_event);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            op.m_event = m_event;

            m_WhereCondition = "WHERE EventID = '" + ip.m_ID + "' ";
            List<TBL_TRN_EVENT_PARTICIPANT> ml_event_participant = new List<TBL_TRN_EVENT_PARTICIPANT> ();
            m_rc = m_dbc.Select(ref m_ErrorMessage, m_WhereCondition, ref ml_event_participant);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            op.ml_event_participant = ml_event_participant;

            return 0;
        }
    }
}