using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Template.Models
{
    public class PurchasedProduct
    {
        public int Id { get; set; }
        public DateTime PurchasedDate { get; set; }
        public string ActivationCode { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }


    }
}