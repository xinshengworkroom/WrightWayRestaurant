using System.Web.Mvc;
using WrightWayRestaurant.Framework.Web;
using WrightWayRestaurant.Model.QueryEntity;
using WrightWayRestaurant.Services.Interface;

namespace WrightWayRestaurant.Web.Controllers
{
    public class HomeController : BaseController
    {
        ISystemUserService SystemUserService { get; set; }

        public HomeController(ISystemUserService service)
        {
            SystemUserService = service;
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
    }
}