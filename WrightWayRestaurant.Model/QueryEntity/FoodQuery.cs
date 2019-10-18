using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrightWayRestaurant.Model.QueryEntity
{
    public class FoodQuery
    {
        public int? FoodId { get; set; }
        public string FoodName { get; set; }
        public int? TypeId { get; set; }
    }
}
