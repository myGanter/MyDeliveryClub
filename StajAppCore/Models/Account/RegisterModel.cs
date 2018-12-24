using StajAppCore.Attributes;
using StajAppCore.Models.Auth;
using System.ComponentModel.DataAnnotations;

namespace StajAppCore.Models.Account
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Поле Имя не должно быть пустым")]
        [StringLength(maximumLength: 30, MinimumLength = 5, ErrorMessage = "Длина строки должна быть от 5 до 30 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле Email не должно быть пустым")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле Пароль не должно быть пустым")]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 30, MinimumLength = 5, ErrorMessage = "Длина пароля должна быть от 5 до 30 символов")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Поле Телефон не должно быть пустым")]
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
