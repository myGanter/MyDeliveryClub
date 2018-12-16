using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StajAppCore.Models.Account
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Некорректный адрес")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = ":(")]
        [StringLength(maximumLength: 30, MinimumLength = 5, ErrorMessage = ":(")]
        public string Password { get; set; }
    }
}
