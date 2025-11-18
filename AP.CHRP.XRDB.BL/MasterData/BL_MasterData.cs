
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AP.CHRP.XRDB.AC.BusinessLogic;
using AP.CHRP.XRDB.AC.DataBase;
using AP.CHRP.XRDB.AC.DataContract;
using AP.CHRP.XRDB.BL.User;
using AP.CHRP.XRDB.DT.MasterData;
using AP.CHRP.XRDB.DT.User;



namespace AP.CHRP.XRDB.BL.MasterData
{
    public class BL_MasterData
    {
        private int rc;
        private AC_MSSQL_Access m_dbc;
        private AC_BC_DataContract _ip;
        private AC_BC_DataContract _op;

        public int get_LocationCity(ref get_LocationCity_IP ip, ref get_LocationCity_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                get_LocationCity_BL bl = new get_LocationCity_BL(ref m_dbc);
                rc = bl.get_LocationCity(ref ip, ref op);
            }
            catch (Exception ex)
            {
                op.MessageInfo.ReturnMessage = ex.Message;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            AC_MSSQL_ManageConnection.FreeConnectionObject(ref m_dbc, ref _ip, ref _op);
            return rc;
        }

        public int get_LocationState(ref get_LocationState_IP ip, ref get_LocationState_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                get_LocationState_BL bl = new get_LocationState_BL(ref m_dbc);
                rc = bl.get_LocationState(ref ip, ref op);
            }
            catch (Exception ex)
            {
                op.MessageInfo.ReturnMessage = ex.Message;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            AC_MSSQL_ManageConnection.FreeConnectionObject(ref m_dbc, ref _ip, ref _op);
            return rc;
        }

        public int get_LocationCountry(ref get_LocationCountry_IP ip, ref get_LocationCountry_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                get_LocationCountry_BL bl = new get_LocationCountry_BL(ref m_dbc);
                rc = bl.get_LocationCountry(ref ip, ref op);
            }
            catch (Exception ex)
            {
                op.MessageInfo.ReturnMessage = ex.Message;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            AC_MSSQL_ManageConnection.FreeConnectionObject(ref m_dbc, ref _ip, ref _op);
            return rc;
        }

        public int get_LocationPostalCode(ref get_LocationPostalCode_IP ip, ref get_LocationPostalCode_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                get_LocationPostalCode_BL bl = new get_LocationPostalCode_BL(ref m_dbc);
                rc = bl.get_LocationPostalCode(ref ip, ref op);
            }
            catch (Exception ex)
            {
                op.MessageInfo.ReturnMessage = ex.Message;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            AC_MSSQL_ManageConnection.FreeConnectionObject(ref m_dbc, ref _ip, ref _op);
            return rc;
        }

        public int get_AppRole(ref get_AppRole_IP ip, ref get_AppRole_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                get_AppRole_BL bl = new get_AppRole_BL(ref m_dbc);
                rc = bl.get_AppRole(ref ip, ref op);
            }
            catch (Exception ex)
            {
                op.MessageInfo.ReturnMessage = ex.Message;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            AC_MSSQL_ManageConnection.FreeConnectionObject(ref m_dbc, ref _ip, ref _op);
            return rc;
        }

        public int get_Organization(ref get_Organization_IP ip, ref get_Organization_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                get_Organization_BL bl = new get_Organization_BL(ref m_dbc);
                rc = bl.get_Organization(ref ip, ref op);
            }
            catch (Exception ex)
            {
                op.MessageInfo.ReturnMessage = ex.Message;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            AC_MSSQL_ManageConnection.FreeConnectionObject(ref m_dbc, ref _ip, ref _op);
            return rc;
        }

        public int get_MDForUser(ref get_MDForUser_IP ip, ref get_MDForUser_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                get_MDForUser_BL bl = new get_MDForUser_BL(ref m_dbc);
                rc = bl.get_MDForUser(ref ip, ref op);
            }
            catch (Exception ex)
            {
                op.MessageInfo.ReturnMessage = ex.Message;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            AC_MSSQL_ManageConnection.FreeConnectionObject(ref m_dbc, ref _ip, ref _op);
            return rc;
        }

        public int get_OrganizationDepartment(ref get_OrganizationDepartment_IP ip, ref get_OrganizationDepartment_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                get_OrganizationDepartment_BL bl = new get_OrganizationDepartment_BL(ref m_dbc);
                rc = bl.get_OrganizationDepartment(ref ip, ref op);
            }
            catch (Exception ex)
            {
                op.MessageInfo.ReturnMessage = ex.Message;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            AC_MSSQL_ManageConnection.FreeConnectionObject(ref m_dbc, ref _ip, ref _op);
            return rc;
        }

        public int get_OrganizationDesignation(ref get_OrganizationDesignation_IP ip, ref get_OrganizationDesignation_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                get_OrganizationDesignation_BL bl = new get_OrganizationDesignation_BL(ref m_dbc);
                rc = bl.get_OrganizationDesignation(ref ip, ref op);
            }
            catch (Exception ex)
            {
                op.MessageInfo.ReturnMessage = ex.Message;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            AC_MSSQL_ManageConnection.FreeConnectionObject(ref m_dbc, ref _ip, ref _op);
            return rc;
        }

        public int get_MasCertificate(ref get_MasCertificate_IP ip, ref get_MasCertificate_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                get_MasCertificate_BL bl = new get_MasCertificate_BL(ref m_dbc);
                rc = bl.get_MasCertificate(ref ip, ref op);
            }
            catch (Exception ex)
            {
                op.MessageInfo.ReturnMessage = ex.Message;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            AC_MSSQL_ManageConnection.FreeConnectionObject(ref m_dbc, ref _ip, ref _op);
            return rc;
        }

        public int put_OrganizationDepartment(ref put_OrganizationDepartment_IP ip, ref put_OrganizationDepartment_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                put_OrganizationDepartment_BL bl = new put_OrganizationDepartment_BL(ref m_dbc);
                rc = bl.put_OrganizationDepartment(ref ip, ref op);
            }
            catch (Exception ex)
            {
                op.MessageInfo.ReturnMessage = ex.Message;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            AC_MSSQL_ManageConnection.FreeConnectionObject(ref m_dbc, ref _ip, ref _op);
            return rc;
        }

        public int put_OrganizationDesignation(ref put_OrganizationDesignation_IP ip, ref put_OrganizationDesignation_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                put_OrganizationDesignation_BL bl = new put_OrganizationDesignation_BL(ref m_dbc);
                rc = bl.put_OrganizationDesignation(ref ip, ref op);
            }
            catch (Exception ex)
            {
                op.MessageInfo.ReturnMessage = ex.Message;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            AC_MSSQL_ManageConnection.FreeConnectionObject(ref m_dbc, ref _ip, ref _op);
            return rc;
        }

        public int put_OrganizationDepartmentEdit(ref put_OrganizationDepartmentEdit_IP ip, ref put_OrganizationDepartmentEdit_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                put_OrganizationDepartmentEdit_BL bl = new put_OrganizationDepartmentEdit_BL(ref m_dbc);
                rc = bl.put_OrganizationDepartmentEdit(ref ip, ref op);
            }
            catch (Exception ex)
            {
                op.MessageInfo.ReturnMessage = ex.Message;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            AC_MSSQL_ManageConnection.FreeConnectionObject(ref m_dbc, ref _ip, ref _op);
            return rc;
        }

        public int put_OrganizationDesignationEdit(ref put_OrganizationDesignationEdit_IP ip, ref put_OrganizationDesignationEdit_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                put_OrganizationDesignationEdit_BL bl = new put_OrganizationDesignationEdit_BL(ref m_dbc);
                rc = bl.put_OrganizationDesignationEdit(ref ip, ref op);
            }
            catch (Exception ex)
            {
                op.MessageInfo.ReturnMessage = ex.Message;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            AC_MSSQL_ManageConnection.FreeConnectionObject(ref m_dbc, ref _ip, ref _op);
            return rc;
        }

        public int put_OrganizationDepartmentDelete(ref put_OrganizationDepartmentDelete_IP ip, ref put_OrganizationDepartmentDelete_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                put_OrganizationDepartmentDelete_BL bl = new put_OrganizationDepartmentDelete_BL(ref m_dbc);
                rc = bl.put_OrganizationDepartmentDelete(ref ip, ref op);
            }
            catch (Exception ex)
            {
                op.MessageInfo.ReturnMessage = ex.Message;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            AC_MSSQL_ManageConnection.FreeConnectionObject(ref m_dbc, ref _ip, ref _op);
            return rc;
        }

        public int put_OrganizationDesignationDelete(ref put_OrganizationDesignationDelete_IP ip, ref put_OrganizationDesignationDelete_OP op)
        {
            _ip = ip; _op = op;
            if (AC_MSSQL_ManageConnection.RequestConnectionObject(ref m_dbc, ref _ip, ref _op) < 0)
            {
                return -1;
            }

            try
            {
                put_OrganizationDesignationDelete_BL bl = new put_OrganizationDesignationDelete_BL(ref m_dbc);
                rc = bl.put_OrganizationDesignationDelete(ref ip, ref op);
            }
            catch (Exception ex)
            {
                op.MessageInfo.ReturnMessage = ex.Message;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            AC_MSSQL_ManageConnection.FreeConnectionObject(ref m_dbc, ref _ip, ref _op);
            return rc;
        }

    }
}