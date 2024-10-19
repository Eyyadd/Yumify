using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yumify.API.DTO.Products;
using Yumify.API.Helper;
using Yumify.Core.Entities;
using Yumify.Core.IRepository;
using Yumify.Core.IServices;
using Yumify.Repository.SpecificationEvaluator;
using Yumify.Repository.SpecificationEvaluator.ProductSpec;
using Yumify.Service.DTO.Products;
using Yumify.Service.Helper.Pagintion;
using Yumify.Service.SpecificationEvaluator.Sorting;

namespace Yumify.API.Controllers
{
    public class ProductsController : BaseAPIController
    {
        private readonly IProductServices _ProductServices;
        private readonly IMapper _Mapper;

        public ProductsController(IProductServices productServices,IMapper mapper)
        {
            _ProductServices = productServices;
            _Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<IEnumerable<GetProductDTO>>>> GetProducts([FromQuery] GetSpecParts? specParts)
        {
            var response = new GeneralResponse(NotFound().StatusCode,String.Empty);
            var products =await _ProductServices.GetProductsAsync(specParts);

            if (products is not null)
            {
                var MappingProducts = _Mapper.Map<IEnumerable<GetProductDTO>>(products);
                response.Data = MappingProducts;
                response.StatusCode = 200;
                response.Message = response.chooseMessage(Ok().StatusCode);

                var ProductsCount = _ProductServices.GetCountAsync(specParts);
                var Paginated = new Pagination<IEnumerable<GetProductDTO>>()
                { 
                    PageIndex=specParts.PageIndex,
                    PageSize=specParts.PageSize
                };

                Paginated.PageData=response.Data;
                Paginated.PageCount = await ProductsCount;

                return Ok(Paginated);
            }
            response.Message = response.chooseMessage(NotFound().StatusCode);
            return NotFound(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetProductDTO>> GetProduct(int id)
        {
            var response = new GeneralResponse(NotFound().StatusCode);
            var product=await _ProductServices.GetProductByIdAsync(id);
           
            if (product is not null)
            {
                var MappingProduct = _Mapper.Map<GetProductDTO>(product);
                response.StatusCode=Ok().StatusCode;
                response.Message=response.chooseMessage(Ok().StatusCode);
                response.Data = MappingProduct;
                return Ok(response);
            }
            response.Message= response.chooseMessage(NotFound().StatusCode);
            return NotFound(response);
        }

        [HttpPost("AddProduct")]
        public async Task<ActionResult<AddUpdateProduct>> AddProduct(AddUpdateProduct product)
        {
            var response = new GeneralResponse(Ok().StatusCode);
            var mappedProduct = _Mapper.Map<Product>(product);
            var AddedProduct = await _ProductServices.AddProductAsync(mappedProduct);
            if (AddedProduct is not null)
            {
                var ReturnedProducts = _Mapper.Map<AddUpdateProduct>(AddedProduct);
                response.Message = "Product Added Sucessfully";
                response.Data = ReturnedProducts;
                return Ok(response);
            }

            response.StatusCode = NotFound().StatusCode;
            response.Message = response.chooseMessage(NotFound().StatusCode);
            return BadRequest(response);
        }

        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<AddUpdateProduct>> UpdateProduct(AddUpdateProduct product)
        {
            var response = new GeneralResponse(Ok().StatusCode);

            var mappedProduct= _Mapper.Map<Product>(product);
            var UpdatedProduct = await _ProductServices.UpdateProductAsync(product.Id, mappedProduct);
            if (UpdatedProduct is not null)
            {
                var ReturnedProducts = _Mapper.Map<AddUpdateProduct>(UpdatedProduct);
                response.Message = "Brand Updated Sucessfully";
                response.Data = ReturnedProducts;
                return Ok(response);
            }

            response.StatusCode = NotFound().StatusCode;
            response.Message = response.chooseMessage(NotFound().StatusCode);
            return BadRequest(response);
        }

        [HttpDelete("DeleteProduct")]
        public async Task<ActionResult<GetProductDTO>> DeleteProduct(int categoryId)
        {
            var response = new GeneralResponse(Ok().StatusCode);
            var DeleteCategory = await _ProductServices.DeleteProductAsync(categoryId);
            if (DeleteCategory is not null)
            {
                var mappedProduct=_Mapper.Map<GetProductDTO>(DeleteCategory);
                response.Message = "category deleted Sucessfully";
                response.Data = mappedProduct;
                return Ok(response);
            }

            response.StatusCode = NotFound().StatusCode;
            response.Message = response.chooseMessage(NotFound().StatusCode);
            return BadRequest(response);
        }

    }
}
