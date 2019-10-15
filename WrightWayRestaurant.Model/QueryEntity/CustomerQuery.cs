using System;

namespace WrightWayRestaurant.Model.QueryEntity
{
    public class CustomerQuery
    {
        public Guid? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
    }
}
