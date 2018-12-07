using StajAppCore.Models.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using StajAppCore.Attributes;

namespace StajAppCore.Models.Account
{
    public class RegisterModel
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 5, ErrorMessage = "Длина строки должна быть от 5 до 30 символов")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 30, MinimumLength = 5, ErrorMessage = "Длина строки должна быть от 5 до 30 символов")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        [RegisterRole]
        public int RoleId { get; set; }

        public static explicit operator User(RegisterModel model) =>
            new User()
            {
                Email = model.Email,
                Name = model.Name,
                Password = model.Password,
                Phone = model.Phone,
                RoleId = model.RoleId
            };
    }
}
