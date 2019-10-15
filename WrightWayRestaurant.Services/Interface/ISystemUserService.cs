using System;
using System.Collections.Generic;
using WrightWayRestaurant.Model;
using WrightWayRestaurant.Model.QueryEntity;

namespace WrightWayRestaurant.Services.Interface
{
    public interface ISystemUserService
    {
        SystemUser FirstOrDefault(SystemUserQuery query);
        List<SystemUser> Get(SystemUserQuery query);
        int Add(SystemUser entity);

        int Update(SystemUser entity);

        int Delete(Guid entityId);


    }
}
