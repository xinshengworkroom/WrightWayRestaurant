using WrightWayRestaurant.Model;
using WrightWayRestaurant.Services.Interface;
using System.Linq;
using System.Collections.Generic;
using WrightWayRestaurant.Model.QueryEntity;
using System;
using System.Data.Entity;

namespace WrightWayRestaurant.Services.Implement
{
    public class SystemUserService: WrightWayRestaurantEntities,ISystemUserService
    {
        public SystemUser FirstOrDefault(SystemUserQuery query)
        {
            SystemUser result = null;
            List<SystemUser> list = Get(query);
            if (list != null)
            {
                result = list.FirstOrDefault();
            }
            return result;
        }

        public List<SystemUser> Get(SystemUserQuery query)
        {
            List<SystemUser> result = null;
            IEnumerable<SystemUser> enumerable = this.SystemUser.AsEnumerable();
            if (query.UserId != null)
            {
                enumerable = enumerable.Where(u => u.UserId == query.UserId.Value);
            }

            if (!string.IsNullOrEmpty(query.UserName))
            {
                enumerable = enumerable.Where(u => u.UserName == query.UserName);
            }

            if (!string.IsNullOrEmpty(query.PhoneNo))
            {
                enumerable = enumerable.Where(u => u.PhoneNo == query.PhoneNo);
            }

            if (!string.IsNullOrEmpty(query.Email))
            {
                enumerable = enumerable.Where(u => u.Email == query.Email);
            }

            if (enumerable != null)
            {
                result = enumerable.ToList();
            }

            return result;
        }
        public int Add(SystemUser entity)
        {
            this.SystemUser.Add(entity);
            this.Entry<SystemUser>(entity).State = EntityState.Added;
            return this.SaveChanges();
        }

        public int Update(SystemUser entity)
        {
            int result = 0;
            var instance = this.SystemUser.FirstOrDefault(e => e.UserId == entity.UserId);
            if (instance != null)
            {
                instance = entity;
                this.Entry<SystemUser>(instance).State = EntityState.Modified;
                result = this.SaveChanges();
            }
            return result;
        }

        public int Delete(Guid entityId)
        {
            int result = 0;
            var instance = this.SystemUser.FirstOrDefault(e => e.UserId == entityId);
            if (instance != null)
            {
                this.Entry<SystemUser>(instance).State = EntityState.Deleted;
                result = this.SaveChanges();
            }
            return result;
        }

    }
}
