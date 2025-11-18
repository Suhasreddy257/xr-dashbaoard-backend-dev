using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AP.CHRP.XRDB.AC.DataContract;
using AP.CHRP.XRDB.DB.DataTable;

namespace AP.CHRP.XRDB.DT.Event
{
    [DataContract]
    public class get_AllEvent_OP : AC_BC_DataContract
    {
        [DataMember]
        public List<TBL_TRN_EVENT> ml_event { get; set; }

        public get_AllEvent_OP()
        {
            ml_event = new List<TBL_TRN_EVENT>();
        }
    }
}