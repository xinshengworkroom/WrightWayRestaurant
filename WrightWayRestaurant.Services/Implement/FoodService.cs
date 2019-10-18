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
            Food result = null;
            List<Food> list = Get(query);
            if (list != null)
            {
                result = list.FirstOrDefault();
            }
            return result;
        }

        public List<Food> Get(FoodQuery query)
        {
            List<Food> result = null;
            IEnumerable<Food> enumerable = this.Food.AsEnumerable();
            if (query.FoodId != null)
            {
                enumerable = enumerable.Where(o => o.FoodId == query.FoodId.Value);
            }

            if (!string.IsNullOrEmpty(query.FoodName))
            {
                enumerable = enumerable.Where(o => o.FoodName == query.FoodName);
            }

            if (query.TypeId != null)
            {
                enumerable = enumerable.Where(o => o.TypeId == query.TypeId);
            }

            if (enumerable != null)
            {
                result = enumerable.ToList();
            }
            return result;
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
            this.Set<Food>().Attach(entity);
            this.Entry(entity).State = EntityState.Modified;
            this.Entry(entity).Property("UserId").IsModified = false;          
            this.Entry(entity).Property("CreateTime").IsModified = false;
            if(string.IsNullOrEmpty(entity.Foodimg))
                this.Entry(entity).Property("Foodimg").IsModified = false;
            result = this.SaveChanges();
            return result;
        }

        public int Delete(int entityId)
        {
            int result = 0;
            var entity = this.Set<Food>().Find(entityId);
            if (entity != null)
            {
                this.Set<Food>().Remove(entity);
                result = this.SaveChanges();
            }
            return result;
        }
    }
}
