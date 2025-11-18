using AP.CHRP.XRDB.AC.DataContract;
using AP.CHRP.XRDB.DB.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AP.CHRP.XRDB.DT.MasterData
{
    [DataContract]
    public class get_MDForUser_OP : AC_BC_DataContract
    {

        [DataMember]
        public List<TBL_MAS_APP_ROLE> ml_AppRole { get; set; }

        [DataMember]
        public List<TBL_MAS_LOCATION_COUNTRY> ml_LocationCountry { get; set; }

        [DataMember]
        public List<TBL_MAS_ORGANIZATION_DEPARTMENT> ml_OrganizationDepartment { get; set; }

        [DataMember]
        public List<TBL_MAS_ORGANIZATION_DESIGNATION> ml_OrganizationDesignation { get; set; }

        [DataMember]
        public List<TBL_MAS_ORGANIZATION_LANGUAGE> ml_OrganizationLanguage { get; set; }

        [DataMember]
        public List<TBL_MAS_ORGANIZATION_CONTRACT> ml_OrganizationContract { get; set; }

        [DataMember]
        public List<TBL_MAS_ORGANIZATION_BRANCH> ml_OrganizationBranch { get; set; }

        public get_MDForUser_OP()
        {
            ml_AppRole = new List<TBL_MAS_APP_ROLE>();
            ml_LocationCountry = new List<TBL_MAS_LOCATION_COUNTRY>();
            ml_OrganizationDepartment = new List<TBL_MAS_ORGANIZATION_DEPARTMENT>();
            ml_OrganizationDesignation = new List<TBL_MAS_ORGANIZATION_DESIGNATION>();
            ml_OrganizationLanguage = new List<TBL_MAS_ORGANIZATION_LANGUAGE>();
            ml_OrganizationContract = new List<TBL_MAS_ORGANIZATION_CONTRACT>();
            ml_OrganizationBranch = new List<TBL_MAS_ORGANIZATION_BRANCH>();
        }
    }
}