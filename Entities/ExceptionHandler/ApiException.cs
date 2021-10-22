using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Entities.ExceptionHandler
{
    /// <summary>
    /// A class for API exception
    /// </summary>
    public class ApiException : Exception
    {
        public ApiException() : base()
        {
        }
        public ApiException(string message) : base(message)
        {
        }
        public ApiException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
