using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MadPay724.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="ایمیل وارد شده معتبر نمی باشد")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
