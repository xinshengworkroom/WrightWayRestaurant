using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrightWayRestaurant.Model.QueryEntity
{
    public class OrderQuery
    {
        public int? OrderId { get; set; }
        public Guid? CustomerId { get; set; }
    }
}
