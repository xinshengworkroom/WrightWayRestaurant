using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WrightWayRestaurant.Model;
using WrightWayRestaurant.Model.QueryEntity;
using WrightWayRestaurant.Services.Interface;

namespace WrightWayRestaurant.Services.Implement
{
    public class FoodTypeService: WrightWayRestaurantEntities, IFoodTypeService
    {
        public FoodType FirstOrDefault(FoodTypeQuery query)
        {
            FoodType result = null;
            List<FoodType> list = Get(query);
            if (list != null)
            {
                result = list.FirstOrDefault();
            }
            return result;
        }

        public List<FoodType> Get(FoodTypeQuery query)
        {
            List<FoodType> result = null;
            IEnumerable<FoodType> enumerable = this.FoodType.AsEnumerable();
            if (query.TypeId != null)
            {
                enumerable = enumerable.Where(o => o.TypeId == query.TypeId.Value);
            }

            if (!string.IsNullOrEmpty(query.TypeName))
            {
                enumerable = enumerable.Where(o => o.TypeName == query.TypeName);
            }
            

            if (enumerable != null)
            {
                result = enumerable.ToList();
            }
            return result;
        }

        public int Add(FoodType entity)
        {
            this.FoodType.Add(entity);
            this.Entry<FoodType>(entity).State = EntityState.Added;
            return this.SaveChanges();
        }

        public int Update(FoodType entity)
        {
            int result = 0;
            var instance = this.FoodType.FirstOrDefault(f => f.TypeId == entity.TypeId);
            if (instance != null)
            {
                instance = entity;
                this.Entry<FoodType>(instance).State = EntityState.Modified;
                result = this.SaveChanges();
            }
            return result;
        }

        public int Delete(int entityId)
        {
            int result = 0;
            var instance = this.FoodType.FirstOrDefault(f => f.TypeId == entityId);
            if (instance != null)
            {
                this.Entry<FoodType>(instance).State = EntityState.Deleted;
                result = this.SaveChanges();
            }
            return result;
        }
    }
}
