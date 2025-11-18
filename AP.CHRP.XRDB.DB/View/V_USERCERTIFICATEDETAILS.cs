using System;
using System.Data;
using System.Data.Common;


namespace AP.CHRP.XRDB.DB.DataTable
{

    public partial class V_USERCERTIFICATEDETAILS
    {
        public V_USERCERTIFICATEDETAILS()
        {
        }

        /// Column Comment...
        private string _UserName;
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }

        /// Column Comment...
        private string _CertificateName;
        public virtual string CertificateName
        {
            get
            {
                return this._CertificateName;
            }
            set
            {
                this._CertificateName = value;
            }
        }

        /// Column Comment...
        private string _CertificateType;
        public virtual string CertificateType
        {
            get
            {
                return this._CertificateType;
            }
            set
            {
                this._CertificateType = value;
            }
        }

        /// Column Comment...
        private DateTime _IssueDate;
        public virtual DateTime IssueDate
        {
            get
            {
                return this._IssueDate;
            }
            set
            {
                this._IssueDate = value;
            }
        }

        /// Column Comment...
        private string _CertificateURL;
        public virtual string CertificateURL
        {
            get
            {
                return this._CertificateURL;
            }
            set
            {
                this._CertificateURL = value;
            }
        }

        /// Column Comment...
        private bool _IsRevoked;
        public virtual bool IsRevoked
        {
            get
            {
                return this._IsRevoked;
            }
            set
            {
                this._IsRevoked = value;
            }
        }

        /// Column Comment...
        private bool _IsActive;
        public virtual bool IsActive
        {
            get
            {
                return this._IsActive;
            }
            set
            {
                this._IsActive = value;
            }
        }
    }
}
