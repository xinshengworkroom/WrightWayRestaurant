using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WrightWayRestaurant.Model;
using WrightWayRestaurant.Model.QueryEntity;
using WrightWayRestaurant.Services.Interface;

namespace WrightWayRestaurant.Services.Implement
{
    public class OrderStateService: WrightWayRestaurantEntities, IOrderStateService
    {
        public OrderState FirstOrDefault(OrderStateQuery query)
        {
            throw new System.NotImplementedException();
        }

        public List<OrderState> Get(OrderStateQuery query)
        {
            throw new System.NotImplementedException();
        }

        public int Add(OrderState entity)
        {
            this.OrderState.Add(entity);
            this.Entry<OrderState>(entity).State = EntityState.Added;
            return this.SaveChanges();
        }

        public int Update(OrderState entity)
        {
            int result = 0;
            var instance = this.OrderState.FirstOrDefault(o => o.StateId == entity.StateId);
            if (instance != null)
            {
                instance = entity;
                this.Entry<OrderState>(instance).State = EntityState.Modified;
                result = this.SaveChanges();
            }
            return result;
        }

        public int Delete(int entityId)
        {
            int result = 0;
            var instance = this.OrderState.FirstOrDefault(o => o.StateId == entityId);
            if (instance != null)
            {
                this.Entry<OrderState>(instance).State = EntityState.Deleted;
                result = this.SaveChanges();
            }
            return result;
        }
    }
}
