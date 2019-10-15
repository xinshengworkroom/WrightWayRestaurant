using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WrightWayRestaurant.Model;
using WrightWayRestaurant.Model.QueryEntity;
using WrightWayRestaurant.Services.Interface;

namespace WrightWayRestaurant.Services.Implement
{
    public class OrderService: WrightWayRestaurantEntities, IOrderService
    {
        public Order FirstOrDefault(OrderQuery query)
        {
            throw new System.NotImplementedException();
        }

        public List<Order> Get(OrderQuery query)
        {
            throw new System.NotImplementedException();
        }

        public int Add(Order entity)
        {
            this.Order.Add(entity);
            this.Entry<Order>(entity).State = EntityState.Added;
            return this.SaveChanges();
        }

        public int Update(Order entity)
        {
            int result = 0;
            var instance = this.Order.FirstOrDefault(o => o.OrderId == entity.OrderId);
            if (instance != null)
            {
                instance = entity;
                this.Entry<Order>(instance).State = EntityState.Modified;
                result = this.SaveChanges();
            }
            return result;
        }

        public int Delete(int entityId)
        {
            int result = 0;
            var instance = this.Order.FirstOrDefault(o => o.OrderId == entityId);
            if (instance != null)
            {
                this.Entry<Order>(instance).State = EntityState.Deleted;
                result = this.SaveChanges();
            }
            return result;
        }
    }
}
