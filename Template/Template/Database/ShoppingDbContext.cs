using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Template.Models;

namespace Template.Database
{
    public class ShoppingDbContext : DbContext
    {
        public ShoppingDbContext() : base("Server=localhost;Database=rubbish; integrated security=true")
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PurchasedProduct> PurchasedProducts { get; set; }
        public DbSet<ViewCart> ViewCarts { get; set; }
    }
}