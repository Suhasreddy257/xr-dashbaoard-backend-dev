using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AP.CHRP.XRDB.AC.DataContract;
using AP.CHRP.XRDB.DB.DataTable;

namespace AP.CHRP.XRDB.DT.MasterData
{
    [DataContract]
    public class get_LocationCity_OP : AC_BC_DataContract
    {
        [DataMember]
        public int m_CountryID { get; set; }

        [DataMember]
        public int m_StateID { get; set; }

        [DataMember]
        public int m_PageNumber { get; set; }

        [DataMember]
        public int m_RowsPerPage { get; set; }

        [DataMember]
        public List<TBL_MAS_LOCATION_CITY> ml_LocationCity { get; set; }

        public get_LocationCity_OP()
        {
            m_PageNumber = 1;
            m_RowsPerPage = 1000;
            ml_LocationCity = new List<TBL_MAS_LOCATION_CITY>();
        }
    }
}
