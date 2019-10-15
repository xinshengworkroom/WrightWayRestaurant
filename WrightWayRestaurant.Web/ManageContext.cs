using System;
using System.Web;
using WrightWayRestaurant.Model;

namespace WrightWayRestaurant.Web
{
    public class ManageContext
    {
        public ManageContext(HttpContext context)
        {

        }

        [ThreadStatic]
        private static ManageContext _Context;
        public static ManageContext Current
        {
            get
            {
                if (_Context == null)
                {
                    _Context = new ManageContext(HttpContext.Current);
                }
                return _Context;
            }
        }

        public SystemUser SessionUser
        {
            get
            {
                return HttpContext.Current.Session["SessionUser"] as SystemUser;
            }
        }

        public void Login(SystemUser user)
        {
            HttpContext.Current.Session["SessionUser"] = user;
        }

        public void Logout()
        {
            HttpContext.Current.Session.Remove("SessionUser");
        }

        public bool IsAuthenticated
        {
            get { return SessionUser != null; }
        }
    }
}