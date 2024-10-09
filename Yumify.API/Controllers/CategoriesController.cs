using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yumify.API.Helper;
using Yumify.Core.Entities;
using Yumify.Core.IRepository;
using Yumify.Core.Specification;

namespace Yumify.API.Controllers
{
    public class CategoriesController : BaseAPIController
    {
        private readonly IGenericRepository<ProductCategory> _CategoryRepos;

        public CategoriesController(IGenericRepository<ProductCategory> CategoryRepos) 
        {
            _CategoryRepos = CategoryRepos;
        }

        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            var response = new GeneralResponse(Ok().StatusCode);
            var spec = new BaseSpecification<ProductCategory>();
            var AllCategories = await _CategoryRepos.GetAllWithSpec(spec);
            if (AllCategories is not null)
            {
                response.Message = response.chooseMessage(Ok().StatusCode);
                response.Data = AllCategories;
                return Ok(response);
            }
            response.StatusCode = NoContent().StatusCode;
            response.Message = response.chooseMessage(NoContent().StatusCode);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBrands(int id)
        {
            var response = new GeneralResponse(Ok().StatusCode);
            var spec = new BaseSpecification<ProductCategory>(C=>C.Id==id);
            var Category = await _CategoryRepos.GetAllWithSpec(spec);
            if (Category is not null)
            {
                response.Message = response.chooseMessage(Ok().StatusCode);
                response.Data = Category;
                return Ok(response);
            }
            response.StatusCode = NoContent().StatusCode;
            response.Message = response.chooseMessage(NoContent().StatusCode);
            return Ok(response);
        }
    }
}
