using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopSolution.Dtos.Products
{
    public class ProductViewModel
    {
		public int Id { get; set; }
		public decimal Price { get; set; }
		public decimal OriginPrice { get; set; }
		public string Details { get; set; }
		public int Stock { get; set; }
		public int ViewCount { get; set; }
		public DateTime DateCreated { get; set; }

		public string Name { set; get; }
		public string Description { set; get; }
		public string SeoDescription { set; get; }
		public string SeoTitle { set; get; }

		public string SeoAlias { get; set; }
		public string LanguageId { set; get; }
	}
}
