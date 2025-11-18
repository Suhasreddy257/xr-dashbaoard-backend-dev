using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.CHRP.XRDB.UL.GenDTCls
{
    public class DBColumnDefination
    {
        private String _Name;
        public virtual String Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        private String _DataType;
        public virtual String DataType
        {
            get
            {
                return this._DataType;
            }
            set
            {
                this._DataType = value;
            }
        }

        private String _IsNullable;
        public virtual String IsNullable
        {
            get
            {
                return this._IsNullable;
            }
            set
            {
                this._IsNullable = value;
            }
        }

        private String _ColumnType;
        public virtual String ColumnType
        {
            get
            {
                return this._ColumnType;
            }
            set
            {
                this._ColumnType = value;
            }
        }

    }
}
