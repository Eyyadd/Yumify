using Microsoft.AspNetCore.Mvc;
using Yumify.API.Helper;
using Yumify.Core.Entities;
using Yumify.Core.IServices;

namespace Yumify.API.Controllers
{
    public class BrandsController : BaseAPIController
    {
        private readonly IProductServices _BrandServices;

        public BrandsController(IProductServices productServices)
        {
            _BrandServices = productServices;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var response = new GeneralResponse(Ok().StatusCode);
            var Brands= await _BrandServices.GetBrandsAsync();
            if (Brands is not null)
            {
                response.Message=response.chooseMessage(Ok().StatusCode);
                response.Data= Brands;
                return Ok(response);
            }
            response.StatusCode = NotFound().StatusCode;
            response.Message = response.chooseMessage(NotFound().StatusCode);
            return BadRequest(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductBrand>> GetBrand(int id)
        {
            var response = new GeneralResponse(Ok().StatusCode);
            var Brand= await _BrandServices.GetBrandByIdAsync(id);
            if (Brand is not null)
            {
                response.Message = response.chooseMessage(Ok().StatusCode);
                response.Data = Brand;
                return Ok(response);
            }
            response.StatusCode = NotFound().StatusCode;
            response.Message = response.chooseMessage(NotFound().StatusCode);
            return BadRequest(response);
        }

        [HttpPost("AddBrand")]
        public async Task<ActionResult<ProductBrand>> AddBrands(ProductBrand brand)
        {
            var response = new GeneralResponse(Ok().StatusCode);
            var AddedBrand = await _BrandServices.AddBrandsAsync(brand);
            if (AddedBrand is not null)
            {
                response.Message = "Brand Added Sucessfully";
                response.Data = AddedBrand;
                return Ok(response);
            }

            response.StatusCode = NotFound().StatusCode;
            response.Message = response.chooseMessage(NotFound().StatusCode);
            return BadRequest(response);
        }

        [HttpPut("UpdateBrand")]
        public async Task<ActionResult<ProductBrand>> UpdateBrand(ProductBrand brand)
        {
            var response = new GeneralResponse(Ok().StatusCode);

            var UpdatedBrand = await _BrandServices.UpdateBrandsAsync(brand);
            if (UpdatedBrand is not null)
            {
                response.Message = "Brand Updated Sucessfully";
                response.Data = UpdatedBrand;
                return Ok(response);
            }

            response.StatusCode = NotFound().StatusCode;
            response.Message = response.chooseMessage(NotFound().StatusCode);
            return BadRequest(response);
        }

        [HttpDelete("DeleteBrand")]
        public async Task<ActionResult<ProductBrand>> DeleteBrand(int BrandId)
        {
            var response = new GeneralResponse(Ok().StatusCode);

            var deletedBrand = await _BrandServices.DeleteBrandsAsync(BrandId);
            if (deletedBrand is not null)
            {
                response.Message = "Brand deleted Sucessfully";
                response.Data = deletedBrand;
                return Ok(response);
            }

            response.StatusCode = NotFound().StatusCode;
            response.Message = response.chooseMessage(NotFound().StatusCode);
            return BadRequest(response);
        }


    }
}
