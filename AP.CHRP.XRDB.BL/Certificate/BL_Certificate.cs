using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AP.CHRP.XRDB.AC.BusinessLogic;
using AP.CHRP.XRDB.AC.DataBase;
using AP.CHRP.XRDB.AC.DataContract;
using AP.CHRP.XRDB.DT.Certificate;
using AP.CHRP.XRDB.DT.Event;
using AP.CHRP.XRDB.DT.Login;


namespace AP.CHRP.XRDB.BL.Certificate
{
    public class BL_Certificate
    {
        private int rc;
        private AC_MSSQL_Access m_dbc;
        private AC_BC_DataContract _ip;
        private AC_BC_DataContract _op;

        public int get_AllCertificate(ref get_AllCertificate_IP ip, ref get_AllCertificate_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                get_AllCertificate_BL bl = new get_AllCertificate_BL(ref m_dbc);
                rc = bl.get_AllCertificate(ref ip, ref op);
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

        public int put_NewCertificate(ref put_NewCertificate_IP ip, ref put_NewCertificate_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                put_NewCertificate_BL bl = new put_NewCertificate_BL(ref m_dbc);
                rc = bl.put_NewCertificate(ref ip, ref op);
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