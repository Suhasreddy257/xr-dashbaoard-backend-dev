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
    public class get_AppFeatureLicense_OP : AC_BC_DataContract
    {
        [DataMember]
        public List<TBL_MAS_APP_FEATURE_LICENSE> ml_AppFeatureLicense { get; set; }

        public get_AppFeatureLicense_OP()
        {
            ml_AppFeatureLicense = new List<TBL_MAS_APP_FEATURE_LICENSE>();
        }
    }
}
