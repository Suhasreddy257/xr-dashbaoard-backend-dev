using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AP.CHRP.XRDB.AC.DataContract;

namespace AP.CHRP.XRDB.DT.MasterData
{
    [DataContract]
    public class get_OrganizationLanguage_IP : AC_BC_DataContract
    {

        [DataMember]
        public int m_PageNumber { get; set; }

        [DataMember]
        public int m_RowsPerPage { get; set; }

        public get_OrganizationLanguage_IP()
        {
            m_PageNumber = 1;
            m_RowsPerPage = 1000;
        }
    }
}
