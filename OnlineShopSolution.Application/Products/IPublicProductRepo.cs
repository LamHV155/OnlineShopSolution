
using OnlineShopSolution.Dtos.Common;
using OnlineShopSolution.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopSolution.Service.Products
{
    public interface IPublicProductRepo
    {
        public Task<PagedResult<ProductViewModel>> GetAllByCategoryId(pub_GetProductPagingRequest req);
        public Task<List<ProductViewModel>> GetAll_test();
    }
}
