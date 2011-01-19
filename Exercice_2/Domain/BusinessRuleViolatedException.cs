using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class BusinessRuleViolatedException : ApplicationException
    {
        public readonly ErrorCode ErrorCode;

        public BusinessRuleViolatedException(ErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }
    }

    public enum ErrorCode
    {
        MaximumOfMediaPermitedExceeded,
        CanNotRentWhenLateReturn,
        AlreadyRented,
        MediaIsNotRentedByThisCustomer,
    }
}
