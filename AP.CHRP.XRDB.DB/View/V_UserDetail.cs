using System;
using System.Data;
using System.Data.Common;


namespace AP.CHRP.XRDB.DB.DataTable
{

    public partial class V_UserDetail
    {
        public V_UserDetail()
        {
        }

        /// Column Comment...
        private int _UserID;
        public virtual int UserID
        {
            get
            {
                return this._UserID;
            }
            set
            {
                this._UserID = value;
            }
        }

        /// Column Comment...
        private string _FirstName;
        public virtual string FirstName
        {
            get
            {
                return this._FirstName;
            }
            set
            {
                this._FirstName = value;
            }
        }

        /// Column Comment...
        private string _LastName;
        public virtual string LastName
        {
            get
            {
                return this._LastName;
            }
            set
            {
                this._LastName = value;
            }
        }

        /// Column Comment...
        private string _EmployeeCode;
        public virtual string EmployeeCode
        {
            get
            {
                return this._EmployeeCode;
            }
            set
            {
                this._EmployeeCode = value;
            }
        }

        /// Column Comment...
        private string _EmployeementType;
        public virtual string EmployeementType
        {
            get
            {
                return this._EmployeementType;
            }
            set
            {
                this._EmployeementType = value;
            }
        }

        /// Column Comment...
        private string _Department;
        public virtual string Department
        {
            get
            {
                return this._Department;
            }
            set
            {
                this._Department = value;
            }
        }

        /// Column Comment...
        private string _Designation;
        public virtual string Designation
        {
            get
            {
                return this._Designation;
            }
            set
            {
                this._Designation = value;
            }
        }

        /// Column Comment...
        private int _UserRoleID;
        public virtual int UserRoleID
        {
            get
            {
                return this._UserRoleID;
            }
            set
            {
                this._UserRoleID = value;
            }
        }
    }
}
