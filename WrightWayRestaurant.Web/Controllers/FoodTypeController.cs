using System.Web.Mvc;
using WrightWayRestaurant.Framework.Web;
using WrightWayRestaurant.Model;
using WrightWayRestaurant.Model.Common;
using WrightWayRestaurant.Model.Enums;
using WrightWayRestaurant.Model.QueryEntity;
using WrightWayRestaurant.Services.Interface;

namespace WrightWayRestaurant.Web.Controllers
{
    public class FoodTypeController : BaseController
    {
        IFoodTypeService FoodTypeService { get; set; }
        public FoodTypeController(IFoodTypeService service)
        {
            FoodTypeService = service;
        }

        // GET: FoodType
        public ActionResult Index()
        {
            return View();
        }


        [ManageAuthorize]
        public ActionResult Get()
        {
            var result = new ResultData<object>(ResultStatusEnums.Fail) { Message = "" };
            result.Data = FoodTypeService.Get(new FoodTypeQuery { });
            result.Code = (int)ResultStatusEnums.Success;
            result.Message = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [ManageAuthorize]
        public ActionResult GetFirstOrDefault(int foodId)
        {
            var result = new ResultData<object>(ResultStatusEnums.Fail) { Message = "" };
            result.Data = FoodTypeService.FirstOrDefault(new FoodTypeQuery { });
            result.Code = (int)ResultStatusEnums.Success;
            result.Message = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [ManageAuthorize]
        public ActionResult Add(FoodType entity)
        {
            var result = new ResultData<object>(ResultStatusEnums.Fail) { Message = "" };                  
            entity.UserId = ManageContext.Current.SessionUser.UserId;          
            int rows = FoodTypeService.Add(entity);
            if (rows > 0)
            {
                result.Code = (int)ResultStatusEnums.Success;
                result.Message = "Add Success";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }


        [ManageAuthorize]
        public ActionResult Update(FoodType entity)
        {
            var result = new ResultData<object>(ResultStatusEnums.Fail) { Message = "" };          
            int rows = FoodTypeService.Update(entity);
            if (rows > 0)
            {
                result.Code = (int)ResultStatusEnums.Success;
                result.Message = "Update Success";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [ManageAuthorize]
        public ActionResult Delete(int foodTypeId)
        {
            var result = new ResultData<object>(ResultStatusEnums.Fail) { Message = "" };
            FoodType foodType = FoodTypeService.FirstOrDefault(new  FoodTypeQuery { TypeId = foodTypeId });
            if (foodType != null)
            {
                int rows = FoodTypeService.Delete(foodTypeId);
                if (rows > 0)
                {
                    result.Code = (int)ResultStatusEnums.Success;
                    result.Message = "Delete Success";
                }
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }
    }
}