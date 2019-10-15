using System.Collections.Generic;
using WrightWayRestaurant.Model;
using WrightWayRestaurant.Model.QueryEntity;

namespace WrightWayRestaurant.Services.Interface
{
    public interface IOrderDetailService
    {
        OrderDetail FirstOrDefault(OrderDetailQuery query);

        List<OrderDetail> Get(OrderDetailQuery query);

        int Add(OrderDetail entity);

        int Update(OrderDetail entity);

        int Delete(int entityId);
    }
}
