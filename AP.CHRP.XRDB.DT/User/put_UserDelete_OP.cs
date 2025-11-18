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
    public class put_UserDelete_OP : AC_BC_DataContract
    {

        [DataMember]
        public TBL_MAS_USER m_user { get; set; }

        [DataMember]
        public TBL_MAS_USER_LOGIN m_user_login { get; set; }
        public put_UserDelete_OP()
        {
            m_user = new TBL_MAS_USER();
            m_user_login = new TBL_MAS_USER_LOGIN();
        }
    }
}