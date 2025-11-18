using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AP.CHRP.XRDB.AC.DataContract;
using AP.CHRP.XRDB.DB.DataTable;

namespace AP.CHRP.XRDB.DT.Certificate
{
    [DataContract]
    public class put_NewCertificate_OP : AC_BC_DataContract
    {
        [DataMember]
        public TBL_TRN_USER_CERTIFICATE m_certificate {  get; set; }
        public put_NewCertificate_OP()
        {
            m_certificate = new TBL_TRN_USER_CERTIFICATE();
        }
    }
}