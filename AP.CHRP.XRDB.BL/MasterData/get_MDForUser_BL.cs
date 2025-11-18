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
    class get_MDForUser_BL : AC_BC_BusinessLogic
    {

        public get_MDForUser_BL(ref AC_MSSQL_Access dbc)
        {
            m_dbc = dbc;
        }

        public int get_MDForUser(ref get_MDForUser_IP ip, ref get_MDForUser_OP op)
        {
            m_WhereCondition = "WHERE ID > 0";
            List<TBL_MAS_APP_ROLE> ml_AppRole = new List<TBL_MAS_APP_ROLE>();
            m_rc = m_dbc.Select<TBL_MAS_APP_ROLE>(ref m_ErrorMessage, m_WhereCondition, ref ml_AppRole);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }
            op.ml_AppRole = ml_AppRole;

            m_WhereCondition = "WHERE ID > 0";
            List<TBL_MAS_LOCATION_COUNTRY> ml_LocationCountry = new List<TBL_MAS_LOCATION_COUNTRY>();
            m_rc = m_dbc.Select<TBL_MAS_LOCATION_COUNTRY>(ref m_ErrorMessage, m_WhereCondition, ref ml_LocationCountry);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }
            op.ml_LocationCountry = ml_LocationCountry;

            m_WhereCondition = "WHERE ID > 0";
            List<TBL_MAS_ORGANIZATION_DEPARTMENT> ml_OrganizationDepartment = new List<TBL_MAS_ORGANIZATION_DEPARTMENT>();
            m_rc = m_dbc.Select<TBL_MAS_ORGANIZATION_DEPARTMENT>(ref m_ErrorMessage, m_WhereCondition, ref ml_OrganizationDepartment);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }
            op.ml_OrganizationDepartment = ml_OrganizationDepartment;

            m_WhereCondition = "WHERE ID > 0";
            List<TBL_MAS_ORGANIZATION_DESIGNATION> ml_OrganizationDesignation = new List<TBL_MAS_ORGANIZATION_DESIGNATION>();
            m_rc = m_dbc.Select<TBL_MAS_ORGANIZATION_DESIGNATION>(ref m_ErrorMessage, m_WhereCondition, ref ml_OrganizationDesignation);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }
            op.ml_OrganizationDesignation = ml_OrganizationDesignation;

            m_WhereCondition = "WHERE ID > 0";
            List<TBL_MAS_ORGANIZATION_LANGUAGE> ml_OrganizationLanguage = new List<TBL_MAS_ORGANIZATION_LANGUAGE>();
            m_rc = m_dbc.Select<TBL_MAS_ORGANIZATION_LANGUAGE>(ref m_ErrorMessage, m_WhereCondition, ref ml_OrganizationLanguage);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }
            op.ml_OrganizationLanguage = ml_OrganizationLanguage;

            m_WhereCondition = "WHERE ID > 0";
            List<TBL_MAS_ORGANIZATION_CONTRACT> ml_OrganizationContract = new List<TBL_MAS_ORGANIZATION_CONTRACT>();
            m_rc = m_dbc.Select<TBL_MAS_ORGANIZATION_CONTRACT>(ref m_ErrorMessage, m_WhereCondition, ref ml_OrganizationContract);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }
            op.ml_OrganizationContract = ml_OrganizationContract;

            m_WhereCondition = "WHERE ID > 0";
            List<TBL_MAS_ORGANIZATION_BRANCH> ml_OrganizationBranch = new List<TBL_MAS_ORGANIZATION_BRANCH>();
            m_rc = m_dbc.Select<TBL_MAS_ORGANIZATION_BRANCH>(ref m_ErrorMessage, m_WhereCondition, ref ml_OrganizationBranch);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }
            op.ml_OrganizationBranch = ml_OrganizationBranch;

            return 0;
        }
    }
}