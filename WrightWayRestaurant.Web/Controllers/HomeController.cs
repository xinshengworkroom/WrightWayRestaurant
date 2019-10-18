using System.Web.Mvc;
using WrightWayRestaurant.Framework.Web;
using WrightWayRestaurant.Model.Common;
using WrightWayRestaurant.Model.Enums;
using WrightWayRestaurant.Model.QueryEntity;
using WrightWayRestaurant.Services.Interface;

namespace WrightWayRestaurant.Web.Controllers
{
    public class HomeController : BaseController
    {
        ISystemUserService SystemUserService { get; set; }
        IFoodService FoodService { get; set; }

      
        public HomeController(ISystemUserService systemUserService, IFoodService foodService)
        {
            SystemUserService = systemUserService;
            FoodService = foodService;
        }

        public ActionResult Index()
        {
            //MailUtility.SendEmail(strTitle: "订餐成功通知",
            //              strBody: "尊敬的客户您好，欢迎来到 Wright Way Restaurant",
            //              strToEmails: "407218253@qq.com"
            //          );

            var user = SystemUserService.FirstOrDefault(new SystemUserQuery { UserName = "admin" });

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";


            
            return View();
        }




        public ActionResult Rotationchart()
        {
            var Data = FoodService.Get(new FoodQuery { });         
            return PartialView(Data);
        }

        [WebAuthorize]
        public ActionResult ShoppingCart()
        {
            var data = WebContext.Current.ShoppingCard;
            return View(data);
        }

        [WebAuthorize]
        public ActionResult AddToShoppingCard(int foodId)
        {
            var result = new ResultData<object>(ResultStatusEnums.Fail) { Message = "" };
            var food = FoodService.FirstOrDefault(new FoodQuery { FoodId = foodId });
            food.Stock = 1;
            WebContext.Current.AddToShoppingCard(food);
            result.Data = WebContext.Current.ShoppingCard;
            result.Code = (int)ResultStatusEnums.Success;
            result.Message = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}