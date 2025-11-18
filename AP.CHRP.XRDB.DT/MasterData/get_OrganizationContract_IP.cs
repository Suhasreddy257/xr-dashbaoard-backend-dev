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
    public class get_OrganizationContract_IP : AC_BC_DataContract
    {

        [DataMember]
        public int m_PageNumber { get; set; }

        [DataMember]
        public int m_RowsPerPage { get; set; }

        public get_OrganizationContract_IP()
        {
            m_PageNumber = 1;
            m_RowsPerPage = 1000;
        }
    }
}