using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows;
using System.Reflection;
using System.Configuration;
using System.Threading;

using Microsoft.Data.SqlClient;


namespace AP.CHRP.XRDB.AC.DataBase
{
    public class AC_MSSQL_Access
    {
        private SqlConnection connection;
        private SqlTransaction tr = null;
        private static AC_MSSQL_Access myInstance;
        private String m_ConnectionString;

        public bool m_Initialization;
        public bool m_Busy;
        public String strErrorMessage;

        //Constructor
        public AC_MSSQL_Access()
        {
            m_Initialization = false;
            m_Busy = false;
            strErrorMessage = "";
            string connectionString = "Server = 127.0.0.1; database = kier_production_db; UID = dev; password = dev@123";
            Initialize(connectionString);
        }

        public AC_MSSQL_Access(String strConnectionString)
        {
            strErrorMessage = "";
            Initialize(strConnectionString);
        }

        public static AC_MSSQL_Access getInstance(String ConnectionString)
        {
            if (myInstance == null)
            {
                myInstance = new AC_MSSQL_Access(ConnectionString);
            }
            return myInstance;
        }

        //Code for private Funcitons......
        private void Initialize(string connectionString)
        {
            try
            {
                connection = new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
                connection = null;
                m_Initialization = false;
                return;
            }
            m_Initialization = true;
        }

        public bool OpenConnection(ref String strErrorMessage)
        {
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }
            }
            catch (SqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        strErrorMessage = "Cannot connect to server.  Contact administrator";
                        break;
                    case 1045:
                        strErrorMessage = "Invalid username/password, please try again";
                        break;
                    default:
                        strErrorMessage = ex.Message;
                        break;
                }

                return false;
            }
            strErrorMessage = "";
            return true;
        }

        public bool CloseConnection(ref String strErrorMessage)
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                strErrorMessage = ex.Message;
                return false;
            }
        }

        public void BeginTransaction()
        {
            tr = connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (tr != null)
            {
                tr.Commit();
            }
        }
        public void RollBackTransaction()
        {
            if (tr != null)
            {
                tr.Rollback();
            }
        }

        private void TrimAndOr(String strAndOr, ref String strQueryCondition)
        {
            if (strAndOr == "or")
            {
                strQueryCondition = strQueryCondition.TrimEnd(' ');
                strQueryCondition = strQueryCondition.TrimEnd('r');
                strQueryCondition = strQueryCondition.TrimEnd('o');
                strQueryCondition = strQueryCondition.TrimEnd(' ');
            }

            if (strAndOr == "and")
            {
                strQueryCondition = strQueryCondition.TrimEnd(' ');
                strQueryCondition = strQueryCondition.TrimEnd('d');
                strQueryCondition = strQueryCondition.TrimEnd('n');
                strQueryCondition = strQueryCondition.TrimEnd('a');
                strQueryCondition = strQueryCondition.TrimEnd(' ');
            }
        }

        private int Insert<T>(ref String strErrorMessage, ref T dataRow)
        {
            Type mTableName = typeof(T);
            String strTableName = mTableName.Name;  //mTableName.GetType().Name;
            String strCommandText = "INSERT INTO " + strTableName + " VALUES(";

            MemberInfo[] arrMemberInfo;
            arrMemberInfo = mTableName.GetMembers();

            // Prepare Command Text names
            foreach (MemberInfo propertyInfo in arrMemberInfo)
            {
                //Get Only Public Property;
                if (propertyInfo.MemberType == MemberTypes.Property)
                {
                    try
                    {
                        String strColumnName = propertyInfo.Name;
                        if(strColumnName != "ID")
                        {
                            strCommandText = strCommandText + "@" + strColumnName + ", ";
                            var property = dataRow.GetType().GetProperty(strColumnName);
                        }

                    }
                    catch (Exception ex)
                    {
                        strErrorMessage = ex.Message;
                        return -1;
                    }
                }
            }

            strCommandText = strCommandText.TrimEnd(' ');
            strCommandText = strCommandText.TrimEnd(',');
            strCommandText = strCommandText + ");";

            SqlCommand cmd = new SqlCommand(strCommandText);
            cmd.Connection = connection;

            // AssignValues names
            foreach (MemberInfo propertyInfo in arrMemberInfo)
            {
                //Get Only Public Property;
                if (propertyInfo.MemberType == MemberTypes.Property)
                {
                    try
                    {
                        String strColumnName = propertyInfo.Name;
                        if (strColumnName != "ID")
                        {
                            var property = dataRow.GetType().GetProperty(strColumnName);
                            var value = property.GetValue(dataRow);
                            cmd.Parameters.AddWithValue("@" + strColumnName, value);
                        }
                    }
                    catch (Exception ex)
                    {
                        strErrorMessage = ex.Message;
                        return -1;
                    }
                }
            }

            int rc;
            try
            {
                rc = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
                return -1;
            }

            String strWhereCondition = "ORDER BY ID DESC";
            rc = Select_ForSave(ref strErrorMessage, strWhereCondition, ref dataRow);

            return rc;
        }

        private int Insert_V1<T>(ref String strErrorMessage, ref T dataRow)
        {
            Type mTableName = typeof(T);
            String strTableName = mTableName.Name;  //mTableName.GetType().Name;
            String strCommandText = "INSERT INTO " + strTableName + " VALUES(";

            MemberInfo[] arrMemberInfo;
            arrMemberInfo = mTableName.GetMembers();

            // Prepare Command Text names
            foreach (MemberInfo propertyInfo in arrMemberInfo)
            {
                //Get Only Public Property;
                if (propertyInfo.MemberType == MemberTypes.Property)
                {
                    try
                    {
                        String strColumnName = propertyInfo.Name;
                        strCommandText = strCommandText + "@" + strColumnName + ", ";
                        var property = dataRow.GetType().GetProperty(strColumnName);
                    }
                    catch (Exception ex)
                    {
                        strErrorMessage = ex.Message;
                        return -1;
                    }
                }
            }

            strCommandText = strCommandText.TrimEnd(' ');
            strCommandText = strCommandText.TrimEnd(',');
            strCommandText = strCommandText + ");";

            SqlCommand cmd = new SqlCommand(strCommandText);
            cmd.Connection = connection;

            // AssignValues names
            foreach (MemberInfo propertyInfo in arrMemberInfo)
            {
                //Get Only Public Property;
                if (propertyInfo.MemberType == MemberTypes.Property)
                {
                    try
                    {
                        String strColumnName = propertyInfo.Name;
                        var property = dataRow.GetType().GetProperty(strColumnName);
                        var value = property.GetValue(dataRow);
                        cmd.Parameters.AddWithValue("@" + strColumnName, value);
                    }
                    catch (Exception ex)
                    {
                        strErrorMessage = ex.Message;
                        return -1;
                    }
                }
            }

            int rc;
            try
            {
                rc = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
                return -1;
            }

            String strWhereCondition = "ORDER BY ID DESC LIMIT 1";
            rc = Select_ForSave(ref strErrorMessage, strWhereCondition, ref dataRow);

            return rc;
        }

        private int Update<T>(ref String strErrorMessage, ref T dataRow)
        {
            Type mTableName = typeof(T);
            String strTableName = mTableName.Name;  //mTableName.GetType().Name;
            String strCommandText = "UPDATE " + strTableName + " SET ";

            MemberInfo[] arrMemberInfo;
            arrMemberInfo = dataRow.GetType().GetMembers();

            // Prepare Command Text names
            foreach (MemberInfo propertyInfo in arrMemberInfo)
            {
                //Get Only Public Property;
                if (propertyInfo.MemberType == MemberTypes.Property)
                {
                    try
                    {
                        String strColumnName = propertyInfo.Name;
                        if (strColumnName != "ID")
                        {
                            strCommandText = strCommandText + strColumnName + " = @" + strColumnName + ", ";
                            var property = dataRow.GetType().GetProperty(strColumnName);
                        }
                    }
                    catch (Exception ex)
                    {
                        strErrorMessage = ex.Message;
                        return -1;
                    }
                }
            }

            strCommandText = strCommandText.TrimEnd(' ');
            strCommandText = strCommandText.TrimEnd(',');
            strCommandText = strCommandText + " WHERE ID = @ID;";



            SqlCommand cmd = new SqlCommand(strCommandText);
            cmd.Connection = connection;

            // AssignValues names
            foreach (MemberInfo propertyInfo in arrMemberInfo)
            {
                //Get Only Public Property;
                if (propertyInfo.MemberType == MemberTypes.Property)
                {
                    try
                    {
                        String strColumnName = propertyInfo.Name;
                        var property = dataRow.GetType().GetProperty(strColumnName);
                        var value = property.GetValue(dataRow);
                        cmd.Parameters.AddWithValue("@" + strColumnName, value);
                    }
                    catch (Exception ex)
                    {
                        strErrorMessage = ex.Message;

                        return -1;
                    }
                }
            }

            int rc;
            try
            {
                rc = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
                return -1;
            }

            return rc;
        }

        private int Update_V1<T>(ref String strErrorMessage, ref T dataRow)
        {
            Type mTableName = typeof(T);
            String strTableName = mTableName.Name;  //mTableName.GetType().Name;
            String strCommandText = "UPDATE " + strTableName + " SET ";

            MemberInfo[] arrMemberInfo;
            arrMemberInfo = dataRow.GetType().GetMembers();

            // Prepare Command Text names
            foreach (MemberInfo propertyInfo in arrMemberInfo)
            {
                //Get Only Public Property;
                if (propertyInfo.MemberType == MemberTypes.Property)
                {
                    try
                    {
                        String strColumnName = propertyInfo.Name;
                        if (strColumnName != "ID")
                        {
                            strCommandText = strCommandText + "`" + strColumnName + "` = @" + strColumnName + ", ";
                            var property = dataRow.GetType().GetProperty(strColumnName);
                        }
                    }
                    catch (Exception ex)
                    {
                        strErrorMessage = ex.Message;
                        return -1;
                    }
                }
            }

            strCommandText = strCommandText.TrimEnd(' ');
            strCommandText = strCommandText.TrimEnd(',');
            strCommandText = strCommandText + " WHERE ID = @ID;";



            SqlCommand cmd = new SqlCommand(strCommandText);
            cmd.Connection = connection;

            // AssignValues names
            foreach (MemberInfo propertyInfo in arrMemberInfo)
            {
                //Get Only Public Property;
                if (propertyInfo.MemberType == MemberTypes.Property)
                {
                    try
                    {
                        String strColumnName = propertyInfo.Name;
                        var property = dataRow.GetType().GetProperty(strColumnName);
                        var value = property.GetValue(dataRow);
                        cmd.Parameters.AddWithValue("@" + strColumnName, value);
                    }
                    catch (Exception ex)
                    {
                        strErrorMessage = ex.Message;
                        return -1;
                    }
                }
            }

            int rc;
            try
            {
                rc = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
                return -1;
            }

            return rc;
        }
        private int Update<T>(ref String strErrorMessage, ref T dataRow, ref List<String> ml_Columns)
        {
            Type mTableName = typeof(T);
            String strTableName = mTableName.Name;  //mTableName.GetType().Name;
            String strCommandText = "UPDATE " + strTableName + " SET ";

            MemberInfo[] arrMemberInfo;
            arrMemberInfo = dataRow.GetType().GetMembers();

            // Prepare Command Text names
            foreach (MemberInfo propertyInfo in arrMemberInfo)
            {
                //Get Only Public Property;
                if (propertyInfo.MemberType == MemberTypes.Property)
                {
                    try
                    {
                        String strColumnName = propertyInfo.Name;
                        if (strColumnName != "ID")
                        {
                            for (int i = 0; i < ml_Columns.Count; i++)
                            {
                                if (ml_Columns[i] == strColumnName)
                                {
                                    strCommandText = strCommandText + strColumnName + " = @" + strColumnName + ", ";
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        strErrorMessage = ex.Message;

                        return -1;
                    }
                }
            }

            strCommandText = strCommandText.TrimEnd(' ');
            strCommandText = strCommandText.TrimEnd(',');
            strCommandText = strCommandText + " WHERE ID = @ID;";



            SqlCommand cmd = new SqlCommand(strCommandText);
            cmd.Connection = connection;
            cmd.CommandTimeout = 60; //60 Seconds.
            // AssignValues names
            foreach (MemberInfo propertyInfo in arrMemberInfo)
            {
                //Get Only Public Property;
                if (propertyInfo.MemberType == MemberTypes.Property)
                {
                    try
                    {
                        ml_Columns.Add("ID");
                        for (int i = 0; i < ml_Columns.Count; i++)
                        {
                            String strColumnName = propertyInfo.Name;
                            if (ml_Columns[i] == strColumnName)
                            {
                                var property = dataRow.GetType().GetProperty(strColumnName);
                                var value = property.GetValue(dataRow);
                                cmd.Parameters.AddWithValue("@" + strColumnName, value);
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        strErrorMessage = ex.Message;

                        return -1;
                    }
                }
            }

            int rc;
            try
            {
                rc = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
                return -1;
            }

            return rc;
        }

        private int Update_V1<T>(ref String strErrorMessage, ref T dataRow, ref List<String> ml_Columns)
        {
            Type mTableName = typeof(T);
            String strTableName = mTableName.Name;  //mTableName.GetType().Name;
            String strCommandText = "UPDATE " + strTableName + " SET ";

            MemberInfo[] arrMemberInfo;
            arrMemberInfo = dataRow.GetType().GetMembers();

            // Prepare Command Text names
            foreach (MemberInfo propertyInfo in arrMemberInfo)
            {
                //Get Only Public Property;
                if (propertyInfo.MemberType == MemberTypes.Property)
                {
                    try
                    {
                        String strColumnName = propertyInfo.Name;
                        if (strColumnName != "ID")
                        {
                            for (int i = 0; i < ml_Columns.Count; i++)
                            {
                                if (ml_Columns[i] == strColumnName)
                                {
                                    strCommandText = strCommandText + "`" + strColumnName + "` = @" + strColumnName + ", ";
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        strErrorMessage = ex.Message;
                        return -1;
                    }
                }
            }

            strCommandText = strCommandText.TrimEnd(' ');
            strCommandText = strCommandText.TrimEnd(',');
            strCommandText = strCommandText + " WHERE ID = @ID;";



            SqlCommand cmd = new SqlCommand(strCommandText);
            cmd.Connection = connection;
            cmd.CommandTimeout = 60; //60 Seconds.
            // AssignValues names
            foreach (MemberInfo propertyInfo in arrMemberInfo)
            {
                //Get Only Public Property;
                if (propertyInfo.MemberType == MemberTypes.Property)
                {
                    try
                    {
                        ml_Columns.Add("ID");
                        for (int i = 0; i < ml_Columns.Count; i++)
                        {
                            String strColumnName = propertyInfo.Name;
                            if (ml_Columns[i] == strColumnName)
                            {
                                var property = dataRow.GetType().GetProperty(strColumnName);
                                var value = property.GetValue(dataRow);
                                cmd.Parameters.AddWithValue("@" + strColumnName, value);
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        strErrorMessage = ex.Message;
                        return -1;
                    }
                }
            }

            int rc;
            try
            {
                rc = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
                return -1;
            }

            return rc;
        }

        //Code for public Funcitons......

        public String SearchAllColumns<T>(String strAndOr, ref T dataRow, String SearchText)
        {
            String strQueryCondition = "WHERE";

            // get all public static properties of MyClass type
            MemberInfo[] arrMemberInfo;
            arrMemberInfo = dataRow.GetType().GetMembers();

            // write property names
            foreach (MemberInfo propertyInfo in arrMemberInfo)
            {
                //Get Only Public Property;
                if (propertyInfo.MemberType == MemberTypes.Property)
                {
                    String strColumnName = propertyInfo.Name;
                    strQueryCondition = strQueryCondition + " " + strColumnName + " like '%" + SearchText + "%' " + strAndOr + " ";
                }
            }

            TrimAndOr(strAndOr, ref strQueryCondition);
            return strQueryCondition;
        }

        public String SearchInColumns(String strAndOr, ref List<String> ml_Columns, String SearchText)
        {
            String strQueryCondition = "WHERE";

            for (int i = 0; i < ml_Columns.Count; i++)
            {
                strQueryCondition = strQueryCondition + " " + ml_Columns[i] + " like '%" + SearchText + "%' " + strAndOr + " ";
            }

            TrimAndOr(strAndOr, ref strQueryCondition);

            return strQueryCondition;
        }

        public String PIHMS_Where(String strWhereCondition)
        {
            String strWhereCondition_New;

            strWhereCondition_New = strWhereCondition.Replace("WHERE ", "WHERE IsActive = 1 and ");

            return strWhereCondition_New;
        }


        private int Select_ForSave<T>(ref String strErrorMessage, String strWhereCondition, ref T dataRow)
        {
            Type mTableName = typeof(T);

            String strTableName = mTableName.Name;  //mTableName.GetType().Name;


            strWhereCondition = PIHMS_Where(strWhereCondition);
            String strQuery = "SELECT TOP 1 * FROM " + strTableName + " " + strWhereCondition;

            //Open connection
            if (this.OpenConnection(ref strErrorMessage) == true)
            {
                //Create Command
                SqlCommand cmd = new SqlCommand(strQuery, connection);
                //Create a data reader and Execute the command
                SqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    if (dataReader.HasRows)
                    {

                        // get all public static properties of MyClass type
                        MemberInfo[] arrMemberInfo;
                        arrMemberInfo = dataRow.GetType().GetMembers();

                        // write property names
                        foreach (MemberInfo propertyInfo in arrMemberInfo)
                        {
                            //Get Only Public Property;
                            if (propertyInfo.MemberType == MemberTypes.Property)
                            {
                                try
                                {
                                    String strColumnName = propertyInfo.Name;
                                    var property = dataRow.GetType().GetProperty(strColumnName);

                                    object value = dataReader[strColumnName];
                                    if (value == DBNull.Value)
                                        value = null;

                                    property.SetValue(dataRow, value, null);

                                }
                                catch (Exception ex)
                                {
                                    strErrorMessage = ex.Message;
                                    return -1;
                                }
                            }
                        }

                        break;
                    }
                }

                //close Data Reader
                dataReader.Close();
                return 1;
            }
            else
            {
                return -2;
            }
            return 0;
        }

        public int Select<T>(ref String strErrorMessage, String strWhereCondition, ref T dataRow)
        {
            Type mTableName = typeof(T);

            String strTableName = mTableName.Name;  //mTableName.GetType().Name;


            strWhereCondition = PIHMS_Where(strWhereCondition);
            String strQuery = "SELECT * FROM " + strTableName + " " + strWhereCondition;

            //Open connection
            if (this.OpenConnection(ref strErrorMessage) == true)
            {
                //Create Command
                SqlCommand cmd = new SqlCommand(strQuery, connection);
                cmd.CommandTimeout = 100;
                //Create a data reader and Execute the command
                SqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    if (dataReader.HasRows)
                    {

                        // get all public static properties of MyClass type
                        MemberInfo[] arrMemberInfo;
                        arrMemberInfo = dataRow.GetType().GetMembers();

                        // write property names
                        foreach (MemberInfo propertyInfo in arrMemberInfo)
                        {
                            //Get Only Public Property;
                            if (propertyInfo.MemberType == MemberTypes.Property)
                            {
                                try
                                {
                                    String strColumnName = propertyInfo.Name;
                                    var property = dataRow.GetType().GetProperty(strColumnName);

                                    object value = dataReader[strColumnName];
                                    if (value == DBNull.Value)
                                        value = null;

                                    property.SetValue(dataRow, value, null);

                                }
                                catch (Exception ex)
                                {
                                    strErrorMessage = ex.Message;
                                    // m_Log.Error("DBConnection.Select() " + strQuery);
                                    // m_Log.Error("DBConnection.Select() " + strErrorMessage);
                                    return -1;
                                }
                            }
                        }

                        break;
                    }
                }

                //close Data Reader
                dataReader.Close();
                // CloseConnection(ref strErrorMessage);
                return 1;
            }
            else
            {
                return -2;
            }

            // CloseConnection(ref strErrorMessage);
            return 0;
        }

        public int Select<T>(ref String strErrorMessage, String strWhereCondition, ref List<T> ml_Rows)
        {
            Type mTableName = typeof(T);

            String strTableName = mTableName.Name;  //mTableName.GetType().Name;

            strWhereCondition = PIHMS_Where(strWhereCondition);
            String strQuery = "SELECT * FROM " + strTableName + " " + strWhereCondition;

            //Open connection
            try
            {
                if (this.OpenConnection(ref strErrorMessage) == true)
                {
                    //Create Command
                    SqlCommand cmd = new SqlCommand(strQuery, connection);
                    cmd.CommandTimeout = 100;
                    //Create a data reader and Execute the command
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        if (dataReader.HasRows)
                        {
                            //Create New Instace of the Table Row Object
                            T T_Object = Activator.CreateInstance<T>();

                            // get all public static properties of MyClass type
                            MemberInfo[] arrMemberInfo;
                            arrMemberInfo = T_Object.GetType().GetMembers();

                            // write property names
                            foreach (MemberInfo propertyInfo in arrMemberInfo)
                            {
                                //Get Only Public Property;
                                if (propertyInfo.MemberType == MemberTypes.Property)
                                {
                                    try
                                    {
                                        String strColumnName = propertyInfo.Name;
                                        var property = T_Object.GetType().GetProperty(strColumnName);

                                        object value = dataReader[strColumnName];
                                        if (value == DBNull.Value)
                                            value = null;

                                        property.SetValue(T_Object, value, null);
                                    }
                                    catch (Exception ex)
                                    {
                                        strErrorMessage = ex.Message;
                                        // m_Log.Error("DBConnection.Select() " + strQuery);
                                        // m_Log.Error("DBConnection.Select() " + strErrorMessage);
                                        return -1;
                                    }
                                }
                            }

                            //Adding the Read Row to the List
                            ml_Rows.Add(T_Object);
                        }
                    }

                    //close Data Reader
                    dataReader.Close();
                    // CloseConnection(ref strErrorMessage);
                    return ml_Rows.Count();
                }
                else
                {
                    return -2;
                }

            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
                // m_Log.Error("DBConnection.Select() " + strQuery);
                // m_Log.Error("DBConnection.Select() " + strErrorMessage);
                return -1;
            }

            // CloseConnection(ref strErrorMessage);
            return 0;
        }

        public int SelectView<T>(ref String strErrorMessage, String strWhereCondition, ref List<T> ml_Rows)
        {
            Type mTableName = typeof(T);

            String strTableName = mTableName.Name;  //mTableName.GetType().Name;

            String strQuery = "SELECT * FROM " + strTableName + " " + strWhereCondition;

            //Open connection
            try
            {
                if (this.OpenConnection(ref strErrorMessage) == true)
                {
                    //Create Command
                    SqlCommand cmd = new SqlCommand(strQuery, connection);
                    cmd.CommandTimeout = 100;
                    //Create a data reader and Execute the command
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        if (dataReader.HasRows)
                        {
                            //Create New Instace of the Table Row Object
                            T T_Object = Activator.CreateInstance<T>();

                            // get all public static properties of MyClass type
                            MemberInfo[] arrMemberInfo;
                            arrMemberInfo = T_Object.GetType().GetMembers();

                            // write property names
                            foreach (MemberInfo propertyInfo in arrMemberInfo)
                            {
                                //Get Only Public Property;
                                if (propertyInfo.MemberType == MemberTypes.Property)
                                {
                                    try
                                    {
                                        String strColumnName = propertyInfo.Name;
                                        var property = T_Object.GetType().GetProperty(strColumnName);

                                        object value = dataReader[strColumnName];
                                        if (value == DBNull.Value)
                                            value = null;

                                        property.SetValue(T_Object, value, null);
                                    }
                                    catch (Exception ex)
                                    {
                                        strErrorMessage = ex.Message;
                                        // m_Log.Error("DBConnection.Select() " + strQuery);
                                        // m_Log.Error("DBConnection.Select() " + strErrorMessage);
                                        return -1;
                                    }
                                }
                            }

                            //Adding the Read Row to the List
                            ml_Rows.Add(T_Object);
                        }
                    }

                    //close Data Reader
                    dataReader.Close();
                    // CloseConnection(ref strErrorMessage);
                    return ml_Rows.Count();
                }
                else
                {
                    return -2;
                }

            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
                // m_Log.Error("DBConnection.Select() " + strQuery);
                // m_Log.Error("DBConnection.Select() " + strErrorMessage);
                return -1;
            }

            // CloseConnection(ref strErrorMessage);
            return 0;
        }


        public int Select_IsRowDeleted_Not_Added<T>(ref String strErrorMessage, String strWhereCondition, ref List<T> ml_Rows)
        {
            Type mTableName = typeof(T);

            String strTableName = mTableName.Name;  //mTableName.GetType().Name;

            String strQuery = "SELECT * FROM " + strTableName + " " + strWhereCondition;

            //Open connection
            try
            {
                if (this.OpenConnection(ref strErrorMessage) == true)
                {
                    //Create Command
                    SqlCommand cmd = new SqlCommand(strQuery, connection);
                    cmd.CommandTimeout = 100;
                    //Create a data reader and Execute the command
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        if (dataReader.HasRows)
                        {
                            //Create New Instace of the Table Row Object
                            T T_Object = Activator.CreateInstance<T>();

                            // get all public static properties of MyClass type
                            MemberInfo[] arrMemberInfo;
                            arrMemberInfo = T_Object.GetType().GetMembers();

                            // write property names
                            foreach (MemberInfo propertyInfo in arrMemberInfo)
                            {
                                //Get Only Public Property;
                                if (propertyInfo.MemberType == MemberTypes.Property)
                                {
                                    try
                                    {
                                        String strColumnName = propertyInfo.Name;
                                        var property = T_Object.GetType().GetProperty(strColumnName);

                                        object value = dataReader[strColumnName];
                                        if (value == DBNull.Value)
                                            value = null;

                                        property.SetValue(T_Object, value, null);
                                    }
                                    catch (Exception ex)
                                    {
                                        strErrorMessage = ex.Message;
                                        // m_Log.Error("DBConnection.Select() " + strQuery);
                                        // m_Log.Error("DBConnection.Select() " + strErrorMessage);
                                        return -1;
                                    }
                                }
                            }

                            //Adding the Read Row to the List
                            ml_Rows.Add(T_Object);
                        }
                    }

                    //close Data Reader
                    dataReader.Close();
                    // CloseConnection(ref strErrorMessage);
                    return ml_Rows.Count();
                }
                else
                {
                    return -2;
                }

            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
                // m_Log.Error("DBConnection.Select() " + strQuery);
                // m_Log.Error("DBConnection.Select() " + strErrorMessage);
                return -1;
            }

            // CloseConnection(ref strErrorMessage);
            return 0;
        }

        public int Select<T>(ref String strErrorMessage, ref List<String> ml_Columns, String strWhereCondition, ref List<T> ml_Rows)
        {
            Type mTableName = typeof(T);

            String strTableName = mTableName.Name;  //mTableName.GetType().Name;

            strWhereCondition = PIHMS_Where(strWhereCondition);
            String strQuery = "SELECT";
            for (int i = 0; i < ml_Columns.Count; i++)
            {
                strQuery = strQuery + " " + ml_Columns[i] + ",";
            }
            strQuery = strQuery.TrimEnd(',');
            strQuery = strQuery + " FROM " + strTableName + " " + strWhereCondition;

            //Open connection
            if (this.OpenConnection(ref strErrorMessage) == true)
            {
                //Create Command
                SqlCommand cmd = new SqlCommand(strQuery, connection);
                cmd.CommandTimeout = 100;
                //Create a data reader and Execute the command
                SqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    if (dataReader.HasRows)
                    {
                        //Create New Instace of the Table Row Object
                        T T_Object = Activator.CreateInstance<T>();

                        // get only Specified columns
                        for (int i = 0; i < ml_Columns.Count; i++)
                        {
                            try
                            {
                                String strColumnName = ml_Columns[i];
                                var property = T_Object.GetType().GetProperty(strColumnName);

                                object value = dataReader[strColumnName];
                                if (value == DBNull.Value)
                                    value = null;

                                property.SetValue(T_Object, value, null);
                            }
                            catch (Exception ex)
                            {
                                // CloseConnection(ref strErrorMessage);
                                strErrorMessage = ex.Message;
                                // m_Log.Error("DBConnection.Select() " + strQuery);
                                // m_Log.Error("DBConnection.Select() " + strErrorMessage);

                                return -1;
                            }
                        }

                        //Adding the Read Row to the List
                        ml_Rows.Add(T_Object);
                    }
                }

                //close Data Reader
                dataReader.Close();

                // CloseConnection(ref strErrorMessage);
                return ml_Rows.Count();
            }
            else
            {
                // CloseConnection(ref strErrorMessage);
                return -2;
            }

            // CloseConnection(ref strErrorMessage);
            return 0;
        }

        public int SelectScalar(ref String strErrorMessage, String strQuery, ref object res)
        {
            if (this.OpenConnection(ref strErrorMessage) == true)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(strQuery, connection);
                    res = cmd.ExecuteScalar();
                    // CloseConnection(ref strErrorMessage);
                    return 1;
                }
                catch (Exception ex)
                {
                    strErrorMessage = ex.Message;
                    // m_Log.Error("DBConnection.SelectScalar() " + strQuery);
                    // m_Log.Error("DBConnection.SelectScalar() " + strErrorMessage);
                    return -1;
                }
            }
            else
            {
                return -2;
            }

            // CloseConnection(ref strErrorMessage);
            return 0;
        }

        public int Save<T>(ref String strErrorMessage, ref T m_Row)
        {
            //Open connection


            if (this.OpenConnection(ref strErrorMessage) == false)
            {
                return -1;
            }

            T t1 = m_Row;
            var property = t1.GetType().GetProperty("ID");
            int value = (int)property.GetValue(t1);
            if (value == 0)
            {
                if (Insert(ref strErrorMessage, ref t1) < 0)
                {
                    // this.CloseConnection(ref strErrorMessage);
                    return -1;
                }
            }
            else
            {
                if (Update(ref strErrorMessage, ref t1) < 0)
                {
                    // this.CloseConnection(ref strErrorMessage);
                    return -1;
                }
            }

            //Just Give 100 Milisecond sleep.
            Thread.Sleep(100);

            // this.CloseConnection(ref strErrorMessage);
            return 0;
        }

        public int Save<T>(ref String strErrorMessage, ref List<T> ml_Rows)
        {
            //Open connection


            if (this.OpenConnection(ref strErrorMessage) == false)
            {
                return -1;
            }

            for (int i = 0; i < ml_Rows.Count; i++)
            {
                T t1 = ml_Rows[i];
                var property = t1.GetType().GetProperty("ID");
                //long value = (long)property.GetValue(t1);
                long value = Convert.ToInt64(property.GetValue(t1));
                if (value == 0)
                {
                    if (Insert(ref strErrorMessage, ref t1) < 0)
                    {
                        // this.CloseConnection(ref strErrorMessage);
                        return -1;
                    }
                }
                else
                {
                    if (Update(ref strErrorMessage, ref t1) < 0)
                    {
                        // this.CloseConnection(ref strErrorMessage);
                        return -1;
                    }
                }

                //Just Give 100 Milisecond sleep.
                Thread.Sleep(100);
            }

            // this.CloseConnection(ref strErrorMessage);
            return 0;
        }

        public int InsertWithPK<T>(ref String strErrorMessage, ref List<T> ml_Rows)
        {
            //Open connection


            if (this.OpenConnection(ref strErrorMessage) == false)
            {
                return -1;
            }

            for (int i = 0; i < ml_Rows.Count; i++)
            {
                T t1 = ml_Rows[i];
                if (Insert(ref strErrorMessage, ref t1) < 0)
                {
                    // this.CloseConnection(ref strErrorMessage);
                    return -1;
                }

                //Just Give 100 Milisecond sleep.
                Thread.Sleep(100);
            }

            // this.CloseConnection(ref strErrorMessage);
            return 0;
        }




        public int Save<T>(ref String strErrorMessage, ref List<T> ml_Rows, ref List<String> ml_Columns)
        {
            //Open connection
            if (this.OpenConnection(ref strErrorMessage) == false)
            {
                return -1;
            }

            for (int i = 0; i < ml_Rows.Count; i++)
            {
                T t1 = ml_Rows[i];
                var property = t1.GetType().GetProperty("ID");
                long value = (long)property.GetValue(t1);
                if (value == 0)
                {
                    if (Insert(ref strErrorMessage, ref t1) < 0)
                    {
                        // this.CloseConnection(ref strErrorMessage);
                        return -1;
                    }
                }
                else
                {
                    if (Update(ref strErrorMessage, ref t1, ref ml_Columns) < 0)
                    {
                        // this.CloseConnection(ref strErrorMessage);
                        return -1;
                    }
                }
            }

            // this.CloseConnection(ref strErrorMessage);
            return 0;
        }

        public int DeleteAllRows<T>(ref String strErrorMessage, ref T dr)
        {
            //Open connection
            if (this.OpenConnection(ref strErrorMessage) == false)
            {
                return -1;
            }

            Type mTableName = typeof(T);
            String strCommandText = "DELETE  FROM " + mTableName.Name;
            SqlCommand cmd = new SqlCommand(strCommandText);
            cmd.Connection = connection;
            cmd.CommandTimeout = 60; //60 Seconds.

            int rc;
            try
            {
                rc = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
                // m_Log.Error("DBConnection.DeleteAllRows() " + strCommandText);
                // m_Log.Error("DBConnection.DeleteAllRows() " + strErrorMessage);
                return -1;
            }

            // this.CloseConnection(ref strErrorMessage);
            return 0;
        }

        public int DeleteAllRows(ref String strErrorMessage, String strTable)
        {
            //Open connection
            if (this.OpenConnection(ref strErrorMessage) == false)
            {
                return -1;
            }

            String strCommandText = "DELETE  FROM " + strTable;
            SqlCommand cmd = new SqlCommand(strCommandText);
            cmd.Connection = connection;
            cmd.CommandTimeout = 60; //60 Seconds.

            int rc;
            try
            {
                rc = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
                // m_Log.Error("DBConnection.DeleteAllRows() " + strCommandText);
                // m_Log.Error("DBConnection.DeleteAllRows() " + strErrorMessage);

                return -1;
            }

            // this.CloseConnection(ref strErrorMessage);
            return 0;
        }

        public int DeleteAllRowsCondition(ref String strErrorMessage, String strTable, String Condition)
        {
            //Open connection
            if (this.OpenConnection(ref strErrorMessage) == false)
            {
                return -1;
            }

            String strCommandText = "DELETE  FROM " + strTable + " " + Condition;
            SqlCommand cmd = new SqlCommand(strCommandText);
            cmd.Connection = connection;
            cmd.CommandTimeout = 60; //60 Seconds.

            int rc;
            try
            {
                rc = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
                // m_Log.Error("DBConnection.DeleteAllRowsCondition() " + strCommandText);
                // m_Log.Error("DBConnection.DeleteAllRowsCondition() " + strErrorMessage);

                return -1;
            }

            // this.CloseConnection(ref strErrorMessage);
            return 0;
        }

        public int AssignCreaterData<T>(long strStaffID, ref String strErrorMessage, ref T dataRow)
        {
            DateTime dt = DateTime.Now;
            PropertyInfo[] arrPropertyInfo = dataRow.GetType().GetProperties();

            // Prepare Command Text names
            foreach (PropertyInfo propertyInfo in arrPropertyInfo)
            {
                //Get Only Public Property;
                if (propertyInfo.MemberType == MemberTypes.Property)
                {
                    try
                    {
                        String strColumnName = propertyInfo.Name;
                        switch (strColumnName)
                        {
                            case "CreatedStaffID":
                                propertyInfo.SetValue(dataRow, strStaffID);
                                break;
                            case "CreatedDateTime":
                                propertyInfo.SetValue(dataRow, dt);
                                break;
                            case "UpdatedStaffID":
                                propertyInfo.SetValue(dataRow, strStaffID);
                                break;
                            case "UpdatedDateTime":
                                propertyInfo.SetValue(dataRow, dt);
                                break;
                            case "IsRowDeleted":
                                propertyInfo.SetValue(dataRow, "N");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        strErrorMessage = ex.Message;
                        return -1;
                    }
                }
            }

            return 0;
        }

        public int AssignCreaterData<T>(long strStaffID, ref String strErrorMessage, ref List<T> ml_Rows)
        {
            for (int i = 0; i < ml_Rows.Count; i++)
            {
                T t1 = ml_Rows[i];
                AssignCreaterData(strStaffID, ref strErrorMessage, ref t1);
            }
            return 0;
        }

        public int AssignUpdaterData<T>(long strStaffID, ref String strErrorMessage, ref T dataRow)
        {
            DateTime dt = DateTime.Now;
            PropertyInfo[] arrPropertyInfo = dataRow.GetType().GetProperties();

            // Prepare Command Text names
            foreach (PropertyInfo propertyInfo in arrPropertyInfo)
            {
                //Get Only Public Property;
                if (propertyInfo.MemberType == MemberTypes.Property)
                {
                    try
                    {
                        String strColumnName = propertyInfo.Name;
                        switch (strColumnName)
                        {
                            case "UpdatedStaffID":
                                propertyInfo.SetValue(dataRow, strStaffID);
                                break;
                            case "UpdatedDateTime":
                                propertyInfo.SetValue(dataRow, dt);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        strErrorMessage = ex.Message;
                        // m_Log.Error("DBConnection.AssignUpdaterData() " + strErrorMessage);
                        return -1;
                    }
                }
            }

            return 0;
        }

        public int AssignUpdaterData<T>(long strStaffID, ref String strErrorMessage, ref List<T> ml_Rows)
        {
            for (int i = 0; i < ml_Rows.Count; i++)
            {
                T t1 = ml_Rows[i];
                AssignUpdaterData(strStaffID, ref strErrorMessage, ref t1);
            }
            return 0;
        }

        public String getWhereContionIDList<T>(ref List<T> ml_Rows)
        {
            String strCondition = " WHERE ";

            for (int i = 0; i < ml_Rows.Count; i++)
            {
                try
                {
                    T t1 = ml_Rows[i];
                    var property = t1.GetType().GetProperty("ID");
                    long value = (long)property.GetValue(t1);

                    if (i == 0)
                    {
                        strCondition = strCondition + " ID = " + value.ToString();
                    }
                    else
                    {
                        strCondition = strCondition + " or ID = " + value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    strErrorMessage = ex.Message;
                    continue;
                }
            }

            return strCondition;
        }


        private int GetInsertCommand<T>(ref String strErrorMessage, ref T dataRow, ref String strCommandText)
        {
            Type mTableName = typeof(T);
            String strTableName = mTableName.Name;  //mTableName.GetType().Name;
            strCommandText = "INSERT INTO " + strTableName + " (";

            MemberInfo[] arrMemberInfo;
            arrMemberInfo = mTableName.GetMembers();

            // Prepare Command Text names
            foreach (MemberInfo propertyInfo in arrMemberInfo)
            {
                //Get Only Public Property;
                if (propertyInfo.MemberType == MemberTypes.Property)
                {
                    try
                    {
                        String strColumnName = propertyInfo.Name;
                        strCommandText = strCommandText + strColumnName + ", ";
                        var property = dataRow.GetType().GetProperty(strColumnName);
                    }
                    catch (Exception ex)
                    {
                        strErrorMessage = ex.Message;
                        // m_Log.Error("DBConnection.GetInsertCommand() " + strCommandText);
                        // m_Log.Error("DBConnection.GetInsertCommand() " + strErrorMessage);

                        return -1;
                    }
                }
            }

            strCommandText = strCommandText.TrimEnd(' ');
            strCommandText = strCommandText.TrimEnd(',');
            strCommandText = strCommandText + ") VALUES ";


            String strSingleRowValue = " ( ";

            foreach (MemberInfo propertyInfo in arrMemberInfo)
            {
                //Get Only Public Property;
                if (propertyInfo.MemberType == MemberTypes.Property)
                {
                    try
                    {
                        String strColumnName = propertyInfo.Name;
                        var property = dataRow.GetType().GetProperty(strColumnName);
                        var value = property.GetValue(dataRow);
                        var valuetype = value.GetType();
                        strSingleRowValue = strSingleRowValue + value;
                    }
                    catch (Exception ex)
                    {
                        strErrorMessage = ex.Message;
                        // m_Log.Error("DBConnection.GetInsertCommand() " + strCommandText);
                        // m_Log.Error("DBConnection.GetInsertCommand() " + strErrorMessage);

                        return -1;
                    }
                }
            }

            return 0;
        }



        public int InsertSingleQuery<T>(ref String strErrorMessage, ref List<T> ml_Rows)
        {
            if (ml_Rows.Count <= 0)
            {
                strErrorMessage = "There is no rows list to Insert ml_Rows.Count = " + ml_Rows.Count.ToString();
                return -1;
            }


            T t1 = ml_Rows[0];
            Type mTableName = typeof(T);
            String strTableName = mTableName.Name;  //mTableName.GetType().Name;
            String strCommandText = "INSERT INTO " + strTableName + " (";

            MemberInfo[] arrMemberInfo;
            arrMemberInfo = mTableName.GetMembers();

            // Prepare Command Text names
            foreach (MemberInfo propertyInfo in arrMemberInfo)
            {
                //Get Only Public Property;
                if (propertyInfo.MemberType == MemberTypes.Property)
                {
                    try
                    {
                        String strColumnName = propertyInfo.Name;
                        strCommandText = strCommandText + strColumnName + ", ";
                        var property = t1.GetType().GetProperty(strColumnName);
                    }
                    catch (Exception ex)
                    {
                        strErrorMessage = ex.Message;
                        return -1;
                    }
                }
            }

            strCommandText = strCommandText.TrimEnd(' ');
            strCommandText = strCommandText.TrimEnd(',');
            strCommandText = strCommandText + ") VALUES ";

            for (int i = 0; i < ml_Rows.Count; i++)
            {
                String strSingleRowValue = " ( ";

                T dataRow = ml_Rows[i];

                foreach (MemberInfo propertyInfo in arrMemberInfo)
                {
                    //Get Only Public Property;
                    if (propertyInfo.MemberType == MemberTypes.Property)
                    {
                        try
                        {
                            String strColumnName = propertyInfo.Name;
                            var property = dataRow.GetType().GetProperty(strColumnName);
                            var value = property.GetValue(dataRow);


                            String ValueString = "";
                            if (value != null)
                            {
                                ValueString = value.ToString();
                                var valuetype = value.GetType();
                                switch (valuetype.Name)
                                {
                                    case "Int64":
                                    case "Int32":
                                    case "Decimal":
                                        strSingleRowValue = strSingleRowValue + ValueString + ", ";
                                        break;
                                    case "DateTime":
                                        DateTime dtVal = (DateTime)value;
                                        ValueString = dtVal.ToString("yyyy-MM-dd HH:mm:ss");
                                        strSingleRowValue = strSingleRowValue + " '" + ValueString + "' , ";
                                        break;
                                    case "String":
                                        strSingleRowValue = strSingleRowValue + " '" + ValueString + "' , ";
                                        break;
                                    default:
                                        strSingleRowValue = strSingleRowValue + " '" + ValueString + "' , ";
                                        break;
                                }
                            }
                            else
                            {
                                strSingleRowValue = strSingleRowValue + " NULL, ";
                            }



                        }
                        catch (Exception ex)
                        {
                            strErrorMessage = ex.Message;
                            // m_Log.Error("DBConnection.InsertSingleQuery() " + strCommandText);
                            // m_Log.Error("DBConnection.InsertSingleQuery() " + strErrorMessage);
                            return -1;
                        }
                    }
                }

                strSingleRowValue = strSingleRowValue.TrimEnd(' ');
                strSingleRowValue = strSingleRowValue.TrimEnd(',');
                strSingleRowValue = strSingleRowValue + "), ";

                strCommandText = strCommandText + strSingleRowValue;
            }

            strCommandText = strCommandText.TrimEnd(' ');
            strCommandText = strCommandText.TrimEnd(',');


            if (this.OpenConnection(ref strErrorMessage) == false)
            {
                return -2;
            }

            SqlCommand cmd = new SqlCommand(strCommandText);
            cmd.Connection = connection;

            int rc;
            try
            {
                rc = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
                // m_Log.Error("DBConnection.InsertSingleQuery() " + strCommandText);
                // m_Log.Error("DBConnection.InsertSingleQuery() " + strErrorMessage);

                return -1;
            }

            // this.CloseConnection(ref strErrorMessage);
            return 0;
        }


        public int ExecureNonQuery(ref String strErrorMessage, String strCommandText)
        {
            if (this.OpenConnection(ref strErrorMessage) == false)
            {
                return -1;
            }

            SqlCommand cmd = new SqlCommand(strCommandText);
            cmd.Connection = connection;
            cmd.CommandTimeout = 60; //60 Seconds.

            int rc;
            try
            {
                rc = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
                // m_Log.Error("DBConnection.ExecureNonQuery() " + strCommandText);
                // m_Log.Error("DBConnection.ExecureNonQuery() " + strErrorMessage);

                return -1;
            }

            return 0;
        }



        //public int ExecuteSqlScript(ref String strErrorMessage, String strSQLScript)
        //{
        //    if (this.OpenConnection(ref strErrorMessage) == false)
        //    {
        //        return -1;
        //    }

        //    SqlScript script = new SqlScript(strSQLScript);
        //    script.Connection = connection;

        //    int rc;
        //    try
        //    {
        //        rc = script.Execute();
        //    }
        //    catch (Exception ex)
        //    {
        //        strErrorMessage = ex.Message;
        //        return -1;
        //    }

        //    return 0;
        //}

        public int Save<T>(ref String strErrorMessage, ref T m_Row, ref List<String> ml_Columns)
        {
            if (this.OpenConnection(ref strErrorMessage) == false)
            {
                return -1;
            }

            T t1 = m_Row;
            var property = t1.GetType().GetProperty("ID");
            long value = (long)property.GetValue(t1);
            if (value == 0)
            {
                if (Insert_V1(ref strErrorMessage, ref t1) < 0)
                {
                    return -1;
                }
            }
            else
            {
                if (Update_V1(ref strErrorMessage, ref t1, ref ml_Columns) < 0)
                {
                    return -1;
                }
            }

            return 0;
        }


    }

}



