using System;
using System.IO;
using System.Web.Mvc;
using WrightWayRestaurant.Framework.Web;
using WrightWayRestaurant.Model;
using WrightWayRestaurant.Model.Common;
using WrightWayRestaurant.Model.Enums;
using WrightWayRestaurant.Model.QueryEntity;
using WrightWayRestaurant.Services.Interface;

namespace WrightWayRestaurant.Web.Controllers
{
    public class FoodController : BaseController
    {
        IFoodService FoodService { get; set; }

        public FoodController(IFoodService service)
        {
            FoodService = service;
        }

        // GET: Food
        [ManageAuthorize]
        public ActionResult Index()
        {
            return View();
        }


        [ManageAuthorize]
        public ActionResult Get()
        {
            var result = new ResultData<object>(ResultStatusEnums.Fail) { Message = "" };
            result.Data = FoodService.Get(new FoodQuery { });
            result.Code = (int)ResultStatusEnums.Success;
            result.Message = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [ManageAuthorize]
        public ActionResult GetFirstOrDefault(int foodId)
        {
            var result = new ResultData<object>(ResultStatusEnums.Fail) { Message = "" };
            result.Data = FoodService.FirstOrDefault(new FoodQuery { FoodId = foodId });
            result.Code = (int)ResultStatusEnums.Success;
            result.Message = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [ManageAuthorize]
        public ActionResult Add(Food entity)
        {
            var result = new ResultData<object>(ResultStatusEnums.Fail) { Message = "" };
            var file = System.Web.HttpContext.Current.Request.Files["Foodimg"];
            if (file != null)
            {
                string savePath = Server.MapPath("~/Upload/Food\\");
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                string saveFile = savePath + file.FileName;
                file.SaveAs(saveFile);
                entity.Foodimg = "Upload/Food/" + file.FileName;
            }
            entity.UserId = ManageContext.Current.SessionUser.UserId;
            entity.CreateTime = DateTime.Now;
            int rows = FoodService.Add(entity);
            if (rows > 0)
            {
                result.Code = (int)ResultStatusEnums.Success;
                result.Message = "Add Success";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }


        [ManageAuthorize]
        public ActionResult Update(Food entity)
        {
            var result = new ResultData<object>(ResultStatusEnums.Fail) { Message = "" };
            var file = System.Web.HttpContext.Current.Request.Files["Foodimg"];
            if (file != null)
            {
                string savePath = Server.MapPath("~/Upload/Food\\");
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                string saveFile = savePath + file.FileName;
                file.SaveAs(saveFile);
                entity.Foodimg = "Upload/Food/" + file.FileName;
            }
      
            int rows = FoodService.Update(entity);
            if (rows > 0)
            {
                result.Code = (int)ResultStatusEnums.Success;
                result.Message = "Update Success";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [ManageAuthorize]
        public ActionResult Delete(int foodId)
        {
            var result = new ResultData<object>(ResultStatusEnums.Fail) { Message = "" };
            Food food = FoodService.FirstOrDefault(new FoodQuery { FoodId = foodId });
            if (food != null)
            {
                string file = Server.MapPath("~/" + food.Foodimg);
                if (System.IO.File.Exists(file))
                {
                    System.IO.File.Delete(file);
                }                
                int rows = FoodService.Delete(foodId);
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