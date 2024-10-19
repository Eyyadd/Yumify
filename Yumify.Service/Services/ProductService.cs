using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumify.Core.Entities;
using Yumify.Core.IRepository;
using Yumify.Core.IServices;
using Yumify.Repository.SpecificationEvaluator;
using Yumify.Repository.SpecificationEvaluator.ProductSpec;

namespace Yumify.Service.Services
{
    public class ProductService : IProductServices
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _Mapper;
        private readonly IGenericRepository<Product> _ProductRepo;
        private readonly IGenericRepository<ProductBrand> _BrandsRepos;
        private readonly IGenericRepository<ProductCategory> _CategoriessRepos;

        public ProductService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _Mapper = mapper;
            _ProductRepo = _UnitOfWork.Myrepository<Product>();
            _BrandsRepos = _UnitOfWork.Myrepository<ProductBrand>();
            _CategoriessRepos = _UnitOfWork.Myrepository<ProductCategory>();
        }

        public async Task<ProductBrand?> AddBrandsAsync(ProductBrand productBrand)
        {
            var IsBrandExist = await _BrandsRepos.GetByIdAsync(productBrand.Id);
            if (productBrand is not null && IsBrandExist is null)
            {
                await _BrandsRepos.AddAsync(productBrand);
                var result = await _UnitOfWork.CompleteAsync();
                if (result > 0)
                {
                    return productBrand;
                }
            }
            return null;

        }

        public async Task<ProductCategory?> AddCategoryAsync(ProductCategory productCategory)
        {
            var IsCategoryExist = await _CategoriessRepos.GetByIdAsync(productCategory.Id);
            if (productCategory is not null && IsCategoryExist is null)
            {
                await _CategoriessRepos.AddAsync(productCategory);
                var result = await _UnitOfWork.CompleteAsync();
                if (result > 0)
                {
                    return productCategory;
                }
            }
            return null;

        }

        public async Task<Product?> AddProductAsync(Product product)
        {

            if (product is not null)
            {
                await _ProductRepo.AddAsync(product);
                var result = await _UnitOfWork.CompleteAsync();

                if(result > 0)
                {
                    return product;
                }
            }
            return null;
        }

        public async Task<ProductBrand?> DeleteBrandsAsync(int BrandId)
        {
            var IsBrandExist = await _BrandsRepos.GetByIdAsync(BrandId);
            if (IsBrandExist is not null)
            {
                _BrandsRepos.Delete(IsBrandExist);
                var result = await _UnitOfWork.CompleteAsync();
                if (result > 0)
                {
                    return IsBrandExist;
                }
            }
            return null;
        }

        public async Task<ProductCategory?> DeleteCategoryAsync(int categoryId)
        {
            var IsCategoryExist = await _CategoriessRepos.GetByIdAsync(categoryId);
            if (IsCategoryExist is not null)
            {
                _CategoriessRepos.Delete(IsCategoryExist);
                var result = await _UnitOfWork.CompleteAsync();
                if (result > 0)
                {
                    return IsCategoryExist;
                }
            }
            return null;
        }

        public async Task<Product?> DeleteProductAsync(int id)
        {
            var IsProductExist= await _ProductRepo.GetByIdAsync(id);
            if (IsProductExist is not null)
            {
                 _ProductRepo.Delete(IsProductExist);
                var result = await _UnitOfWork.CompleteAsync();

                if (result > 0)
                {
                    return IsProductExist;
                }
            }
            return null;
        }

        public async Task<ProductBrand?> GetBrandByIdAsync(int BrandId)
        {
            var allBrands = await _BrandsRepos.GetByIdAsync(BrandId);
            return allBrands;
        }

        public async Task<IReadOnlyList<ProductBrand>?> GetBrandsAsync()
        {
            var allBrands = await _BrandsRepos.GetAllAsync();
            return allBrands as IReadOnlyList<ProductBrand>;

        }

        public async Task<IReadOnlyList<ProductCategory>?> GetCategoriesAsync()
        {
            var AllCategories = await _CategoriessRepos.GetAllAsync();
            return AllCategories as IReadOnlyList<ProductCategory>;

        }

        public async Task<ProductCategory?> GetCategoryByIdAsync(int CategoryId)
        {
            var Category = await _CategoriessRepos.GetByIdAsync(CategoryId);
            return Category;
        }

        public async Task<int> GetCountAsync(GetSpecParts? getSpecParts)
        {
            var ProductsCount = new ProductCountSpec(getSpecParts);
            var counts = await _ProductRepo.GetCountAsync(ProductsCount);
            return counts;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var spec = new ProductsSpec(id);
            var productwithSpec = await _ProductRepo.GetByIdWithSpecAsync(spec);
            return productwithSpec;
        }

        public async Task<IReadOnlyList<Product>?> GetProductsAsync(GetSpecParts? getSpecParts)
        {
            var ProductSpec = new ProductsSpec(getSpecParts);
            var products = await _ProductRepo.GetAllWithSpecAsync(ProductSpec);
            return products as IReadOnlyList<Product>;
        }

        public async Task<ProductBrand?> UpdateBrandsAsync(ProductBrand productBrand)
        {
            var IsBrandExist = await _BrandsRepos.GetByIdAsync(productBrand.Id);
            if (IsBrandExist is not null)
            {
                IsBrandExist.Name=productBrand.Name;
                var result = await _UnitOfWork.CompleteAsync();
                if (result > 0)
                {
                    return productBrand;
                }
            }
            return null;
        }

        public async Task<ProductCategory?> UpdateCategoryAsync(ProductCategory productCategory)
        {

            var IsCategoryExist = await _CategoriessRepos.GetByIdAsync(productCategory.Id);
            if (IsCategoryExist is not null)
            {
                IsCategoryExist.Name=productCategory.Name;
                var result = await _UnitOfWork.CompleteAsync();
                if (result > 0)
                {
                    return productCategory;
                }
            }
            return null;
        }

        public async Task<Product?> UpdateProductAsync(int productId,Product product)
        {
            var IsProductExist = await _ProductRepo.GetByIdAsync(productId);
            if (IsProductExist is not null)
            {
                IsProductExist.Name=product.Name;
                IsProductExist.Price = product.Price;
                IsProductExist.PictureUrl = product.PictureUrl;
                IsProductExist.BrandId = product.BrandId;
                IsProductExist.Description = product.Description;
                //_ProductRepo.Update(product);
                var result = await _UnitOfWork.CompleteAsync();
                if (result > 0)
                {
                    return product;
                }
            }
            return null;
        }
    }
}
