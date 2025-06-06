﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Data.DbModels
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
    }
}
