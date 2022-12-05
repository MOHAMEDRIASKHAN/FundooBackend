using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer
{
    public class PasswordValidation
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The NewPassword and ConfirmPassword do not match.")]

        public string ConfirmPassword { get; set; }
    }
}
