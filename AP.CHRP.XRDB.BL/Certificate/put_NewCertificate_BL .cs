using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AP.CHRP.XRDB.AC.BusinessLogic;
using AP.CHRP.XRDB.AC.DataBase;
using AP.CHRP.XRDB.DB.DataTable;
using AP.CHRP.XRDB.DT.Certificate;
using AP.CHRP.XRDB.DT.Event;

namespace AP.CHRP.XRDB.BL.Certificate
{
    class put_NewCertificate_BL : AC_BC_BusinessLogic
    {

        public put_NewCertificate_BL(ref AC_MSSQL_Access dbc)
        {
            m_dbc = dbc;
        }

        public int put_NewCertificate(ref put_NewCertificate_IP ip, ref put_NewCertificate_OP op)
        {
            TBL_TRN_USER_CERTIFICATE m_certificate = ip.m_certificate;
            m_rc = m_dbc.Save(ref m_ErrorMessage, ref m_certificate);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            op.m_certificate = m_certificate;

            return 0;
        }
    }
}