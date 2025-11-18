using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AP.CHRP.XRDB.AC.DataContract;
using AP.CHRP.XRDB.DB.DataTable;

namespace AP.CHRP.XRDB.DT.Login
{
    [DataContract]
    public class get_LoginAccess_OP : AC_BC_DataContract
    {
        [DataMember]
        public bool IsValidUser { get; set; }

        [DataMember]
        public TBL_MAS_USER m_user { get; set; }

        [DataMember]
        public TBL_MAS_USER_LOGIN m_user_login { get; set; }

        [DataMember]
        public TBL_MAS_ORGANIZATION m_organization { get; set; }

        [DataMember]
        public String m_login_token { get; set; }

        public get_LoginAccess_OP()
        {
            IsValidUser = new bool();
            m_user = new TBL_MAS_USER();
            m_user_login = new TBL_MAS_USER_LOGIN();
            m_organization = new TBL_MAS_ORGANIZATION();
            m_login_token = "";
        }
    }
}