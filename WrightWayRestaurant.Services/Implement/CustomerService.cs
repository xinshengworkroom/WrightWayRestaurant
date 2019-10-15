using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrightWayRestaurant.Model;
using WrightWayRestaurant.Model.QueryEntity;
using WrightWayRestaurant.Services.Interface;

namespace WrightWayRestaurant.Services.Implement
{
    public class CustomerService : WrightWayRestaurantEntities, ICustomerService
    {
        public Customer FirstOrDefault(CustomerQuery query)
        {
            Customer result = null;
            List<Customer> list = Get(query);
            if (list != null)
            {
                result = list.FirstOrDefault();
            }
            return result;
        }

        public List<Customer> Get(CustomerQuery query)
        {
            List<Customer> result = null;
            IEnumerable<Customer> enumerable = this.Customer.AsEnumerable();
            if (query.CustomerId != null)
            {
                enumerable = enumerable.Where(u => u.CustomerId == query.CustomerId.Value);
            }

            if (!string.IsNullOrEmpty(query.CustomerName))
            {
                enumerable = enumerable.Where(u => u.CustomerName == query.CustomerName);
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

        public int Add(Customer entity)
        {
            this.Customer.Add(entity);
            this.Entry<Customer>(entity).State = EntityState.Added;
            return this.SaveChanges();
        }

        public int Update(Customer entity)
        {
            int result = 0;
            var instance = this.Customer.FirstOrDefault(e => e.CustomerId == entity.CustomerId);
            if (instance != null)
            {
                instance = entity;
                this.Entry<Customer>(instance).State = EntityState.Modified;
                result = this.SaveChanges();
            }
            return result;
        }

        public int Delete(Guid entityId)
        {
            int result = 0;
            var instance = this.Customer.FirstOrDefault(e => e.CustomerId == entityId);
            if (instance != null)
            {
                this.Entry<Customer>(instance).State = EntityState.Deleted;
                result = this.SaveChanges();
            }
            return result;
        }

        public Customer FirstOrDefault(string accountName)
        {
            return this.Customer.FirstOrDefault(c => c.CustomerName.Equals(accountName) || c.PhoneNo.Equals(accountName) || c.Email.Equals(accountName));
        }
    }
}
