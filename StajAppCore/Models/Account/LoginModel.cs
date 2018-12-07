using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StajAppCore.Models.Account
{
    public class LoginModel
    {
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 5, ErrorMessage = "Длина строки должна быть от 5 до 30 символов")]
        public string Password { get; set; }
    }
}
