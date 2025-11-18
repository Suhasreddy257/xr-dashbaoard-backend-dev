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
    public class get_Organization_OP : AC_BC_DataContract
    {
        [DataMember]
        public TBL_MAS_ORGANIZATION m_Organization { get; set; }

        public get_Organization_OP()
        {
            m_Organization = new TBL_MAS_ORGANIZATION();
        }
    }
}
