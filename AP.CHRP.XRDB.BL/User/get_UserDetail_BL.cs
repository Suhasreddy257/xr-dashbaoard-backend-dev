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
    class get_UserDetail_BL : AC_BC_BusinessLogic
    {

        public get_UserDetail_BL(ref AC_MSSQL_Access dbc)
        {
            m_dbc = dbc;
        }

        public int get_UserDetail(ref get_UserDetail_IP ip, ref get_UserDetail_OP op)
        {
            m_WhereCondition = "WHERE ID = '" + ip.m_UserID + "' ";
            TBL_MAS_USER m_user = new TBL_MAS_USER();
            m_rc = m_dbc.Select(ref m_ErrorMessage, m_WhereCondition, ref m_user);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }
            op.m_user = m_user;

            m_WhereCondition = "WHERE UserID = '" + ip.m_UserID + "' ";
            TBL_MAS_USER_LOGIN m_user_login = new TBL_MAS_USER_LOGIN();
            m_rc = m_dbc.Select(ref m_ErrorMessage, m_WhereCondition, ref m_user_login);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }
            op.m_user_login = m_user_login;

            return 0;
        }
    }
}