using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Template.Models;
using System.Data.Entity;

namespace Template.Database.Services
{
    public class ViewCartService
    {
        private ShoppingDbContext _context;

        public ViewCartService()
        {
            _context = new ShoppingDbContext();
        }

        public void AddProductinViewCart (ViewCart viewCart)
        {
            _context.ViewCarts.Add(viewCart);
            _context.SaveChanges();
        }
        public List<ViewCart> MovePurchasedProductsFromViewCart(int UserId)
        {
            List<ViewCart> viewCartList = new List<ViewCart>();
            viewCartList = _context.ViewCarts.Include(m=>m.Customer).Include(m=>m.Product).Where(p => p.CustomerId == UserId).ToList();

            return viewCartList;
        }

        public void DeleteProduct(List<PurchasedProduct> list)
        {
            foreach (var item in list)
            {
                var product = _context.ViewCarts.FirstOrDefault(m => m.ProductId == item.ProductId);
                if (product!=null)
                {
                    _context.ViewCarts.Remove(product);
                    _context.SaveChanges();
                }
            }
        }

        public List<ViewCart> GetProductsFromViewCart(int userId)
        {
            return _context.ViewCarts.Include(p=>p.Product).Where(m=>m.CustomerId==userId).ToList();
        }

        public ViewCart GetProductFromViewCart(int productID)
        {
            return _context.ViewCarts.Include(p=>p.Product).SingleOrDefault(m => m.ProductId == productID);
        }

        public void DeleteProductinViewCart(int productID)
        {
            var product = _context.ViewCarts.FirstOrDefault(m => m.ProductId == productID);
            if (product != null)
            {
                _context.ViewCarts.Remove(product);
                _context.SaveChanges();
            }
        }

        public void UpdateQuantity(int id, int quantity)
        {
            var product = _context.ViewCarts.FirstOrDefault(m => m.ProductId == id);
            if (product != null)
            {
                product.Quantity = quantity;
            }
                _context.SaveChanges();
        }
    }
}