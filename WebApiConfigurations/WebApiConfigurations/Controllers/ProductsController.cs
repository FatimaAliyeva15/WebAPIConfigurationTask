using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApiConfigurations.DAL.EFCore;
using WebApiConfigurations.DTOs.ProductDTOs;
using WebApiConfigurations.Entities;

namespace WebApiConfigurations.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext  _context;
        IMapper _mapper;

        public ProductsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var list = await _context.Products.Include(p => p.Category).Select(p => new GetProductDTO
            {
                Name = p.Name,
                Count = p.Count,
                Description = p.Description,
                Price = p.Price,
                Currency = p.Currency,
                CategoryName = p.Category.CategoryName
            }).ToListAsync();
            //var result = _mapper.Map<List<GetProductDTO>>(list);


            return StatusCode((int)HttpStatusCode.OK, list);
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdProduct(Guid id)
        {
            var existsProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if(existsProduct == null) 
                return NotFound();

            var result = _mapper.Map<GetProductDTO>(existsProduct);

            return StatusCode((int)HttpStatusCode.Found, result);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductDTO createProductDTO)
        {
            //Product product = new Product()
            //{
            //    Name = createProductDTO.Name,
            //    Price = createProductDTO.Price,
            //    Description = createProductDTO.Description,
            //    Count = createProductDTO.Count,
            //    Currency = createProductDTO.Currency,
            //    CategoryId = createProductDTO.CategoryId,
            //};

            var product = _mapper.Map<Product>(createProductDTO);

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var existsProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if(existsProduct == null)
                return NotFound();

            _context.Products.Remove(existsProduct);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductDTO updateProductDTO)
        {
            var existsProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if(existsProduct == null)  
                return NotFound();

            //existProduct.Name = updateProductDTO.Name == null? existProduct.Name : updateProductDTO.Name;
            //existProduct.Price = updateProductDTO.Price == null? existProduct.Price : updateProductDTO.Price;
            //existProduct.Description = updateProductDTO.Description == null? existProduct.Description : updateProductDTO.Description;
            //existProduct.Currency = updateProductDTO.Currency == null? existProduct.Currency : updateProductDTO.Currency;
            //existProduct.Count = updateProductDTO.Count == null? existProduct.Count : updateProductDTO.Count;
            //existProduct.CategoryId = updateProductDTO.CategoryId == null? existProduct.CategoryId : updateProductDTO.CategoryId;

            //_context.Update(existProduct);

            _mapper.Map(updateProductDTO, existsProduct);
            await _context.SaveChangesAsync();
            return NoContent();
        } 
    }
}
