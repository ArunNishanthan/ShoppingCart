using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Template.Models;

namespace Template.Database.Services
{
    public class CustomerService
    {
        private ShoppingDbContext _context;

        public CustomerService()
        {
            _context = new ShoppingDbContext();
        }

        public Customer GetCustomer(string userName, string password)
        {
           return _context.Customers.SingleOrDefault(m=>m.Userid==userName && m.Password==password);
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public Customer GetCustomerById(int Id)
        {
            return _context.Customers.SingleOrDefault(m => m.Id ==Id);
        }

        public void UpdateCustomer (Customer customer)
        {
            var customerInDb = _context.Customers.SingleOrDefault(m => m.Id == customer.Id);

            customerInDb.FullName = customer.FullName;
            customerInDb.Password = customer.Password;
            customerInDb.ConfirmPassword = customer.ConfirmPassword;
            customerInDb.Userid = customer.Userid;
            customerInDb.Email= customer.Email;

            _context.SaveChanges();
        }

        public bool CheckCustomerName(string Userid)
        {
            var cus = _context.Customers.Where(p => p.Userid == Userid).FirstOrDefault();

            if (cus == null)
                return false;
            else
                return true;
        }
    }
}