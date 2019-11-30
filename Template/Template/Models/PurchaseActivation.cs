using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Template.Models;

namespace Template.Models
{
    public class PurchaseActivation
    {
        public Product Product { get; set; }
        public List<string> ActivationCodes { get; set; }
    }
}