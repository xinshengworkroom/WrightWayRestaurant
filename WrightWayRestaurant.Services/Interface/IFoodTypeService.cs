using System.Collections.Generic;
using WrightWayRestaurant.Model;
using WrightWayRestaurant.Model.QueryEntity;

namespace WrightWayRestaurant.Services.Interface
{
    public interface IFoodTypeService
    {
        FoodType FirstOrDefault(FoodTypeQuery query);

        List<FoodType> Get(FoodTypeQuery query);

        int Add(FoodType entity);

        int Update(FoodType entity);

        int Delete(int entityId);
    }
}
