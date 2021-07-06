using OnlineShopSolution.Data.EF;
using OnlineShopSolution.Service.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineShopSolution.Dtos.Products;
using OnlineShopSolution.Dtos.Common;

namespace OnlineShopSolution.Service.Products
{
    public class PublicProductRepo : IPublicProductRepo
    {

        private readonly OnlineShopDbContext _context;

        public PublicProductRepo(OnlineShopDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ProductViewModel>> GetAllByCategoryId(pub_GetProductPagingRequest req)
        {
            //join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic };

            //filter
            if (req.CategoryId.HasValue && req.CategoryId.Value > 0)
            {
                query = query.Where(x => x.pic.CategoryId == req.CategoryId);
            }

            //paging
            int totalRow = await query.CountAsync();

            var data = await query
                .Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize)
                .Select(x => new ProductViewModel
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginPrice = x.p.OriginPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();

            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data

            };
            return pagedResult;
        }
    }
}
