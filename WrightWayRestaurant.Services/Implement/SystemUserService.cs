using WrightWayRestaurant.Model;
using WrightWayRestaurant.Services.Interface;
using System.Linq;

namespace WrightWayRestaurant.Services.Implement
{
    public class SystemUserService: WrightWayRestaurantEntities,ISystemUserService
    {
        public SystemUser GetSystemUser(string userName)
        {
            return this.SystemUser.FirstOrDefault(u => u.UserName == userName);
        }
    }
}
