using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Template.Models;

namespace Template.ViewModel
{
    public class PurchaseActivationCode
    {
        public Product Product { get; set; }
        public DateTime PurchaseDate { get; set; }
        public List<string> ActivationCodes { get; set; }
    }
}