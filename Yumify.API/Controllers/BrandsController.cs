﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yumify.API.Helper;
using Yumify.Core.Entities;
using Yumify.Core.IRepository;
using Yumify.Core.Specification;

namespace Yumify.API.Controllers
{
    public class BrandsController : BaseAPIController
    {
        private readonly IGenericRepository<ProductBrand> _BrandRepo;

        public BrandsController(IGenericRepository<ProductBrand> brandRepo)
        {
            _BrandRepo = brandRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            var response = new GeneralResponse(Ok().StatusCode);
            var spec = new BaseSpecification<ProductBrand>();
            var allBrands = await _BrandRepo.GetAllWithSpec(spec);
            if (allBrands is not null)
            {
                response.Message=response.chooseMessage(Ok().StatusCode);
                response.Data= allBrands;
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
            var spec = new BaseSpecification<ProductBrand>(B=>B.Id==id);
            var allBrands = await _BrandRepo.GetByIdWithSpec(spec);
            if (allBrands is not null)
            {
                response.Message = response.chooseMessage(Ok().StatusCode);
                response.Data = allBrands;
                return Ok(response);
            }
            response.StatusCode = NoContent().StatusCode;
            response.Message = response.chooseMessage(NoContent().StatusCode);
            return Ok(response);
        }
    }
}
