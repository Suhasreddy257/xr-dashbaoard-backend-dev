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
    public class get_AllUserDetails_OP : AC_BC_DataContract
    {

        [DataMember]
        public List<V_UserDetail> ml_user { get; set; }


        public get_AllUserDetails_OP()
        {
            ml_user = new List<V_UserDetail> ();
        }
    }
}