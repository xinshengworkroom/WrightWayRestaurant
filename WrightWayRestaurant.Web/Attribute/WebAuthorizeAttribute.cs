using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WrightWayRestaurant.Model.Common;
using WrightWayRestaurant.Model.Enums;

namespace WrightWayRestaurant.Web
{
    public class WebAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("HttpContext");
            }
            return base.AuthorizeCore(httpContext);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                //base.OnAuthorization(filterContext);
                return;
            }


            if (!WebContext.Current.IsAuthenticated)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())//是否为ajax请求
                {
                    filterContext.Result = new JsonResult()
                    {
                        Data = new ResultData(ResultStatusEnums.NoSession),
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                    return;
                }


                filterContext.Result = new RedirectResult(FormsAuthentication.DefaultUrl);
                return;
            }


            //base.OnAuthorization(filterContext);
        }
    }
}