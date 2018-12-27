using System.Collections.Generic;
using StajAppCore.Models.Auth.AuthView;
using System.ComponentModel.DataAnnotations;

namespace StajAppCore.Models.Store.StoreView
{
    public class OrderModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "У вашего заказа должно быть описание")]
        public string Description { get; set; }
        [Required(ErrorMessage = "У вашего заказа должен быть адрес доставки")]
        public string DeliveryAddress { get; set; }
        [Required]
        public List<ProductModel> Products { get; set; }

        public UserModel UserOppositeSide { get; set; }
        public bool DeliveredOppositeSide { get; set; }
        public bool UserCancelled { get; set; }

        public static explicit operator Order (OrderModel OrM) => 
            new Order()
            {
                Description = OrM.Description,
                DeliveryAddress = OrM.DeliveryAddress
            };
    }
}
