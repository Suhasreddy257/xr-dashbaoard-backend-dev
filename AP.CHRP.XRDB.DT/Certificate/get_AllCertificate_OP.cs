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
    public class get_AllCertificate_OP : AC_BC_DataContract
    {
        [DataMember]
        public List<V_USERCERTIFICATEDETAILS> ml_certificate { get; set; }
        public get_AllCertificate_OP()
        {
            ml_certificate = new List<V_USERCERTIFICATEDETAILS> (); 
        }
    }
}