using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.CHRP.XRDB.AC.BusinessLogic
{
    public class AC_ENUM
    {
        public enum getListType
        {
            ID = 0,
            Duration = 1,
            DateTimeRange = 2,
            SearchTextAllColumns = 3,
            SearchTextSpecifiedColumns = 4,
            AllRows = 5,
            SpecifiedWhereCondition = 6
        };

        public enum ErrorType
        {
            DBError = -1,
            DataValidationError = -2,
            BusinessLogicError = -3,
            ExceptionError = -4
        };

        public enum EncryptionType
        {
            No_Encryption = 0,
            MD5 = 1,
            SHA1 = 2,
            SHA2_128 = 3,
        };
    }
}
