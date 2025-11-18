using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AP.CHRP.XRDB.AC.DataContract
{
    [DataContract]
    public class AC_DC_getList
    {
        [DataMember]
        public long m_ID { get; set; }

        [DataMember]
        public Int32 m_Type { get; set; }

        [DataMember]
        public String m_Search { get; set; }

        [DataMember]
        public List<String> ml_SearchColumns { get; set; }

        [DataMember]
        public String m_Limit { get; set; }

        [DataMember]
        public String m_OrderBy { get; set; }

        [DataMember]
        public String m_Duration { get; set; }

        [DataMember]
        public DateTime m_CrDtStart { get; set; }

        [DataMember]
        public DateTime m_CrDtEnd { get; set; }

        [DataMember]
        public String m_WhereCondition { get; set; }

        public AC_DC_getList()
        {

        }

    }
}
