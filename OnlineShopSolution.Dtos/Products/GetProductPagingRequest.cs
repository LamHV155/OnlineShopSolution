
using OnlineShopSolution.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopSolution.Dtos.Products
{
    public class GetProductPagingRequest : PageRequestBase
    {
        public string Keyword { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
