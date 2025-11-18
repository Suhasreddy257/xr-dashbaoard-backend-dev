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
    public class get_OrganizationBranch_OP : AC_BC_DataContract
    {

        [DataMember]
        public int m_PageNumber { get; set; }

        [DataMember]
        public int m_RowsPerPage { get; set; }

        [DataMember]
        public List<TBL_MAS_ORGANIZATION_BRANCH> ml_OrganizationBranch { get; set; }

        public get_OrganizationBranch_OP()
        {
            m_PageNumber = 1;
            m_RowsPerPage = 1000;
            ml_OrganizationBranch = new List<TBL_MAS_ORGANIZATION_BRANCH>();
        }
    }
}