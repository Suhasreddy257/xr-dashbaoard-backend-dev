using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AP.CHRP.XRDB.AC.BusinessLogic;
using AP.CHRP.XRDB.AC.DataBase;
using AP.CHRP.XRDB.AC.DataContract;
using AP.CHRP.XRDB.DT.Login;



namespace AP.CHRP.XRDB.BL.Login
{
    public class BL_Login
    {
        private int rc;
        private AC_MSSQL_Access m_dbc;
        private AC_BC_DataContract _ip;
        private AC_BC_DataContract _op;

        public int get_LoginAccess(ref get_LoginAccess_IP ip, ref get_LoginAccess_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                get_LoginAccess_BL bl = new get_LoginAccess_BL(ref m_dbc);
                rc = bl.get_LoginAccess(ref ip, ref op);
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

