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
    public class CustomerController : BaseController
    {
        ICustomerService CustomerService { get; set; }

        public CustomerController(ICustomerService service)
        {
            CustomerService = service;
        }

        // GET: Customer
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
        public ActionResult Login(CustomerLoginViewModel model)
        {
            var result = new ResultData<object>(ResultStatusEnums.Fail)
            {
                Data = ModelState
            };
            if (ModelState.IsValid)
            {
                Customer customer = CustomerService.FirstOrDefault(model.AccountName);
                if (customer != null)
                {
                    if (model.Password == customer.Password)
                    {
                        WebContext.Current.Login(customer);
                        result.Code = (int)ResultStatusEnums.Success;
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
                    ModelState.AddModelError("AccountName", "Account not find");
                }
            }

            return Json(result, JsonRequestBehavior.DenyGet);
        }
    }
}