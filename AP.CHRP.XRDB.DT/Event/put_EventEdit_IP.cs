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
    public class put_EventEdit_OP : AC_BC_DataContract
    {
        [DataMember]
        public TBL_TRN_EVENT m_event { get; set; }

        [DataMember]
        public List<TBL_TRN_EVENT_PARTICIPANT> ml_event_participant { get; set; }

        public put_EventEdit_OP()
        {
            m_event = new TBL_TRN_EVENT();
            ml_event_participant = new List<TBL_TRN_EVENT_PARTICIPANT>();
        }
    }
}