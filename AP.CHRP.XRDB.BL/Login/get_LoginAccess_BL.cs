using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AP.CHRP.XRDB.AC.BusinessLogic;
using AP.CHRP.XRDB.AC.DataBase;
using AP.CHRP.XRDB.AC.DataContract;
using AP.CHRP.XRDB.DB.DataTable;
using AP.CHRP.XRDB.DT.Login;

namespace AP.CHRP.XRDB.BL.Login
{
    class get_LoginAccess_BL : AC_BC_BusinessLogic
    {

        public get_LoginAccess_BL(ref AC_MSSQL_Access dbc)
        {
            m_dbc = dbc;
        }

        public int get_LoginAccess(ref get_LoginAccess_IP ip, ref get_LoginAccess_OP op)
        {
            m_WhereCondition = " Where UserLoginName = '" + ip.m_UserName + "'" ;
            TBL_MAS_USER_LOGIN tmp_user_login = new TBL_MAS_USER_LOGIN();
            m_rc = m_dbc.Select<TBL_MAS_USER_LOGIN>(ref m_ErrorMessage, m_WhereCondition, ref tmp_user_login);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            if (tmp_user_login.ID == 0)
            {
                op.MessageInfo.ReturnMessage = "User does net exists.";
                op.MessageInfo.ReturnValue = -2;
                return -2;
            }

            if (tmp_user_login.Password != ip.m_Password)
            {
                op.MessageInfo.ReturnMessage = "Wrong Password.";
                op.MessageInfo.ReturnValue = -3;
                return -3;
            }

            m_WhereCondition = " Where ID = " + tmp_user_login.UserID.ToString() ;
            TBL_MAS_USER tmp_user = new TBL_MAS_USER();
            m_rc = m_dbc.Select<TBL_MAS_USER>(ref m_ErrorMessage, m_WhereCondition, ref tmp_user);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }
            if (tmp_user.ID == 0)
            {
                op.MessageInfo.ReturnMessage = "No Data existes for User Details :  TBL_MAS_USER";
                op.MessageInfo.ReturnValue = -2;
                return -2;
            }

            m_WhereCondition = " Where ID > 0 ";
            TBL_MAS_ORGANIZATION tmp_organization = new TBL_MAS_ORGANIZATION();
            m_rc = m_dbc.Select<TBL_MAS_ORGANIZATION>(ref m_ErrorMessage, m_WhereCondition, ref tmp_organization);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }
            if (tmp_organization.ID == 0)
            {
                op.MessageInfo.ReturnMessage = "No Data existes for Organization Details :  tbl_mas_organization";
                op.MessageInfo.ReturnValue = -2;
                return -2;
            }

            op.m_user = tmp_user;
            op.m_user_login = tmp_user_login; //TODO Clean up sensitive data
            op.m_organization = tmp_organization;

            op.IsValidUser = true;

            return 0;
        }


    }
}
