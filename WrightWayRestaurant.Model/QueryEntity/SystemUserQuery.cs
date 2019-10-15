using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrightWayRestaurant.Model.QueryEntity
{
    public class SystemUserQuery
    {
        public Guid? UserId { get; set; }

        public string UserName { get; set; }

        public string PhoneNo { get; set; }

        public string Email { get; set; }
    }
}
