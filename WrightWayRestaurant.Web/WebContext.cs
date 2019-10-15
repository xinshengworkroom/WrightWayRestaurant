using System;
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
    }
}