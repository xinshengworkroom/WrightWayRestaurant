using System.Collections.Generic;
using System.Web.Mvc;
using WrightWayRestaurant.Framework.Utility;
using WrightWayRestaurant.Model;
using WrightWayRestaurant.Services.Interface;

namespace WrightWayRestaurant.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {


            //MailUtility.SendEmail(strTitle: "订餐成功通知",
            //              strBody: "尊敬的客户您好，欢迎来到 Wright Way Restaurant",
            //              strToEmails: "407218253@qq.com"
            //          );

            ISystemUserService service = new WrightWayRestaurant.Services.Implement.SystemUserService();
            var user = service.GetSystemUser("admin");

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
    }
}