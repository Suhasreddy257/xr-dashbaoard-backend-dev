using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AP.CHRP.XRDB.AC.DataContract;
using AP.CHRP.XRDB.DB.DataTable;

namespace AP.CHRP.XRDB.DT.MasterData
{
    [DataContract]
    public class put_OrganizationDesignationEdit_IP : AC_BC_DataContract
    {
        [DataMember]
        public TBL_MAS_ORGANIZATION_DESIGNATION m_OrganizationDesignation { get; set; }
        public put_OrganizationDesignationEdit_IP()
        {
            m_OrganizationDesignation = new TBL_MAS_ORGANIZATION_DESIGNATION();
        }
    }
}