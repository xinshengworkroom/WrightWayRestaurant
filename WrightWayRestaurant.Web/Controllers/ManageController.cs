using System.Web.Mvc;
using WrightWayRestaurant.Framework.Web;

namespace WrightWayRestaurant.Web.Controllers
{
    [ManageAuthorize]
    public class ManageController : BaseController
    {
        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }

  
    }
}