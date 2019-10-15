using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WrightWayRestaurant.Model;
using WrightWayRestaurant.Model.QueryEntity;
using WrightWayRestaurant.Services.Interface;

namespace WrightWayRestaurant.Services.Implement
{
    public class EmailConfigService : WrightWayRestaurantEntities, IEmailConfigService
    {
        public EmailConfig FirstOrDefault(EmailConfigQuery query)
        {
            throw new System.NotImplementedException();
        }

        public List<EmailConfig> Get(EmailConfigQuery query)
        {
            throw new System.NotImplementedException();
        }

        public int Add(EmailConfig entity)
        { 
            this.EmailConfig.Add(entity);
            this.Entry<EmailConfig>(entity).State = EntityState.Added;
            return this.SaveChanges();
        }

        public int Update(EmailConfig entity)
        {
            int result = 0;
            var instance = this.EmailConfig.FirstOrDefault(e => e.ConfigId == entity.ConfigId);
            if (instance != null)
            {
                instance = entity;
                this.Entry<EmailConfig>(instance).State = EntityState.Modified;
                result = this.SaveChanges();
            }
            return result;
        }

        public int Delete(int entityId)
        {
            int result = 0;
            var instance = this.EmailConfig.FirstOrDefault(e => e.ConfigId == entityId);
            if (instance != null)
            {                
                this.Entry<EmailConfig>(instance).State = EntityState.Deleted;
                result = this.SaveChanges();
            }
            return result;
        }


    }
}
