using System;
using System.ComponentModel.DataAnnotations;

namespace WrightWayRestaurant.Web.Models
{
    public class CustomerLoginViewModel
    {
        [Required(ErrorMessage = "Name")]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
   
    }

    public class CustomerRegisterViewModel
    {
        [Required(ErrorMessage = "CustomerId")]
        [DataType(DataType.Text)]
        [Display(Name = "CustomerId")]
        public System.Guid CustomerId { get; set; }

        [Required(ErrorMessage = "Name")]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "PhoneNo")]
        [DataType(DataType.Text)]
        [Display(Name = "PhoneNo")]
        public string PhoneNo { get; set; }

        [Required(ErrorMessage = "Email")]
        [DataType(DataType.Text)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
    }
}