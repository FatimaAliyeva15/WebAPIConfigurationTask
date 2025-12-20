using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApiConfigurations.DAL.EFCore;
using WebApiConfigurations.DTOs.CategoryDTOs;
using WebApiConfigurations.Entities;

namespace WebApiConfigurations.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var list = await _context.Categories.ToListAsync();
            return StatusCode((int)HttpStatusCode.OK, list);
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdCategory(Guid id)
        {
            var existsCategory = await _context.Categories.FirstOrDefaultAsync(x  => x.Id == id);
            if (existsCategory == null) 
                return NotFound();

            return StatusCode((int)HttpStatusCode.Found, existsCategory);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO createCategoryDTO)
        {
            Category category = new Category()
            {
                CategoryName = createCategoryDTO.CategoryName,
                Description = createCategoryDTO.Description,
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var existsCategory = await _context.Categories.FirstOrDefaultAsync(x =>x.Id == id);
            if (existsCategory == null) 
                return NotFound();

            _context.Categories.Remove(existsCategory);
            await _context.SaveChangesAsync();  
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryDTO updateCategoryDTO)
        {
            var existsCategory = await _context.Categories.FirstOrDefaultAsync(x =>x.Id == id);
            if (existsCategory == null)
                return NotFound();

            existsCategory.CategoryName = updateCategoryDTO.CategoryName == null? existsCategory.CategoryName : updateCategoryDTO.CategoryName;
            existsCategory.Description = updateCategoryDTO.Description == null? existsCategory.Description : updateCategoryDTO.Description;

            _context.Update(existsCategory);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
