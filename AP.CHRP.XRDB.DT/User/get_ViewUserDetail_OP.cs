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
    public class get_ViewUserDetail_OP : AC_BC_DataContract
    {
        [DataMember]
        public int m_UserRoleID { get; set; }

        [DataMember]
        public List<V_UserDetail> ml_UserDetail { get; set; }

        public get_ViewUserDetail_OP()
        {
            ml_UserDetail = new List<V_UserDetail>();
        }
    }
}