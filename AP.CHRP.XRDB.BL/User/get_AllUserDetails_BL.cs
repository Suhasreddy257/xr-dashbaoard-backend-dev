using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AP.CHRP.XRDB.AC.BusinessLogic;
using AP.CHRP.XRDB.AC.DataBase;
using AP.CHRP.XRDB.DB.DataTable;
using AP.CHRP.XRDB.DT.User;

namespace AP.CHRP.XRDB.BL.User
{
    class get_AllUserDetails_BL : AC_BC_BusinessLogic
    {

        public get_AllUserDetails_BL(ref AC_MSSQL_Access dbc)
        {
            m_dbc = dbc;
        }

        public int get_AllUserDetails(ref get_AllUserDetails_IP ip, ref get_AllUserDetails_OP op)
        {
            m_WhereCondition = "WHERE UserID IN (" + ip.ml_UserID + ")";
            List<V_UserDetail> ml_user = new List<V_UserDetail>();
            m_rc = m_dbc.SelectView(ref m_ErrorMessage, m_WhereCondition, ref ml_user);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }
            op.ml_user = ml_user;

            return 0;
        }
    }
}