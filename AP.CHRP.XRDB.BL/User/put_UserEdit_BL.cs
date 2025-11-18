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
    class put_UserEdit_BL : AC_BC_BusinessLogic
    {

        public put_UserEdit_BL(ref AC_MSSQL_Access dbc)
        {
            m_dbc = dbc;
        }

        public int put_UserEdit(ref put_UserEdit_IP ip, ref put_UserEdit_OP op)
        {
            TBL_MAS_USER m_user = ip.m_user;
            m_rc = m_dbc.Save(ref m_ErrorMessage, ref m_user);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }
            op.m_user = m_user;

            TBL_MAS_USER_LOGIN m_user_login = ip.m_user_login;
            m_user_login.UserID = op.m_user.ID;
            m_user_login.UserLoginName = op.m_user.Email;
            m_rc = m_dbc.Save(ref m_ErrorMessage, ref m_user_login);
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
