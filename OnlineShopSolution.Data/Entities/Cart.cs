﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopSolution.Data.Entities
{
    public class Cart
    {
        public int Id { set; get; }
        public int ProductId { set; get; }
        public Product Product { get; set; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
