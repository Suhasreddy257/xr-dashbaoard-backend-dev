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
    class get_LocationPostalCode_BL : AC_BC_BusinessLogic
    {

        public get_LocationPostalCode_BL(ref AC_MSSQL_Access dbc)
        {
            m_dbc = dbc;
        }

        public int get_LocationPostalCode(ref get_LocationPostalCode_IP ip, ref get_LocationPostalCode_OP op)
        {
            m_WhereCondition = "WHERE CityID = '" + ip.m_CityID + "' AND CountryID = '" + ip.m_CountryID + "' AND StateID = '" + ip.m_StateID + "'";

            List<TBL_MAS_LOCATION_POSTAL_CODE> ml_LocationPostalCode = new List<TBL_MAS_LOCATION_POSTAL_CODE>();

            m_rc = m_dbc.Select<TBL_MAS_LOCATION_POSTAL_CODE>(ref m_ErrorMessage, m_WhereCondition, ref ml_LocationPostalCode);
            if (m_rc < 0)
            {
                op.MessageInfo.ReturnMessage = m_ErrorMessage;
                op.MessageInfo.ReturnValue = -1;
                return -1;
            }

            op.ml_LocationPostalCode = ml_LocationPostalCode;

            return 0;
        }
    }
}