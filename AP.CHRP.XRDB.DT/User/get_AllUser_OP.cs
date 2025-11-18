using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AP.CHRP.XRDB.AC.DataContract;
using AP.CHRP.XRDB.DB.DataTable;

namespace AP.CHRP.XRDB.DT.User
{
    [DataContract]
    public class get_AllUser_OP : AC_BC_DataContract
    {
        [DataMember]
        public List<V_UserDetail> ml_User { get; set; }
        public get_AllUser_OP()
        {
            ml_User = new List<V_UserDetail>();
        }
    }
}