using System.Collections.Generic;
using WrightWayRestaurant.Model;
using WrightWayRestaurant.Model.QueryEntity;

namespace WrightWayRestaurant.Services.Interface
{
    public interface IOrderStateService
    {
        OrderState FirstOrDefault(OrderStateQuery query);

        List<OrderState> Get(OrderStateQuery query);

        int Add(OrderState entity);

        int Update(OrderState entity);
     
        int Delete(int entityId);
    }
}
