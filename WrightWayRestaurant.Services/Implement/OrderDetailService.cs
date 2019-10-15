using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WrightWayRestaurant.Model;
using WrightWayRestaurant.Model.QueryEntity;
using WrightWayRestaurant.Services.Interface;

namespace WrightWayRestaurant.Services.Implement
{
    public class OrderDetailService: WrightWayRestaurantEntities, IOrderDetailService
    {
        public OrderDetail FirstOrDefault(OrderDetailQuery query)
        {
            throw new System.NotImplementedException();
        }

        public List<OrderDetail> Get(OrderDetailQuery query)
        {
            throw new System.NotImplementedException();
        }

        public int Add(OrderDetail entity)
        {
            this.OrderDetail.Add(entity);
            this.Entry<OrderDetail>(entity).State = EntityState.Added;
            return this.SaveChanges();
        }

        public int Update(OrderDetail entity)
        {
            int result = 0;
            var instance = this.OrderDetail.FirstOrDefault(f => f.DetailId == entity.DetailId);
            if (instance != null)
            {
                instance = entity;
                this.Entry<OrderDetail>(instance).State = EntityState.Modified;
                result = this.SaveChanges();
            }
            return result;
        }

        public int Delete(int entityId)
        {
            int result = 0;
            var instance = this.OrderDetail.FirstOrDefault(f => f.DetailId == entityId);
            if (instance != null)
            {
                this.Entry<OrderDetail>(instance).State = EntityState.Deleted;
                result = this.SaveChanges();
            }
            return result;
        }

    }
}
