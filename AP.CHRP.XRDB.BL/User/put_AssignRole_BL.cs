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
    class put_AssignRole_BL : AC_BC_BusinessLogic
    {

        public put_AssignRole_BL(ref AC_MSSQL_Access dbc)
        {
            m_dbc = dbc;
        }

        public int put_AssignRole(ref put_AssignRole_IP ip, ref put_AssignRole_OP op)
        {
            try
            {
                string sqlQuery = $@"UPDATE TBL_MAS_USER_LOGIN
                    SET UserRoleID = {ip.m_UserRoleID},
                        UserRoleName = '{ip.m_UserRoleName}'
                        WHERE UserID = {ip.m_UserID};
                ";

                m_rc = m_dbc.ExecureNonQuery(ref m_ErrorMessage, sqlQuery);

                if (m_rc < 0)
                {
                    op.MessageInfo.ReturnMessage = "Failed to update user role: " + m_ErrorMessage;
                    op.MessageInfo.ReturnValue = -1;
                    return -1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                op.MessageInfo.ReturnMessage = "Exception occurred: " + ex.Message;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }
        }
    }
}