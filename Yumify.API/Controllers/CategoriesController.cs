using Microsoft.AspNetCore.Mvc;
using Yumify.API.Helper;
using Yumify.Core.Entities;
using Yumify.Core.IServices;

namespace Yumify.API.Controllers
{
    public class CategoriesController : BaseAPIController
    {
        private readonly IProductServices _CategoryServices;

        public CategoriesController(IProductServices productServices)
        {

            _CategoryServices = productServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var response = new GeneralResponse(Ok().StatusCode);
            var Cagtegories = await _CategoryServices.GetCategoriesAsync();
            if (Cagtegories is not null)
            {
                response.Message = response.chooseMessage(Ok().StatusCode);
                response.Data = Cagtegories;
                return Ok(response);
            }
            response.StatusCode = NoContent().StatusCode;
            response.Message = response.chooseMessage(NoContent().StatusCode);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var response = new GeneralResponse(Ok().StatusCode);
            var Category = await _CategoryServices.GetCategoryByIdAsync(id);
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


        [HttpPost("AddCategory")]
        public async Task<ActionResult<ProductBrand>> AddCategory(ProductCategory Category)
        {
            var response = new GeneralResponse(Ok().StatusCode);
            var AddedCategory = await _CategoryServices.AddCategoryAsync(Category);
            if (AddedCategory is not null)
            {
                response.Message = "Category Added Sucessfully";
                response.Data = AddedCategory;
                return Ok(response);
            }

            response.StatusCode = NotFound().StatusCode;
            response.Message = response.chooseMessage(NotFound().StatusCode);
            return BadRequest(response);
        }

        [HttpPut("UpdateCategory")]
        public async Task<ActionResult<ProductBrand>> UpdateCategory(ProductCategory Category)
        {
            var response = new GeneralResponse(Ok().StatusCode);

            var UpdatedCategory = await _CategoryServices.UpdateCategoryAsync(Category);
            if (UpdatedCategory is not null)
            {
                response.Message = "Category Updated Sucessfully";
                response.Data = UpdatedCategory;
                return Ok(response);
            }

            response.StatusCode = NotFound().StatusCode;
            response.Message = response.chooseMessage(NotFound().StatusCode);
            return BadRequest(response);
        }

        [HttpDelete("DeleteCategory")]
        public async Task<ActionResult<ProductBrand>> DeleteCategory(int CategoryId)
        {
            var response = new GeneralResponse(Ok().StatusCode);

            var DeleteCategory = await _CategoryServices.DeleteCategoryAsync(CategoryId);
            if (DeleteCategory is not null)
            {
                response.Message = "Category Deleted Sucessfully";
                response.Data = DeleteCategory;
                return Ok(response);
            }

            response.StatusCode = NotFound().StatusCode;
            response.Message = response.chooseMessage(NotFound().StatusCode);
            return BadRequest(response);
        }

    }
}
