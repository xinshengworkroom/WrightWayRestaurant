using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrightWayRestaurant.Model;
using WrightWayRestaurant.Model.QueryEntity;

namespace WrightWayRestaurant.Services.Interface
{
    public interface IOrderService
    {
        Order FirstOrDefault(OrderQuery query);

        List<Order> Get(OrderQuery query);

        int Add(Order entity);

        int Update(Order entity);

        int Delete(int entityId);
    }
}
