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
    class get_AllCertificate_BL : AC_BC_BusinessLogic
    {

        public get_AllCertificate_BL(ref AC_MSSQL_Access dbc)
        {
            m_dbc = dbc;
        }

        public int get_AllCertificate(ref get_AllCertificate_IP ip, ref get_AllCertificate_OP op)
        {
            m_WhereCondition = "WHERE [IsRevoked] = 0 ";
            List<V_USERCERTIFICATEDETAILS> ml_certificate = new List<V_USERCERTIFICATEDETAILS>();
            m_rc = m_dbc.SelectView(ref m_ErrorMessage, m_WhereCondition, ref ml_certificate);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            op.ml_certificate = ml_certificate;

            return 0;
        }
    }
}