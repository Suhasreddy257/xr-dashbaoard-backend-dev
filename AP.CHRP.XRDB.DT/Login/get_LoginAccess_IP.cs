using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AP.CHRP.XRDB.AC.DataContract;

namespace AP.CHRP.XRDB.DT.Login
{
    [DataContract]
    public class get_LoginAccess_IP : AC_BC_DataContract
    {
        [DataMember]
        public String m_UserName { get; set; }

        [DataMember]
        public String m_Password { get; set; }

        [DataMember]
        public int m_EncryptionType { get; set; }

        public get_LoginAccess_IP()
        {
            m_UserName = "";
            m_Password = "";
            m_EncryptionType = new int();
        }
    }
}
