using System.Web.Mvc;
using WrightWayRestaurant.Framework.Web;

namespace WrightWayRestaurant.Web.Controllers
{
   
    public class OrderController : BaseController
    {
        // GET: Order
        [ManageAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [WebAuthorize]
        public ActionResult BookSeat()
        {
            return View();
        }

        [WebAuthorize]
        public ActionResult OrderFood()
        {
            return View();
        }
    }
}