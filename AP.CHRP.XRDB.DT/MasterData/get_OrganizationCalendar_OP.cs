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
    public class get_OrganizationCalendar_OP : AC_BC_DataContract
    {

        [DataMember]
        public int m_Year { get; set; }

        [DataMember]
        public int m_Month { get; set; }

        [DataMember]
        public DateTime m_Date { get; set; }

        [DataMember]
        public List<TBL_MAS_ORGANIZATION_CALENDAR> ml_OrganizationCalendar { get; set; }

        //public List<TBL_MAS_ORGANIZATION_CALENDAR_EVENT> ml_CalanderDateEventCount { get; set; }

        public get_OrganizationCalendar_OP()
        {
            ml_OrganizationCalendar = new List<TBL_MAS_ORGANIZATION_CALENDAR>();
            //ml_CalanderDateEventCount = new List<TBL_MAS_ORGANIZATION_CALENDAR_EVENT>();
        }
    }
}
