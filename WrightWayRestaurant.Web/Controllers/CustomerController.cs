using System;
using System.Collections.Generic;
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

        public CustomerController(ICustomerService  customerService, IOrderService orderService, IOrderDetailService orderDetailService)
        {
            CustomerService = customerService;
            OrderService = orderService;
            OrderDetailService = orderDetailService;
        }

        // GET: Customer
        public ActionResult Index()
        {
            return View();
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
                Order order = new Order{
                    CustomerId = WebContext.Current.SessionCustomer.CustomerId,
                    CreateTime = DateTime.Now,
                    ReserveTime = model[0].ReserveTime
                };

                var orderid = OrderService.AddBackId(order);
                model.ForEach(m=> {
                    OrderDetail detail = new OrderDetail
                    {
                        OrderId = orderid,
                        FoodId = m.FoodId,
                        Number = m.Number
                    };

                    OrderDetailService.Add(detail);
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
    }

    
}