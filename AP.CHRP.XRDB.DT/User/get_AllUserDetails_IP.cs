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
    public class get_AllUserDetails_IP : AC_BC_DataContract
    {
        [DataMember]
        public string ml_UserID { get; set; }
        public get_AllUserDetails_IP()
        {
        }
    }
}