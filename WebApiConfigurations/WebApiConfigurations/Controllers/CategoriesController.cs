using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using WebApiConfigurations.DAL.EFCore;
using WebApiConfigurations.DTOs.CategoryDTOs;
using WebApiConfigurations.DTOs.ProductDTOs;
using WebApiConfigurations.Entities;
using WebApiConfigurations.Mapping;

namespace WebApiConfigurations.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        IMapper _mapper;

        public CategoriesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAllCategories()
        {
            var list = await _context.Categories.ToListAsync();
            //var result = _mapper.Map<List<GetCategoryDTO>>(list);
            return StatusCode((int)HttpStatusCode.OK, list);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetByIdCategory(Guid id)
        {
            var existsCategory = await _context.Categories.FirstOrDefaultAsync(x  => x.Id == id);
            if (existsCategory == null) 
                return NotFound();

            var result = _mapper.Map<GetCategoryDTO>(existsCategory);

            return StatusCode((int)HttpStatusCode.Found, result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO createCategoryDTO)
        {
            //Category category = new Category()
            //{
            //    CategoryName = createCategoryDTO.CategoryName,
            //    Description = createCategoryDTO.Description,
            //};

            var category = _mapper.Map<Category>(createCategoryDTO);

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryDTO updateCategoryDTO)
        {
            var existsCategory = await _context.Categories.FirstOrDefaultAsync(x =>x.Id == id);
            if (existsCategory == null)
                return NotFound();

            //existsCategory.CategoryName = updateCategoryDTO.CategoryName == null? existsCategory.CategoryName : updateCategoryDTO.CategoryName;
            //existsCategory.Description = updateCategoryDTO.Description == null? existsCategory.Description : updateCategoryDTO.Description;

            //_context.Update(existsCategory);

            _mapper.Map(updateCategoryDTO, existsCategory);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
