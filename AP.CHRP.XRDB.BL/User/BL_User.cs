using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AP.CHRP.XRDB.AC.BusinessLogic;
using AP.CHRP.XRDB.AC.DataBase;
using AP.CHRP.XRDB.AC.DataContract;
using AP.CHRP.XRDB.DT.User;



namespace AP.CHRP.XRDB.BL.User
{
    public class BL_User
    {
        private int rc;
        private AC_MSSQL_Access m_dbc;
        private AC_BC_DataContract _ip;
        private AC_BC_DataContract _op;

        public int get_AllUser(ref get_AllUser_IP ip, ref get_AllUser_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                get_AllUser_BL bl = new get_AllUser_BL(ref m_dbc);
                rc = bl.get_AllUser(ref ip, ref op);
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

        public int get_ViewUserDetail(ref get_ViewUserDetail_IP ip, ref get_ViewUserDetail_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                get_ViewUserDetail_BL bl = new get_ViewUserDetail_BL(ref m_dbc);
                rc = bl.get_ViewUserDetail(ref ip, ref op);
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

        public int get_UserDetail(ref get_UserDetail_IP ip, ref get_UserDetail_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                get_UserDetail_BL bl = new get_UserDetail_BL(ref m_dbc);
                rc = bl.get_UserDetail(ref ip, ref op);
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

        public int put_UserNew(ref put_UserNew_IP ip, ref put_UserNew_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                put_UserNew_BL bl = new put_UserNew_BL(ref m_dbc);
                rc = bl.put_UserNew(ref ip, ref op);
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

        public int put_UserEdit(ref put_UserEdit_IP ip, ref put_UserEdit_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                put_UserEdit_BL bl = new put_UserEdit_BL(ref m_dbc);
                rc = bl.put_UserEdit(ref ip, ref op);
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

        public int put_AssignRole(ref put_AssignRole_IP ip, ref put_AssignRole_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                put_AssignRole_BL bl = new put_AssignRole_BL(ref m_dbc);
                rc = bl.put_AssignRole(ref ip, ref op);
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

        public int put_UserDelete(ref put_UserDelete_IP ip, ref put_UserDelete_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                put_UserDelete_BL bl = new put_UserDelete_BL(ref m_dbc);
                rc = bl.put_UserDelete(ref ip, ref op);
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

        public int get_AllUserDetails(ref get_AllUserDetails_IP ip, ref get_AllUserDetails_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                get_AllUserDetails_BL bl = new get_AllUserDetails_BL(ref m_dbc);
                rc = bl.get_AllUserDetails(ref ip, ref op);
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