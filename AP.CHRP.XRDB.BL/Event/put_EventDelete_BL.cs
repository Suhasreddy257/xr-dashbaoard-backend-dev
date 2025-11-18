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
    class put_EventDelete_BL : AC_BC_BusinessLogic
    {

        public put_EventDelete_BL(ref AC_MSSQL_Access dbc)
        {
            m_dbc = dbc;
        }

        public int put_EventDelete(ref put_EventDelete_IP ip, ref put_EventDelete_OP op)
        {
            try
            {
                string sqlQuery = $@"
                    UPDATE TBL_TRN_EVENT
                    SET IsActive = 0
                    WHERE ID = {ip.m_ID};

                    UPDATE TBL_TRN_EVENT_PARTICIPANT
                    SET IsActive = 0
                    WHERE EventID = {ip.m_ID};
                ";

                m_rc = m_dbc.ExecureNonQuery(ref m_ErrorMessage, sqlQuery);

                if (m_rc < 0)
                {
                    op.MessageInfo.ReturnMessage = "Failed to deactivate event: " + m_ErrorMessage;
                    op.MessageInfo.ReturnValue = -1;
                    return -1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                op.MessageInfo.ReturnMessage = "Exception occurred: " + ex.Message;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }
        }
    }
}