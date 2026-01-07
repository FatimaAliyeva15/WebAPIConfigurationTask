using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using WebApiAdvance.DAL.Repositories.AbstractRepositories;
using WebApiAdvance.DAL.UnitOfWork.Abstract;
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
        private readonly IUnitOfWork _unitOfWork;
        IMapper _mapper;

        public CategoriesController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAllCategories()
        {
            var list = await _unitOfWork.CategoryRepository.GetAllAsync();
            //var result = _mapper.Map<List<GetCategoryDTO>>(list);
            return StatusCode((int)HttpStatusCode.OK, list);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetByIdCategory(Guid id)
        {
            var existsCategory = await _unitOfWork.CategoryRepository.Get(x  => x.Id == id);
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

            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var existsCategory = await _unitOfWork.CategoryRepository.Get(x =>x.Id == id);
            if (existsCategory == null) 
                return NotFound();

            _unitOfWork.CategoryRepository.Delete(existsCategory.Id);
            await _unitOfWork.SaveAsync();  
            return NoContent();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryDTO updateCategoryDTO)
        {
            var existsCategory = await _unitOfWork.CategoryRepository.Get(x =>x.Id == id);
            if (existsCategory == null)
                return NotFound();

            existsCategory.CategoryName = updateCategoryDTO.CategoryName == null? existsCategory.CategoryName : updateCategoryDTO.CategoryName;
            existsCategory.Description = updateCategoryDTO.Description == null? existsCategory.Description : updateCategoryDTO.Description;

            //_context.Update(existsCategory);

            //_mapper.Map(updateCategoryDTO, existsCategory);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
