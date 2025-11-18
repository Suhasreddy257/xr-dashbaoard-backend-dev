using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AP.CHRP.XRDB.AC.DataContract;

namespace AP.CHRP.XRDB.DT.User
{
    [DataContract]
    public class get_UserDetail_IP : AC_BC_DataContract
    {
        [DataMember]
        public int m_UserID { get; set; }
        public get_UserDetail_IP()
        {
        }
    }
}