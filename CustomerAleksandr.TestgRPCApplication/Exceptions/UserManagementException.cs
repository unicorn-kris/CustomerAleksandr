using System;

namespace Exceptions
{
    public class UserManagementException : Exception
    {
        public string ErrorMessage { get; set; }
    }
}
