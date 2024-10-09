using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yumify.API.DTO.Products;
using Yumify.API.Helper;
using Yumify.Core.Entities;
using Yumify.Core.IRepository;
using Yumify.Repository.SpecificationEvaluator;
using Yumify.Repository.SpecificationEvaluator.ProductSpec;
using Yumify.Service.Helper.Pagintion;
using Yumify.Service.SpecificationEvaluator.Sorting;

namespace Yumify.API.Controllers
{
    public class ProductsController : BaseAPIController
    {
        private readonly IGenericRepository<Product> _productsRepo;

        private readonly IMapper _Mapper;

        public ProductsController(IGenericRepository<Product> ProductsRepo,IMapper mapper)
        {
            _productsRepo = ProductsRepo;
            _Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<IEnumerable<GetProductDTO>>>> GetProducts([FromQuery] GetSpecParts? specParts)
        {
            var response = new GeneralResponse(NotFound().StatusCode,String.Empty);
            
            var spec = new ProductsSpec(specParts);
            var productsWitSpec = await _productsRepo.GetAllWithSpec(spec);
            if (productsWitSpec is not null)
            {
                var MappingProducts = _Mapper.Map<IEnumerable<GetProductDTO>>(productsWitSpec);
                response.Data = MappingProducts;
                response.StatusCode = 200;
                response.Message = response.chooseMessage(Ok().StatusCode);
                var Paginated= new Pagination<IEnumerable<GetProductDTO>>() { PageIndex=specParts.PageIndex,PageSize=specParts.PageSize};
                Paginated.PageData=response.Data;
                return Ok(Paginated);
            }
            response.Message = response.chooseMessage(NotFound().StatusCode);
            return NotFound(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var response = new GeneralResponse(NotFound().StatusCode);
            var spec = new ProductsSpec(id);
            var productwithSpec = await _productsRepo.GetByIdWithSpec(spec);
            if (productwithSpec is not null)
            {
                var MappingProduct = _Mapper.Map<GetProductDTO>(productwithSpec);
                response.StatusCode=Ok().StatusCode;
                response.Message=response.chooseMessage(Ok().StatusCode);
                response.Data = MappingProduct;
                return Ok(response);
            }
            response.Message= response.chooseMessage(NotFound().StatusCode);
            return NotFound(response);
        }
    }
}
