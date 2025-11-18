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
    class get_Organization_BL : AC_BC_BusinessLogic
    {

        public get_Organization_BL(ref AC_MSSQL_Access dbc)
        {
            m_dbc = dbc;
        }

        public int get_Organization(ref get_Organization_IP ip, ref get_Organization_OP op)
        {
            m_WhereCondition = "WHERE ID > 0";

            TBL_MAS_ORGANIZATION m_Organization = new TBL_MAS_ORGANIZATION();

            m_rc = m_dbc.Select<TBL_MAS_ORGANIZATION>(ref m_ErrorMessage, m_WhereCondition, ref m_Organization);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            op.m_Organization = m_Organization;

            return 0;
        }
    }
}