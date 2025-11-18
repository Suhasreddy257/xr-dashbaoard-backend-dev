using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AP.CHRP.XRDB.AC.BusinessLogic;
using AP.CHRP.XRDB.AC.DataBase;
using AP.CHRP.XRDB.AC.DataContract;
using AP.CHRP.XRDB.DT.Event;
using AP.CHRP.XRDB.DT.Login;


namespace AP.CHRP.XRDB.BL.Event
{
    public class BL_Event
    {
        private int rc;
        private AC_MSSQL_Access m_dbc;
        private AC_BC_DataContract _ip;
        private AC_BC_DataContract _op;

        public int put_NewEvent(ref put_NewEvent_IP ip, ref put_NewEvent_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                put_NewEvent_BL bl = new put_NewEvent_BL(ref m_dbc);
                rc = bl.put_NewEvent(ref ip, ref op);
            }
            catch (Exception ex)
            {
                op.MessageInfo.ReturnMessage = ex.Message;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            AC_MSSQL_ManageConnection.FreeConnectionObject(ref m_dbc, ref _ip, ref _op);
            return rc;
        }

        public int get_AllEvent(ref get_AllEvent_IP ip, ref get_AllEvent_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                get_AllEvent_BL bl = new get_AllEvent_BL(ref m_dbc);
                rc = bl.get_AllEvent(ref ip, ref op);
            }
            catch (Exception ex)
            {
                op.MessageInfo.ReturnMessage = ex.Message;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            AC_MSSQL_ManageConnection.FreeConnectionObject(ref m_dbc, ref _ip, ref _op);
            return rc;
        }

        public int get_EventByID(ref get_EventByID_IP ip, ref get_EventByID_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                get_EventByID_BL bl = new get_EventByID_BL(ref m_dbc);
                rc = bl.get_EventByID(ref ip, ref op);
            }
            catch (Exception ex)
            {
                op.MessageInfo.ReturnMessage = ex.Message;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            AC_MSSQL_ManageConnection.FreeConnectionObject(ref m_dbc, ref _ip, ref _op);
            return rc;
        }

        public int put_EventEdit(ref put_EventEdit_IP ip, ref put_EventEdit_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                put_EventEdit_BL bl = new put_EventEdit_BL(ref m_dbc);
                rc = bl.put_EventEdit(ref ip, ref op);
            }
            catch (Exception ex)
            {
                op.MessageInfo.ReturnMessage = ex.Message;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            AC_MSSQL_ManageConnection.FreeConnectionObject(ref m_dbc, ref _ip, ref _op);
            return rc;
        }

        public int put_EventDelete(ref put_EventDelete_IP ip, ref put_EventDelete_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                put_EventDelete_BL bl = new put_EventDelete_BL(ref m_dbc);
                rc = bl.put_EventDelete(ref ip, ref op);
            }
            catch (Exception ex)
            {
                op.MessageInfo.ReturnMessage = ex.Message;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            AC_MSSQL_ManageConnection.FreeConnectionObject(ref m_dbc, ref _ip, ref _op);
            return rc;
        }
    }
}