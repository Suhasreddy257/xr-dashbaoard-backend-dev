using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AP.CHRP.XRDB.AC.DataContract
{

    public class AC_DT_MessageInfo
    {
        public AC_DT_MessageInfo() { }

        // Possible Values 0 = Sucesses, > 0 = Warning, < = Error. Look in to Status & Return Message for Details
        Int32 _ReturnValue;
        [DataMember]
        public Int32 ReturnValue
        {
            get { return _ReturnValue; }
            set { _ReturnValue = value; }
        }

        String _ReturnMessage;
        [DataMember]
        public String ReturnMessage
        {
            get { return _ReturnMessage; }
            set { _ReturnMessage = value; }
        }

    }


    [DataContract]
    public class AC_BC_DataContract
    {

        AC_DT_MessageInfo _MessageInfo;
        [DataMember]
        public AC_DT_MessageInfo MessageInfo
        {
            get { return _MessageInfo; }
            set { _MessageInfo = value; }
        }


        String? _UserDBConnStr;
        [DataMember]
        public String? UserDBConnStr
        {
            get { return _UserDBConnStr; }
            set { _UserDBConnStr = value; }
        }

    }

}
