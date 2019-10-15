using System.Collections.Generic;
using WrightWayRestaurant.Model;
using WrightWayRestaurant.Model.QueryEntity;

namespace WrightWayRestaurant.Services.Interface
{
    public interface IEmailConfigService
    {
        EmailConfig FirstOrDefault(EmailConfigQuery query);
        List<EmailConfig> Get(EmailConfigQuery query);

        int Add(EmailConfig entity);

        int Update(EmailConfig entity);

        int Delete(int entityId);
    }
}
