using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AP.CHRP.XRDB.AC.BusinessLogic;
using AP.CHRP.XRDB.AC.DataBase;
using AP.CHRP.XRDB.DB.DataTable;
using AP.CHRP.XRDB.DT.Event;
using AP.CHRP.XRDB.DT.User;

namespace AP.CHRP.XRDB.BL.Event
{
    class put_NewEvent_BL : AC_BC_BusinessLogic
    {

        public put_NewEvent_BL(ref AC_MSSQL_Access dbc)
        {
            m_dbc = dbc;
        }

        public int put_NewEvent(ref put_NewEvent_IP ip, ref put_NewEvent_OP op)
        {
            TBL_TRN_EVENT m_event = ip.m_event;
            m_rc = m_dbc.Save(ref m_ErrorMessage, ref m_event);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }
            

            List<TBL_TRN_EVENT_PARTICIPANT> ml_event_participant = ip.ml_event_participant;
            foreach (var participant in ml_event_participant)
            {
                participant.EventID = m_event.ID;
            }
            m_rc = m_dbc.Save(ref m_ErrorMessage, ref ml_event_participant);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            op.m_event = m_event;
            op.ml_event_participant = ml_event_participant;

            return 0;
        }
    }
}