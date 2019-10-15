using System;
using System.ComponentModel.DataAnnotations;

namespace WrightWayRestaurant.Web.Models
{
    public class CustomerLoginViewModel
    {
        [Required(ErrorMessage = "用户名")]
        [DataType(DataType.Password)]
        [Display(Name = "用户名")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }
   
    }

    public class CustomerRegisterViewModel
    {
        public System.Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
    }
}