using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AP.CHRP.XRDB.AC.DataContract;

namespace AP.CHRP.XRDB.DT.MasterData
{
    [DataContract]
    public class get_OrganizationCalendar_IP : AC_BC_DataContract
    {

        [DataMember]
        public int m_Year{ get; set; }

        [DataMember]
        public int m_Month { get; set; }

        [DataMember]
        public DateTime m_Date { get; set; }


        public get_OrganizationCalendar_IP()
        {
            DateTime dt = DateTime.Now;

            m_Year = dt.Year;
            m_Month = dt.Month;
            m_Date = dt;

        }
    }
}
