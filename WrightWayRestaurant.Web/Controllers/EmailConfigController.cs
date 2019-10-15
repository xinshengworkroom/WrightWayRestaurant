using System.Web.Mvc;
using WrightWayRestaurant.Framework.Web;

namespace WrightWayRestaurant.Web.Controllers
{
    [ManageAuthorize]
    public class EmailConfigController : BaseController
    {
        // GET: EmailConfig
        public ActionResult Index()
        {
            return View();
        }
    }
}