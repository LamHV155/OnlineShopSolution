
using Microsoft.AspNetCore.Http;
using OnlineShopSolution.Dtos.Common;
using OnlineShopSolution.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopSolution.Service.Products
{
    public interface IManageProductRepo
    {
        Task<int> Create(ProductCreateRequest req);
        Task<int> Update(ProductUpdateRequest req);
        Task<int> Delete(int productId);
        Task<bool> UpdatePrice(int productId, decimal newPrice);
        Task<bool> UpdateStock(int productId, int quantity);
        Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest req);
        public Task AddViewCount(int ProductId);

        //Task<int> AddImages(int productId, List<IFormFile> files);
        //Task<int> RemoveImage(int imageId);
        //Task<int> UpdateImages(int imageId, string caption, bool isDefault);
        //Task<List<ProductImageViewModel>> GetListImage(int productId);
    }
}
