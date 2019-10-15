using System.Web.Mvc;
using WrightWayRestaurant.Framework.Web;
using WrightWayRestaurant.Model;
using WrightWayRestaurant.Model.Common;
using WrightWayRestaurant.Model.Enums;
using WrightWayRestaurant.Model.QueryEntity;
using WrightWayRestaurant.Services.Interface;
using WrightWayRestaurant.Web.Models;

namespace WrightWayRestaurant.Web.Controllers
{
    public class SystemUserController : BaseController
    {
        ISystemUserService SystemUserService { get; set; }
        public SystemUserController(ISystemUserService service)
        {
            SystemUserService = service;
        }

        // GET: SystemUser
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(SystemUserLoginViewModel model)
        {
            var result = new ResultData<object>(ResultStatusEnums.Fail)
            {
                Data = ModelState
            };
            if (ModelState.IsValid)
            {
                SystemUser user = SystemUserService.FirstOrDefault(new SystemUserQuery {UserName = model.UserName });
                if (user != null)
                {
                    if (model.Password == user.Password)
                    {
                        ManageContext.Current.Login(user);
                        result.Code = (int)ResultStatusEnums.Success;
                        result.Message = "Login success";
                        //RedirectToAction("Index", "Manage");
                    }
                    else
                    {
                        result.Code = (int)ResultStatusEnums.Fail;
                        ModelState.AddModelError("Password", "Password error");
                    }
                }
                else
                {
                    result.Code = (int)ResultStatusEnums.Fail;
                    ModelState.AddModelError("UserName", "User not find");
                }
            }

            return Json(result, JsonRequestBehavior.DenyGet);
        }

      
        public ActionResult Logout()
        {
            ManageContext.Current.Logout();            
            return RedirectToAction("Login");
        }
    }
}