using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WrightWayRestaurant.Model;
using WrightWayRestaurant.Model.QueryEntity;
using WrightWayRestaurant.Services.Interface;

namespace WrightWayRestaurant.Services.Implement
{
    public class FoodService: WrightWayRestaurantEntities, IFoodService
    {
        public Food FirstOrDefault(FoodQuery query)
        {
            throw new System.NotImplementedException();
        }

        public List<Food> Get(FoodQuery query)
        {
            throw new System.NotImplementedException();
        }

        public int Add(Food entity)
        {
            this.Food.Add(entity);
            this.Entry<Food>(entity).State = EntityState.Added;
            return this.SaveChanges();
        }

        public int Update(Food entity)
        {
            int result = 0;
            var instance = this.Food.FirstOrDefault(f=>f.FoodId == entity.FoodId);
            if (instance != null)
            {
                instance = entity;
                this.Entry<Food>(instance).State = EntityState.Modified;
                result = this.SaveChanges();
            }
            return result;
        }

        public int Delete(int entityId)
        {
            int result = 0;
            var instance = this.Food.FirstOrDefault(f => f.FoodId == entityId);
            if (instance != null)
            {
                this.Entry<Food>(instance).State = EntityState.Deleted;
                result = this.SaveChanges();
            }
            return result;
        }
    }
}
