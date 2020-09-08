using System;
using System.Collections.Generic;
using System.Text;

namespace JwtApiTest.Domain.Requests
{
    public class ChangeUserPassword
    {
        public ChangeUserPassword(string newPassword, string newPasswordConfirmation)
        {
            NewPassword = newPassword;
            NewPasswordConfirmation = newPasswordConfirmation;
        }

        public string NewPassword { get; }
        public string NewPasswordConfirmation { get; }
    }
}
