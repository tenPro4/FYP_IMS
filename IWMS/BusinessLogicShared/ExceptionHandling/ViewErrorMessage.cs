using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicShared.ExceptionHandling
{
    public class ViewErrorMessage
    {
        public ViewErrorMessage(string errorKey, string errorMessage)
        {
            ErrorKey = errorKey;
            ErrorMessage = errorMessage;
        }

        public string ErrorKey { get; set; }
        public string ErrorMessage { get; set; }
    }
}
