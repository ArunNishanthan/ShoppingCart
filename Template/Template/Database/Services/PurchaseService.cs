using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Template.Models;
using System.Data.Entity;

namespace Template.Database.Services
{
    public class PurchaseService
    {

        private ShoppingDbContext _context;

        public PurchaseService()
        {
            _context = new ShoppingDbContext();
        }

        public List<PurchasedProduct> GetPurchasedProductsByUserId(int UserId)
        {
            List<PurchasedProduct> purchasedProductList = new List<PurchasedProduct>();
            purchasedProductList = _context.PurchasedProducts.Include(m=>m.Product).Where(p => p.Customer.Id == UserId).OrderByDescending(m=>m.Id).ToList();

            return purchasedProductList;
        }

        public void SaveDataToPurchasedProducts(List<PurchasedProduct> list)
        {
            foreach (var item in list)
            {
                _context.PurchasedProducts.Add(item);
                _context.SaveChanges();
            }
          
        }
    }
}