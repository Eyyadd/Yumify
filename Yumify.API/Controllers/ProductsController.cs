﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yumify.API.DTO.Products;
using Yumify.API.Helper;
using Yumify.API.Helper.Sorting;
using Yumify.Core.Entities;
using Yumify.Core.IRepository;
using Yumify.Repository.SpecificationEvaluator.ProductSpec;

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
        public async Task<ActionResult<IEnumerable<GetProductDTO>>> GetProducts([FromQuery]Sorting? sorting)
        {
            var response = new GeneralResponse(NotFound().StatusCode,String.Empty);
            
            var spec = new ProductsSpec(sorting);
            var productsWitSpec = await _productsRepo.GetAllWithSpec(spec);
            if (productsWitSpec is not null)
            {
                var MappingProducts = _Mapper.Map<IEnumerable<GetProductDTO>>(productsWitSpec);
                response.Data = MappingProducts;
                response.StatusCode = 200;
                response.Message = response.chooseMessage(Ok().StatusCode);
                return Ok(response);
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
