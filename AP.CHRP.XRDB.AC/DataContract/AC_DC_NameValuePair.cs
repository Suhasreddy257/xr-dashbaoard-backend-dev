using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AP.CHRP.XRDB.AC.DataContract
{
    public class AC_DC_NameValuePair
    {
       

        public AC_DC_NameValuePair(String nam, String val)
        {
            _Name = nam;
            _Value = val;
        }


        public String Name
        {
            get { return _Name; }
            set { _Name = value;  }
        }
        private String _Name;


        public String Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        private String _Value;

    }
}

