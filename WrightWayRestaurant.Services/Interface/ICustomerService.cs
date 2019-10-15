using System;
using System.Collections.Generic;
using WrightWayRestaurant.Model;
using WrightWayRestaurant.Model.QueryEntity;

namespace WrightWayRestaurant.Services.Interface
{
    public interface ICustomerService
    {
        Customer FirstOrDefault(CustomerQuery query);
        List<Customer> Get(CustomerQuery query);

        int Add(Customer entity);

        int Update(Customer entity);

        int Delete(Guid entityId);       
        Customer FirstOrDefault(string accountName);
    }
}
