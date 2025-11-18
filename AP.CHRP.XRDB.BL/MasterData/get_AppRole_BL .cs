using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AP.CHRP.XRDB.AC.BusinessLogic;
using AP.CHRP.XRDB.AC.DataBase;
using AP.CHRP.XRDB.DB.DataTable;
using AP.CHRP.XRDB.DT.MasterData;

namespace AP.CHRP.XRDB.BL.MasterData
{
    class get_AppRole_BL : AC_BC_BusinessLogic
    {

        public get_AppRole_BL(ref AC_MSSQL_Access dbc)
        {
            m_dbc = dbc;
        }

        public int get_AppRole(ref get_AppRole_IP ip, ref get_AppRole_OP op)
        {
            m_WhereCondition = "WHERE ID > 0 ";
            
            List<TBL_MAS_APP_ROLE> ml_AppRole = new List<TBL_MAS_APP_ROLE>();

            m_rc = m_dbc.Select<TBL_MAS_APP_ROLE>(ref m_ErrorMessage, m_WhereCondition, ref ml_AppRole);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            op.ml_AppRole = ml_AppRole;

            return 0;
        }
    }
}