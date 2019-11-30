using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Template.Models;

namespace Template.ViewModel
{
    public class ViewCartViewModel
    {
        public bool haveProduct { get; set; }
        public List<ViewCart> ViewCarts { get; set; }
    }
}