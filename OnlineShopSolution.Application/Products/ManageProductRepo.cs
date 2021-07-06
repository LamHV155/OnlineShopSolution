using Microsoft.EntityFrameworkCore;
using OnlineShopSolution.Data.EF;
using OnlineShopSolution.Data.Entities;
using OnlineShopSolution.Dtos.Common;
using OnlineShopSolution.Dtos.Products;
using OnlineShopSolution.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopSolution.Service.Common;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;

namespace OnlineShopSolution.Service.Products
{
    public class ManageProductRepo : IManageProductRepo
    {
        private readonly OnlineShopDbContext _context;
        private readonly IStorageService _storageService;

        public ManageProductRepo(OnlineShopDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }



        public async Task<int> Create(ProductCreateRequest req)
        {
            var product = new Product()
            {
                Price = req.Price,
                OriginPrice = req.OriginalPrice,
                Stock = req.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation{
                        Name = req.Name,
                        Description = req.Description,
                        Details = req.Details,
                        SeoDescription = req.SeoDescription,
                        SeoAlias = req.SeoAlias,
                        SeoTitle = req.SeoTitle,
                        LanguageId = req.LanguageId
                    }
                }
            };

            // Save image
            if (req.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = req.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(req.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }

            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
        }

        public async Task AddViewCount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount++;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new OnlineShopException($"Cannot find a product: {productId}");

            var images = _context.ProductImages.Where(i => i.ProductId == productId);
            foreach(var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest req)
        {
            //join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic};

            //filter
            if (!string.IsNullOrEmpty(req.Keyword))
            {
                query = query.Where(x => x.pt.Name.Contains(req.Keyword));
            }
            if(req.CategoryIds.Count > 0)
            {
                query = query.Where(x => req.CategoryIds.Contains(x.pic.CategoryId));
            }

            //paging
            int totalRow = await query.CountAsync();
            var data = await query
                .Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize)
                .Select(x => new ProductViewModel {
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

        public async  Task<int> Update(ProductUpdateRequest req)
        {
            var product = _context.Products.Find(req.Id);
            var productTranslation = await _context.ProductTranslations
                .FirstOrDefaultAsync(x => x.ProductId == req.Id && x.LanguageId == req.LanguageId);
            if (product == null) throw new OnlineShopException($"Cannot find a product with id: {req.Id}");

            productTranslation.Name = req.Name;
            productTranslation.SeoAlias = req.SeoAlias;
            productTranslation.SeoDescription = req.SeoDescription;
            productTranslation.SeoTitle = req.SeoTitle;
            productTranslation.Description = req.Description;
            productTranslation.Details = req.Details;


            //Update image
            if (req.ThumbnailImage != null)
            {
                var thumbnailImage = await _context.ProductImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.ProductId == req.Id);
                if (thumbnailImage != null)
                {
                    thumbnailImage.FileSize = req.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(req.ThumbnailImage);
                    _context.ProductImages.Update(thumbnailImage);
                }
            }

            return await _context.SaveChangesAsync();
        }


        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if(product == null) throw new OnlineShopException($"Cannot find a product with id: {productId}");
            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId,int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new OnlineShopException($"Cannot find a product with id: {productId}");
            product.Stock = quantity;
            return await _context.SaveChangesAsync() > 0;
        }


        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
