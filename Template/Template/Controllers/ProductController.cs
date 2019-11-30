using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Template.Models;
using Template.Database;
using Template.Database.Services;
using System.Diagnostics;
using Template.ViewModel;
using Template.Filter;
using System.Data.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Template.Controllers
{
   
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Count = CalculateViewCart();
            return View();
        }

        [HttpGet]
        public JsonResult DisplayProduct(string text)
        {
            text = text.ToLower();
            var productlist = new ProductServices().GetProducts();

            var list = new List<Product>();

            if (text!=null)
            {
                foreach (var item in productlist)
                {
                    if (item.Name.ToLower().Contains(text) || item.ShortDescription.ToLower().Contains(text))
                    {
                        list.Add(item);
                    }
                }
            }
            else
            {
                list = productlist;
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewCart()
        {
            var ViewModel = new ViewCartViewModel();
            var viewCarts = new List<ViewCart>();
            
            if (HttpContext.Request.Cookies["ShoppingCart"] != null)
            {
                var ShoppingCart = HttpContext.Request.Cookies.Get("ShoppingCart").Value;

                var productIds = ShoppingCart.ToString().Split();
                var productListDb = new ProductServices().GetProducts();
                if (productIds.Length != 0)
                {
                    foreach (var id in productIds)
                    {
                        if (id != "")
                        {
                            var product = productListDb.FirstOrDefault(p => p.Id == int.Parse(id));

                           
                            if (product != null)
                            {
                                var isAlreadyPresent = false;
                                foreach (var item in viewCarts)
                                {
                                    if (item.Product.Id == int.Parse(id))
                                    {
                                        item.Quantity += 1;
                                        isAlreadyPresent = true;
                                    }
                                }
                                if (!isAlreadyPresent)
                                {
                                    var viewCart = new ViewCart();
                                    viewCart.Customer = new Customer();
                                    viewCart.Product = product;
                                    viewCart.Quantity = 1;

                                    viewCarts.Add(viewCart);
                                }
                            }
                        }
                    }
                }
               
            }
            var customer = (Customer)Session["Customer"];

            var Ls = new List<ViewCart>();
            if (customer!=null)
            {
                var viewCartService = new ViewCartService();
                var viewCartList = viewCartService.GetProductsFromViewCart(customer.Id);

                if (viewCarts.Count > 0 && viewCartList.Count>0)
                {
                    foreach (var item in viewCarts)
                    {
                        var vc = viewCartService.GetProductFromViewCart(item.Product.Id);
                        if (vc==null)
                        {  var viewCart = new ViewCart();

                            viewCart.Customer = new Customer();
                            viewCart.Product = item.Product;
                            viewCart.Quantity = 1;

                            viewCartList.Add(viewCart);
                        }
                    }

                    viewCarts = viewCartList;
                }
                else
                {
                    if (viewCartList.Count > 0)
                    {
                        viewCarts = viewCartList;
                    }
                }
            }

            ViewModel.haveProduct = false;

            if (viewCarts.Count > 0)
            {
                ViewModel.ViewCarts = viewCarts;
                ViewModel.haveProduct = true;
            }

            return View(ViewModel);
        }

        public ActionResult Save()
        {
            var ViewCartService = new ViewCartService();
            var customer = (Customer)Session["Customer"];

            var ViewCartCookie = HttpContext.Request.Cookies.Get("ViewCart").Value.ToString();
            ViewCartCookie = ViewCartCookie.Replace("{", "");
            ViewCartCookie = ViewCartCookie.Replace("}", "");
            ViewCartCookie = ViewCartCookie.Replace("[", "");
            ViewCartCookie = ViewCartCookie.Replace("]", "");
            ViewCartCookie = ViewCartCookie.Replace("\"", "");

            var productIds = ViewCartCookie.Split(',');
            var CartItems = new List<CartItem>();

            for (int i = 0; i < productIds.Length; i++)
            {
                var CartItem = new CartItem();
                if (productIds[i].Contains("id"))
                {
                    CartItem.id = int.Parse(productIds[i].Split(':')[1]);
                    i++;
                }
                if (productIds[i].Contains("quantity"))
                {
                    CartItem.quantity = int.Parse(productIds[i].Split(':')[1]);
                }
                CartItems.Add(CartItem);
            }

            var productListDb = new ProductServices().GetProducts();

            var viewCartService = new ViewCartService();

            if (CartItems.Count != 0)
            {
                foreach (var cartt in CartItems)
                {
                    var product = productListDb.FirstOrDefault(p => p.Id == cartt.id);
                    if (product != null)
                    {
                        var viewCart = new ViewCart();

                        viewCart.Quantity = cartt.quantity;
                        viewCart.ProductId = product.Id;
                        viewCart.CustomerId = customer.Id;

                        var vc = viewCartService.GetProductFromViewCart(product.Id);
                        if (vc==null)
                        {
                            ViewCartService.AddProductinViewCart(viewCart);
                        }
                        else
                        {
                            ViewCartService.UpdateQuantity(product.Id, cartt.quantity);
                        }
                    }
                }

                if (HttpContext.Request.Cookies["ShoppingCart"] != null)
                {
                    var ShoppingCart = HttpContext.Request.Cookies.Get("ShoppingCart");
                    ShoppingCart.Expires = DateTime.Now.AddDays(-1);

                    Response.SetCookie(ShoppingCart);
                }

                if (HttpContext.Request.Cookies["ViewCart"] != null)
                {
                    var ViewCartCooki = HttpContext.Request.Cookies.Get("ViewCart");
                    ViewCartCooki.Expires = DateTime.Now.AddDays(-1);

                    Response.SetCookie(ViewCartCooki);
                }
            }

            return RedirectToAction("Index","Product");
        }
        public JsonResult AddToCart(int id)
        {
            HttpCookie ShoppingCart;
            if (HttpContext.Request.Cookies["ShoppingCart"] != null)
            {
                 ShoppingCart = HttpContext.Request.Cookies.Get("ShoppingCart");
                 ShoppingCart.Value += " " + id.ToString();
            }
            else
            {
                ShoppingCart = new HttpCookie("ShoppingCart");
                ShoppingCart.Value = id.ToString();
            }
            
            Response.SetCookie(ShoppingCart);
            return Json("ok",JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCart(int id)
        {
            HttpCookie ShoppingCart;
            if (HttpContext.Request.Cookies["ShoppingCart"] != null)
            {
                ShoppingCart = HttpContext.Request.Cookies.Get("ShoppingCart");

                ShoppingCart.Value = ShoppingCart.Value.Replace(id.ToString(),"");
            }
            else
            {
                ShoppingCart = new HttpCookie("ShoppingCart");
            }

            var customer = (Customer)Session["Customer"];

            if (customer!=null)
            {
                new ViewCartService().DeleteProductinViewCart(id);
            }


            Response.SetCookie(ShoppingCart);
            return Json("ok", JsonRequestBehavior.AllowGet);
        }


        [UserAuthenticationFilter]
        public ActionResult ViewPurchasedItem()
        {
            var customer = (Customer)Session["Customer"];

            var purchasedlist = new PurchaseService().GetPurchasedProductsByUserId(customer.Id);
            var purchasedActivationList = new List<PurchaseActivationCode>();
            var viewModel = new PurchaseViewModel();
            foreach (var item in purchasedlist)
            {
                var product = purchasedActivationList.SingleOrDefault(m => m.Product.Id == item.ProductId);
                var purchasedActivation = new PurchaseActivationCode();

                if (product != null)
                {
                    product.ActivationCodes.Add(item.ActivationCode);
                }
                else
                {
                    purchasedActivation.Product = item.Product;
                    purchasedActivation.PurchaseDate = item.PurchasedDate;
                    purchasedActivation.ActivationCodes = new List<string>();
                    purchasedActivation.ActivationCodes.Add(item.ActivationCode);
                    purchasedActivationList.Add(purchasedActivation);
                }
            }

            viewModel.PurchaseActivations = purchasedActivationList;

            ViewBag.Count = CalculateViewCart();
            return View(viewModel);
        }

        [UserAuthenticationFilter]
        public ActionResult Checkout()
        {
            
            var ViewCartService = new ViewCartService();
            var customer = (Customer)Session["Customer"];

            var ViewCartCookie = HttpContext.Request.Cookies.Get("ViewCart").Value.ToString();
            ViewCartCookie = ViewCartCookie.Replace("{", "");
            ViewCartCookie = ViewCartCookie.Replace("}", "");
            ViewCartCookie = ViewCartCookie.Replace("[", "");
            ViewCartCookie = ViewCartCookie.Replace("]", "");
            ViewCartCookie = ViewCartCookie.Replace("\"", "");

            var productIds = ViewCartCookie.Split(',');
            var CartItems = new List<CartItem>();

            for (int i = 0; i < productIds.Length; i++)
            {
                var CartItem = new CartItem();
                if (productIds[i].Contains("id"))
                {
                    CartItem.id = int.Parse(productIds[i].Split(':')[1]);
                    i++;
                }
                if (productIds[i].Contains("quantity"))
                {
                    CartItem.quantity = int.Parse(productIds[i].Split(':')[1]);
                }
                CartItems.Add(CartItem);
            }

            var productListDb = new ProductServices().GetProducts();

            if (CartItems.Count != 0)
            {
                foreach (var cartt in CartItems)
                {
                        var product = productListDb.FirstOrDefault(p => p.Id == cartt.id);
                    if (product != null)
                    {
                        var viewCart = new ViewCart();

                        viewCart.Quantity = cartt.quantity;
                        viewCart.ProductId = product.Id;
                        viewCart.CustomerId = customer.Id;

                        var viewCartProduct = ViewCartService.GetProductFromViewCart(product.Id);
                        if (viewCartProduct == null)
                        {
                            ViewCartService.AddProductinViewCart(viewCart);
                        }
                        else
                        {
                            viewCartProduct.Quantity = cartt.quantity;
                        }
                    }
                }
            }

            var viewCartList =ViewCartService.MovePurchasedProductsFromViewCart(customer.Id);
            List<PurchasedProduct> purchaseProductsList = new List<PurchasedProduct>();

            foreach (var one in viewCartList)
            {
                for (int i = 1; i <= one.Quantity; i++)
                {
                    Guid guid = Guid.NewGuid();
                    PurchasedProduct p = new PurchasedProduct();
                    p.CustomerId = customer.Id;
                    p.ProductId = one.Product.Id;
                    p.PurchasedDate = DateTime.Now.Date;
                    p.ActivationCode = guid.ToString();
                    purchaseProductsList.Add(p);
                }
            }
            new PurchaseService().SaveDataToPurchasedProducts(purchaseProductsList);
            ViewCartService.DeleteProduct(purchaseProductsList);

            if (HttpContext.Request.Cookies["ShoppingCart"] != null)
            {
                var ShoppingCart = HttpContext.Request.Cookies.Get("ShoppingCart");
                ShoppingCart.Expires = DateTime.Now.AddDays(-1);

                Response.SetCookie(ShoppingCart);
            }

            if (HttpContext.Request.Cookies["ViewCart"] != null)
            {
                var ViewCartCooki = HttpContext.Request.Cookies.Get("ViewCart");
                ViewCartCooki.Expires = DateTime.Now.AddDays(-1);

                Response.SetCookie(ViewCartCooki);
            }

            return RedirectToAction("ViewPurchasedItem", "Product");
        }

        public int CalculateViewCart()
        {
            var ViewModel = new ViewCartViewModel();
            var viewCarts = new List<ViewCart>();

            if (HttpContext.Request.Cookies["ShoppingCart"] != null)
            {
                var ShoppingCart = HttpContext.Request.Cookies.Get("ShoppingCart").Value;

                var productIds = ShoppingCart.ToString().Split();
                var productListDb = new ProductServices().GetProducts();
                if (productIds.Length != 0)
                {
                    foreach (var id in productIds)
                    {
                        if (id != "")
                        {
                            var product = productListDb.FirstOrDefault(p => p.Id == int.Parse(id));


                            if (product != null)
                            {
                                var isAlreadyPresent = false;
                                foreach (var item in viewCarts)
                                {
                                    if (item.Product.Id == int.Parse(id))
                                    {
                                        item.Quantity += 1;
                                        isAlreadyPresent = true;
                                    }
                                }
                                if (!isAlreadyPresent)
                                {
                                    var viewCart = new ViewCart();
                                    viewCart.Customer = new Customer();
                                    viewCart.Product = product;
                                    viewCart.Quantity = 1;

                                    viewCarts.Add(viewCart);
                                }
                            }
                        }
                    }
                }

            }
            var customer = (Customer)Session["Customer"];

            var Ls = new List<ViewCart>();
            if (customer != null)
            {
                var viewCartService = new ViewCartService();
                var viewCartList = viewCartService.GetProductsFromViewCart(customer.Id);

                if (viewCarts.Count > 0 && viewCartList.Count > 0)
                {
                    foreach (var item in viewCarts)
                    {
                        var vc = viewCartService.GetProductFromViewCart(item.Product.Id);
                        if (vc == null)
                        {
                            var viewCart = new ViewCart();

                            viewCart.Customer = new Customer();
                            viewCart.Product = item.Product;
                            viewCart.Quantity = 1;

                            viewCartList.Add(viewCart);
                        }
                    }

                    viewCarts = viewCartList;
                }
                else
                {
                    if (viewCartList.Count > 0)
                    {
                        viewCarts = viewCartList;
                    }
                }
            }
            var tot = 0;
            foreach (var item in viewCarts)
            {
                tot += item.Quantity;
            }

            return tot;
        }

    }
}