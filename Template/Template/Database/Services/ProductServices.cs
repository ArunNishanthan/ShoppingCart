using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Template.Database;
using System.Data.Entity;
using Template.Models;
using System.Linq;

namespace Template.Database.Services
{
    public class ProductServices
    {

        private ShoppingDbContext _context;

        public ProductServices()
        {
            _context = new ShoppingDbContext();
        }

        public  List<Product> GetProducts()
        {
            return _context.Products.OrderBy(m=>m.Name).ToList();
        }
    }
}