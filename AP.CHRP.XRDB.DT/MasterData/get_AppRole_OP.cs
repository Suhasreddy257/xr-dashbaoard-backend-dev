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
    public class get_AppRole_OP : AC_BC_DataContract
    {
        [DataMember]
        public List<TBL_MAS_APP_ROLE> ml_AppRole { get; set; }

        public get_AppRole_OP()
        {
            ml_AppRole = new List<TBL_MAS_APP_ROLE>();
        }
    }
}
