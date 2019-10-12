using WrightWayRestaurant.Model;

namespace WrightWayRestaurant.Services.Interface
{
    interface IEmailConfigService
    {
        int Add(EmailConfig entity);

        int Update(EmailConfig entity);

        int Delete(int configId);
    }
}
