using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopSolution.Service.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        IPublicProductRepo _publicProductRepo;
        public ProductController(IPublicProductRepo publicProductRepo)
        {
            _publicProductRepo = publicProductRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _publicProductRepo.GetAll_test();
           //    if (products == null) return BadRequest();
            return Ok(products);
        }
    }
}
