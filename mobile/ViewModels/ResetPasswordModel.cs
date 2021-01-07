using System;
using System.Collections.Generic;
using System.Text;

namespace mobile.ViewModels
{
    public class ResetPasswordModel
    {
        public string newPassword { get; set; }

        public string repeatPassword { get; set; }

        public string resetCode { get; set; }
    }
}
