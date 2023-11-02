using AutoMapper;
using EcommerceAPI.Models.Category;
using EcommerceAPI.Models.Category.Dto;
using EcommerceAPI.Models.Comment;
using EcommerceAPI.Models.Comment.Dto;
using EcommerceAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http;

namespace EcommerceAPI.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository,IMapper mapper) 
        { 
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetAll()
        {
            return  _mapper.Map<List<CategoryDto>>(await _categoryRepository.GetAll());
        }

        public async Task<CategoryDto> GetOne(int id)
        {
            return _mapper.Map<CategoryDto>(await _categoryRepository.GetOne(c=>c.CategoryId==id));
        }

        public async Task<CategoryDto> Update(Category category)
        {
            return _mapper.Map<CategoryDto>(await _categoryRepository.Update(category));
        }

        public async Task DeleteById(int id)
        {

            var category = await _categoryRepository.GetOne(c=>c.CategoryId==id);

            if (category == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            await _categoryRepository.Delete(category);
        }

        public async Task<CategoryDto> UpdateById(int id, UpdateCategoryDto updateCategoryDto)
        {
            var category = await _categoryRepository.GetOne(c => c.CategoryId == id);

            if (category == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var updated = _mapper.Map<Category>(updateCategoryDto);

            return _mapper.Map<CategoryDto>(await _categoryRepository.Update(updated));
        }

        public async Task<CategoryDto> Create(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);


            await _categoryRepository.Add(category);

            return _mapper.Map<CategoryDto>(category);
        }


    }
}

