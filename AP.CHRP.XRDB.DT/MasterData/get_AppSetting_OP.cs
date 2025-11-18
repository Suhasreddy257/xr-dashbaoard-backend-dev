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
    public class get_AppSetting_OP : AC_BC_DataContract
    {
        [DataMember]
        public List<TBL_MAS_APP_SETTING> ml_AppSetting { get; set; }

        public get_AppSetting_OP()
        {
            ml_AppSetting = new List<TBL_MAS_APP_SETTING>();
        }
    }
}
