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
    class put_OrganizationDepartment_BL : AC_BC_BusinessLogic
    {

        public put_OrganizationDepartment_BL(ref AC_MSSQL_Access dbc)
        {
            m_dbc = dbc;
        }

        public int put_OrganizationDepartment(ref put_OrganizationDepartment_IP ip, ref put_OrganizationDepartment_OP op)
        {

            TBL_MAS_ORGANIZATION_DEPARTMENT m_OrganizationDepartment = ip.m_OrganizationDepartment;

            m_rc = m_dbc.Save(ref m_ErrorMessage, ref m_OrganizationDepartment);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            op.m_OrganizationDepartment = m_OrganizationDepartment;

            return 0;
        }
    }
}