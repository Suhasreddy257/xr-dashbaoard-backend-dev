using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


using AP.CHRP.XRDB.AC.DataContract;

namespace AP.CHRP.XRDB.AC.DataBase
{
    public class AC_MSSQL_ManageConnection
    {
        public static int RequestConnectionObject(ref AC_MSSQL_Access? dbc, ref AC_BC_DataContract ipdc, ref AC_BC_DataContract opdc)
        {
            String err = "";
            dbc = new AC_MSSQL_Access(ipdc.UserDBConnStr);
            if (dbc.OpenConnection(ref err) == false)
            {
                opdc.MessageInfo.ReturnMessage = err;
                opdc.MessageInfo.ReturnValue = -1;
                return -1;
            }
            return 0;
        }

        public static int FreeConnectionObject(ref AC_MSSQL_Access dbc, ref AC_BC_DataContract ipdc, ref AC_BC_DataContract opdc)
        {
            String err = "";
            if (dbc.CloseConnection(ref err) == false)
            {
                opdc.MessageInfo.ReturnMessage = err;
                opdc.MessageInfo.ReturnValue = -1;
                return -1;
            }
            return 0;
        }
    }
}

