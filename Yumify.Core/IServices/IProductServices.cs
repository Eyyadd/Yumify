using Yumify.Core.Entities;
using Yumify.Repository.SpecificationEvaluator;

namespace Yumify.Core.IServices
{
    public interface IProductServices
    {
        Task<IReadOnlyList<Product>?> GetProductsAsync(GetSpecParts? getSpecParts);
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product?> AddProductAsync(Product product);
        Task<Product?> UpdateProductAsync(int ProductId, Product product);
        Task<Product?> DeleteProductAsync(int id);
        Task<int> GetCountAsync(GetSpecParts? getSpecParts);
        Task<IReadOnlyList<ProductCategory>?> GetCategoriesAsync();
        Task<ProductCategory?> GetCategoryByIdAsync(int CategoryId);
        Task<ProductCategory?> AddCategoryAsync(ProductCategory productCategory);
        Task<ProductCategory?> UpdateCategoryAsync(ProductCategory productCategory);
        Task<ProductCategory?> DeleteCategoryAsync(int categoryId);
        Task<IReadOnlyList<ProductBrand>?> GetBrandsAsync();
        Task<ProductBrand?> GetBrandByIdAsync(int BrandId);

        Task<ProductBrand?> AddBrandsAsync(ProductBrand productBrand);
        Task<ProductBrand?> UpdateBrandsAsync(ProductBrand productBrand);
        Task<ProductBrand?> DeleteBrandsAsync(int BrandId);
    }
}
