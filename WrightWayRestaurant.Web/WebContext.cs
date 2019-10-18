using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WrightWayRestaurant.Framework.Utility;
using WrightWayRestaurant.Model;

namespace WrightWayRestaurant.Web
{
    public class WebContext
    {
        public WebContext(HttpContext context)
        {

        }

        [ThreadStatic]
        private static WebContext _Context;
        public static WebContext Current
        {
            get
            {
                if (_Context == null)
                {
                    _Context = new WebContext(HttpContext.Current);
                }
                return _Context;
            }
        }

        public Customer SessionCustomer
        {
            get
            {
                return HttpContext.Current.Session["Customer"] as Customer;
            }
        }

        public void Login(Customer user)
        {
            HttpContext.Current.Session["Customer"] = user;
        }



        public void Logout()
        {
            HttpContext.Current.Session.Remove("Customer");
        }

        public bool IsAuthenticated
        {
            get { return SessionCustomer != null; }
        }


        public List<Food> ShoppingCard
        {
            get
            {
                return HttpContext.Current.Session["ShoppingCard"] as List<Food>;
            }
        }

        public void AddToShoppingCard(Food food)
        {
            var curCard = ShoppingCard;
            if (curCard != null)
            {
                var item = curCard.FirstOrDefault(f=>f.FoodId == food.FoodId);
                if (item != null)
                {
                    item.Stock += food.Stock;
                }
                else
                {
                    curCard.Add(food);
                }
            }
            else
            {
                curCard = new List<Food> { food};
            }
            HttpContext.Current.Session["ShoppingCard"] = curCard;
        }

        public void ClearShoppingCard()
        {
            HttpContext.Current.Session.Remove("ShoppingCard");
        }
    }
}