using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.CHRP.XRDB.UL.GenDTCls
{
    public class DBTableDefination
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

        public List<DBColumnDefination> Columns;

        public DBTableDefination(String strName)
        {
            _Name = strName;
            Columns = new List<DBColumnDefination>();
        }
    }
}
