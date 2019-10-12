using WrightWayRestaurant.Model;

namespace WrightWayRestaurant.Services.Interface
{
    public interface ISystemUserService
    {
        SystemUser GetSystemUser(string userName);
    }
}
