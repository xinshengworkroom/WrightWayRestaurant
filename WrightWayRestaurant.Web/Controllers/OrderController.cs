using System;
using System.Web.Mvc;
using WrightWayRestaurant.Framework.Web;
using WrightWayRestaurant.Model;
using WrightWayRestaurant.Model.Common;
using WrightWayRestaurant.Model.Enums;
using WrightWayRestaurant.Model.QueryEntity;
using WrightWayRestaurant.Services.Interface;

namespace WrightWayRestaurant.Web.Controllers
{
   
    public class OrderController : BaseController
    {
      

        IOrderService OrderService { get; set; }

        IOrderDetailService OrderDetailService { get; set; }

        IFoodService FoodService { get; set; }

        public OrderController(IOrderService orderService, 
            IOrderDetailService orderDetailService,
            IFoodService foodService)
        {         
            OrderService = orderService;
            OrderDetailService = orderDetailService;
            FoodService = foodService;
        }


        // GET: Order
        [ManageAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        //[WebAuthorize]
        public ActionResult BookSeat()
        {
            var Data = FoodService.Get(new FoodQuery { });
            return PartialView(Data);
        }

        [WebAuthorize]
        public ActionResult OrderFood()
        {
            return View();
        }


        [ManageAuthorize]
        public ActionResult Get()
        {
            var result = new ResultData<object>(ResultStatusEnums.Fail) { Message = "" };
            result.Data = OrderService.Get(new OrderQuery { });
            result.Code = (int)ResultStatusEnums.Success;
            result.Message = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [ManageAuthorize]
        public ActionResult GetFirstOrDefault(int orderId)
        {
            var result = new ResultData<object>(ResultStatusEnums.Fail) { Message = "" };
            result.Data = OrderService.FirstOrDefault(new  OrderQuery { OrderId = orderId });
            result.Code = (int)ResultStatusEnums.Success;
            result.Message = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [WebAuthorize]
        public ActionResult GetFirstOrDefaultForWeb(int orderId)
        {
            var result = new ResultData<object>(ResultStatusEnums.Fail) { Message = "" };
            result.Data = OrderService.FirstOrDefault(new OrderQuery { OrderId = orderId });
            result.Code = (int)ResultStatusEnums.Success;
            result.Message = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [ManageAuthorize]
        public ActionResult Add(Order entity)
        {
            var result = new ResultData<object>(ResultStatusEnums.Fail) { Message = "" };          
            entity.UserId = ManageContext.Current.SessionUser.UserId;
            entity.CreateTime = DateTime.Now;
            int rows = OrderService.Add(entity);
            if (rows > 0)
            {
                result.Code = (int)ResultStatusEnums.Success;
                result.Message = "Add Success";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }


        [ManageAuthorize]
        public ActionResult Update(Order entity)
        {
            var result = new ResultData<object>(ResultStatusEnums.Fail) { Message = "" };           
            entity.UserId = ManageContext.Current.SessionUser.UserId;
            entity.CreateTime = DateTime.Now;
            int rows = OrderService.Update(entity);
            if (rows > 0)
            {
                result.Code = (int)ResultStatusEnums.Success;
                result.Message = "Update Success";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [ManageAuthorize]
        public ActionResult Delete(int orderId)
        {
            var result = new ResultData<object>(ResultStatusEnums.Fail) { Message = "" };
            var order = OrderService.FirstOrDefault(new OrderQuery { OrderId = orderId });
            foreach (OrderDetail detail in order.OrderDetail)
                OrderDetailService.Delete(detail.DetailId);
            int rows = OrderService.Delete(orderId);
            if (rows > 0)
            {
                result.Code = (int)ResultStatusEnums.Success;
                result.Message = "Delete Success";
            }

            return Json(result, JsonRequestBehavior.DenyGet);
        }
        
        [WebAuthorize]
        public ActionResult DeleteForWeb(int orderId)
        {
            var result = new ResultData<object>(ResultStatusEnums.Fail) { Message = "" };
            var order = OrderService.FirstOrDefault(new OrderQuery { OrderId = orderId });
            foreach (OrderDetail detail in order.OrderDetail)
                OrderDetailService.Delete(detail.DetailId);
          
            int rows = OrderService.Delete(orderId);
            if (rows > 0)
            {
                result.Code = (int)ResultStatusEnums.Success;
                result.Message = "Delete Success";
            }

            return Json(result, JsonRequestBehavior.DenyGet);
        }
    }
}