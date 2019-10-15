using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WrightWayRestaurant.Web.Models
{
    


    public class SystemUserLoginViewModel
    {
        [Required(ErrorMessage = "用户名")]
        [DataType(DataType.Password)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

    }

    public class SystemUserRegisterViewModel
    {
        public System.Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
    }
}