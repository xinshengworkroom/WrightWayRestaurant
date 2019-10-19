using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WrightWayRestaurant.Framework.Utility;
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

        IOrderService OrderService { get; set; }

        IOrderDetailService OrderDetailService { get; set; }

        IFoodService FoodService { get; set; }

        public CustomerController(ICustomerService  customerService, 
            IOrderService orderService, 
            IOrderDetailService orderDetailService,
            IFoodService foodService)
        {
            CustomerService = customerService;
            OrderService = orderService;
            OrderDetailService = orderDetailService;
            FoodService = foodService;
        }

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        [WebAuthorize]
        public ActionResult Information()
        {
            return View();
        }



        [WebAuthorize]
        public ActionResult MyInformation()
        {
            return View();
        }

        [WebAuthorize]
        public ActionResult SaveMyInformation(CustomerRegisterViewModel model)
        {
            var result = new ResultData<object>(ResultStatusEnums.Fail)
            {
                Data = ModelState
            };
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    CustomerId = model.CustomerId,
                    CustomerName = model.CustomerName,
                    Email = model.Email,
                    Password = model.Password,
                    PhoneNo = model.PhoneNo,
                    CreateTime = DateTime.Now
                };
                int count = CustomerService.Update(customer);
                if (count > 0)
                {
                    WebContext.Current.Login(customer);
                    result.Code = (int)ResultStatusEnums.Success;
                    result.Message = "Save My Information success";


                    try
                    {
                        MailUtility.SendEmail(strTitle: "Save My Information Success",
                                      strBody: "Dear customer, welcome to Wright Way Restaurant",
                                      strToEmails: model.Email
                                  );

                    }
                    catch (Exception)
                    {
                        result.Message = "Send Email error";
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

        [WebAuthorize]
        public ActionResult MyOrder()
        {
            var myorder = OrderService.Get(new OrderQuery { CustomerId = WebContext.Current.SessionCustomer.CustomerId });
            return View(myorder);
        }


        [WebAuthorize]
        public ActionResult SaveMyOrder(Order entity)
        {
            var result = new ResultData<object>(ResultStatusEnums.Fail) { Message = "" };
            var order = OrderService.FirstOrDefault(new OrderQuery { OrderId = entity.OrderId });
            order.OrderState = entity.OrderState;
            int rows = OrderService.Update(order);
            if (rows > 0)
            {
                result.Code = (int)ResultStatusEnums.Success;
                result.Message = "Update Success";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
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

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Register(CustomerRegisterViewModel model)
        {
            var result = new ResultData<object>(ResultStatusEnums.Fail)
            {
                Data = ModelState
            };
            if (ModelState.IsValid)
            {
                int count = CustomerService.Add(new Customer {
                    CustomerId = Guid.NewGuid(),
                    CustomerName = model.CustomerName,
                    Email = model.Email,
                    Password = model.Password,
                    PhoneNo = model.PhoneNo,
                    CreateTime = DateTime.Now
                });
                if (count > 0)
                {
                    result.Code = (int)ResultStatusEnums.Success;
                    result.Message = "Register success";


                    try
                    {
                        MailUtility.SendEmail(strTitle: "Register Success",
                                      strBody: "Dear customer, welcome to Wright Way Restaurant",
                                      strToEmails: model.Email
                                  );
                       
                    }
                    catch (Exception)
                    {
                        result.Message = "Send Email error";
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


        [WebAuthorize]
        public ActionResult SubmitOrder(List<OrderViewModel>  model)
        {
            var result = new ResultData<object>(ResultStatusEnums.Fail);
            if (model != null && model.Count > 0)
            {
                var foods = FoodService.Get();
                foreach (var item in model)
                {
                    var food = foods.FirstOrDefault(f => f.FoodId == item.FoodId);
                    if (food.Stock < item.Number)
                    {
                        result.Message = food.FoodName + " out of stock";
                        return Json(result, JsonRequestBehavior.DenyGet);
                    }
                }

                Order order = new Order{
                    CustomerId = WebContext.Current.SessionCustomer.CustomerId,
                    CreateTime = DateTime.Now,
                    ReserveTime = model[0].ReserveTime
                };

                var orderid = OrderService.AddBackId(order);
                model.ForEach(m =>
                {
                    OrderDetail detail = new OrderDetail
                    {
                        OrderId = orderid,
                        FoodId = m.FoodId,
                        Number = m.Number
                    };
                    OrderDetailService.Add(detail);
                    var food = foods.FirstOrDefault(f => f.FoodId == m.FoodId);
                    food.Stock -= m.Number;
                    FoodService.Update(food);
                });

                WebContext.Current.ClearShoppingCard();
                try
                {
                    MailUtility.SendEmail(strTitle: "Order Success",
                                  strBody: "Dear customer, welcome to Wright Way Restaurant",
                                  strToEmails: WebContext.Current.SessionCustomer.Email
                              );
                    result.Code = (int)ResultStatusEnums.Success;
                    result.Message = "Order Success";
                }
                catch (Exception)
                {                   
                    result.Message = "Order Success but Send Email error";
                }
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }


        public ActionResult Logout()
        {
            WebContext.Current.ClearShoppingCard();
            WebContext.Current.Logout();
            return RedirectToAction("Index","Home");
        }
    }

    
}