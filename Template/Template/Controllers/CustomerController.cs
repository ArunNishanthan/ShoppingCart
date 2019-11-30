using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Template.Models;
using Template.Database.Services;

namespace Template.Controllers
{
    public class CustomerController : Controller
    {
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.IsEdit = false;
            ViewBag.Title = "Register";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(CustomerLogin customerLogin)
        {
            ViewBag.Title = "Register";
            var customer = new Customer
            {
                Id = customerLogin.Id,
                FullName = customerLogin.FullName,
                Userid = customerLogin.Userid,
                Password = customerLogin.Password,
                ConfirmPassword = customerLogin.ConfirmPassword,
                Email = customerLogin.Email
            };
            ViewBag.IsEdit = false;
            customer.Password = customer.Password.GetHashCode().ToString();
            customer.ConfirmPassword = customer.ConfirmPassword.GetHashCode().ToString();

            var cus = new CustomerService();
            if (customer.Id==0)
            {
                bool isExist = new CustomerService().CheckCustomerName(customer.Userid);
               
                if (!isExist)
                {
                    cus.AddCustomer(customer);
                }
                else
                {
                    ModelState.AddModelError("", "User ID already Exists");
                    return View(customerLogin);
                }
            }
            else
            {
                cus.UpdateCustomer(customer);
                return RedirectToAction("Index", "Product");
            }
            return RedirectToAction("Login", "Customer");
        }


        [HttpGet]
        public ActionResult Login(string clickedFrom)
        {
            var LoginModel = new LoginModel();
            if (clickedFrom!=null || clickedFrom != "")
            {
                LoginModel.clickedFrom = clickedFrom;
            }

            return View(LoginModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel user)
        {
            if (ModelState.IsValid)
            {
                user.Password = user.Password.GetHashCode().ToString();
                var customer = new CustomerService().GetCustomer(user.UserName, user.Password);


                if (customer!=null)
                {
                    Session["customer"] = customer;
                    
                    if (user.clickedFrom== "viewCart")
                    {
                        return RedirectToAction("ViewCart", "Product");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Product");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid User Name or Password");
                    return View(user);
                }
            }
            else
            {
                return View(user);
            }
        }
        

        public ActionResult Logout()
        {

            if (HttpContext.Request.Cookies["ShoppingCart"] != null)
            {
                var ShoppingCart = HttpContext.Request.Cookies.Get("ShoppingCart");
                ShoppingCart.Expires = DateTime.Now.AddDays(-1);

                Response.SetCookie(ShoppingCart);
            }

           
            Session.Abandon();
            Session.Contents.RemoveAll();
            Session.Clear();

            return RedirectToAction("Index","Product");
        }

        public ActionResult Edit(int Id)
        {
            ViewBag.Title = "Edit";
            var customer = new CustomerService().GetCustomerById(Id);

            var customerLogin = new CustomerLogin
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Userid = customer.Userid,
                Password = customer.Password,
                ConfirmPassword = customer.ConfirmPassword,
                Email = customer.Email
            };
            ViewBag.IsEdit = true;

            return View("Register", customerLogin);
        }

    }
}