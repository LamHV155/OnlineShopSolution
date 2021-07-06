using OnlineShopSolution.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopSolution.Dtos.Products
{
    public class pub_GetProductPagingRequest : PageRequestBase
    {
        public int? CategoryId { get; set; }
    }
}
