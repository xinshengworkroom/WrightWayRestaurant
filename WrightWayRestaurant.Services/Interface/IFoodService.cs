using System.Collections.Generic;
using WrightWayRestaurant.Model;
using WrightWayRestaurant.Model.QueryEntity;

namespace WrightWayRestaurant.Services.Interface
{
    public interface IFoodService
    {
        Food FirstOrDefault(FoodQuery query);

        List<Food> Get(FoodQuery query = null);

        int Add(Food entity);

        int Update(Food entity);

        int Delete(int entityId);
    }
}
