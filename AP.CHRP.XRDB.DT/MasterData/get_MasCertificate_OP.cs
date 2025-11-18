using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AP.CHRP.XRDB.AC.DataContract;
using AP.CHRP.XRDB.DB.DataTable;

namespace AP.CHRP.XRDB.DT.MasterData
{
    [DataContract]
    public class get_MasCertificate_OP : AC_BC_DataContract
    {
        [DataMember]
        public List<TBL_MAS_CERTIFICATE> ml_Certificate { get; set; }

        public get_MasCertificate_OP()
        {
            ml_Certificate = new List<TBL_MAS_CERTIFICATE>();
        }
    }
}