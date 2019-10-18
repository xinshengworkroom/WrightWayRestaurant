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
            Order result = null;
            List<Order> list = Get(query);
            if (list != null)
            {
                result = list.FirstOrDefault();
            }
            return result;
        }

        public List<Order> Get(OrderQuery query)
        {
            List<Order> result = null;
            IEnumerable<Order> enumerable = this.Order.AsEnumerable();
            if (query.OrderId != null)
            {
                enumerable = enumerable.Where(o => o.OrderId == query.OrderId.Value);
            }

            if (enumerable != null)
            {
                result = enumerable.ToList();
            }
            return result;
        }

        public int AddBackId(Order entity)
        {
            this.Order.Add(entity);
            this.Entry<Order>(entity).State = EntityState.Added;
            this.SaveChanges();
            return entity.OrderId;
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
            this.Set<Order>().Attach(entity);
            this.Entry(entity).State = EntityState.Modified;
            this.Entry(entity).Property("UserId").IsModified = false;
            this.Entry(entity).Property("CreateTime").IsModified = false;           
            result = this.SaveChanges();
            return result;
        }

        public int Delete(int entityId)
        {
            int result = 0;
            var entity = this.Set<Order>().Find(entityId);
            if (entity != null)
            {
                this.Set<Order>().Remove(entity);
                result = this.SaveChanges();
            }
            return result;
        }

    }
}
