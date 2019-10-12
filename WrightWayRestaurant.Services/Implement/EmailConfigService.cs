using System.Data.Entity;
using System.Linq;
using WrightWayRestaurant.Model;
using WrightWayRestaurant.Services.Interface;

namespace WrightWayRestaurant.Services.Implement
{
    public class EmailConfigService : WrightWayRestaurantEntities, IEmailConfigService
    {
      
        public int Add(EmailConfig entity)
        { 
            this.EmailConfig.Add(entity);
            this.Entry<EmailConfig>(entity).State = EntityState.Added;
            return this.SaveChanges();
        }

        public int Update(EmailConfig entity)
        {
            int result = 0;
            var config = this.EmailConfig.FirstOrDefault(e => e.ConfigId == entity.ConfigId);
            if (config != null)
            {
                config = entity;
                this.Entry<EmailConfig>(config).State = EntityState.Modified;
                result = this.SaveChanges();
            }
            return result;
        }

        public int Delete(int configId)
        {
            int result = 0;
            var config = this.EmailConfig.FirstOrDefault(e => e.ConfigId == configId);
            if (config != null)
            {                
                this.Entry<EmailConfig>(config).State = EntityState.Deleted;
                result = this.SaveChanges();
            }
            return result;
        }
    }
}
