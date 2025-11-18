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
    public class get_AppModule_OP : AC_BC_DataContract
    {
        [DataMember]
        public List<TBL_MAS_APP_MODULE> ml_AppModule { get; set; }

        public get_AppModule_OP()
        {
            ml_AppModule = new List<TBL_MAS_APP_MODULE>();
        }
    }
}
