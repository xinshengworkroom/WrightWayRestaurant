using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WrightWayRestaurant.Web.Models
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "ReserveTime")]
        [DataType(DataType.DateTime)]
        [Display(Name = "ReserveTime")]
        public DateTime ReserveTime { get; set; }
        public int FoodId { get; set; }

        public int Number { get; set; }

    }
}